using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using Fody;
using InlineIL.Fody.Processing;
using InlineIL.Fody.Support;
using JetBrains.Annotations;
using Mono.Cecil;
using Mono.Cecil.Cil;
using Mono.Cecil.Rocks;
using TypeSystem = Mono.Cecil.TypeSystem;

namespace InlineIL.Fody.Extensions
{
    internal static class CecilExtensions
    {
        [NotNull]
        public static TypeDefinition ResolveRequiredType(this TypeReference typeRef)
        {
            try
            {
                var typeDef = typeRef.Resolve();

                if (typeDef == null)
                    throw new WeavingException($"Could not resolve type {typeRef.FullName}");

                return typeDef;
            }
            catch (Exception ex)
            {
                throw new WeavingException($"Could not resolve type {typeRef.FullName}: {ex.Message}");
            }
        }

        public static MethodReference Clone(this MethodReference method)
        {
            var clone = new MethodReference(method.Name, method.ReturnType, method.DeclaringType)
            {
                HasThis = method.HasThis,
                ExplicitThis = method.ExplicitThis,
                CallingConvention = method.CallingConvention
            };

            foreach (var param in method.Parameters)
                clone.Parameters.Add(new ParameterDefinition(param.Name, param.Attributes, param.ParameterType));

            foreach (var param in method.GenericParameters)
                clone.GenericParameters.Add(new GenericParameter(param.Name, clone));

            return clone;
        }

        public static MethodReference MakeGeneric(this MethodReference method, TypeReference declaringType)
        {
            if (!declaringType.IsGenericInstance || method.DeclaringType.IsGenericInstance)
                return method;

            var result = method.Clone();
            result.DeclaringType = result.DeclaringType.MakeGenericInstanceType(((GenericInstanceType)declaringType).GenericArguments.ToArray());
            return result;
        }

        [CanBeNull]
        public static Instruction PrevSkipNops(this Instruction instruction)
        {
            instruction = instruction?.Previous;

            while (instruction != null && instruction.OpCode == OpCodes.Nop)
                instruction = instruction.Previous;

            return instruction;
        }

        [ContractAnnotation("null => null; notnull => notnull")]
        public static Instruction SkipNops(this Instruction instruction)
        {
            while (instruction != null && instruction.OpCode == OpCodes.Nop)
                instruction = instruction.Next;

            return instruction;
        }

        [CanBeNull]
        public static Instruction NextSkipNops(this Instruction instruction)
            => instruction?.Next?.SkipNops();

        [NotNull]
        public static Instruction GetValueConsumingInstruction(this Instruction instruction)
        {
            var stackSize = 0;

            while (true)
            {
                stackSize += GetPushCount(instruction);

                instruction = instruction.Next;
                if (instruction == null)
                    throw new WeavingException("Unexpected end of method");

                stackSize -= GetPopCount(instruction);

                if (stackSize <= 0)
                    return instruction;
            }
        }

        [NotNull]
        public static Instruction[] GetArgumentPushInstructions(this Instruction instruction)
        {
            if (instruction.OpCode.FlowControl != FlowControl.Call)
                throw new InstructionWeavingException(instruction, "Expected a call instruction");

            var method = (IMethodSignature)instruction.Operand;
            var argCount = GetArgCount(instruction.OpCode, method);

            if (argCount == 0)
                return Array.Empty<Instruction>();

            var result = new Instruction[argCount];
            var currentInstruction = instruction.Previous;

            for (var paramIndex = result.Length - 1; paramIndex >= 0; --paramIndex)
                result[paramIndex] = BackwardScanPush(ref currentInstruction);

            return result;
        }

        private static Instruction BackwardScanPush(ref Instruction currentInstruction)
        {
            var startInstruction = currentInstruction;
            Instruction result = null;
            var stackToConsume = 1;

            while (stackToConsume > 0)
            {
                switch (currentInstruction.OpCode.FlowControl)
                {
                    case FlowControl.Branch:
                    case FlowControl.Cond_Branch:
                    case FlowControl.Return:
                    case FlowControl.Throw:
                        throw new InstructionWeavingException(startInstruction, $"Could not locate call argument due to {currentInstruction}");

                    case FlowControl.Call:
                        if (currentInstruction.OpCode == OpCodes.Jmp)
                            throw new InstructionWeavingException(startInstruction, $"Could not locate call argument due to {currentInstruction}");
                        break;
                }

                var popCount = GetPopCount(currentInstruction);
                var pushCount = GetPushCount(currentInstruction);

                stackToConsume -= pushCount;

                if (stackToConsume == 0 && result == null)
                    result = currentInstruction;

                if (stackToConsume < 0)
                    throw new InstructionWeavingException(startInstruction, $"Could not locate call argument due to {currentInstruction} which pops an unexpected number of items from the stack");

                stackToConsume += popCount;
                currentInstruction = currentInstruction.Previous;
            }

            if (result == null)
                throw new InstructionWeavingException(startInstruction, "Could not locate call argument, reached beginning of method");

            return result;
        }

