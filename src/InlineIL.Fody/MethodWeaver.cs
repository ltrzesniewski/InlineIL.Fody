using System;
using System.Collections.Generic;
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
        private readonly Dictionary<string, LabelInfo> _labels = new Dictionary<string, LabelInfo>();

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

            ProcessMethodCalls();
            ProcessLabels();

            _method.Body.OptimizeMacros();
        }

        private void ProcessMethodCalls()
        {
            var instruction = Instructions.FirstOrDefault();

            while (instruction != null)
            {
                var nextInstruction = instruction.Next;

                if (instruction.OpCode == OpCodes.Call && instruction.Operand is MethodReference calledMethod && IsIlType(calledMethod.DeclaringType))
                {
                    switch (calledMethod.Name)
                    {
                        case KnownNames.Short.EmitMethod:
                            ProcessEmitMethod(instruction);
                            break;

                        case KnownNames.Short.PushMethod:
                            ProcessPushMethod(instruction);
                            break;

                        case KnownNames.Short.UnreachableMethod:
                            ProcessUnreachableMethod(instruction);
                            break;

                        case KnownNames.Short.ReturnMethod:
                            ProcessReturnMethod(instruction);
                            break;

                        case KnownNames.Short.MarkLabelMethod:
                            ProcessMarkLabelMethod(instruction);
                            break;

                        default:
                            throw new WeavingException($"Unsupported method: {calledMethod.FullName}");
                    }
                }

                instruction = nextInstruction;
            }
        }

        private void ProcessLabels()
        {
            foreach (var (name, info) in _labels)
            {
                if (!info.IsDefined)
                    throw new WeavingException($"Undefined label: {name}");

                var actualTarget = info.PlaceholderTarget.Next;
                _il.Remove(info.PlaceholderTarget);

                foreach (var labelRef in info.References)
                {
                    switch (labelRef.OpCode.OperandType)
                    {
                        case OperandType.InlineBrTarget:
                        case OperandType.ShortInlineBrTarget:
                        {
                            if (labelRef.Operand != info.PlaceholderTarget)
                                throw new WeavingException($"Unexpected branch target: {labelRef}");

                            labelRef.Operand = actualTarget;
                            break;
                        }

                        case OperandType.InlineSwitch:
                        {
                            var targets = (Instruction[])labelRef.Operand;
                            for (var i = 0; i < targets.Length; ++i)
                            {
                                if (targets[i] == info.PlaceholderTarget)
                                    targets[i] = actualTarget;
                            }

                            break;
                        }

                        default:
                            throw new WeavingException($"Invalid branch instruction: {labelRef}");
                    }
                }
            }

            _labels.Clear();
        }

        private void ProcessEmitMethod(Instruction instruction)
        {
            var method = (MethodReference)instruction.Operand;
            var methodParams = method.Parameters;

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

            switch (operandType.FullName)
            {
                case KnownNames.Full.TypeRefType:
                {
                    var typeRef = ConsumeArgTypeRef(args[1]);
                    _il.Replace(instruction, InstructionHelper.Create(opCode, typeRef));
                    return;
                }

                case KnownNames.Full.MethodRefType:
                {
                    var methodRef = ConsumeArgMethodRef(args[1]);
                    _il.Replace(instruction, InstructionHelper.Create(opCode, methodRef));
                    return;
                }

                case KnownNames.Full.FieldRefType:
                {
                    var fieldRef = ConsumeArgFieldRef(args[1]);
                    _il.Replace(instruction, InstructionHelper.Create(opCode, fieldRef));
                    return;
                }

                case KnownNames.Full.LabelRefType:
                {
                    var labelName = ConsumeArgLabelRef(args[1]);
                    var labelInfo = GetOrCreateLabelInfo(labelName);
                    var resultInstruction = InstructionHelper.Create(opCode, labelInfo.PlaceholderTarget);
                    labelInfo.References.Add(resultInstruction);
                    _il.Replace(instruction, resultInstruction);
                    return;
                }

                case KnownNames.Full.LabelRefType + "[]":
                {
                    var labelInfos = ConsumeArgArray(args[1], ConsumeArgLabelRef).Select(GetOrCreateLabelInfo).ToList();
                    var resultInstruction = InstructionHelper.Create(opCode, labelInfos.Select(i => i.PlaceholderTarget).ToArray());

                    foreach (var info in labelInfos)
                        info.References.Add(resultInstruction);

                    _il.Replace(instruction, resultInstruction);
                    return;
                }
            }

            throw new InvalidOperationException($"Unsupported IL.Emit overload: {method.FullName}");
        }

        private void ProcessPushMethod(Instruction instruction)
        {
            _il.Remove(instruction);
        }

        private void ProcessUnreachableMethod(Instruction instruction)
        {
            var throwInstruction = instruction.NextSkipNops();
            if (throwInstruction.OpCode != OpCodes.Throw)
                throw new WeavingException("The Unreachable method should be used along with the throw keyword: throw IL.Unreachable();");

            _il.Remove(instruction);
            _il.RemoveNopsAround(throwInstruction);
            _il.Remove(throwInstruction);
        }

        private void ProcessReturnMethod(Instruction instruction)
        {
            var nextInstruction = instruction.NextSkipNops();

            switch (nextInstruction.OpCode.Code)
            {
                case Code.Ret:
                    break;

                case Code.Stloc: // Debug builds
                {
                    var localIndex = ((VariableReference)nextInstruction.Operand).Index;
                    var branchInstruction = nextInstruction.NextSkipNops();
                    if (branchInstruction?.OpCode == OpCodes.Br && branchInstruction.Operand is Instruction branchTarget)
                    {
                        if (branchTarget.OpCode == OpCodes.Nop)
                            branchTarget = branchTarget.NextSkipNops();

                        if (branchTarget.OpCode == OpCodes.Ldloc && ((VariableReference)branchTarget.Operand).Index == localIndex)
                            break;
                    }

                    goto default;
                }

                default:
                    throw new WeavingException("The Return method should be used along the return keyword: return IL.Return<T>();");
            }

            _il.Remove(instruction);
        }

        private void ProcessMarkLabelMethod(Instruction instruction)
        {
            var labelName = ConsumeArgString(instruction.GetArgumentPushInstructions().Single());
            var labelInfo = GetOrCreateLabelInfo(labelName);

            if (labelInfo.IsDefined)
                throw new WeavingException($"Label {labelName} is already defined");

            _il.Replace(instruction, labelInfo.PlaceholderTarget);
        }

        private LabelInfo GetOrCreateLabelInfo(string labelName)
        {
            if (!_labels.TryGetValue(labelName, out var labelInfo))
            {
                labelInfo = new LabelInfo();
                _labels.Add(labelName, labelInfo);
            }

            return labelInfo;
        }

        private OpCode ConsumeArgOpCode(Instruction instruction)
        {
            _il.Remove(instruction);
            return OpCodeMap.FromLdsfld(instruction);
        }

        private string ConsumeArgString(Instruction instruction)
        {
            if (instruction.OpCode != OpCodes.Ldstr)
                throw new WeavingException($"Unexpected instruction, expected a constant string, but was {instruction}");

            _il.Remove(instruction);
            return (string)instruction.Operand;
        }

        private int ConsumeArgInt32(Instruction instruction)
        {
            var value = ConsumeArgConst(instruction);
            if (value is int intValue)
                return intValue;

            throw new WeavingException($"Unexpected instruction, expected a constant int, but was {instruction}");
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

            throw new WeavingException($"Unexpected instruction, expected a constant, but was {instruction}");
        }

        private TypeReference ConsumeArgTypeRef(Instruction instruction)
        {
            if (instruction.OpCode.FlowControl != FlowControl.Call || !(instruction.Operand is MethodReference method))
                throw new WeavingException($"Unexpected instruction, expected a call, but was {instruction}");

            switch (method.FullName)
            {
                case "System.Type System.Type::GetTypeFromHandle(System.RuntimeTypeHandle)":
                {
                    var ldToken = instruction.GetArgumentPushInstructions().Single();
                    if (ldToken.OpCode != OpCodes.Ldtoken)
                        throw new WeavingException($"Unexpected instruction, expected ldtoken, but was {ldToken}");

                    _il.Remove(ldToken);
                    _il.Remove(instruction);
                    return (TypeReference)ldToken.Operand;
                }

                case "InlineIL.TypeRef InlineIL.TypeRef::op_Implicit(System.Type)":
                case "System.Void InlineIL.TypeRef::.ctor(System.Type)":
                {
                    var innerTypeRef = ConsumeArgTypeRef(instruction.GetArgumentPushInstructions().Single());
                    _il.Remove(instruction);
                    return innerTypeRef;
                }

                case "System.Void InlineIL.TypeRef::.ctor(System.String,System.String)":
                {
                    var args = instruction.GetArgumentPushInstructions();
                    var assemblyName = ConsumeArgString(args[0]);
                    var typeName = ConsumeArgString(args[1]);

                    var assembly = assemblyName == _module.Assembly.Name.Name
                        ? _module.Assembly
                        : _module.AssemblyResolver.Resolve(new AssemblyNameReference(assemblyName, null));

                    if (assembly == null)
                        throw new WeavingException($"Could not resolve assembly {assemblyName}");

                    var typeReference = assembly.Modules
                                                .Select(module =>
                                                {
                                                    var parsedType = module.GetType(typeName, true);

                                                    // parsedType is not null when the type doesn't exist in the current version of Cecil
                                                    return parsedType != null ? module.GetType(parsedType.FullName) : null;
                                                })
                                                .FirstOrDefault(t => t != null);

                    if (typeReference == null)
                        throw new WeavingException($"Could not find type {typeName} in assembly {assemblyName}");

                    _il.Remove(instruction);
                    return _module.ImportReference(typeReference);
                }

                case "InlineIL.TypeRef InlineIL.TypeRef::MakePointerType()":
                case "System.Type System.Type::MakePointerType()":
                {
                    var innerTypeRef = ConsumeArgTypeRef(instruction.GetArgumentPushInstructions().Single());
                    _il.Remove(instruction);
                    return innerTypeRef.MakePointerType();
                }

                case "InlineIL.TypeRef InlineIL.TypeRef::MakeByRefType()":
                case "System.Type System.Type::MakeByRefType()":
                {
                    var innerTypeRef = ConsumeArgTypeRef(instruction.GetArgumentPushInstructions().Single());
                    _il.Remove(instruction);
                    return innerTypeRef.MakeByReferenceType();
                }

                case "InlineIL.TypeRef InlineIL.TypeRef::MakeArrayType()":
                case "System.Type System.Type::MakeArrayType()":
                {
                    var innerTypeRef = ConsumeArgTypeRef(instruction.GetArgumentPushInstructions().Single());
                    _il.Remove(instruction);
                    return innerTypeRef.MakeArrayType();
                }

                case "InlineIL.TypeRef InlineIL.TypeRef::MakeArrayType(System.Int32)":
                case "System.Type System.Type::MakeArrayType(System.Int32)":
                {
                    var args = instruction.GetArgumentPushInstructions();
                    var innerTypeRef = ConsumeArgTypeRef(args[0]);
                    var rank = ConsumeArgInt32(args[1]);
                    if (rank < 1 || rank > 32)
                        throw new WeavingException($"Invalid array rank: {rank}, must be between 1 and 32");

                    _il.Remove(instruction);
                    return innerTypeRef.MakeArrayType(rank);
                }

                case "InlineIL.TypeRef InlineIL.TypeRef::MakeGenericType(InlineIL.TypeRef[])":
                case "System.Type System.Type::MakeGenericType(System.Type[])":
                {
                    var args = instruction.GetArgumentPushInstructions();
                    var innerTypeRef = _module.ImportReference(ConsumeArgTypeRef(args[0]).ResolveRequiredType());
                    var typeArgs = ConsumeArgArray(args[1], ConsumeArgTypeRef);
                    _il.Remove(instruction);
                    return innerTypeRef.MakeGenericInstanceType(typeArgs);
                }
            }

            throw new WeavingException($"Invalid operand, expected a type reference at {instruction}");
        }

        private MethodReference ConsumeArgMethodRef(Instruction instruction)
        {
            if (instruction.OpCode.FlowControl != FlowControl.Call || !(instruction.Operand is MethodReference method))
                throw new WeavingException($"Unexpected instruction, expected a call, but was {instruction}");

            switch (method.FullName)
            {
                case "System.Void InlineIL.MethodRef::.ctor(InlineIL.TypeRef,System.String)":
                {
                    var args = instruction.GetArgumentPushInstructions();
                    var typeDef = ConsumeArgTypeRef(args[0]).ResolveRequiredType();
                    var methodName = ConsumeArgString(args[1]);

                    var methods = typeDef.Methods.Where(m => m.Name == methodName).ToList();
                    switch (methods.Count)
                    {
                        case 0:
                            throw new WeavingException($"Method {methodName} not found in type {typeDef.FullName}");

                        case 1:
                            _il.Remove(instruction);
                            return _module.ImportReference(methods.Single());

                        default:
                            throw new WeavingException($"Ambiguous method {methodName} in type {typeDef.FullName}");
                    }
                }

                case "System.Void InlineIL.MethodRef::.ctor(InlineIL.TypeRef,System.String,InlineIL.TypeRef[])":
                {
                    var args = instruction.GetArgumentPushInstructions();
                    var typeDef = ConsumeArgTypeRef(args[0]).ResolveRequiredType();
                    var methodName = ConsumeArgString(args[1]);
                    var paramTypes = ConsumeArgArray(args[2], ConsumeArgTypeRef);

                    var methods = typeDef.Methods
                                         .Where(m => m.Name == methodName
                                                     && m.Parameters.Count == paramTypes.Length
                                                     && m.Parameters.Select(p => p.ParameterType.FullName).SequenceEqual(paramTypes.Select(p => p.FullName)))
                                         .ToList();

                    switch (methods.Count)
                    {
                        case 0:
                            throw new WeavingException($"Method {methodName}({string.Join(", ", paramTypes.Select(p => p.FullName))}) not found in type {typeDef.FullName}");

                        case 1:
                            _il.Remove(instruction);
                            return _module.ImportReference(methods.Single());

                        default:
                            // This should never happen
                            throw new WeavingException($"Ambiguous method {methodName}({string.Join(", ", paramTypes.Select(p => p.FullName))}) in type {typeDef.FullName}");
                    }
                }
            }

            throw new WeavingException($"Invalid operand, expected a type reference at {instruction}");
        }

        private FieldReference ConsumeArgFieldRef(Instruction instruction)
        {
            if (instruction.OpCode != OpCodes.Newobj || !(instruction.Operand is MethodReference ctor) || ctor.FullName != "System.Void InlineIL.FieldRef::.ctor(InlineIL.TypeRef,System.String)")
                throw new WeavingException($"Unexpected instruction, expected newobj FieldRef, but was {instruction}");

            var args = instruction.GetArgumentPushInstructions();
            var typeDef = ConsumeArgTypeRef(args[0]).ResolveRequiredType();
            var fieldName = ConsumeArgString(args[1]);

            var fields = typeDef.Fields.Where(f => f.Name == fieldName).ToList();

            switch (fields.Count)
            {
                case 0:
                    throw new WeavingException($"Field {fieldName} not found in type {typeDef.FullName}");

                case 1:
                    _il.Remove(instruction);
                    return fields.Single();

                default:
                    // This should never happen
                    throw new WeavingException($"Ambiguous field {fieldName} in type {typeDef.FullName}");
            }
        }

        private T[] ConsumeArgArray<T>(Instruction instruction, Func<Instruction, T> consumeItem)
        {
            if (instruction.OpCode == OpCodes.Call)
            {
                if (!(instruction.Operand is MethodReference method) || method.GetElementMethod().FullName != "!!0[] System.Array::Empty()")
                    throw new WeavingException($"Unexpected instruction, expected newarr or call to Array.Empty, but was {instruction}");

                _il.Remove(instruction);
                return Array.Empty<T>();
            }

            if (instruction.OpCode != OpCodes.Newarr)
                throw new WeavingException($"Unexpected instruction, expected newarr or call to Array.Empty, but was {instruction}");

            var newarrInstruction = instruction;

            var countInstruction = newarrInstruction.PrevSkipNops();
            if (countInstruction.OpCode != OpCodes.Ldc_I4)
                throw new WeavingException($"Unexpected instruction, expected ldc.i4, but was {countInstruction}");

            var count = (int)countInstruction.Operand;
            var args = new T[count];

            var currentDupInstruction = newarrInstruction.NextSkipNops();

            for (var index = 0; index < count; ++index)
            {
                var dupInstruction = currentDupInstruction;
                if (dupInstruction.OpCode != OpCodes.Dup)
                    throw new WeavingException($"Unexpected instruction, expected dup, but was {dupInstruction}");

                var indexInstruction = dupInstruction.NextSkipNops();
                if (indexInstruction.OpCode != OpCodes.Ldc_I4)
                    throw new WeavingException($"Unexpected instruction, expected ldc.i4, but was {indexInstruction}");

                if ((int)indexInstruction.Operand != index)
                    throw new WeavingException($"Unexpected instruction, expected ldc.i4 with value of {index}, but was {indexInstruction}");

                var stelemInstruction = dupInstruction.GetValueConsumingInstruction();
                if (!stelemInstruction.OpCode.IsStelem())
                    throw new WeavingException($"Unexpected instruction, expected stelem, but was {stelemInstruction}");

                args[index] = consumeItem(stelemInstruction.PrevSkipNops());

                currentDupInstruction = stelemInstruction.NextSkipNops();

                _il.Remove(dupInstruction);
                _il.Remove(indexInstruction);
                _il.Remove(stelemInstruction);
            }

            _il.Remove(countInstruction);
            _il.Remove(newarrInstruction);

            return args;
        }

        private string ConsumeArgLabelRef(Instruction instruction)
        {
            if (instruction.OpCode != OpCodes.Newobj || !(instruction.Operand is MethodReference ctor) || ctor.FullName != "System.Void InlineIL.LabelRef::.ctor(System.String)")
                throw new WeavingException($"Unexpected instruction, expected newobj LabelRef, but was {instruction}");

            var labelName = ConsumeArgString(instruction.GetArgumentPushInstructions().Single());
            _il.Remove(instruction);

            return labelName;
        }

        private class LabelInfo
        {
            public Instruction PlaceholderTarget { get; } = Instruction.Create(OpCodes.Nop);
            public ICollection<Instruction> References { get; } = new List<Instruction>();

            public bool IsDefined => PlaceholderTarget.Next != null;
        }
    }
}
