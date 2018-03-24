using System.Diagnostics.CodeAnalysis;

namespace InlineIL
{
    /// <summary>
    /// Represents a named local variable reference, for use with <see cref="IL.Emit(System.Reflection.Emit.OpCode, LocalRef)" />.
    /// </summary>
    [SuppressMessage("ReSharper", "UnusedParameter.Local")]
    public sealed class LocalRef
    {
        /// <summary>
        /// Constructs a local variable reference.
        /// </summary>
        /// <param name="localName">The local variable name.</param>
        public LocalRef(string localName)
            => IL.Throw();
    }
}
