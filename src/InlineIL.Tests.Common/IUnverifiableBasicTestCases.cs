using System.Diagnostics.CodeAnalysis;

namespace InlineIL.Tests.Common
{
    [SuppressMessage("ReSharper", "UnusedParameter.Global")]
    public unsafe interface IUnverifiableBasicTestCases
    {
        void PushPointer(int* value);

        int PopPointerLocal(int* arg, int offset);
        int PopPointerArg(int* arg, int offset);
        int PopPointerStatic(int* arg, int offset);
        int PopVoidPointerLocal(int* arg, int offset);
        int PopVoidPointerArg(void* arg, int offset);
        int PopVoidPointerStatic(int* arg, int offset);

        ref int ReturnRef(int[] values, int offset);
        int* ReturnPointer(int[] values, int offset);
        void* ReturnVoidPointer(int[] values, int offset);
        void* ReturnPointerWithConversion(int[] values, int offset);
    }
}
