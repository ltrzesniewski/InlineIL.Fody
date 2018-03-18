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
        private readonly List<SequencePoint> _sequencePoints;
        private readonly Dictionary<string, LabelInfo> _labels = new Dictionary<string, LabelInfo>();
        private readonly Dictionary<string, VariableDefinition> _locals = new Dictionary<string, VariableDefinition>();

        private Collection<Instruction> Instructions => _method.Body.Instructions;

        public MethodWeaver(ModuleDefinition module, MethodDefinition method)
        {
            _module = module;
            _method = method;
            _il = _method.Body.GetILProcessor();
            _sequencePoints = _method.DebugInformation.SequencePoints.ToList();
        }

        public static bool NeedsProcessing(MethodDefinition method)
            => GetLibReferencingInstruction(method) != null;

        private static Instruction GetLibReferencingInstruction(MethodDefinition method)
        {
            if (!method.HasBody)
                return null;

            return method.Body
                         .Instructions
                         .FirstOrDefault(
                             i => i.OpCode.FlowControl == FlowControl.Call
                                  && i.Operand is MethodReference opMethod
                                  && KnownNames.Full.AllTypes.Contains(opMethod.DeclaringType.FullName)
                                  ||
                                  i.Operand is TypeReference typeRef
                                  && KnownNames.Full.AllTypes.Contains(typeRef.FullName)
                         );
        }

        public void Process()
        {
            try
            {
                ProcessImpl();
            }
            catch (InstructionWeavingException ex)
            {
                var message = ex.Message.Contains(_method.FullName)
                    ? ex.Message
                    : ex.Instruction != null
                        ? $"Error in {_method.FullName} at instruction {ex.Instruction}: {ex.Message}"
                        : $"Error in {_method.FullName}: {ex.Message}";

                throw new SequencePointWeavingException(_sequencePoints.LastOrDefault(sp => sp.Offset <= ex.Instruction?.Offset), message);
            }
            catch (WeavingException ex)
            {
                throw new WeavingException($"Error in {_method.FullName}: {ex.Message}");
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Unexpected error occured while processing method {_method.FullName}: {ex.Message}", ex);
            }
        }

        private void ProcessImpl()
        {
            _method.Body.SimplifyMacros();

            ProcessMethodCalls();
            ProcessLabels();
            ValidateMethod();

            _method.Body.OptimizeMacros();
        }

        private void ProcessMethodCalls()
        {
            var instruction = Instructions.FirstOrDefault();

            while (instruction != null)
            {
                var nextInstruction = instruction.Next;

                if (instruction.OpCode == OpCodes.Call
                    && instruction.Operand is MethodReference calledMethod
                    && calledMethod.DeclaringType.FullName == KnownNames.Full.IlType)
                {
                    try
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

                            case KnownNames.Short.DeclareLocalMethod:
                                ProcessDeclareLocalMethod(instruction);
                                break;

                            default:
                                throw new InstructionWeavingException(instruction, $"Unsupported method: {calledMethod.FullName}");
                        }
                    }
                    catch (InstructionWeavingException)
                    {
                        throw;
                    }
                    catch (WeavingException ex)
                    {
                        throw new InstructionWeavingException(instruction, $"Error in {_method.FullName} at instruction {instruction}: {ex.Message}");
                    }
                    catch (Exception ex)
                    {
                        throw new InstructionWeavingException(instruction, $"Unexpected error occured while processing method {_method.FullName} at instruction {instruction}: {ex}");
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
                    throw new InstructionWeavingException(info.References.FirstOrDefault(), $"Undefined label: {name}");

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
                                throw new InstructionWeavingException(labelRef, "Unexpected branch target");

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
                            throw new InstructionWeavingException(labelRef, "Invalid branch instruction");
                    }
                }
            }

            _labels.Clear();
        }

        private void ValidateMethod()
        {
            var libReferencingInstruction = GetLibReferencingInstruction(_method);
            if (libReferencingInstruction != null)
                throw new InstructionWeavingException(libReferencingInstruction, "Unconsumed reference to InlineIL");
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
                    var labelInfo = ConsumeArgLabelRef(args[1]);
                    var resultInstruction = InstructionHelper.Create(opCode, labelInfo.PlaceholderTarget);
                    labelInfo.References.Add(resultInstruction);
                    _il.Replace(instruction, resultInstruction);
                    return;
                }

                case KnownNames.Full.LabelRefType + "[]":
                {
                    var labelInfos = ConsumeArgArray(args[1], ConsumeArgLabelRef).ToList();
                    var resultInstruction = InstructionHelper.Create(opCode, labelInfos.Select(i => i.PlaceholderTarget).ToArray());

                    foreach (var info in labelInfos)
                        info.References.Add(resultInstruction);

                    _il.Replace(instruction, resultInstruction);
                    return;
                }

                case KnownNames.Full.LocalRefType:
                {
                    var variableDef = ConsumeArgLocalRef(args[1]);
                    _il.Replace(instruction, InstructionHelper.Create(opCode, variableDef));
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
                throw new InstructionWeavingException(instruction, "The Unreachable method should be used along with the throw keyword: throw IL.Unreachable();");

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
                    throw new InstructionWeavingException(instruction, "The Return method should be used along the return keyword: return IL.Return<T>();");
            }

            _il.Remove(instruction);
        }

        private void ProcessMarkLabelMethod(Instruction instruction)
        {
            var labelName = ConsumeArgString(instruction.GetArgumentPushInstructions().Single());
            var labelInfo = _labels.GetOrAddNew(labelName);

            if (labelInfo.IsDefined)
                throw new InstructionWeavingException(instruction, $"Label {labelName} is already defined");

            _il.Replace(instruction, labelInfo.PlaceholderTarget);
        }

        private void ProcessDeclareLocalMethod(Instruction instruction)
        {
            var args = instruction.GetArgumentPushInstructions();
            var localName = ConsumeArgString(args[0]);
            var localType = ConsumeArgTypeRef(args[1]);
            var pinned = args.Length == 3 && ConsumeArgBool(args[2]);

            if (_locals.ContainsKey(localName))
                throw new InstructionWeavingException(instruction, $"Local {localName} is already defined");

            var variableDef = new VariableDefinition(pinned ? localType.MakePinnedType() : localType);
            _il.Body.Variables.Add(variableDef);
            _locals.Add(localName, variableDef);

            _il.Remove(instruction);
        }

        private OpCode ConsumeArgOpCode(Instruction instruction)
        {
            _il.Remove(instruction);
            return OpCodeMap.FromLdsfld(instruction);
        }

        private string ConsumeArgString(Instruction instruction)
        {
            if (instruction.OpCode != OpCodes.Ldstr)
                throw UnexpectedInstruction(instruction, OpCodes.Ldstr);

            _il.Remove(instruction);
            return (string)instruction.Operand;
        }

        private int ConsumeArgInt32(Instruction instruction)
        {
            var value = ConsumeArgConst(instruction);
            if (value is int intValue)
                return intValue;

            throw UnexpectedInstruction(instruction, OpCodes.Ldc_I4);
        }

        private bool ConsumeArgBool(Instruction instruction)
            => ConsumeArgInt32(instruction) != 0;

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

            throw UnexpectedInstruction(instruction, "a constant");
        }

        private TypeReference ConsumeArgTypeRef(Instruction instruction)
        {
            if (instruction.OpCode.FlowControl != FlowControl.Call || !(instruction.Operand is MethodReference method))
                throw UnexpectedInstruction(instruction, "a method call");

            switch (method.FullName)
            {
                case "System.Type System.Type::GetTypeFromHandle(System.RuntimeTypeHandle)":
                {
                    var ldToken = instruction.GetArgumentPushInstructions().Single();
                    if (ldToken.OpCode != OpCodes.Ldtoken)
                        throw UnexpectedInstruction(ldToken, OpCodes.Ldtoken);

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
                        throw new InstructionWeavingException(instruction, $"Could not resolve assembly {assemblyName}");

                    var typeReference = assembly.Modules
                                                .Select(module =>
                                                {
                                                    var parsedType = module.GetType(typeName, true);

                                                    // parsedType is not null when the type doesn't exist in the current version of Cecil
                                                    return parsedType != null ? module.GetType(parsedType.FullName) : null;
                                                })
                                                .FirstOrDefault(t => t != null);

                    if (typeReference == null)
                        throw new InstructionWeavingException(instruction, $"Could not find type {typeName} in assembly {assemblyName}");

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
                    if (rank < 1)
                        throw new InstructionWeavingException(args[1], $"Invalid array rank: {rank}, must be at least 1");

                    _il.Remove(instruction);
                    return innerTypeRef.MakeArrayType(rank);
                }

                case "InlineIL.TypeRef InlineIL.TypeRef::MakeGenericType(InlineIL.TypeRef[])":
                case "System.Type System.Type::MakeGenericType(System.Type[])":
                {
                    var args = instruction.GetArgumentPushInstructions();
                    var genericType = _module.ImportReference(ConsumeArgTypeRef(args[0]).ResolveRequiredType());
                    var genericArgs = ConsumeArgArray(args[1], ConsumeArgTypeRef);

                    if (!genericType.HasGenericParameters)
                        throw new InstructionWeavingException(instruction, $"Not a generic type: {genericType.FullName}");

                    if (genericArgs.Length == 0)
                        throw new InstructionWeavingException(instruction, "No generic arguments supplied");

                    if (genericType.GenericParameters.Count != genericArgs.Length)
                        throw new InstructionWeavingException(instruction, $"Incorrect number of generic arguments supplied for type {genericType.FullName} - expected {genericType.GenericParameters.Count}, but got {genericArgs.Length}");

                    _il.Remove(instruction);
                    return genericType.MakeGenericInstanceType(genericArgs);
                }
            }

            throw UnexpectedInstruction(instruction, "a type reference");
        }

        private MethodReference ConsumeArgMethodRef(Instruction instruction)
        {
            if (instruction.OpCode.FlowControl != FlowControl.Call || !(instruction.Operand is MethodReference method))
                throw UnexpectedInstruction(instruction, "a method call");

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
                            throw new InstructionWeavingException(instruction, $"Method {methodName} not found in type {typeDef.FullName}");

                        case 1:
                            _il.Remove(instruction);
                            return _module.ImportReference(methods.Single());

                        default:
                            throw new InstructionWeavingException(instruction, $"Ambiguous method {methodName} in type {typeDef.FullName}");
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
                            throw new InstructionWeavingException(instruction, $"Method {methodName}({string.Join(", ", paramTypes.Select(p => p.FullName))}) not found in type {typeDef.FullName}");

                        case 1:
                            _il.Remove(instruction);
                            return _module.ImportReference(methods.Single());

                        default:
                            // This should never happen
                            throw new InstructionWeavingException(instruction, $"Ambiguous method {methodName}({string.Join(", ", paramTypes.Select(p => p.FullName))}) in type {typeDef.FullName}");
                    }
                }

                case "InlineIL.MethodRef InlineIL.MethodRef::MakeGenericMethod(InlineIL.TypeRef[])":
                {
                    var args = instruction.GetArgumentPushInstructions();
                    var genericMethod = ConsumeArgMethodRef(args[0]);
                    var genericArgs = ConsumeArgArray(args[1], ConsumeArgTypeRef);

                    if (!genericMethod.HasGenericParameters)
                        throw new InstructionWeavingException(instruction, $"Not a generic method: {genericMethod.FullName}");

                    if (genericArgs.Length == 0)
                        throw new InstructionWeavingException(instruction, "No generic arguments supplied");

                    if (genericMethod.GenericParameters.Count != genericArgs.Length)
                        throw new InstructionWeavingException(instruction, $"Incorrect number of generic arguments supplied for method {genericMethod.FullName} - expected {genericMethod.GenericParameters.Count}, but got {genericArgs.Length}");

                    var genericInstance = new GenericInstanceMethod(genericMethod);
                    genericInstance.GenericArguments.AddRange(genericArgs);

                    _il.Remove(instruction);
                    return genericInstance;
                }

                case "InlineIL.MethodRef InlineIL.MethodRef::WithOptionalParameters(InlineIL.TypeRef[])":
                {
                    var args = instruction.GetArgumentPushInstructions();
                    var baseMethod = ConsumeArgMethodRef(args[0]);
                    var optionalParamTypes = ConsumeArgArray(args[1], ConsumeArgTypeRef);

                    if (baseMethod.CallingConvention != MethodCallingConvention.VarArg)
                        throw new InstructionWeavingException(instruction, $"Not a vararg method: {baseMethod.FullName}");

                    if (baseMethod.Parameters.Any(p => p.ParameterType.IsSentinel))
                        throw new InstructionWeavingException(instruction, "Optional parameters for vararg call have already been supplied");

                    if (optionalParamTypes.Length == 0)
                        throw new InstructionWeavingException(instruction, "No optional parameter type supplied for vararg method call");

                    var methodRef = new MethodReference(baseMethod.Name, baseMethod.ReturnType, baseMethod.DeclaringType)
                    {
                        CallingConvention = MethodCallingConvention.VarArg,
                        HasThis = baseMethod.HasThis,
                        ExplicitThis = baseMethod.ExplicitThis,
                        MethodReturnType = baseMethod.MethodReturnType
                    };

                    foreach (var param in baseMethod.Parameters)
                        methodRef.Parameters.Add(param);

                    for (var i = 0; i < optionalParamTypes.Length; ++i)
                    {
                        var paramType = optionalParamTypes[i];
                        if (i == 0)
                            paramType = paramType.MakeSentinelType();

                        methodRef.Parameters.Add(new ParameterDefinition(paramType));
                    }

                    _il.Remove(instruction);
                    return _module.ImportReference(methodRef);
                }
            }

            throw UnexpectedInstruction(instruction, "a method reference");
        }

        private FieldReference ConsumeArgFieldRef(Instruction instruction)
        {
            if (instruction.OpCode != OpCodes.Newobj || !(instruction.Operand is MethodReference ctor) || ctor.FullName != "System.Void InlineIL.FieldRef::.ctor(InlineIL.TypeRef,System.String)")
                throw UnexpectedInstruction(instruction, "newobj FieldRef");

            var args = instruction.GetArgumentPushInstructions();
            var typeDef = ConsumeArgTypeRef(args[0]).ResolveRequiredType();
            var fieldName = ConsumeArgString(args[1]);

            var fields = typeDef.Fields.Where(f => f.Name == fieldName).ToList();

            switch (fields.Count)
            {
                case 0:
                    throw new InstructionWeavingException(instruction, $"Field {fieldName} not found in type {typeDef.FullName}");

                case 1:
                    _il.Remove(instruction);
                    return fields.Single();

                default:
                    // This should never happen
                    throw new InstructionWeavingException(instruction, $"Ambiguous field {fieldName} in type {typeDef.FullName}");
            }
        }

        private T[] ConsumeArgArray<T>(Instruction instruction, Func<Instruction, T> consumeItem)
        {
            if (instruction.OpCode == OpCodes.Call)
            {
                if (!(instruction.Operand is MethodReference method) || method.GetElementMethod().FullName != "!!0[] System.Array::Empty()")
                    throw UnexpectedInstruction(instruction, "newarr or call to Array.Empty");

                _il.Remove(instruction);
                return Array.Empty<T>();
            }

            if (instruction.OpCode != OpCodes.Newarr)
                throw UnexpectedInstruction(instruction, "newarr or call to Array.Empty");

            var newarrInstruction = instruction;

            var countInstruction = newarrInstruction.PrevSkipNops();
            if (countInstruction.OpCode != OpCodes.Ldc_I4)
                throw UnexpectedInstruction(countInstruction, OpCodes.Ldc_I4);

            var count = (int)countInstruction.Operand;
            var args = new T[count];

            var currentDupInstruction = newarrInstruction.NextSkipNops();

            for (var index = 0; index < count; ++index)
            {
                var dupInstruction = currentDupInstruction;
                if (dupInstruction.OpCode != OpCodes.Dup)
                    throw UnexpectedInstruction(dupInstruction, OpCodes.Dup);

                var indexInstruction = dupInstruction.NextSkipNops();
                if (indexInstruction.OpCode != OpCodes.Ldc_I4)
                    throw UnexpectedInstruction(indexInstruction, OpCodes.Ldc_I4);

                if ((int)indexInstruction.Operand != index)
                    throw UnexpectedInstruction(indexInstruction, $"ldc.i4 with value of {index}");

                var stelemInstruction = dupInstruction.GetValueConsumingInstruction();
                if (!stelemInstruction.OpCode.IsStelem())
                    throw UnexpectedInstruction(stelemInstruction, "stelem");

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

        private LabelInfo ConsumeArgLabelRef(Instruction instruction)
        {
            if (instruction.OpCode != OpCodes.Newobj || !(instruction.Operand is MethodReference ctor) || ctor.FullName != "System.Void InlineIL.LabelRef::.ctor(System.String)")
                throw UnexpectedInstruction(instruction, "newobj LabelRef");

            var labelName = ConsumeArgString(instruction.GetArgumentPushInstructions().Single());
            _il.Remove(instruction);

            return _labels.GetOrAddNew(labelName);
        }

        private VariableDefinition ConsumeArgLocalRef(Instruction instruction)
        {
            if (instruction.OpCode != OpCodes.Newobj || !(instruction.Operand is MethodReference ctor) || ctor.FullName != "System.Void InlineIL.LocalRef::.ctor(System.String)")
                throw UnexpectedInstruction(instruction, "newobj LocalRef");

            var localName = ConsumeArgString(instruction.GetArgumentPushInstructions().Single());

            if (!_locals.TryGetValue(localName, out var variableDef))
                throw new InstructionWeavingException(instruction, $"Local {localName} is not defined");

            _il.Remove(instruction);

            return variableDef;
        }

        private InstructionWeavingException UnexpectedInstruction(Instruction instruction, OpCode expectedOpcode)
            => UnexpectedInstruction(instruction, expectedOpcode.Name);

        private InstructionWeavingException UnexpectedInstruction(Instruction instruction, string expected)
            => new InstructionWeavingException(instruction, $"Error in {_method.FullName}: Unexpected instruction, expected {expected} but was: {instruction}");

        private class LabelInfo
        {
            public Instruction PlaceholderTarget { get; } = Instruction.Create(OpCodes.Nop);
            public ICollection<Instruction> References { get; } = new List<Instruction>();

            public bool IsDefined => PlaceholderTarget.Next != null;
        }
    }
}
