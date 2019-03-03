namespace InlineIL.Tests.Common
{
    public interface IReturnRef<T>
    {
        ref T ReturnRef(T[] values, int offset);
    }
}
