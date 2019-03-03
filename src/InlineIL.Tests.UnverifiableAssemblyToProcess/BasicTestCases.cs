using System;
using System.Diagnostics.CodeAnalysis;
using static InlineIL.IL.Emit;

namespace InlineIL.Tests.UnverifiableAssemblyToProcess
{
    [SuppressMessage("ReSharper", "UnusedMember.Global")]
    [SuppressMessage("ReSharper", "UnusedParameter.Global")]
    public class BasicTestCases : IBasicTestCases
    {
        public void HandlePrefixesInDebugMode(ref Guid value)
        {
            IL.Push(ref value);

            Ldc_I4_0();
            Conv_U1();

            Sizeof(typeof(Guid));

            Unaligned(1);
            Initblk();
        }

        public ref int ReturnRef(int[] values, int offset)
        {
            Ldarg(nameof(values));
            Ldarg(nameof(offset));
            Ldelema(typeof(int));
            return ref IL.ReturnRef<int>();
        }

        public unsafe int* ReturnPointer(int[] values, int offset)
        {
            Ldarg(nameof(values));
            Ldarg(nameof(offset));
            Ldelema(typeof(int));
            return IL.ReturnPointer<int>();
        }

        public int ReturnRefWithDereference(int[] values, int offset)
        {
            Ldarg(nameof(values));
            Ldarg(nameof(offset));
            Ldelema(typeof(int));
            return IL.ReturnRef<int>();
        }

        public double ReturnRefWithDereferenceAndConversion(int[] values, int offset)
        {
            Ldarg(nameof(values));
            Ldarg(nameof(offset));
            Ldelema(typeof(int));
            return IL.ReturnRef<int>();
        }

        public unsafe void* ReturnPointerWithConversion(int[] values, int offset)
        {
            Ldarg(nameof(values));
            Ldarg(nameof(offset));
            Ldelema(typeof(int));
            return IL.ReturnPointer<int>();
        }

        public unsafe int ReturnPointerWithDereference(int[] values, int offset)
        {
            Ldarg(nameof(values));
            Ldarg(nameof(offset));
            Ldelema(typeof(int));
            return *IL.ReturnPointer<int>();
        }
    }
}
