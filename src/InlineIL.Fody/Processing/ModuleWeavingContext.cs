using System;
using System.Collections.Generic;
using InlineIL.Fody.Extensions;
using InlineIL.Fody.Support;
using Mono.Cecil;

namespace InlineIL.Fody.Processing;

internal class ModuleWeavingContext : IDisposable
{
    public ModuleDefinition Module { get; }
    public WeaverConfig Config { get; }

    public bool IsDebugBuild { get; }
    public string? ProjectDirectory { get; }

    internal Dictionary<TypeReference, bool> LibUsageTypeCache { get; } = new();
    internal InjectedAssemblyResolver InjectedAssemblyResolver { get; }

    public ModuleWeavingContext(ModuleDefinition module, WeaverConfig config, string? projectDirectory)
    {
        Module = module;
        Config = config;

        IsDebugBuild = Module.IsDebugBuild();
        ProjectDirectory = projectDirectory;

        InjectedAssemblyResolver = new InjectedAssemblyResolver(this);
    }

    public void Dispose()
    {
        InjectedAssemblyResolver.Dispose();
    }
}
