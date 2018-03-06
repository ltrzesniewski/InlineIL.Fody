using System.Collections.Generic;
using System.Linq;
using Fody;

namespace InlineIL.Fody
{
    public class ModuleWeaver : BaseModuleWeaver
    {
        public override bool ShouldCleanReference => true;

        public override IEnumerable<string> GetAssembliesForScanning()
        {
            yield return "InlineIL";
        }

        public override void Execute()
        {
            foreach (var method in ModuleDefinition.Assembly.Modules.SelectMany(m => m.Types).SelectMany(t => t.Methods))
            {
                if (!MethodWeaver.NeedsProcessing(method))
                    continue;

                new MethodWeaver(ModuleDefinition, method).Process();
            }
        }
    }
}
