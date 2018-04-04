using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Fody;
using Mono.Cecil;
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
                    InvalidateMethod(method, ex.Message);
                }
                catch (WeavingException ex)
                {
                    AddError(ex.Message, null);
                    InvalidateMethod(method, ex.Message);
                }
            }

            RemoveLibReference();
        }

        private void InvalidateMethod(MethodDefinition method, string message)
        {
            method.Body.Instructions.Clear();
            method.Body.Variables.Clear();
            method.Body.ExceptionHandlers.Clear();

            var exceptionCtor = new TypeReference("System", nameof(InvalidProgramException), ModuleDefinition, ModuleDefinition.TypeSystem.CoreLibrary)
                                .Resolve()?
                                .Methods
                                .FirstOrDefault(m => m.IsRuntimeSpecialName
                                                     && m.Name == ".ctor"
                                                     && m.Parameters.Count == 1
                                                     && m.Parameters[0].ParameterType.FullName == ModuleDefinition.TypeSystem.String.FullName
                                );

            if (exceptionCtor != null)
            {
                method.Body.Instructions.AddRange(
                    Instruction.Create(OpCodes.Ldstr, $"InlineIL processing failed: {message}"),
                    Instruction.Create(OpCodes.Newobj, ModuleDefinition.ImportReference(exceptionCtor)),
                    Instruction.Create(OpCodes.Throw)
                );
            }
            else
            {
                method.Body.Instructions.AddRange(
                    Instruction.Create(OpCodes.Ldnull),
                    Instruction.Create(OpCodes.Throw)
                );
            }
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
