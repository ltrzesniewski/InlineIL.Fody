using System.Diagnostics.CodeAnalysis;

namespace InlineIL
{
    [SuppressMessage("ReSharper", "UnusedParameter.Local")]
    public sealed class FieldRef
    {
        public FieldRef(TypeRef type, string fieldName)
            => IL.Throw();
    }
}
