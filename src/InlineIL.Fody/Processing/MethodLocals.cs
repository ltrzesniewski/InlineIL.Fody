using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Fody;
using InlineIL.Fody.Extensions;
using InlineIL.Fody.Model;
using InlineIL.Fody.Support;
using Mono.Cecil;
using Mono.Cecil.Cil;

namespace InlineIL.Fody.Processing;

internal class MethodLocals
{
    private readonly ILogger _log;
    private readonly SequencePoint? _sequencePoint;

    private readonly Dictionary<string, LocalVar> _localsByName = new();
    private readonly List<LocalVar> _localsByIndex = new();

    public MethodLocals(MethodDefinition method,
                        IEnumerable<LocalVarBuilder> localVarBuilders,
                        ILogger log,
                        SequencePoint? sequencePoint)
    {
        _log = log;
        _sequencePoint = sequencePoint;

        foreach (var builder in localVarBuilders)
        {
            var localVar = new LocalVar
            {
                Variable = builder.Build(),
                Name = builder.Name,
                Index = _localsByIndex.Count
            };

            if (localVar.Name != null)
            {
                if (_localsByName.ContainsKey(localVar.Name))
                    throw new WeavingException($"Local {localVar.Name} is already defined");

                _localsByName.Add(localVar.Name, localVar);
            }

            _localsByIndex.Add(localVar);
            method.Body.Variables.Add(localVar.Variable);

            method.DebugInformation.Scope?.Variables.Add(new VariableDebugInformation(localVar.Variable, localVar.Name ?? $"InlineIL_{localVar.Index}"));
        }
    }

    public VariableDefinition? TryGetByName(string name)
    {
        var local = _localsByName.GetValueOrDefault(name);
        local?.MarkAsUsed();
        return local?.Variable;
    }

    public static void MapMacroInstruction(MethodLocals? locals, Instruction instruction)
    {
        switch (instruction.OpCode.Code)
        {
            case Code.Ldloc_0:
                MapIndex(OpCodes.Ldloc, 0);
                break;
            case Code.Ldloc_1:
                MapIndex(OpCodes.Ldloc, 1);
                break;
            case Code.Ldloc_2:
                MapIndex(OpCodes.Ldloc, 2);
                break;
            case Code.Ldloc_3:
                MapIndex(OpCodes.Ldloc, 3);
                break;
            case Code.Stloc_0:
                MapIndex(OpCodes.Stloc, 0);
                break;
            case Code.Stloc_1:
                MapIndex(OpCodes.Stloc, 1);
                break;
            case Code.Stloc_2:
                MapIndex(OpCodes.Stloc, 2);
                break;
            case Code.Stloc_3:
                MapIndex(OpCodes.Stloc, 3);
                break;
        }

        void MapIndex(OpCode opCode, int index)
        {
            var local = GetLocalByIndex(locals, index);
            instruction.OpCode = opCode;
            instruction.Operand = local;
        }
    }

    public static bool MapIndexInstruction(MethodLocals? locals, ref OpCode opCode, int index, [MaybeNullWhen(false)] out VariableDefinition result)
    {
        switch (opCode.Code)
        {
            case Code.Ldloc:
            case Code.Ldloc_S:
            case Code.Ldloca:
            case Code.Ldloca_S:
            case Code.Stloc:
            case Code.Stloc_S:
            {
                result = GetLocalByIndex(locals, index);
                return true;
            }

            default:
                result = null;
                return false;
        }
    }

    internal static VariableDefinition GetLocalByIndex(MethodLocals? locals, int index)
    {
        if (locals == null)
            throw new WeavingException("No locals are defined");

        if (index < 0 || index >= locals._localsByIndex.Count)
            throw new WeavingException($"Local index {index} is out of range");

        var local = locals._localsByIndex[index];
        local.MarkAsUsed();
        return local.Variable;
    }

    public void PostProcess()
    {
        foreach (var localVar in _localsByIndex)
        {
            if (!localVar.IsUsed)
            {
                _log.Warning(
                    localVar.Name is not null
                        ? $"Unused local: '{localVar.Name}'"
                        : $"Unused local at index {localVar.Index}",
                    _sequencePoint
                );
            }
        }
    }

    private class LocalVar
    {
        public required VariableDefinition Variable { get; init; }
        public required string? Name { get; init; }
        public required int Index { get; init; }

        public bool IsUsed { get; private set; }

        public void MarkAsUsed()
            => IsUsed = true;

        public override string ToString()
            => Name ?? $"(index {Index})";
    }
}
