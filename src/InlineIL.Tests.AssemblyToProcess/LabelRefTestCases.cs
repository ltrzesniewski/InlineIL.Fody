using System.Diagnostics.CodeAnalysis;
using System.Reflection.Emit;
using InlineIL;
using static InlineIL.ILEmit;

[SuppressMessage("ReSharper", "UnusedMember.Global")]
public class LabelRefTestCases
{
    public int Branch(bool returnOne)
    {
        IL.Push(returnOne);
        Brtrue("one");
        IL.Push(42);
        Br("end");
        IL.MarkLabel("one");
        IL.Push(1);
        IL.MarkLabel("end");
        return IL.Return<int>();
    }

    public int BranchAlt(bool returnOne)
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
        Switch("one", "two", "three");

        IL.Push(42);
        Br("end");

        IL.MarkLabel("one");
        IL.Push(1);
        Br("end");

        IL.MarkLabel("two");
        IL.Push(2);
        Br("end");

        IL.MarkLabel("three");
        IL.Push(3);

        IL.MarkLabel("end");
        return IL.Return<int>();
    }

    public int JumpTableAlt(uint value)
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
