using System.Collections.Generic;
using Fody;
using InlineIL.Fody.Extensions;
using JetBrains.Annotations;
using Mono.Cecil.Cil;

namespace InlineIL.Fody.Processing
{
    internal class MethodLocals
    {
        private readonly Dictionary<string, VariableDefinition> _localsByName = new Dictionary<string, VariableDefinition>();
        private readonly List<VariableDefinition> _localsByIndex = new List<VariableDefinition>();

        public MethodLocals(MethodBody methodBody, IEnumerable<NamedLocal> locals)
        {
            foreach (var local in locals)
            {
                if (local.Name != null)
                {
                    if (_localsByName.ContainsKey(local.Name))
                        throw new WeavingException($"Local {local.Name} is already defined");

                    _localsByName.Add(local.Name, local.Definition);
                }

                _localsByIndex.Add(local.Definition);
                methodBody.Variables.Add(local.Definition);
            }
        }

        [CanBeNull]
        public VariableDefinition TryGetByName(string name)
            => _localsByName.GetValueOrDefault(name);

        public static void MapMacroInstruction([CanBeNull] MethodLocals locals, Instruction instruction)
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

        public static bool MapIndexInstruction([CanBeNull] MethodLocals locals, ref OpCode opCode, int index, out VariableDefinition result)
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

        private static VariableDefinition GetLocalByIndex([CanBeNull] MethodLocals locals, int index)
        {
            if (locals == null)
                throw new WeavingException("No locals are defined");

            if (index < 0 || index >= locals._localsByIndex.Count)
                throw new WeavingException($"Local index {index} is out of range");

            return locals._localsByIndex[index];
        }

        public class NamedLocal
        {
            [CanBeNull]
            public string Name { get; }

            public VariableDefinition Definition { get; }

            public NamedLocal([CanBeNull] string name, VariableDefinition definition)
            {
                Name = name;
                Definition = definition;
            }

            public override string ToString() => Name ?? $"#{Definition.Index}";
        }
    }
}
