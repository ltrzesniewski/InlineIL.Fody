using System;
using System.Diagnostics.CodeAnalysis;
using static InlineIL.IL.Emit;

namespace InlineIL.Tests.InvalidAssemblyToProcess;

[SuppressMessage("ReSharper", "UnusedType.Global")]
[SuppressMessage("ReSharper", "UnusedMember.Global")]
public class FieldRefTestCases
{
    public void NullField()
    {
        Ldfld(new FieldRef(typeof(FieldRefTestCases), null!));
    }

    public void NullFieldRef()
    {
        Ldfld(null!);
    }

    public void UnknownField()
    {
        Ldfld(new FieldRef(typeof(FieldRefTestCases), "Nope"));
    }

    public void UnusedInstance()
    {
        GC.KeepAlive(new FieldRef(typeof(int), "foo"));
    }
}
