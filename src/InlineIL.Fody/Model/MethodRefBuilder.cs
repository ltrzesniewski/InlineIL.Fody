using System;
using System.Collections.Generic;
using System.Linq;
using Fody;
using InlineIL.Fody.Extensions;
using JetBrains.Annotations;
using Mono.Cecil;
using Mono.Cecil.Rocks;

namespace InlineIL.Fody.Model
{
    internal class MethodRefBuilder
    {
        private readonly ModuleDefinition _module;
        private MethodReference _method;

        private MethodRefBuilder(ModuleDefinition module, TypeReference typeRef, MethodReference method)
        {
            _module = module;
            _method = _module.ImportReference(_module.ImportReference(method).MakeGeneric(typeRef));
        }

        public static MethodRefBuilder MethodByName(ModuleDefinition module, TypeReference typeRef, string methodName)
            => new MethodRefBuilder(module, typeRef, FindMethod(typeRef, methodName, null));

        public static MethodRefBuilder MethodByNameAndSignature(ModuleDefinition module, TypeReference typeRef, string methodName, IReadOnlyCollection<TypeReference> paramTypes)
            => new MethodRefBuilder(module, typeRef, FindMethod(typeRef, methodName, paramTypes ?? throw new ArgumentNullException(nameof(paramTypes))));

        private static MethodReference FindMethod(TypeReference typeRef, string methodName, [CanBeNull] IReadOnlyCollection<TypeReference> paramTypes)
        {
            var typeDef = typeRef.ResolveRequiredType();

            var methods = typeDef.Methods
                                 .Where(m => m.Name == methodName)
                                 .Where(m => paramTypes == null
                                             || m.Parameters.Count == paramTypes.Count
                                             && m.Parameters.Select(p => p.ParameterType.FullName).SequenceEqual(paramTypes.Select(p => p.FullName)))
                                 .ToList();

            switch (methods.Count)
            {
                case 1:
                    return methods.Single();

                case 0:
                    throw paramTypes == null
                        ? new WeavingException($"Method '{methodName}' not found in type {typeDef.FullName}")
                        : new WeavingException($"Method {methodName}({string.Join(", ", paramTypes.Select(p => p.FullName))}) not found in type {typeDef.FullName}");

                default:
                    throw paramTypes == null
                        ? new WeavingException($"Ambiguous method '{methodName}' in type {typeDef.FullName}")
                        : new WeavingException($"Ambiguous method {methodName}({string.Join(", ", paramTypes.Select(p => p.FullName))}) in type {typeDef.FullName}");
            }
        }

        public static MethodRefBuilder PropertyGet(ModuleDefinition module, TypeReference typeRef, string propertyName)
        {
            var property = FindProperty(typeRef, propertyName);

            if (property.GetMethod == null)
                throw new WeavingException($"Property '{propertyName}' in type {typeRef.FullName} has no getter");

            return new MethodRefBuilder(module, typeRef, property.GetMethod);
        }

        public static MethodRefBuilder PropertySet(ModuleDefinition module, TypeReference typeRef, string propertyName)
        {
            var property = FindProperty(typeRef, propertyName);

            if (property.SetMethod == null)
                throw new WeavingException($"Property '{propertyName}' in type {typeRef.FullName} has no setter");

            return new MethodRefBuilder(module, typeRef, property.SetMethod);
        }

        private static PropertyDefinition FindProperty(TypeReference typeRef, string propertyName)
        {
            var typeDef = typeRef.ResolveRequiredType();

            var properties = typeDef.Properties.Where(p => p.Name == propertyName).ToList();

            switch (properties.Count)
            {
                case 1:
                    return properties.Single();

                case 0:
                    throw new WeavingException($"Property '{propertyName}' not found in type {typeDef.FullName}");

                default:
                    throw new WeavingException($"Ambiguous property '{propertyName}' in type {typeDef.FullName}");
            }
        }

        public static MethodRefBuilder EventAdd(ModuleDefinition module, TypeReference typeRef, string eventName)
        {
            var property = FindEvent(typeRef, eventName);

            if (property.AddMethod == null)
                throw new WeavingException($"Event '{eventName}' in type {typeRef.FullName} has no add method");

            return new MethodRefBuilder(module, typeRef, property.AddMethod);
        }

