using System;
using System.Linq;
using Fody;
using Mono.Cecil;
using Mono.Cecil.Cil;
using Mono.Cecil.Rocks;
using Mono.Collections.Generic;

namespace InlineIL.Fody
{
    internal class MethodWeaver
    {
        private readonly ModuleDefinition _module;
        private readonly MethodDefinition _method;
        private readonly ILProcessor _il;

        private Collection<Instruction> Instructions => _method.Body.Instructions;

        public MethodWeaver(ModuleDefinition module, MethodDefinition method)
        {
            _module = module;
            _method = method;
            _il = _method.Body.GetILProcessor();
        }

        public static bool NeedsProcessing(MethodDefinition method)
        {
            return method.HasBody
                   && method.Body
                            .Instructions
                            .Where(i => i.OpCode == OpCodes.Call)
                            .Select(i => i.Operand)
                            .OfType<MethodReference>()
                            .Any(m => IsIlType(m.DeclaringType));
        }

        public void Process()
        {
            try
            {
                ProcessImpl();
            }
            catch (WeavingException ex)
            {
                throw new WeavingException($"Error processing method {_method.FullName}: {ex.Message}");
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Unexpected error occured while processing method {_method.FullName}: {ex.Message}", ex);
            }
        }

        private static bool IsIlType(TypeReference type)
            => type.Name == KnownNames.Short.IlType && type.FullName == KnownNames.Full.IlType;

        private void ProcessImpl()
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
                        case KnownNames.Short.OpMethod:
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

            if (operandType.FullName == KnownNames.Full.TypeReferenceType)
            {
                var typeRef = ConsumeArgTypeRef(args[1]);
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

        private string ConsumeArgString(Instruction instruction)
        {
            if (instruction.OpCode != OpCodes.Ldstr)
                throw new WeavingException($"Invalid instruction, expected a constant string, but was {instruction}");

            _il.Remove(instruction);
            return (string)instruction.Operand;
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

            throw new WeavingException($"Invalid instruction, expected a constant, but was {instruction}");
        }

        private TypeReference ConsumeArgTypeRef(Instruction instruction)
        {
            if (instruction.OpCode.FlowControl != FlowControl.Call || !(instruction.Operand is MethodReference method))
                throw new WeavingException("Invalid opcode, expected a call");

            switch (method.FullName)
            {
                case "System.Type System.Type::GetTypeFromHandle(System.RuntimeTypeHandle)":
                {
                    var ldToken = instruction.GetArgumentPushInstructions().Single();
                    if (ldToken.OpCode != OpCodes.Ldtoken)
                        throw new WeavingException("Invalid operand, expected ldtoken");

                    _il.Remove(ldToken);
                    _il.Remove(instruction);
                    return (TypeReference)ldToken.Operand;
                }

                case "InlineIL.TypeReference InlineIL.TypeReference::op_Implicit(System.Type)":
                case "System.Void InlineIL.TypeReference::.ctor(System.Type)":
                {
                    var innerTypeRef = ConsumeArgTypeRef(instruction.GetArgumentPushInstructions().Single());
                    _il.Remove(instruction);
                    return innerTypeRef;
                }

                case "System.Void InlineIL.TypeReference::.ctor(System.String,System.String)":
                {
                    var args = instruction.GetArgumentPushInstructions();
                    var assemblyName = ConsumeArgString(args[0]);
                    var typeName = ConsumeArgString(args[1]);

                    var assembly = assemblyName == _module.Assembly.Name.Name
                        ? _module.Assembly
                        : _module.AssemblyResolver.Resolve(new AssemblyNameReference(assemblyName, null));

                    if (assembly == null)
                        throw new WeavingException($"Could not resolve assembly {assemblyName}");

                    var typeReference = assembly.Modules.Select(m => m.GetType(typeName, true)).FirstOrDefault();
                    if (typeReference == null)
                        throw new WeavingException($"Could not find type {typeName} in assembly {assemblyName}");

                    _il.Remove(instruction);
                    return _module.ImportReference(typeReference);
                }

                case "InlineIL.TypeReference InlineIL.TypeReference::ToPointer()":
                {
                    var innerTypeRef = ConsumeArgTypeRef(instruction.GetArgumentPushInstructions().Single());
                    _il.Remove(instruction);
                    return innerTypeRef.MakePointerType();
                }

                case "InlineIL.TypeReference InlineIL.TypeReference::ToReference()":
                {
                    var innerTypeRef = ConsumeArgTypeRef(instruction.GetArgumentPushInstructions().Single());
                    _il.Remove(instruction);
                    return innerTypeRef.MakeByReferenceType();
                }

                case "InlineIL.TypeReference InlineIL.TypeReference::ToArray()":
                {
                    var innerTypeRef = ConsumeArgTypeRef(instruction.GetArgumentPushInstructions().Single());
                    _il.Remove(instruction);
                    return innerTypeRef.MakeArrayType();
                }
            }

            throw new WeavingException($"Invalid operand, expected a type reference at {instruction}");
        }
    }
}
