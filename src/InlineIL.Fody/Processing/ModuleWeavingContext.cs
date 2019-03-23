using InlineIL.Fody.Support;
using Mono.Cecil;

namespace InlineIL.Fody.Processing
{
    internal class ModuleWeavingContext
    {
        public ModuleDefinition Module { get; }
        public WeaverConfig Config { get; }

        public ModuleWeavingContext(ModuleDefinition module, WeaverConfig config)
        {
            Module = module;
            Config = config;
        }
    }
}
