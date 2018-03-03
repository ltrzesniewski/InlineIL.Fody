using Mono.Cecil;
using Mono.Cecil.Cil;
using Mono.Collections.Generic;

namespace InlineIL.Fody
{
    internal class MethodContext
    {
        private readonly MethodDefinition _method;
        private readonly KnownReferences _refs;

        private Collection<Instruction> Instructions => _method.Body.Instructions;

        public MethodContext(MethodDefinition method, KnownReferences refs)
        {
            _method = method;
            _refs = refs;
        }

        public void Process()
        {
            for (var i = 0; i < Instructions.Count; ++i)
            {
                var instruction = Instructions[i];

                if (instruction.OpCode == OpCodes.Call && instruction.Operand is MethodReference calledMethod)
                {
                    switch (calledMethod.FullName)
                    {
                        case var name when name == _refs.OpMethodNoArg.FullName:
                            ProcessOpNoArg(i);
                            break;
                    }
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
    }
}
