using System;
using System.Diagnostics.CodeAnalysis;
using System.Reflection.Emit;
using InlineIL;

[SuppressMessage("ReSharper", "UnusedMember.Global")]
public class BasicTestCases
{
    public void HandlePrefixesInDebugMode(ref Guid value)
    {
        IL.Push(ref value);

        IL.Emit(OpCodes.Ldc_I4_0);
        IL.Emit(OpCodes.Conv_U1);

        IL.Emit(OpCodes.Sizeof, typeof(Guid));

        IL.Emit(OpCodes.Unaligned, 1);
        IL.Emit(OpCodes.Initblk);
    }
}
