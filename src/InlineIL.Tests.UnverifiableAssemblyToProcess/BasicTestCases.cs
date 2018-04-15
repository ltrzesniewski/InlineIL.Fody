using System;
using System.Diagnostics.CodeAnalysis;
using System.Reflection.Emit;
using InlineIL;
using static InlineIL.ILEmit;

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

    public void HandlePrefixesInDebugModeAlt(ref Guid value)
    {
        IL.Push(ref value);

        IL.Emit(OpCodes.Ldc_I4_0);
        IL.Emit(OpCodes.Conv_U1);

        IL.Emit(OpCodes.Sizeof, typeof(Guid));

        IL.Emit(OpCodes.Unaligned, 1);
        IL.Emit(OpCodes.Initblk);
    }
}
