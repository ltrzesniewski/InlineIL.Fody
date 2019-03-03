using System;
using System.Diagnostics.CodeAnalysis;

namespace InlineIL
{
    /// <summary>
    /// Injects IL code to the calling method, using InlineIL.Fody.
    /// All method calls are replaced at weaving time.
    /// </summary>
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    [SuppressMessage("ReSharper", "UnusedParameter.Global")]
    public static partial class IL
    {
        /// <summary>
        /// Declares local variables, with the init flag set to true.
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
        /// Pops a value from the top of the evaluation stack into a local variable.
        /// </summary>
        /// <typeparam name="T">The type of the value to pop.</typeparam>
        /// <param name="value">A reference to a local variable receiving the value.</param>
        public static void Pop<T>(out T value)
            => throw Throw();

        /// <summary>
        /// Pops a pointer from the top of the evaluation stack into a local variable.
        /// </summary>
        /// <typeparam name="T">The type of the pointer to pop.</typeparam>
        /// <param name="value">A reference to a local variable receiving the pointer.</param>
        public static unsafe void Pop<T>(out T* value)
            where T : unmanaged
            => throw Throw();

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

        /// <summary>
        /// Returns the reference on top of the evaluation stack. The return value of this method should be immediately returned from the weaved method.
        /// This is an alternative to emitting a <c>ret</c> instruction followed by a call to <see cref="Unreachable"/>.
        /// </summary>
        /// <typeparam name="T">The returned reference type</typeparam>
        /// <returns>The reference on top of the evaluation stack, which should be immediately returned from the method.</returns>
        public static ref T ReturnRef<T>()
            => throw Throw();

        /// <summary>
        /// Returns the pointer on top of the evaluation stack. The return value of this method should be immediately returned from the weaved method.
        /// This is an alternative to emitting a <c>ret</c> instruction followed by a call to <see cref="Unreachable"/>.
        /// </summary>
        /// <typeparam name="T">The returned pointer type</typeparam>
        /// <returns>The reference on top of the evaluation stack, which should be immediately returned from the method.</returns>
        public static unsafe T* ReturnPointer<T>()
            where T : unmanaged
            => throw Throw();

        internal static Exception Throw()
            => throw new InvalidOperationException("This method is meant to be replaced at compile time by InlineIL.Fody, but the weaver has not been executed correctly.");
    }
}
