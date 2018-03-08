using System;
using System.Diagnostics.CodeAnalysis;
using System.Reflection.Emit;
using InlineIL;

[SuppressMessage("ReSharper", "UnusedMember.Global")]
[SuppressMessage("ReSharper", "UnusedParameter.Global")]
[SuppressMessage("ReSharper", "UnusedTypeParameter")]
[SuppressMessage("ReSharper", "UnusedParameter.Local")]
public class BasicClass
{
    public int MultiplyBy3(int value)
    {
        IL.Push(value);
        IL.Op(OpCodes.Dup);
        IL.Op(OpCodes.Dup);
        IL.Op(OpCodes.Add);
        IL.Op(OpCodes.Add);
        IL.Op(OpCodes.Ret);
        throw IL.Unreachable();
    }

    public void AddAssign(ref int a, int b)
    {
        IL.Push(ref a);
        IL.Push(a);
        IL.Push(b);
        IL.Op(OpCodes.Add);
        IL.Op(OpCodes.Stind_I4);
    }

    public int TwoPlusTwo()
    {
        IL.Op(OpCodes.Ldc_I4, 2);
        IL.Op(OpCodes.Conv_I8);
        IL.Op(OpCodes.Ldc_I8, 2L);
        IL.Op(OpCodes.Add);
        IL.Op(OpCodes.Conv_I4);
        IL.Op(OpCodes.Ret);
        throw IL.Unreachable();
    }

    public double TwoPlusTwoFloat()
    {
        IL.Op(OpCodes.Ldc_R4, 2.0f);
        IL.Op(OpCodes.Ldc_R8, 2.0);
        IL.Op(OpCodes.Add);
        IL.Op(OpCodes.Ret);
        throw IL.Unreachable();
    }

    public int TwoPlusTwoByte()
    {
        IL.Op(OpCodes.Ldc_I4_S, 2);
        IL.Op(OpCodes.Ldc_I4_S, 2);
        IL.Op(OpCodes.Add);
        IL.Op(OpCodes.Ret);
        throw IL.Unreachable();
    }

    public string SayHi()
    {
        IL.Op(OpCodes.Ldstr, "Hello!");
        IL.Op(OpCodes.Ret);
        throw IL.Unreachable();
    }

    public int ReturnArg(int value)
    {
        IL.Op(OpCodes.Ldarg, 1);
        return IL.Return<int>();
    }

    public RuntimeTypeHandle ReturnTypeHandle<T>()
    {
        IL.Op(OpCodes.Ldtoken, typeof(T));
        IL.Op(OpCodes.Ret);
        throw IL.Unreachable();
    }

    public Type ReturnType<T>()
    {
        IL.Op(OpCodes.Ldtoken, typeof(T));
        IL.Op(OpCodes.Call, new MethodRef(typeof(Type), nameof(Type.GetTypeFromHandle)));
        IL.Op(OpCodes.Ret);
        throw IL.Unreachable();
    }

    [SuppressMessage("ReSharper", "RedundantCast")]
    public RuntimeTypeHandle[] LoadTypeDifferentWays()
    {
        var result = new RuntimeTypeHandle[3];

        IL.Push(result);
        IL.Op(OpCodes.Ldc_I4_0);
        IL.Op(OpCodes.Ldtoken, typeof(int));
        IL.Op(OpCodes.Stelem, typeof(RuntimeTypeHandle));

        IL.Push(result);
        IL.Op(OpCodes.Ldc_I4_1);
        IL.Op(OpCodes.Ldtoken, new TypeRef(typeof(int)));
        IL.Op(OpCodes.Stelem, new TypeRef(typeof(RuntimeTypeHandle)));

        IL.Push(result);
        IL.Op(OpCodes.Ldc_I4_2);
        IL.Op(OpCodes.Ldtoken, new TypeRef("mscorlib", "System.Int32"));
        IL.Op(OpCodes.Stelem, new TypeRef("mscorlib", "System.RuntimeTypeHandle"));

        return result;
    }

    public RuntimeTypeHandle LoadPointerType()
    {
        IL.Op(OpCodes.Ldtoken, new TypeRef(typeof(int)).ToPointer().ToPointer());
        IL.Op(OpCodes.Ret);
        throw IL.Unreachable();
    }

