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

    private static void Foo()
    {
    }

    private static void Foo(int i)
    {
    }
}
