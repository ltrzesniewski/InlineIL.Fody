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
        public void CheckCoreAssembly()
        {
            using var assembly = ModuleDefinition.ReadModule(typeof(object).Assembly.Location);

            var methodCount = 0;
            var invalidMethods = new List<string>();

            foreach (var type in assembly.GetTypes())
            {
                foreach (var method in type.Methods)
                {
                    ++methodCount;

                    if (!CheckMethod(method))
                        invalidMethods.Add(method.ToString());
                }
            }

            _output.WriteLine($"Invalid methods: {invalidMethods.Count} / {methodCount}");

            foreach (var name in invalidMethods)
                _output.WriteLine(name);
        }

        private static bool CheckMethod(MethodDefinition method)
        {
            var branchStates = new Dictionary<Instruction, StackState>(ReferenceEqualityComparer<Instruction>.Instance);

            if (!method.HasBody)
                return true;

            if (method.Body.HasExceptionHandlers)
            {
                foreach (var handler in method.Body.ExceptionHandlers)
                {
                    if (handler.FilterStart != null)
                        branchStates[handler.FilterStart] = StackState.ExceptionHandlerStackState;

                    if (handler.HandlerStart != null)
                        branchStates[handler.HandlerStart] = StackState.ExceptionHandlerStackState;
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
