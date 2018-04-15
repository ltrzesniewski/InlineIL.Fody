using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Runtime.Versioning;
using static InlineIL.ILEmit;

namespace InlineIL.Examples
{
    [SuppressMessage("ReSharper", "UnusedMember.Global")]
    [SuppressMessage("ReSharper", "UnusedParameter.Global")]
    public static unsafe class Unsafe
    {
        // This is the InlineIL equivalent of System.Runtime.CompilerServices.Unsafe
        // https://github.com/dotnet/corefx/blob/master/src/System.Runtime.CompilerServices.Unsafe/src/System.Runtime.CompilerServices.Unsafe.il

        // This version uses the friendlier ILEmit API
        // Note the using static InlineIL.ILEmit;

        [NonVersionable]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Read<T>(void* source)
        {
            Ldarg(nameof(source));
            Ldobj(typeof(T));
            return IL.Return<T>();
        }

        [NonVersionable]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T ReadUnaligned<T>(void* source)
        {
            Ldarg(nameof(source));
            Unaligned(1);
            Ldobj(typeof(T));
            return IL.Return<T>();
        }

        [NonVersionable]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T ReadUnaligned<T>(ref byte source)
        {
            Ldarg(nameof(source));
            Unaligned(1);
            Ldobj(typeof(T));
            return IL.Return<T>();
        }

        [NonVersionable]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Write<T>(void* destination, T value)
        {
            Ldarg(nameof(destination));
            Ldarg(nameof(value));
            Stobj(typeof(T));
        }

        [NonVersionable]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void WriteUnaligned<T>(void* destination, T value)
        {
            Ldarg(nameof(destination));
            Ldarg(nameof(value));
            Unaligned(1);
            Stobj(typeof(T));
        }

        [NonVersionable]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void WriteUnaligned<T>(ref byte destination, T value)
        {
            Ldarg(nameof(destination));
            Ldarg(nameof(value));
            Unaligned(1);
            Stobj(typeof(T));
        }

        [NonVersionable]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Copy<T>(void* destination, ref T source)
        {
            Ldarg(nameof(destination));
            Ldarg(nameof(source));
            Ldobj(typeof(T));
            Stobj(typeof(T));
        }

        [NonVersionable]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Copy<T>(ref T destination, void* source)
        {
            Ldarg(nameof(destination));
            Ldarg(nameof(source));
            Ldobj(typeof(T));
            Stobj(typeof(T));
        }

        [NonVersionable]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void* AsPointer<T>(ref T value)
        {
            Ldarg(nameof(value));
            Conv_U();
            Ret();
            throw IL.Unreachable();
        }

        [NonVersionable]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int SizeOf<T>()
        {
            Sizeof(typeof(T));
            return IL.Return<int>();
        }

        [NonVersionable]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void CopyBlock(void* destination, void* source, uint byteCount)
        {
            Ldarg(nameof(destination));
            Ldarg(nameof(source));
            Ldarg(nameof(byteCount));
            Cpblk();
        }

        [NonVersionable]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void CopyBlock(ref byte destination, ref byte source, uint byteCount)
        {
            Ldarg(nameof(destination));
            Ldarg(nameof(source));
            Ldarg(nameof(byteCount));
            Cpblk();
        }

        [NonVersionable]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void CopyBlockUnaligned(void* destination, void* source, uint byteCount)
        {
            Ldarg(nameof(destination));
            Ldarg(nameof(source));
            Ldarg(nameof(byteCount));
            Unaligned(1);
            Cpblk();
        }

        [NonVersionable]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void CopyBlockUnaligned(ref byte destination, ref byte source, uint byteCount)
        {
            Ldarg(nameof(destination));
            Ldarg(nameof(source));
            Ldarg(nameof(byteCount));
            Unaligned(1);
            Cpblk();
        }

        [NonVersionable]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void InitBlock(void* startAddress, byte value, uint byteCount)
        {
            Ldarg(nameof(startAddress));
            Ldarg(nameof(value));
            Ldarg(nameof(byteCount));
            Initblk();
        }

        [NonVersionable]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void InitBlock(ref byte startAddress, byte value, uint byteCount)
        {
            Ldarg(nameof(startAddress));
            Ldarg(nameof(value));
            Ldarg(nameof(byteCount));
            Initblk();
        }

        [NonVersionable]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void InitBlockUnaligned(void* startAddress, byte value, uint byteCount)
        {
            Ldarg(nameof(startAddress));
            Ldarg(nameof(value));
            Ldarg(nameof(byteCount));
            Unaligned(1);
            Initblk();
        }

