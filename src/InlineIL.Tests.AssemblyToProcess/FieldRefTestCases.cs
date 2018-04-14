using System;
using System.Diagnostics.CodeAnalysis;
using System.Reflection.Emit;
using InlineIL;
using static InlineIL.ILEmit;

[SuppressMessage("ReSharper", "UnusedMember.Global")]
[SuppressMessage("ReSharper", "UnassignedField.Global")]
[SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
public class FieldRefTestCases
{
    public int IntField;

    public int ReturnIntField()
    {
        IL.Emit(OpCodes.Ldarg_0);
        IL.Emit(OpCodes.Ldfld, new FieldRef(typeof(FieldRefTestCases), nameof(IntField)));
        return IL.Return<int>();
    }

    public int ReturnIntFieldAlt()
    {
        Ldarg_0();
        Ldfld(new FieldRef(typeof(FieldRefTestCases), nameof(IntField)));
        return IL.Return<int>();
    }

    public RuntimeFieldHandle ReturnFieldHandle()
    {
        Ldtoken(new FieldRef(typeof(FieldRefTestCases), nameof(IntField)));
        return IL.Return<RuntimeFieldHandle>();
    }
}
