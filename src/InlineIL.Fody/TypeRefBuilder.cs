using System.Collections.Generic;
using System.Linq;
using Fody;
using Mono.Cecil;
using Mono.Cecil.Rocks;

namespace InlineIL.Fody
{
    internal class TypeRefBuilder
    {
        private readonly ModuleDefinition _module;
        private readonly List<TypeReference> _optionalModifiers = new List<TypeReference>();
        private readonly List<TypeReference> _requiredModifiers = new List<TypeReference>();
        private TypeReference _typeRef;

        public TypeRefBuilder(ModuleDefinition module, TypeReference typeRef)
        {
            _module = module;

            if (typeRef.MetadataType == MetadataType.Class && !(typeRef is TypeDefinition))
            {
                // TypeRefs from different assemblies get imported as MetadataType.Class
                // since this information is not stored in the assembly metadata.
                typeRef = typeRef.ResolveRequiredType();
            }

            _typeRef = _module.ImportReference(typeRef);
        }

        public TypeRefBuilder(ModuleDefinition module, string assemblyName, string typeName)
            : this(module, FindType(module, assemblyName, typeName))
        {
        }

        private static TypeReference FindType(ModuleDefinition module, string assemblyName, string typeName)
        {
            var assembly = assemblyName == module.Assembly.Name.Name
                ? module.Assembly
                : module.AssemblyResolver.Resolve(new AssemblyNameReference(assemblyName, null));

            if (assembly == null)
                throw new WeavingException($"Could not resolve assembly '{assemblyName}'");

            var typeRef = assembly.Modules
                                  .Select(m => m.GetType(typeName, false) ?? m.GetType(typeName, true))
                                  .FirstOrDefault(t => t != null);

            if (typeRef == null)
                throw new WeavingException($"Could not find type '{typeName}' in assembly '{assemblyName}'");

            return typeRef;
        }

        public TypeReference Build()
        {
            var type = _typeRef;

            foreach (var modifier in _optionalModifiers)
                type = type.MakeOptionalModifierType(modifier);

            foreach (var modifier in _requiredModifiers)
                type = type.MakeRequiredModifierType(modifier);

            return type;
        }

        public void MakePointerType()
        {
            _typeRef = _module.ImportReference(_typeRef.MakePointerType());
        }

        public void MakeByRefType()
        {
            _typeRef = _module.ImportReference(_typeRef.MakeByReferenceType());
        }

        public void MakeArrayType()
        {
            _typeRef = _module.ImportReference(_typeRef.MakeArrayType());
        }

        public void MakeArrayType(int rank)
        {
            if (rank < 1)
                throw new WeavingException($"Invalid array rank: {rank}, must be at least 1");

            _typeRef = _module.ImportReference(_typeRef.MakeArrayType(rank));
        }

        public void MakeGenericType(TypeReference[] genericArgs)
        {
            var typeDef = _typeRef.ResolveRequiredType();

            if (!typeDef.HasGenericParameters)
                throw new WeavingException($"Not a generic type: {typeDef.FullName}");

            if (genericArgs.Length == 0)
                throw new WeavingException("No generic arguments supplied");

            if (typeDef.GenericParameters.Count != genericArgs.Length)
                throw new WeavingException($"Incorrect number of generic arguments supplied for type {typeDef.FullName} - expected {typeDef.GenericParameters.Count}, but got {genericArgs.Length}");

            _typeRef = _module.ImportReference(typeDef.MakeGenericInstanceType(genericArgs));
        }

        public void AddOptionalModifier(TypeReference modifierType)
        {
            _optionalModifiers.Add(modifierType);
        }

        public void AddRequiredModifier(TypeReference modifierType)
        {
            _requiredModifiers.Add(modifierType);
        }

        public override string ToString() => _typeRef.ToString();
    }
}
