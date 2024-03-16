using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Fody;
using InlineIL.Fody.Extensions;
using InlineIL.Fody.Processing;
using Mono.Cecil;
using Mono.Cecil.Rocks;

namespace InlineIL.Fody.Model;

internal class TypeRefBuilder
{
    private readonly ModuleWeavingContext _context;
    private List<Modifier>? _modifiers;
    private TypeRefResolver _resolver;

    public TypeRefBuilder(ModuleWeavingContext context, TypeReference typeRef)
    {
        _context = context;
        _resolver = new ConstantTypeRefResolver(context, typeRef);
    }

    public TypeRefBuilder(ModuleWeavingContext context, string assemblyName, string typeName)
        : this(context, FindType(context, assemblyName, typeName))
    {
    }

    public TypeRefBuilder(ModuleWeavingContext context, GenericParameterType genericParameterType, int genericParameterIndex)
    {
        _context = context;
        _resolver = new GenericParameterTypeRefResolver(genericParameterType, genericParameterIndex);
    }

    private static TypeReference FindType(ModuleWeavingContext context, string assemblyName, string typeName)
    {
        var assembly = assemblyName == context.Module.Assembly.Name.Name
            ? context.Module.Assembly
            : context.Module.AssemblyResolver.Resolve(new AssemblyNameReference(assemblyName, null))
              ?? context.InjectedAssemblyResolver.Resolve(new AssemblyNameReference(assemblyName, null));

        if (assembly == null)
            throw new WeavingException($"Could not resolve assembly '{assemblyName}'");

        var declaredTypeRef = TryFindDeclaredType(assembly, typeName);
        if (declaredTypeRef != null)
            return declaredTypeRef;

        var forwardedTypeRef = TryFindForwardedType(assembly, typeName, context.Module);
        if (forwardedTypeRef != null)
            return forwardedTypeRef;

        throw new WeavingException($"Could not find type '{typeName}' in assembly '{assemblyName}'");
    }

    private static TypeReference? TryFindDeclaredType(AssemblyDefinition assembly, string typeName)
        => assembly.Modules
                   .Select(m => m.GetType(typeName, false) ?? m.GetType(typeName, true))
                   .FirstOrDefault(t => t != null);

    private static TypeReference? TryFindForwardedType(AssemblyDefinition assembly, string typeName, ModuleDefinition targetModule)
    {
        var ecmaTypeName = Regex.Replace(typeName, @"\\.|\+", m => m.Length == 1 ? "/" : m.Value.Substring(1), RegexOptions.CultureInvariant);

        foreach (var module in assembly.Modules)
        {
            if (!module.HasExportedTypes)
                continue;

            foreach (var exportedType in module.ExportedTypes)
            {
                if (!exportedType.IsForwardedType())
                    continue;

                if (exportedType.FullName == typeName || exportedType.FullName == ecmaTypeName)
                    return exportedType.CreateReference(module, targetModule);
            }
        }

        return null;
    }

    public static TypeRefBuilder FromDll(ModuleWeavingContext context, string assemblyPath, string typeName)
    {
        if (context.ProjectDirectory is { } projectDirectory and not "")
            assemblyPath = Path.Combine(projectDirectory, assemblyPath);

        var assembly = context.InjectedAssemblyResolver.ResolveAssemblyByPath(assemblyPath);

        var declaredTypeRef = TryFindDeclaredType(assembly, typeName);
        if (declaredTypeRef != null)
            return new TypeRefBuilder(context, declaredTypeRef);

        var forwardedTypeRef = TryFindForwardedType(assembly, typeName, context.Module);
        if (forwardedTypeRef != null)
            return new TypeRefBuilder(context, forwardedTypeRef);

        throw new WeavingException($"Could not find type '{typeName}' in assembly: {assemblyPath}");
    }

    public TypeReference Build()
    {
        var typeRef = _resolver.Resolve(_context);
        return AddModifiers(typeRef);
    }

