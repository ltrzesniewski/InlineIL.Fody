using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using Fody;
using InlineIL.Fody.Extensions;
using InlineIL.Fody.Model;
using InlineIL.Fody.Support;
using Mono.Cecil;
using Mono.Cecil.Cil;

namespace InlineIL.Fody.Processing
{
    internal class ArgumentConsumer
    {
        private readonly WeaverILProcessor _il;

        private MethodDefinition Method => _il.Method;
        private ModuleDefinition Module => Method.Module;

        public ArgumentConsumer(WeaverILProcessor il)
        {
            _il = il;
        }

        public string ConsumeArgString(Instruction instruction)
        {
            switch (instruction.OpCode.Code)
            {
                case Code.Ldstr:
                    _il.Remove(instruction);
                    return (string)instruction.Operand;

                case Code.Ldnull:
                    throw new InstructionWeavingException(instruction, "A non-null string literal is expected");

                default:
                    throw UnexpectedInstruction(instruction, "a string literal");
            }
        }

        private int ConsumeArgInt32(Instruction instruction)
        {
            var value = ConsumeArgConst(instruction);
            if (value is int intValue)
                return intValue;

            throw UnexpectedInstruction(instruction, OpCodes.Ldc_I4);
        }

        private T ConsumeArgEnumInt32<T>(Instruction instruction)
            where T : struct, Enum
        {
            var boxedValue = (object)ConsumeArgInt32(instruction);

            if (!Enum.IsDefined(typeof(T), boxedValue))
                throw new InstructionWeavingException(instruction, $"Invalid enum value for {typeof(T).Name}");

            return (T)boxedValue;
        }

        public bool ConsumeArgBool(Instruction instruction)
            => ConsumeArgInt32(instruction) != 0;

        public object ConsumeArgConst(Instruction instruction)
        {
            if (!TryConsumeArgConst(instruction, out var result))
                throw UnexpectedInstruction(instruction, "a constant value");

            return result;
        }

        private bool TryConsumeArgConst(Instruction instruction, [MaybeNullWhen(false)] out object result)
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
                    result = instruction.Operand;
                    return true;
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
                    if (TryConsumeArgConst(instruction.PrevSkipNopsRequired(), out result))
                    {
                        _il.Remove(instruction);
                        return true;
                    }

                    goto default;

