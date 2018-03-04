using System;
using System.Linq;
using Fody;
using Mono.Cecil;
using Mono.Cecil.Cil;
using Mono.Cecil.Rocks;
using Mono.Collections.Generic;

namespace InlineIL.Fody
{
    internal class MethodContext
    {
        private readonly ModuleDefinition _module;
        private readonly MethodDefinition _method;
        private readonly ILProcessor _il;

        private Collection<Instruction> Instructions => _method.Body.Instructions;

        public MethodContext(ModuleDefinition module, MethodDefinition method)
        {
            _module = module;
            _method = method;
            _il = _method.Body.GetILProcessor();
        }

        public static bool IsIlType(TypeReference type)
            => type.Name == KnownNames.Short.IlType && type.FullName == KnownNames.Full.IlType;

        public void Process()
        {
            _method.Body.SimplifyMacros();

            var instruction = Instructions.FirstOrDefault();

            while (instruction != null)
            {
                var nextInstruction = instruction.Next;

                if (instruction.OpCode == OpCodes.Call && instruction.Operand is MethodReference calledMethod && IsIlType(calledMethod.DeclaringType))
                {
                    switch (calledMethod.Name)
                    {
                        case KnownNames.Short.Op:
                            ProcessOpMethod(instruction);
                            break;

                        case KnownNames.Short.PushMethod:
                            ProcessPushMethod(instruction);
                            break;

                        case KnownNames.Short.UnreachableMethod:
                            ProcessUnreachableMethod(instruction);
                            break;

                        default:
                            throw new WeavingException($"Unsupported method: {calledMethod.FullName}");
                    }
                }

                instruction = nextInstruction;
            }

            _method.Body.OptimizeMacros();
        }

        private void ProcessOpMethod(Instruction instruction)
        {
            var methodParams = ((MethodReference)instruction.Operand).Parameters;

            var args = instruction.GetArgumentPushInstructions();
            var opCode = ConsumeArgOpCode(args[0]);

            if (methodParams.Count == 1)
            {
                _il.Replace(instruction, InstructionHelper.Create(opCode));
                return;
            }

            var operandType = methodParams[1].ParameterType;
            if (operandType.IsPrimitive || operandType.FullName == _module.TypeSystem.String.FullName)
            {
                var operandValue = ConsumeArgConst(args[1]);
                _il.Replace(instruction, InstructionHelper.CreateConst(_il, opCode, operandValue));
                return;
            }

            if (operandType.FullName == "System.Type")
            {
                var typeRef = ConsumeArgType(args[1]);
                _il.Replace(instruction, InstructionHelper.Create(opCode, typeRef));
                return;
            }

            throw new InvalidOperationException("Unsupported IL.Op overload");
        }

        private void ProcessPushMethod(Instruction instruction)
        {
            _il.Remove(instruction);
        }

        private void ProcessUnreachableMethod(Instruction instruction)
        {
            var throwInstruction = instruction.NextSkipNops();
            if (throwInstruction.OpCode != OpCodes.Throw)
                throw new WeavingException("The Unreachable method should be used like this: throw IL.Unreachable();");

            _il.Remove(instruction);
            _il.RemoveNopsAround(throwInstruction);
            _il.Remove(throwInstruction);
        }

        private OpCode ConsumeArgOpCode(Instruction instruction)
        {
            _il.Remove(instruction);
            return OpCodeMap.FromLdsfld(instruction);
        }

        private object ConsumeArgConst(Instruction instruction)
        {
            switch (instruction.OpCode.OperandType)
            {
                case OperandType.InlineI:
                case OperandType.InlineI8:
                case OperandType.InlineR:
                case OperandType.ShortInlineI:
                case OperandType.ShortInlineR:
                case OperandType.InlineString:
                    _il.Remove(instruction);
                    return instruction.Operand;
            }

            switch (instruction.OpCode.Code)
            {
                case Code.Conv_I1:
                case Code.Conv_I2:
                case Code.Conv_I4:
                case Code.Conv_I8:
                case Code.Conv_U1:
                case Code.Conv_U2:
                case Code.Conv_U4:
                case Code.Conv_U8:
                case Code.Conv_R4:
                case Code.Conv_R8:
                    var value = ConsumeArgConst(instruction.PrevSkipNops());
                    _il.Remove(instruction);
                    return value;
            }

            throw new WeavingException($"Invalid operand, expected a constant, but was {instruction.Operand ?? "null"}");
        }

        private TypeReference ConsumeArgType(Instruction instruction)
        {
            if (instruction.OpCode != OpCodes.Call || !(instruction.Operand is MethodReference method) || method.FullName != "System.Type System.Type::GetTypeFromHandle(System.RuntimeTypeHandle)")
                throw new WeavingException("Invalid operand, expected System.Type");

            var ldToken = instruction.GetArgumentPushInstructions().Single();
            if (ldToken.OpCode != OpCodes.Ldtoken)
                throw new WeavingException("Invalid operand, expected ldtoken");

            _il.Remove(ldToken);
            _il.Remove(instruction);

            return (TypeReference)ldToken.Operand;
        }
    }
}
