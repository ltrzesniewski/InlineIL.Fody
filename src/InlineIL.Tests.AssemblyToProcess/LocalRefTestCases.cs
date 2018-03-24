using System;
using System.Diagnostics.CodeAnalysis;
using System.Reflection.Emit;
using InlineIL;

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
        IL.Emit(OpCodes.Stloc, new LocalRef("foo"));

        IL.Emit(OpCodes.Ldc_I4, 42);
        IL.Emit(OpCodes.Stloc, new LocalRef("bar"));

        IL.Emit(OpCodes.Ldloc, new LocalRef("foo"));
        IL.Emit(OpCodes.Ldloc, new LocalRef("bar"));
        IL.Emit(OpCodes.Add);

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
        IL.Emit(OpCodes.Stloc, new LocalRef("foo"));

        IL.Emit(OpCodes.Ldc_I4, 42);
        IL.Emit(OpCodes.Stloc, new LocalRef("bar"));

        IL.Emit(OpCodes.Ldloc, new LocalRef("foo"));
        IL.Emit(OpCodes.Ldloc, new LocalRef("bar"));
        IL.Emit(OpCodes.Add);

        return IL.Return<int>();
    }

    public int MapLocalIndexes(int a, int b, int c, int d)
    {
        IL.DeclareLocals(
            new LocalVar("a", typeof(int)),
            new LocalVar("b", typeof(int)),
            new LocalVar("c", typeof(int)),
            new LocalVar("d", typeof(int))
        );

        // Make sure the compiler declares some (double) locals on its own
        var dummyA = Math.Cos(Math.PI / 4.0);
        var dummyB = Math.Log(42);
        var dummyC = Math.Acos(dummyA);
        GC.KeepAlive(dummyA + dummyB + dummyC);

        IL.Push(a);
        IL.Emit(OpCodes.Stloc_0);

        IL.Push(b);
        IL.Emit(OpCodes.Stloc_1);

        IL.Push(c);
        IL.Emit(OpCodes.Stloc_2);

        IL.Push(d);
        IL.Emit(OpCodes.Stloc_3);

        IL.Emit(OpCodes.Ldloc_0);
        IL.Emit(OpCodes.Ldloc_1);
        IL.Emit(OpCodes.Mul);

        IL.Emit(OpCodes.Ldloc_2);
        IL.Emit(OpCodes.Ldloc_3);
        IL.Emit(OpCodes.Div);

        IL.Emit(OpCodes.Add);

        return IL.Return<int>();
    }

    public int MapLocalIndexesLong(int a, int b)
    {
        IL.DeclareLocals(
            new LocalVar("a", typeof(int)),
            new LocalVar("b", typeof(int))
        );

        // Make sure the compiler declares some (double) locals on its own
        var dummyA = Math.Cos(Math.PI / 4.0);
        var dummyB = Math.Log(42);
        var dummyC = Math.Acos(dummyA);
        GC.KeepAlive(dummyA + dummyB + dummyC);

        IL.Push(a);
        IL.Emit(OpCodes.Stloc, 0);

        IL.Push(b);
        IL.Emit(OpCodes.Stloc_S, 1);

        IL.Emit(OpCodes.Ldloc, 0);
        IL.Emit(OpCodes.Ldloc_S, 1);
        IL.Emit(OpCodes.Add);

        return IL.Return<int>();
    }
}
