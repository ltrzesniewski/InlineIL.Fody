namespace InlineIL
{
    public sealed class MethodRef
    {
        public MethodRef(TypeRef type, string methodName)
            => IL.Throw();

        public MethodRef(TypeRef type, string methodName, params TypeRef[] parameterTypes)
            => IL.Throw();
    }
}
