using System.Linq;
using Fody;
using Mono.Cecil;
using Mono.Cecil.Cil;
using Mono.Collections.Generic;

namespace InlineIL.Fody
{
    internal class MethodContext
    {
        private readonly MethodDefinition _method;
        private readonly ILProcessor _il;

        private Collection<Instruction> Instructions => _method.Body.Instructions;

        public MethodContext(MethodDefinition method)
        {
            _method = method;
            _il = _method.Body.GetILProcessor();
        }

        public void Process()
        {
            var instruction = Instructions.FirstOrDefault();

            while (instruction != null)
            {
                var nextInstruction = instruction.Next;

                if (instruction.OpCode == OpCodes.Call && instruction.Operand is MethodReference calledMethod)
                {
                    switch (calledMethod.Name)
                    {
                        case KnownNames.Short.Op:
                            ProcessOpNoArg(instruction);
                            break;

                        case KnownNames.Short.PushMethod:
                            ProcessPushMethod(instruction);
                            break;

                        case KnownNames.Short.UnreachableMethod:
                            ProcessUnreachable(instruction);
                            break;

                        default:
                            throw new WeavingException($"Unsupported method: {calledMethod.FullName}");
                    }
                }

                instruction = nextInstruction;
            }
        }

        private void ProcessOpNoArg(Instruction instruction)
        {
            var args = instruction.GetArgumentPushInstructions();
            var opCode = ConsumeArgOpCode(args[0]);

            _il.Replace(instruction, InstructionHelper.Create(opCode));
        }

        private void ProcessPushMethod(Instruction instruction)
        {
            _il.Remove(instruction);
        }

        private void ProcessUnreachable(Instruction instruction)
        {
            var throwInstruction = instruction.NextSkipNops();
            if (throwInstruction.OpCode != OpCodes.Throw)
                throw new WeavingException("The Unreachable method should be used like this: throw IL.Unreachable();");

            _il.Remove(instruction);
            Instructions.RemoveNopsAround(throwInstruction);
            _il.Remove(throwInstruction);
        }

        private OpCode ConsumeArgOpCode(Instruction instruction)
        {
            var opCode = OpCodeMap.FromLdsfld(instruction);
            _il.Remove(instruction);
            return opCode;
        }
    }
}
