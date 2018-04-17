using System;
using System.Diagnostics.CodeAnalysis;
using InlineIL;
using static InlineIL.IL.Emit;

[SuppressMessage("ReSharper", "UnusedMember.Global")]
public class BasicTestCases
{
    public void HandlePrefixesInDebugMode(ref Guid value)
    {
        IL.Push(ref value);

        Ldc_I4_0();
        Conv_U1();

        Sizeof(typeof(Guid));

        Unaligned(1);
        Initblk();
    }
}
