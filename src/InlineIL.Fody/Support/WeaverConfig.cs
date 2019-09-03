using System;
using InlineIL.Fody.Extensions;
using Mono.Cecil;

namespace InlineIL.Fody.Support
{
    internal class WeaverConfig
    {
        public bool GenerateSequencePoints { get; }

        public WeaverConfig(WeaverConfigOptions? config, ModuleDefinition module)
        {
            if (config == null)
                config = new WeaverConfigOptions(null);

            GenerateSequencePoints = ShouldGenerateSequencePoints(config, module);
        }

        private static bool ShouldGenerateSequencePoints(WeaverConfigOptions config, ModuleDefinition module)
        {
            return config.SequencePoints switch
            {
                WeaverConfigOptions.SequencePointsBehavior.False => false,
                WeaverConfigOptions.SequencePointsBehavior.True => true,
                WeaverConfigOptions.SequencePointsBehavior.Debug => module.IsDebugBuild(),
                WeaverConfigOptions.SequencePointsBehavior.Release => !module.IsDebugBuild(),
                _ => throw new ArgumentOutOfRangeException()
            };
        }
    }
}
