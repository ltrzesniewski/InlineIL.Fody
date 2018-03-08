using System;
using Fody;
using Mono.Cecil;
using Mono.Cecil.Cil;

namespace InlineIL.Fody
{
    internal static class InstructionHelper
    {
        public static Instruction Create(OpCode opCode)
        {
            try
            {
                return Instruction.Create(opCode);
            }
            catch (ArgumentException)
            {
                throw ExceptionInvalidOperand(opCode);
            }
        }

        public static Instruction Create(OpCode opCode, TypeReference typeRef)
        {
            try
            {
                return Instruction.Create(opCode, typeRef);
            }
            catch (ArgumentException)
            {
                throw ExceptionInvalidOperand(opCode);
            }
        }

        public static Instruction Create(OpCode opCode, MethodReference methodRef)
        {
            try
            {
                return Instruction.Create(opCode, methodRef);
            }
            catch (ArgumentException)
            {
                throw ExceptionInvalidOperand(opCode);
            }
        }

        public static Instruction Create(OpCode opCode, Instruction instruction)
        {
            try
            {
                return Instruction.Create(opCode, instruction);
            }
            catch (ArgumentException)
            {
                throw ExceptionInvalidOperand(opCode);
            }
        }

        public static Instruction Create(OpCode opCode, Instruction[] instructions)
        {
            try
            {
                return Instruction.Create(opCode, instructions);
            }
            catch (ArgumentException)
            {
                throw ExceptionInvalidOperand(opCode);
            }
        }

        public static Instruction CreateConst(ILProcessor il, OpCode opCode, object operand)
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
                        return il.Create(opCode, value);
                    case sbyte value:
                        return il.Create(opCode, value);
                    case int value:
                        return il.Create(opCode, value);
                    case long value:
                        return il.Create(opCode, value);
                    case double value:
                        return il.Create(opCode, value);
                    case short value:
                        return il.Create(opCode, value);
                    case float value:
                        return il.Create(opCode, value);
                    case string value:
                        return il.Create(opCode, value);
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
