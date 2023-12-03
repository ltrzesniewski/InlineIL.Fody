using Fody;
using Mono.Cecil.Cil;

// ReSharper disable once RedundantDisableWarningComment
#pragma warning disable CA1032

namespace InlineIL.Fody.Support;

internal class InstructionWeavingException(Instruction? instruction, string message) : WeavingException(message)
{
    public Instruction? Instruction { get; } = instruction;
}
