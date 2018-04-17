using System.Diagnostics.CodeAnalysis;
using InlineIL;
using static InlineIL.IL.Emit;

[SuppressMessage("ReSharper", "UnusedMember.Global")]
public class LocalVarsTestCases
{
    public void UndefinedLocal()
    {
        Ldloc("foo");
    }

    public void UndefinedLocal2()
    {
        IL.DeclareLocals(new LocalVar("bar", typeof(int)));
        Ldloc("foo");
    }

    public void RedefinedLocal()
    {
        IL.DeclareLocals(
            new LocalVar("foo", typeof(int)),
            new LocalVar("foo", typeof(int))
        );
    }

    public void MultipleDeclarations()
    {
        IL.DeclareLocals(
            new LocalVar("foo", typeof(int))
        );

        IL.DeclareLocals(
            new LocalVar("bar", typeof(int))
        );
    }

    public void NullLocal()
    {
        IL.DeclareLocals(
            null
        );
    }

    public void NullLocalName()
    {
        IL.DeclareLocals(
            new LocalVar(null, typeof(int))
        );
    }

    public void NullLocalRefName()
    {
        Ldloc(null);
    }

    public void UndefinedLocalByIndexMacro()
    {
        Ldloc_0();
    }

    public void UndefinedLocalByIndex()
    {
        Ldloc(0);
    }

    public void LocalOutOfRangeMacro()
    {
        IL.DeclareLocals(new LocalVar("foo", typeof(int)));
        Ldloc_1();
    }

    public void LocalOutOfRange()
    {
        IL.DeclareLocals(new LocalVar("foo", typeof(int)));
        Ldloc(1);
    }
}