    public RuntimeTypeHandle LoadReferenceType()
    {
        IL.Op(OpCodes.Ldtoken, new TypeRef(typeof(int)).ToReference());
        IL.Op(OpCodes.Ret);
        throw IL.Unreachable();
    }

    public RuntimeTypeHandle LoadArrayType()
    {
        IL.Op(OpCodes.Ldtoken, new TypeRef(typeof(int)).ToArray().ToArray());
        IL.Op(OpCodes.Ret);
        throw IL.Unreachable();
    }

    public Type ReturnNestedType()
    {
        IL.Op(OpCodes.Ldtoken, new TypeRef("InlineIL.Tests.AssemblyToProcess", "BasicClass+NestedType"));
        IL.Op(OpCodes.Call, new MethodRef(typeof(Type), nameof(Type.GetTypeFromHandle)));
        IL.Op(OpCodes.Ret);
        throw IL.Unreachable();
    }

    public int[] ResolveOverloads()
    {
        var result = new int[7];

        IL.Push(result);
        IL.Op(OpCodes.Ldc_I4_0);
        IL.Op(OpCodes.Call, new MethodRef(typeof(BasicClass), nameof(OverloadedMethod), new TypeRef[0]));
        IL.Op(OpCodes.Stelem_I4);

        IL.Push(result);
        IL.Op(OpCodes.Ldc_I4_1);
        IL.Op(OpCodes.Call, new MethodRef(typeof(BasicClass), nameof(OverloadedMethod), Array.Empty<TypeRef>()));
        IL.Op(OpCodes.Stelem_I4);

        IL.Push(result);
        IL.Op(OpCodes.Ldc_I4_2);
        IL.Op(OpCodes.Ldc_I4, 42);
        IL.Op(OpCodes.Call, new MethodRef(typeof(BasicClass), nameof(OverloadedMethod), typeof(int)));
        IL.Op(OpCodes.Stelem_I4);

        IL.Push(result);
        IL.Op(OpCodes.Ldc_I4_3);
        IL.Push(result);
        IL.Op(OpCodes.Ldc_I4_0);
        IL.Op(OpCodes.Ldelema, typeof(int));
        IL.Op(OpCodes.Call, new MethodRef(typeof(BasicClass), nameof(OverloadedMethod), new TypeRef(typeof(int)).ToReference()));
        IL.Op(OpCodes.Stelem_I4);

        IL.Push(result);
        IL.Op(OpCodes.Ldc_I4_4);
        IL.Op(OpCodes.Ldnull);
        IL.Op(OpCodes.Call, new MethodRef(typeof(BasicClass), nameof(OverloadedMethod), typeof(int[])));
        IL.Op(OpCodes.Stelem_I4);

        IL.Push(result);
        IL.Op(OpCodes.Ldc_I4_5);
        IL.Op(OpCodes.Ldc_R8, 42.0);
        IL.Push(result);
        IL.Op(OpCodes.Ldc_I4_0);
        IL.Op(OpCodes.Ldelema, typeof(int));
        IL.Op(OpCodes.Call, new MethodRef(typeof(BasicClass), nameof(OverloadedMethod), typeof(double), new TypeRef(typeof(int)).ToReference()));
        IL.Op(OpCodes.Stelem_I4);

        IL.Push(result);
        IL.Op(OpCodes.Ldc_I4_6);
        IL.Op(OpCodes.Ldc_R8, 42.0);
        IL.Op(OpCodes.Ldc_I4, 42);
        IL.Op(OpCodes.Call, new MethodRef(typeof(BasicClass), nameof(OverloadedMethod), typeof(double), typeof(int)));
        IL.Op(OpCodes.Stelem_I4);

        IL.Push(result);
        return IL.Return<int[]>();
    }

    public int Branch(bool returnOne)
    {
        IL.Push(returnOne);
        IL.Op(OpCodes.Brtrue, new LabelRef("one"));
        IL.Push(42);
        IL.Op(OpCodes.Br, new LabelRef("end"));
        IL.Label("one");
        IL.Push(1);
        IL.Label("end");
        return IL.Return<int>();
    }

    private static int OverloadedMethod() => 10;
    private static int OverloadedMethod(int a) => 20;
    private static int OverloadedMethod(ref int a) => 30;
    private static int OverloadedMethod(int[] a) => 40;
    private static int OverloadedMethod(double a, ref int b) => 50;
    private static int OverloadedMethod(double a, int b) => 60;

    public class NestedType
    {
    }
}