        [NonVersionable]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void InitBlockUnaligned(ref byte startAddress, byte value, uint byteCount)
        {
            Ldarg(nameof(startAddress));
            Ldarg(nameof(value));
            Ldarg(nameof(byteCount));
            Unaligned(1);
            Initblk();
        }

        [NonVersionable]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T As<T>(object o)
            where T : class
        {
            Ldarg(nameof(o));
            return IL.Return<T>();
        }

        [NonVersionable]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ref T AsRef<T>(void* source)
        {
            // Roundtrip via a local to avoid type mismatch on return that the JIT inliner chokes on.
            IL.DeclareLocals(
                false,
                new LocalVar("local", typeof(int).MakeByRefType())
            );

            IL.Push(source);
            Stloc("local");
            Ldloc("local");
            Ret();
            throw IL.Unreachable();
        }

        [NonVersionable]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ref T AsRef<T>(in T source)
        {
            Ldarg(nameof(source));
            Ret();
            throw IL.Unreachable();
        }

        [NonVersionable]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ref TTo As<TFrom, TTo>(ref TFrom source)
        {
            Ldarg(nameof(source));
            Ret();
            throw IL.Unreachable();
        }

        [NonVersionable]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ref T Add<T>(ref T source, int elementOffset)
        {
            Ldarg(nameof(source));
            Ldarg(nameof(elementOffset));
            Sizeof(typeof(T));
            Conv_I();
            Mul();
            ILEmit.Add();
            Ret();
            throw IL.Unreachable();
        }

        [NonVersionable]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void* Add<T>(void* source, int elementOffset)
        {
            Ldarg(nameof(source));
            Ldarg(nameof(elementOffset));
            Sizeof(typeof(T));
            Conv_I();
            Mul();
            ILEmit.Add();
            Ret();
            throw IL.Unreachable();
        }

        [NonVersionable]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ref T Add<T>(ref T source, IntPtr elementOffset)
        {
            Ldarg(nameof(source));
            Ldarg(nameof(elementOffset));
            Sizeof(typeof(T));
            Mul();
            ILEmit.Add();
            Ret();
            throw IL.Unreachable();
        }

        [NonVersionable]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ref T AddByteOffset<T>(ref T source, IntPtr byteOffset)
        {
            Ldarg(nameof(source));
            Ldarg(nameof(byteOffset));
            ILEmit.Add();
            Ret();
            throw IL.Unreachable();
        }

        [NonVersionable]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ref T Subtract<T>(ref T source, int elementOffset)
        {
            Ldarg(nameof(source));
            Ldarg(nameof(elementOffset));
            Sizeof(typeof(T));
            Conv_I();
            Mul();
            Sub();
            Ret();
            throw IL.Unreachable();
        }

        [NonVersionable]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void* Subtract<T>(void* source, int elementOffset)
        {
            Ldarg(nameof(source));
            Ldarg(nameof(elementOffset));
            Sizeof(typeof(T));
            Conv_I();
            Mul();
            Sub();
            Ret();
            throw IL.Unreachable();
        }

        [NonVersionable]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ref T Subtract<T>(ref T source, IntPtr elementOffset)
        {
            Ldarg(nameof(source));
            Ldarg(nameof(elementOffset));
            Sizeof(typeof(T));
            Mul();
            Sub();
            Ret();
            throw IL.Unreachable();
        }

        [NonVersionable]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ref T SubtractByteOffset<T>(ref T source, IntPtr byteOffset)
        {
            Ldarg(nameof(source));
            Ldarg(nameof(byteOffset));
            Sub();
            Ret();
            throw IL.Unreachable();
        }

        [NonVersionable]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IntPtr ByteOffset<T>(ref T origin, ref T target)
        {
            Ldarg(nameof(target));
            Ldarg(nameof(origin));
            Sub();
            return IL.Return<IntPtr>();
        }

        [NonVersionable]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool AreSame<T>(ref T left, ref T right)
        {
            Ldarg(nameof(left));
            Ldarg(nameof(right));
            Ceq();
            return IL.Return<bool>();
        }

        [NonVersionable]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsAddressGreaterThan<T>(ref T left, ref T right)
        {
            Ldarg(nameof(left));
            Ldarg(nameof(right));
            Cgt_Un();
            return IL.Return<bool>();
        }

        [NonVersionable]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsAddressLessThan<T>(ref T left, ref T right)
        {
            Ldarg(nameof(left));
            Ldarg(nameof(right));
            Clt_Un();
            return IL.Return<bool>();
        }
    }
}

namespace System.Runtime.Versioning
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Method | AttributeTargets.Constructor, Inherited = false)]
    internal sealed class NonVersionableAttribute : Attribute
    {
    }
}
