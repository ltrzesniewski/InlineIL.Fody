using System;
using System.Collections.Generic;
using InlineIL.Fody.Processing;
using InlineIL.Fody.Support;
using InlineIL.Tests.Support;
using Mono.Cecil;
using Mono.Cecil.Cil;
using Xunit.Abstractions;

namespace InlineIL.Tests.Debug
{
    public class StackStateTests
    {
        private readonly ITestOutputHelper _output;

        public StackStateTests(ITestOutputHelper output)
        {
            _output = output;
        }

        [DebugTest]
        public void CheckAllAssemblies()
        {
            var methodCount = 0;
            var invalidMethods = new List<string>();

            foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
            {
                if (assembly.IsDynamic)
                    continue;

                using var module = ModuleDefinition.ReadModule(assembly.Location);

                foreach (var type in module.GetTypes())
                {
                    foreach (var method in type.Methods)
                    {
                        if (!method.HasBody)
                            continue;

                        ++methodCount;

                        if (!CheckMethod(method))
                            invalidMethods.Add(method.ToString());
                    }
                }
            }

            _output.WriteLine($"Invalid methods: {invalidMethods.Count} / {methodCount}");

            foreach (var name in invalidMethods)
                _output.WriteLine(name);
        }

        private static bool CheckMethod(MethodDefinition method)
        {
            var branchStates = new Dictionary<Instruction, StackState>(ReferenceEqualityComparer<Instruction>.Instance);

            if (method.Body.HasExceptionHandlers)
            {
                foreach (var handler in method.Body.ExceptionHandlers)
                {
                    switch (handler.HandlerType)
                    {
                        case ExceptionHandlerType.Catch:
                            branchStates[handler.HandlerStart] = StackState.ExceptionHandlerStackState;
                            break;

                        case ExceptionHandlerType.Filter:
                            branchStates[handler.HandlerStart] = StackState.ExceptionHandlerStackState;
                            branchStates[handler.FilterStart] = StackState.ExceptionHandlerStackState;
                            break;
                    }
                }
            }

            var state = StackState.EmptyStackState;

            foreach (var instruction in method.Body.Instructions)
            {
                if (branchStates.TryGetValue(instruction, out var forwardBranchState))
                {
                    if (forwardBranchState.Priority == StackStatePriority.Forced
                        || state.Priority == StackStatePriority.IgnoreWhenThereIsAForwardBranch)
                    {
                        state = forwardBranchState;
                    }
                    else
                    {
                        if (state.StackSize != forwardBranchState.StackSize)
                            return false;
                    }
                }

                var stackSize = state.StackSize;

                PushPreProcessor.PopStack(instruction, ref stackSize);

                if (stackSize < 0)
                    return false;

                PushPreProcessor.PushStack(instruction, ref stackSize);

                state = new StackState(stackSize, StackStatePriority.Normal);

                switch (instruction.OpCode.OperandType)
                {
                    case OperandType.InlineBrTarget:
                    case OperandType.ShortInlineBrTarget:
                    {
                        if (instruction.Operand is Instruction operand)
                            branchStates[operand] = state;

                        break;
                    }

                    case OperandType.InlineSwitch:
                    {
                        if (instruction.Operand is Instruction?[] operands)
                        {
                            foreach (var operand in operands)
                            {
                                if (operand != null)
                                    branchStates[operand] = state;
                            }
                        }

                        break;
                    }
                }

                switch (instruction.OpCode.FlowControl)
                {
                    case FlowControl.Branch:
                    case FlowControl.Throw:
                    case FlowControl.Return:
                        state = StackState.EmptyStackStateAfterUnconditionalBranch;
                        break;
                }
            }

            return true;
        }

        private readonly struct StackState
        {
            public static StackState EmptyStackState => new(0, StackStatePriority.Normal);
            public static StackState EmptyStackStateAfterUnconditionalBranch => new(0, StackStatePriority.IgnoreWhenThereIsAForwardBranch);
            public static StackState ExceptionHandlerStackState => new(1, StackStatePriority.Forced);

            public readonly int StackSize;
            public readonly StackStatePriority Priority;

            public StackState(int stackSize, StackStatePriority priority)
            {
                StackSize = stackSize;
                Priority = priority;
            }
        }

        private enum StackStatePriority
        {
            Normal,
            IgnoreWhenThereIsAForwardBranch,
            Forced
        }
    }
}
