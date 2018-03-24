using System.Collections.Generic;
using Fody;
using JetBrains.Annotations;
using Mono.Cecil;
using Mono.Cecil.Cil;

namespace InlineIL.Fody
{
    internal class MethodLocals
    {
        private readonly Dictionary<string, VariableDefinition> _localsByName = new Dictionary<string, VariableDefinition>();
        private readonly List<VariableDefinition> _localsByIndex = new List<VariableDefinition>();

        public MethodLocals(MethodDefinition method, IEnumerable<NamedLocal> locals)
        {
            foreach (var local in locals)
            {
                if (_localsByName.ContainsKey(local.Name))
                    throw new WeavingException($"Local {local.Name} is already defined");

                _localsByName.Add(local.Name, local.Definition);
                _localsByIndex.Add(local.Definition);
                method.Body.Variables.Add(local.Definition);
            }
        }

        [CanBeNull]
        public VariableDefinition TryGetByName(string name)
            => _localsByName.GetValueOrDefault(name);

        public class NamedLocal
        {
            public string Name { get; }
            public VariableDefinition Definition { get; }

            public NamedLocal(string name, VariableDefinition definition)
            {
                Name = name;
                Definition = definition;
            }

            public override string ToString() => Name;
        }
    }
}
