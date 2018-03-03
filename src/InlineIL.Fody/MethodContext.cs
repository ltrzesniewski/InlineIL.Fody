using Fody;
using Mono.Cecil;
using Mono.Cecil.Cil;
using Mono.Collections.Generic;

namespace InlineIL.Fody
{
    internal class MethodContext
    {
        private readonly MethodDefinition _method;

        private Collection<Instruction> Instructions => _method.Body.Instructions;

        public MethodContext(MethodDefinition method)
        {
            _method = method;
        }

        public void Process()
        {
            for (var instructionIndex = 0; instructionIndex < Instructions.Count;)
            {
                var instruction = Instructions[instructionIndex];

                if (instruction.OpCode == OpCodes.Call && instruction.Operand is MethodReference calledMethod)
                {
                    switch (calledMethod.Name)
                    {
                        case KnownNames.Short.Op:
                            ProcessOpNoArg(instructionIndex);
                            break;

                        case KnownNames.Short.PushMethod:
                            ProcessPushMethod(instructionIndex);
                            break;

                        case KnownNames.Short.UnreachableMethod:
                            ProcessUnreachable(instructionIndex);
                            break;

                        default:
                            throw new WeavingException($"Unsupported method: {calledMethod.FullName}");
                    }
                }
                else
                {
                    ++instructionIndex;
                }
            }
        }

        private void ProcessOpNoArg(int instructionIndex)
        {
            var instruction = Instructions[instructionIndex];

            var opCode = OpCodeMap.FromLdsfld(instruction.Previous);

            var firstIndex = instructionIndex - 1;
            Instructions.RemoveAt(firstIndex);
            Instructions.RemoveAt(firstIndex);
            Instructions.Insert(firstIndex, Instruction.Create(opCode));
        }

        private void ProcessPushMethod(int instructionIndex)
        {
            Instructions.RemoveAt(instructionIndex);
        }

        private void ProcessUnreachable(int instructionIndex)
        {
            Instructions.RemoveAt(instructionIndex);
            var throwInstruction = Instructions[instructionIndex];

            if (throwInstruction.OpCode != OpCodes.Throw)
                throw new WeavingException("The Unreachable method should be used like this: throw IL.Unreachable();");

            Instructions.RemoveAt(instructionIndex);
            RemoveNops(instructionIndex);
        }

        private void RemoveNops(int instructionIndex)
        {
            while (instructionIndex < Instructions.Count && Instructions[instructionIndex].OpCode == OpCodes.Nop)
                Instructions.RemoveAt(instructionIndex);

            while (--instructionIndex > 0 && Instructions[instructionIndex].OpCode == OpCodes.Nop)
                Instructions.RemoveAt(instructionIndex);
        }
    }
}
