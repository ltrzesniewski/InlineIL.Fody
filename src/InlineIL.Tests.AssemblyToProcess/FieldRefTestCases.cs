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
        => GenericType<Guid>.GetValue();

    public int GetValueFromGenericType2()
        => GenericType<int>.GetValue2();

    public int GetValueFromImportedGenericType()
    {
        IL.Push((42, 10));
        Ldfld(new FieldRef(typeof(ValueTuple<int, int>), nameof(ValueTuple<int, int>.Item2)));
        return IL.Return<int>();
    }

    public T GetValueFromImportedGenericType2<T>(T value)
    {
        IL.Push((42, value));
        Ldfld(new FieldRef(typeof(ValueTuple<int, T>), nameof(ValueTuple<int, T>.Item2)));
        return IL.Return<T>();
    }

    [SuppressMessage("ReSharper", "StaticMemberInGenericType")]
    private class GenericType<T>
    {
        private static readonly int _value = 10;
        private static readonly T _value2 = typeof(T) == typeof(int) ? (T)(object)10 : default;

        public static int GetValue()
        {
            Ldsfld(new FieldRef(typeof(GenericType<T>), nameof(_value)));
            return IL.Return<int>();
        }

        public static T GetValue2()
        {
            Ldsfld(new FieldRef(typeof(GenericType<T>), nameof(_value2)));
            return IL.Return<T>();
        }
    }
}
