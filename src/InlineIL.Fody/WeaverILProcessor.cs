using System;
using System.Collections.Generic;
using Fody;
using Mono.Cecil;
using Mono.Cecil.Cil;

namespace InlineIL.Fody
{
    internal class WeaverILProcessor
    {
        private readonly ILProcessor _il;
        private readonly HashSet<Instruction> _referencedInstructions = new HashSet<Instruction>();

        public WeaverILProcessor(MethodDefinition method)
        {
            _il = method.Body.GetILProcessor();

            foreach (var handler in method.Body.ExceptionHandlers)
                _referencedInstructions.UnionWith(handler.GetInstructions());

            foreach (var instruction in method.Body.Instructions)
            {
                switch (instruction.Operand)
                {
                    case Instruction target:
                        _referencedInstructions.Add(target);
                        break;

                    case Instruction[] targets:
                        _referencedInstructions.UnionWith(targets);
                        break;
                }
            }
        }

        public void Remove(Instruction instruction)
        {
            var newRef = instruction.Next ?? instruction.Previous;
            _il.Remove(instruction);
            UpdateReferences(instruction, newRef);
        }

        public void Replace(Instruction oldInstruction, Instruction newInstruction)
        {
            _il.Replace(oldInstruction, newInstruction);
            UpdateReferences(oldInstruction, newInstruction);
        }

        private void UpdateReferences(Instruction oldInstruction, Instruction newInstruction)
        {
            foreach (var handler in _il.Body.ExceptionHandlers)
            {
                if (handler.TryStart == oldInstruction)
                    handler.TryStart = newInstruction;

                if (handler.TryEnd == oldInstruction)
                    handler.TryEnd = newInstruction;

                if (handler.FilterStart == oldInstruction)
                    handler.FilterStart = newInstruction;

                if (handler.HandlerStart == oldInstruction)
                    handler.HandlerStart = newInstruction;

                if (handler.HandlerEnd == oldInstruction)
                    handler.HandlerEnd = newInstruction;
            }

            foreach (var instruction in _il.Body.Instructions)
            {
                switch (instruction.Operand)
                {
                    case Instruction target when target == oldInstruction:
                        instruction.Operand = newInstruction;
                        break;

                    case Instruction[] targets:
                        for (var i = 0; i < targets.Length; ++i)
                        {
                            if (targets[i] == oldInstruction)
                                targets[i] = newInstruction;
                        }

                        break;
                }
            }

            _referencedInstructions.Remove(oldInstruction);
            _referencedInstructions.Add(newInstruction);
        }

        public void RemoveNopsAround(Instruction instruction)
        {
            var currentInstruction = instruction.Previous;
            while (currentInstruction != null && currentInstruction.OpCode == OpCodes.Nop)
            {
                var next = currentInstruction.Previous;
                Remove(currentInstruction);
                currentInstruction = next;
            }

            currentInstruction = instruction.Next;
            while (currentInstruction != null && currentInstruction.OpCode == OpCodes.Nop)
            {
                var next = currentInstruction.Next;
                Remove(currentInstruction);
                currentInstruction = next;
            }
        }

        public Instruction Create(OpCode opCode)
        {
            try
            {
                return _il.Create(opCode);
            }
            catch (ArgumentException)
            {
                throw ExceptionInvalidOperand(opCode);
            }
        }

        public Instruction Create(OpCode opCode, TypeReference typeRef)
        {
            try
            {
                return _il.Create(opCode, typeRef);
            }
            catch (ArgumentException)
            {
                throw ExceptionInvalidOperand(opCode);
            }
        }

        public Instruction Create(OpCode opCode, MethodReference methodRef)
        {
            try
            {
                return _il.Create(opCode, methodRef);
            }
            catch (ArgumentException)
            {
                throw ExceptionInvalidOperand(opCode);
            }
        }

        public Instruction Create(OpCode opCode, FieldReference fieldRef)
        {
            try
            {
                return _il.Create(opCode, fieldRef);
            }
            catch (ArgumentException)
            {
                throw ExceptionInvalidOperand(opCode);
            }
        }

        public Instruction Create(OpCode opCode, Instruction instruction)
        {
            try
            {
                return _il.Create(opCode, instruction);
            }
            catch (ArgumentException)
            {
                throw ExceptionInvalidOperand(opCode);
            }
        }

        public Instruction Create(OpCode opCode, Instruction[] instructions)
        {
            try
            {
                return _il.Create(opCode, instructions);
            }
            catch (ArgumentException)
            {
                throw ExceptionInvalidOperand(opCode);
            }
        }

        public Instruction Create(OpCode opCode, VariableDefinition variableDef)
        {
            try
            {
                return _il.Create(opCode, variableDef);
            }
            catch (ArgumentException)
            {
                throw ExceptionInvalidOperand(opCode);
            }
        }

        public Instruction Create(OpCode opCode, CallSite callSite)
        {
            try
            {
                return _il.Create(opCode, callSite);
            }
            catch (ArgumentException)
            {
                throw ExceptionInvalidOperand(opCode);
            }
        }

        public Instruction CreateConst(OpCode opCode, object operand)
        {
            try
            {
                switch (opCode.OperandType)
                {
                    case OperandType.InlineI:
                        operand = Convert.ToInt32(operand);
                        break;

                    case OperandType.InlineI8:
                        operand = Convert.ToInt64(operand);
                        break;

                    case OperandType.InlineR:
                        operand = Convert.ToDouble(operand);
                        break;

                    case OperandType.ShortInlineI:
                        operand = opCode != OpCodes.Ldc_I4_S
                            ? (object)Convert.ToByte(operand)
                            : Convert.ToSByte(operand);
                        break;

                    case OperandType.ShortInlineR:
                        operand = Convert.ToSingle(operand);
                        break;
                }

                switch (operand)
                {
                    case byte value:
                        return _il.Create(opCode, value);
                    case sbyte value:
                        return _il.Create(opCode, value);
                    case int value:
                        return _il.Create(opCode, value);
                    case long value:
                        return _il.Create(opCode, value);
                    case double value:
                        return _il.Create(opCode, value);
                    case short value:
                        return _il.Create(opCode, value);
                    case float value:
                        return _il.Create(opCode, value);
                    case string value:
                        return _il.Create(opCode, value);
                    default:
                        throw new WeavingException($"Unexpected operand for instruction {opCode}: {operand}");
                }
            }
            catch (ArgumentException)
            {
                throw ExceptionInvalidOperand(opCode);
            }
        }

        private static WeavingException ExceptionInvalidOperand(OpCode opCode)
            => new WeavingException($"Invalid instruction/operand combination: {opCode}");
    }
}
