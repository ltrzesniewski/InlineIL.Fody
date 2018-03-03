using System;
using System.Diagnostics.CodeAnalysis;
using System.Reflection.Emit;

namespace InlineIL
{
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public static class IL
    {
        public static void Op(OpCode opCode)
            => Throw();

        private static void Throw()
            => throw new InvalidOperationException("This function is not meant to be replaced at compile time by InlineIL.Fody.");
    }
}
