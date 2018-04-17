using System;
using System.Diagnostics.CodeAnalysis;
using InlineIL;
using static InlineIL.IL.Emit;

[SuppressMessage("ReSharper", "UnusedMember.Global")]
public class BasicTestCases
{
    public void InvalidUnreachable()
    {
        IL.Unreachable();
    }

    public void InvalidReturn()
    {
        IL.Return<int>();
    }

    public void UnusedInstance()
    {
        GC.KeepAlive(typeof(IL));
    }

    public void InvalidPushUsage()
    {
        var guid = Guid.NewGuid();

        IL.Push(42);
        IL.Push(guid);
    }

    public void NonExistingParameter()
    {
        Ldarg("foo");
    }
}