        public static MethodRefBuilder EventRemove(ModuleDefinition module, TypeReference typeRef, string eventName)
        {
            var property = FindEvent(typeRef, eventName);

            if (property.RemoveMethod == null)
                throw new WeavingException($"Event '{eventName}' in type {typeRef.FullName} has no remove method");

            return new MethodRefBuilder(module, typeRef, property.RemoveMethod);
        }

        public static MethodRefBuilder EventRaise(ModuleDefinition module, TypeReference typeRef, string eventName)
        {
            var property = FindEvent(typeRef, eventName);

            if (property.InvokeMethod == null)
                throw new WeavingException($"Event '{eventName}' in type {typeRef.FullName} has no raise method");

            return new MethodRefBuilder(module, typeRef, property.InvokeMethod);
        }

        private static EventDefinition FindEvent(TypeReference typeRef, string eventName)
        {
            var typeDef = typeRef.ResolveRequiredType();

            var events = typeDef.Events.Where(e => e.Name == eventName).ToList();

            switch (events.Count)
            {
                case 1:
                    return events.Single();

                case 0:
                    throw new WeavingException($"Event '{eventName}' not found in type {typeDef.FullName}");

                default:
                    throw new WeavingException($"Ambiguous event '{eventName}' in type {typeDef.FullName}");
            }
        }

        public static MethodRefBuilder Constructor(ModuleDefinition module, TypeReference typeRef, IReadOnlyCollection<TypeReference> paramTypes)
        {
            var typeDef = typeRef.ResolveRequiredType();

            var ctors = typeDef.GetConstructors()
                               .Where(i => !i.IsStatic && i.Name == ".ctor")
                               .Where(i => i.Parameters.Select(p => p.ParameterType.FullName).SequenceEqual(paramTypes.Select(p => p.FullName)))
                               .ToList();

            if (ctors.Count == 1)
                return new MethodRefBuilder(module, typeRef, ctors.Single());

            if (paramTypes.Count == 0)
                throw new WeavingException($"Type {typeDef.FullName} has no default constructor");

            throw new WeavingException($"Type {typeDef.FullName} has no constructor with signature ({string.Join(", ", paramTypes.Select(p => p.FullName))})");
        }

        public static MethodRefBuilder TypeInitializer(ModuleDefinition module, TypeReference typeRef)
        {
            var typeDef = typeRef.ResolveRequiredType();

            var cctors = typeDef.GetConstructors()
                                .Where(i => i.IsStatic && i.Name == ".cctor" && i.Parameters.Count == 0)
                                .ToList();

            if (cctors.Count == 1)
                return new MethodRefBuilder(module, typeRef, cctors.Single());

            throw new WeavingException($"Type {typeDef.FullName} has no type initializer");
        }

        public MethodReference Build()
            => _method;

        public void MakeGenericMethod(TypeReference[] genericArgs)
        {
            if (!_method.HasGenericParameters)
                throw new WeavingException($"Not a generic method: {_method.FullName}");

            if (genericArgs.Length == 0)
                throw new WeavingException("No generic arguments supplied");

            if (_method.GenericParameters.Count != genericArgs.Length)
                throw new WeavingException($"Incorrect number of generic arguments supplied for method {_method.FullName} - expected {_method.GenericParameters.Count}, but got {genericArgs.Length}");

            var genericInstance = new GenericInstanceMethod(_method);
            genericInstance.GenericArguments.AddRange(genericArgs);

            _method = _module.ImportReference(genericInstance);
        }

        public void SetOptionalParameters(TypeReference[] optionalParamTypes)
        {
            if (_method.CallingConvention != MethodCallingConvention.VarArg)
                throw new WeavingException($"Not a vararg method: {_method.FullName}");

            if (_method.Parameters.Any(p => p.ParameterType.IsSentinel))
                throw new WeavingException("Optional parameters for vararg call have already been supplied");

            if (optionalParamTypes.Length == 0)
                throw new WeavingException("No optional parameter type supplied for vararg method call");

            var methodRef = _method.Clone();
            methodRef.CallingConvention = MethodCallingConvention.VarArg;

            for (var i = 0; i < optionalParamTypes.Length; ++i)
            {
                var paramType = optionalParamTypes[i];
                if (i == 0)
                    paramType = paramType.MakeSentinelType();

                methodRef.Parameters.Add(new ParameterDefinition(paramType));
            }

            _method = _module.ImportReference(methodRef);
        }

        public override string ToString() => _method.ToString();
    }
}
