using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Fody;
using Mono.Cecil;

namespace InlineIL.Fody.Processing;

internal class InjectedAssemblyResolver
{
    private readonly ModuleWeavingContext _context;
    private readonly Dictionary<string, AssemblyDefinition?> _assemblyByPath = new(RuntimeInformation.IsOSPlatform(OSPlatform.Windows) ? StringComparer.OrdinalIgnoreCase : StringComparer.Ordinal);
    private readonly ConditionalWeakTable<TypeReference, TypeDefinition> _typeDefinitions = new();

    public InjectedAssemblyResolver(ModuleWeavingContext context)
    {
        _context = context;
    }

    public void Dispose()
    {
        foreach (var assembly in _assemblyByPath.Values)
            assembly?.Dispose();

        _assemblyByPath.Clear();
    }

    public AssemblyDefinition ResolveAssemblyByPath(string assemblyPath)
    {
        if (!_assemblyByPath.TryGetValue(assemblyPath, out var assembly))
        {
            try
            {
                assembly = AssemblyDefinition.ReadAssembly(
                    assemblyPath,
                    new ReaderParameters // Same as in Fody
                    {
                        AssemblyResolver = _context.Module.AssemblyResolver,
                        InMemory = true
                    }
                );

                _assemblyByPath.Add(assemblyPath, assembly);
            }
            catch (Exception ex)
            {
                assembly?.Dispose();
                _assemblyByPath[assemblyPath] = null;
                throw new WeavingException($"Could not read assembly: {assemblyPath} - {ex.Message}");
            }
        }

        return assembly ?? throw new WeavingException($"Could not use assembly due to a previous error: {assemblyPath}");
    }

    public void RegisterTypeDefinition(TypeReference typeRef, TypeDefinition typeDef)
    {
        if (_typeDefinitions.TryGetValue(typeDef, out var existingValue))
        {
            if (existingValue == typeDef)
                return;

            throw new WeavingException("Unexpected error: trying to inject a different type definition");
        }

        _typeDefinitions.Add(typeRef, typeDef);
    }

    public TypeDefinition? ResolveRegisteredType(TypeReference typeRef)
        => _typeDefinitions.TryGetValue(typeRef.GetElementType(), out var typeDef) ? typeDef : null;
}
