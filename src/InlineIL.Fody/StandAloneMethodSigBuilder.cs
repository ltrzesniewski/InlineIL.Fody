using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using Fody;
using Mono.Cecil;
using Mono.Cecil.Rocks;

namespace InlineIL.Fody
{
    internal class StandAloneMethodSigBuilder
    {
        private readonly CallSite _callSite;

        public StandAloneMethodSigBuilder(CallingConventions callingConvention, TypeReference returnType, IEnumerable<TypeReference> paramTypes)
        {
            _callSite = new CallSite(returnType)
            {
                CallingConvention = (callingConvention & CallingConventions.VarArgs) == 0
                    ? MethodCallingConvention.Default
                    : MethodCallingConvention.VarArg,
                HasThis = (callingConvention & CallingConventions.HasThis) != 0,
                ExplicitThis = (callingConvention & CallingConventions.ExplicitThis) != 0
            };

            _callSite.Parameters.AddRange(paramTypes.Select(t => new ParameterDefinition(t)));
        }

        public StandAloneMethodSigBuilder(CallingConvention callingConvention, TypeReference returnType, IEnumerable<TypeReference> paramTypes)
        {
            _callSite = new CallSite(returnType)
            {
                CallingConvention = callingConvention.ToMethodCallingConvention()
            };

            _callSite.Parameters.AddRange(paramTypes.Select(t => new ParameterDefinition(t)));
        }

        public CallSite Build()
            => _callSite;

        public void SetOptionalParameters(TypeReference[] optionalParamTypes)
        {
            if (_callSite.CallingConvention != MethodCallingConvention.VarArg)
                throw new WeavingException("Not a vararg calling convention");

            if (_callSite.Parameters.Any(p => p.ParameterType.IsSentinel))
                throw new WeavingException("Optional parameters for vararg call site have already been supplied");

            if (optionalParamTypes.Length == 0)
                throw new WeavingException("No optional parameter type supplied for vararg call site");

            for (var i = 0; i < optionalParamTypes.Length; ++i)
            {
                var paramType = optionalParamTypes[i];
                if (i == 0)
                    paramType = paramType.MakeSentinelType();

                _callSite.Parameters.Add(new ParameterDefinition(paramType));
            }
        }

        public override string ToString() => _callSite.ToString();
    }
}
