using System.Reflection.Emit;

namespace InlineIL.Examples
{
    public static class Examples
    {
        public static void ZeroInit<T>(ref T value)
            where T : struct
        {
            IL.Push(ref value);
            IL.Push(0);
            IL.Emit(OpCodes.Sizeof, typeof(T));
            IL.Emit(OpCodes.Unaligned, 1);
            IL.Emit(OpCodes.Initblk);
        }
    }
}
