using System;
using System.Diagnostics.CodeAnalysis;

#pragma warning disable CS8500

namespace InlineIL;

/// <summary>
/// Injects IL code to the calling method, using InlineIL.Fody.
/// All method calls are replaced at weaving time.
/// </summary>
[SuppressMessage("ReSharper", "UnusedParameter.Global")]
public static unsafe partial class IL
{
    /// <summary>
    /// Declares local variables, with the <c>init</c> flag set to true.
    /// </summary>
    /// <remarks>
    /// <para>
    /// These variables are appended to the ones already declared by the compiler,
    /// but indexes of emitted <c>ldloc</c>/<c>stloc</c> instructions are adjusted to account for that.
    /// </para>
    /// <para>
    /// The <c>init</c> flag applies to all locals, including the ones declared by the compiler,
    /// and space allocated in the stack frame by <c>localloc</c>.
    /// </para>
    /// </remarks>
    /// <param name="locals">The list of local variable declarations.</param>
    public static void DeclareLocals(params LocalVar[] locals)
        => Throw();

    /// <summary>
    /// Declares local variables.
    /// </summary>
    /// <remarks>
    /// <para>
    /// These variables are appended to the ones already declared by the compiler,
    /// but indexes of emitted <c>ldloc</c>/<c>stloc</c> instructions are adjusted to account for that.
    /// </para>
    /// <para>
    /// The <c>init</c> flag applies to all locals, including the ones declared by the compiler,
    /// and space allocated in the stack frame by <c>localloc</c>.
    /// Setting this to <c>false</c> will cause the method to be unverifiable.
    /// </para>
    /// </remarks>
    /// <param name="init">Flag which specifies if the local variables should be zero initialized.</param>
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
    /// </summary>
    /// <typeparam name="T">The type of the value to push.</typeparam>
    /// <param name="value">The value to push.</param>
    public static void Push<T>(T? value)
#if NET9_0_OR_GREATER
        where T : allows ref struct
#endif
        => Throw();

    /// <summary>
    /// Pushes a reference onto the evaluation stack.
    /// </summary>
    /// <typeparam name="T">The reference target type.</typeparam>
    /// <param name="value">The reference to push.</param>
    public static void Push<T>(ref T? value)
#if NET9_0_OR_GREATER
        where T : allows ref struct
#endif
        => Throw();

    /// <summary>
    /// Pushes a pointer onto the evaluation stack.
    /// </summary>
    /// <param name="value">The pointer to push.</param>
    public static void Push(void* value)
        => Throw();

    /// <summary>
    /// Pushes a read-only reference onto the evaluation stack.
    /// </summary>
    /// <typeparam name="T">The reference target type.</typeparam>
    /// <param name="value">The reference to push.</param>
    public static void PushInRef<T>(ref readonly T? value)
#if NET9_0_OR_GREATER
        where T : allows ref struct
#endif
        => Throw();

    /// <summary>
    /// Pushes an output reference onto the evaluation stack.
    /// </summary>
    /// <typeparam name="T">The reference target type.</typeparam>
    /// <param name="value">The reference to push.</param>
    public static void PushOutRef<T>(out T value)
#if NET9_0_OR_GREATER
        where T : allows ref struct
#endif
        => throw Throw();

    /// <summary>
    /// Pops a value from the top of the evaluation stack into a local variable.
    /// </summary>
    /// <typeparam name="T">The type of the value to pop.</typeparam>
    /// <param name="value">A reference to a local variable receiving the value.</param>
    public static void Pop<T>(out T value)
#if NET9_0_OR_GREATER
        where T : allows ref struct
#endif
        => throw Throw();

    /// <summary>
    /// Pops a pointer from the top of the evaluation stack into a local variable.
    /// </summary>
    /// <typeparam name="T">The type of the pointer to pop.</typeparam>
    /// <param name="value">A reference to a local variable receiving the pointer.</param>
    public static void Pop<T>(out T* value)
#if NET9_0_OR_GREATER
        where T : allows ref struct
#endif
        => throw Throw();

