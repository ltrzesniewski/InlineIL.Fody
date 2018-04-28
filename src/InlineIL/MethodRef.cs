using System.Diagnostics.CodeAnalysis;

namespace InlineIL
{
    /// <summary>
    /// Represents a method reference.
    /// </summary>
    [SuppressMessage("ReSharper", "UnusedParameter.Local")]
    [SuppressMessage("ReSharper", "UnusedParameter.Global")]
    [SuppressMessage("ReSharper", "MemberCanBeMadeStatic.Global")]
    public sealed class MethodRef
    {
        /// <summary>
        /// Constructs a method reference. If the method is overloaded, use <see cref="MethodRef(TypeRef, string, TypeRef[])" /> instead.
        /// </summary>
        /// <param name="type">The type declaring the method.</param>
        /// <param name="methodName">The method name.</param>
        public MethodRef(TypeRef type, string methodName)
            => IL.Throw();

        /// <summary>
        /// Constructs a method reference
        /// </summary>
        /// <param name="type">The type declaring the method.</param>
        /// <param name="methodName">The method name.</param>
        /// <param name="parameterTypes">The types of the method parameters.</param>
        public MethodRef(TypeRef type, string methodName, params TypeRef[] parameterTypes)
            => IL.Throw();

        /// <summary>
        /// Constructs a method reference to a property getter.
        /// </summary>
        /// <param name="type">The type declaring the property.</param>
        /// <param name="propertyName">The property name.</param>
        public static MethodRef PropertyGetter(TypeRef type, string propertyName)
            => throw IL.Throw();

        /// <summary>
        /// Constructs a method reference to a property setter.
        /// </summary>
        /// <param name="type">The type declaring the property.</param>
        /// <param name="propertyName">The property name.</param>
        public static MethodRef PropertySetter(TypeRef type, string propertyName)
            => throw IL.Throw();

        /// <summary>
        /// Returns a reference to a constructed generic method, using the supplied generic parameter types.
        /// </summary>
        /// <param name="genericParameterTypes">An array of type references to be substituted for the type parameters of the current generic method.</param>
        /// <returns>A reference to a generic method instance.</returns>
        public MethodRef MakeGenericMethod(params TypeRef[] genericParameterTypes)
            => throw IL.Throw();

        /// <summary>
        /// Returns a reference to a varargs method instantiation, using the supplied optional parameter types.
        /// </summary>
        /// <param name="optionalParameterTypes">The optional parameter types.</param>
        /// <returns>A reference to a varargs method instance.</returns>
        public MethodRef WithOptionalParameters(params TypeRef[] optionalParameterTypes)
            => throw IL.Throw();
    }
}
