﻿using System;
using System.Diagnostics.CodeAnalysis;
using System.Reflection.Emit;

namespace InlineIL
{
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    [SuppressMessage("ReSharper", "UnusedParameter.Global")]
    public static class IL
    {
        public static void Emit(OpCode opCode)
            => Throw();

        public static void Emit(OpCode opCode, string arg)
            => Throw();

        public static void Emit(OpCode opCode, int arg)
            => Throw();

        public static void Emit(OpCode opCode, long arg)
            => Throw();

        public static void Emit(OpCode opCode, double arg)
            => Throw();

        public static void Emit(OpCode opCode, TypeRef arg)
            => Throw();

        public static void Emit(OpCode opCode, MethodRef arg)
            => Throw();

        public static void Emit(OpCode opCode, FieldRef arg)
            => Throw();

        public static void Emit(OpCode opCode, LabelRef arg)
            => Throw();

        public static void Emit(OpCode opCode, LocalRef arg)
            => Throw();

        public static void Emit(OpCode opCode, params LabelRef[] args)
            => Throw();

        public static void Emit(OpCode opCode, StandAloneMethodSig arg)
            => Throw();

        public static void DeclareLocals(params LocalVar[] locals)
            => Throw();

        public static void DeclareLocals(bool init, params LocalVar[] locals)
            => Throw();

        public static void MarkLabel(string labelName)
            => Throw();

        public static void Push<T>(T value)
            => Throw();

        public static void Push<T>(ref T value)
            => Throw();

        public static unsafe void Push(void* value)
            => Throw();

        public static Exception Unreachable()
            => throw Throw();

        public static T Return<T>()
            => throw Throw();

        internal static Exception Throw()
            => throw new InvalidOperationException("This function is meant to be replaced at compile time by InlineIL.Fody.");
    }
}
