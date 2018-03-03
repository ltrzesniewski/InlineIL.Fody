using System;
using System.Linq;
using Mono.Cecil;

namespace InlineIL.Fody
{
    internal class KnownReferences
    {
        public TypeDefinition IlType { get; }
        public MethodDefinition OpMethodNoArg { get; }

        public KnownReferences(Func<string, TypeDefinition> findType)
        {
            IlType = findType("InlineIL.IL");

            var methods = IlType.Methods.ToDictionary(m => m.FullName);
            OpMethodNoArg = methods["System.Void InlineIL.IL::Op(System.Reflection.Emit.OpCode)"];
        }
    }
}
