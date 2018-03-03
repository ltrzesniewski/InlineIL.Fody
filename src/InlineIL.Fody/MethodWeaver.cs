using System.Linq;
using Mono.Cecil;
using Mono.Cecil.Cil;

namespace InlineIL.Fody
{
    internal class MethodWeaver
    {
        public void ProcessMethod(MethodDefinition method)
        {
            if (NeedsProcessing(method))
                new MethodContext(method).Process();
        }

        private static bool NeedsProcessing(MethodDefinition method)
        {
            return method.Body
                         .Instructions
                         .Where(i => i.OpCode == OpCodes.Call)
                         .Select(i => i.Operand)
                         .OfType<MethodReference>()
                         .Any(m => m.DeclaringType.FullName == MemberNames.IlType);
        }
    }
}