        private static int GetArgCount(OpCode opCode, IMethodSignature method)
        {
            var argCount = method.Parameters.Count;

            if (method.HasThis && !method.ExplicitThis && opCode.Code != Code.Newobj)
                ++argCount;

            if (opCode.Code == Code.Calli)
                ++argCount;

            return argCount;
        }

        public static int GetPopCount(this Instruction instruction)
        {
            if (instruction.OpCode.FlowControl == FlowControl.Call)
                return GetArgCount(instruction.OpCode, (IMethodSignature)instruction.Operand);

            if (instruction.OpCode == OpCodes.Dup)
                return 0;

            switch (instruction.OpCode.StackBehaviourPop)
            {
                case StackBehaviour.Pop0:
                    return 0;

                case StackBehaviour.Popi:
                case StackBehaviour.Popref:
                case StackBehaviour.Pop1:
                    return 1;

                case StackBehaviour.Pop1_pop1:
                case StackBehaviour.Popi_pop1:
                case StackBehaviour.Popi_popi:
                case StackBehaviour.Popi_popi8:
                case StackBehaviour.Popi_popr4:
                case StackBehaviour.Popi_popr8:
                case StackBehaviour.Popref_pop1:
                case StackBehaviour.Popref_popi:
                    return 2;

                case StackBehaviour.Popi_popi_popi:
                case StackBehaviour.Popref_popi_popi:
                case StackBehaviour.Popref_popi_popi8:
                case StackBehaviour.Popref_popi_popr4:
                case StackBehaviour.Popref_popi_popr8:
                case StackBehaviour.Popref_popi_popref:
                    return 3;

                case StackBehaviour.PopAll:
                    throw new InstructionWeavingException(instruction, "Unexpected stack-clearing instruction encountered");

                default:
                    throw new InstructionWeavingException(instruction, "Could not locate method argument value");
            }
        }

        public static int GetPushCount(this Instruction instruction)
        {
            if (instruction.OpCode.FlowControl == FlowControl.Call)
            {
                var method = (IMethodSignature)instruction.Operand;
                return method.ReturnType.MetadataType != MetadataType.Void || instruction.OpCode.Code == Code.Newobj ? 1 : 0;
            }

            if (instruction.OpCode == OpCodes.Dup)
                return 1;

            switch (instruction.OpCode.StackBehaviourPush)
            {
                case StackBehaviour.Push0:
                    return 0;

                case StackBehaviour.Push1:
                case StackBehaviour.Pushi:
                case StackBehaviour.Pushi8:
                case StackBehaviour.Pushr4:
                case StackBehaviour.Pushr8:
                case StackBehaviour.Pushref:
                    return 1;

                case StackBehaviour.Push1_push1:
                    return 2;

                default:
                    throw new InstructionWeavingException(instruction, "Could not locate method argument value");
            }
        }

        public static bool IsStelem(this OpCode opCode)
        {
            switch (opCode.Code)
            {
                case Code.Stelem_Any:
                case Code.Stelem_I:
                case Code.Stelem_I1:
                case Code.Stelem_I2:
                case Code.Stelem_I4:
                case Code.Stelem_I8:
                case Code.Stelem_R4:
                case Code.Stelem_R8:
                case Code.Stelem_Ref:
                    return true;

                default:
                    return false;
            }
        }

        public static MethodCallingConvention ToMethodCallingConvention(this CallingConvention callingConvention)
        {
            switch (callingConvention)
            {
                case CallingConvention.Cdecl:
                    return MethodCallingConvention.C;

                case CallingConvention.StdCall:
                case CallingConvention.Winapi:
                    return MethodCallingConvention.StdCall;

                case CallingConvention.FastCall:
                    return MethodCallingConvention.FastCall;

                case CallingConvention.ThisCall:
                    return MethodCallingConvention.ThisCall;

                default:
                    throw new WeavingException("Invalid calling convention");
            }
        }

        public static IEnumerable<Instruction> GetInstructions(this ExceptionHandler handler)
        {
            if (handler.TryStart != null)
                yield return handler.TryStart;

            if (handler.TryEnd != null)
                yield return handler.TryEnd;

            if (handler.FilterStart != null)
                yield return handler.FilterStart;

            if (handler.HandlerStart != null)
                yield return handler.HandlerStart;

            if (handler.HandlerEnd != null)
                yield return handler.HandlerEnd;
        }

