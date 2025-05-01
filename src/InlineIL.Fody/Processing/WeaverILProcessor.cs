using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Fody;
using InlineIL.Fody.Extensions;
using InlineIL.Fody.Support;
using Mono.Cecil;
using Mono.Cecil.Cil;
using static Mono.Cecil.Cil.OperandType;

namespace InlineIL.Fody.Processing;

internal class WeaverILProcessor
{
    private const int _emittedBasicBlockId = 0; // Basic block id assigned to emitted instructions

    private readonly ILProcessor _il;
    private readonly MethodLocals _locals;
    private readonly HashSet<Instruction> _referencedInstructions;
    private readonly Dictionary<Instruction, int> _basicBlocks;

    public MethodDefinition Method { get; }

    public WeaverILProcessor(MethodDefinition method, MethodLocals locals)
    {
        Method = method;
        _locals = locals;
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
            _locals.MapMacroInstruction(instruction);
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
            operand = opCode.OperandType switch
            {
                InlineI                                      => Convert.ToInt32(operand, CultureInfo.InvariantCulture),
                InlineI8                                     => Convert.ToInt64(operand, CultureInfo.InvariantCulture),
                InlineR                                      => Convert.ToDouble(operand, CultureInfo.InvariantCulture),
                InlineVar or InlineArg                       => Convert.ToInt32(operand, CultureInfo.InvariantCulture), // It's an uint16 but Cecil expects int32
                ShortInlineI when opCode == OpCodes.Ldc_I4_S => Convert.ToSByte(operand, CultureInfo.InvariantCulture),
                ShortInlineI                                 => Convert.ToByte(operand, CultureInfo.InvariantCulture),
                ShortInlineR                                 => Convert.ToSingle(operand, CultureInfo.InvariantCulture),
                ShortInlineVar or ShortInlineArg             => Convert.ToByte(operand, CultureInfo.InvariantCulture),
                _                                            => operand
            };

            return operand switch
            {
                int value    => _locals.MapIndexInstruction(opCode, value, out var localVar) ? Create(opCode, localVar) : _il.Create(opCode, value),
                byte value   => _locals.MapIndexInstruction(opCode, value, out var localVar) ? Create(opCode, localVar) : _il.Create(opCode, value),
                sbyte value  => _il.Create(opCode, value),
                long value   => _il.Create(opCode, value),
                double value => _il.Create(opCode, value),
                short value  => _il.Create(opCode, value),
                float value  => _il.Create(opCode, value),
                string value => _il.Create(opCode, value),
                _            => throw new WeavingException($"Unexpected operand for instruction {opCode}: {operand}")
            };
        }
        catch (ArgumentException)
        {
            throw ExceptionInvalidOperand(opCode);
        }
    }

    private static WeavingException ExceptionInvalidOperand(OpCode opCode)
    {
        return opCode.OperandType switch
        {
            InlineNone                            => new WeavingException($"Opcode {opCode} does not expect an operand"),
            InlineString                          => Expected(nameof(String)),
            InlineI or ShortInlineI               => Expected(nameof(Int32)),
            InlineR or ShortInlineR               => Expected(nameof(Double)),
            InlineI8                              => Expected(nameof(Int64)),
            InlineType                            => Expected($"{KnownNames.Short.TypeRefType} or {nameof(Type)}"),
            InlineMethod                          => Expected(KnownNames.Short.MethodRefType),
            InlineField                           => Expected(KnownNames.Short.FieldRefType),
            InlineSig                             => Expected(KnownNames.Short.StandAloneMethodSigType),
            InlineArg or ShortInlineArg           => Expected("parameter name or index"), // Name mapping is handled elsewhere
            InlineVar or ShortInlineVar           => Expected("local variable name or index"), // Name mapping is handled elsewhere
            InlineBrTarget or ShortInlineBrTarget => Expected("label name"),
            InlineSwitch                          => Expected("array of label names"),
            _                                     => Expected(opCode.OperandType.ToString())
        };

        WeavingException Expected(string expected)
            => new($"Opcode {opCode} expects an operand of type {expected}");
    }
}
