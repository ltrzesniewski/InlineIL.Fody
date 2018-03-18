using Fody;
using Mono.Cecil;
using Mono.Cecil.Cil;

namespace InlineIL.Fody
{
    internal class InstructionWeavingException : WeavingException
    {
        public Instruction Instruction { get; }

        public InstructionWeavingException(Instruction instruction, string message)
            : base(message)
        {
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
