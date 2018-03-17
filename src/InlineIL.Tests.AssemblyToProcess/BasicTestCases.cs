using System.Diagnostics.CodeAnalysis;
using System.Reflection.Emit;
using InlineIL;

[SuppressMessage("ReSharper", "UnusedMember.Global")]
[SuppressMessage("ReSharper", "UnusedParameter.Global")]
public class BasicTestCases
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
}
