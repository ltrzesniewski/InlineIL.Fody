using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Fody;
using Mono.Cecil;
using Mono.Cecil.Cil;

namespace InlineIL.Fody
{
    internal static class OpCodeMap
    {
        private static readonly Dictionary<short, OpCode> _byValue;
        private static readonly Dictionary<string, OpCode> _byCecilFieldName;
        private static readonly Dictionary<string, OpCode> _byReflectionEmitFieldName;

        static OpCodeMap()
        {
            var cecilOpCodes = typeof(OpCodes)
                               .GetFields(BindingFlags.Public | BindingFlags.Static)
                               .Where(field => field.IsInitOnly && field.FieldType == typeof(OpCode))
                               .Select(field => new { field, opCode = (OpCode)field.GetValue(null) })
                               .ToDictionary(field => field.opCode.Value);

            var items = typeof(System.Reflection.Emit.OpCodes)
                        .GetFields(BindingFlags.Public | BindingFlags.Static)
                        .Where(field => field.IsInitOnly && field.FieldType == typeof(System.Reflection.Emit.OpCode))
                        .Select(field => new { field, opCode = (System.Reflection.Emit.OpCode)field.GetValue(null) })
                        .Where(item => cecilOpCodes.ContainsKey(item.opCode.Value))
                        .Select(item => new { item.field, item.opCode, cecilOpCode = cecilOpCodes[item.opCode.Value].opCode })
                        .ToList();

            _byCecilFieldName = cecilOpCodes.Values.ToDictionary(item => item.field.Name, item => item.opCode);
            _byReflectionEmitFieldName = items.ToDictionary(item => item.field.Name, item => item.cecilOpCode);
            _byValue = items.ToDictionary(item => item.opCode.Value, item => item.cecilOpCode);
        }

        public static OpCode FromReflectionEmit(System.Reflection.Emit.OpCode opCode)
        {
            if (!_byValue.TryGetValue(opCode.Value, out var result))
                throw new WeavingException($"Unsupported opcode: {opCode.Name}");

            return result;
        }

        public static OpCode FromCecilFieldName(string fieldName)
        {
            if (!_byCecilFieldName.TryGetValue(fieldName, out var opCode))
                throw new WeavingException($"Unknown opcode: {fieldName}");

            return opCode;
        }

        public static OpCode FromLdsfld(Instruction ldsfld)
        {
            var argType = typeof(System.Reflection.Emit.OpCodes);

            if (ldsfld.OpCode != OpCodes.Ldsfld)
                throw new InstructionWeavingException(ldsfld, $"IL.Emit should be given a parameter directly from the {argType.Name} type (expected ldsfld instruction, but got {ldsfld.OpCode} instead)");

            var field = (FieldReference)ldsfld.Operand;
            if (field.DeclaringType.FullName != argType.FullName)
                throw new InstructionWeavingException(ldsfld, $"IL.Emit expects an argument directly from the {argType.FullName} type");

            if (!_byReflectionEmitFieldName.TryGetValue(field.Name, out var result))
                throw new InstructionWeavingException(ldsfld, $"Unsupported opcode: {field.Name}");

            return result;
        }
    }
}
