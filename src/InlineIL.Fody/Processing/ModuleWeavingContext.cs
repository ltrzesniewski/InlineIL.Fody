using System.Collections.Generic;
using InlineIL.Fody.Support;
using Mono.Cecil;

namespace InlineIL.Fody.Processing
{
    internal class ModuleWeavingContext
    {
        public ModuleDefinition Module { get; }
        public WeaverConfig Config { get; }

        internal Dictionary<TypeReference, bool> LibUsageTypeCache { get; } = new Dictionary<TypeReference, bool>();

        public ModuleWeavingContext(ModuleDefinition module, WeaverConfig config)
        {
            Module = module;
            Config = config;
        }
    }
}
