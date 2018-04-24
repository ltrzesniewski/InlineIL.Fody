using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using Fody;
using JetBrains.Annotations;
using Mono.Cecil;
using Mono.Cecil.Cil;
using Mono.Cecil.Rocks;

namespace InlineIL.Fody
{
    internal class MethodWeaver
    {
        private readonly ModuleDefinition _module;
        private readonly MethodDefinition _method;
        private readonly WeaverILProcessor _il;
        private readonly List<SequencePoint> _sequencePoints;
        private readonly Dictionary<string, LabelInfo> _labels = new Dictionary<string, LabelInfo>();

        private IEnumerable<Instruction> Instructions => _method.Body.Instructions;

        public MethodWeaver(ModuleDefinition module, MethodDefinition method)
        {
            _module = module;
            _method = method;
            _il = new WeaverILProcessor(_method);
            _sequencePoints = _method.DebugInformation.SequencePoints.ToList();
        }

        public static bool NeedsProcessing(MethodDefinition method)
            => GetLibReferencingInstruction(method) != null;

        [CanBeNull]
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
                        ? $"{ex.Message} (in {_method.FullName} at instruction {ex.Instruction})"
                        : $"{ex.Message} (in {_method.FullName})";

                throw new WeavingException(message)
                {
                    SequencePoint = _sequencePoints.LastOrDefault(sp => sp.Offset <= ex.Instruction?.Offset)
                };
            }
            catch (WeavingException ex)
            {
                throw new WeavingException($"{ex.Message} (in {_method.FullName})")
                {
                    SequencePoint = ex.SequencePoint
                };
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Unexpected error occured while processing method {_method.FullName}: {ex.Message}", ex);
            }
        }

        private void ProcessImpl()
        {
            _method.Body.SimplifyMacros();
            _method.Body.InitLocals = true; // Force fat method header

            ValidateBeforeProcessing();
            ProcessMethodCalls();
            ProcessLabels();
            ValidateAfterProcessing();

            _method.Body.OptimizeMacros();
        }

        private void ValidateBeforeProcessing()
        {
            foreach (var instruction in Instructions)
            {
                if (instruction.OpCode == OpCodes.Call
                    && instruction.Operand is MethodReference calledMethod
                    && calledMethod.DeclaringType.FullName == KnownNames.Full.IlType)
                {
                    try
                    {
                        switch (calledMethod.Name)
                        {
                            case KnownNames.Short.PushMethod:
                                ValidatePushMethod(instruction);
                                break;
                        }
                    }
                    catch (InstructionWeavingException)
                    {
                        throw;
                    }
                    catch (WeavingException ex)
                    {
                        throw new InstructionWeavingException(instruction, $"{ex.Message} (in {_method.FullName} at instruction {instruction})");
                    }
                    catch (Exception ex)
                    {
                        throw new InstructionWeavingException(instruction, $"Unexpected error occured while processing method {_method.FullName} at instruction {instruction}: {ex}");
                    }
                }
            }
        }

        private void ProcessMethodCalls()
        {
            var instruction = Instructions.FirstOrDefault();

            while (instruction != null)
            {
                var nextInstruction = instruction.Next;

                if (instruction.OpCode == OpCodes.Call && instruction.Operand is MethodReference calledMethod)
                {
                    try
                    {
                        switch (calledMethod.DeclaringType.FullName)
                        {
                            case KnownNames.Full.IlType:
                                ProcessIlMethodCall(instruction, out nextInstruction);
                                break;

                            case KnownNames.Full.IlEmitType:
                                ProcessIlEmitMethodCall(instruction, out nextInstruction);
                                break;
                        }
                    }
                    catch (InstructionWeavingException)
                    {
                        throw;
                    }
                    catch (WeavingException ex)
                    {
                        throw new InstructionWeavingException(instruction, $"{ex.Message} (in {_method.FullName} at instruction {instruction})");
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
                    throw new InstructionWeavingException(info.References.FirstOrDefault(), $"Undefined label: '{name}'");

                _il.Remove(info.PlaceholderTarget);
            }

            _labels.Clear();
        }

        private void ValidateAfterProcessing()
        {
            var libReferencingInstruction = GetLibReferencingInstruction(_method);
            if (libReferencingInstruction != null)
                throw new InstructionWeavingException(libReferencingInstruction, "Unconsumed reference to InlineIL");

            var invalidRefs = _il.GetAllReferencedInstructions().Except(Instructions).ToList();
            if (invalidRefs.Any())
                throw new WeavingException($"Found invalid references to instructions:{Environment.NewLine}{string.Join(Environment.NewLine, invalidRefs)}");
        }

        private void ProcessIlMethodCall(Instruction instruction, out Instruction nextInstruction)
        {
            var calledMethod = (MethodReference)instruction.Operand;
            nextInstruction = instruction.Next;

            switch (calledMethod.Name)
            {
                case KnownNames.Short.PushMethod:
                    ProcessPushMethod(instruction);
                    break;

                case KnownNames.Short.UnreachableMethod:
                    ProcessUnreachableMethod(instruction, out nextInstruction);
                    break;

                case KnownNames.Short.ReturnMethod:
                    ProcessReturnMethod(instruction);
                    break;

                case KnownNames.Short.MarkLabelMethod:
                    ProcessMarkLabelMethod(instruction);
                    break;

                case KnownNames.Short.DeclareLocalsMethod:
                    ProcessDeclareLocalsMethod(instruction);
                    break;

                default:
                    throw new InstructionWeavingException(instruction, $"Unsupported method: {calledMethod.FullName}");
            }
        }

        private void ProcessIlEmitMethodCall(Instruction emitCallInstruction, out Instruction nextInstruction)
        {
            var emittedInstruction = CreateInstructionToEmit();
            _il.Replace(emitCallInstruction, emittedInstruction);

            if (emittedInstruction.OpCode.OpCodeType == OpCodeType.Prefix)
                _il.RemoveNopsAfter(emittedInstruction);

            nextInstruction = emittedInstruction.Next;

            Instruction CreateInstructionToEmit()
            {
                var method = (MethodReference)emitCallInstruction.Operand;
                var opCode = OpCodeMap.FromCecilFieldName(method.Name);
                var args = emitCallInstruction.GetArgumentPushInstructions();

                switch (opCode.OperandType)
                {
                    case OperandType.InlineNone:
                        if (args.Length != 0)
                            throw new InstructionWeavingException(emitCallInstruction, "Unexpected operand argument");

                        return _il.Create(opCode);

                    case OperandType.InlineI:
                    case OperandType.ShortInlineI:
                    case OperandType.InlineI8:
                    case OperandType.InlineR:
                    case OperandType.ShortInlineR:
                        return _il.CreateConst(opCode, ConsumeArgConst(args.Single()));

                    case OperandType.InlineString:
                        return _il.CreateConst(opCode, ConsumeArgString(args.Single()));

                    case OperandType.InlineType:
                        return _il.Create(opCode, ConsumeArgTypeRef(args.Single()));

                    case OperandType.InlineMethod:
                        return _il.Create(opCode, ConsumeArgMethodRef(args.Single()));

                    case OperandType.InlineField:
                        return _il.Create(opCode, ConsumeArgFieldRef(args.Single()));

                    case OperandType.InlineTok:
                    {
                        switch (method.Parameters[0].ParameterType.FullName)
                        {
                            case KnownNames.Full.TypeRefType:
                                return _il.Create(opCode, ConsumeArgTypeRef(args.Single()));

                            case KnownNames.Full.MethodRefType:
                                return _il.Create(opCode, ConsumeArgMethodRef(args.Single()));

                            case KnownNames.Full.FieldRefType:
                                return _il.Create(opCode, ConsumeArgFieldRef(args.Single()));

                            default:
                                throw new InstructionWeavingException(emitCallInstruction, $"Unexpected argument type: {method.Parameters[0].ParameterType.FullName}");
                        }
                    }

                    case OperandType.InlineBrTarget:
                    case OperandType.ShortInlineBrTarget:
                    {
                        var labelInfo = ConsumeArgLabelRef(args.Single());
                        var resultInstruction = _il.Create(opCode, labelInfo.PlaceholderTarget);
                        labelInfo.References.Add(resultInstruction);
                        return resultInstruction;
                    }

                    case OperandType.InlineSwitch:
                    {
                        var labelInfos = ConsumeArgArray(args.Single(), ConsumeArgLabelRef).ToList();
                        var resultInstruction = _il.Create(opCode, labelInfos.Select(i => i.PlaceholderTarget).ToArray());

                        foreach (var info in labelInfos)
                            info.References.Add(resultInstruction);

                        return resultInstruction;
                    }

                    case OperandType.InlineVar:
                    case OperandType.ShortInlineVar:
                    {
                        switch (method.Parameters[0].ParameterType.FullName)
                        {
                            case "System.String":
                                return _il.Create(opCode, ConsumeArgLocalRef(args.Single()));

                            case "System.UInt16":
                            case "System.UInt32":
                                return _il.CreateConst(opCode, ConsumeArgConst(args.Single()));

                            default:
                                throw new InstructionWeavingException(emitCallInstruction, $"Unexpected argument type: {method.Parameters[0].ParameterType.FullName}");
                        }
                    }

                    case OperandType.InlineArg:
                    case OperandType.ShortInlineArg:
                    {
                        switch (method.Parameters[0].ParameterType.FullName)
                        {
                            case "System.String":
                                return _il.CreateConst(opCode, ConsumeArgParamName(args.Single()));

                            case "System.Byte":
                            case "System.UInt16":
                                return _il.CreateConst(opCode, ConsumeArgConst(args.Single()));

                            default:
                                throw new InstructionWeavingException(emitCallInstruction, $"Unexpected argument type: {method.Parameters[0].ParameterType.FullName}");
                        }
                    }

                    case OperandType.InlineSig:
                        return _il.Create(opCode, ConsumeArgCallSite(args.Single()));

                    default:
                        throw new NotSupportedException($"Unsupported operand type: {opCode.OperandType}");
                }
            }
        }

        private void ValidatePushMethod(Instruction instruction)
        {
            if (_method.Body.ExceptionHandlers.Any(h => h.HandlerType == ExceptionHandlerType.Catch && h.HandlerStart == instruction
                                                        || h.HandlerType == ExceptionHandlerType.Filter && (h.FilterStart == instruction || h.HandlerStart == instruction)))
                return;

            var args = instruction.GetArgumentPushInstructions();
            var prevInstruction = instruction.PrevSkipNops();

            if (args[0] != prevInstruction)
                throw new InstructionWeavingException(instruction, "IL.Push cannot be used in this context, as the instruction which supplies its argument does not immediately precede the call");
        }

        private void ProcessPushMethod(Instruction instruction)
        {
            _il.Remove(instruction);
        }

        private void ProcessUnreachableMethod(Instruction instruction, out Instruction nextInstruction)
        {
            var throwInstruction = instruction.NextSkipNops();
            if (throwInstruction?.OpCode != OpCodes.Throw)
                throw new InstructionWeavingException(instruction, "The result of the IL.Unreachable method should be immediately thrown: throw IL.Unreachable();");

            _il.Remove(instruction);
            _il.RemoveNopsAround(throwInstruction);
            nextInstruction = throwInstruction.Next;
            _il.Remove(throwInstruction);
        }

        private void ProcessReturnMethod(Instruction instruction)
        {
            ValidateReturnMethod();

            _il.Remove(instruction);

            void ValidateReturnMethod()
            {
                var currentInstruction = instruction.NextSkipNops();

                while (true)
                {
                    switch (currentInstruction?.OpCode.Code)
                    {
                        case Code.Ret:
                            return;

                        case Code.Stloc:
                        {
                            var localIndex = ((VariableReference)currentInstruction.Operand).Index;
                            var branchInstruction = currentInstruction.NextSkipNops();

                            switch (branchInstruction?.OpCode.Code)
                            {
                                case Code.Br: // Debug builds
                                case Code.Leave: // try/catch blocks
                                {
                                    if (branchInstruction.Operand is Instruction branchTarget)
                                    {
                                        branchTarget = branchTarget.SkipNops();

                                        if (branchTarget.OpCode == OpCodes.Ldloc && ((VariableReference)branchTarget.Operand).Index == localIndex)
                                            return;
                                    }

                                    break;
                                }
                            }

                            throw InvalidReturnException();
                        }

                        default:
                        {
                            // Allow implicit conversions
                            if (currentInstruction != null
                                && (currentInstruction.OpCode.FlowControl == FlowControl.Next
                                    || currentInstruction.OpCode.FlowControl == FlowControl.Call)
                                && currentInstruction.GetPopCount() == 1
                                && currentInstruction.GetPushCount() == 1
                            )
                            {
                                currentInstruction = currentInstruction.NextSkipNops();
                                continue;
                            }

                            throw InvalidReturnException();
                        }
                    }
                }

                Exception InvalidReturnException() => new InstructionWeavingException(instruction, "The result of the IL.Return method should be immediately returned: return IL.Return<T>();");
            }
        }

        private void ProcessMarkLabelMethod(Instruction instruction)
        {
            var labelName = ConsumeArgString(instruction.GetArgumentPushInstructions().Single());
            var labelInfo = _labels.GetOrAddNew(labelName);

            if (labelInfo.IsDefined)
                throw new InstructionWeavingException(instruction, $"Label '{labelName}' is already defined");

            _il.Replace(instruction, labelInfo.PlaceholderTarget);
        }

        private void ProcessDeclareLocalsMethod(Instruction instruction)
        {
            var method = (MethodReference)instruction.Operand;

            switch (method.FullName)
            {
                case "System.Void InlineIL.IL::DeclareLocals(InlineIL.LocalVar[])":
                {
                    var args = instruction.GetArgumentPushInstructions();
                    _il.DeclareLocals(ConsumeArgArray(args[0], ConsumeArgLocalVar));
                    _il.Remove(instruction);
                    return;
                }

                case "System.Void InlineIL.IL::DeclareLocals(System.Boolean,InlineIL.LocalVar[])":
                {
                    var args = instruction.GetArgumentPushInstructions();
                    _method.Body.InitLocals = ConsumeArgBool(args[0]);
                    _il.DeclareLocals(ConsumeArgArray(args[1], ConsumeArgLocalVar));
                    _il.Remove(instruction);
                    return;
                }

                default:
                    throw UnexpectedInstruction(instruction, "a InlineIL.DeclareLocals method call");
            }
        }

        [NotNull]
        private string ConsumeArgString(Instruction instruction)
        {
            if (instruction.OpCode == OpCodes.Ldstr)
            {
                _il.Remove(instruction);
                return (string)instruction.Operand;
            }

            if (instruction.OpCode.FlowControl == FlowControl.Call && instruction.Operand is MethodReference method)
            {
                switch (method.FullName)
                {
                    case "System.String InlineIL.TypeRef::get_CoreLibrary()":
                    {
                        _il.Remove(instruction);
                        return _module.TypeSystem.CoreLibrary.Name;
                    }
                }
            }

            throw UnexpectedInstruction(instruction, OpCodes.Ldstr);
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

            throw UnexpectedInstruction(instruction, "a constant value");
        }

        [NotNull]
        private TypeReference ConsumeArgTypeRef(Instruction typeRefInstruction)
        {
            return ConsumeArgTypeRefBuilder(typeRefInstruction).Build();

            TypeRefBuilder ConsumeArgTypeRefBuilder(Instruction instruction)
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

                        var builder = new TypeRefBuilder(_module, (TypeReference)ldToken.Operand);

                        _il.Remove(ldToken);
                        _il.Remove(instruction);
                        return builder;
                    }

                    case "InlineIL.TypeRef InlineIL.TypeRef::op_Implicit(System.Type)":
                    case "System.Void InlineIL.TypeRef::.ctor(System.Type)":
                    {
                        var builder = ConsumeArgTypeRefBuilder(instruction.GetArgumentPushInstructions().Single());

                        _il.Remove(instruction);
                        return builder;
                    }

                    case "System.Void InlineIL.TypeRef::.ctor(System.String,System.String)":
                    {
                        var args = instruction.GetArgumentPushInstructions();
                        var assemblyName = ConsumeArgString(args[0]);
                        var typeName = ConsumeArgString(args[1]);
                        var builder = new TypeRefBuilder(_module, assemblyName, typeName);

                        _il.Remove(instruction);
                        return builder;
                    }

                    case "InlineIL.TypeRef InlineIL.TypeRef::MakePointerType()":
                    case "System.Type System.Type::MakePointerType()":
                    {
                        var builder = ConsumeArgTypeRefBuilder(instruction.GetArgumentPushInstructions().Single());
                        builder.MakePointerType();

                        _il.Remove(instruction);
                        return builder;
                    }

                    case "InlineIL.TypeRef InlineIL.TypeRef::MakeByRefType()":
                    case "System.Type System.Type::MakeByRefType()":
                    {
                        var builder = ConsumeArgTypeRefBuilder(instruction.GetArgumentPushInstructions().Single());
                        builder.MakeByRefType();

                        _il.Remove(instruction);
                        return builder;
                    }

                    case "InlineIL.TypeRef InlineIL.TypeRef::MakeArrayType()":
                    case "System.Type System.Type::MakeArrayType()":
                    {
                        var builder = ConsumeArgTypeRefBuilder(instruction.GetArgumentPushInstructions().Single());
                        builder.MakeArrayType(1);

                        _il.Remove(instruction);
                        return builder;
                    }

                    case "InlineIL.TypeRef InlineIL.TypeRef::MakeArrayType(System.Int32)":
                    case "System.Type System.Type::MakeArrayType(System.Int32)":
                    {
                        var args = instruction.GetArgumentPushInstructions();
                        var builder = ConsumeArgTypeRefBuilder(args[0]);
                        var rank = ConsumeArgInt32(args[1]);
                        builder.MakeArrayType(rank);

                        _il.Remove(instruction);
                        return builder;
                    }

                    case "InlineIL.TypeRef InlineIL.TypeRef::MakeGenericType(InlineIL.TypeRef[])":
                    case "System.Type System.Type::MakeGenericType(System.Type[])":
                    {
                        var args = instruction.GetArgumentPushInstructions();
                        var builder = ConsumeArgTypeRefBuilder(args[0]);
                        var genericArgs = ConsumeArgArray(args[1], ConsumeArgTypeRef);
                        builder.MakeGenericType(genericArgs);

                        _il.Remove(instruction);
                        return builder;
                    }

                    case "InlineIL.TypeRef InlineIL.TypeRef::WithOptionalModifier(InlineIL.TypeRef)":
                    {
                        var args = instruction.GetArgumentPushInstructions();
                        var builder = ConsumeArgTypeRefBuilder(args[0]);
                        var modifierType = ConsumeArgTypeRef(args[1]);
                        builder.AddOptionalModifier(modifierType);

                        _il.Remove(instruction);
                        return builder;
                    }

                    case "InlineIL.TypeRef InlineIL.TypeRef::WithRequiredModifier(InlineIL.TypeRef)":
                    {
                        var args = instruction.GetArgumentPushInstructions();
                        var builder = ConsumeArgTypeRefBuilder(args[0]);
                        var modifierType = ConsumeArgTypeRef(args[1]);
                        builder.AddRequiredModifier(modifierType);

                        _il.Remove(instruction);
                        return builder;
                    }

                    default:
                        throw UnexpectedInstruction(instruction, "a type reference");
                }
            }
        }

        [NotNull]
        private MethodReference ConsumeArgMethodRef(Instruction methodRefInstruction)
        {
            return ConsumeArgMethodRefBuilder(methodRefInstruction).Build();

            MethodRefBuilder ConsumeArgMethodRefBuilder(Instruction instruction)
            {
                if (instruction.OpCode.FlowControl != FlowControl.Call || !(instruction.Operand is MethodReference method))
                    throw UnexpectedInstruction(instruction, "a method call");

                switch (method.FullName)
                {
                    case "System.Void InlineIL.MethodRef::.ctor(InlineIL.TypeRef,System.String)":
                    {
                        var args = instruction.GetArgumentPushInstructions();
                        var typeRef = ConsumeArgTypeRef(args[0]);
                        var methodName = ConsumeArgString(args[1]);
                        var builder = new MethodRefBuilder(_module, typeRef, methodName);

                        _il.Remove(instruction);
                        return builder;
                    }

                    case "System.Void InlineIL.MethodRef::.ctor(InlineIL.TypeRef,System.String,InlineIL.TypeRef[])":
                    {
                        var args = instruction.GetArgumentPushInstructions();
                        var typeRef = ConsumeArgTypeRef(args[0]);
                        var methodName = ConsumeArgString(args[1]);
                        var paramTypes = ConsumeArgArray(args[2], ConsumeArgTypeRef);
                        var builder = new MethodRefBuilder(_module, typeRef, methodName, paramTypes);

                        _il.Remove(instruction);
                        return builder;
                    }

                    case "InlineIL.MethodRef InlineIL.MethodRef::MakeGenericMethod(InlineIL.TypeRef[])":
                    {
                        var args = instruction.GetArgumentPushInstructions();
                        var builder = ConsumeArgMethodRefBuilder(args[0]);
                        var genericArgs = ConsumeArgArray(args[1], ConsumeArgTypeRef);
                        builder.MakeGenericMethod(genericArgs);

                        _il.Remove(instruction);
                        return builder;
                    }

                    case "InlineIL.MethodRef InlineIL.MethodRef::WithOptionalParameters(InlineIL.TypeRef[])":
                    {
                        var args = instruction.GetArgumentPushInstructions();
                        var builder = ConsumeArgMethodRefBuilder(args[0]);
                        var optionalParamTypes = ConsumeArgArray(args[1], ConsumeArgTypeRef);
                        builder.SetOptionalParameters(optionalParamTypes);

                        _il.Remove(instruction);
                        return builder;
                    }

                    default:
                        throw UnexpectedInstruction(instruction, "a method reference");
                }
            }
        }

        [NotNull]
        private FieldReference ConsumeArgFieldRef(Instruction fieldRefInstruction)
        {
            return ConsumeArgFieldRefBuilder(fieldRefInstruction).Build();

            FieldRefBuilder ConsumeArgFieldRefBuilder(Instruction instruction)
            {
                if (instruction.OpCode != OpCodes.Newobj || !(instruction.Operand is MethodReference ctor) || ctor.FullName != "System.Void InlineIL.FieldRef::.ctor(InlineIL.TypeRef,System.String)")
                    throw UnexpectedInstruction(instruction, "newobj FieldRef");

                var args = instruction.GetArgumentPushInstructions();
                var typeRef = ConsumeArgTypeRef(args[0]);
                var fieldName = ConsumeArgString(args[1]);
                var builder = new FieldRefBuilder(typeRef, fieldName);

                _il.Remove(instruction);
                return builder;
            }
        }

        [NotNull]
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
            if (countInstruction?.OpCode != OpCodes.Ldc_I4)
                throw UnexpectedInstruction(countInstruction, OpCodes.Ldc_I4);

            var count = (int)countInstruction.Operand;
            var args = new T[count];

            var currentDupInstruction = newarrInstruction.NextSkipNops();

            for (var index = 0; index < count; ++index)
            {
                var dupInstruction = currentDupInstruction;
                if (dupInstruction?.OpCode != OpCodes.Dup)
                    throw UnexpectedInstruction(dupInstruction, OpCodes.Dup);

                var indexInstruction = dupInstruction.NextSkipNops();
                if (indexInstruction?.OpCode != OpCodes.Ldc_I4)
                    throw UnexpectedInstruction(indexInstruction, OpCodes.Ldc_I4);

                if ((int?)indexInstruction?.Operand != index)
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

        [NotNull]
        private LabelInfo ConsumeArgLabelRef(Instruction instruction)
            => _labels.GetOrAddNew(ConsumeArgString(instruction));

        [NotNull]
        private MethodLocals.NamedLocal ConsumeArgLocalVar(Instruction instruction)
        {
            if (instruction.OpCode.FlowControl != FlowControl.Call || !(instruction.Operand is MethodReference method))
                throw UnexpectedInstruction(instruction, "a call on LocalVar");

            switch (method.FullName)
            {
                case "System.Void InlineIL.LocalVar::.ctor(System.String,InlineIL.TypeRef)":
                {
                    var args = instruction.GetArgumentPushInstructions();
                    var name = ConsumeArgString(args[0]);
                    var type = ConsumeArgTypeRef(args[1]);

                    _il.Remove(instruction);
                    return new MethodLocals.NamedLocal(name, new VariableDefinition(type));
                }

                case "System.Void InlineIL.LocalVar::.ctor(InlineIL.TypeRef)":
                case "InlineIL.LocalVar InlineIL.LocalVar::op_Implicit(System.Type)":
                {
                    var args = instruction.GetArgumentPushInstructions();
                    var type = ConsumeArgTypeRef(args[0]);

                    _il.Remove(instruction);
                    return new MethodLocals.NamedLocal(null, new VariableDefinition(type));
                }

                case "InlineIL.LocalVar InlineIL.LocalVar::Pinned()":
                {
                    var args = instruction.GetArgumentPushInstructions();
                    var innerDefinition = ConsumeArgLocalVar(args[0]);

                    if (innerDefinition.Definition.IsPinned)
                        throw new InstructionWeavingException(instruction, $"Local '{innerDefinition.Name}' is already pinned");

                    innerDefinition.Definition.VariableType = innerDefinition.Definition.VariableType.MakePinnedType();

                    _il.Remove(instruction);
                    return innerDefinition;
                }

                default:
                    throw UnexpectedInstruction(instruction, "a method call on LocalVar");
            }
        }

        [NotNull]
        private VariableDefinition ConsumeArgLocalRef(Instruction instruction)
        {
            var localName = ConsumeArgString(instruction);

            if (_il.Locals == null)
                throw new InstructionWeavingException(instruction, $"Local '{localName}' is not defined, call IL.DeclareLocals");

            var variableDef = _il.Locals.TryGetByName(localName);
            if (variableDef == null)
                throw new InstructionWeavingException(instruction, $"Local '{localName}' is not defined");

            return variableDef;
        }

        private int ConsumeArgParamName(Instruction instruction)
        {
            var paramName = ConsumeArgString(instruction);

            var paramIndex = _method.Parameters.IndexOfFirst(p => p.Name == paramName);
            if (paramIndex < 0)
                throw new InstructionWeavingException(instruction, $"Parameter '{paramName}' is not defined");

            if (_method.HasThis && !_method.ExplicitThis)
                ++paramIndex;

            return paramIndex;
        }

        [NotNull]
        private CallSite ConsumeArgCallSite(Instruction instruction)
        {
            if (instruction.OpCode.FlowControl != FlowControl.Call || !(instruction.Operand is MethodReference method))
                throw UnexpectedInstruction(instruction, "a method call");

            switch (method.FullName)
            {
                case "System.Void InlineIL.StandAloneMethodSig::.ctor(System.Reflection.CallingConventions,InlineIL.TypeRef,InlineIL.TypeRef[])":
                {
                    var args = instruction.GetArgumentPushInstructions();
                    var callingConvention = (CallingConventions)ConsumeArgInt32(args[0]);
                    var returnType = ConsumeArgTypeRef(args[1]);
                    var paramTypes = ConsumeArgArray(args[2], ConsumeArgTypeRef);

                    var callSite = new CallSite(returnType)
                    {
                        CallingConvention = (callingConvention & CallingConventions.VarArgs) == 0
                            ? MethodCallingConvention.Default
                            : MethodCallingConvention.VarArg,
                        HasThis = (callingConvention & CallingConventions.HasThis) != 0,
                        ExplicitThis = (callingConvention & CallingConventions.ExplicitThis) != 0
                    };

                    callSite.Parameters.AddRange(paramTypes.Select(t => new ParameterDefinition(t)));

                    _il.Remove(instruction);
                    return callSite;
                }

                case "InlineIL.StandAloneMethodSig InlineIL.StandAloneMethodSig::WithOptionalParameters(InlineIL.TypeRef[])":
                {
                    var args = instruction.GetArgumentPushInstructions();
                    var callSite = ConsumeArgCallSite(args[0]);
                    var optionalParamTypes = ConsumeArgArray(args[1], ConsumeArgTypeRef);

                    if (callSite.CallingConvention != MethodCallingConvention.VarArg)
                        throw new InstructionWeavingException(instruction, "Not a vararg calling convention");

                    if (callSite.Parameters.Any(p => p.ParameterType.IsSentinel))
                        throw new InstructionWeavingException(instruction, "Optional parameters for vararg call site have already been supplied");

                    if (optionalParamTypes.Length == 0)
                        throw new InstructionWeavingException(instruction, "No optional parameter type supplied for vararg call site");

                    for (var i = 0; i < optionalParamTypes.Length; ++i)
                    {
                        var paramType = optionalParamTypes[i];
                        if (i == 0)
                            paramType = paramType.MakeSentinelType();

                        callSite.Parameters.Add(new ParameterDefinition(paramType));
                    }

                    _il.Remove(instruction);
                    return callSite;
                }

                case "System.Void InlineIL.StandAloneMethodSig::.ctor(System.Runtime.InteropServices.CallingConvention,InlineIL.TypeRef,InlineIL.TypeRef[])":
                {
                    var args = instruction.GetArgumentPushInstructions();
                    var callingConvention = (CallingConvention)ConsumeArgInt32(args[0]);
                    var returnType = ConsumeArgTypeRef(args[1]);
                    var paramTypes = ConsumeArgArray(args[2], ConsumeArgTypeRef);

                    var callSite = new CallSite(returnType)
                    {
                        CallingConvention = callingConvention.ToMethodCallingConvention()
                    };

                    callSite.Parameters.AddRange(paramTypes.Select(t => new ParameterDefinition(t)));

                    _il.Remove(instruction);
                    return callSite;
                }

                default:
                    throw UnexpectedInstruction(instruction, "a stand-alone method signature");
            }
        }

        private static InstructionWeavingException UnexpectedInstruction([CanBeNull] Instruction instruction, OpCode expectedOpcode)
            => UnexpectedInstruction(instruction, expectedOpcode.Name);

        private static InstructionWeavingException UnexpectedInstruction([CanBeNull] Instruction instruction, string expected)
            => new InstructionWeavingException(instruction, $"Unexpected instruction, expected {expected} but was: {instruction}");

        private class LabelInfo
        {
            public Instruction PlaceholderTarget { get; } = Instruction.Create(OpCodes.Nop);
            public ICollection<Instruction> References { get; } = new List<Instruction>();

            public bool IsDefined => PlaceholderTarget.Next != null;
        }
    }
}
