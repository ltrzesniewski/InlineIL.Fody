using System.Diagnostics.CodeAnalysis;
using System.Reflection.Emit;
using InlineIL;

[SuppressMessage("ReSharper", "UnusedMember.Global")]
public class LabelRefTestCases
{
    public int Branch(bool returnOne)
    {
        IL.Push(returnOne);
        IL.Emit(OpCodes.Brtrue, new LabelRef("one"));
        IL.Push(42);
        IL.Emit(OpCodes.Br, new LabelRef("end"));
        IL.MarkLabel("one");
        IL.Push(1);
        IL.MarkLabel("end");
        return IL.Return<int>();
    }

    public int JumpTable(uint value)
    {
        IL.Push(value);
        IL.Emit(OpCodes.Switch, new LabelRef("one"), new LabelRef("two"), new LabelRef("three"));

        IL.Push(42);
        IL.Emit(OpCodes.Br, new LabelRef("end"));

        IL.MarkLabel("one");
        IL.Push(1);
        IL.Emit(OpCodes.Br, new LabelRef("end"));

        IL.MarkLabel("two");
        IL.Push(2);
        IL.Emit(OpCodes.Br, new LabelRef("end"));

        IL.MarkLabel("three");
        IL.Push(3);

        IL.MarkLabel("end");
        return IL.Return<int>();
    }
}
