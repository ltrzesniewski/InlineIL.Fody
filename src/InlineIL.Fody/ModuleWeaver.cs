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
            var hasErrors = false;

            foreach (var method in ModuleDefinition.Assembly.Modules.SelectMany(m => m.Types).SelectMany(t => t.Methods))
            {
                try
                {
                    if (MethodWeaver.NeedsProcessing(method))
                        new MethodWeaver(ModuleDefinition, method).Process();
                }
                catch (SequencePointWeavingException ex)
                {
                    LogErrorPoint(ex.Message, ex.SequencePoint);
                    hasErrors = true;
                }
                catch (WeavingException ex)
                {
                    LogError(ex.Message);
                    hasErrors = true;
                }
            }

            if (hasErrors)
                throw new WeavingException("Weaving failed - see logged errors");
        }
    }
}
