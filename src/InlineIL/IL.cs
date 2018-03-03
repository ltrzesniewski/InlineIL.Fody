using System;
using System.Diagnostics.CodeAnalysis;
using System.Reflection.Emit;

namespace InlineIL
{
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    [SuppressMessage("ReSharper", "UnusedParameter.Global")]
    public static class IL
    {
        public static void Op(OpCode opCode)
            => Throw();

        public static void Push<T>(T value)
            => Throw();

        public static Exception Unreachable()
            => Throw<Exception>();

        private static void Throw()
            => throw new InvalidOperationException("This function is meant to be replaced at compile time by InlineIL.Fody.");

        private static TReturn Throw<TReturn>()
        {
            Throw();
            return default;
        }
    }
}
