using System;
using System.Diagnostics.CodeAnalysis;
using System.Text;
using InlineIL;
using static InlineIL.IL.Emit;

[SuppressMessage("ReSharper", "UnusedMember.Global")]
[SuppressMessage("ReSharper", "UnusedParameter.Local")]
public class MethodRefTestCases
{
    public int Value { get; set; }

    public event Action Event;

    public Type ReturnType<T>()
    {
        Ldtoken(typeof(T));
        Call(new MethodRef(typeof(Type), nameof(Type.GetTypeFromHandle)));
        Ret();
        throw IL.Unreachable();
    }

    public Type ReturnTypeAlt<T>()
    {
        Ldtoken(typeof(T));
        Call(new MethodRef(typeof(Type), nameof(Type.GetTypeFromHandle)));
        Ret();
        throw IL.Unreachable();
    }

    public int[] ResolveOverloads()
    {
        var result = new int[7];

        IL.Push(result);
        Ldc_I4_0();
        Call(new MethodRef(typeof(MethodRefTestCases), nameof(OverloadedMethod), new TypeRef[0]));
        Stelem_I4();

        IL.Push(result);
        Ldc_I4_1();
        Call(new MethodRef(typeof(MethodRefTestCases), nameof(OverloadedMethod), Array.Empty<TypeRef>()));
        Stelem_I4();

        IL.Push(result);
        Ldc_I4_2();
        Ldc_I4(42);
        Call(new MethodRef(typeof(MethodRefTestCases), nameof(OverloadedMethod), typeof(int)));
        Stelem_I4();

        IL.Push(result);
        Ldc_I4_3();
        IL.Push(result);
        Ldc_I4_0();
        Ldelema(typeof(int));
        Call(new MethodRef(typeof(MethodRefTestCases), nameof(OverloadedMethod), new TypeRef(typeof(int)).MakeByRefType()));
        Stelem_I4();

        IL.Push(result);
        Ldc_I4_4();
        Ldnull();
        Call(new MethodRef(typeof(MethodRefTestCases), nameof(OverloadedMethod), typeof(int[])));
        Stelem_I4();

        IL.Push(result);
        Ldc_I4_5();
        Ldc_R8(42.0);
        IL.Push(result);
        Ldc_I4_0();
        Ldelema(typeof(int));
        Call(new MethodRef(typeof(MethodRefTestCases), nameof(OverloadedMethod), typeof(double), new TypeRef(typeof(int)).MakeByRefType()));
        Stelem_I4();

        IL.Push(result);
        Ldc_I4_6();
        Ldc_R8(42.0);
        Ldc_I4(42);
        Call(new MethodRef(typeof(MethodRefTestCases), nameof(OverloadedMethod), typeof(double), typeof(int)));
        Stelem_I4();

        IL.Push(result);
        return IL.Return<int[]>();
    }

    public int CallGenericMethod()
    {
        IL.Push(42);
        Call(new MethodRef(typeof(MethodRefTestCases), nameof(GenericMethod)).MakeGenericMethod(typeof(int)));
        return IL.Return<int>();
    }

    public string CallMethodInGenericType()
    {
        Call(new MethodRef(typeof(GenericType<>).MakeGenericType(typeof(Guid)), nameof(GenericType<object>.NormalMethod)));
        return IL.Return<string>();
    }

    public string CallMethodInGenericTypeArray()
    {
        Call(new MethodRef(typeof(GenericType<>).MakeGenericType(typeof(Guid[])), nameof(GenericType<object>.NormalMethod)));
        return IL.Return<string>();
    }

    public string CallMethodInGenericTypeGeneric<TClass>()
    {
        Call(new MethodRef(typeof(GenericType<>).MakeGenericType(typeof(TClass)), nameof(GenericType<object>.NormalMethod)));
        return IL.Return<string>();
    }

    public string CallGenericMethodInGenericType()
    {
        Call(new MethodRef(typeof(GenericType<>).MakeGenericType(typeof(Guid)), nameof(GenericType<object>.GenericMethod)).MakeGenericMethod(typeof(TimeSpan)));
        return IL.Return<string>();
    }

