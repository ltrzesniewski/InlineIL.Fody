using System;
using System.Diagnostics.CodeAnalysis;
using System.Reflection.Emit;
using InlineIL;

[SuppressMessage("ReSharper", "UnusedMember.Global")]
public class FieldRefTestCases
{
    public void NullField()
    {
        IL.Emit(OpCodes.Ldfld, new FieldRef(typeof(FieldRefTestCases), null));
    }

    public void NullFieldRef()
    {
        IL.Emit(OpCodes.Ldfld, (FieldRef)null);
    }

    public void UnknownField()
    {
        IL.Emit(OpCodes.Ldfld, new FieldRef(typeof(FieldRefTestCases), "Nope"));
    }

    public void UnusedInstance()
    {
        GC.KeepAlive(new FieldRef(typeof(int), "foo"));
    }
}
