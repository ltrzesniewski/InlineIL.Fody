using System.Linq;
using Fody;
using Mono.Cecil;
using Mono.Cecil.Rocks;

namespace InlineIL.Fody
{
    internal class MethodRefBuilder
    {
        private readonly ModuleDefinition _module;
        private MethodReference _method;

        public MethodRefBuilder(ModuleDefinition module, TypeReference typeRef, string methodName)
        {
            _module = module;

            var typeDef = typeRef.ResolveRequiredType();

            var methods = typeDef.Methods.Where(m => m.Name == methodName).ToList();
            switch (methods.Count)
            {
                case 0:
                    throw new WeavingException($"Method '{methodName}' not found in type {typeDef.FullName}");

                case 1:
                    _method = _module.ImportReference(_module.ImportReference(methods.Single()).MakeGeneric(typeRef));
                    break;

                default:
                    throw new WeavingException($"Ambiguous method '{methodName}' in type {typeDef.FullName}");
            }
        }

        public MethodRefBuilder(ModuleDefinition module, TypeReference typeRef, string methodName, TypeReference[] paramTypes)
        {
            _module = module;

            var typeDef = typeRef.ResolveRequiredType();

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
                    _method = _module.ImportReference(_module.ImportReference(methods.Single()).MakeGeneric(typeRef));
                    break;

                default:
                    // This should never happen
                    throw new WeavingException($"Ambiguous method {methodName}({string.Join(", ", paramTypes.Select(p => p.FullName))}) in type {typeDef.FullName}");
            }
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
