using Fody;
using Mono.Cecil;
using Mono.Cecil.Cil;

namespace InlineIL.Fody
{
    internal class InstructionWeavingException : WeavingException
    {
        public MethodDefinition Method { get; }
        public Instruction Instruction { get; }

        public InstructionWeavingException(MethodDefinition method, Instruction instruction, string message)
            : base(message)
        {
            Method = method;
            Instruction = instruction;
        }
    }

    internal class SequencePointWeavingException : WeavingException
    {
        public SequencePoint SequencePoint { get; }

        public SequencePointWeavingException(SequencePoint sequencePoint, string message)
            : base(message)
        {
            SequencePoint = sequencePoint;
        }
    }
}
