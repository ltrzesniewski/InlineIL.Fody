using System.Linq;
using Fody;
using InlineIL.Fody.Extensions;
using Mono.Cecil;

namespace InlineIL.Fody.Model;

internal class FieldRefBuilder
{
    private readonly FieldReference _field;

    public FieldRefBuilder(TypeReference typeRef, string fieldName)
    {
        var typeDef = typeRef.ResolveRequiredType();
        var fields = typeDef.Fields.Where(f => f.Name == fieldName).ToList();

        _field = fields switch
        {
            [ var field ] => field.Clone(),
            [ ]           => throw new WeavingException($"Field '{fieldName}' not found in type {typeDef.FullName}"),
            _             => throw new WeavingException($"Ambiguous field '{fieldName}' in type {typeDef.FullName}")
        };

        _field.DeclaringType = typeRef;
    }

    public FieldReference Build()
        => _field;
}
