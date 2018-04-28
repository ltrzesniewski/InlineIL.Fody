using System;
using System.Diagnostics.CodeAnalysis;
using InlineIL;
using static InlineIL.IL.Emit;

[SuppressMessage("ReSharper", "UnusedMember.Global")]
[SuppressMessage("ReSharper", "UnusedParameter.Local")]
public class MethodRefTestCases
{
    public int Value { get; set; }

    public int ValueGetOnly => Value;

    public int ValueSetOnly
    {
        set => Value = value;
    }

    public void UnknownMethodWithoutParams()
    {
        Call(new MethodRef(typeof(object), "Nope"));
    }

    public void UnknownMethodWithParams()
    {
        Call(new MethodRef(typeof(object), "Nope", typeof(int)));
    }

    public void AmbiguousMethod()
    {
        Call(new MethodRef(typeof(MethodRefTestCases), nameof(Foo)));
    }

    public void NullMethod()
    {
        Call(new MethodRef(typeof(MethodRefTestCases), null));
    }

    public void NullMethodRef()
    {
        Call(null);
    }

    public void UnusedInstance()
    {
        GC.KeepAlive(new MethodRef(typeof(int), "foo"));
    }

    public void NotAGenericMethod()
    {
        Call(new MethodRef(typeof(MethodRefTestCases), nameof(NotAGenericMethod)).MakeGenericMethod(typeof(int)));
    }

    public void NoGenericArgsProvided()
    {
        Call(new MethodRef(typeof(MethodRefTestCases), nameof(GenericMethod)).MakeGenericMethod());
    }

    public void TooManyGenericArgsProvided()
    {
        Call(new MethodRef(typeof(MethodRefTestCases), nameof(GenericMethod)).MakeGenericMethod(typeof(int), typeof(string)));
    }

    public void NotAVarArgMethod()
    {
        Call(new MethodRef(typeof(MethodRefTestCases), nameof(NotAVarArgMethod)).WithOptionalParameters(typeof(int)));
    }

    public void VarArgParamsAlreadySupplied()
    {
        Call(new MethodRef(typeof(MethodRefTestCases), nameof(VarArgMethod)).WithOptionalParameters(typeof(int)).WithOptionalParameters(typeof(int)));
    }

    public void EmptyVarArgParams()
    {
        Call(new MethodRef(typeof(MethodRefTestCases), nameof(VarArgMethod)).WithOptionalParameters());
    }

    public void UnknownProperty()
    {
        Call(MethodRef.PropertyGet(typeof(MethodRefTestCases), "Nope"));
    }

    public void PropertyWithoutGetter()
    {
        Call(MethodRef.PropertyGet(typeof(MethodRefTestCases), nameof(ValueSetOnly)));
    }

    public void PropertyWithoutSetter()
    {
        Call(MethodRef.PropertySet(typeof(MethodRefTestCases), nameof(ValueGetOnly)));
    }

    private static void Foo()
    {
    }

    private static void Foo(int i)
    {
    }

    private static T GenericMethod<T>(T value) => value;

    private static int[] VarArgMethod(int count, __arglist) => null;
}
