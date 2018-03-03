using System.Collections.Generic;
using System.Linq;
using Fody;

namespace InlineIL.Fody
{
    public class ModuleWeaver : BaseModuleWeaver
    {
        private KnownReferences _refs;
        private MethodWeaver _methodWeaver;

        public override bool ShouldCleanReference => true;

        public override IEnumerable<string> GetAssembliesForScanning()
        {
            yield return "InlineIL";
        }

        public override void Execute()
        {
            _refs = new KnownReferences(FindType);
            _methodWeaver = new MethodWeaver(_refs);

            foreach (var method in ModuleDefinition.Assembly.Modules.SelectMany(m => m.Types).SelectMany(t => t.Methods))
            {
                _methodWeaver.ProcessMethod(method);
            }
        }
    }
}
