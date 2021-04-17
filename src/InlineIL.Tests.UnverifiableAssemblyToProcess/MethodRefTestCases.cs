using System;
using System.Diagnostics.CodeAnalysis;
using static InlineIL.IL.Emit;

namespace InlineIL.Tests.UnverifiableAssemblyToProcess
{
    [SuppressMessage("ReSharper", "UnusedType.Global")]
    [SuppressMessage("ReSharper", "UnusedMember.Global")]
    [SuppressMessage("ReSharper", "UnusedParameter.Global")]
    [SuppressMessage("ReSharper", "UnusedParameter.Local")]
    public unsafe class MethodRefTestCases
    {
        public int[] ResolveOverloads()
        {
            var result = new int[7];

            IL.Push(result);
            Ldc_I4_0();
            Call(new MethodRef(typeof(MethodRefTestCases), nameof(OverloadedMethod), new TypeRef[0]));
            Stelem_I4();

            IL.Push(result);
            Ldc_I4_1();
            Call(new MethodRef(typeof(MethodRefTestCases), nameof(OverloadedMethod), Array.Empty<TypeRef>()));
            Stelem_I4();

            IL.Push(result);
            Ldc_I4_2();
            Ldc_I4(42);
            Call(new MethodRef(typeof(MethodRefTestCases), nameof(OverloadedMethod), typeof(int)));
            Stelem_I4();

            IL.Push(result);
            Ldc_I4_3();
            Ldc_I4_0();
            Conv_U();
            Call(new MethodRef(typeof(MethodRefTestCases), nameof(OverloadedMethod), new TypeRef(typeof(int*)).MakePointerType()));
            Stelem_I4();

            IL.Push(result);
            Ldc_I4_4();
            Ldnull();
            Call(new MethodRef(typeof(MethodRefTestCases), nameof(OverloadedMethod), typeof(int[])));
            Stelem_I4();

            IL.Push(result);
            Ldc_I4_5();
            Ldc_R8(42.0);
            IL.Push(result);
            Ldc_I4_0();
            Ldelema(typeof(int));
            Call(new MethodRef(typeof(MethodRefTestCases), nameof(OverloadedMethod), typeof(double), new TypeRef(typeof(int)).MakeByRefType()));
            Stelem_I4();

            IL.Push(result);
            Ldc_I4_6();
            Ldc_R8(42.0);
            Ldc_I4(42);
            Call(new MethodRef(typeof(MethodRefTestCases), nameof(OverloadedMethod), typeof(double), typeof(int)));
            Stelem_I4();

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
}
