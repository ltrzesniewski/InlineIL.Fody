using System.Diagnostics.CodeAnalysis;

namespace InlineIL
{
    [SuppressMessage("ReSharper", "UnusedParameter.Local")]
    public sealed class MethodRef
    {
        public MethodRef(TypeRef type, string methodName)
            => IL.Throw();

        public MethodRef(TypeRef type, string methodName, params TypeRef[] parameterTypes)
            => IL.Throw();
    }
}
