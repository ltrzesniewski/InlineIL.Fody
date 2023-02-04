﻿using System;
using System.Diagnostics.CodeAnalysis;
using InlineIL.Tests.Common;
using static InlineIL.IL.Emit;

namespace InlineIL.Tests.UnverifiableAssemblyToProcess;

[TestCases]
[SuppressMessage("ReSharper", "UnusedType.Global")]
[SuppressMessage("ReSharper", "UnusedMember.Global")]
[SuppressMessage("ReSharper", "UnusedParameter.Global")]
[SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
[SuppressMessage("ReSharper", "EntityNameCapturedOnly.Global")]
public unsafe class BasicTestCases : IUnverifiableBasicTestCases
{
    public static int* StaticIntPtrField;
    public static void* StaticVoidPtrField;
    public static readonly int[] IntArrayField = { 42 };

    public void PushPointer(int* value)
    {
        IL.Push(value);
        Ldc_I4(42);
        Stind_I4();
    }

    public void HandlePrefixesInDebugMode(ref Guid value)
    {
        IL.Push(ref value);

        Ldc_I4_0();
        Conv_U1();

        Sizeof(typeof(Guid));

        Unaligned(1);
        Initblk();
    }

    public int PopPointerLocal(int* arg, int offset)
    {
        Ldarg(nameof(arg));
        Ldarg(nameof(offset));
        Conv_I();
        Ldc_I4(sizeof(int));
        Mul();
        Add();
        IL.Pop(out int* value);
        return *value;
    }

    public int PopPointerArg(int* arg, int offset)
    {
        _ = arg; // Make ReSharper happy
        Ldarg(nameof(arg));
        Ldarg(nameof(offset));
        Conv_I();
        Ldc_I4(sizeof(int));
        Mul();
        Add();
        IL.Pop(out arg);
        return *arg;
    }

    public int PopPointerStatic(int* arg, int offset)
    {
        Ldarg(nameof(arg));
        Ldarg(nameof(offset));
        Conv_I();
        Ldc_I4(sizeof(int));
        Mul();
        Add();
        IL.Pop(out StaticIntPtrField);
        return *StaticIntPtrField;
    }

    public int PopVoidPointerLocal(int* arg, int offset)
    {
        Ldarg(nameof(arg));
        Ldarg(nameof(offset));
        Conv_I();
        Ldc_I4(sizeof(int));
        Mul();
        Add();
        IL.Pop(out void* value);
        return *(int*)value;
    }

    public int PopVoidPointerArg(void* arg, int offset)
    {
        _ = arg; // Make ReSharper happy
        Ldarg(nameof(arg));
        Ldarg(nameof(offset));
        Conv_I();
        Ldc_I4(sizeof(int));
        Mul();
        Add();
        IL.Pop(out arg);
        return *(int*)arg;
    }

    public int PopVoidPointerStatic(int* arg, int offset)
    {
        Ldarg(nameof(arg));
        Ldarg(nameof(offset));
        Conv_I();
        Ldc_I4(sizeof(int));
        Mul();
        Add();
        IL.Pop(out StaticVoidPtrField);
        return *(int*)StaticVoidPtrField;
    }

    public ref int ReturnRef(int[] values, int offset)
    {
        Ldarg(nameof(values));
        Ldarg(nameof(offset));
        Ldelema(typeof(int));
        return ref IL.ReturnRef<int>();
    }

    public int* ReturnPointer(int[] values, int offset)
    {
        Ldarg(nameof(values));
        Ldarg(nameof(offset));
        Ldelema(typeof(int));
        return IL.ReturnPointer<int>();
    }

    public void* ReturnVoidPointer(int[] values, int offset)
    {
        Ldarg(nameof(values));
        Ldarg(nameof(offset));
        Ldelema(typeof(int));
        return IL.ReturnPointer();
    }

    public int ReturnRefWithDereference(int[] values, int offset)
    {
        Ldarg(nameof(values));
        Ldarg(nameof(offset));
        Ldelema(typeof(int));
        return IL.ReturnRef<int>();
    }

    public double ReturnRefWithDereferenceAndConversion(int[] values, int offset)
    {
        Ldarg(nameof(values));
        Ldarg(nameof(offset));
        Ldelema(typeof(int));
        return IL.ReturnRef<int>();
    }

    public void* ReturnPointerWithConversion(int[] values, int offset)
    {
        Ldarg(nameof(values));
        Ldarg(nameof(offset));
        Ldelema(typeof(int));
        return IL.ReturnPointer<int>();
    }

    public int ReturnPointerWithDereference(int[] values, int offset)
    {
        Ldarg(nameof(values));
        Ldarg(nameof(offset));
        Ldelema(typeof(int));
        return *IL.ReturnPointer<int>();
    }

    public void NoOpCodeByte()
    {
        IL.Push(IntArrayField);
        IL.Push(0);
        No(0x01 | 0x04);
        Ldelema(typeof(int));
        Pop();
    }

    public void NoOpCodeEnum()
    {
        IL.Push(IntArrayField);
        IL.Push(0);
        No(NoArg.TypeCheck | NoArg.RangeCheck | NoArg.NullCheck);
        Ldelema(typeof(int));
        Pop();
    }
}
