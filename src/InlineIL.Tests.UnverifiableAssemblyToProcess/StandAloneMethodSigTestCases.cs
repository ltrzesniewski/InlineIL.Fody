using System;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using System.Reflection.Emit;
using System.Runtime.InteropServices;
using InlineIL;
using static InlineIL.ILEmit;

[SuppressMessage("ReSharper", "UnusedMember.Global")]
[SuppressMessage("ReSharper", "MemberCanBeMadeStatic.Local")]
public unsafe class StandAloneMethodSigTestCases
{
    public int CallIndirectStatic()
    {
        Ldc_I4(42);
        Ldftn(new MethodRef(typeof(StandAloneMethodSigTestCases), nameof(IndirectCallStaticTargetMethod)));
        Calli(new StandAloneMethodSig(CallingConventions.Standard, typeof(int), typeof(int)));
        return IL.Return<int>();
    }

    public int CallIndirectStaticAlt()
    {
        IL.Push(42);
        IL.Emit(OpCodes.Ldftn, new MethodRef(typeof(StandAloneMethodSigTestCases), nameof(IndirectCallStaticTargetMethod)));
        IL.Emit(OpCodes.Calli, new StandAloneMethodSig(CallingConventions.Standard, typeof(int), typeof(int)));
        return IL.Return<int>();
    }

    public int CallIndirectInstance()
    {
        IL.Push(this);
        IL.Push(42);
        IL.Emit(OpCodes.Ldftn, new MethodRef(typeof(StandAloneMethodSigTestCases), nameof(IndirectCallInstanceTargetMethod)));
        IL.Emit(OpCodes.Calli, new StandAloneMethodSig(CallingConventions.Standard | CallingConventions.HasThis, typeof(int), typeof(int)));
        return IL.Return<int>();
    }

#if NETFWK
    public int CallIndirectVarArg()
    {
        IL.Push(40);
        IL.Push(10);
        IL.Push(20);
        IL.Emit(OpCodes.Ldftn, new MethodRef(typeof(StandAloneMethodSigTestCases), nameof(IndirectCallVarArgTargetMethod)));
        IL.Emit(OpCodes.Calli, new StandAloneMethodSig(CallingConventions.VarArgs, typeof(int), typeof(int)).WithOptionalParameters(typeof(int), typeof(int)));
        return IL.Return<int>();
    }
#endif

    public int CallIndirectNativeStdcall()
    {
        var fn = new IntToIntStdcall(IndirectCallNativeTargetMethod);

        IL.Push(40);
        IL.Push(2);
        IL.Push(40);
        IL.Push(-20);
        IL.Push(-20);
        IL.Push(-20);
        IL.Push(5);
        IL.Push(15);
        IL.Push(Marshal.GetFunctionPointerForDelegate(fn).ToPointer());
        IL.Emit(OpCodes.Calli, new StandAloneMethodSig(CallingConvention.StdCall, typeof(int), typeof(int), typeof(int), typeof(int), typeof(int), typeof(int), typeof(int), typeof(int), typeof(int)));

        GC.KeepAlive(fn);
        return IL.Return<int>();
    }

    public int CallIndirectNativeCdecl()
    {
        var fn = new IntToIntCdecl(IndirectCallNativeTargetMethod);

        IL.Push(40);
        IL.Push(2);
        IL.Push(40);
        IL.Push(-20);
        IL.Push(-20);
        IL.Push(-20);
        IL.Push(5);
        IL.Push(15);
        IL.Push(Marshal.GetFunctionPointerForDelegate(fn).ToPointer());
        IL.Emit(OpCodes.Calli, new StandAloneMethodSig(CallingConvention.Cdecl, typeof(int), typeof(int), typeof(int), typeof(int), typeof(int), typeof(int), typeof(int), typeof(int), typeof(int)));

        GC.KeepAlive(fn);
        return IL.Return<int>();
    }

    private static int IndirectCallStaticTargetMethod(int value) => value;
    private int IndirectCallInstanceTargetMethod(int value) => value;

#if NETFWK
    private static int IndirectCallVarArgTargetMethod(int value, __arglist)
    {
        var it = new ArgIterator(__arglist);
        return value + it.GetRemainingCount();
    }
#endif

    private static int IndirectCallNativeTargetMethod(int a, int b, int c, int d, int e, int f, int g, int h)
        => a + b + c + d + e + f + g + h;

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    private delegate int IntToIntStdcall(int a, int b, int c, int d, int e, int f, int g, int h);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    private delegate int IntToIntCdecl(int a, int b, int c, int d, int e, int f, int g, int h);
}