    /// <summary>
    /// Pops a pointer from the top of the evaluation stack into a local variable.
    /// </summary>
    /// <param name="value">A reference to a local variable receiving the pointer.</param>
    public static void Pop(out void* value)
        => throw Throw();

    /// <summary>
    /// Marks the given region of code as unreachable, for example just after a <c>ret</c> instruction.
    /// </summary>
    /// <remarks>
    /// This method returns an <see cref="Exception"/> which should be immediately thrown by the caller.
    /// It enables writing code with a valid control flow for compile-time.
    /// </remarks>
    /// <returns>An <see cref="Exception"/> which should be immediately thrown.</returns>
    public static Exception Unreachable()
        => throw Throw();

    /// <summary>
    /// Returns the value on top of the evaluation stack.
    /// </summary>
    /// <remarks>
    /// The return value of this method should be immediately returned from the weaved method.
    /// This is an alternative to emitting a <c>ret</c> instruction followed by a call to <see cref="Unreachable"/>.
    /// </remarks>
    /// <typeparam name="T">The returned value type.</typeparam>
    /// <returns>The value on top of the evaluation stack, which should be immediately returned from the method.</returns>
    public static T Return<T>()
#if NET9_0_OR_GREATER
        where T : allows ref struct
#endif
        => throw Throw();

    /// <summary>
    /// Returns the reference on top of the evaluation stack.
    /// </summary>
    /// <remarks>
    /// The return value of this method should be immediately returned from the weaved method.
    /// This is an alternative to emitting a <c>ret</c> instruction followed by a call to <see cref="Unreachable"/>.
    /// </remarks>
    /// <typeparam name="T">The returned reference type.</typeparam>
    /// <returns>The reference on top of the evaluation stack, which should be immediately returned from the method.</returns>
    public static ref T ReturnRef<T>()
#if NET9_0_OR_GREATER
        where T : allows ref struct
#endif
        => throw Throw();

    /// <summary>
    /// Returns the pointer on top of the evaluation stack.
    /// </summary>
    /// <remarks>
    /// The return value of this method should be immediately returned from the weaved method.
    /// This is an alternative to emitting a <c>ret</c> instruction followed by a call to <see cref="Unreachable"/>.
    /// </remarks>
    /// <typeparam name="T">The returned pointer type.</typeparam>
    /// <returns>The pointer on top of the evaluation stack, which should be immediately returned from the method.</returns>
    public static T* ReturnPointer<T>()
#if NET9_0_OR_GREATER
        where T : allows ref struct
#endif
        => throw Throw();

    /// <summary>
    /// Returns the pointer on top of the evaluation stack.
    /// </summary>
    /// <remarks>
    /// The return value of this method should be immediately returned from the weaved method.
    /// This is an alternative to emitting a <c>ret</c> instruction followed by a call to <see cref="Unreachable"/>.
    /// </remarks>
    /// <returns>The pointer on top of the evaluation stack, which should be immediately returned from the method.</returns>
    public static void* ReturnPointer()
        => throw Throw();

    /// <summary>
    /// Ensures the compiler emits a local variable in IL for the variable passed as parameter in optimized builds.
    /// </summary>
    /// <remarks>
    /// This should make the variable usable in <c>IL.Push</c> calls that would otherwise fail.
    /// </remarks>
    /// <param name="value">A non-ref local variable.</param>
    /// <typeparam name="T">The type of the local variable.</typeparam>
    public static void EnsureLocal<T>(in T? value)
#if NET9_0_OR_GREATER
        where T : allows ref struct
#endif
        => throw Throw();

    internal static Exception Throw()
        => throw new InvalidOperationException("This method is meant to be replaced at compile time by InlineIL.Fody, but the weaver has not been executed correctly.");
}
