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

namespace InlineIL.Fody.Extensions;

internal static partial class CecilExtensions
{
    public static TypeDefinition ResolveRequiredType(this TypeReference typeRef, ModuleWeavingContext context)
    {
        TypeDefinition typeDef;

        try
        {
            typeDef = context.InjectedAssemblyResolver.ResolveRegisteredType(typeRef)
                      ?? typeRef.Resolve();
        }
        catch (Exception ex)
        {
            throw new WeavingException($"Could not resolve type {typeRef.FullName}: {ex.Message}");
        }

        return typeDef ?? throw new WeavingException($"Could not resolve type {typeRef.FullName}");
    }

    public static bool IsForwardedType(this ExportedType exportedType)
    {
        for (; exportedType != null; exportedType = exportedType.DeclaringType)
        {
            if (exportedType.IsForwarder)
                return true;
        }

        return false;
    }

    private static TypeDefinition ResolveRequiredType(this ExportedType exportedType)
    {
        TypeDefinition typeDef;

        try
        {
            typeDef = exportedType.Resolve();
        }
        catch (Exception ex)
        {
            throw new WeavingException($"Could not resolve type {exportedType.FullName}: {ex.Message}");
        }

        return typeDef ?? throw new WeavingException($"Could not resolve type {exportedType.FullName}");
    }

    public static TypeReference Clone(this TypeReference typeRef)
    {
        var clone = new TypeReference(typeRef.Namespace, typeRef.Name, typeRef.Module, typeRef.Scope, typeRef.IsValueType)
        {
            DeclaringType = typeRef.DeclaringType
        };

        if (typeRef.HasGenericParameters)
        {
            foreach (var param in typeRef.GenericParameters)
                clone.GenericParameters.Add(new GenericParameter(param.Name, clone));
        }

        return clone;
    }

    public static TypeReference CreateReference(this ExportedType exportedType, ModuleDefinition exportingModule, ModuleDefinition targetModule)
    {
        var typeDef = exportedType.ResolveRequiredType();
        var metadataScope = MapAssemblyReference(targetModule, exportingModule.Assembly.Name);

        var typeRef = new TypeReference(exportedType.Namespace, exportedType.Name, exportingModule, metadataScope, typeDef.IsValueType)
        {
            DeclaringType = exportedType.DeclaringType?.CreateReference(exportingModule, targetModule)
        };

        if (typeDef.HasGenericParameters)
        {
            foreach (var param in typeDef.GenericParameters)
                typeRef.GenericParameters.Add(new GenericParameter(param.Name, typeRef));
        }

        return typeRef;
    }

    private static AssemblyNameReference MapAssemblyReference(ModuleDefinition module, AssemblyNameReference name)
    {
        // Try to map to an existing assembly reference by name,
        // to avoid adding additional versions of a referenced assembly
        // (netstandard v2.0 can be mapped to netstandard 2.1 for instance)

        foreach (var assemblyReference in module.AssemblyReferences)
        {
            if (assemblyReference.Name == name.Name)
                return assemblyReference;
        }

        return name;
    }

    public static MethodReference Clone(this MethodReference method)
    {
        var clone = new MethodReference(method.Name, method.ReturnType, method.DeclaringType)
        {
            HasThis = method.HasThis,
            ExplicitThis = method.ExplicitThis,
            CallingConvention = method.CallingConvention
        };

        if (method.HasParameters)
        {
            foreach (var param in method.Parameters)
                clone.Parameters.Add(new ParameterDefinition(param.Name, param.Attributes, param.ParameterType));
        }

        if (method.HasGenericParameters)
        {
            foreach (var param in method.GenericParameters)
                clone.GenericParameters.Add(new GenericParameter(param.Name, clone));
        }

        return clone;
    }

