using System;
using System.Diagnostics.CodeAnalysis;
using InlineIL;
using static InlineIL.IL.Emit;

[SuppressMessage("ReSharper", "UnusedMember.Global")]
public class BasicTestCases
{
    private int _intField;

    public void InvalidUnreachable()
    {
        IL.Unreachable();
    }

    public void InvalidReturn()
    {
        IL.Return<int>();
    }

    public void UnusedInstance()
    {
        GC.KeepAlive(typeof(IL));
    }

    public void InvalidPushUsage()
    {
        var guid = Guid.NewGuid();

        IL.Push(42);
        IL.Push(guid);
    }

    public void NonExistingParameter()
    {
        Ldarg("foo");
    }

    public void PopToField()
    {
        IL.Pop(out _intField);
    }

    public void PopToArray()
    {
        var array = new int[1];
        IL.Pop(out array[0]);
    }

    public void LibRefLocal()
    {
        TypeRef local = null;
        DoNothing(ref local);
    }

    public void LibRefLocal2()
    {
        Foo<TypeRef> local = null;
        DoNothing(ref local);
    }

    public void LibRefGenericCall()
    {
        DoNothing<TypeRef>();
    }

    public void LibRefGenericCall2()
    {
        DoNothing<Foo<TypeRef>>();
    }

    [SuppressMessage("ReSharper", "UnusedParameter.Global")]
    public void LibRefParam(TypeRef typeRef)
    {
    }

    [SuppressMessage("ReSharper", "UnusedParameter.Global")]
    public void LibRefParam2(Foo<TypeRef> typeRef)
    {
    }

    [SomeAttribute(typeof(TypeRef))]
    public void LibRefAttributeCtor()
    {
    }

    [SomeAttribute(Type = typeof(TypeRef))]
    public void LibRefAttributeParam()
    {
    }

    [SuppressMessage("ReSharper", "UnusedParameter.Global")]
    public void LibRefAttributeMethodParam([SomeAttribute(typeof(TypeRef))] int foo)
    {
    }

    [SuppressMessage("ReSharper", "UnusedTypeParameter")]
    public void LibRefGenericConstraint<T>()
        where T : Foo<TypeRef>
    {
    }

    [SuppressMessage("ReSharper", "UnusedTypeParameter")]
    public void LibRefGenericParamAttribute<[SomeAttribute(typeof(TypeRef))] T>()
    {
    }

    private static void DoNothing<T>(ref T _)
    {
    }

    [SuppressMessage("ReSharper", "UnusedTypeParameter")]
    private static void DoNothing<T>()
    {
    }

    [SuppressMessage("ReSharper", "ClassNeverInstantiated.Local")]
    [SuppressMessage("ReSharper", "ClassNeverInstantiated.Global")]
    [SuppressMessage("ReSharper", "UnusedTypeParameter")]
    public class Foo<T>
    {
    }

    [SuppressMessage("ReSharper", "UnusedTypeParameter")]
    public interface IFoo<T>
    {
    }

    private class SomeAttributeAttribute : Attribute
    {
        public Type Type { get; set; }

        public SomeAttributeAttribute()
        {
        }

        public SomeAttributeAttribute(Type type)
        {
            Type = type;
        }
    }

    [SomeAttribute(typeof(TypeRef))]
    public class LibRefTypeAttr
    {
    }

    public class LibRefFieldAttr
    {
        [SomeAttribute(typeof(TypeRef))] public int Item;
    }

    public class LibRefPropAttr
    {
        [SomeAttribute(typeof(TypeRef))]
        public int Item { get; set; }
    }

    public class LibRefEventAttr
    {
#pragma warning disable 67
        [SomeAttribute(typeof(TypeRef))]
        public event Action Item;
#pragma warning restore 67
    }

    public class LibRefFieldType
    {
        public TypeRef Item;
    }

    public class LibRefPropType
    {
        public TypeRef Item { get; set; }
    }

    public class LibRefEventType
    {
#pragma warning disable 67
        public event Action<TypeRef> Item;
#pragma warning restore 67
    }

    public class LibRefBaseType : Foo<TypeRef>
    {
    }

    public class LibRefIfaceType : IFoo<TypeRef>
    {
    }
}
