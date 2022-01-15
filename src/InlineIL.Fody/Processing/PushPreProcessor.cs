using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using InlineIL.Fody.Extensions;
using InlineIL.Fody.Support;
using Mono.Cecil;
using Mono.Cecil.Cil;

namespace InlineIL.Fody.Processing;

internal class PushPreProcessor
{
    private readonly MethodDefinition _method;
    private readonly Dictionary<Instruction, StackState> _branchStates = new(ReferenceEqualityComparer<Instruction>.Instance);

    private PushPreProcessor(MethodDefinition method)
    {
        _method = method;
    }

    public static void ValidatePushMethods(MethodDefinition method)
    {
        if (method.Body.Instructions.Any(instruction => IsCallToPushMethod(instruction, out _)))
            new PushPreProcessor(method).Validate();
    }

    private void Validate()
    {
        InitExceptionHandlers();
        ValidateInstructions();
    }

    private void InitExceptionHandlers()
    {
        if (!_method.Body.HasExceptionHandlers)
            return;

        foreach (var handler in _method.Body.ExceptionHandlers)
        {
            // The stack should be empty at the start of protected blocks, but we can ignore that here.

            if (handler.HandlerStart != null)
            {
                _branchStates[handler.HandlerStart] = handler.HandlerType switch
                {
                    ExceptionHandlerType.Catch   => StackState.ExceptionHandlerStackState,
                    ExceptionHandlerType.Filter  => StackState.ExceptionHandlerStackState,
                    ExceptionHandlerType.Finally => StackState.FinallyOrFaultHandlerStackState,
                    ExceptionHandlerType.Fault   => StackState.FinallyOrFaultHandlerStackState,
                    _                            => throw new InstructionWeavingException(handler.HandlerStart, "Unknown exception handler type")
                };
            }

            if (handler.HandlerType == ExceptionHandlerType.Filter && handler.FilterStart != null)
                _branchStates[handler.FilterStart] = StackState.ExceptionHandlerStackState;
        }
    }

    private void ValidateInstructions()
    {
        var state = StackState.EmptyStackState;

        foreach (var instruction in _method.Body.Instructions)
        {
            if (_branchStates.TryGetValue(instruction, out var forwardBranchState))
                state = StackState.Merge(state, forwardBranchState);

            var (stackSize, unsafeToPushCount) = state;
            var isCallToPushMethod = IsCallToPushMethod(instruction, out var pushMethod);

            PopStack(instruction, ref stackSize);

            var consumesUnsafeValue = stackSize < unsafeToPushCount;

            PushStack(instruction, ref stackSize);

            if (consumesUnsafeValue && isCallToPushMethod)
                ThrowInvalidPush(instruction, pushMethod!);

            if (consumesUnsafeValue || isCallToPushMethod)
                unsafeToPushCount = stackSize;

            state = new StackState(stackSize, unsafeToPushCount);

            UpdateStackStateForForwardBranches(instruction, state);
            UpdateStackStateForNextInstruction(instruction, ref state);
        }
    }

    internal static void PopStack(Instruction instruction, ref int stackSize)
    {
        switch (instruction.OpCode.Code)
        {
            case Code.Dup: // Special-cased in GetPopCount
                --stackSize;
                return;

            case Code.Ret:
                stackSize = 0;
                return;
        }

        if (instruction.OpCode.StackBehaviourPop == StackBehaviour.PopAll)
        {
            stackSize = 0;
            return;
        }

        stackSize -= instruction.GetPopCount();
    }

    internal static void PushStack(Instruction instruction, ref int stackSize)
    {
        if (instruction.OpCode == OpCodes.Dup) // Special-cased in GetPushCount
        {
            stackSize += 2;
            return;
        }

        stackSize += instruction.GetPushCount();
    }

    private void UpdateStackStateForForwardBranches(Instruction instruction, StackState state)
    {
        switch (instruction.OpCode.OperandType)
        {
            case OperandType.InlineBrTarget:
            case OperandType.ShortInlineBrTarget:
            {
                if (instruction.Operand is Instruction operand)
                    UpdateBranchState(operand);

                break;
            }

            case OperandType.InlineSwitch:
            {
                if (instruction.Operand is Instruction?[] operands)
                {
                    foreach (var operand in operands)
                        UpdateBranchState(operand);
                }

                break;
            }
        }

        void UpdateBranchState(Instruction? targetInstruction)
        {
            if (targetInstruction is null)
                return;

            if (_branchStates.TryGetValue(targetInstruction, out var existingState))
                _branchStates[targetInstruction] = StackState.Merge(existingState, state);
            else
                _branchStates[targetInstruction] = state;
        }
    }

    private static void UpdateStackStateForNextInstruction(Instruction instruction, ref StackState state)
    {
        switch (instruction.OpCode.FlowControl)
        {
            case FlowControl.Branch:
            case FlowControl.Throw:
            case FlowControl.Return:
                state = StackState.EmptyStackState;
                break;
        }
    }

    private static bool IsCallToPushMethod(Instruction instruction, [NotNullWhen(true)] out MethodReference? method)
    {
        if (instruction.OpCode == OpCodes.Call
            && instruction.Operand is MethodReference calledMethod
            && calledMethod.DeclaringType.FullName == KnownNames.Full.IlType)
        {
            switch (calledMethod.Name)
            {
                case KnownNames.Short.PushMethod:
                case KnownNames.Short.PushInRefMethod:
                case KnownNames.Short.PushOutRefMethod:
                    method = calledMethod;
                    return true;
            }
        }

        method = null;
        return false;
    }

    [DoesNotReturn]
    private static void ThrowInvalidPush(Instruction instruction, MethodReference pushMethod)
    {
        var errorMessage = $"IL.{pushMethod.Name} cannot be used in this context, as the IL layout makes it unsafe to process.";

        if (pushMethod.GetElementMethod().FullName == "System.Void InlineIL.IL::Push(!!0)")
            errorMessage += $" You may be able to make the IL layout safe by using IL.{KnownNames.Short.EnsureLocalMethod}.";

        throw new InstructionWeavingException(instruction, errorMessage);
    }

    private readonly struct StackState
    {
        public static StackState EmptyStackState => new(0, 0);
        public static StackState ExceptionHandlerStackState => new(1, 0, true);
        public static StackState FinallyOrFaultHandlerStackState => new(0, 0, true);

        public readonly int StackSize;

        // Number of values at the bottom of the stack that should not be used (even indirectly) in a call to IL.Push
        public readonly int UnsafeToPushCount;

        private readonly bool _forcedValue;

        public StackState(int stackSize, int unsafeToPushCount)
            : this(stackSize, unsafeToPushCount, false)
        {
        }

        private StackState(int stackSize, int unsafeToPushCount, bool forcedValue)
        {
            StackSize = stackSize;
            UnsafeToPushCount = Math.Min(stackSize, unsafeToPushCount);
            _forcedValue = forcedValue;
        }

        public void Deconstruct(out int stackSize, out int unsafeToPushCount)
        {
            stackSize = StackSize;
            unsafeToPushCount = UnsafeToPushCount;
        }

        public static StackState Merge(StackState previousStackState, StackState newStackState)
        {
            if (newStackState._forcedValue)
                return newStackState;

            if (previousStackState._forcedValue)
                return previousStackState;

            if (previousStackState.StackSize != newStackState.StackSize)
                return newStackState;

            return new StackState(newStackState.StackSize, Math.Max(previousStackState.UnsafeToPushCount, newStackState.UnsafeToPushCount));
        }

        public override string ToString()
            => $"{StackSize} ({UnsafeToPushCount} unsafe)";
    }
}
