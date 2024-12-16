using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using static InlineIL.IL.Emit;

#pragma warning disable CS8500

namespace InlineIL.Examples;

/// <summary>
/// Contains generic, low-level functionality for manipulating pointers.
/// </summary>
[SuppressMessage("ReSharper", "UnusedType.Global")]
[SuppressMessage("ReSharper", "UnusedMember.Global")]
[SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
[SuppressMessage("ReSharper", "EntityNameCapturedOnly.Global")]
public static unsafe class UnsafeNet9
{
    // This is the InlineIL equivalent of System.Runtime.CompilerServices.Unsafe in .NET 9, based on the following file:
    // https://github.com/dotnet/runtime/blob/main/src/libraries/System.Private.CoreLib/src/System/Runtime/CompilerServices/Unsafe.cs
    // Last update: db32911d71075a81b50ad07bfcf10194212bda20

    // The InlineIL code is based upon the commented IL code when present in the original method, which should be the same as in the .NET 6 version.
    // Otherwise, the original method body is kept unchanged.

    /// <summary>
    /// Returns a pointer to the given by-ref parameter.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void* AsPointer<T>(ref T value)
        where T : allows ref struct
    {
        // ldarg.0
        // conv.u
        // ret

        Ldarg(nameof(value));
        Conv_U();
        return IL.ReturnPointer();
    }

    /// <summary>
    /// Returns the size of an object of the given type parameter.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int SizeOf<T>()
        where T : allows ref struct
    {
        return sizeof(T);
    }

    /// <summary>
    /// Casts the given object to the specified type, performs no dynamic type checking.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [return: NotNullIfNotNull(nameof(o))]
    public static T As<T>(object? o)
        where T : class?
    {
        // ldarg.0
        // ret

        Ldarg(nameof(o));
        return IL.Return<T>();
    }

    /// <summary>
    /// Reinterprets the given reference as a reference to a value of type <typeparamref name="TTo"/>.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ref TTo As<TFrom, TTo>(ref TFrom source)
        where TFrom : allows ref struct
        where TTo : allows ref struct
    {
        // ldarg.0
        // ret

        Ldarg(nameof(source));
        return ref IL.ReturnRef<TTo>();
    }

    /// <summary>
    /// Adds an element offset to the given reference.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ref T Add<T>(ref T source, int elementOffset)
        where T : allows ref struct
    {
        // ldarg .0
        // ldarg .1
        // sizeof !!T
        // conv.i
        // mul
        // add
        // ret

        Ldarg(nameof(source));
        Ldarg(nameof(elementOffset));
        Sizeof(typeof(T));
        Conv_I();
        Mul();
        IL.Emit.Add();
        return ref IL.ReturnRef<T>();
    }

    /// <summary>
    /// Adds an element offset to the given reference.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ref T Add<T>(ref T source, IntPtr elementOffset)
        where T : allows ref struct
    {
        // ldarg .0
        // ldarg .1
        // sizeof !!T
        // mul
        // add
        // ret

        Ldarg(nameof(source));
        Ldarg(nameof(elementOffset));
        Sizeof(typeof(T));
        Mul();
        IL.Emit.Add();
        return ref IL.ReturnRef<T>();
    }

    /// <summary>
    /// Adds an element offset to the given pointer.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void* Add<T>(void* source, int elementOffset)
        where T : allows ref struct
    {
        // ldarg .0
        // ldarg .1
        // sizeof !!T
        // conv.i
        // mul
        // add
        // ret

        Ldarg(nameof(source));
        Ldarg(nameof(elementOffset));
        Sizeof(typeof(T));
        Conv_I();
        Mul();
        IL.Emit.Add();
        return IL.ReturnPointer();
    }

    /// <summary>
    /// Adds an element offset to the given reference.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ref T Add<T>(ref T source, nuint elementOffset)
        where T : allows ref struct
    {
        // ldarg .0
        // ldarg .1
        // sizeof !!T
        // mul
        // add
        // ret

        Ldarg(nameof(source));
        Ldarg(nameof(elementOffset));
        Sizeof(typeof(T));
        Mul();
        IL.Emit.Add();
        return ref IL.ReturnRef<T>();
    }

    /// <summary>
    /// Adds a byte offset to the given reference.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ref T AddByteOffset<T>(ref T source, nuint byteOffset)
        where T : allows ref struct
    {
        // ldarg .0
        // ldarg .1
        // add
        // ret

        Ldarg(nameof(source));
        Ldarg(nameof(byteOffset));
        IL.Emit.Add();
        return ref IL.ReturnRef<T>();
    }

    /// <summary>
    /// Determines whether the specified references point to the same location.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool AreSame<T>([AllowNull] ref readonly T left, [AllowNull] ref readonly T right)
        where T : allows ref struct
    {
        // ldarg.0
        // ldarg.1
        // ceq
        // ret

        Ldarg(nameof(left));
        Ldarg(nameof(right));
        Ceq();
        return IL.Return<bool>();
    }

    /// <summary>
    /// Reinterprets the given value of type <typeparamref name="TFrom" /> as a value of type <typeparamref name="TTo" />.
    /// </summary>
    /// <exception cref="NotSupportedException">The sizes of <typeparamref name="TFrom" /> and <typeparamref name="TTo" /> are not the same
    /// or the type parameters are not <see langword="struct"/>s.</exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static TTo BitCast<TFrom, TTo>(TFrom source)
        where TFrom : allows ref struct
        where TTo : allows ref struct
    {
        if (sizeof(TFrom) != sizeof(TTo) || default(TFrom) is null || default(TTo) is null)
        {
            ThrowHelper.ThrowNotSupportedException();
        }

        return ReadUnaligned<TTo>(ref As<TFrom, byte>(ref source));
    }

    /// <summary>
    /// Copies a value of type T to the given location.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void Copy<T>(void* destination, ref readonly T source)
        where T : allows ref struct
    {
        // ldarg .0
        // ldarg .1
        // ldobj !!T
        // stobj !!T
        // ret

        Ldarg(nameof(destination));
        Ldarg(nameof(source));
        Ldobj(typeof(T));
        Stobj(typeof(T));
    }

    /// <summary>
    /// Copies a value of type T to the given location.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void Copy<T>(ref T destination, void* source)
        where T : allows ref struct
    {
        // ldarg .0
        // ldarg .1
        // ldobj !!T
        // stobj !!T
        // ret

        Ldarg(nameof(destination));
        Ldarg(nameof(source));
        Ldobj(typeof(T));
        Stobj(typeof(T));
    }

    /// <summary>
    /// Copies bytes from the source address to the destination address.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void CopyBlock(void* destination, void* source, uint byteCount)
    {
        // ldarg .0
        // ldarg .1
        // ldarg .2
        // cpblk
        // ret

        Ldarg(nameof(destination));
        Ldarg(nameof(source));
        Ldarg(nameof(byteCount));
        Cpblk();
    }

    /// <summary>
    /// Copies bytes from the source address to the destination address.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void CopyBlock(ref byte destination, ref readonly byte source, uint byteCount)
    {
        // ldarg .0
        // ldarg .1
        // ldarg .2
        // cpblk
        // ret

        Ldarg(nameof(destination));
        Ldarg(nameof(source));
        Ldarg(nameof(byteCount));
        Cpblk();
    }

    /// <summary>
    /// Copies bytes from the source address to the destination address without assuming architecture dependent alignment of the addresses.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void CopyBlockUnaligned(void* destination, void* source, uint byteCount)
    {
        // ldarg .0
        // ldarg .1
        // ldarg .2
        // unaligned. 0x1
        // cpblk
        // ret

        Ldarg(nameof(destination));
        Ldarg(nameof(source));
        Ldarg(nameof(byteCount));
        Unaligned(1);
        Cpblk();
    }

    /// <summary>
    /// Copies bytes from the source address to the destination address without assuming architecture dependent alignment of the addresses.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void CopyBlockUnaligned(ref byte destination, ref readonly byte source, uint byteCount)
    {
        // ldarg .0
        // ldarg .1
        // ldarg .2
        // unaligned. 0x1
        // cpblk
        // ret

        Ldarg(nameof(destination));
        Ldarg(nameof(source));
        Ldarg(nameof(byteCount));
        Unaligned(1);
        Cpblk();
    }

    /// <summary>
    /// Determines whether the memory address referenced by <paramref name="left"/> is greater than
    /// the memory address referenced by <paramref name="right"/>.
    /// </summary>
    /// <remarks>
    /// This check is conceptually similar to "(void*)(&amp;left) &gt; (void*)(&amp;right)".
    /// </remarks>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsAddressGreaterThan<T>([AllowNull] ref readonly T left, [AllowNull] ref readonly T right)
        where T : allows ref struct
    {
        // ldarg.0
        // ldarg.1
        // cgt.un
        // ret

        Ldarg(nameof(left));
        Ldarg(nameof(right));
        Cgt_Un();
        return IL.Return<bool>();
    }

    /// <summary>
    /// Determines whether the memory address referenced by <paramref name="left"/> is less than
    /// the memory address referenced by <paramref name="right"/>.
    /// </summary>
    /// <remarks>
    /// This check is conceptually similar to "(void*)(&amp;left) &lt; (void*)(&amp;right)".
    /// </remarks>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsAddressLessThan<T>([AllowNull] ref readonly T left, [AllowNull] ref readonly T right)
        where T : allows ref struct
    {
        // ldarg.0
        // ldarg.1
        // clt.un
        // ret

        Ldarg(nameof(left));
        Ldarg(nameof(right));
        Clt_Un();
        return IL.Return<bool>();
    }

    /// <summary>
    /// Initializes a block of memory at the given location with a given initial value.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void InitBlock(void* startAddress, byte value, uint byteCount)
    {
        // ldarg .0
        // ldarg .1
        // ldarg .2
        // initblk
        // ret

        Ldarg(nameof(startAddress));
        Ldarg(nameof(value));
        Ldarg(nameof(byteCount));
        Initblk();
    }

    /// <summary>
    /// Initializes a block of memory at the given location with a given initial value.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void InitBlock(ref byte startAddress, byte value, uint byteCount)
    {
        // ldarg .0
        // ldarg .1
        // ldarg .2
        // initblk
        // ret

        Ldarg(nameof(startAddress));
        Ldarg(nameof(value));
        Ldarg(nameof(byteCount));
        Initblk();
    }

    /// <summary>
    /// Initializes a block of memory at the given location with a given initial value
    /// without assuming architecture dependent alignment of the address.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void InitBlockUnaligned(void* startAddress, byte value, uint byteCount)
    {
        // ldarg .0
        // ldarg .1
        // ldarg .2
        // unaligned. 0x1
        // initblk
        // ret

        Ldarg(nameof(startAddress));
        Ldarg(nameof(value));
        Ldarg(nameof(byteCount));
        Unaligned(1);
        Initblk();
    }

    /// <summary>
    /// Initializes a block of memory at the given location with a given initial value
    /// without assuming architecture dependent alignment of the address.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void InitBlockUnaligned(ref byte startAddress, byte value, uint byteCount)
    {
        // ldarg .0
        // ldarg .1
        // ldarg .2
        // unaligned. 0x1
        // initblk
        // ret

        Ldarg(nameof(startAddress));
        Ldarg(nameof(value));
        Ldarg(nameof(byteCount));
        Unaligned(1);
        Initblk();
    }

    /// <summary>
    /// Reads a value of type <typeparamref name="T"/> from the given location.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static T ReadUnaligned<T>(void* source)
        where T : allows ref struct
    {
        // ldarg.0
        // unaligned. 0x1
        // ldobj !!T
        // ret

        Ldarg(nameof(source));
        Unaligned(1);
        Ldobj(typeof(T));
        return IL.Return<T>();
    }

    /// <summary>
    /// Reads a value of type <typeparamref name="T"/> from the given location.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static T ReadUnaligned<T>(scoped ref readonly byte source)
        where T : allows ref struct
    {
        // ldarg.0
        // unaligned. 0x1
        // ldobj !!T
        // ret

        Ldarg(nameof(source));
        Unaligned(1);
        Ldobj(typeof(T));
        return IL.Return<T>();
    }

    /// <summary>
    /// Writes a value of type <typeparamref name="T"/> to the given location.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void WriteUnaligned<T>(void* destination, T value)
        where T : allows ref struct
    {
        // ldarg .0
        // ldarg .1
        // unaligned. 0x01
        // stobj !!T
        // ret

        Ldarg(nameof(destination));
        Ldarg(nameof(value));
        Unaligned(1);
        Stobj(typeof(T));
    }

    /// <summary>
    /// Writes a value of type <typeparamref name="T"/> to the given location.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void WriteUnaligned<T>(ref byte destination, T value)
        where T : allows ref struct
    {
        // ldarg .0
        // ldarg .1
        // unaligned. 0x01
        // stobj !!T
        // ret

        Ldarg(nameof(destination));
        Ldarg(nameof(value));
        Unaligned(1);
        Stobj(typeof(T));
    }

    /// <summary>
    /// Adds a byte offset to the given reference.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ref T AddByteOffset<T>(ref T source, IntPtr byteOffset)
        where T : allows ref struct
    {
        // ldarg.0
        // ldarg.1
        // add
        // ret

        Ldarg(nameof(source));
        Ldarg(nameof(byteOffset));
        IL.Emit.Add();
        return ref IL.ReturnRef<T>();
    }

    /// <summary>
    /// Reads a value of type <typeparamref name="T"/> from the given location.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static T Read<T>(void* source)
        where T : allows ref struct
    {
        return *(T*)source;
    }

    /// <summary>
    /// Writes a value of type <typeparamref name="T"/> to the given location.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void Write<T>(void* destination, T value)
        where T : allows ref struct
    {
        *(T*)destination = value;
    }

    /// <summary>
    /// Reinterprets the given location as a reference to a value of type <typeparamref name="T"/>.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ref T AsRef<T>(void* source)
        where T : allows ref struct
    {
        return ref *(T*)source;
    }

    /// <summary>
    /// Reinterprets the given location as a reference to a value of type <typeparamref name="T"/>.
    /// </summary>
    /// <remarks>The lifetime of the reference will not be validated when using this API.</remarks>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ref T AsRef<T>(scoped ref readonly T source)
        where T : allows ref struct
    {
        //ldarg .0
        //ret

        Ldarg(nameof(source));
        return ref IL.ReturnRef<T>();
    }

    /// <summary>
    /// Determines the byte offset from origin to target from the given references.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IntPtr ByteOffset<T>([AllowNull] ref readonly T origin, [AllowNull] ref readonly T target)
        where T : allows ref struct
    {
        // ldarg .1
        // ldarg .0
        // sub
        // ret

        Ldarg(nameof(target));
        Ldarg(nameof(origin));
        Sub();
        return IL.Return<IntPtr>();
    }

    /// <summary>
    /// Returns a by-ref to type <typeparamref name="T"/> that is a null reference.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ref T NullRef<T>()
        where T : allows ref struct
    {
        // ldc.i4.0
        // conv.u
        // ret

        Ldc_I4_0();
        Conv_U();
        return ref IL.ReturnRef<T>();
    }

    /// <summary>
    /// Returns if a given by-ref to type <typeparamref name="T"/> is a null reference.
    /// </summary>
    /// <remarks>
    /// This check is conceptually similar to "(void*)(&amp;source) == nullptr".
    /// </remarks>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsNullRef<T>(ref readonly T source)
        where T : allows ref struct
    {
        // ldarg.0
        // ldc.i4.0
        // conv.u
        // ceq
        // ret

        Ldarg(nameof(source));
        Ldc_I4_0();
        Conv_U();
        Ceq();
        return IL.Return<bool>();
    }

    /// <summary>
    /// Bypasses definite assignment rules by taking advantage of <c>out</c> semantics.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void SkipInit<T>(out T value)
        where T : allows ref struct
    {
        // ret

        Ret();
        throw IL.Unreachable();
    }

    /// <summary>
    /// Subtracts an element offset from the given reference.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ref T Subtract<T>(ref T source, int elementOffset)
        where T : allows ref struct
    {
        // ldarg .0
        // ldarg .1
        // sizeof !!T
        // conv.i
        // mul
        // sub
        // ret

        Ldarg(nameof(source));
        Ldarg(nameof(elementOffset));
        Sizeof(typeof(T));
        Conv_I();
        Mul();
        Sub();
        return ref IL.ReturnRef<T>();
    }

    /// <summary>
    /// Subtracts an element offset from the given void pointer.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void* Subtract<T>(void* source, int elementOffset)
        where T : allows ref struct
    {
        // ldarg .0
        // ldarg .1
        // sizeof !!T
        // conv.i
        // mul
        // sub
        // ret

        Ldarg(nameof(source));
        Ldarg(nameof(elementOffset));
        Sizeof(typeof(T));
        Conv_I();
        Mul();
        Sub();
        return IL.ReturnPointer();
    }

    /// <summary>
    /// Subtracts an element offset from the given reference.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ref T Subtract<T>(ref T source, IntPtr elementOffset)
        where T : allows ref struct
    {
        // ldarg .0
        // ldarg .1
        // sizeof !!T
        // mul
        // sub
        // ret

        Ldarg(nameof(source));
        Ldarg(nameof(elementOffset));
        Sizeof(typeof(T));
        Mul();
        Sub();
        return ref IL.ReturnRef<T>();
    }

    /// <summary>
    /// Subtracts an element offset from the given reference.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ref T Subtract<T>(ref T source, nuint elementOffset)
        where T : allows ref struct
    {
        // ldarg .0
        // ldarg .1
        // sizeof !!T
        // mul
        // sub
        // ret

        Ldarg(nameof(source));
        Ldarg(nameof(elementOffset));
        Sizeof(typeof(T));
        Mul();
        Sub();
        return ref IL.ReturnRef<T>();
    }

    /// <summary>
    /// Subtracts a byte offset from the given reference.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ref T SubtractByteOffset<T>(ref T source, IntPtr byteOffset)
        where T : allows ref struct
    {
        // ldarg .0
        // ldarg .1
        // sub
        // ret

        Ldarg(nameof(source));
        Ldarg(nameof(byteOffset));
        Sub();
        return ref IL.ReturnRef<T>();
    }

    /// <summary>
    /// Subtracts a byte offset from the given reference.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ref T SubtractByteOffset<T>(ref T source, nuint byteOffset)
        where T : allows ref struct
    {
        // ldarg .0
        // ldarg .1
        // sub
        // ret

        Ldarg(nameof(source));
        Ldarg(nameof(byteOffset));
        Sub();
        return ref IL.ReturnRef<T>();
    }

    /// <summary>
    /// Returns a mutable ref to a boxed value
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ref T Unbox<T>(object box)
        where T : struct
    {
        // ldarg .0
        // unbox !!T
        // ret

        IL.Push(box);
        IL.Emit.Unbox(typeof(T));
        return ref IL.ReturnRef<T>();
    }

    private static class ThrowHelper
    {
        [DoesNotReturn]
        internal static void ThrowNotSupportedException()
        {
            throw new NotSupportedException();
        }
    }
}
