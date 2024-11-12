using System;
using System.Diagnostics.CodeAnalysis;
using InlineIL.Tests.Common;
using static InlineIL.IL.Emit;

namespace InlineIL.Tests.AssemblyToProcess;

[SuppressMessage("ReSharper", "UnusedType.Global")]
[SuppressMessage("ReSharper", "UnusedMember.Global")]
[SuppressMessage("ReSharper", "UnusedParameter.Global")]
[SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
[SuppressMessage("ReSharper", "EntityNameCapturedOnly.Global")]
public class BasicTestCases : IBasicTestCases
{
    public static int StaticIntField;

    public string PushValue()
    {
        IL.Push("Hello");
        return IL.Return<string>();
    }

    public string? PushNullValue()
    {
        IL.Push<string?>(null);
        return IL.Return<string?>();
    }

    public void PushRef(ref int i)
    {
        IL.Push(ref i);
        Ldc_I4(42);
        Stind_I4();
    }

    public void PushInRef(in int i)
    {
#if CSHARP_12_OR_GREATER
        IL.PushInRef(in i);
#else
        Ldarg_1(); // Not a real test below C# 12
#endif

        Ldc_I4(42);
        Stind_I4();
    }

    public void PushOutRef(out int i)
    {
        IL.PushOutRef(out i);
        Ldc_I4(42);
        Stind_I4();
    }

    public int MultiplyBy3(int value)
    {
        Ldarg(1);
        Dup();
        Dup();
        Add();
        Add();
        Ret();
        throw IL.Unreachable();
    }

    public void AddAssign(ref int a, int b)
    {
        Ldarg(nameof(a));
        Ldarg(nameof(a));
        Ldind_I4();
        Ldarg(nameof(b));
        Add();
        Stind_I4();
    }

    public int TwoPlusTwo()
    {
        Ldc_I4(2);
        Conv_I8();
        Ldc_I8(2L);
        Add();
        Conv_I4();
        Ret();
        throw IL.Unreachable();
    }

    public double TwoPlusTwoFloat()
    {
        Ldc_R4(2.0f);
        Ldc_R8(2.0);
        Add();
        Ret();
        throw IL.Unreachable();
    }

    public int TwoPlusTwoByte()
    {
        Ldc_I4_S(2);
        Ldc_I4_S(2);
        Add();
        Ret();
        throw IL.Unreachable();
    }

    public string SayHi()
    {
        Ldstr("Hello!");
        Ret();
        throw IL.Unreachable();
    }

    public int ReturnArg(int value)
    {
        Ldarg(1);
        return IL.Return<int>();
    }

    public int HandleExceptionBlocks()
    {
        Ldc_I4(1);
        Ldc_I4(2);
        Add();
        Pop();

        try
        {
            Ldc_I4(3);
            Ldc_I4(4);
            Add();
            IL.Push(new InvalidOperationException("foo"));
            Throw();
            throw IL.Unreachable();
        }
        catch (InvalidOperationException ex) when (ex.Message.Contains("foo"))
        {
            IL.Push(ex);
            Pop();
            Ldc_I4(5);
            Ldc_I4(6);
            Add();
            Pop();
        }
        finally
        {
            Ldc_I4(7);
            Ldc_I4(8);
            Add();
            Pop();
        }

        Ldc_I4(9);
        Ldc_I4(10);
        Add();
        return IL.Return<int>();
    }

    public float ReturnWithConversion1()
    {
        Ldc_I4(42);
        return IL.Return<int>();
    }

    public int? ReturnWithConversion2()
    {
        Ldc_I4(42);
        return IL.Return<int>();
    }

    public void ExplicitRet()
    {
        Ret();
    }

    public void ExplicitLeave()
    {
        try
        {
            Leave("target");
        }
        catch
        {
            Leave_S("target");
        }

        IL.MarkLabel("target");
    }

    public void ExplicitEndFinally()
    {
        try
        {
        }
        finally
        {
            Endfinally();
        }
    }

    public void NoLeaveAfterThrowOrRethrow()
    {
        try
        {
            IL.Push(new InvalidOperationException());
            Throw();
        }
        catch (InvalidOperationException)
        {
            Nop();
            throw;
        }
    }

    public int NestedClass()
        => NestedClassA.NestedClassB.Call();

    public int PopLocals()
    {
        Ldc_I4_2();
        Ldc_I4(10);
        Ldc_I4_4();
        IL.Pop(out int a);
        IL.Pop(out int b);
        IL.Pop(out int c);

        return a * b + c;
    }

    public int PopArgs(int arg)
    {
        IL.Push(arg);
        Dup();
        Add();
        IL.Pop(out arg);
        return arg;
    }

    public int PopStaticField(int arg)
    {
        IL.Push(arg);
        Dup();
        Add();
        IL.Pop(out StaticIntField);
        return StaticIntField;
    }

    public void LdargaS(object obj)
    {
        Ldarga_S(nameof(obj));
        Pop();
    }

    public sbyte LdcI4S()
    {
        Ldc_I4_S(-42);
        return IL.Return<sbyte>();
    }

    public void ShortenInstructions(int arg)
    {
        Ldarg_1();
        Pop();

        Ldarg(1);
        Pop();

        Ldarg(nameof(arg));
        Pop();

        Ldarg_S(1);
        Pop();

        Ldarg_S(nameof(arg));
        Pop();
    }

    public void HandleNop()
    {
        Ldnull();
        Nop();
        Ldnull();

        Pop();
        Pop();
    }

    public void EnsureLocal()
    {
        var guid = Guid.NewGuid();
        IL.EnsureLocal(guid);
    }

    public void ValidPushUsageWithEnsureLocal()
    {
        var guid = Guid.NewGuid();
        IL.EnsureLocal(guid);

        IL.Push(42);
        IL.Push(guid);
        Call(new MethodRef(typeof(BasicTestCases), nameof(ValidatePushUsageWithEnsureLocal)));
    }

#if NET9_0_OR_GREATER && CSHARP_13_OR_GREATER
    public int PushAndPopRefStruct()
    {
        var input = new RefStruct
        {
            Value = 42
        };

        IL.Push(input);
        input.Value = 0;

        IL.Pop(out RefStruct output);
        return output.Value;
    }
#endif

    private static void ValidatePushUsageWithEnsureLocal(int value, Guid guid)
    {
        if (value != 42 || guid == Guid.Empty)
            throw new InvalidOperationException("Invalid value");
    }

    private static class NestedClassA
    {
        public static class NestedClassB
        {
            public static int Call()
            {
                Ldc_I4_1();
                Ldc_I4_2();
                Add();
                return IL.Return<int>();
            }
        }
    }

    [SuppressMessage("ReSharper", "UnusedType.Local")]
    private ref struct RefStruct
    {
#pragma warning disable CS0649 // Field is never assigned to, and will always have its default value
        public int Value;
#pragma warning restore CS0649 // Field is never assigned to, and will always have its default value
    }
}
