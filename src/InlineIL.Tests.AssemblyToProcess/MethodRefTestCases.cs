using System;
using System.Diagnostics.CodeAnalysis;
using System.Reflection.Emit;
using InlineIL;
using static InlineIL.ILEmit;

[SuppressMessage("ReSharper", "UnusedMember.Global")]
[SuppressMessage("ReSharper", "UnusedParameter.Local")]
public class MethodRefTestCases
{
    public Type ReturnType<T>()
    {
        IL.Emit(OpCodes.Ldtoken, typeof(T));
        IL.Emit(OpCodes.Call, new MethodRef(typeof(Type), nameof(Type.GetTypeFromHandle)));
        IL.Emit(OpCodes.Ret);
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
        IL.Emit(OpCodes.Ldc_I4_0);
        IL.Emit(OpCodes.Call, new MethodRef(typeof(MethodRefTestCases), nameof(OverloadedMethod), new TypeRef[0]));
        IL.Emit(OpCodes.Stelem_I4);

        IL.Push(result);
        IL.Emit(OpCodes.Ldc_I4_1);
        IL.Emit(OpCodes.Call, new MethodRef(typeof(MethodRefTestCases), nameof(OverloadedMethod), Array.Empty<TypeRef>()));
        IL.Emit(OpCodes.Stelem_I4);

        IL.Push(result);
        IL.Emit(OpCodes.Ldc_I4_2);
        IL.Emit(OpCodes.Ldc_I4, 42);
        IL.Emit(OpCodes.Call, new MethodRef(typeof(MethodRefTestCases), nameof(OverloadedMethod), typeof(int)));
        IL.Emit(OpCodes.Stelem_I4);

        IL.Push(result);
        IL.Emit(OpCodes.Ldc_I4_3);
        IL.Push(result);
        IL.Emit(OpCodes.Ldc_I4_0);
        IL.Emit(OpCodes.Ldelema, typeof(int));
        IL.Emit(OpCodes.Call, new MethodRef(typeof(MethodRefTestCases), nameof(OverloadedMethod), new TypeRef(typeof(int)).MakeByRefType()));
        IL.Emit(OpCodes.Stelem_I4);

        IL.Push(result);
        IL.Emit(OpCodes.Ldc_I4_4);
        IL.Emit(OpCodes.Ldnull);
        IL.Emit(OpCodes.Call, new MethodRef(typeof(MethodRefTestCases), nameof(OverloadedMethod), typeof(int[])));
        IL.Emit(OpCodes.Stelem_I4);

        IL.Push(result);
        IL.Emit(OpCodes.Ldc_I4_5);
        IL.Emit(OpCodes.Ldc_R8, 42.0);
        IL.Push(result);
        IL.Emit(OpCodes.Ldc_I4_0);
        IL.Emit(OpCodes.Ldelema, typeof(int));
        IL.Emit(OpCodes.Call, new MethodRef(typeof(MethodRefTestCases), nameof(OverloadedMethod), typeof(double), new TypeRef(typeof(int)).MakeByRefType()));
        IL.Emit(OpCodes.Stelem_I4);

        IL.Push(result);
        IL.Emit(OpCodes.Ldc_I4_6);
        IL.Emit(OpCodes.Ldc_R8, 42.0);
        IL.Emit(OpCodes.Ldc_I4, 42);
        IL.Emit(OpCodes.Call, new MethodRef(typeof(MethodRefTestCases), nameof(OverloadedMethod), typeof(double), typeof(int)));
        IL.Emit(OpCodes.Stelem_I4);

        IL.Push(result);
        return IL.Return<int[]>();
    }

    public int CallGenericMethod()
    {
        IL.Push(42);
        IL.Emit(OpCodes.Call, new MethodRef(typeof(MethodRefTestCases), nameof(GenericMethod)).MakeGenericMethod(typeof(int)));
        return IL.Return<int>();
    }

    public string CallMethodInGenericType()
    {
        IL.Emit(OpCodes.Call, new MethodRef(typeof(GenericType<>).MakeGenericType(typeof(Guid)), nameof(GenericType<object>.NormalMethod)));
        return IL.Return<string>();
    }

    public string CallMethodInGenericTypeArray()
    {
        IL.Emit(OpCodes.Call, new MethodRef(typeof(GenericType<>).MakeGenericType(typeof(Guid[])), nameof(GenericType<object>.NormalMethod)));
        return IL.Return<string>();
    }

    public string CallMethodInGenericTypeGeneric<TClass>()
    {
        IL.Emit(OpCodes.Call, new MethodRef(typeof(GenericType<>).MakeGenericType(typeof(TClass)), nameof(GenericType<object>.NormalMethod)));
        return IL.Return<string>();
    }

    public string CallGenericMethodInGenericType()
    {
        IL.Emit(OpCodes.Call, new MethodRef(typeof(GenericType<>).MakeGenericType(typeof(Guid)), nameof(GenericType<object>.GenericMethod)).MakeGenericMethod(typeof(TimeSpan)));
        return IL.Return<string>();
    }

    public string CallGenericMethodInGenericTypeArray()
    {
        IL.Emit(OpCodes.Call, new MethodRef(typeof(GenericType<>).MakeGenericType(typeof(Guid[])), nameof(GenericType<object>.GenericMethod)).MakeGenericMethod(typeof(TimeSpan[])));
        return IL.Return<string>();
    }

    public string CallGenericMethodInGenericTypeGeneric<TClass, TMethod>()
    {
        IL.Emit(OpCodes.Call, new MethodRef(typeof(GenericType<>).MakeGenericType(typeof(TClass)), nameof(GenericType<object>.GenericMethod)).MakeGenericMethod(typeof(TMethod)));
        return IL.Return<string>();
    }

    public bool CallCoreLibMethod<T>(T? value)
        where T : struct
    {
        IL.Emit(OpCodes.Ldarga, 1);
        IL.Emit(OpCodes.Call, new MethodRef(typeof(T?), "get_HasValue"));
        return IL.Return<bool>();
    }

    public RuntimeMethodHandle ReturnMethodHandle()
    {
        Ldtoken(new MethodRef(typeof(Type), nameof(Type.GetTypeFromHandle)));
        return IL.Return<RuntimeMethodHandle>();
    }

#if NETFWK
    public int[] CallVarArgMethod()
    {
        IL.Push(5);
        IL.Push(1);
        IL.Push(2);
        IL.Push(3);
        IL.Emit(OpCodes.Call, new MethodRef(typeof(MethodRefTestCases), nameof(VarArgMethod)).WithOptionalParameters(typeof(int), typeof(int), typeof(int)));
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

#if NETFWK
    private static int[] VarArgMethod(int count, __arglist)
    {
        var it = new ArgIterator(__arglist);
        var result = new int[count];

        for (var i = 0; i < count; ++i)
            result[i] = it.GetRemainingCount() > 0 ? __refvalue(it.GetNextArg(), int) : 0;

        return result;
    }
#endif
}
