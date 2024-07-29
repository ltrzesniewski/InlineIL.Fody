using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using InlineIL.Tests.Common;
using static InlineIL.IL.Emit;

namespace InlineIL.Tests.UnverifiableAssemblyToProcess;

[SuppressMessage("ReSharper", "UnusedType.Global")]
[SuppressMessage("ReSharper", "UnusedMember.Global")]
[SuppressMessage("ReSharper", "UnassignedField.Global")]
[SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
public unsafe class FieldRefTestCases
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
        Call(MethodRef.Method(typeof(FieldRefTestCases), nameof(ConsumePointer)));
    }

    public void ReadGenericPointerFieldFromReferencedAssemblyType()
    {
        IL.Push(new TypeFromReferencedAssembly<OtherStructFromReferencedAssembly>());
        Ldfld(FieldRef.Field(typeof(TypeFromReferencedAssembly<OtherStructFromReferencedAssembly>), nameof(TypeFromReferencedAssembly<OtherStructFromReferencedAssembly>.PointerField)));
        Call(MethodRef.Method(typeof(FieldRefTestCases), nameof(ConsumePointer)));

        IL.Push(new TypeFromReferencedAssembly<DateTime>());
        Ldfld(FieldRef.Field(typeof(TypeFromReferencedAssembly<DateTime>), nameof(TypeFromReferencedAssembly<DateTime>.PointerField)));
        Call(MethodRef.Method(typeof(FieldRefTestCases), nameof(ConsumePointer)));

        IL.Push(new TypeFromReferencedAssembly<InternalStruct>());
        Ldfld(FieldRef.Field(typeof(TypeFromReferencedAssembly<InternalStruct>), nameof(TypeFromReferencedAssembly<InternalStruct>.PointerField)));
        Call(MethodRef.Method(typeof(FieldRefTestCases), nameof(ConsumePointer)));
    }

    public void ReadGenericTypeFieldFromReferencedAssemblyType()
    {
        IL.Push(new TypeFromReferencedAssembly());
        Ldfld(FieldRef.Field(typeof(TypeFromReferencedAssembly), nameof(TypeFromReferencedAssembly.GenericTypeField)));
        Ldfld(FieldRef.Field(typeof(TypeFromReferencedAssembly<StructFromReferencedAssembly>), nameof(TypeFromReferencedAssembly<StructFromReferencedAssembly>.PointerField)));
        Call(MethodRef.Method(typeof(FieldRefTestCases), nameof(ConsumePointer)));
    }

    public void ReadGenericTypeFieldFromReferencedAssemblyType_2()
    {
        IL.Push(new TypeFromReferencedAssembly());
        Ldfld(FieldRef.Field(typeof(TypeFromReferencedAssembly), nameof(TypeFromReferencedAssembly.GenericTypeField)));
        Ldfld(FieldRef.Field(typeof(TypeFromReferencedAssembly<StructFromReferencedAssembly>), nameof(TypeFromReferencedAssembly<StructFromReferencedAssembly>.FieldOfT)));
        Call(MethodRef.Method(typeof(FieldRefTestCases), nameof(Consume)).MakeGenericMethod(typeof(StructFromReferencedAssembly)));
    }

    [MethodImpl(MethodImplOptions.NoInlining)]
    [SuppressMessage("ReSharper", "UnusedParameter.Local")]
    private static void Consume<T>(T value)
    {
        // This could probably have been replaced by a pop, but having a noinline method
        // makes sure the JIT won't be able to optimize away reading the unused field value
    }

    [MethodImpl(MethodImplOptions.NoInlining)]
    [SuppressMessage("ReSharper", "UnusedParameter.Local")]
    private static void ConsumePointer(void* value)
    {
        // Same as above
    }
}

internal struct InternalStruct
{
}
