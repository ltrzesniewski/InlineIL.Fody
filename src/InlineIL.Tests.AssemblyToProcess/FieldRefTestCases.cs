using System;
using System.Diagnostics.CodeAnalysis;
using InlineIL;
using static InlineIL.IL.Emit;

[SuppressMessage("ReSharper", "UnusedMember.Global")]
[SuppressMessage("ReSharper", "UnassignedField.Global")]
[SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
public class FieldRefTestCases
{
    public int IntField;

    public int ReturnIntField()
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

    public int GetValueFromGenericType()
        => GenericType<int>.GetValue();

    private class GenericType<T>
    {
        [SuppressMessage("ReSharper", "StaticMemberInGenericType")]
        private static readonly int _value = 10;

        public static int GetValue()
        {
            Ldsfld(new FieldRef(typeof(GenericType<T>), nameof(_value)));
            return IL.Return<int>();
        }
    }
}
