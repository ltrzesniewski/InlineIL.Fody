using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using InlineIL.Fody.Extensions;
using InlineIL.Fody.Support;
using Mono.Cecil;
using Mono.Cecil.Cil;

namespace InlineIL.Fody.Processing
{
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
                if (handler.FilterStart != null)
                    _branchStates[handler.FilterStart] = new StackState(1, 0);

                if (handler.HandlerStart != null)
                    _branchStates[handler.HandlerStart] = new StackState(1, 0);
            }
        }

        private void ValidateInstructions()
        {
            var state = new StackState(0, 0);

            foreach (var instruction in _method.Body.Instructions)
            {
                if (_branchStates.TryGetValue(instruction, out var branchState))
                    state = branchState;

                var stackSize = state.StackSize;
                var invalidItems = state.InvalidForPushCount;

                var isCallToPushMethod = IsCallToPushMethod(instruction, out var pushMethodName);

                PopStack(instruction, ref stackSize);

                var consumesInvalidItem = stackSize < invalidItems;

                PushStack(instruction, ref stackSize);

                if (consumesInvalidItem && isCallToPushMethod)
                    throw new InstructionWeavingException(instruction, $"IL.{pushMethodName} cannot be used in this context, as the IL layout makes it unsafe to process");

                if (consumesInvalidItem || isCallToPushMethod)
                    invalidItems = stackSize;

                state = new StackState(stackSize, invalidItems);

                UpdateBranchStates(instruction, state);
                UpdateNextInstructionState(instruction, ref state);
            }
        }

        private static void PopStack(Instruction instruction, ref int stackSize)
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

        private static void PushStack(Instruction instruction, ref int stackSize)
        {
            if (instruction.OpCode == OpCodes.Dup) // Special-cased in GetPushCount
            {
                stackSize += 2;
                return;
            }

            stackSize += instruction.GetPushCount();
        }

        private void UpdateBranchStates(Instruction instruction, StackState state)
        {
            switch (instruction.OpCode.OperandType)
            {
                case OperandType.InlineBrTarget:
                case OperandType.ShortInlineBrTarget:
                {
                    if (instruction.Operand is Instruction operand)
                        _branchStates[operand] = state;

                    break;
                }

                case OperandType.InlineSwitch:
                {
                    if (instruction.Operand is Instruction?[] operands)
                    {
                        foreach (var operand in operands)
                        {
                            if (operand != null)
                                _branchStates[operand] = state;
                        }
                    }

                    break;
                }
            }
        }

        private static void UpdateNextInstructionState(Instruction instruction, ref StackState state)
        {
            switch (instruction.OpCode.FlowControl)
            {
                case FlowControl.Branch:
                case FlowControl.Throw:
                case FlowControl.Return:
                    state = new StackState(0, 0);
                    break;
            }
        }

        private static bool IsCallToPushMethod(Instruction instruction, [NotNullWhen(true)] out string? methodName)
        {
            methodName = null;

            if (instruction.OpCode == OpCodes.Call
                && instruction.Operand is MethodReference calledMethod
                && calledMethod.DeclaringType.FullName == KnownNames.Full.IlType)
            {
                switch (calledMethod.Name)
                {
                    case KnownNames.Short.PushMethod:
                    case KnownNames.Short.PushInRefMethod:
                    case KnownNames.Short.PushOutRefMethod:
                        methodName = calledMethod.Name;
                        return true;

                    default:
                        return false;
                }
            }

            return false;
        }

        private readonly struct StackState
        {
            public readonly int StackSize;

            // Number of slots at the bottom of the stack that should have no impact on a call to Push
            public readonly int InvalidForPushCount;

            public StackState(int stackSize, int invalidForPushCount)
            {
                StackSize = stackSize;
                InvalidForPushCount = Math.Min(stackSize, invalidForPushCount);
            }

            public override string ToString()
                => $"{StackSize} ({InvalidForPushCount} invalid)";
        }
    }
}
