using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Fody;
using Mono.Cecil.Cil;

namespace InlineIL.Fody
{
    internal static class OpCodeMap
    {
        private static readonly Dictionary<string, OpCode> _opCodes;

        static OpCodeMap()
        {
            _opCodes = typeof(OpCodes)
                       .GetFields(BindingFlags.Public | BindingFlags.Static)
                       .Where(field => field.IsInitOnly && field.FieldType == typeof(OpCode))
                       .ToDictionary(field => field.Name, field => (OpCode)field.GetValue(null));
        }

        public static OpCode FromCecilFieldName(string fieldName)
        {
            if (!_opCodes.TryGetValue(fieldName, out var opCode))
                throw new WeavingException($"Unknown opcode: {fieldName}");

            return opCode;
        }
    }
}
