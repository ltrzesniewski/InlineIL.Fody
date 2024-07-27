using System;
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

    public void ReadPointerFieldFromReferencedAssemblyType()
    {
        IL.Push(new TypeFromReferencedAssembly());
        Ldfld(FieldRef.Field(typeof(TypeFromReferencedAssembly), nameof(TypeFromReferencedAssembly.PointerField)));
        Pop();
    }

    public void ReadGenericPointerFieldFromReferencedAssemblyType()
    {
        IL.Push(new TypeFromReferencedAssembly<OtherStructFromReferencedAssembly>());
        Ldfld(FieldRef.Field(typeof(TypeFromReferencedAssembly<OtherStructFromReferencedAssembly>), nameof(TypeFromReferencedAssembly<OtherStructFromReferencedAssembly>.PointerField)));
        Pop();

        IL.Push(new TypeFromReferencedAssembly<DateTime>());
        Ldfld(FieldRef.Field(typeof(TypeFromReferencedAssembly<DateTime>), nameof(TypeFromReferencedAssembly<DateTime>.PointerField)));
        Pop();

        IL.Push(new TypeFromReferencedAssembly<InternalStruct>());
        Ldfld(FieldRef.Field(typeof(TypeFromReferencedAssembly<InternalStruct>), nameof(TypeFromReferencedAssembly<InternalStruct>.PointerField)));
        Pop();
    }
}

internal struct InternalStruct
{
}
