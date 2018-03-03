using System.Linq;
using Mono.Cecil;
using Mono.Cecil.Cil;

namespace InlineIL.Fody
{
    internal class MethodWeaver
    {
        private readonly KnownReferences _refs;

        public MethodWeaver(KnownReferences refs)
        {
            _refs = refs;
        }

        public void ProcessMethod(MethodDefinition method)
        {
            if (NeedsProcessing(method))
                new MethodContext(method, _refs).Process();
        }

        private bool NeedsProcessing(MethodDefinition method)
        {
            return method.Body
                         .Instructions
                         .Where(i => i.OpCode == OpCodes.Call)
                         .Select(i => i.Operand)
                         .OfType<MethodReference>()
                         .Any(m => m.DeclaringType.FullName == _refs.IlType.FullName);
        }
    }
}
