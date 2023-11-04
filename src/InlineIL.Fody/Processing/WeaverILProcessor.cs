using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Fody;
using InlineIL.Fody.Extensions;
using InlineIL.Fody.Model;
using InlineIL.Fody.Support;
using Mono.Cecil;
using Mono.Cecil.Cil;

namespace InlineIL.Fody.Processing;

internal class WeaverILProcessor
{
    private const int _emittedBasicBlockId = 0; // Basic block id assigned to emitted instructions

    private readonly ILogger _log;
    private readonly ILProcessor _il;
    private readonly HashSet<Instruction> _referencedInstructions;
    private readonly Dictionary<Instruction, int> _basicBlocks;

    public MethodDefinition Method { get; }

    public MethodLocals? Locals { get; private set; }

    public WeaverILProcessor(MethodDefinition method, ILogger log)
    {
        Method = method;
        _log = log;
        _il = method.Body.GetILProcessor();
        _referencedInstructions = GetAllReferencedInstructions();
        _basicBlocks = SplitToBasicBlocks(method.Body.Instructions, _referencedInstructions);
    }

    public void Remove(Instruction instruction)
    {
        var newRef = instruction.Next ?? instruction.Previous ?? throw new InstructionWeavingException(instruction, "Cannot remove single instruction of method");
        _il.Remove(instruction);
        UpdateReferences(instruction, newRef);
    }

    public void Remove(params Instruction[] instructions)
    {
        foreach (var instruction in instructions)
            Remove(instruction);
    }

    public void Replace(Instruction oldInstruction, Instruction newInstruction, bool mapToBasicBlock = false)
    {
        _il.Replace(oldInstruction, newInstruction);
        UpdateReferences(oldInstruction, newInstruction);

        if (mapToBasicBlock)
            _basicBlocks[newInstruction] = GetBasicBlock(oldInstruction);
    }

    public void DeclareLocals(IEnumerable<LocalVarBuilder> locals, SequencePoint? sequencePoint)
    {
        if (Locals != null)
            throw new WeavingException("Local variables have already been declared for this method");

        Locals = new MethodLocals(_il.Body.Method, locals, _log, sequencePoint);
    }

    public HashSet<Instruction> GetAllReferencedInstructions()
    {
        var refs = new HashSet<Instruction>(ReferenceEqualityComparer<Instruction>.Instance);

        foreach (var instruction in _il.Body.Instructions)
            refs.UnionWith(instruction.GetReferencedInstructions());

        if (_il.Body.HasExceptionHandlers)
        {
            foreach (var handler in _il.Body.ExceptionHandlers)
                refs.UnionWith(handler.GetReferencedInstructions());
        }

        return refs;
    }

    private static Dictionary<Instruction, int> SplitToBasicBlocks(IEnumerable<Instruction> instructions, HashSet<Instruction> referencedInstructions)
    {
        var result = new Dictionary<Instruction, int>(ReferenceEqualityComparer<Instruction>.Instance);
        var basicBlock = _emittedBasicBlockId + 1;

        foreach (var instruction in instructions)
        {
            if (referencedInstructions.Contains(instruction))
                ++basicBlock;

            result[instruction] = basicBlock;

            switch (instruction.OpCode.FlowControl)
            {
                case FlowControl.Branch:
                case FlowControl.Cond_Branch:
                case FlowControl.Return:
                case FlowControl.Throw:
                    ++basicBlock;
                    break;

                case FlowControl.Call:
                    if (instruction.OpCode == OpCodes.Jmp)
                        ++basicBlock;
                    break;
            }
        }

        return result;
    }

    internal int GetBasicBlock(Instruction instruction)
        => _basicBlocks.GetValueOrDefault(instruction);

    public Instruction[] GetArgumentPushInstructionsInSameBasicBlock(Instruction instruction)
    {
        var result = instruction.GetArgumentPushInstructions();
        var basicBlock = GetBasicBlock(instruction);

        foreach (var argInstruction in result)
            EnsureSameBasicBlock(argInstruction, basicBlock);

        return result;
    }

    public Instruction GetPrevSkipNopsInSameBasicBlock(Instruction instruction)
    {
        var prev = instruction.PrevSkipNopsRequired();
        EnsureSameBasicBlock(prev, instruction);
        return prev;
    }

    public void EnsureSameBasicBlock(Instruction checkedInstruction, Instruction referenceInstruction)
        => EnsureSameBasicBlock(checkedInstruction, GetBasicBlock(referenceInstruction));

    private void EnsureSameBasicBlock(Instruction instruction, int basicBlock)
    {
        if (GetBasicBlock(instruction) != basicBlock)
            throw new InstructionWeavingException(instruction, "An unconditional expression was expected.");
    }

