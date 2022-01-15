using Fody;
using Mono.Cecil;
using Mono.Cecil.Cil;
using Mono.Cecil.Rocks;

namespace InlineIL.Fody.Model;

internal class LocalVarBuilder
{
    private TypeReference _type;

    public string? Name { get; }

    public LocalVarBuilder(TypeReference typeRef)
    {
        _type = typeRef;
    }

    public LocalVarBuilder(TypeReference typeRef, string? name)
        : this(typeRef)
    {
        Name = name;
    }

    public VariableDefinition Build()
        => new(_type);

    public void MakePinned()
    {
        if (_type.IsPinned)
            throw new WeavingException($"Local '{Name ?? "(unnamed)"}' is already pinned");

        _type = _type.MakePinnedType();
    }

    public override string ToString() => $"{Name ?? "(unnamed)"} {_type.FullName}";
}
