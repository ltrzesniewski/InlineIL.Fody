using System;
using System.Diagnostics.CodeAnalysis;
using InlineIL;

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
}
