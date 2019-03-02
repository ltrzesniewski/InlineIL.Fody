using System.Collections.Generic;
using System.Linq;
using InlineIL.Fody.Processing;
using JetBrains.Annotations;
using Mono.Cecil;

namespace InlineIL.Fody.Extensions
{
    internal static partial class CecilExtensions
    {
        private static readonly Dictionary<TypeReference, bool> _checkedTypes = new Dictionary<TypeReference, bool>();

        [ContractAnnotation("null => false")]
        public static bool IsInlineILTypeUsage([CanBeNull] this TypeReference type)
        {
            if (type == null)
                return false;

            lock (_checkedTypes)
            {
                if (_checkedTypes.TryGetValue(type, out var result))
                    return result;

                _checkedTypes[type] = false;
                result = DoCheck();
                _checkedTypes[type] = result;
                return result;
            }

            bool DoCheck()
            {
                switch (type)
                {
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
        }

        [ContractAnnotation("null => false")]
        public static bool IsInlineILTypeUsageDeep([CanBeNull] this TypeDefinition typeDef)
        {
            if (typeDef == null)
                return false;

            return typeDef.IsInlineILTypeUsage()
                   || typeDef.BaseType.IsInlineILTypeUsage()
                   || typeDef.HasInterfaces && typeDef.Interfaces.Any(i => i.IsInlineILTypeUsage())
                   || typeDef.HasGenericParameters && typeDef.GenericParameters.Any(i => i.IsInlineILTypeUsage())
                   || typeDef.HasCustomAttributes && typeDef.CustomAttributes.Any(i => i.IsInlineILTypeUsage())
                   || typeDef.HasMethods && typeDef.Methods.Any(i => i.IsInlineILTypeUsage())
                   || typeDef.HasFields && typeDef.Fields.Any(i => i.IsInlineILTypeUsage())
                   || typeDef.HasProperties && typeDef.Properties.Any(i => i.IsInlineILTypeUsage())
                   || typeDef.HasEvents && typeDef.Events.Any(i => i.IsInlineILTypeUsage());
        }

        [ContractAnnotation("null => false")]
        public static bool IsInlineILTypeUsage([CanBeNull] this IMethodSignature method)
        {
            if (method == null)
                return false;

            if (method.ReturnType.IsInlineILTypeUsage() || method.HasParameters && method.Parameters.Any(i => i.IsInlineILTypeUsage()))
                return true;

            if (method is IGenericInstance genericInstance && genericInstance.HasGenericArguments && genericInstance.GenericArguments.Any(i => i.IsInlineILTypeUsage()))
                return true;

            if (method is IGenericParameterProvider generic && generic.HasGenericParameters && generic.GenericParameters.Any(i => i.IsInlineILTypeUsage()))
                return true;

            if (method is MethodReference methodRef)
            {
                if (methodRef is MethodDefinition methodDef)
                {
                    if (methodDef.HasCustomAttributes && methodDef.CustomAttributes.Any(i => i.IsInlineILTypeUsage()))
                        return true;
                }
                else
                {
                    if (methodRef.DeclaringType.IsInlineILTypeUsage())
                        return true;
                }
            }

            return false;
        }

        [ContractAnnotation("null => false")]
        public static bool IsInlineILTypeUsage([CanBeNull] this FieldReference fieldRef)
        {
            if (fieldRef == null)
                return false;

            if (fieldRef.FieldType.IsInlineILTypeUsage())
                return true;

            if (fieldRef is FieldDefinition fieldDef)
            {
                if (fieldDef.HasCustomAttributes && fieldDef.CustomAttributes.Any(i => i.IsInlineILTypeUsage()))
                    return true;
            }
            else
            {
                if (fieldRef.DeclaringType.IsInlineILTypeUsage())
                    return true;
            }

            return false;
        }

        [ContractAnnotation("null => false")]
        public static bool IsInlineILTypeUsage([CanBeNull] this PropertyReference propRef)
        {
            if (propRef == null)
                return false;

            if (propRef.PropertyType.IsInlineILTypeUsage())
                return true;

            if (propRef is PropertyDefinition propDef)
            {
                if (propDef.HasCustomAttributes && propDef.CustomAttributes.Any(i => i.IsInlineILTypeUsage()))
                    return true;
            }
            else
            {
                if (propRef.DeclaringType.IsInlineILTypeUsage())
                    return true;
            }

            return false;
        }

        public static bool IsInlineILTypeUsage(this EventReference eventRef)
        {
            if (eventRef == null)
                return false;

            if (eventRef.EventType.IsInlineILTypeUsage())
                return true;

            if (eventRef is EventDefinition eventDef)
            {
                if (eventDef.HasCustomAttributes && eventDef.CustomAttributes.Any(i => i.IsInlineILTypeUsage()))
                    return true;
            }
            else
            {
                if (eventRef.DeclaringType.IsInlineILTypeUsage())
                    return true;
            }

            return false;
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
        public static bool IsInlineILTypeUsage([CanBeNull] this InterfaceImplementation ifaceImpl)
        {
            if (ifaceImpl == null)
                return false;

            return ifaceImpl.InterfaceType.IsInlineILTypeUsage()
                   || ifaceImpl.HasCustomAttributes && ifaceImpl.CustomAttributes.Any(i => i.IsInlineILTypeUsage());
        }
    }
}
