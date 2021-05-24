using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
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
            var assemblyCount = 0;
            var methodCount = 0;
            var invalidMethods = new List<string>();

            foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
            {
                if (assembly.IsDynamic)
                    continue;

                using var asm = AssemblyDefinition.ReadAssembly(assembly.Location);
                ++assemblyCount;

                foreach (var module in asm.Modules)
                {
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
            }

            _output.WriteLine(RuntimeInformation.FrameworkDescription);
            _output.WriteLine($"Invalid methods: {invalidMethods.Count} / {methodCount} in {assemblyCount} assemblies");

            foreach (var name in invalidMethods)
                _output.WriteLine(name);

            invalidMethods.Count.ShouldEqual(0);
        }

        private static bool CheckMethod(MethodDefinition method)
        {
            var branchStates = new Dictionary<Instruction, StackState>(ReferenceEqualityComparer<Instruction>.Instance);

            if (method.Body.HasExceptionHandlers)
            {
                foreach (var handler in method.Body.ExceptionHandlers)
                {
                    branchStates[handler.TryStart] = StackState.StartOfProtectedBlockStackState;

                    switch (handler.HandlerType)
                    {
                        case ExceptionHandlerType.Catch:
                            branchStates[handler.HandlerStart] = StackState.ExceptionHandlerStackState;
                            break;

                        case ExceptionHandlerType.Filter:
                            branchStates[handler.HandlerStart] = StackState.ExceptionHandlerStackState;
                            branchStates[handler.FilterStart] = StackState.ExceptionHandlerStackState;
                            break;

                        case ExceptionHandlerType.Finally:
                        case ExceptionHandlerType.Fault:
                            branchStates[handler.HandlerStart] = StackState.FinallyOrFaultHandlerStackState;
                            break;
                    }
                }
            }

            var state = StackState.InitialStackState;

            foreach (var instruction in method.Body.Instructions)
            {
                if (branchStates.TryGetValue(instruction, out var forwardBranchState))
                {
                    if (forwardBranchState.Priority == StackStatePriority.Forced
                        || state.Priority == StackStatePriority.IgnoreWhenFollowedByTargetOfForwardBranch)
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
                        if (instruction.Operand is not Instruction operand)
                            return false;

                        if (!AddBranchState(operand, state))
                            return false;

                        break;
                    }

                    case OperandType.InlineSwitch:
                    {
                        if (instruction.Operand is not Instruction[] operands)
                            return false;

                        foreach (var operand in operands)
                        {
                            if (!AddBranchState(operand, state))
                                return false;
                        }

                        break;
                    }
                }

                switch (instruction.OpCode.FlowControl)
                {
                    case FlowControl.Branch:
                    case FlowControl.Throw:
                    case FlowControl.Return:
                        state = StackState.PostUnconditionalBranchStackState;
                        break;
                }
            }

            return true;

            bool AddBranchState(Instruction targetInstruction, StackState branchState)
            {
                if (branchStates.TryGetValue(targetInstruction, out var existingState))
                {
                    if (existingState.StackSize != branchState.StackSize)
                        return false;
                }
                else
                {
                    branchStates[targetInstruction] = branchState;
                }

                return true;
            }
        }

        private readonly struct StackState
        {
            public static StackState InitialStackState => new(0, StackStatePriority.Normal);
            public static StackState PostUnconditionalBranchStackState => new(0, StackStatePriority.IgnoreWhenFollowedByTargetOfForwardBranch);
            public static StackState StartOfProtectedBlockStackState => new(0, StackStatePriority.Normal);
            public static StackState ExceptionHandlerStackState => new(1, StackStatePriority.Forced);
            public static StackState FinallyOrFaultHandlerStackState => new(0, StackStatePriority.Forced);

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
            IgnoreWhenFollowedByTargetOfForwardBranch,
            Forced
        }
    }
}
