using System.Diagnostics.CodeAnalysis;
using InlineIL;
using static InlineIL.IL.Emit;

[SuppressMessage("ReSharper", "UnusedMember.Global")]
public class LocalRefTestCases
{
    public byte UsePinnedLocalVariables(byte[] buf, int offset)
    {
        IL.DeclareLocals(
            new LocalVar("buf", typeof(byte).MakeByRefType()).Pinned()
        );

        IL.Push(buf);
        Ldc_I4_0();
        Ldelema(typeof(byte));
        Stloc("buf");

        Ldloc("buf");
        Conv_I();

        IL.Push(offset);
        Add();
        Ldind_U1();

        return IL.Return<byte>();
    }

    public int UseLocalVariablesNoInit(int value)
    {
        IL.DeclareLocals(
            false,
            new LocalVar("foo", typeof(int)),
            new LocalVar("bar", typeof(int))
        );

        IL.Push(value);
        Stloc("foo");

        Ldc_I4(42);
        Stloc("bar");

        Ldloc("foo");
        Ldloc("bar");
        Add();

        return IL.Return<int>();
    }
}
