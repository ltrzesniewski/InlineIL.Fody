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
        IL.Emit(OpCodes.Dup);
        IL.Emit(OpCodes.Dup);
        IL.Emit(OpCodes.Add);
        IL.Emit(OpCodes.Add);
        IL.Emit(OpCodes.Ret);
        throw IL.Unreachable();
    }

    public void AddAssign(ref int a, int b)
    {
        IL.Push(ref a);
        IL.Push(a);
        IL.Push(b);
        IL.Emit(OpCodes.Add);
        IL.Emit(OpCodes.Stind_I4);
    }

    public int TwoPlusTwo()
    {
        IL.Emit(OpCodes.Ldc_I4, 2);
        IL.Emit(OpCodes.Conv_I8);
        IL.Emit(OpCodes.Ldc_I8, 2L);
        IL.Emit(OpCodes.Add);
        IL.Emit(OpCodes.Conv_I4);
        IL.Emit(OpCodes.Ret);
        throw IL.Unreachable();
    }

    public double TwoPlusTwoFloat()
    {
        IL.Emit(OpCodes.Ldc_R4, 2.0f);
        IL.Emit(OpCodes.Ldc_R8, 2.0);
        IL.Emit(OpCodes.Add);
        IL.Emit(OpCodes.Ret);
        throw IL.Unreachable();
    }

    public int TwoPlusTwoByte()
    {
        IL.Emit(OpCodes.Ldc_I4_S, 2);
        IL.Emit(OpCodes.Ldc_I4_S, 2);
        IL.Emit(OpCodes.Add);
        IL.Emit(OpCodes.Ret);
        throw IL.Unreachable();
    }

    public string SayHi()
    {
        IL.Emit(OpCodes.Ldstr, "Hello!");
        IL.Emit(OpCodes.Ret);
        throw IL.Unreachable();
    }

    public int ReturnArg(int value)
    {
        IL.Emit(OpCodes.Ldarg, 1);
        return IL.Return<int>();
    }

    public RuntimeTypeHandle ReturnTypeHandle<T>()
    {
        IL.Emit(OpCodes.Ldtoken, typeof(T));
        IL.Emit(OpCodes.Ret);
        throw IL.Unreachable();
    }

    public Type ReturnType<T>()
    {
        IL.Emit(OpCodes.Ldtoken, typeof(T));
        IL.Emit(OpCodes.Call, new MethodRef(typeof(Type), nameof(Type.GetTypeFromHandle)));
        IL.Emit(OpCodes.Ret);
        throw IL.Unreachable();
    }

    [SuppressMessage("ReSharper", "RedundantCast")]
    public RuntimeTypeHandle[] LoadTypeDifferentWays()
    {
        var result = new RuntimeTypeHandle[3];

        IL.Push(result);
        IL.Emit(OpCodes.Ldc_I4_0);
        IL.Emit(OpCodes.Ldtoken, typeof(int));
        IL.Emit(OpCodes.Stelem, typeof(RuntimeTypeHandle));

        IL.Push(result);
        IL.Emit(OpCodes.Ldc_I4_1);
        IL.Emit(OpCodes.Ldtoken, new TypeRef(typeof(int)));
        IL.Emit(OpCodes.Stelem, new TypeRef(typeof(RuntimeTypeHandle)));

        IL.Push(result);
        IL.Emit(OpCodes.Ldc_I4_2);
        IL.Emit(OpCodes.Ldtoken, new TypeRef("mscorlib", "System.Int32"));
        IL.Emit(OpCodes.Stelem, new TypeRef("mscorlib", "System.RuntimeTypeHandle"));

        return result;
    }

    public RuntimeTypeHandle LoadPointerType()
    {
        IL.Emit(OpCodes.Ldtoken, new TypeRef(typeof(int)).ToPointer().ToPointer());
        IL.Emit(OpCodes.Ret);
        throw IL.Unreachable();
    }

    public RuntimeTypeHandle LoadReferenceType()
    {
        IL.Emit(OpCodes.Ldtoken, new TypeRef(typeof(int)).ToReference());
        IL.Emit(OpCodes.Ret);
        throw IL.Unreachable();
    }

    public RuntimeTypeHandle LoadArrayType()
    {
        IL.Emit(OpCodes.Ldtoken, new TypeRef(typeof(int)).ToArray().ToArray());
        IL.Emit(OpCodes.Ret);
        throw IL.Unreachable();
    }

    public Type ReturnNestedType()
    {
        IL.Emit(OpCodes.Ldtoken, new TypeRef("InlineIL.Tests.AssemblyToProcess", "BasicClass+NestedType"));
        IL.Emit(OpCodes.Call, new MethodRef(typeof(Type), nameof(Type.GetTypeFromHandle)));
        IL.Emit(OpCodes.Ret);
        throw IL.Unreachable();
    }

    public int[] ResolveOverloads()
    {
        var result = new int[7];

        IL.Push(result);
        IL.Emit(OpCodes.Ldc_I4_0);
        IL.Emit(OpCodes.Call, new MethodRef(typeof(BasicClass), nameof(OverloadedMethod), new TypeRef[0]));
        IL.Emit(OpCodes.Stelem_I4);

        IL.Push(result);
        IL.Emit(OpCodes.Ldc_I4_1);
        IL.Emit(OpCodes.Call, new MethodRef(typeof(BasicClass), nameof(OverloadedMethod), Array.Empty<TypeRef>()));
        IL.Emit(OpCodes.Stelem_I4);

        IL.Push(result);
        IL.Emit(OpCodes.Ldc_I4_2);
        IL.Emit(OpCodes.Ldc_I4, 42);
        IL.Emit(OpCodes.Call, new MethodRef(typeof(BasicClass), nameof(OverloadedMethod), typeof(int)));
        IL.Emit(OpCodes.Stelem_I4);

        IL.Push(result);
        IL.Emit(OpCodes.Ldc_I4_3);
        IL.Push(result);
        IL.Emit(OpCodes.Ldc_I4_0);
        IL.Emit(OpCodes.Ldelema, typeof(int));
        IL.Emit(OpCodes.Call, new MethodRef(typeof(BasicClass), nameof(OverloadedMethod), new TypeRef(typeof(int)).ToReference()));
        IL.Emit(OpCodes.Stelem_I4);

        IL.Push(result);
        IL.Emit(OpCodes.Ldc_I4_4);
        IL.Emit(OpCodes.Ldnull);
        IL.Emit(OpCodes.Call, new MethodRef(typeof(BasicClass), nameof(OverloadedMethod), typeof(int[])));
        IL.Emit(OpCodes.Stelem_I4);

        IL.Push(result);
        IL.Emit(OpCodes.Ldc_I4_5);
        IL.Emit(OpCodes.Ldc_R8, 42.0);
        IL.Push(result);
        IL.Emit(OpCodes.Ldc_I4_0);
        IL.Emit(OpCodes.Ldelema, typeof(int));
        IL.Emit(OpCodes.Call, new MethodRef(typeof(BasicClass), nameof(OverloadedMethod), typeof(double), new TypeRef(typeof(int)).ToReference()));
        IL.Emit(OpCodes.Stelem_I4);

        IL.Push(result);
        IL.Emit(OpCodes.Ldc_I4_6);
        IL.Emit(OpCodes.Ldc_R8, 42.0);
        IL.Emit(OpCodes.Ldc_I4, 42);
        IL.Emit(OpCodes.Call, new MethodRef(typeof(BasicClass), nameof(OverloadedMethod), typeof(double), typeof(int)));
        IL.Emit(OpCodes.Stelem_I4);

        IL.Push(result);
        return IL.Return<int[]>();
    }

    public int Branch(bool returnOne)
    {
        IL.Push(returnOne);
        IL.Emit(OpCodes.Brtrue, new LabelRef("one"));
        IL.Push(42);
        IL.Emit(OpCodes.Br, new LabelRef("end"));
        IL.Label("one");
        IL.Push(1);
        IL.Label("end");
        return IL.Return<int>();
    }

    public int JumpTable(uint value)
    {
        IL.Push(value);
        IL.Emit(OpCodes.Switch, new LabelRef("one"), new LabelRef("two"), new LabelRef("three"));

        IL.Push(42);
        IL.Emit(OpCodes.Br, new LabelRef("end"));

        IL.Label("one");
        IL.Push(1);
        IL.Emit(OpCodes.Br, new LabelRef("end"));

        IL.Label("two");
        IL.Push(2);
        IL.Emit(OpCodes.Br, new LabelRef("end"));

        IL.Label("three");
        IL.Push(3);

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
