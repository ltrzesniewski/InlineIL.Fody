using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using Fody;
using InlineIL.Fody.Extensions;
using InlineIL.Fody.Processing;
using InlineIL.Fody.Support;
using Mono.Cecil;
using Mono.Cecil.Cil;

namespace InlineIL.Fody;

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
        var configOptions = new WeaverConfigOptions(Config);
        var config = new WeaverConfig(configOptions, ModuleDefinition);
        var context = new ModuleWeavingContext(ModuleDefinition, config);
        var weaverLog = new WeaverLogger(_log, configOptions);

        foreach (var type in ModuleDefinition.GetTypes())
        {
            foreach (var method in type.Methods)
            {
                try
                {
                    if (!MethodWeaver.NeedsProcessing(context, method))
                        continue;

                    _log.Debug($"Processing: {method.FullName}");
                    new MethodWeaver(context, method, weaverLog).Process();
                }
                catch (WeavingException ex)
                {
                    AddError(ex.Message, ex.SequencePoint);
                    InvalidateMethod(method, ex.Message);
                }
            }

            if (type.IsInlineILTypeUsageDeep(context))
                AddError($"Reference to InlineIL found in type {type.FullName}. InlineIL should not be referenced in attributes/constraints, as its assembly reference will be removed.", null);
        }

        RemoveLibReference(context);
    }

    private void InvalidateMethod(MethodDefinition method, string message)
    {
        method.Body.Instructions.Clear();

        if (method.Body.HasVariables)
            method.Body.Variables.Clear();

        if (method.Body.HasExceptionHandlers)
            method.Body.ExceptionHandlers.Clear();

        if (method.DebugInformation.HasSequencePoints)
            method.DebugInformation.SequencePoints.Clear();

        method.DebugInformation.Scope = null;
        method.DebugInformation.StateMachineKickOffMethod = null;

        if (method.HasCustomDebugInformations)
            method.CustomDebugInformations.Clear();

        MethodReference? exceptionCtor = null;

        var coreLibrary = ModuleDefinition.GetCoreLibrary();
        if (coreLibrary != null)
        {
            exceptionCtor = new TypeReference("System", nameof(InvalidProgramException), ModuleDefinition, coreLibrary)
                            .Resolve()?
                            .Methods
                            .FirstOrDefault(m => m.IsRuntimeSpecialName
                                                 && m.Name == ".ctor"
                                                 && m.Parameters.Count == 1
                                                 && m.Parameters[0].ParameterType.FullName == "System.String"
                            )?
                            .TryMapToScope(coreLibrary, ModuleDefinition);
        }

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

    private void RemoveLibReference(ModuleWeavingContext context)
    {
        var libRef = ModuleDefinition.AssemblyReferences.FirstOrDefault(i => i.IsInlineILAssembly());
        if (libRef == null)
            return;

        var importScopes = new HashSet<ImportDebugInformation>();

        foreach (var method in ModuleDefinition.GetTypes().SelectMany(t => t.Methods))
        {
            foreach (var scope in method.DebugInformation.GetScopes())
                ProcessScope(scope);
        }

        ModuleDefinition.AssemblyReferences.Remove(libRef);

        var copyLocalFilesToRemove = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
        {
            libRef.Name + ".dll",
            libRef.Name + ".xml",
            libRef.Name + ".pdb" // We don't ship this, but future-proof that ;)
        };

        ReferenceCopyLocalPaths.RemoveAll(i => copyLocalFilesToRemove.Contains(Path.GetFileName(i)));

        _log.Debug("Removed reference to InlineIL");

        void ProcessScope(ScopeDebugInformation scope)
        {
            ProcessImportScope(scope.Import);

            if (scope.HasScopes)
            {
                foreach (var childScope in scope.Scopes)
                    ProcessScope(childScope);
            }
        }

        void ProcessImportScope(ImportDebugInformation? importScope)
        {
            if (importScope == null || !importScopes.Add(importScope))
                return;

            importScope.Targets.RemoveWhere(t => t.AssemblyReference.IsInlineILAssembly() || t.Type.IsInlineILTypeUsage(context));
            ProcessImportScope(importScope.Parent);
        }
    }

    public override bool ShouldCleanReference => true; // Enable the check for PrivateAssets

    protected virtual void AddError(string message, SequencePoint? sequencePoint)
        => _log.Error(message, sequencePoint);
}
