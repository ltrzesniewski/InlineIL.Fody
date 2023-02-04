using System;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using System.Runtime.InteropServices;
using InlineIL.Tests.Common;
using static InlineIL.IL.Emit;

namespace InlineIL.Tests.UnverifiableAssemblyToProcess;

[TestCases]
[SuppressMessage("ReSharper", "UnusedType.Global")]
[SuppressMessage("ReSharper", "UnusedMember.Global")]
[SuppressMessage("ReSharper", "UnusedParameter.Global")]
[SuppressMessage("ReSharper", "EntityNameCapturedOnly.Global")]
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
        Ldc_I4(42);
        Ldftn(new MethodRef(typeof(StandAloneMethodSigTestCases), nameof(IndirectCallStaticTargetMethod)));
        Calli(StandAloneMethodSig.ManagedMethod(CallingConventions.Standard, typeof(int), typeof(int)));
        return IL.Return<int>();
    }

    public int CallIndirectInstance()
    {
        IL.Push(this);
        IL.Push(42);
        Ldftn(new MethodRef(typeof(StandAloneMethodSigTestCases), nameof(IndirectCallInstanceTargetMethod)));
        Calli(new StandAloneMethodSig(CallingConventions.Standard | CallingConventions.HasThis, typeof(int), typeof(int)));
        return IL.Return<int>();
    }

#if NETFRAMEWORK
    public int CallIndirectVarArg()
    {
        IL.Push(40);
        IL.Push(10);
        IL.Push(20);
        Ldftn(new MethodRef(typeof(StandAloneMethodSigTestCases), nameof(IndirectCallVarArgTargetMethod)));
        Calli(new StandAloneMethodSig(CallingConventions.VarArgs, typeof(int), typeof(int)).WithOptionalParameters(typeof(int), typeof(int)));
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
        Calli(new StandAloneMethodSig(CallingConvention.StdCall, typeof(int), typeof(int), typeof(int), typeof(int), typeof(int), typeof(int), typeof(int), typeof(int), typeof(int)));

        GC.KeepAlive(fn);
        return IL.Return<int>();
    }

    public int CallIndirectNativeStdcallAlt()
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
        Calli(StandAloneMethodSig.UnmanagedMethod(CallingConvention.StdCall, typeof(int), typeof(int), typeof(int), typeof(int), typeof(int), typeof(int), typeof(int), typeof(int), typeof(int)));

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
        Calli(new StandAloneMethodSig(CallingConvention.Cdecl, typeof(int), typeof(int), typeof(int), typeof(int), typeof(int), typeof(int), typeof(int), typeof(int), typeof(int)));

        GC.KeepAlive(fn);
        return IL.Return<int>();
    }

    public int TailCallIndirectStatic()
    {
        IL.Push(42);
        Ldftn(new MethodRef(typeof(StandAloneMethodSigTestCases), nameof(IndirectCallStaticTargetMethod)));
        Tail();
        Calli(new StandAloneMethodSig(CallingConventions.Standard, typeof(int), typeof(int)));
        return IL.Return<int>();
    }

    public void TailCallIndirectStaticVoid()
    {
        Ldftn(new MethodRef(typeof(StandAloneMethodSigTestCases), nameof(IndirectCallStaticVoidTargetMethod)));
        Tail();
        Calli(new StandAloneMethodSig(CallingConventions.Standard, typeof(void)));
    }

    public int BranchOverTailCall(bool branch)
    {
        IL.Push(42);

        Ldarg(nameof(branch));
        Brtrue("end");

        Dup();
        Add();
        Ldftn(new MethodRef(typeof(StandAloneMethodSigTestCases), nameof(IndirectCallStaticTargetMethod)));
        Tail();
        Calli(new StandAloneMethodSig(CallingConventions.Standard, typeof(int), typeof(int)));

        IL.MarkLabel("end");
        return IL.Return<int>();
    }

    public int MultipleTailCalls(bool branch)
    {
        if (branch)
        {
            IL.Push(1);
            Ldftn(new MethodRef(typeof(StandAloneMethodSigTestCases), nameof(IndirectCallStaticTargetMethod)));
            Tail();
            Calli(new StandAloneMethodSig(CallingConventions.Standard, typeof(int), typeof(int)));
            return IL.Return<int>();
        }

        IL.Push(2);
        Ldftn(new MethodRef(typeof(StandAloneMethodSigTestCases), nameof(IndirectCallStaticTargetMethod)));
        Tail();
        Calli(new StandAloneMethodSig(CallingConventions.Standard, typeof(int), typeof(int)));
        return IL.Return<int>();
    }

    public int MixedNonTailAndTailCall(bool branch)
    {
        if (branch)
        {
            IL.Push(1);
            Ldftn(new MethodRef(typeof(StandAloneMethodSigTestCases), nameof(IndirectCallStaticTargetMethod)));
            Tail();
            Calli(new StandAloneMethodSig(CallingConventions.Standard, typeof(int), typeof(int)));
            return IL.Return<int>();
        }

        IL.Push(2);
        return IL.Return<int>();
    }

    public int MixedNonTailAndTailCall2(bool branch)
    {
        if (branch)
        {
            IL.Push(1);
            return IL.Return<int>();
        }

        IL.Push(2);
        Ldftn(new MethodRef(typeof(StandAloneMethodSigTestCases), nameof(IndirectCallStaticTargetMethod)));
        Tail();
        Calli(new StandAloneMethodSig(CallingConventions.Standard, typeof(int), typeof(int)));
        return IL.Return<int>();
    }

    private static int IndirectCallStaticTargetMethod(int value) => value;
    private int IndirectCallInstanceTargetMethod(int value) => value;

    private void IndirectCallStaticVoidTargetMethod()
    {
    }

#if NETFRAMEWORK
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
