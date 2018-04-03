using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Fody;
using Mono.Cecil.Cil;

namespace InlineIL.Fody
{
    public class ModuleWeaver : BaseModuleWeaver
    {
        private readonly Logger _log;

        [SuppressMessage("ReSharper", "MemberCanBeProtected.Global")]
        public ModuleWeaver()
        {
            _log = new Logger(this);
        }

        public override IEnumerable<string> GetAssembliesForScanning()
            => Enumerable.Empty<string>();

        public override void Execute()
        {
            var hasErrors = false;

            foreach (var method in ModuleDefinition.GetTypes().SelectMany(t => t.Methods))
            {
                try
                {
                    if (!MethodWeaver.NeedsProcessing(method))
                        continue;

                    _log.Debug($"Processing: {method.FullName}");
                    new MethodWeaver(ModuleDefinition, method).Process();
                }
                catch (SequencePointWeavingException ex)
                {
                    AddError(ex.Message, ex.SequencePoint);
                    hasErrors = true;
                }
                catch (WeavingException ex)
                {
                    AddError(ex.Message, null);
                    hasErrors = true;
                }
            }

            if (hasErrors)
                throw new WeavingException("Weaving failed - see logged errors");

            RemoveLibReference();
        }

        private void RemoveLibReference()
        {
            var libRef = ModuleDefinition.AssemblyReferences.FirstOrDefault(i => i.Name == "InlineIL");
            if (libRef != null)
            {
                ModuleDefinition.AssemblyReferences.Remove(libRef);
                _log.Debug("Removed reference to InlineIL");
            }
        }

        protected virtual void AddError(string message, SequencePoint sequencePoint)
            => _log.Error(message, sequencePoint);
    }
}
