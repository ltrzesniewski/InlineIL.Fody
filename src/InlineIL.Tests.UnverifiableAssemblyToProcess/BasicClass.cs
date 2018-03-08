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
        IL.Emit(OpCodes.Ldc_I4_0);
        IL.Emit(OpCodes.Conv_U);
        IL.Emit(OpCodes.Call, new MethodRef(typeof(BasicClass), nameof(OverloadedMethod), new TypeRef(typeof(int*)).ToPointer()));
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

    private static int OverloadedMethod() => 10;
    private static int OverloadedMethod(int a) => 20;
    private static int OverloadedMethod(int** a) => 30;
    private static int OverloadedMethod(int[] a) => 40;
    private static int OverloadedMethod(double a, ref int b) => 50;
    private static int OverloadedMethod(double a, int b) => 60;
}
