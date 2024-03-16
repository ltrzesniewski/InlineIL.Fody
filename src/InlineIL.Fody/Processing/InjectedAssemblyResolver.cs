using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using Fody;
using Mono.Cecil;

namespace InlineIL.Fody.Processing;

internal class InjectedAssemblyResolver : IAssemblyResolver
{
    private readonly ModuleWeavingContext _context;
    private readonly Dictionary<string, AssemblyDefinition?> _assemblyByPath = new(RuntimeInformation.IsOSPlatform(OSPlatform.Windows) ? StringComparer.OrdinalIgnoreCase : StringComparer.Ordinal);

    public IMetadataResolver MetadataResolver { get; }

    public InjectedAssemblyResolver(ModuleWeavingContext context)
    {
        _context = context;
        MetadataResolver = new MetadataResolver(this);
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
                _assemblyByPath.Add(assemblyPath, null);
                throw new WeavingException($"Could not read assembly: {assemblyPath} - {ex.Message}");
            }
        }

        return assembly ?? throw new WeavingException($"Could not use assembly due to a previous error: {assemblyPath}");
    }

    public AssemblyDefinition? Resolve(AssemblyNameReference name)
        => _assemblyByPath.Values.FirstOrDefault(assembly => assembly?.FullName == name.FullName);

    public AssemblyDefinition? Resolve(AssemblyNameReference name, ReaderParameters parameters)
        => Resolve(name);
}
