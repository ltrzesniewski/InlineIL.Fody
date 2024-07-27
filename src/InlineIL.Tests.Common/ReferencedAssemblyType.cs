namespace InlineIL.Tests.Common;

public unsafe class TypeFromReferencedAssembly
{
    public readonly StructFromReferencedAssembly FieldWithTypeFromThisAssembly = default;
    public readonly StructFromReferencedAssembly* PointerField = default;
}

public unsafe class TypeFromReferencedAssembly<T>
    where T : unmanaged
{
    public readonly T* PointerField = default;
}

public struct StructFromReferencedAssembly;
public struct OtherStructFromReferencedAssembly;
