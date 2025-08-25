﻿using System.Linq;
using InlineIL.Fody.Processing;
using Mono.Cecil;

namespace InlineIL.Fody.Extensions;

internal static partial class CecilExtensions
{
    public static bool IsInlineILTypeUsage(this TypeReference? type, ModuleWeavingContext context)
    {
        if (type == null)
            return false;

        if (context.LibUsageTypeCache.TryGetValue(type, out var result))
            return result;

        context.LibUsageTypeCache[type] = false;
        result = DoCheck(type, context);
        context.LibUsageTypeCache[type] = result;
        return result;

        static bool DoCheck(TypeReference typeRef, ModuleWeavingContext ctx)
        {
            switch (typeRef)
            {
                case GenericInstanceType t:
                    return t.ElementType.IsInlineILTypeUsage(ctx)
                           || t.GenericParameters.Any(i => i.IsInlineILTypeUsage(ctx))
                           || t.GenericArguments.Any(i => i.IsInlineILTypeUsage(ctx));

                case GenericParameter t:
                    return t.HasConstraints && t.Constraints.Any(c => c.IsInlineILTypeUsage(ctx))
                           || t.HasCustomAttributes && t.CustomAttributes.Any(i => i.IsInlineILTypeUsage(ctx));

                case IModifierType t:
                    return t.ElementType.IsInlineILTypeUsage(ctx)
                           || t.ModifierType.IsInlineILTypeUsage(ctx);

                case FunctionPointerType t:
                    return ((IMethodSignature)t).IsInlineILTypeUsage(ctx);

                default:
                    return typeRef.Scope is { MetadataScopeType: MetadataScopeType.AssemblyNameReference, Name: "InlineIL" };
            }
        }
    }

    public static bool IsInlineILTypeUsageDeep(this TypeDefinition? typeDef, ModuleWeavingContext context)
    {
        if (typeDef == null)
            return false;

        return typeDef.IsInlineILTypeUsage(context)
               || typeDef.BaseType.IsInlineILTypeUsage(context)
               || typeDef.HasInterfaces && typeDef.Interfaces.Any(i => i.IsInlineILTypeUsage(context))
               || typeDef.HasGenericParameters && typeDef.GenericParameters.Any(i => i.IsInlineILTypeUsage(context))
               || typeDef.HasCustomAttributes && typeDef.CustomAttributes.Any(i => i.IsInlineILTypeUsage(context))
               || typeDef.HasMethods && typeDef.Methods.Any(i => i.IsInlineILTypeUsage(context))
               || typeDef.HasFields && typeDef.Fields.Any(i => i.IsInlineILTypeUsage(context))
               || typeDef.HasProperties && typeDef.Properties.Any(i => i.IsInlineILTypeUsage(context))
               || typeDef.HasEvents && typeDef.Events.Any(i => i.IsInlineILTypeUsage(context));
    }

    public static bool IsInlineILTypeUsage(this IMethodSignature? method, ModuleWeavingContext context)
    {
        if (method == null)
            return false;

        if (method.ReturnType.IsInlineILTypeUsage(context) || method.HasParameters && method.Parameters.Any(i => i.IsInlineILTypeUsage(context)))
            return true;

        if (method is IGenericInstance { HasGenericArguments: true } genericInstance && genericInstance.GenericArguments.Any(i => i.IsInlineILTypeUsage(context)))
            return true;

        if (method is IGenericParameterProvider { HasGenericParameters: true } generic && generic.GenericParameters.Any(i => i.IsInlineILTypeUsage(context)))
            return true;

        if (method is MethodReference methodRef)
        {
            if (methodRef is MethodDefinition methodDef)
            {
                if (methodDef.HasCustomAttributes && methodDef.CustomAttributes.Any(i => i.IsInlineILTypeUsage(context)))
                    return true;
            }
            else
            {
                if (methodRef.DeclaringType.IsInlineILTypeUsage(context))
                    return true;
            }
        }

        return false;
    }

    public static bool IsInlineILTypeUsage(this FieldReference? fieldRef, ModuleWeavingContext context)
    {
        if (fieldRef == null)
            return false;

        if (fieldRef.FieldType.IsInlineILTypeUsage(context))
            return true;

        if (fieldRef is FieldDefinition fieldDef)
        {
            if (fieldDef.HasCustomAttributes && fieldDef.CustomAttributes.Any(i => i.IsInlineILTypeUsage(context)))
                return true;
        }
        else
        {
            if (fieldRef.DeclaringType.IsInlineILTypeUsage(context))
                return true;
        }

        return false;
    }

    public static bool IsInlineILTypeUsage(this PropertyReference? propRef, ModuleWeavingContext context)
    {
        if (propRef == null)
            return false;

        if (propRef.PropertyType.IsInlineILTypeUsage(context))
            return true;

        if (propRef is PropertyDefinition propDef)
        {
            if (propDef.HasCustomAttributes && propDef.CustomAttributes.Any(i => i.IsInlineILTypeUsage(context)))
                return true;
        }
        else
        {
            if (propRef.DeclaringType.IsInlineILTypeUsage(context))
                return true;
        }

        return false;
    }

    public static bool IsInlineILTypeUsage(this EventReference? eventRef, ModuleWeavingContext context)
    {
        if (eventRef == null)
            return false;

        if (eventRef.EventType.IsInlineILTypeUsage(context))
            return true;

        if (eventRef is EventDefinition eventDef)
        {
            if (eventDef.HasCustomAttributes && eventDef.CustomAttributes.Any(i => i.IsInlineILTypeUsage(context)))
                return true;
        }
        else
        {
            if (eventRef.DeclaringType.IsInlineILTypeUsage(context))
                return true;
        }

        return false;
    }

    public static bool IsInlineILTypeUsage(this ParameterDefinition? paramDef, ModuleWeavingContext context)
    {
        if (paramDef == null)
            return false;

        if (paramDef.ParameterType.IsInlineILTypeUsage(context))
            return true;

        if (paramDef.HasCustomAttributes && paramDef.CustomAttributes.Any(i => i.IsInlineILTypeUsage(context)))
            return true;

        return false;
    }

    public static bool IsInlineILTypeUsage(this CustomAttribute? attr, ModuleWeavingContext context)
    {
        if (attr == null)
            return false;

        if (attr.AttributeType.IsInlineILTypeUsage(context))
            return true;

        if (attr.HasConstructorArguments && attr.ConstructorArguments.Any(i => i.Value is TypeReference typeRef && typeRef.IsInlineILTypeUsage(context)))
            return true;

        if (attr.HasProperties && attr.Properties.Any(i => i.Argument.Value is TypeReference typeRef && typeRef.IsInlineILTypeUsage(context)))
            return true;

        return false;
    }

    public static bool IsInlineILTypeUsage(this InterfaceImplementation? interfaceImpl, ModuleWeavingContext context)
    {
        if (interfaceImpl == null)
            return false;

        return interfaceImpl.InterfaceType.IsInlineILTypeUsage(context)
               || interfaceImpl.HasCustomAttributes && interfaceImpl.CustomAttributes.Any(i => i.IsInlineILTypeUsage(context));
    }

    public static bool IsInlineILTypeUsage(this GenericParameterConstraint? constraint, ModuleWeavingContext context)
    {
        if (constraint == null)
            return false;

        if (constraint.ConstraintType.IsInlineILTypeUsage(context))
            return true;

        if (constraint.HasCustomAttributes && constraint.CustomAttributes.Any(i => i.IsInlineILTypeUsage(context)))
            return true;

        return false;
    }
}