    public string CallGenericMethodInGenericTypeArray()
    {
        Call(new MethodRef(typeof(GenericType<>).MakeGenericType(typeof(Guid[])), nameof(GenericType<object>.GenericMethod)).MakeGenericMethod(typeof(TimeSpan[])));
        return IL.Return<string>();
    }

    public string CallGenericMethodInGenericTypeGeneric<TClass, TMethod>()
    {
        Call(new MethodRef(typeof(GenericType<>).MakeGenericType(typeof(TClass)), nameof(GenericType<object>.GenericMethod)).MakeGenericMethod(typeof(TMethod)));
        return IL.Return<string>();
    }

    public bool CallCoreLibMethod<T>(T? value)
        where T : struct
    {
        Ldarga(1);
        Call(new MethodRef(typeof(T?), "get_HasValue"));
        return IL.Return<bool>();
    }

    public RuntimeMethodHandle ReturnMethodHandle()
    {
        Ldtoken(new MethodRef(typeof(Type), nameof(Type.GetTypeFromHandle)));
        return IL.Return<RuntimeMethodHandle>();
    }

    public int GetValue()
    {
        Ldarg_0();
        Call(MethodRef.PropertyGet(typeof(MethodRefTestCases), nameof(Value)));
        return IL.Return<int>();
    }

    public void SetValue(int value)
    {
        Ldarg_0();
        Ldarg(nameof(value));
        Call(MethodRef.PropertySet(typeof(MethodRefTestCases), nameof(Value)));
    }

    public void AddEvent(Action callback)
    {
        Ldarg_0();
        Ldarg(nameof(callback));
        Call(MethodRef.EventAdd(typeof(MethodRefTestCases), nameof(Event)));
    }

    public void RemoveEvent(Action callback)
    {
        Ldarg_0();
        Ldarg(nameof(callback));
        Call(MethodRef.EventRemove(typeof(MethodRefTestCases), nameof(Event)));
    }

    public StringBuilder CallDefaultConstructor()
    {
        Newobj(MethodRef.Constructor(typeof(StringBuilder)));
        return IL.Return<StringBuilder>();
    }

    public StringBuilder CallNonDefaultConstructor()
    {
        Ldc_I4(42);
        Newobj(MethodRef.Constructor(typeof(StringBuilder), typeof(int)));
        return IL.Return<StringBuilder>();
    }

    public RuntimeMethodHandle GetTypeInitializer()
    {
        Ldtoken(MethodRef.TypeInitializer(typeof(TypeWithInitializer)));
        return IL.Return<RuntimeMethodHandle>();
    }

    public void RaiseEvent()
        => Event?.Invoke();

#if NETFRAMEWORK
    public int[] CallVarArgMethod()
    {
        IL.Push(5);
        IL.Push(1);
        IL.Push(2);
        IL.Push(3);
        IL.Emit.Call(new MethodRef(typeof(MethodRefTestCases), nameof(VarArgMethod)).WithOptionalParameters(typeof(int), typeof(int), typeof(int)));
        return IL.Return<int[]>();
    }
#endif

    private static int OverloadedMethod() => 10;
    private static int OverloadedMethod(int a) => 20;
    private static int OverloadedMethod(ref int a) => 30;
    private static int OverloadedMethod(int[] a) => 40;
    private static int OverloadedMethod(double a, ref int b) => 50;
    private static int OverloadedMethod(double a, int b) => 60;

    private static T GenericMethod<T>(T value) => value;

    private class GenericType<TClass>
    {
        public static string NormalMethod()
            => typeof(TClass).FullName;

        public static string GenericMethod<TMethod>()
            => $"{typeof(TClass).FullName} {typeof(TMethod).FullName}";
    }

#if NETFRAMEWORK
    private static int[] VarArgMethod(int count, __arglist)
    {
        var it = new ArgIterator(__arglist);
        var result = new int[count];

        for (var i = 0; i < count; ++i)
            result[i] = it.GetRemainingCount() > 0 ? __refvalue(it.GetNextArg(), int) : 0;

        return result;
    }
#endif

    private class TypeWithInitializer
    {
        static TypeWithInitializer()
        {
            GC.KeepAlive(null);
        }
    }
}
