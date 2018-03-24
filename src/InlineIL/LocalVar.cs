using System.Diagnostics.CodeAnalysis;

namespace InlineIL
{
    /// <summary>
    /// Represents a local variable declaration, for use with <see cref="IL.DeclareLocals(InlineIL.LocalVar[])" />.
    /// </summary>
    [SuppressMessage("ReSharper", "UnusedParameter.Local")]
    [SuppressMessage("ReSharper", "MemberCanBeMadeStatic.Global")]
    public sealed class LocalVar
    {
        /// <summary>
        /// Constructs a named local variable, which can either be accessed by name through <see cref="LocalRef"/> or by index.
        /// </summary>
        /// <param name="localName">The local variable name.</param>
        /// <param name="type">The local variable type.</param>
        public LocalVar(string localName, TypeRef type)
            => IL.Throw();

        /// <summary>
        /// Constructs an anonymous local variable, which can be accessed by index.
        /// </summary>
        /// <param name="type">The local variable type.</param>
        public LocalVar(TypeRef type)
            => IL.Throw();

        /// <summary>
        /// Makes the object referred to by the local pinned in memory, which prevents the garbage collector from moving it.
        /// </summary>
        /// <returns>The local variable declaration.</returns>
        public LocalVar Pinned()
            => throw IL.Throw();
    }
}
