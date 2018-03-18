using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using System.Runtime.InteropServices;

namespace InlineIL
{
    [SuppressMessage("ReSharper", "UnusedParameter.Local")]
    [SuppressMessage("ReSharper", "UnusedParameter.Global")]
    [SuppressMessage("ReSharper", "MemberCanBeMadeStatic.Global")]
    public sealed class StandAloneMethodSig
    {
        public StandAloneMethodSig(CallingConvention callingConvention, TypeRef returnType, params TypeRef[] parameterTypes)
        {
        }

        public StandAloneMethodSig(CallingConventions callingConvention, TypeRef returnType, params TypeRef[] parameterTypes)
        {
        }

        public StandAloneMethodSig WithOptionalParameters(params TypeRef[] optionalParameterTypes)
            => throw IL.Throw();
    }
}
