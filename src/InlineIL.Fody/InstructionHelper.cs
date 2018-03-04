using System;
using Fody;
using Mono.Cecil.Cil;

namespace InlineIL.Fody
{
    internal static class InstructionHelper
    {
        public static Instruction Create(OpCode opCode)
        {
            try
            {
                return Instruction.Create(opCode);
            }
            catch (ArgumentException)
            {
                throw new WeavingException($"Invalid instruction/operand combination: {opCode}");
            }
        }
    }
}
