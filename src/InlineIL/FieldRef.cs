using System.Diagnostics.CodeAnalysis;

namespace InlineIL
{
    /// <summary>
    /// Represents a field reference.
    /// </summary>
    [SuppressMessage("ReSharper", "UnusedParameter.Local")]
    public sealed class FieldRef
    {
        /// <summary>
        /// Constructs a field reference.
        /// </summary>
        /// <param name="type">The field type.</param>
        /// <param name="fieldName">The field name.</param>
        public FieldRef(TypeRef type, string fieldName)
            => IL.Throw();
    }
}
