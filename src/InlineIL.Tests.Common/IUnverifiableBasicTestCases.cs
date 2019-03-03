namespace InlineIL.Tests.Common
{
    public unsafe interface IUnverifiableBasicTestCases
    {
        int PopPointerLocal(int* arg, int offset);
        int PopPointerArg(int* arg, int offset);
        int PopPointerStatic(int* arg, int offset);

        ref int ReturnRef(int[] values, int offset);
        int* ReturnPointer(int[] values, int offset);
        void* ReturnPointerWithConversion(int[] values, int offset);
    }
}
