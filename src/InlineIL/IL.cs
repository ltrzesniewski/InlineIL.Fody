using System;
using System.Diagnostics.CodeAnalysis;
using System.Reflection.Emit;

namespace InlineIL
{
    /// <summary>
    /// Injects IL code to the calling method, using InlineIL.Fody.
    /// All method calls are replaced at weaving time.
    /// </summary>
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    [SuppressMessage("ReSharper", "UnusedParameter.Global")]
    public static class IL
    {
        /// <summary>
        /// Emits an IL instruction.
        /// </summary>
        /// <param name="opCode">The instruction opcode to emit.</param>
        public static void Emit(OpCode opCode)
            => Throw();

        /// <summary>
        /// Emits an IL instruction.
        /// </summary>
        /// <param name="opCode">The instruction opcode to emit.</param>
        /// <param name="arg">The instruction operand.</param>
        public static void Emit(OpCode opCode, string arg)
            => Throw();

        /// <summary>
        /// Emits an IL instruction.
        /// </summary>
        /// <param name="opCode">The instruction opcode to emit.</param>
        /// <param name="arg">The instruction operand.</param>
        public static void Emit(OpCode opCode, int arg)
            => Throw();

        /// <summary>
        /// Emits an IL instruction.
        /// </summary>
        /// <param name="opCode">The instruction opcode to emit.</param>
        /// <param name="arg">The instruction operand.</param>
        public static void Emit(OpCode opCode, long arg)
            => Throw();

        /// <summary>
        /// Emits an IL instruction.
        /// </summary>
        /// <param name="opCode">The instruction opcode to emit.</param>
        /// <param name="arg">The instruction operand.</param>
        public static void Emit(OpCode opCode, double arg)
            => Throw();

        /// <summary>
        /// Emits an IL instruction.
        /// </summary>
        /// <param name="opCode">The instruction opcode to emit.</param>
        /// <param name="arg">
        /// The instruction operand representing a type.
        /// Note that <see cref="Type" /> is implicitly convertible to <see cref="TypeRef"/>.
        /// </param>
        public static void Emit(OpCode opCode, TypeRef arg)
            => Throw();

        /// <summary>
        /// Emits an IL instruction.
        /// </summary>
        /// <param name="opCode">The instruction opcode to emit.</param>
        /// <param name="arg">The instruction operand representing a method.</param>
        public static void Emit(OpCode opCode, MethodRef arg)
            => Throw();

        /// <summary>
        /// Emits an IL instruction.
        /// </summary>
        /// <param name="opCode">The instruction opcode to emit.</param>
        /// <param name="arg">The instruction operand representing a field.</param>
        public static void Emit(OpCode opCode, FieldRef arg)
            => Throw();

        /// <summary>
        /// Emits an IL instruction.
        /// </summary>
        /// <param name="opCode">The instruction opcode to emit.</param>
        /// <param name="arg">The instruction operand representing a label declared with <see cref="MarkLabel"/>.</param>
        public static void Emit(OpCode opCode, LabelRef arg)
            => Throw();

        /// <summary>
        /// Emits an IL instruction.
        /// </summary>
        /// <param name="opCode">The instruction opcode to emit.</param>
        /// <param name="arg">The instruction operand representing a local variable decalred with <see cref="DeclareLocals(InlineIL.LocalVar[])"/>.</param>
        public static void Emit(OpCode opCode, LocalRef arg)
            => Throw();

        /// <summary>
        /// Emits a switch IL instruction.
        /// </summary>
        /// <param name="opCode">The instruction opcode to emit, must be <see cref="OpCodes.Switch"/>.</param>
        /// <param name="args">The instruction operand representing the targets of the switch declared with <see cref="MarkLabel"/>.</param>
        public static void Emit(OpCode opCode, params LabelRef[] args)
            => Throw();

        /// <summary>
        /// Emits a calli IL instruction.
        /// </summary>
        /// <param name="opCode">The instruction opcode to emit, must be <see cref="OpCodes.Calli"/>.</param>
        /// <param name="arg">The instruction operand representing the call signature.</param>
        public static void Emit(OpCode opCode, StandAloneMethodSig arg)
            => Throw();

        /// <summary>
        /// Declares local variables.
        /// These variables are appended to the ones already declared by the compiler, but indexes of emitted ldloc/stloc instructions are adjusted to account for that.
        /// </summary>
        /// <param name="locals">The list of local variable declarations.</param>
        public static void DeclareLocals(params LocalVar[] locals)
            => Throw();

        /// <summary>
        /// Declares local variables.
        /// These variables are appended to the ones already declared by the compiler, but indexes of emitted ldloc/stloc instructions are adjusted to account for that.
        /// </summary>
        /// <param name="init">
        /// Flag which specifies if the local variables should be zero initialized. The default value is <c>true</c>.
        /// Setting this to <c>false</c> will cause the method to be unverifiable. It will also affect the initialization of locals emitted by the compiler.
        /// </param>
        /// <param name="locals">The list of local variable declarations.</param>
        public static void DeclareLocals(bool init, params LocalVar[] locals)
            => Throw();

        /// <summary>
        /// Marks the current code position with the given label.
        /// </summary>
        /// <param name="labelName">The label name.</param>
        public static void MarkLabel(string labelName)
            => Throw();

        /// <summary>
        /// Pushes a value onto the evaluation stack.
        /// The <paramref name="value"/> parameter should be a constant or an argument.
        /// Other values will either work correctly or cause a weaving-time error, depending on the surrounding code.
        /// </summary>
        /// <typeparam name="T">The type of the value to push.</typeparam>
        /// <param name="value">The value to push.</param>
        public static void Push<T>(T value)
            => Throw();

        /// <summary>
        /// Pushes a reference to a value onto the evaluation stack.
        /// The <paramref name="value"/> parameter should be an argument reference.
        /// Other values will either work correctly or cause a weaving-time error, depending on the surrounding code.
        /// </summary>
        /// <typeparam name="T">The type of the value to push.</typeparam>
        /// <param name="value">The reference to push.</param>
        public static void Push<T>(ref T value)
            => Throw();

        /// <summary>
        /// Pushes a pointer onto the evaluation stack.
        /// The <paramref name="value"/> parameter should be an argument.
        /// Other values will either work correctly or cause a weaving-time error, depending on the surrounding code.
        /// </summary>
        /// <param name="value">The pointer to push.</param>
        public static unsafe void Push(void* value)
            => Throw();

        /// <summary>
        /// Marks the given region of code as unreachable, for example just after a <c>ret</c> instruction.
        /// This method returns an <see cref="Exception"/> which should be immediately thrown by the caller.
        /// It enables writing code with a valid control flow for compile-time.
        /// </summary>
        /// <returns>An <see cref="Exception"/> which should be immediately thrown.</returns>
        public static Exception Unreachable()
            => throw Throw();

        /// <summary>
        /// Returns the value on top of the evaluation stack. The return value of this method should be immediately returned from the weaved method.
        /// This is an alternative to emitting a <c>ret</c> instruction followed by a call to <see cref="Unreachable"/>.
        /// </summary>
        /// <typeparam name="T">The returned value type</typeparam>
        /// <returns>The value on top of the evaluation stack, which should be immediately returned from the method.</returns>
        public static T Return<T>()
            => throw Throw();

        internal static Exception Throw()
            => throw new InvalidOperationException("This method is meant to be replaced at compile time by InlineIL.Fody, but the weaver has not been executed correctly.");
    }
}
