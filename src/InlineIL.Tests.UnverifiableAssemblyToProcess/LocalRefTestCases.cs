﻿using System.Diagnostics.CodeAnalysis;
using System.Reflection.Emit;
using InlineIL;

[SuppressMessage("ReSharper", "UnusedMember.Global")]
public class LocalRefTestCases
{
    public byte UsePinnedLocalVariables(byte[] buf, int offset)
    {
        IL.DeclareLocals(
            new LocalVar("buf", typeof(byte).MakeByRefType()).Pinned()
        );

        IL.Push(buf);
        IL.Emit(OpCodes.Ldc_I4_0);
        IL.Emit(OpCodes.Ldelema, typeof(byte));
        IL.Emit(OpCodes.Stloc, new LocalRef("buf"));

        IL.Emit(OpCodes.Ldloc, new LocalRef("buf"));
        IL.Emit(OpCodes.Conv_I);

        IL.Push(offset);
        IL.Emit(OpCodes.Add);
        IL.Emit(OpCodes.Ldind_U1);

        return IL.Return<byte>();
    }
}