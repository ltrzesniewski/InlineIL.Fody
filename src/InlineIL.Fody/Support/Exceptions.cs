using Fody;
using Mono.Cecil.Cil;

// ReSharper disable once RedundantDisableWarningComment
#pragma warning disable CA1032

namespace InlineIL.Fody.Support;

internal class InstructionWeavingException : WeavingException
{
    public Instruction? Instruction { get; }

    public InstructionWeavingException(Instruction? instruction, string message)
        : base(message)
    {
        Instruction = instruction;
    }
}
