using System;
using System.Collections.Generic;
using System.Linq;
using Fody;
using InlineIL.Fody.Extensions;
using JetBrains.Annotations;
using Mono.Cecil;
using Mono.Cecil.Rocks;

namespace InlineIL.Fody.Model
{
    internal class TypeRefBuilder
    {
        private readonly ModuleDefinition _module;
        private readonly List<TypeReference> _optionalModifiers = new List<TypeReference>();
        private readonly List<TypeReference> _requiredModifiers = new List<TypeReference>();
        private Func<IGenericParameterProvider, TypeReference> _typeRefProvider;

        public TypeRefBuilder(ModuleDefinition module, TypeReference typeRef)
        {
            _module = module;

            if (typeRef.MetadataType == MetadataType.Class && !(typeRef is TypeDefinition))
            {
                // TypeRefs from different assemblies get imported as MetadataType.Class
                // since this information is not stored in the assembly metadata.
                typeRef = typeRef.ResolveRequiredType();
            }

            typeRef = _module.ImportReference(typeRef);
            _typeRefProvider = _ => typeRef;
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

        [CanBeNull]
        public TypeReference TryBuild([CanBeNull] IGenericParameterProvider context)
        {
            var type = _typeRefProvider(context);
            if (type == null)
                return null;

            foreach (var modifier in _optionalModifiers)
                type = type.MakeOptionalModifierType(modifier);

            foreach (var modifier in _requiredModifiers)
                type = type.MakeRequiredModifierType(modifier);

            return type;
        }

        public TypeReference Build()
            => TryBuild(null) ?? throw new WeavingException("Cannot construct type reference");

        public void MakePointerType()
        {
            WrapType(typeRef =>
            {
                EnsureCanWrapType(typeRef);

                if (typeRef.IsByReference)
                    throw new WeavingException("Cannot make a pointer to a ByRef type");

                return typeRef.MakePointerType();
            });
        }

        public void MakeByRefType()
        {
            WrapType(typeRef =>
            {
                EnsureCanWrapType(typeRef);

                if (typeRef.IsByReference)
                    throw new WeavingException("Type is already a ByRef type");

                return typeRef.MakeByReferenceType();
            });
        }

        public void MakeArrayType(int rank)
        {
            WrapType(typeRef =>
            {
                EnsureCanWrapType(typeRef);

                if (typeRef.IsByReference)
                    throw new WeavingException("Cannot make an array of a ByRef type");

                if (rank < 1)
                    throw new WeavingException($"Invalid array rank: {rank}, must be at least 1");

                return rank == 1 ? typeRef.MakeArrayType() : typeRef.MakeArrayType(rank);
            });
        }

        public void MakeGenericType(TypeReference[] genericArgs)
        {
            WrapType(typeRef =>
            {
                if (typeRef.IsGenericInstance)
                    throw new WeavingException($"Type is already a generic instance: {typeRef.FullName}");

                if (typeRef.IsByReference || typeRef.IsPointer || typeRef.IsArray)
                    throw new WeavingException("Cannot make a generic instance of a ByRef, pointer or array type");

                var typeDef = typeRef.ResolveRequiredType();

                if (!typeDef.HasGenericParameters)
                    throw new WeavingException($"Not a generic type definition: {typeDef.FullName}");

                if (genericArgs.Length == 0)
                    throw new WeavingException("No generic arguments supplied");

                if (typeDef.GenericParameters.Count != genericArgs.Length)
                    throw new WeavingException($"Incorrect number of generic arguments supplied for type {typeDef.FullName} - expected {typeDef.GenericParameters.Count}, but got {genericArgs.Length}");

                return typeDef.MakeGenericInstanceType(genericArgs);
            });
        }

        public void AddOptionalModifier(TypeReference modifierType)
        {
            _optionalModifiers.Add(modifierType);
        }

        public void AddRequiredModifier(TypeReference modifierType)
        {
            _requiredModifiers.Add(modifierType);
        }

        private void WrapType(Func<TypeReference, TypeReference> chainFunc)
        {
            var prevProvider = _typeRefProvider;
            _typeRefProvider = context =>
            {
                var typeRef = prevProvider(context);
                return typeRef != null ? _module.ImportReference(chainFunc(typeRef)) : null;
            };
        }

        private static void EnsureCanWrapType(TypeReference typeRef)
        {
            if (typeRef.MetadataType == MetadataType.TypedByReference)
                throw new WeavingException($"Cannot create an array, pointer or ByRef to {nameof(TypedReference)}");
        }
    }
}
