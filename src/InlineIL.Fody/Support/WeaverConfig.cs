using System;
using InlineIL.Fody.Extensions;
using Mono.Cecil;

namespace InlineIL.Fody.Support
{
    internal class WeaverConfig
    {
        public bool GenerateSequencePoints { get; }

        public WeaverConfig(WeaverConfigOptions config, ModuleDefinition module)
        {
            GenerateSequencePoints = ShouldGenerateSequencePoints(config, module);
        }

        private static bool ShouldGenerateSequencePoints(WeaverConfigOptions config, ModuleDefinition module)
        {
            switch (config.SequencePoints)
            {
                case WeaverConfigOptions.SequencePointsBehavior.False:
                    return false;

                case WeaverConfigOptions.SequencePointsBehavior.True:
                    return true;

                case WeaverConfigOptions.SequencePointsBehavior.Debug:
                    return module.IsDebugBuild();

                case WeaverConfigOptions.SequencePointsBehavior.Release:
                    return !module.IsDebugBuild();

                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}
