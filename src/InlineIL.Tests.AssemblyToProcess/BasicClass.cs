using System;
using System.Diagnostics.CodeAnalysis;
using System.Reflection.Emit;
using InlineIL;

[SuppressMessage("ReSharper", "UnusedMember.Global")]
[SuppressMessage("ReSharper", "UnusedParameter.Global")]
[SuppressMessage("ReSharper", "UnusedTypeParameter")]
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
        IL.Op(OpCodes.Ret);
        throw IL.Unreachable();
    }

    public RuntimeTypeHandle ReturnTypeHandle<T>()
    {
        IL.Op(OpCodes.Ldtoken, typeof(T));
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
        IL.Op(OpCodes.Ldtoken, new TypeReference(typeof(int)));
        IL.Op(OpCodes.Stelem, new TypeReference(typeof(RuntimeTypeHandle)));

        IL.Push(result);
        IL.Op(OpCodes.Ldc_I4_2);
        IL.Op(OpCodes.Ldtoken, new TypeReference("mscorlib", "System.Int32"));
        IL.Op(OpCodes.Stelem, new TypeReference("mscorlib", "System.RuntimeTypeHandle"));

        return result;
    }
}
