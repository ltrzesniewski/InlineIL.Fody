using System.Collections.Generic;
using InlineIL.Fody.Extensions;
using InlineIL.Fody.Support;
using Mono.Cecil;

namespace InlineIL.Fody.Processing
{
    internal class ModuleWeavingContext
    {
        public ModuleDefinition Module { get; }
        public WeaverConfig Config { get; }

        public bool IsDebugBuild { get; }

        internal Dictionary<TypeReference, bool> LibUsageTypeCache { get; } = new();

        public ModuleWeavingContext(ModuleDefinition module, WeaverConfig config)
        {
            Module = module;
            Config = config;

            IsDebugBuild = Module.IsDebugBuild();
        }
    }
}
