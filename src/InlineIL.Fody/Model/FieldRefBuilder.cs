using System.Linq;
using Fody;
using InlineIL.Fody.Extensions;
using Mono.Cecil;

namespace InlineIL.Fody.Model
{
    internal class FieldRefBuilder
    {
        private readonly FieldReference _field;

        public FieldRefBuilder(TypeReference typeRef, string fieldName)
        {
            var typeDef = typeRef.ResolveRequiredType();
            var fields = typeDef.Fields.Where(f => f.Name == fieldName).ToList();

            switch (fields.Count)
            {
                case 0:
                    throw new WeavingException($"Field '{fieldName}' not found in type {typeDef.FullName}");

                case 1:
                    _field = fields.Single();
                    break;

                default:
                    // This should never happen
                    throw new WeavingException($"Ambiguous field '{fieldName}' in type {typeDef.FullName}");
            }
        }

        public FieldReference Build()
            => _field;
    }
}
