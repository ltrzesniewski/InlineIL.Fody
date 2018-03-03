using System.Reflection.Emit;

namespace InlineIL.Tests.AssemblyToProcess
{
    public class BasicClass
    {
        public static void Nop()
        {
            IL.Op(OpCodes.Nop);
        }
    }
}
