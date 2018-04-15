using System;
using System.Diagnostics.CodeAnalysis;
using System.Reflection.Emit;
using System.Runtime.CompilerServices;
using System.Runtime.Versioning;

namespace InlineIL.Examples
{
    [SuppressMessage("ReSharper", "UnusedMember.Global")]
    [SuppressMessage("ReSharper", "UnusedParameter.Global")]
    public static unsafe class Unsafe2
    {
        // This is the InlineIL equivalent of System.Runtime.CompilerServices.Unsafe
        // https://github.com/dotnet/corefx/blob/master/src/System.Runtime.CompilerServices.Unsafe/src/System.Runtime.CompilerServices.Unsafe.il

        // This version uses an API similar to ILGenerator

        [MethodImpl(MethodImplOptions.AggressiveInlining), NonVersionable]
        public static T Read<T>(void* source)
        {
            IL.Push(source);
            IL.Emit(OpCodes.Ldobj, typeof(T));
            return IL.Return<T>();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining), NonVersionable]
        public static T ReadUnaligned<T>(void* source)
        {
            IL.Push(source);
            IL.Emit(OpCodes.Unaligned, 1);
            IL.Emit(OpCodes.Ldobj, typeof(T));
            return IL.Return<T>();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining), NonVersionable]
        public static T ReadUnaligned<T>(ref byte source)
        {
            IL.Push(ref source);
            IL.Emit(OpCodes.Unaligned, 1);
            IL.Emit(OpCodes.Ldobj, typeof(T));
            return IL.Return<T>();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining), NonVersionable]
        public static void Write<T>(void* destination, T value)
        {
            IL.Push(destination);
            IL.Push(value);
            IL.Emit(OpCodes.Stobj, typeof(T));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining), NonVersionable]
        public static void WriteUnaligned<T>(void* destination, T value)
        {
            IL.Push(destination);
            IL.Push(value);
            IL.Emit(OpCodes.Unaligned, 1);
            IL.Emit(OpCodes.Stobj, typeof(T));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining), NonVersionable]
        public static void WriteUnaligned<T>(ref byte destination, T value)
        {
            IL.Push(ref destination);
            IL.Push(value);
            IL.Emit(OpCodes.Unaligned, 1);
            IL.Emit(OpCodes.Stobj, typeof(T));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining), NonVersionable]
        public static void Copy<T>(void* destination, ref T source)
        {
            IL.Push(destination);
            IL.Push(ref source);
            IL.Emit(OpCodes.Ldobj, typeof(T));
            IL.Emit(OpCodes.Stobj, typeof(T));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining), NonVersionable]
        public static void Copy<T>(ref T destination, void* source)
        {
            IL.Push(ref destination);
            IL.Push(source);
            IL.Emit(OpCodes.Ldobj, typeof(T));
            IL.Emit(OpCodes.Stobj, typeof(T));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining), NonVersionable]
        public static void* AsPointer<T>(ref T value)
        {
            IL.Push(ref value);
            IL.Emit(OpCodes.Conv_U);
            IL.Emit(OpCodes.Ret);
            throw IL.Unreachable();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining), NonVersionable]
        public static int SizeOf<T>()
        {
            IL.Emit(OpCodes.Sizeof, typeof(T));
            return IL.Return<int>();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining), NonVersionable]
        public static void CopyBlock(void* destination, void* source, uint byteCount)
        {
            IL.Push(destination);
            IL.Push(source);
            IL.Push(byteCount);
            IL.Emit(OpCodes.Cpblk);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining), NonVersionable]
        public static void CopyBlock(ref byte destination, ref byte source, uint byteCount)
        {
            IL.Push(ref destination);
            IL.Push(ref source);
            IL.Push(byteCount);
            IL.Emit(OpCodes.Cpblk);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining), NonVersionable]
        public static void CopyBlockUnaligned(void* destination, void* source, uint byteCount)
        {
            IL.Push(destination);
            IL.Push(source);
            IL.Push(byteCount);
            IL.Emit(OpCodes.Unaligned, 1);
            IL.Emit(OpCodes.Cpblk);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining), NonVersionable]
        public static void CopyBlockUnaligned(ref byte destination, ref byte source, uint byteCount)
        {
            IL.Push(ref destination);
            IL.Push(ref source);
            IL.Push(byteCount);
            IL.Emit(OpCodes.Unaligned, 1);
            IL.Emit(OpCodes.Cpblk);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining), NonVersionable]
        public static void InitBlock(void* startAddress, byte value, uint byteCount)
        {
            IL.Push(startAddress);
            IL.Push(value);
            IL.Push(byteCount);
            IL.Emit(OpCodes.Initblk);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining), NonVersionable]
        public static void InitBlock(ref byte startAddress, byte value, uint byteCount)
        {
            IL.Push(ref startAddress);
            IL.Push(value);
            IL.Push(byteCount);
            IL.Emit(OpCodes.Initblk);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining), NonVersionable]
        public static void InitBlockUnaligned(void* startAddress, byte value, uint byteCount)
        {
            IL.Push(startAddress);
            IL.Push(value);
            IL.Push(byteCount);
            IL.Emit(OpCodes.Unaligned, 1);
            IL.Emit(OpCodes.Initblk);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining), NonVersionable]
        public static void InitBlockUnaligned(ref byte startAddress, byte value, uint byteCount)
        {
            IL.Push(ref startAddress);
            IL.Push(value);
            IL.Push(byteCount);
            IL.Emit(OpCodes.Unaligned, 1);
            IL.Emit(OpCodes.Initblk);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining), NonVersionable]
        public static T As<T>(object o)
            where T : class
        {
            IL.Push(o);
            return IL.Return<T>();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining), NonVersionable]
        public static ref T AsRef<T>(void* source)
        {
            // Roundtrip via a local to avoid type mismatch on return that the JIT inliner chokes on.
            IL.DeclareLocals(
                false,
                new LocalVar("local", typeof(int).MakeByRefType())
            );

            IL.Push(source);
            IL.Emit(OpCodes.Stloc, new LocalRef("local"));
            IL.Emit(OpCodes.Ldloc, new LocalRef("local"));
            IL.Emit(OpCodes.Ret);
            throw IL.Unreachable();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining), NonVersionable]
        public static ref T AsRef<T>(in T source)
        {
            IL.Emit(OpCodes.Ldarg_0);
            IL.Emit(OpCodes.Ret);
            throw IL.Unreachable();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining), NonVersionable]
        public static ref TTo As<TFrom, TTo>(ref TFrom source)
        {
            IL.Emit(OpCodes.Ldarg_0);
            IL.Emit(OpCodes.Ret);
            throw IL.Unreachable();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining), NonVersionable]
        public static ref T Add<T>(ref T source, int elementOffset)
        {
            IL.Push(ref source);
            IL.Push(elementOffset);
            IL.Emit(OpCodes.Sizeof, typeof(T));
            IL.Emit(OpCodes.Conv_I);
            IL.Emit(OpCodes.Mul);
            IL.Emit(OpCodes.Add);
            IL.Emit(OpCodes.Ret);
            throw IL.Unreachable();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining), NonVersionable]
        public static void* Add<T>(void* source, int elementOffset)
        {
            IL.Push(source);
            IL.Push(elementOffset);
            IL.Emit(OpCodes.Sizeof, typeof(T));
            IL.Emit(OpCodes.Conv_I);
            IL.Emit(OpCodes.Mul);
            IL.Emit(OpCodes.Add);
            IL.Emit(OpCodes.Ret);
            throw IL.Unreachable();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining), NonVersionable]
        public static ref T Add<T>(ref T source, IntPtr elementOffset)
        {
            IL.Push(ref source);
            IL.Push(elementOffset);
            IL.Emit(OpCodes.Sizeof, typeof(T));
            IL.Emit(OpCodes.Mul);
            IL.Emit(OpCodes.Add);
            IL.Emit(OpCodes.Ret);
            throw IL.Unreachable();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining), NonVersionable]
        public static ref T AddByteOffset<T>(ref T source, IntPtr byteOffset)
        {
            IL.Push(ref source);
            IL.Push(byteOffset);
            IL.Emit(OpCodes.Add);
            IL.Emit(OpCodes.Ret);
            throw IL.Unreachable();
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining), NonVersionable]
        public static ref T Subtract<T>(ref T source, int elementOffset)
        {
            IL.Push(ref source);
            IL.Push(elementOffset);
            IL.Emit(OpCodes.Sizeof, typeof(T));
            IL.Emit(OpCodes.Conv_I);
            IL.Emit(OpCodes.Mul);
            IL.Emit(OpCodes.Sub);
            IL.Emit(OpCodes.Ret);
            throw IL.Unreachable();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining), NonVersionable]
        public static void* Subtract<T>(void* source, int elementOffset)
        {
            IL.Push(source);
            IL.Push(elementOffset);
            IL.Emit(OpCodes.Sizeof, typeof(T));
            IL.Emit(OpCodes.Conv_I);
            IL.Emit(OpCodes.Mul);
            IL.Emit(OpCodes.Sub);
            IL.Emit(OpCodes.Ret);
            throw IL.Unreachable();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining), NonVersionable]
        public static ref T Subtract<T>(ref T source, IntPtr elementOffset)
        {
            IL.Push(ref source);
            IL.Push(elementOffset);
            IL.Emit(OpCodes.Sizeof, typeof(T));
            IL.Emit(OpCodes.Mul);
            IL.Emit(OpCodes.Sub);
            IL.Emit(OpCodes.Ret);
            throw IL.Unreachable();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining), NonVersionable]
        public static ref T SubtractByteOffset<T>(ref T source, IntPtr byteOffset)
        {
            IL.Push(ref source);
            IL.Push(byteOffset);
            IL.Emit(OpCodes.Sub);
            IL.Emit(OpCodes.Ret);
            throw IL.Unreachable();
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining), NonVersionable]
        public static IntPtr ByteOffset<T>(ref T origin, ref T target)
        {
            IL.Push(ref target);
            IL.Push(ref origin);
            IL.Emit(OpCodes.Sub);
            return IL.Return<IntPtr>();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining), NonVersionable]
        public static bool AreSame<T>(ref T left, ref T right)
        {
            IL.Push(ref left);
            IL.Push(ref right);
            IL.Emit(OpCodes.Ceq);
            return IL.Return<bool>();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining), NonVersionable]
        public static bool IsAddressGreaterThan<T>(ref T left, ref T right)
        {
            IL.Push(ref left);
            IL.Push(ref right);
            IL.Emit(OpCodes.Cgt_Un);
            return IL.Return<bool>();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining), NonVersionable]
        public static bool IsAddressLessThan<T>(ref T left, ref T right)
        {
            IL.Push(ref left);
            IL.Push(ref right);
            IL.Emit(OpCodes.Clt_Un);
            return IL.Return<bool>();
        }
    }
}
