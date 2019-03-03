namespace InlineIL.Tests.Common
{
    public unsafe interface IReturnPointer<T>
        where T : unmanaged
    {
        T* ReturnPointer(T[] values, int offset);
    }
}