    public bool TryMergeBasicBlocks(Instruction sourceBasicBlock, params Instruction[] basicBlocksToUpdate)
    {
        var sourceBlock = GetBasicBlock(sourceBasicBlock);
        var targetBlocks = basicBlocksToUpdate.Select(GetBasicBlock).ToHashSet();

        // Sanity check: don't try to analyze emitted instructions, let the subsequent basic block check fail
        if (sourceBlock == _emittedBasicBlockId || targetBlocks.Contains(_emittedBasicBlockId))
            return false;

        targetBlocks.Remove(sourceBlock);
        if (targetBlocks.Count == 0)
            return true;

        var sourceBlockInstructions = _il.Body.Instructions
                                         .Where(i => GetBasicBlock(i) == sourceBlock)
                                         .ToList();

        var instructionsToUpdate = _basicBlocks.Where(i => targetBlocks.Contains(i.Value))
                                               .Select(i => i.Key)
                                               .ToList();

        // Make sure the merge is valid
        if (GetAllReferencedInstructions().Overlaps(instructionsToUpdate))
            return false;

        if (sourceBlockInstructions.SelectMany(i => i.GetReferencedInstructions()).Any())
            return false;

        foreach (var instruction in instructionsToUpdate)
            _basicBlocks[instruction] = sourceBlock;

        return true;
    }

    private void UpdateReferences(Instruction oldInstruction, Instruction newInstruction)
    {
        if (!_referencedInstructions.Contains(oldInstruction))
            return;

        if (_il.Body.HasExceptionHandlers)
        {
            foreach (var handler in _il.Body.ExceptionHandlers)
            {
                if (handler.TryStart == oldInstruction)
                    handler.TryStart = newInstruction;

                if (handler.TryEnd == oldInstruction)
                    handler.TryEnd = newInstruction;

                if (handler.FilterStart == oldInstruction)
                    handler.FilterStart = newInstruction;

                if (handler.HandlerStart == oldInstruction)
                    handler.HandlerStart = newInstruction;

                if (handler.HandlerEnd == oldInstruction)
                    handler.HandlerEnd = newInstruction;
            }
        }

        foreach (var instruction in _il.Body.Instructions)
        {
            switch (instruction.Operand)
            {
                case Instruction target when target == oldInstruction:
                    instruction.Operand = newInstruction;
                    break;

                case Instruction[] targets:
                    for (var i = 0; i < targets.Length; ++i)
                    {
                        if (targets[i] == oldInstruction)
                            targets[i] = newInstruction;
                    }

                    break;
            }
        }

        _referencedInstructions.Remove(oldInstruction);
        _referencedInstructions.Add(newInstruction);
    }

    public void RemoveNopsAround(Instruction? instruction)
    {
        RemoveNopsBefore(instruction);
        RemoveNopsAfter(instruction);
    }

    private void RemoveNopsBefore(Instruction? instruction)
    {
        var currentInstruction = instruction?.Previous;
        while (currentInstruction != null && currentInstruction.OpCode == OpCodes.Nop)
        {
            var prev = currentInstruction.Previous;
            Remove(currentInstruction);
            currentInstruction = prev;
        }
    }

    public void RemoveNopsAfter(Instruction? instruction)
    {
        var currentInstruction = instruction?.Next;
        while (currentInstruction != null && currentInstruction.OpCode == OpCodes.Nop)
        {
            var next = currentInstruction.Next;
            Remove(currentInstruction);
            currentInstruction = next;
        }
    }

    public Instruction Create(OpCode opCode)
    {
        try
        {
            var instruction = _il.Create(opCode);
            MethodLocals.MapMacroInstruction(Locals, instruction);
            return instruction;
        }
        catch (ArgumentException)
        {
            throw ExceptionInvalidOperand(opCode);
        }
    }

    public Instruction Create(OpCode opCode, TypeReference typeRef)
    {
        try
        {
            return _il.Create(opCode, typeRef);
        }
        catch (ArgumentException)
        {
            throw ExceptionInvalidOperand(opCode);
        }
    }

    public Instruction Create(OpCode opCode, MethodReference methodRef)
    {
        try
        {
            return _il.Create(opCode, methodRef);
        }
        catch (ArgumentException)
        {
            throw ExceptionInvalidOperand(opCode);
        }
    }

    public Instruction Create(OpCode opCode, FieldReference fieldRef)
    {
        try
        {
            return _il.Create(opCode, fieldRef);
        }
        catch (ArgumentException)
        {
            throw ExceptionInvalidOperand(opCode);
        }
    }

    public Instruction Create(OpCode opCode, Instruction instruction)
    {
        try
        {
            var result = _il.Create(opCode, instruction);
            _referencedInstructions.Add(instruction);
            return result;
        }
        catch (ArgumentException)
        {
            throw ExceptionInvalidOperand(opCode);
        }
    }

