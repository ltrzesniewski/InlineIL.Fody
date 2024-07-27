using System.Diagnostics.CodeAnalysis;
using InlineIL.Tests.Common;
using static InlineIL.IL.Emit;

namespace InlineIL.Tests.UnverifiableAssemblyToProcess;

[SuppressMessage("ReSharper", "UnusedType.Global")]
[SuppressMessage("ReSharper", "UnusedMember.Global")]
[SuppressMessage("ReSharper", "UnassignedField.Global")]
[SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
public class FieldRefTestCases
{
    public void ReadFieldFromReferencedAssemblyType()
    {
        // Repro of issue #33

        IL.Push(new TypeFromReferencedAssembly());
        Ldfld(FieldRef.Field(typeof(TypeFromReferencedAssembly), nameof(TypeFromReferencedAssembly.FieldWithTypeFromThisAssembly)));
        Pop();
    }
}
