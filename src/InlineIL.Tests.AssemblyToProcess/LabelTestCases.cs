using System.Diagnostics.CodeAnalysis;
using InlineIL.Tests.Common;
using static InlineIL.IL.Emit;

namespace InlineIL.Tests.AssemblyToProcess;

[TestCases]
[SuppressMessage("ReSharper", "UnusedType.Global")]
[SuppressMessage("ReSharper", "UnusedMember.Global")]
public class LabelTestCases
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

    public void UnusedLabel()
    {
        IL.MarkLabel("SomeLabel");
    }
}
