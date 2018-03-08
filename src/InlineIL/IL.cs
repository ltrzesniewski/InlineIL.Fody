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

        public static void Op(OpCode opCode, string arg)
            => Throw();

        public static void Op(OpCode opCode, int arg)
            => Throw();

        public static void Op(OpCode opCode, long arg)
            => Throw();

        public static void Op(OpCode opCode, double arg)
            => Throw();

        public static void Op(OpCode opCode, TypeRef arg)
            => Throw();

        public static void Op(OpCode opCode, MethodRef arg)
            => Throw();

        public static void Op(OpCode opCode, LabelRef arg)
            => Throw();

        public static void Label(string labelName)
            => Throw();

        public static void Push<T>(T value)
            => Throw();

        public static void Push<T>(ref T value)
            => Throw();

        public static Exception Unreachable()
            => throw Throw();

        public static T Return<T>()
            => throw Throw();

        internal static Exception Throw()
            => throw new InvalidOperationException("This function is meant to be replaced at compile time by InlineIL.Fody.");
    }
}