        public static TypeSystem GetTypeSystem(this ModuleDefinition module)
        {
            // No way, I really need *that* one

#pragma warning disable 618
            return module.TypeSystem;
#pragma warning restore 618
        }

        public static bool IsDebugBuild(this ModuleDefinition module)
        {
            const string attributeName = "System.Diagnostics.DebuggableAttribute";
            const string enumName = attributeName + "/DebuggingModes";

            var debuggableAttribute = module.Assembly.CustomAttributes.FirstOrDefault(i => i.AttributeType.FullName == attributeName)
                                      ?? module.CustomAttributes.FirstOrDefault(i => i.AttributeType.FullName == attributeName);

            if (debuggableAttribute == null)
                return false;

            var args = debuggableAttribute.ConstructorArguments;

            switch (args.Count)
            {
                case 1 when args[0].Type.FullName == enumName && args[0].Value is int intValue:
                    return ((DebuggableAttribute.DebuggingModes)intValue & DebuggableAttribute.DebuggingModes.DisableOptimizations) != 0;

                case 2 when args[0].Value is bool && args[1].Value is bool isJitOptimizerDisabled:
                    return isJitOptimizerDisabled;

                default:
                    return false;
            }
        }

        [ContractAnnotation("null => false")]
        public static bool IsInlineILTypeUsage([CanBeNull] this TypeReference type)
        {
            switch (type)
            {
                case null:
                    return false;

                case GenericInstanceType t:
                    return t.ElementType.IsInlineILTypeUsage()
                           || t.GenericParameters.Any(i => i.IsInlineILTypeUsage())
                           || t.GenericArguments.Any(i => i.IsInlineILTypeUsage());

                case GenericParameter t:
                    return t.HasConstraints && t.Constraints.Any(c => c.IsInlineILTypeUsage())
                           || t.HasCustomAttributes && t.CustomAttributes.Any(i => i.IsInlineILTypeUsage());

                case IModifierType t:
                    return t.ElementType.IsInlineILTypeUsage()
                           || t.ModifierType.IsInlineILTypeUsage();

                case FunctionPointerType t:
                    return ((IMethodSignature)t).IsInlineILTypeUsage();

                case TypeSpecification t:
                    return t.ElementType.IsInlineILTypeUsage();

                default:
                    return KnownNames.Full.AllTypes.Contains(type.FullName);
            }
        }

        [ContractAnnotation("null => false")]
        public static bool IsInlineILTypeUsage([CanBeNull] this IMethodSignature method)
        {
            if (method == null)
                return false;

            if (method.ReturnType.IsInlineILTypeUsage() || method.HasParameters && method.Parameters.Any(i => i.IsInlineILTypeUsage()))
                return true;

            if (method is MethodReference methodRef && methodRef.DeclaringType.IsInlineILTypeUsage())
                return true;

            if (method is IGenericInstance genericInstance && genericInstance.HasGenericArguments && genericInstance.GenericArguments.Any(i => i.IsInlineILTypeUsage()))
                return true;

            if (method is IGenericParameterProvider generic && generic.HasGenericParameters && generic.GenericParameters.Any(i => i.IsInlineILTypeUsage()))
                return true;

            return false;
        }

        [ContractAnnotation("null => false")]
        public static bool IsInlineILTypeUsage([CanBeNull] this FieldReference fieldRef)
        {
            if (fieldRef == null)
                return false;

            return fieldRef.DeclaringType.IsInlineILTypeUsage() || fieldRef.FieldType.IsInlineILTypeUsage();
        }

        [ContractAnnotation("null => false")]
        public static bool IsInlineILTypeUsage([CanBeNull] this ParameterDefinition paramDef)
        {
            if (paramDef == null)
                return false;

            if (paramDef.ParameterType.IsInlineILTypeUsage())
                return true;

            if (paramDef.HasCustomAttributes && paramDef.CustomAttributes.Any(i => i.IsInlineILTypeUsage()))
                return true;

            return false;
        }

        [ContractAnnotation("null => false")]
        public static bool IsInlineILTypeUsage([CanBeNull] this CustomAttribute attr)
        {
            if (attr == null)
                return false;

            if (attr.AttributeType.IsInlineILTypeUsage())
                return true;

            if (attr.HasConstructorArguments && attr.ConstructorArguments.Any(i => i.Value is TypeReference typeRef && typeRef.IsInlineILTypeUsage()))
                return true;

            if (attr.HasProperties && attr.Properties.Any(i => i.Argument.Value is TypeReference typeRef && typeRef.IsInlineILTypeUsage()))
                return true;

            return false;
        }

        [ContractAnnotation("null => false")]
        public static bool IsInlineILAssembly([CanBeNull] this AssemblyNameReference assembly)
            => assembly?.Name == "InlineIL";
    }
}
