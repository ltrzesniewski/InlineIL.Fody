using System.Collections.Generic;
using System.Linq;
using Fody;

namespace InlineIL.Fody
{
    public class ModuleWeaver : BaseModuleWeaver
    {
        private MethodWeaver _methodWeaver;

        public override bool ShouldCleanReference => true;

        public override IEnumerable<string> GetAssembliesForScanning()
        {
            yield return "InlineIL";
        }

        public override void Execute()
        {
            _methodWeaver = new MethodWeaver(ModuleDefinition);

            foreach (var method in ModuleDefinition.Assembly.Modules.SelectMany(m => m.Types).SelectMany(t => t.Methods))
            {
                _methodWeaver.ProcessMethod(method);
            }
        }
    }
}