                default:
                    result = null;
                    return false;
            }
        }

        public TypeReference ConsumeArgTypeRef(Instruction typeRefInstruction)
            => ConsumeArgTypeRefBuilder(typeRefInstruction).Build();

        private TypeRefBuilder ConsumeArgTypeRefBuilder(Instruction instruction)
        {
            if (instruction.OpCode.FlowControl != FlowControl.Call || !(instruction.Operand is MethodReference method))
                throw UnexpectedInstruction(instruction, "a method call");

            switch (method.GetElementMethod().FullName)
            {
                case "System.Type System.Type::GetTypeFromHandle(System.RuntimeTypeHandle)":
                {
                    var ldToken = _il.GetArgumentPushInstructionsInSameBasicBlock(instruction).Single();
                    if (ldToken.OpCode != OpCodes.Ldtoken)
                        throw UnexpectedInstruction(ldToken, OpCodes.Ldtoken);

                    var builder = new TypeRefBuilder(Module, (TypeReference)ldToken.Operand);

                    _il.Remove(ldToken);
                    _il.Remove(instruction);
                    return builder;
                }

                case "InlineIL.TypeRef InlineIL.TypeRef::op_Implicit(System.Type)":
                case "InlineIL.TypeRef InlineIL.TypeRef::Type(System.Type)":
                case "System.Void InlineIL.TypeRef::.ctor(System.Type)":
                {
                    var builder = ConsumeArgTypeRefBuilder(_il.GetArgumentPushInstructionsInSameBasicBlock(instruction).Single());

                    _il.Remove(instruction);
                    return builder;
                }

                case "InlineIL.TypeRef InlineIL.TypeRef::Type()":
                {
                    var builder = new TypeRefBuilder(Module, ((GenericInstanceMethod)method).GenericArguments[0]);

                    _il.Remove(instruction);
                    return builder;
                }

                case "System.Void InlineIL.TypeRef::.ctor(System.String,System.String)":
                {
                    var args = _il.GetArgumentPushInstructionsInSameBasicBlock(instruction);
                    var assemblyName = ConsumeArgString(args[0]);
                    var typeName = ConsumeArgString(args[1]);
                    var builder = new TypeRefBuilder(Module, assemblyName, typeName);

                    _il.Remove(instruction);
                    return builder;
                }

                case "InlineIL.TypeRef InlineIL.GenericParameters::get_Item(System.Int32)":
                {
                    var args = _il.GetArgumentPushInstructionsInSameBasicBlock(instruction);
                    var genericParameterType = ConsumeArgGenericParameterType(args[0]);
                    var genericParameterIndex = ConsumeArgInt32(args[1]);
                    var builder = new TypeRefBuilder(Module, genericParameterType, genericParameterIndex);

                    _il.Remove(instruction);
                    return builder;
                }

                case "System.Type System.Type::MakeGenericMethodParameter(System.Int32)":
                {
                    var args = _il.GetArgumentPushInstructionsInSameBasicBlock(instruction);
                    var genericParameterIndex = ConsumeArgInt32(args[0]);
                    var builder = new TypeRefBuilder(Module, GenericParameterType.Method, genericParameterIndex);

                    _il.Remove(instruction);
                    return builder;
                }

                case "InlineIL.TypeRef InlineIL.TypeRef::MakePointerType()":
                case "System.Type System.Type::MakePointerType()":
                {
                    var builder = ConsumeArgTypeRefBuilder(_il.GetArgumentPushInstructionsInSameBasicBlock(instruction).Single());
                    builder.MakePointerType();

                    _il.Remove(instruction);
                    return builder;
                }

                case "InlineIL.TypeRef InlineIL.TypeRef::MakeByRefType()":
                case "System.Type System.Type::MakeByRefType()":
                {
                    var builder = ConsumeArgTypeRefBuilder(_il.GetArgumentPushInstructionsInSameBasicBlock(instruction).Single());
                    builder.MakeByRefType();

                    _il.Remove(instruction);
                    return builder;
                }

                case "InlineIL.TypeRef InlineIL.TypeRef::MakeArrayType()":
                case "System.Type System.Type::MakeArrayType()":
                {
                    var builder = ConsumeArgTypeRefBuilder(_il.GetArgumentPushInstructionsInSameBasicBlock(instruction).Single());
                    builder.MakeArrayType(1);

                    _il.Remove(instruction);
                    return builder;
                }

                case "InlineIL.TypeRef InlineIL.TypeRef::MakeArrayType(System.Int32)":
                case "System.Type System.Type::MakeArrayType(System.Int32)":
                {
                    var args = _il.GetArgumentPushInstructionsInSameBasicBlock(instruction);
                    var builder = ConsumeArgTypeRefBuilder(args[0]);
                    var rank = ConsumeArgInt32(args[1]);
                    builder.MakeArrayType(rank);

                    _il.Remove(instruction);
                    return builder;
                }

                case "InlineIL.TypeRef InlineIL.TypeRef::MakeGenericType(InlineIL.TypeRef[])":
                case "System.Type System.Type::MakeGenericType(System.Type[])":
                {
                    var args = _il.GetArgumentPushInstructionsInSameBasicBlock(instruction);
                    var builder = ConsumeArgTypeRefBuilder(args[0]);
                    var genericArgs = ConsumeArgArray(args[1], ConsumeArgTypeRefBuilder);
                    builder.MakeGenericType(genericArgs);

                    _il.Remove(instruction);
                    return builder;
                }

                case "InlineIL.TypeRef InlineIL.TypeRef::WithOptionalModifier(InlineIL.TypeRef)":
                {
                    var args = _il.GetArgumentPushInstructionsInSameBasicBlock(instruction);
                    var builder = ConsumeArgTypeRefBuilder(args[0]);
                    var modifierType = ConsumeArgTypeRef(args[1]);
                    builder.AddOptionalModifier(modifierType);

                    _il.Remove(instruction);
                    return builder;
                }

                case "InlineIL.TypeRef InlineIL.TypeRef::WithRequiredModifier(InlineIL.TypeRef)":
                {
                    var args = _il.GetArgumentPushInstructionsInSameBasicBlock(instruction);
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

        private GenericParameterType ConsumeArgGenericParameterType(Instruction instruction)
        {
            const string expectation = "a call to TypeRef.TypeGenericParameters or TypeRef.MethodGenericParameters";

            if (instruction.OpCode.FlowControl != FlowControl.Call || !(instruction.Operand is MethodReference method))
                throw UnexpectedInstruction(instruction, expectation);

            switch (method.FullName)
            {
                case "InlineIL.GenericParameters InlineIL.TypeRef::get_TypeGenericParameters()":
                {
                    _il.Remove(instruction);
                    return GenericParameterType.Type;
                }

                case "InlineIL.GenericParameters InlineIL.TypeRef::get_MethodGenericParameters()":
                {
                    _il.Remove(instruction);
                    return GenericParameterType.Method;
                }

                default:
                    throw UnexpectedInstruction(instruction, expectation);
            }
        }

        public MethodReference ConsumeArgMethodRef(Instruction methodRefInstruction)
        {
            return ConsumeArgMethodRefBuilder(methodRefInstruction).Build();

            MethodRefBuilder ConsumeArgMethodRefBuilder(Instruction instruction)
            {
                if (instruction.OpCode.FlowControl != FlowControl.Call || !(instruction.Operand is MethodReference method))
                    throw UnexpectedInstruction(instruction, "a method call");

                switch (method.GetElementMethod().FullName)
                {
                    case "System.Void InlineIL.MethodRef::.ctor(InlineIL.TypeRef,System.String,InlineIL.TypeRef[])":
                    case "InlineIL.MethodRef InlineIL.MethodRef::Method(InlineIL.TypeRef,System.String,InlineIL.TypeRef[])":
                    {
                        var args = _il.GetArgumentPushInstructionsInSameBasicBlock(instruction);
                        var typeRef = ConsumeArgTypeRef(args[0]);
                        var methodName = ConsumeArgString(args[1]);
                        var paramTypes = ConsumeArgArray(args[2], ConsumeArgTypeRefBuilder);
                        var builder = MethodRefBuilder.MethodByNameAndSignature(Module, typeRef, methodName, null, null, paramTypes);

                        _il.Remove(instruction);
                        return builder;
                    }

                    case "System.Void InlineIL.MethodRef::.ctor(InlineIL.TypeRef,System.String,System.Int32,InlineIL.TypeRef[])":
                    case "InlineIL.MethodRef InlineIL.MethodRef::Method(InlineIL.TypeRef,System.String,System.Int32,InlineIL.TypeRef[])":
                    {
                        var args = _il.GetArgumentPushInstructionsInSameBasicBlock(instruction);
                        var typeRef = ConsumeArgTypeRef(args[0]);
                        var methodName = ConsumeArgString(args[1]);
                        var genericParameterCount = ConsumeArgInt32(args[2]);
                        var paramTypes = ConsumeArgArray(args[3], ConsumeArgTypeRefBuilder);
                        var builder = MethodRefBuilder.MethodByNameAndSignature(Module, typeRef, methodName, genericParameterCount, null, paramTypes);

                        _il.Remove(instruction);
                        return builder;
                    }

                    case "System.Void InlineIL.MethodRef::.ctor(InlineIL.TypeRef,System.String,InlineIL.TypeRef,System.Int32,InlineIL.TypeRef[])":
                    case "InlineIL.MethodRef InlineIL.MethodRef::Method(InlineIL.TypeRef,System.String,InlineIL.TypeRef,System.Int32,InlineIL.TypeRef[])":
                    {
                        var args = _il.GetArgumentPushInstructionsInSameBasicBlock(instruction);
                        var typeRef = ConsumeArgTypeRef(args[0]);
                        var methodName = ConsumeArgString(args[1]);
                        var returnType = ConsumeArgTypeRefBuilder(args[2]);
                        var genericParameterCount = ConsumeArgInt32(args[3]);
                        var paramTypes = ConsumeArgArray(args[4], ConsumeArgTypeRefBuilder);
                        var builder = MethodRefBuilder.MethodByNameAndSignature(Module, typeRef, methodName, genericParameterCount, returnType, paramTypes);

                        _il.Remove(instruction);
                        return builder;
                    }

                    case "System.Void InlineIL.MethodRef::.ctor(InlineIL.TypeRef,System.String)":
                    case "InlineIL.MethodRef InlineIL.MethodRef::Method(InlineIL.TypeRef,System.String)":
                        return FromNamedMember((typeRef, methodName) => MethodRefBuilder.MethodByName(Module, typeRef, methodName));

                    case "InlineIL.MethodRef InlineIL.MethodRef::PropertyGet(InlineIL.TypeRef,System.String)":
                        return FromNamedMember((typeRef, propertyName) => MethodRefBuilder.PropertyGet(Module, typeRef, propertyName));

                    case "InlineIL.MethodRef InlineIL.MethodRef::PropertySet(InlineIL.TypeRef,System.String)":
                        return FromNamedMember((typeRef, propertyName) => MethodRefBuilder.PropertySet(Module, typeRef, propertyName));

                    case "InlineIL.MethodRef InlineIL.MethodRef::EventAdd(InlineIL.TypeRef,System.String)":
                        return FromNamedMember((typeRef, eventName) => MethodRefBuilder.EventAdd(Module, typeRef, eventName));

                    case "InlineIL.MethodRef InlineIL.MethodRef::EventRemove(InlineIL.TypeRef,System.String)":
                        return FromNamedMember((typeRef, eventName) => MethodRefBuilder.EventRemove(Module, typeRef, eventName));

                    case "InlineIL.MethodRef InlineIL.MethodRef::EventRaise(InlineIL.TypeRef,System.String)":
                        return FromNamedMember((typeRef, eventName) => MethodRefBuilder.EventRaise(Module, typeRef, eventName));

                    case "InlineIL.MethodRef InlineIL.MethodRef::Constructor(InlineIL.TypeRef,InlineIL.TypeRef[])":
                    {
                        var args = _il.GetArgumentPushInstructionsInSameBasicBlock(instruction);
                        var typeRef = ConsumeArgTypeRef(args[0]);
                        var paramTypes = ConsumeArgArray(args[1], ConsumeArgTypeRefBuilder);
                        var builder = MethodRefBuilder.Constructor(Module, typeRef, paramTypes);

                        _il.Remove(instruction);
                        return builder;
                    }

                    case "InlineIL.MethodRef InlineIL.MethodRef::TypeInitializer(InlineIL.TypeRef)":
                    {
                        var args = _il.GetArgumentPushInstructionsInSameBasicBlock(instruction);
                        var typeRef = ConsumeArgTypeRef(args[0]);
                        var builder = MethodRefBuilder.TypeInitializer(Module, typeRef);

                        _il.Remove(instruction);
                        return builder;
                    }

                    case "InlineIL.MethodRef InlineIL.MethodRef::Operator(InlineIL.TypeRef,InlineIL.UnaryOperator)":
                    {
                        var args = _il.GetArgumentPushInstructionsInSameBasicBlock(instruction);
                        var typeRef = ConsumeArgTypeRef(args[0]);
                        var op = ConsumeArgEnumInt32<UnaryOperator>(args[1]);
                        var builder = MethodRefBuilder.Operator(Module, typeRef, op);

                        _il.Remove(instruction);
                        return builder;
                    }

                    case "InlineIL.MethodRef InlineIL.MethodRef::Operator(InlineIL.TypeRef,InlineIL.BinaryOperator,InlineIL.TypeRef,InlineIL.TypeRef)":
                    {
                        var args = _il.GetArgumentPushInstructionsInSameBasicBlock(instruction);
                        var typeRef = ConsumeArgTypeRef(args[0]);
                        var op = ConsumeArgEnumInt32<BinaryOperator>(args[1]);
                        var leftOperandType = ConsumeArgTypeRefBuilder(args[2]);
                        var rightOperandType = ConsumeArgTypeRefBuilder(args[3]);
                        var builder = MethodRefBuilder.Operator(Module, typeRef, op, leftOperandType, rightOperandType);

                        _il.Remove(instruction);
                        return builder;
                    }

                    case "InlineIL.MethodRef InlineIL.MethodRef::Operator(InlineIL.TypeRef,InlineIL.ConversionOperator,InlineIL.ConversionDirection,InlineIL.TypeRef)":
                    {
                        var args = _il.GetArgumentPushInstructionsInSameBasicBlock(instruction);
                        var typeRef = ConsumeArgTypeRef(args[0]);
                        var op = ConsumeArgEnumInt32<ConversionOperator>(args[1]);
                        var direction = ConsumeArgEnumInt32<ConversionDirection>(args[2]);
                        var otherType = ConsumeArgTypeRefBuilder(args[3]);
                        var builder = MethodRefBuilder.Operator(Module, typeRef, op, direction, otherType);

                        _il.Remove(instruction);
                        return builder;
                    }

                    case "InlineIL.MethodRef InlineIL.MethodRef::FromDelegate(!!0)":
                    {
                        var args = instruction.GetArgumentPushInstructions();
                        var builder = ConsumeArgDelegate(args[0]);
                        _il.EnsureSameBasicBlock(args[0], instruction);

                        _il.Remove(instruction);
                        return builder;
                    }

                    case "InlineIL.MethodRef InlineIL.MethodRef::MakeGenericMethod(InlineIL.TypeRef[])":
                    {
                        var args = _il.GetArgumentPushInstructionsInSameBasicBlock(instruction);
                        var builder = ConsumeArgMethodRefBuilder(args[0]);
                        var genericArgs = ConsumeArgArray(args[1], ConsumeArgTypeRef);
                        builder.MakeGenericMethod(genericArgs);

                        _il.Remove(instruction);
                        return builder;
                    }

                    case "InlineIL.MethodRef InlineIL.MethodRef::WithOptionalParameters(InlineIL.TypeRef[])":
                    {
                        var args = _il.GetArgumentPushInstructionsInSameBasicBlock(instruction);
                        var builder = ConsumeArgMethodRefBuilder(args[0]);
                        var optionalParamTypes = ConsumeArgArray(args[1], ConsumeArgTypeRef);
                        builder.SetOptionalParameters(optionalParamTypes);

                        _il.Remove(instruction);
                        return builder;
                    }

                    default:
                        throw UnexpectedInstruction(instruction, "a method reference");
                }

                MethodRefBuilder FromNamedMember(Func<TypeReference, string, MethodRefBuilder> resolver)
                {
                    var args = _il.GetArgumentPushInstructionsInSameBasicBlock(instruction);
                    var typeRef = ConsumeArgTypeRef(args[0]);
                    var memberName = ConsumeArgString(args[1]);
                    var builder = resolver(typeRef, memberName);

                    _il.Remove(instruction);
                    return builder;
                }
            }

            MethodRefBuilder ConsumeArgDelegate(Instruction instruction)
            {
                if (instruction.OpCode != OpCodes.Newobj)
                    throw UnexpectedInstruction(instruction, "a delegate instantiation");

                var args = _il.GetArgumentPushInstructionsInSameBasicBlock(instruction);
                var methodRef = ConsumeArgLdFtn(args[1]);
                var builder = MethodRefBuilder.MethodFromDelegateReference(Module, methodRef);
                ConsumeArgObjRefNoSideEffects(args[0]);

                _il.Remove(instruction);
                return builder;
            }

            void ConsumeArgObjRefNoSideEffects(Instruction instruction)
            {
                switch (instruction.OpCode.Code)
                {
                    // Pop 0, Push 1
                    case Code.Ldnull:
                    case Code.Ldarg:
                    case Code.Ldarga:
                    case Code.Ldloc:
                    case Code.Ldloca:
                    case Code.Ldsfld:
                    case Code.Ldsflda:
                    case Code.Sizeof:
                    case Code.Dup: // Not really pop 0, push 1, but used as an arg to ldvirtftn
                    {
                        _il.Remove(instruction);
                        return;
                    }

                    // Pop 1, Push 1
                    case Code.Box:
                    case Code.Ldobj:
                    case Code.Ldfld:
                    case Code.Ldflda:
                    {
                        var arg = _il.GetPrevSkipNopsInSameBasicBlock(instruction);
                        ConsumeArgObjRefNoSideEffects(arg);

                        _il.Remove(instruction);
                        return;
                    }

                    default:
                    {
                        if (TryConsumeArgConst(instruction, out _))
                            return;

                        throw UnexpectedInstruction(instruction, "a side-effect free object load, or null");
                    }
                }
            }

            MethodReference ConsumeArgLdFtn(Instruction instruction)
            {
                switch (instruction.OpCode.Code)
                {
                    case Code.Ldftn:
                    {
                        _il.Remove(instruction);
                        return (MethodReference)instruction.Operand;
                    }

                    case Code.Ldvirtftn:
                    {
                        var arg = _il.GetPrevSkipNopsInSameBasicBlock(instruction);
                        ConsumeArgObjRefNoSideEffects(arg);

                        _il.Remove(instruction);
                        return (MethodReference)instruction.Operand;
                    }

                    default:
                        throw UnexpectedInstruction(instruction, "a ldftn or ldvirtftn");
                }
            }
        }

        public FieldReference ConsumeArgFieldRef(Instruction fieldRefInstruction)
        {
            return ConsumeArgFieldRefBuilder(fieldRefInstruction).Build();

            FieldRefBuilder ConsumeArgFieldRefBuilder(Instruction instruction)
            {
                if (instruction.OpCode.FlowControl != FlowControl.Call || !(instruction.Operand is MethodReference method))
                    throw UnexpectedInstruction(instruction, "a method call");

                switch (method.FullName)
                {
                    case "System.Void InlineIL.FieldRef::.ctor(InlineIL.TypeRef,System.String)":
                    case "InlineIL.FieldRef InlineIL.FieldRef::Field(InlineIL.TypeRef,System.String)":
                    {
                        var args = _il.GetArgumentPushInstructionsInSameBasicBlock(instruction);
                        var typeRef = ConsumeArgTypeRef(args[0]);
                        var fieldName = ConsumeArgString(args[1]);
                        var builder = new FieldRefBuilder(typeRef, fieldName);

                        _il.Remove(instruction);
                        return builder;
                    }

                    default:
                        throw UnexpectedInstruction(instruction, "a field reference");
                }
            }
        }

        public T[] ConsumeArgArray<T>(Instruction instruction, Func<Instruction, T> consumeItem)
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

            var countInstruction = _il.GetPrevSkipNopsInSameBasicBlock(newarrInstruction);
            if (countInstruction.OpCode != OpCodes.Ldc_I4)
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

                if ((int?)indexInstruction.Operand != index)
                    throw UnexpectedInstruction(indexInstruction, $"ldc.i4 with value of {index}");

                var stelemInstruction = dupInstruction.GetValueConsumingInstruction();
                if (!stelemInstruction.OpCode.IsStelem())
                    throw UnexpectedInstruction(stelemInstruction, "stelem");

                args[index] = consumeItem(stelemInstruction.PrevSkipNopsRequired());

                currentDupInstruction = stelemInstruction.NextSkipNops();

                _il.EnsureSameBasicBlock(dupInstruction, newarrInstruction);
                _il.Remove(dupInstruction);

                _il.EnsureSameBasicBlock(indexInstruction, newarrInstruction);
                _il.Remove(indexInstruction);

                _il.EnsureSameBasicBlock(stelemInstruction, newarrInstruction);
                _il.Remove(stelemInstruction);
            }

            _il.Remove(countInstruction);
            _il.Remove(newarrInstruction);

            return args;
        }

        public LocalVarBuilder ConsumeArgLocalVarBuilder(Instruction instruction)
        {
            if (instruction.OpCode.FlowControl != FlowControl.Call || !(instruction.Operand is MethodReference method))
                throw UnexpectedInstruction(instruction, "a call on LocalVar");

            switch (method.FullName)
            {
                case "System.Void InlineIL.LocalVar::.ctor(System.String,InlineIL.TypeRef)":
                {
                    var args = _il.GetArgumentPushInstructionsInSameBasicBlock(instruction);
                    var name = ConsumeArgString(args[0]);
                    var typeRef = ConsumeArgTypeRef(args[1]);
                    var builder = new LocalVarBuilder(typeRef, name);

                    _il.Remove(instruction);
                    return builder;
                }

                case "System.Void InlineIL.LocalVar::.ctor(InlineIL.TypeRef)":
                case "InlineIL.LocalVar InlineIL.LocalVar::op_Implicit(System.Type)":
                {
                    var args = _il.GetArgumentPushInstructionsInSameBasicBlock(instruction);
                    var typeRef = ConsumeArgTypeRef(args[0]);
                    var builder = new LocalVarBuilder(typeRef);

                    _il.Remove(instruction);
                    return builder;
                }

                case "InlineIL.LocalVar InlineIL.LocalVar::Pinned()":
                {
                    var args = _il.GetArgumentPushInstructionsInSameBasicBlock(instruction);
                    var builder = ConsumeArgLocalVarBuilder(args[0]);
                    builder.MakePinned();

                    _il.Remove(instruction);
                    return builder;
                }

                default:
                    throw UnexpectedInstruction(instruction, "a method call on LocalVar");
            }
        }

        public VariableDefinition ConsumeArgLocalRef(Instruction instruction)
        {
            var localName = ConsumeArgString(instruction);

            if (_il.Locals == null)
                throw new InstructionWeavingException(instruction, $"IL local '{localName}' is not defined, call IL.DeclareLocals to declare IL locals, or IL.Push/IL.Pop to reference locals declared in the source code.");

            var variableDef = _il.Locals.TryGetByName(localName);
            if (variableDef == null)
                throw new InstructionWeavingException(instruction, $"IL local '{localName}' is not defined. If it is a local declared in the source code, use IL.Push/IL.Pop to reference it.");

            return variableDef;
        }

        public int ConsumeArgParamName(Instruction instruction)
        {
            var paramName = ConsumeArgString(instruction);

            var paramIndex = Method.Parameters.IndexOfFirst(p => p.Name == paramName);
            if (paramIndex < 0)
                throw new InstructionWeavingException(instruction, $"Parameter '{paramName}' is not defined");

            if (Method.HasThis && !Method.ExplicitThis)
                ++paramIndex;

            return paramIndex;
        }

        public CallSite ConsumeArgCallSite(Instruction callSiteInstruction)
        {
            return ConsumeArgStandAloneMethodSigBuilder(callSiteInstruction).Build();

            StandAloneMethodSigBuilder ConsumeArgStandAloneMethodSigBuilder(Instruction instruction)
            {
                if (instruction.OpCode.FlowControl != FlowControl.Call || !(instruction.Operand is MethodReference method))
                    throw UnexpectedInstruction(instruction, "a method call");

                switch (method.FullName)
                {
                    case "System.Void InlineIL.StandAloneMethodSig::.ctor(System.Reflection.CallingConventions,InlineIL.TypeRef,InlineIL.TypeRef[])":
                    case "InlineIL.StandAloneMethodSig InlineIL.StandAloneMethodSig::ManagedMethod(System.Reflection.CallingConventions,InlineIL.TypeRef,InlineIL.TypeRef[])":
                    {
                        var args = _il.GetArgumentPushInstructionsInSameBasicBlock(instruction);
                        var callingConvention = (CallingConventions)ConsumeArgInt32(args[0]);
                        var returnType = ConsumeArgTypeRef(args[1]);
                        var paramTypes = ConsumeArgArray(args[2], ConsumeArgTypeRef);
                        var builder = new StandAloneMethodSigBuilder(callingConvention, returnType, paramTypes);

                        _il.Remove(instruction);
                        return builder;
                    }

                    case "System.Void InlineIL.StandAloneMethodSig::.ctor(System.Runtime.InteropServices.CallingConvention,InlineIL.TypeRef,InlineIL.TypeRef[])":
                    case "InlineIL.StandAloneMethodSig InlineIL.StandAloneMethodSig::UnmanagedMethod(System.Runtime.InteropServices.CallingConvention,InlineIL.TypeRef,InlineIL.TypeRef[])":
                    {
                        var args = _il.GetArgumentPushInstructionsInSameBasicBlock(instruction);
                        var callingConvention = (CallingConvention)ConsumeArgInt32(args[0]);
                        var returnType = ConsumeArgTypeRef(args[1]);
                        var paramTypes = ConsumeArgArray(args[2], ConsumeArgTypeRef);
                        var builder = new StandAloneMethodSigBuilder(callingConvention, returnType, paramTypes);

                        _il.Remove(instruction);
                        return builder;
                    }

                    case "InlineIL.StandAloneMethodSig InlineIL.StandAloneMethodSig::WithOptionalParameters(InlineIL.TypeRef[])":
                    {
                        var args = _il.GetArgumentPushInstructionsInSameBasicBlock(instruction);
                        var builder = ConsumeArgStandAloneMethodSigBuilder(args[0]);
                        var optionalParamTypes = ConsumeArgArray(args[1], ConsumeArgTypeRef);
                        builder.SetOptionalParameters(optionalParamTypes);

                        _il.Remove(instruction);
                        return builder;
                    }

                    default:
                        throw UnexpectedInstruction(instruction, "a stand-alone method signature");
                }
            }
        }

        private static WeavingException UnexpectedInstruction(Instruction? instruction, OpCode expectedOpcode)
            => UnexpectedInstruction(instruction, expectedOpcode.Name);

        private static WeavingException UnexpectedInstruction(Instruction? instruction, string expected)
        {
            return instruction?.OpCode.Code switch
            {
                null        => new WeavingException($"Expected {expected} but the instruction was missing"),
                Code.Ldnull => new InstructionWeavingException(instruction, $"Expected {expected} but was null"),
                _           => new InstructionWeavingException(instruction, $"Unexpected instruction, expected {expected} but was: {instruction} - InlineIL requires that arguments to IL-emitting methods be constructed in place.")
            };
        }
    }
}
