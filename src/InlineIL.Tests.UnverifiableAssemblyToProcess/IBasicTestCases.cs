public unsafe interface IBasicTestCases
{
    ref int ReturnRef(int[] values, int offset);
    int* ReturnPointer(int[] values, int offset);
    void* ReturnPointerWithConversion(int[] values, int offset);
}
