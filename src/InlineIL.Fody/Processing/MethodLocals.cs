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
    private readonly MethodDefinition _method;
    private readonly ILogger _log;

    private readonly Dictionary<string, LocalVar> _localsByName = new();
    private readonly List<LocalVar> _localsByIndex = new();

    private bool _localsDeclared;
    private SequencePoint? _declarationSequencePoint;

    public MethodLocals(MethodDefinition method, ILogger log)
    {
        _method = method;
        _log = log;
    }

    public void DeclareLocals(IEnumerable<LocalVarBuilder> localVarBuilders, SequencePoint? sequencePoint)
    {
        if (_localsDeclared)
            throw new WeavingException("Local variables have already been declared for this method.");

        _localsDeclared = true;
        _declarationSequencePoint = sequencePoint;

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
            _method.Body.Variables.Add(localVar.Variable);

            _method.DebugInformation.Scope?.Variables.Add(new VariableDebugInformation(localVar.Variable, localVar.Name ?? $"InlineIL_{localVar.Index}"));
        }
    }

    public VariableDefinition GetLocalByName(string name)
    {
        if (!_localsDeclared)
            throw new WeavingException($"IL local '{name}' is not defined, call IL.DeclareLocals to declare IL locals, or IL.Push/IL.Pop to reference locals declared in the source code.");

        var local = _localsByName.GetValueOrDefault(name)
                    ?? throw new WeavingException($"IL local '{name}' is not defined. If it is a local declared in the source code, use IL.Push/IL.Pop to reference it.");

        local.MarkAsUsed();
        return local.Variable;
    }

    public VariableDefinition GetLocalByIndex(int index)
    {
        if (!_localsDeclared)
            throw new WeavingException($"IL local at index {index} is not defined, call IL.DeclareLocals to declare IL locals.");

        if (index < 0 || index >= _localsByIndex.Count)
            throw new WeavingException($"IL local index {index} is out of range.");

        var local = _localsByIndex[index];

        local.MarkAsUsed();
        return local.Variable;
    }

    public void MapMacroInstruction(Instruction instruction)
    {
        _ = instruction.OpCode.Code switch
        {
            Code.Ldloc_0 => MapIndex(OpCodes.Ldloc, 0),
            Code.Ldloc_1 => MapIndex(OpCodes.Ldloc, 1),
            Code.Ldloc_2 => MapIndex(OpCodes.Ldloc, 2),
            Code.Ldloc_3 => MapIndex(OpCodes.Ldloc, 3),
            Code.Stloc_0 => MapIndex(OpCodes.Stloc, 0),
            Code.Stloc_1 => MapIndex(OpCodes.Stloc, 1),
            Code.Stloc_2 => MapIndex(OpCodes.Stloc, 2),
            Code.Stloc_3 => MapIndex(OpCodes.Stloc, 3),
            _            => default
        };

        int MapIndex(OpCode opCode, int index)
        {
            var local = GetLocalByIndex(index);
            instruction.OpCode = opCode;
            instruction.Operand = local;
            return default;
        }
    }

    public bool MapIndexInstruction(OpCode opCode, int index, [MaybeNullWhen(false)] out VariableDefinition result)
    {
        switch (opCode.Code)
        {
            case Code.Ldloc:
            case Code.Ldloc_S:
            case Code.Ldloca:
            case Code.Ldloca_S:
            case Code.Stloc:
            case Code.Stloc_S:
                result = GetLocalByIndex(index);
                return true;

            default:
                result = null;
                return false;
        }
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
                    _declarationSequencePoint
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
