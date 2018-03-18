using System;
using System.Diagnostics.CodeAnalysis;
using System.Reflection.Emit;
using InlineIL;

[SuppressMessage("ReSharper", "UnusedMember.Global")]
[SuppressMessage("ReSharper", "UnusedParameter.Local")]
public class MethodRefTestCases
{
    public void UnknownMethodWithoutParams()
    {
        IL.Emit(OpCodes.Call, new MethodRef(typeof(object), "Nope"));
    }

    public void UnknownMethodWithParams()
    {
        IL.Emit(OpCodes.Call, new MethodRef(typeof(object), "Nope", typeof(int)));
    }

    public void AmbiguousMethod()
    {
        IL.Emit(OpCodes.Call, new MethodRef(typeof(MethodRefTestCases), nameof(Foo)));
    }

    public void NullMethod()
    {
        IL.Emit(OpCodes.Call, new MethodRef(typeof(MethodRefTestCases), null));
    }

    public void NullMethodRef()
    {
        IL.Emit(OpCodes.Call, (MethodRef)null);
    }

    public void UnusedInstance()
    {
        GC.KeepAlive(new MethodRef(typeof(int), "foo"));
    }

    public void NotAVarArgMethod()
    {
        IL.Emit(OpCodes.Call, new MethodRef(typeof(MethodRefTestCases), nameof(NotAVarArgMethod)).WithOptionalParameters(typeof(int)));
    }

    public void VarArgParamsAlreadySupplied()
    {
        IL.Emit(OpCodes.Call, new MethodRef(typeof(MethodRefTestCases), nameof(VarArgMethod)).WithOptionalParameters(typeof(int)).WithOptionalParameters(typeof(int)));
    }

    public void EmptyVarArgParams()
    {
        IL.Emit(OpCodes.Call, new MethodRef(typeof(MethodRefTestCases), nameof(VarArgMethod)).WithOptionalParameters());
    }

    private static void Foo()
    {
    }

    private static void Foo(int i)
    {
    }

    private static int[] VarArgMethod(int count, __arglist) => null;
}
