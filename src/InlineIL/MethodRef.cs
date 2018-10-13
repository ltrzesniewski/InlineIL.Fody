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
        public static MethodRef PropertyGet(TypeRef type, string propertyName)
            => throw IL.Throw();

        /// <summary>
        /// Constructs a method reference to a property setter.
        /// </summary>
        /// <param name="type">The type declaring the property.</param>
        /// <param name="propertyName">The property name.</param>
        public static MethodRef PropertySet(TypeRef type, string propertyName)
            => throw IL.Throw();

        /// <summary>
        /// Constructs a method reference to the add method of an event.
        /// </summary>
        /// <param name="type">The type declaring the event.</param>
        /// <param name="eventName">The event name.</param>
        public static MethodRef EventAdd(TypeRef type, string eventName)
            => throw IL.Throw();

        /// <summary>
        /// Constructs a method reference to the remove method of an event.
        /// </summary>
        /// <param name="type">The type declaring the event.</param>
        /// <param name="eventName">The event name.</param>
        public static MethodRef EventRemove(TypeRef type, string eventName)
            => throw IL.Throw();

        /// <summary>
        /// Constructs a method reference to the raise method of an event.
        /// </summary>
        /// <param name="type">The type declaring the event.</param>
        /// <param name="eventName">The event name.</param>
        public static MethodRef EventRaise(TypeRef type, string eventName)
            => throw IL.Throw();

        /// <summary>
        /// Constructs a method reference to a constructor.
        /// </summary>
        /// <param name="type">The type declaring the constructor.</param>
        /// <param name="parameterTypes">The types of the constructor parameters.</param>
        public static MethodRef Constructor(TypeRef type, params TypeRef[] parameterTypes)
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
