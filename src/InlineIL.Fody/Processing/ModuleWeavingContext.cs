using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using InlineIL.Fody.Extensions;
using InlineIL.Fody.Support;
using Mono.Cecil;

namespace InlineIL.Fody.Processing;

internal class ModuleWeavingContext : IDisposable
{
    public ModuleDefinition Module { get; }
    public WeaverConfig Config { get; }

    public bool IsDebugBuild { get; }
    public string ProjectDirectory { get; }

    internal Dictionary<TypeReference, bool> LibUsageTypeCache { get; } = new();
    internal Dictionary<string, AssemblyDefinition?> AssemblyByPath { get; } = new(RuntimeInformation.IsOSPlatform(OSPlatform.Windows) ? StringComparer.OrdinalIgnoreCase : StringComparer.Ordinal);

    public ModuleWeavingContext(ModuleDefinition module, WeaverConfig config, string projectDirectory)
    {
        Module = module;
        Config = config;

        IsDebugBuild = Module.IsDebugBuild();
        ProjectDirectory = projectDirectory;
    }

    public void Dispose()
    {
        foreach (var assembly in AssemblyByPath.Values)
            assembly?.Dispose();

        AssemblyByPath.Clear();
    }
}
