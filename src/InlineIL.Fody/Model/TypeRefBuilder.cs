using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
        private TypeRefResolver _resolver;

        public TypeRefBuilder(ModuleDefinition module, TypeReference typeRef)
        {
            _module = module;
            _resolver = new TypeDefinitionResolver(typeRef);
        }

        public TypeRefBuilder(ModuleDefinition module, string assemblyName, string typeName)
            : this(module, FindType(module, assemblyName, typeName))
        {
        }

        public TypeRefBuilder(ModuleDefinition module, GenericParameterType genericParameterType, int genericParameterIndex)
        {
            _module = module;
            _resolver = new GenericParameterTypeRefResolver(genericParameterType, genericParameterIndex);
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
            var type = _resolver.TryResolve(_module, context);
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

        public string GetDisplayName()
            => _resolver.GetDisplayName();

        public void MakePointerType()
            => _resolver = new PointerTypeRefResolver(_resolver);

        public void MakeByRefType()
            => _resolver = new ByRefTypeRefResolver(_resolver);

        public void MakeArrayType(int rank)
            => _resolver = new ArrayTypeRefResolver(_resolver, rank);

        public void MakeGenericType(TypeRefBuilder[] genericArgs)
            => _resolver = new GenericTyperRefResolver(_resolver, genericArgs);

        public void AddOptionalModifier(TypeReference modifierType)
            => _optionalModifiers.Add(modifierType);

        public void AddRequiredModifier(TypeReference modifierType)
            => _requiredModifiers.Add(modifierType);

        public override string ToString() => GetDisplayName();

        private abstract class TypeRefResolver
        {
            [CanBeNull]
            public abstract TypeReference TryResolve(ModuleDefinition module, [CanBeNull] IGenericParameterProvider context);

            public abstract string GetDisplayName();
        }

        private class TypeDefinitionResolver : TypeRefResolver
        {
            private readonly TypeReference _typeRef;

            public TypeDefinitionResolver(TypeReference typeRef)
            {
                if (typeRef.MetadataType == MetadataType.Class && !(typeRef is TypeDefinition))
                {
                    // TypeRefs from different assemblies get imported as MetadataType.Class
                    // since this information is not stored in the assembly metadata.
                    typeRef = typeRef.ResolveRequiredType();
                }

                _typeRef = typeRef;
            }

            public override TypeReference TryResolve(ModuleDefinition module, IGenericParameterProvider context)
                => module.ImportReference(_typeRef);

            public override string GetDisplayName()
                => _typeRef.FullName;
        }

        private class GenericParameterTypeRefResolver : TypeRefResolver
        {
            private readonly GenericParameterType _type;
            private readonly int _index;

            public GenericParameterTypeRefResolver(GenericParameterType type, int index)
            {
                _type = type;
                _index = index;

                if (index < 0)
                    throw new WeavingException($"Invalid generic parameter index: {index}");
            }

            public override TypeReference TryResolve(ModuleDefinition module, IGenericParameterProvider context)
            {
                if (context == null)
                    return null;

                switch (_type)
                {
                    case GenericParameterType.Type:
                    {
                        if (context is MethodReference method)
                            context = method.DeclaringType;

                        if (!context.HasGenericParameters)
                            return null;

                        if (_index >= context.GenericParameters.Count)
                            return null;

                        return context.GenericParameters[_index];
                    }

                    case GenericParameterType.Method:
                    {
                        if (!context.HasGenericParameters || context.GenericParameterType != GenericParameterType.Method)
                            return null;

                        if (_index >= context.GenericParameters.Count)
                            return null;

                        return context.GenericParameters[_index];
                    }

                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }

            public override string GetDisplayName()
            {
                switch (_type)
                {
                    case GenericParameterType.Type:
                        return "!" + _index;

                    case GenericParameterType.Method:
                        return "!!" + _index;

                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
        }

        private abstract class WrappedTypeTypeRefResolver : TypeRefResolver
        {
            private readonly TypeRefResolver _baseResolver;

            protected WrappedTypeTypeRefResolver(TypeRefResolver baseResolver)
                => _baseResolver = baseResolver;

            protected abstract TypeReference WrapTypeRef(TypeReference typeRef);

            public sealed override TypeReference TryResolve(ModuleDefinition module, IGenericParameterProvider context)
            {
                var typeRef = _baseResolver.TryResolve(module, context);
                if (typeRef == null)
                    return null;

                if (typeRef.MetadataType == MetadataType.TypedByReference)
                    throw new WeavingException($"Cannot create an array, pointer or ByRef to {nameof(TypedReference)}");

                typeRef = WrapTypeRef(typeRef);
                if (typeRef == null)
                    return null;

                return module.ImportReference(typeRef);
            }

            public sealed override string GetDisplayName()
                => GetDisplayName(_baseResolver.GetDisplayName());

            protected abstract string GetDisplayName(string baseName);
        }

        private class ArrayTypeRefResolver : WrappedTypeTypeRefResolver
        {
            private readonly int _rank;

            public ArrayTypeRefResolver(TypeRefResolver baseResolver, int rank)
                : base(baseResolver)
            {
                _rank = rank;

                if (rank < 1)
                    throw new WeavingException($"Invalid array rank: {rank}, must be at least 1");
            }

            protected override TypeReference WrapTypeRef(TypeReference typeRef)
            {
                if (typeRef.IsByReference)
                    throw new WeavingException("Cannot make an array of a ByRef type");

                return _rank == 1 ? typeRef.MakeArrayType() : typeRef.MakeArrayType(_rank);
            }

            protected override string GetDisplayName(string baseName)
            {
                if (_rank == 1)
                    return baseName + "[]";

                return baseName + "[" + new string(',', _rank - 1) + "]";
            }
        }

        private class ByRefTypeRefResolver : WrappedTypeTypeRefResolver
        {
            public ByRefTypeRefResolver(TypeRefResolver baseResolver)
                : base(baseResolver)
            {
            }

            protected override TypeReference WrapTypeRef(TypeReference typeRef)
            {
                if (typeRef.IsByReference)
                    throw new WeavingException("Type is already a ByRef type");

                return typeRef.MakeByReferenceType();
            }

            protected override string GetDisplayName(string baseName)
                => baseName + "&";
        }

        private class PointerTypeRefResolver : WrappedTypeTypeRefResolver
        {
            public PointerTypeRefResolver(TypeRefResolver baseResolver)
                : base(baseResolver)
            {
            }

            protected override TypeReference WrapTypeRef(TypeReference typeRef)
            {
                if (typeRef.IsByReference)
                    throw new WeavingException("Cannot make a pointer to a ByRef type");

                return typeRef.MakePointerType();
            }

            protected override string GetDisplayName(string baseName)
                => baseName + "*";
        }

        private class GenericTyperRefResolver : TypeRefResolver
        {
            private readonly TypeRefResolver _baseResolver;
            private readonly TypeRefBuilder[] _genericArgs;

            public GenericTyperRefResolver(TypeRefResolver baseResolver, TypeRefBuilder[] genericArgs)
            {
                _baseResolver = baseResolver;
                _genericArgs = genericArgs;

                if (genericArgs.Length == 0)
                    throw new WeavingException("No generic arguments supplied");
            }

            public override TypeReference TryResolve(ModuleDefinition module, IGenericParameterProvider context)
            {
                var typeRef = _baseResolver.TryResolve(module, context);
                if (typeRef == null)
                    return null;

                if (typeRef.IsGenericInstance)
                    throw new WeavingException($"Type is already a generic instance: {typeRef.FullName}");

                if (typeRef.IsByReference || typeRef.IsPointer || typeRef.IsArray)
                    throw new WeavingException("Cannot make a generic instance of a ByRef, pointer or array type");

                var typeDef = typeRef.ResolveRequiredType();

                if (!typeDef.HasGenericParameters)
                    throw new WeavingException($"Not a generic type definition: {typeDef.FullName}");

                if (typeDef.GenericParameters.Count != _genericArgs.Length)
                    throw new WeavingException($"Incorrect number of generic arguments supplied for type {typeDef.FullName} - expected {typeDef.GenericParameters.Count}, but got {_genericArgs.Length}");

                var argTypeRefs = new TypeReference[_genericArgs.Length];

                for (var i = 0; i < _genericArgs.Length; ++i)
                {
                    var argTypeBuilder = _genericArgs[i];
                    var argTypeRef = argTypeBuilder.TryBuild(context);
                    argTypeRefs[i] = argTypeRef ?? throw new WeavingException("Cannot instantiate a generic type with the supplied type parameter.");
                }

                return module.ImportReference(typeDef.MakeGenericInstanceType(argTypeRefs));
            }

            public override string GetDisplayName()
            {
                var sb = new StringBuilder();
                sb.Append(_baseResolver.GetDisplayName());
                sb.Append("<");

                for (var i = 0; i < _genericArgs.Length; ++i)
                {
                    if (i != 0)
                        sb.Append(",");

                    sb.Append(_genericArgs[i].GetDisplayName());
                }

                sb.Append(">");
                return sb.ToString();
            }
        }
    }
}
