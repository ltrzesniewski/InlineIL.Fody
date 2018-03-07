using System;
using System.Diagnostics.CodeAnalysis;
using System.Reflection.Emit;
using InlineIL;

[SuppressMessage("ReSharper", "UnusedMember.Global")]
[SuppressMessage("ReSharper", "UnusedParameter.Global")]
[SuppressMessage("ReSharper", "UnusedTypeParameter")]
[SuppressMessage("ReSharper", "UnusedParameter.Local")]
public unsafe class BasicClass
{
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
        IL.Op(OpCodes.Ldc_I4_0);
        IL.Op(OpCodes.Conv_U);
        IL.Op(OpCodes.Call, new MethodRef(typeof(BasicClass), nameof(OverloadedMethod), new TypeRef(typeof(int*)).ToPointer()));
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

    private static int OverloadedMethod() => 10;
    private static int OverloadedMethod(int a) => 20;
    private static int OverloadedMethod(int** a) => 30;
    private static int OverloadedMethod(int[] a) => 40;
    private static int OverloadedMethod(double a, ref int b) => 50;
    private static int OverloadedMethod(double a, int b) => 60;
}