    /// <summary>
    /// Make an effort to map a method reference to another metadata scope.
    /// </summary>
    /// <remarks>
    /// This only has an effect when a user tries to bind to a method declared on a forwarded type.
    /// In that case, try to reference the forwarding assembly, since it's what the user asked for.
    /// </remarks>
    public static MethodReference TryMapToScope(this MethodReference method, IMetadataScope scope, ModuleDefinition module)
    {
        if (!ShouldTryToMapScope(method.DeclaringType, scope))
            return method;

        var clone = new MethodReference(method.Name, TryToMapType(method.ReturnType), TryToMapType(method.DeclaringType))
        {
            HasThis = method.HasThis,
            ExplicitThis = method.ExplicitThis,
            CallingConvention = method.CallingConvention
        };

        if (method.HasParameters)
        {
            foreach (var param in method.Parameters)
                clone.Parameters.Add(new ParameterDefinition(param.Name, param.Attributes, TryToMapType(param.ParameterType)));
        }

        if (method.HasGenericParameters)
        {
            foreach (var param in method.GenericParameters)
                clone.GenericParameters.Add(new GenericParameter(param.Name, clone));
        }

        return clone;

        static bool ShouldTryToMapScope(TypeReference typeRef, IMetadataScope scope)
        {
            if (scope.MetadataScopeType != MetadataScopeType.AssemblyNameReference)
                return false;

            if (ReferenceEquals(typeRef.Scope, scope))
                return false;

            var assemblyFullName = typeRef.Scope switch
            {
                AssemblyNameReference assemblyName => assemblyName.FullName,
                ModuleDefinition moduleDef         => moduleDef.Assembly.FullName,
                _                                  => null
            };

            return assemblyFullName != ((AssemblyNameReference)scope).FullName;
        }

        TypeReference TryToMapType(TypeReference typeRef)
        {
            if (!ShouldTryToMapScope(typeRef, scope))
                return typeRef;

            var assemblyName = (AssemblyNameReference)scope;
            var assembly = module.AssemblyResolver.Resolve(assemblyName) ?? throw new WeavingException($"Could not resolve assembly {assemblyName.Name}");

            if (assembly.MainModule.HasExportedTypes)
            {
                foreach (var exportedType in assembly.MainModule.ExportedTypes)
                {
                    if (!exportedType.IsForwardedType())
                        continue;

                    // TODO: Handle TypeSpecs
                    if (exportedType.FullName == typeRef.FullName)
                        return exportedType.CreateReference(assembly.MainModule, module);
                }
            }

            return typeRef;
        }
    }

    public static FieldReference Clone(this FieldReference field)
        => new(field.Name, field.FieldType, field.DeclaringType);

    public static MethodReference MakeGeneric(this MethodReference method, TypeReference declaringType)
    {
        if (!declaringType.IsGenericInstance || method.DeclaringType.IsGenericInstance)
            return method;

        var genericDeclType = new GenericInstanceType(method.DeclaringType);

        foreach (var argument in ((GenericInstanceType)declaringType).GenericArguments)
            genericDeclType.GenericArguments.Add(argument);

        var result = method.Clone();
        result.DeclaringType = genericDeclType;
        return result;
    }

    public static Instruction? PrevSkipNops(this Instruction? instruction)
    {
        instruction = instruction?.Previous;

        while (instruction != null && instruction.OpCode == OpCodes.Nop)
            instruction = instruction.Previous;

        return instruction;
    }

    public static Instruction PrevSkipNopsRequired(this Instruction instruction)
        => instruction.PrevSkipNops() ?? throw new InstructionWeavingException(instruction, "The first instruction causes a stack underflow");

    public static Instruction? SkipNops(this Instruction? instruction)
    {
        while (instruction != null && instruction.OpCode == OpCodes.Nop)
            instruction = instruction.Next;

        return instruction;
    }

    public static Instruction? NextSkipNops(this Instruction? instruction)
        => instruction?.Next?.SkipNops();

