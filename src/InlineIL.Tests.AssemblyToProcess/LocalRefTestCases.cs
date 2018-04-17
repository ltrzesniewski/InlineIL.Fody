using System;
using System.Diagnostics.CodeAnalysis;
using InlineIL;
using static InlineIL.IL.Emit;

[SuppressMessage("ReSharper", "UnusedMember.Global")]
[SuppressMessage("ReSharper", "UnusedParameter.Global")]
public class LocalRefTestCases
{
    public int UseLocalVariables(int value)
    {
        IL.DeclareLocals(
            new LocalVar("foo", typeof(int)),
            new LocalVar("bar", typeof(int))
        );

        IL.Push(value);
        Stloc("foo");

        Ldc_I4(42);
        Stloc("bar");

        Ldloc("foo");
        Ldloc("bar");
        Add();

        return IL.Return<int>();
    }

    public int UseLocalVariablesAlt(int value)
    {
        IL.DeclareLocals(
            new LocalVar("foo", typeof(int)),
            new LocalVar("bar", typeof(int))
        );

        IL.Push(value);
        Stloc("foo");

        Ldc_I4(42);
        Stloc("bar");

        Ldloc("foo");
        Ldloc("bar");
        Add();

        return IL.Return<int>();
    }

    public int UseLocalVariablesExplicitInit(int value)
    {
        IL.DeclareLocals(
            true,
            new LocalVar("foo", typeof(int)),
            new LocalVar("bar", typeof(int))
        );

        IL.Push(value);
        Stloc("foo");

        Ldc_I4(42);
        Stloc("bar");

        Ldloc("foo");
        Ldloc("bar");
        Add();

        return IL.Return<int>();
    }

    public int MapLocalIndexes(int a, int b, int c, int d)
    {
        IL.DeclareLocals(
            new LocalVar(typeof(int)),
            new LocalVar(typeof(int)),
            typeof(int),
            typeof(int)
        );

        // Make sure the compiler declares some (double) locals on its own
        var dummyA = Math.Cos(Math.PI / 4.0);
        var dummyB = Math.Log(42);
        var dummyC = Math.Acos(dummyA);
        GC.KeepAlive(dummyA + dummyB + dummyC);

        IL.Push(a);
        Stloc_0();

        IL.Push(b);
        Stloc_1();

        IL.Push(c);
        Stloc_2();

        IL.Push(d);
        Stloc_3();

        Ldloc_0();
        Ldloc_1();
        Mul();

        Ldloc_2();
        Ldloc_3();
        Div();

        Add();

        return IL.Return<int>();
    }

    public int MapLocalIndexesLong(int a, int b)
    {
        IL.DeclareLocals(
            new LocalVar(typeof(int)),
            new LocalVar(typeof(int))
        );

        // Make sure the compiler declares some (double) locals on its own
        var dummyA = Math.Cos(Math.PI / 4.0);
        var dummyB = Math.Log(42);
        var dummyC = Math.Acos(dummyA);
        GC.KeepAlive(dummyA + dummyB + dummyC);

        IL.Push(a);
        Stloc(0);

        IL.Push(b);
        Stloc_S(1);

        Ldloc(0);
        Ldloc_S(1);
        Add();

        return IL.Return<int>();
    }

    public int MapLocalIndexesLongAlt(int a, int b)
    {
        IL.DeclareLocals(
            new LocalVar(typeof(int)),
            new LocalVar(typeof(int))
        );

        // Make sure the compiler declares some (double) locals on its own
        var dummyA = Math.Cos(Math.PI / 4.0);
        var dummyB = Math.Log(42);
        var dummyC = Math.Acos(dummyA);
        GC.KeepAlive(dummyA + dummyB + dummyC);

        IL.Push(a);
        Stloc(0);

        IL.Push(b);
        Stloc_S(1);

        Ldloc(0);
        Ldloc_S(1);
        Add();

        return IL.Return<int>();
    }
}
