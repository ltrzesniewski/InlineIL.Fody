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
            _resolver = new ConstantTypeRefResolver(typeRef);
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

            var declaredTypeRef = TryFindDeclaredType(assembly, typeName);
            if (declaredTypeRef != null)
                return declaredTypeRef;

            var forwardedTypeRef = TryFindForwardedType(assembly, typeName);
            if (forwardedTypeRef != null)
                return forwardedTypeRef;

            throw new WeavingException($"Could not find type '{typeName}' in assembly '{assemblyName}'");
        }

        private static TypeReference TryFindDeclaredType(AssemblyDefinition assembly, string typeName)
            => assembly.Modules
                       .Select(m => m.GetType(typeName, false) ?? m.GetType(typeName, true))
                       .FirstOrDefault(t => t != null);

        private static TypeReference TryFindForwardedType(AssemblyDefinition assembly, string typeName)
        {
            foreach (var module in assembly.Modules)
            {
                foreach (var exportedType in module.ExportedTypes)
                {
                    // TODO find by runtime name
                    if (exportedType.FullName != typeName)
                        continue;

                    var typeRef = ToTypeRef(module, exportedType);
                    if (typeRef != null)
                        return typeRef;
                }
            }

            return null;

            TypeReference ToTypeRef(ModuleDefinition module, ExportedType exportedType)
            {
                var resolved = exportedType?.Resolve();
                if (resolved == null)
                    return null;

                return new TypeReference(exportedType.Namespace, exportedType.Name, module, module, resolved.IsValueType)
                {
                    DeclaringType = ToTypeRef(module, exportedType.DeclaringType)
                };
            }
        }

        public TypeReference Build()
        {
            var typeRef = _resolver.Resolve(_module);
            return AddModifiers(typeRef);
        }

        [CanBeNull]
        public TypeReference TryBuild(IGenericParameterProvider context)
        {
            var typeRef = _resolver.TryResolve(_module, context);
            return typeRef != null ? AddModifiers(typeRef) : null;
        }

        private TypeReference AddModifiers(TypeReference type)
        {
            foreach (var modifier in _optionalModifiers)
                type = type.MakeOptionalModifierType(modifier);

            foreach (var modifier in _requiredModifiers)
                type = type.MakeRequiredModifierType(modifier);

            return type;
        }

        public string GetDisplayName()
            => _resolver.GetDisplayName();

        public void MakePointerType()
            => _resolver = new PointerTypeRefResolver(_resolver);

        public void MakeByRefType()
            => _resolver = new ByRefTypeRefResolver(_resolver);

        public void MakeArrayType(int rank)
            => _resolver = new ArrayTypeRefResolver(_resolver, rank);

        public void MakeGenericType(TypeRefBuilder[] genericArgs)
            => _resolver = new GenericTypeRefResolver(_resolver, genericArgs);

        public void AddOptionalModifier(TypeReference modifierType)
            => _optionalModifiers.Add(modifierType);

        public void AddRequiredModifier(TypeReference modifierType)
            => _requiredModifiers.Add(modifierType);

        public override string ToString() => GetDisplayName();

        private abstract class TypeRefResolver
        {
            [NotNull]
            public abstract TypeReference Resolve(ModuleDefinition module);

            [CanBeNull]
            public abstract TypeReference TryResolve(ModuleDefinition module, [NotNull] IGenericParameterProvider context);

            public abstract string GetDisplayName();
        }

        private class ConstantTypeRefResolver : TypeRefResolver
        {
            private readonly TypeReference _typeRef;

            public ConstantTypeRefResolver(TypeReference typeRef)
            {
                if (typeRef.MetadataType == MetadataType.Class && !(typeRef is TypeDefinition))
                {
                    // TypeRefs from different assemblies get imported as MetadataType.Class
                    // since this information is not stored in the assembly metadata.
                    typeRef = typeRef.ResolveRequiredType();
                }

                _typeRef = typeRef;
            }

            public override TypeReference Resolve(ModuleDefinition module)
                => module.ImportReference(_typeRef);

            public override TypeReference TryResolve(ModuleDefinition module, IGenericParameterProvider context)
                => Resolve(module);

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

            public override TypeReference Resolve(ModuleDefinition module)
                => throw new WeavingException($"TypeRef.{(_type == GenericParameterType.Method ? "MethodGenericParameters" : "TypeGenericParameters")} can only be used in MethodRef definitions for overload resolution");

            public override TypeReference TryResolve(ModuleDefinition module, IGenericParameterProvider context)
            {
                switch (_type)
                {
                    case GenericParameterType.Type:
                    {
                        if (context.GenericParameterType != GenericParameterType.Type && context is MemberReference member)
                            context = member.DeclaringType;

                        if (!context.HasGenericParameters || context.GenericParameterType != GenericParameterType.Type)
                            return null;

                        if (_index >= context.GenericParameters.Count)
                            return null;

                        return module.ImportReference(context.GenericParameters[_index]);
                    }

                    case GenericParameterType.Method:
                    {
                        if (!context.HasGenericParameters || context.GenericParameterType != GenericParameterType.Method)
                            return null;

                        if (_index >= context.GenericParameters.Count)
                            return null;

                        return module.ImportReference(context.GenericParameters[_index]);
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

        private abstract class TypeSpecTypeRefResolver : TypeRefResolver
        {
            private readonly TypeRefResolver _baseResolver;

            protected TypeSpecTypeRefResolver(TypeRefResolver baseResolver)
                => _baseResolver = baseResolver;

            [NotNull]
            protected abstract TypeReference WrapTypeRef([NotNull] TypeReference typeRef);

            public sealed override TypeReference Resolve(ModuleDefinition module)
            {
                var typeRef = _baseResolver.Resolve(module);
                return ResolveImpl(module, typeRef);
            }

            public sealed override TypeReference TryResolve(ModuleDefinition module, IGenericParameterProvider context)
            {
                var typeRef = _baseResolver.TryResolve(module, context);
                return typeRef != null ? ResolveImpl(module, typeRef) : null;
            }

            private TypeReference ResolveImpl(ModuleDefinition module, TypeReference typeRef)
            {
                if (typeRef.MetadataType == MetadataType.TypedByReference)
                    throw new WeavingException($"Cannot create an array, pointer or ByRef to {nameof(TypedReference)}");

                typeRef = WrapTypeRef(typeRef);
                return module.ImportReference(typeRef);
            }

            public sealed override string GetDisplayName()
                => GetDisplayName(_baseResolver.GetDisplayName());

            protected abstract string GetDisplayName(string baseName);
        }

        private class ArrayTypeRefResolver : TypeSpecTypeRefResolver
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

        private class ByRefTypeRefResolver : TypeSpecTypeRefResolver
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

        private class PointerTypeRefResolver : TypeSpecTypeRefResolver
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

        private class GenericTypeRefResolver : TypeRefResolver
        {
            private readonly TypeRefResolver _baseResolver;
            private readonly TypeRefBuilder[] _genericArgs;

            public GenericTypeRefResolver(TypeRefResolver baseResolver, TypeRefBuilder[] genericArgs)
            {
                _baseResolver = baseResolver;
                _genericArgs = genericArgs;

                if (genericArgs.Length == 0)
                    throw new WeavingException("No generic arguments supplied");
            }

            public override TypeReference Resolve(ModuleDefinition module)
            {
                var typeRef = _baseResolver.Resolve(module);
                return ResolveImpl(module, null, typeRef);
            }

            public override TypeReference TryResolve(ModuleDefinition module, IGenericParameterProvider context)
            {
                var typeRef = _baseResolver.TryResolve(module, context);
                return typeRef != null ? ResolveImpl(module, context, typeRef) : null;
            }

            private TypeReference ResolveImpl(ModuleDefinition module, [CanBeNull] IGenericParameterProvider context, TypeReference typeRef)
            {
                if (typeRef.IsGenericInstance)
                    throw new WeavingException($"Type is already a generic instance: {typeRef.FullName}");

                if (typeRef.IsByReference || typeRef.IsPointer || typeRef.IsArray)
                    throw new WeavingException("Cannot make a generic instance of a ByRef, pointer or array type");

                if (!typeRef.HasGenericParameters)
                    throw new WeavingException($"Not a generic type definition: {typeRef.FullName}");

                if (typeRef.GenericParameters.Count != _genericArgs.Length)
                    throw new WeavingException($"Incorrect number of generic arguments supplied for type {typeRef.FullName} - expected {typeRef.GenericParameters.Count}, but got {_genericArgs.Length}");

                var argTypeRefs = new TypeReference[_genericArgs.Length];

                for (var i = 0; i < _genericArgs.Length; ++i)
                {
                    var argTypeBuilder = _genericArgs[i];
                    TypeReference argTypeRef;

                    if (context != null)
                    {
                        argTypeRef = argTypeBuilder.TryBuild(context);
                        if (argTypeRef == null)
                            return null;
                    }
                    else
                    {
                        argTypeRef = argTypeBuilder.Build();
                    }

                    argTypeRefs[i] = argTypeRef;
                }

                return module.ImportReference(typeRef.MakeGenericInstanceType(argTypeRefs));
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