    public static Instruction GetValueConsumingInstruction(this Instruction instruction)
    {
        var stackSize = 0;

        while (true)
        {
            stackSize += GetPushCount(instruction);
            instruction = instruction.Next ?? throw new WeavingException("Unexpected end of method");
            stackSize -= GetPopCount(instruction);

            if (stackSize <= 0)
                return instruction;
        }
    }

    public static Instruction[] GetArgumentPushInstructions(this Instruction instruction)
    {
        if (instruction.OpCode.FlowControl != FlowControl.Call)
            throw new InstructionWeavingException(instruction, "Expected a call instruction");

        var method = (IMethodSignature)instruction.Operand;
        var argCount = GetArgCount(instruction.OpCode, method);

        if (argCount == 0)
            return [];

        var result = new Instruction[argCount];
        var currentInstruction = instruction.Previous;

        for (var paramIndex = result.Length - 1; paramIndex >= 0; --paramIndex)
            result[paramIndex] = BackwardScanPush(ref currentInstruction);

        return result;
    }

    private static Instruction BackwardScanPush(ref Instruction currentInstruction)
    {
        var startInstruction = currentInstruction;
        Instruction? result = null;
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

        return result ?? throw new InstructionWeavingException(startInstruction, "Could not locate call argument, reached beginning of method");
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
                throw new InstructionWeavingException(instruction, $"Unexpected stack pop behavior: {instruction.OpCode.StackBehaviourPop}");
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
                throw new InstructionWeavingException(instruction, $"Unexpected stack push behavior: {instruction.OpCode.StackBehaviourPush}");
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
        return callingConvention switch
        {
            CallingConvention.Cdecl    => MethodCallingConvention.C,
            CallingConvention.StdCall  => MethodCallingConvention.StdCall,
            CallingConvention.Winapi   => MethodCallingConvention.StdCall,
            CallingConvention.FastCall => MethodCallingConvention.FastCall,
            CallingConvention.ThisCall => MethodCallingConvention.ThisCall,
            _                          => throw new WeavingException("Invalid calling convention")
        };
    }

    public static IEnumerable<Instruction> GetReferencedInstructions(this Instruction instruction)
    {
        return instruction.Operand switch
        {
            Instruction operand   => [operand],
            Instruction[] operand => operand,
            _                     => []
        };
    }

    public static IEnumerable<Instruction> GetReferencedInstructions(this ExceptionHandler handler)
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

    public static IMetadataScope? GetCoreLibrary(this ModuleDefinition module)
        => module.TypeSystem.CoreLibrary;

    public static bool IsDebugBuild(this ModuleDefinition module)
    {
        const string attributeName = "System.Diagnostics.DebuggableAttribute";
        const string enumName = attributeName + "/DebuggingModes";

        var debuggableAttribute = module.Assembly.CustomAttributes.FirstOrDefault(i => i.AttributeType.FullName == attributeName)
                                  ?? module.CustomAttributes.FirstOrDefault(i => i.AttributeType.FullName == attributeName);

        if (debuggableAttribute == null)
            return false;

        var args = debuggableAttribute.ConstructorArguments;

        return args switch
        {
            [{ Type.FullName: enumName, Value: int intValue }]
                => ((DebuggableAttribute.DebuggingModes)intValue & DebuggableAttribute.DebuggingModes.DisableOptimizations) != 0,

            [{ Value: bool }, { Value: bool isJitOptimizerDisabled }]
                => isJitOptimizerDisabled,

            _ => false
        };
    }

    [ContractAnnotation("null => false")]
    public static bool IsInlineILAssembly(this AssemblyNameReference? assembly)
        => assembly?.Name == "InlineIL";

    public static bool IsCompilerGenerated(this ICustomAttributeProvider? definition)
        => definition?.HasCustomAttributes is true
           && definition.CustomAttributes.Any(i => i.AttributeType.FullName == "System.Runtime.CompilerServices.CompilerGeneratedAttribute");
}