    public Instruction Create(OpCode opCode, Instruction[] instructions)
    {
        try
        {
            var result = _il.Create(opCode, instructions);
            _referencedInstructions.UnionWith(instructions.Where(i => i != null));
            return result;
        }
        catch (ArgumentException)
        {
            throw ExceptionInvalidOperand(opCode);
        }
    }

    public Instruction Create(OpCode opCode, VariableDefinition variableDef)
    {
        try
        {
            return _il.Create(opCode, variableDef);
        }
        catch (ArgumentException)
        {
            throw ExceptionInvalidOperand(opCode);
        }
    }

    public Instruction Create(OpCode opCode, CallSite callSite)
    {
        try
        {
            return _il.Create(opCode, callSite);
        }
        catch (ArgumentException)
        {
            throw ExceptionInvalidOperand(opCode);
        }
    }

    public Instruction CreateConst(OpCode opCode, object operand)
    {
        try
        {
            switch (opCode.OperandType)
            {
                case OperandType.InlineI:
                    operand = Convert.ToInt32(operand, CultureInfo.InvariantCulture);
                    break;

                case OperandType.InlineI8:
                    operand = Convert.ToInt64(operand, CultureInfo.InvariantCulture);
                    break;

                case OperandType.InlineR:
                    operand = Convert.ToDouble(operand, CultureInfo.InvariantCulture);
                    break;

                case OperandType.InlineVar:
                case OperandType.InlineArg:
                    // It's an uint16 but Cecil expects int32
                    operand = Convert.ToInt32(operand, CultureInfo.InvariantCulture);
                    break;

                case OperandType.ShortInlineI:
                    operand = opCode == OpCodes.Ldc_I4_S
                        ? Convert.ToSByte(operand, CultureInfo.InvariantCulture)
                        : Convert.ToByte(operand, CultureInfo.InvariantCulture);
                    break;

                case OperandType.ShortInlineR:
                    operand = Convert.ToSingle(operand, CultureInfo.InvariantCulture);
                    break;

                case OperandType.ShortInlineVar:
                case OperandType.ShortInlineArg:
                    operand = Convert.ToByte(operand, CultureInfo.InvariantCulture);
                    break;
            }

            switch (operand)
            {
                case int value:
                {
                    if (MethodLocals.MapIndexInstruction(Locals, ref opCode, value, out var localVar))
                        return Create(opCode, localVar);

                    return _il.Create(opCode, value);
                }

                case byte value:
                {
                    if (MethodLocals.MapIndexInstruction(Locals, ref opCode, value, out var localVar))
                        return Create(opCode, localVar);

                    return _il.Create(opCode, value);
                }

                case sbyte value:
                    return _il.Create(opCode, value);
                case long value:
                    return _il.Create(opCode, value);
                case double value:
                    return _il.Create(opCode, value);
                case short value:
                    return _il.Create(opCode, value);
                case float value:
                    return _il.Create(opCode, value);
                case string value:
                    return _il.Create(opCode, value);

                default:
                    throw new WeavingException($"Unexpected operand for instruction {opCode}: {operand}");
            }
        }
        catch (ArgumentException)
        {
            throw ExceptionInvalidOperand(opCode);
        }
    }

    private static WeavingException ExceptionInvalidOperand(OpCode opCode)
    {
        switch (opCode.OperandType)
        {
            case OperandType.InlineNone:
                return new WeavingException($"Opcode {opCode} does not expect an operand");

            case OperandType.InlineBrTarget:
            case OperandType.ShortInlineBrTarget:
                return ExpectedOperand("label name");

            case OperandType.InlineField:
                return ExpectedOperand(KnownNames.Short.FieldRefType);

            case OperandType.InlineI:
            case OperandType.ShortInlineI:
            case OperandType.InlineArg:
            case OperandType.ShortInlineArg:
                return ExpectedOperand(nameof(Int32));

            case OperandType.InlineI8:
                return ExpectedOperand(nameof(Int64));

            case OperandType.InlineMethod:
                return ExpectedOperand(KnownNames.Short.MethodRefType);

            case OperandType.InlineR:
            case OperandType.ShortInlineR:
                return ExpectedOperand(nameof(Double));

            case OperandType.InlineSig:
                return ExpectedOperand(KnownNames.Short.StandAloneMethodSigType);

            case OperandType.InlineString:
                return ExpectedOperand(nameof(String));

            case OperandType.InlineSwitch:
                return ExpectedOperand("array of label names");

            case OperandType.InlineType:
                return ExpectedOperand($"{KnownNames.Short.TypeRefType} or {nameof(Type)}");

            case OperandType.InlineVar:
            case OperandType.ShortInlineVar:
                return ExpectedOperand("local variable name or index");

            default:
                return ExpectedOperand(opCode.OperandType.ToString());
        }

        WeavingException ExpectedOperand(string expected)
            => new($"Opcode {opCode} expects an operand of type {expected}");
    }
}
