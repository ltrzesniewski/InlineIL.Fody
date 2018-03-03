using System.Collections.Generic;
using Fody;

namespace InlineIL.Fody
{
    public class ModuleWeaver : BaseModuleWeaver
    {
        public override void Execute()
        {
            LogWarning($"Processing: {ModuleDefinition.Assembly.Name.FullName}");
        }

        public override IEnumerable<string> GetAssembliesForScanning()
        {
            yield break;
        }
    }
}
