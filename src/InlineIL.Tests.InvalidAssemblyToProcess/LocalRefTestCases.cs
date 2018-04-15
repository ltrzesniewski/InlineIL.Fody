using System;
using System.Diagnostics.CodeAnalysis;
using System.Reflection.Emit;
using InlineIL;

[SuppressMessage("ReSharper", "UnusedMember.Global")]
public class LocalRefTestCases
{
    public void UndefinedLocal()
    {
        IL.Emit(OpCodes.Ldloc, new LocalRef("foo"));
    }

    public void UndefinedLocal2()
    {
        IL.DeclareLocals(new LocalVar("bar", typeof(int)));
        IL.Emit(OpCodes.Ldloc, new LocalRef("foo"));
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
        IL.Emit(OpCodes.Ldloc, new LocalRef(null));
    }

    public void NullLocalRef()
    {
        IL.Emit(OpCodes.Ldloc, (LocalRef)null);
    }

    public void UnusedInstance()
    {
        GC.KeepAlive(new LocalRef("foo"));
    }

    public void UndefinedLocalByIndexMacro()
    {
        IL.Emit(OpCodes.Ldloc_0);
    }

    public void UndefinedLocalByIndex()
    {
        IL.Emit(OpCodes.Ldloc, 0);
    }

    public void LocalOutOfRangeMacro()
    {
        IL.DeclareLocals(new LocalVar("foo", typeof(int)));
        IL.Emit(OpCodes.Ldloc_1);
    }

    public void LocalOutOfRange()
    {
        IL.DeclareLocals(new LocalVar("foo", typeof(int)));
        IL.Emit(OpCodes.Ldloc, 1);
    }
}
