using System;
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
        /// Constructs a method reference for a non-overloaded method.
        /// </summary>
        /// <param name="type">The type declaring the method.</param>
        /// <param name="methodName">The method name.</param>
        public MethodRef(TypeRef type, string methodName)
            => IL.Throw();

        /// <summary>
        /// Constructs a method reference.
        /// </summary>
        /// <param name="type">The type declaring the method.</param>
        /// <param name="methodName">The method name.</param>
        /// <param name="parameterTypes">The types of the method parameters.</param>
        /// <remarks>
        /// Use the <see cref="TypeRef.TypeGenericParameters"/> and <see cref="TypeRef.MethodGenericParameters"/> static properties
        /// from the <see cref="TypeRef"/> class to represent generic parameter types.
        /// </remarks>
        public MethodRef(TypeRef type, string methodName, params TypeRef[] parameterTypes)
            => IL.Throw();

        /// <summary>
        /// Constructs a method reference.
        /// </summary>
        /// <param name="type">The type declaring the method.</param>
        /// <param name="methodName">The method name.</param>
        /// <param name="genericParameterCount">The generic parameter count. Use 0 for a non-generic method.</param>
        /// <param name="parameterTypes">The types of the method parameters.</param>
        /// <remarks>
        /// Use the <see cref="TypeRef.TypeGenericParameters"/> and <see cref="TypeRef.MethodGenericParameters"/> static properties
        /// from the <see cref="TypeRef"/> class to represent generic parameter types.
        /// </remarks>
        public MethodRef(TypeRef type, string methodName, int genericParameterCount, params TypeRef[] parameterTypes)
            => IL.Throw();

        /// <summary>
        /// Constructs a method reference.
        /// </summary>
        /// <param name="type">The type declaring the method.</param>
        /// <param name="methodName">The method name.</param>
        /// <param name="returnType">The return type.</param>
        /// <param name="genericParameterCount">The generic parameter count. Use 0 for a non-generic method.</param>
        /// <param name="parameterTypes">The types of the method parameters.</param>
        /// <remarks>
        /// Use the <see cref="TypeRef.TypeGenericParameters"/> and <see cref="TypeRef.MethodGenericParameters"/> static properties
        /// from the <see cref="TypeRef"/> class to represent generic parameter types.
        /// </remarks>
        public MethodRef(TypeRef type, string methodName, TypeRef returnType, int genericParameterCount, params TypeRef[] parameterTypes)
            => IL.Throw();

        /// <summary>
        /// Constructs a method reference for a non-overloaded method.
        /// </summary>
        /// <param name="type">The type declaring the method.</param>
        /// <param name="methodName">The method name.</param>
        public static MethodRef Method(TypeRef type, string methodName)
            => throw IL.Throw();

        /// <summary>
        /// Constructs a method reference.
        /// </summary>
        /// <param name="type">The type declaring the method.</param>
        /// <param name="methodName">The method name.</param>
        /// <param name="parameterTypes">The types of the method parameters.</param>
        /// <remarks>
        /// Use the <see cref="TypeRef.TypeGenericParameters"/> and <see cref="TypeRef.MethodGenericParameters"/> static properties
        /// from the <see cref="TypeRef"/> class to represent generic parameter types.
        /// </remarks>
        public static MethodRef Method(TypeRef type, string methodName, params TypeRef[] parameterTypes)
            => throw IL.Throw();

        /// <summary>
        /// Constructs a method reference.
        /// </summary>
        /// <param name="type">The type declaring the method.</param>
        /// <param name="methodName">The method name.</param>
        /// <param name="genericParameterCount">The generic parameter count. Use 0 for a non-generic method.</param>
        /// <param name="parameterTypes">The types of the method parameters.</param>
        /// <remarks>
        /// Use the <see cref="TypeRef.TypeGenericParameters"/> and <see cref="TypeRef.MethodGenericParameters"/> static properties
        /// from the <see cref="TypeRef"/> class to represent generic parameter types.
        /// </remarks>
        public static MethodRef Method(TypeRef type, string methodName, int genericParameterCount, params TypeRef[] parameterTypes)
            => throw IL.Throw();

        /// <summary>
        /// Constructs a method reference.
        /// </summary>
        /// <param name="type">The type declaring the method.</param>
        /// <param name="methodName">The method name.</param>
        /// <param name="returnType">The return type.</param>
        /// <param name="genericParameterCount">The generic parameter count. Use 0 for a non-generic method.</param>
        /// <param name="parameterTypes">The types of the method parameters.</param>
        /// <remarks>
        /// Use the <see cref="TypeRef.TypeGenericParameters"/> and <see cref="TypeRef.MethodGenericParameters"/> static properties
        /// from the <see cref="TypeRef"/> class to represent generic parameter types.
        /// </remarks>
        public static MethodRef Method(TypeRef type, string methodName, TypeRef returnType, int genericParameterCount, params TypeRef[] parameterTypes)
            => throw IL.Throw();

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
        /// Constructs a method reference to a constructor (<c>.ctor</c> method).
        /// </summary>
        /// <param name="type">The type declaring the constructor.</param>
        /// <param name="parameterTypes">The types of the constructor parameters.</param>
        /// <remarks>
        /// Use the <see cref="TypeRef.TypeGenericParameters"/> static property
        /// from the <see cref="TypeRef"/> class to represent generic parameter types.
        /// </remarks>
        public static MethodRef Constructor(TypeRef type, params TypeRef[] parameterTypes)
            => throw IL.Throw();

        /// <summary>
        /// Constructs a method reference to a type initializer (<c>.cctor</c> method).
        /// </summary>
        /// <param name="type">The type declaring the constructor.</param>
        public static MethodRef TypeInitializer(TypeRef type)
            => throw IL.Throw();

        /// <summary>
        /// Constructs a method reference to a unary operator.
        /// </summary>
        /// <param name="type">The type declaring the operator.</param>
        /// <param name="operator">The operator kind.</param>
        public static MethodRef Operator(TypeRef type, UnaryOperator @operator)
            => throw IL.Throw();

        /// <summary>
        /// Constructs a method reference to a binary operator.
        /// </summary>
        /// <param name="type">The type declaring the operator.</param>
        /// <param name="operator">The operator kind.</param>
        /// <param name="leftType">The left operand type.</param>
        /// <param name="rightType">The right operand type.</param>
        public static MethodRef Operator(TypeRef type, BinaryOperator @operator, TypeRef leftType, TypeRef rightType)
            => throw IL.Throw();

        /// <summary>
        /// Constructs a method reference to a conversion operator.
        /// </summary>
        /// <param name="type">The type declaring the operator.</param>
        /// <param name="operator">The operator kind.</param>
        /// <param name="direction">The direction of the conversion.</param>
        /// <param name="otherType">The other type of the conversion.</param>
        public static MethodRef Operator(TypeRef type, ConversionOperator @operator, ConversionDirection direction, TypeRef otherType)
            => throw IL.Throw();

        /// <summary>
        /// Constructs a method reference from a delegate.
        /// </summary>
        /// <param name="delegate">A delegate which references the method.</param>
        /// <typeparam name="TDelegate">Type of the delegate.</typeparam>
        /// <remarks>
        /// Do not use with lambdas, local functions, or any other compiler-generated methods.
        /// </remarks>
        public static MethodRef FromDelegate<TDelegate>(TDelegate @delegate)
            where TDelegate : Delegate
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
