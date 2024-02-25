using System;
using System.Diagnostics.CodeAnalysis;

namespace InlineIL;

/// <summary>
/// Represents a type reference.
/// </summary>
/// <remarks>
/// This class is implicitly convertible from <see cref="System.Type"/>.
/// </remarks>
[SuppressMessage("ReSharper", "UnusedParameter.Local")]
[SuppressMessage("ReSharper", "MemberCanBeMadeStatic.Global")]
[SuppressMessage("ReSharper", "UnusedParameter.Global")]
public sealed class TypeRef
{
    /// <summary>
    /// Returns the core library name, for use with <see cref="TypeRef(string, string)"/>.
    /// </summary>
    public static string CoreLibrary
        => throw IL.Throw();

    /// <summary>
    /// Generic parameters of the declaring type, for overload resolution in <see cref="MethodRef"/>.
    /// </summary>
    /// <remarks>
    /// Generic parameters of a nested type come after generic parameters of its enclosing type.
    /// </remarks>
    public static GenericParameters TypeGenericParameters
        => throw IL.Throw();

    /// <summary>
    /// Generic parameters of the method, for overload resolution in <see cref="MethodRef"/>.
    /// </summary>
    public static GenericParameters MethodGenericParameters
        => throw IL.Throw();

    /// <summary>
    /// Constructs a type reference from a <see cref="System.Type"/>.
    /// </summary>
    /// <param name="type">The type to reference.</param>
    public static TypeRef Type(Type type)
        => throw IL.Throw();

    /// <summary>
    /// Constructs a type reference from a <see cref="System.Type"/>.
    /// </summary>
    /// <typeparam name="T">The type to reference.</typeparam>
    public static TypeRef Type<T>()
        => throw IL.Throw();

    /// <summary>
    /// Constructs a type reference from a <see cref="System.Type"/>.
    /// </summary>
    /// <param name="type">The type to reference.</param>
    public TypeRef(Type type)
        => IL.Throw();

    /// <summary>
    /// Constructs a type reference.
    /// </summary>
    /// <param name="assemblyName">The assembly name containing the type. This assembly should be referenced by the weaved assembly.</param>
    /// <param name="typeName">The full runtime type name, as returned by <see cref="System.Type.FullName"/>.</param>
    public TypeRef(string assemblyName, string typeName)
        => IL.Throw();

    /// <summary>
    /// Converts a <see cref="System.Type"/> to a <see cref="TypeRef"/>.
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
    /// Returns a type that represents an array of the current type, with the specified number of dimensions.
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

    /// <summary>
    /// Returns a type with an applied custom optional modifier (<c>modopt</c>).
    /// </summary>
    /// <param name="modifierType">The custom modifier type.</param>
    /// <returns>A <see cref="TypeRef"/> with the custom modifier applied.</returns>
    public TypeRef WithOptionalModifier(TypeRef modifierType)
        => throw IL.Throw();

    /// <summary>
    /// Returns a type with an applied custom required modifier (<c>modreq</c>).
    /// </summary>
    /// <param name="modifierType">The custom modifier type.</param>
    /// <returns>A <see cref="TypeRef"/> with the custom modifier applied.</returns>
    public TypeRef WithRequiredModifier(TypeRef modifierType)
        => throw IL.Throw();

    /// <summary>
    /// EXPERIMENTAL API - Returns a reference to a type from an assembly specified by its full path,
    /// adding a reference to that assembly name if required.
    /// </summary>
    /// <param name="assemblyPath">The full path to an assembly file.</param>
    /// <param name="typeName">The full runtime type name, as returned by <see cref="System.Type.FullName"/>.</param>
    /// <returns>A <see cref="TypeRef"/> to the given type.</returns>
    [Obsolete("This is an experimental API.")]
    public static TypeRef FromDll(string assemblyPath, string typeName)
        => throw IL.Throw();
}