    public TypeReference? TryBuild(IGenericParameterProvider genericParameterProvider)
    {
        var typeRef = _resolver.TryResolve(_context, genericParameterProvider);
        return typeRef != null ? AddModifiers(typeRef) : null;
    }

    private TypeReference AddModifiers(TypeReference type)
    {
        if (_modifiers != null)
        {
            foreach (var modifier in _modifiers)
            {
                if (modifier.IsRequired)
                    type = type.MakeRequiredModifierType(modifier.Type);
                else
                    type = type.MakeOptionalModifierType(modifier.Type);
            }
        }

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
        => AddModifier(modifierType, false);

    public void AddRequiredModifier(TypeReference modifierType)
        => AddModifier(modifierType, true);

    private void AddModifier(TypeReference modifierType, bool required)
    {
        _modifiers ??= new List<Modifier>();
        _modifiers.Add(new Modifier(modifierType, required));
    }

    public override string ToString()
        => GetDisplayName();

    private abstract class TypeRefResolver
    {
        public abstract TypeReference Resolve(ModuleWeavingContext context);

        public abstract TypeReference? TryResolve(ModuleWeavingContext context, IGenericParameterProvider genericParameterProvider);

        public abstract string GetDisplayName();
    }

    private class ConstantTypeRefResolver : TypeRefResolver
    {
        private readonly TypeReference _typeRef;

        public ConstantTypeRefResolver(ModuleWeavingContext context, TypeReference typeRef)
        {
            if (typeRef.MetadataType == MetadataType.Class && typeRef is not TypeDefinition)
            {
                // TypeRefs from different assemblies get imported as MetadataType.Class
                // since this information is not stored in the assembly metadata.
                var typeDef = typeRef.ResolveRequiredType(context);
                typeRef = typeRef.Clone();
                typeRef.IsValueType = typeDef.IsValueType;
            }

            _typeRef = typeRef;
        }

        public override TypeReference Resolve(ModuleWeavingContext context)
            => context.Module.ImportReference(_typeRef);

        public override TypeReference TryResolve(ModuleWeavingContext context, IGenericParameterProvider genericParameterProvider)
            => Resolve(context);

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

        public override TypeReference Resolve(ModuleWeavingContext context)
            => throw new WeavingException($"TypeRef.{(_type == GenericParameterType.Method ? "MethodGenericParameters" : "TypeGenericParameters")} can only be used in MethodRef definitions for overload resolution");

        public override TypeReference? TryResolve(ModuleWeavingContext context, IGenericParameterProvider genericParameterProvider)
        {
            if (_type == GenericParameterType.Type && genericParameterProvider.GenericParameterType != GenericParameterType.Type && genericParameterProvider is MemberReference member)
                genericParameterProvider = member.DeclaringType;

            if (!genericParameterProvider.HasGenericParameters || genericParameterProvider.GenericParameterType != _type)
                return null;

            if (_index >= genericParameterProvider.GenericParameters.Count)
                return null;

            genericParameterProvider = genericParameterProvider switch
            {
                TypeReference type     => context.Module.ImportReference(type),
                MethodReference method => context.Module.ImportReference(method),
                _                      => throw new ArgumentException($"Unexpected generic parameter provider type: {genericParameterProvider.GetType().Name}")
            };

            return context.Module.ImportReference(genericParameterProvider.GenericParameters[_index]);
        }

        public override string GetDisplayName()
        {
            return _type switch
            {
                GenericParameterType.Type   => "!" + _index,
                GenericParameterType.Method => "!!" + _index,
                _                           => throw new ArgumentOutOfRangeException()
            };
        }
    }

    private abstract class TypeSpecTypeRefResolver(TypeRefResolver baseResolver) : TypeRefResolver
    {
        protected abstract TypeReference WrapTypeRef(TypeReference typeRef);

        public sealed override TypeReference Resolve(ModuleWeavingContext context)
        {
            var typeRef = baseResolver.Resolve(context);
            return ResolveImpl(context, typeRef);
        }

        public sealed override TypeReference? TryResolve(ModuleWeavingContext context, IGenericParameterProvider genericParameterProvider)
        {
            var typeRef = baseResolver.TryResolve(context, genericParameterProvider);
            return typeRef != null ? ResolveImpl(context, typeRef) : null;
        }

        private TypeReference ResolveImpl(ModuleWeavingContext context, TypeReference typeRef)
        {
            if (typeRef.MetadataType == MetadataType.TypedByReference)
                throw new WeavingException($"Cannot create an array, pointer or ByRef to {nameof(TypedReference)}");

            typeRef = WrapTypeRef(typeRef);
            return context.Module.ImportReference(typeRef);
        }

        public sealed override string GetDisplayName()
            => GetDisplayName(baseResolver.GetDisplayName());

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

    private class ByRefTypeRefResolver(TypeRefResolver baseResolver) : TypeSpecTypeRefResolver(baseResolver)
    {
        protected override TypeReference WrapTypeRef(TypeReference typeRef)
        {
            if (typeRef.IsByReference)
                throw new WeavingException("Type is already a ByRef type");

            return typeRef.MakeByReferenceType();
        }

        protected override string GetDisplayName(string baseName)
            => baseName + "&";
    }

    private class PointerTypeRefResolver(TypeRefResolver baseResolver) : TypeSpecTypeRefResolver(baseResolver)
    {
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

        public override TypeReference Resolve(ModuleWeavingContext context)
        {
            var typeRef = _baseResolver.Resolve(context);
            return ResolveImpl(context, null, typeRef)!;
        }

        public override TypeReference? TryResolve(ModuleWeavingContext context, IGenericParameterProvider genericParameterProvider)
        {
            var typeRef = _baseResolver.TryResolve(context, genericParameterProvider);
            return typeRef != null ? ResolveImpl(context, genericParameterProvider, typeRef) : null;
        }

        private TypeReference? ResolveImpl(ModuleWeavingContext context, IGenericParameterProvider? genericParameterProvider, TypeReference typeRef)
        {
            if (typeRef.IsGenericInstance)
                throw new WeavingException($"Type is already a generic instance: {typeRef.FullName}");

            if (typeRef.IsByReference || typeRef.IsPointer || typeRef.IsArray)
                throw new WeavingException("Cannot make a generic instance of a ByRef, pointer or array type");

            var typeDef = typeRef.ResolveRequiredType(context);

            if (!typeDef.HasGenericParameters)
                throw new WeavingException($"Not a generic type definition: {typeRef.FullName}");

            if (typeDef.GenericParameters.Count != _genericArgs.Length)
                throw new WeavingException($"Incorrect number of generic arguments supplied for type {typeRef.FullName} - expected {typeRef.GenericParameters.Count}, but got {_genericArgs.Length}");

            var genericType = new GenericInstanceType(typeRef);

            foreach (var argTypeBuilder in _genericArgs)
            {
                TypeReference? argTypeRef;

                if (genericParameterProvider != null)
                {
                    argTypeRef = argTypeBuilder.TryBuild(genericParameterProvider);
                    if (argTypeRef == null)
                        return null;
                }
                else
                {
                    argTypeRef = argTypeBuilder.Build();
                }

                genericType.GenericArguments.Add(argTypeRef);
            }

            return context.Module.ImportReference(genericType);
        }

        public override string GetDisplayName()
        {
            var sb = new StringBuilder();
            sb.Append(_baseResolver.GetDisplayName());
            sb.Append('<');

            for (var i = 0; i < _genericArgs.Length; ++i)
            {
                if (i != 0)
                    sb.Append(',');

                sb.Append(_genericArgs[i].GetDisplayName());
            }

            sb.Append('>');
            return sb.ToString();
        }
    }

    private readonly struct Modifier(TypeReference type, bool isRequired)
    {
        public TypeReference Type { get; } = type;
        public bool IsRequired { get; } = isRequired;
    }
}
