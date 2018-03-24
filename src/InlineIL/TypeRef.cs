using System;
using System.Diagnostics.CodeAnalysis;

namespace InlineIL
{
    /// <summary>
    /// Represents a type reference. This class is implicitly convertible from <see cref="Type"/>.
    /// </summary>
    [SuppressMessage("ReSharper", "UnusedParameter.Local")]
    [SuppressMessage("ReSharper", "MemberCanBeMadeStatic.Global")]
    [SuppressMessage("ReSharper", "UnusedParameter.Global")]
    public sealed class TypeRef
    {
        /// <summary>
        /// Constructs a type reference from a <see cref="Type"/>.
        /// </summary>
        /// <param name="type">The type to reference.</param>
        public TypeRef(Type type)
            => IL.Throw();

        /// <summary>
        /// Constructs a type reference.
        /// </summary>
        /// <param name="assemblyName">The assembly name containing the type. This assembly should be referenced by the weaved assembly.</param>
        /// <param name="typeName">The full runtime type name, as returned by <see cref="Type.FullName"/>.</param>
        public TypeRef(string assemblyName, string typeName)
            => IL.Throw();

        /// <summary>
        /// Converts a <see cref="Type"/> to a <see cref="TypeRef"/>.
        /// </summary>
        /// <param name="type">The type to reference.</param>
        public static implicit operator TypeRef(Type type)
            => throw IL.Throw();

        /// <summary>
        /// Returns a type that represents a pointer to the current type.
        /// </summary>
        /// <returns>A <see cref="TypeRef"/> that represents a pointer to the current type.</returns>
        public TypeRef MakePointerType()
            => throw IL.Throw();

        /// <summary>
        /// Returns a type that represents a reference to the current type.
        /// </summary>
        /// <returns>A <see cref="TypeRef"/> that represents a reference to the current type.</returns>
        public TypeRef MakeByRefType()
            => throw IL.Throw();

        /// <summary>
        /// Returns a type that represents a one-dimensional array of the current type.
        /// </summary>
        /// <returns>A <see cref="TypeRef"/> that represents a one-dimensional array of the current type.</returns>
        public TypeRef MakeArrayType()
            => throw IL.Throw();

        /// <summary>
        /// Returns a type that represents an array of the current type, with the specifiend number of dimensions.
        /// </summary>
        /// <param name="rank">The number of dimensions for the array.</param>
        /// <returns>A <see cref="TypeRef"/> that represents an array of the current type.</returns>
        public TypeRef MakeArrayType(int rank)
            => throw IL.Throw();

        /// <summary>
        /// Returns a type that represents a constructed generic type.
        /// </summary>
        /// <param name="typeArguments">An array of type references to be substituted for the type parameters of the current generic type.</param>
        /// <returns>A <see cref="TypeRef"/> that represents a constructed generic type.</returns>
        public TypeRef MakeGenericType(params TypeRef[] typeArguments)
            => throw IL.Throw();
    }
}
