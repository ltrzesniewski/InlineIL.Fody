using System.Diagnostics.CodeAnalysis;

namespace InlineIL
{
    /// <summary>
    /// Represents a list of generic parameters.
    /// </summary>
    /// <remarks>
    /// Use the indexer to retrieve a given parameter.
    /// </remarks>
    [SuppressMessage("ReSharper", "ClassCannotBeInstantiated")]
    [SuppressMessage("ReSharper", "ClassNeverInstantiated.Global")]
    [SuppressMessage("ReSharper", "UnusedParameter.Global")]
    public sealed class GenericParameters
    {
        private GenericParameters()
            => IL.Throw();

        /// <summary>
        /// Gets the generic parameter type at the specified index.
        /// </summary>
        /// <param name="index">The index of the generic parameter type to get.</param>
        public TypeRef this[int index]
            => throw IL.Throw();
    }
}
