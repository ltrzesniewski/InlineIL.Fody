using System.Diagnostics.CodeAnalysis;

namespace InlineIL
{
    [SuppressMessage("ReSharper", "UnusedParameter.Local")]
    [SuppressMessage("ReSharper", "UnusedParameter.Global")]
    [SuppressMessage("ReSharper", "MemberCanBeMadeStatic.Global")]
    public sealed class MethodRef
    {
        public MethodRef(TypeRef type, string methodName)
            => IL.Throw();

        public MethodRef(TypeRef type, string methodName, params TypeRef[] parameterTypes)
            => IL.Throw();

        public MethodRef WithOptionalParameters(params TypeRef[] optionalParameterTypes)
            => throw IL.Throw();
    }
}
