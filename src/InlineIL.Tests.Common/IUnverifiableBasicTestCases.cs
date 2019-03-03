namespace InlineIL.Tests.Common
{
    public unsafe interface IUnverifiableBasicTestCases
    {
        ref int ReturnRef(int[] values, int offset);
        int* ReturnPointer(int[] values, int offset);
        void* ReturnPointerWithConversion(int[] values, int offset);
    }
}
