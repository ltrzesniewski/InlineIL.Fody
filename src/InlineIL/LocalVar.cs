using System.Diagnostics.CodeAnalysis;

namespace InlineIL
{
    [SuppressMessage("ReSharper", "UnusedParameter.Local")]
    public sealed class LocalVar
    {
        public LocalVar(string localName, TypeRef type)
            => IL.Throw();

        public LocalVar(TypeRef type)
            => IL.Throw();

        public LocalVar Pinned()
            => throw IL.Throw();
    }
}
