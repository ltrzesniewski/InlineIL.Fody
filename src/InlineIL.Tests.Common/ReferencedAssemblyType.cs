namespace InlineIL.Tests.Common;

public unsafe class TypeFromReferencedAssembly
{
    public readonly StructFromReferencedAssembly FieldWithTypeFromThisAssembly = default;
    public readonly StructFromReferencedAssembly* PointerField = default;
    public readonly TypeFromReferencedAssembly<StructFromReferencedAssembly> GenericTypeField = new();
}

public unsafe class TypeFromReferencedAssembly<T>
    where T : unmanaged
{
    public readonly T FieldOfT = default;
    public readonly T* PointerField = default;
}

public struct StructFromReferencedAssembly;

public struct OtherStructFromReferencedAssembly;
