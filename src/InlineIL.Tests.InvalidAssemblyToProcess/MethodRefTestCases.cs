using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using static InlineIL.IL.Emit;

namespace InlineIL.Tests.InvalidAssemblyToProcess
{
    [SuppressMessage("ReSharper", "UnusedType.Global")]
    [SuppressMessage("ReSharper", "UnusedMember.Global")]
    [SuppressMessage("ReSharper", "UnusedParameter.Local")]
    [SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
    [SuppressMessage("ReSharper", "EventNeverSubscribedTo.Global")]
    [SuppressMessage("ReSharper", "ConvertToLambdaExpression")]
    public class MethodRefTestCases
    {
        public int Value { get; set; }

        public int ValueGetOnly => Value;

        public int ValueSetOnly
        {
            set => Value = value;
        }

        public event Action? Event;

        public void UnknownMethodWithoutParams()
        {
            Call(new MethodRef(typeof(object), "Nope"));
        }

        public void UnknownMethodWithParams()
        {
            Call(new MethodRef(typeof(object), "Nope", typeof(int)));
        }

        public void AmbiguousMethod()
        {
            Call(new MethodRef(typeof(MethodRefTestCases), nameof(Foo)));
        }

        public void NullMethod()
        {
            Call(new MethodRef(typeof(MethodRefTestCases), null!));
        }

        public void NullMethodRef()
        {
            Call(null!);
        }

        public void UnusedInstance()
        {
            GC.KeepAlive(new MethodRef(typeof(int), "foo"));
        }

        public void NotAGenericMethod()
        {
            Call(new MethodRef(typeof(MethodRefTestCases), nameof(NotAGenericMethod)).MakeGenericMethod(typeof(int)));
        }

        public void NoGenericArgsProvided()
        {
            Call(new MethodRef(typeof(MethodRefTestCases), nameof(GenericMethod)).MakeGenericMethod());
        }

        public void TooManyGenericArgsProvided()
        {
            Call(new MethodRef(typeof(MethodRefTestCases), nameof(GenericMethod)).MakeGenericMethod(typeof(int), typeof(string)));
        }

        public void NoMatchingGenericOverload()
        {
            Call(new MethodRef(typeof(MethodRefTestCases), nameof(GenericMethod), typeof(string)).MakeGenericMethod(typeof(int)));
        }

        public void TypeGenericArgOutsideOfGenericType()
        {
            Call(new MethodRef(typeof(MethodRefTestCases), nameof(GenericMethod), TypeRef.TypeGenericParameters[42]).MakeGenericMethod(typeof(int)));
        }

        public void TypeGenericArgOutOfBounds()
        {
            Call(new MethodRef(typeof(GenericType<int>), nameof(GenericType<int>.Method), TypeRef.TypeGenericParameters[42]).MakeGenericMethod(typeof(int)));
        }

        public void MethodGenericArgOutOfBounds()
        {
            Call(new MethodRef(typeof(MethodRefTestCases), nameof(GenericMethod), TypeRef.MethodGenericParameters[42]).MakeGenericMethod(typeof(int)));
        }

        public void NoMatchingGenericOverloadArray()
        {
            Call(new MethodRef(typeof(MethodRefTestCases), nameof(GenericMethod), typeof(string).MakeArrayType()).MakeGenericMethod(typeof(int)));
        }

        public void NoMatchingGenericOverloadArray2()
        {
            Call(new MethodRef(typeof(MethodRefTestCases), nameof(GenericMethod), typeof(string).MakeArrayType(2)).MakeGenericMethod(typeof(int)));
        }

        public void NoMatchingGenericOverloadByRef()
        {
            Call(new MethodRef(typeof(MethodRefTestCases), nameof(GenericMethod), typeof(string).MakeByRefType()).MakeGenericMethod(typeof(int)));
        }

        public void NoMatchingGenericOverloadPointer()
        {
            Call(new MethodRef(typeof(MethodRefTestCases), nameof(GenericMethod), typeof(int).MakePointerType()).MakeGenericMethod(typeof(int)));
        }

        public void NoMatchingGenericOverloadGeneric()
        {
            Call(new MethodRef(typeof(MethodRefTestCases), nameof(GenericMethod), typeof(Dictionary<,>).MakeGenericType(typeof(int), typeof(string))).MakeGenericMethod(typeof(int)));
        }

        public void NotAVarArgMethod()
        {
            Call(new MethodRef(typeof(MethodRefTestCases), nameof(NotAVarArgMethod)).WithOptionalParameters(typeof(int)));
        }

        public void VarArgParamsAlreadySupplied()
        {
            Call(new MethodRef(typeof(MethodRefTestCases), nameof(VarArgMethod)).WithOptionalParameters(typeof(int)).WithOptionalParameters(typeof(int)));
        }

        public void EmptyVarArgParams()
        {
            Call(new MethodRef(typeof(MethodRefTestCases), nameof(VarArgMethod)).WithOptionalParameters());
        }

        public void UnknownProperty()
        {
            Call(MethodRef.PropertyGet(typeof(MethodRefTestCases), "Nope"));
        }

        public void PropertyWithoutGetter()
        {
            Call(MethodRef.PropertyGet(typeof(MethodRefTestCases), nameof(ValueSetOnly)));
        }

        public void PropertyWithoutSetter()
        {
            Call(MethodRef.PropertySet(typeof(MethodRefTestCases), nameof(ValueGetOnly)));
        }

        public void EventWithoutInvoker()
        {
            Call(MethodRef.EventRaise(typeof(MethodRefTestCases), nameof(Event)));
        }

        public void UnknownConstructor()
        {
            Call(MethodRef.Constructor(typeof(MethodRefTestCases), typeof(Guid), typeof(Guid)));
        }

        public void NoDefaultConstructor()
        {
            Call(MethodRef.Constructor(typeof(ClassWithNoDefaultConstructor)));
        }

        public void NoTypeInitializer()
        {
            Call(MethodRef.TypeInitializer(typeof(ClassWithoutInitializer)));
        }

        public void StaticLambdaFromDelegate()
        {
            Call(MethodRef.FromDelegate<Func<int, int>>(i => i * 2));
        }

        public void NonStaticLambdaFromDelegate(int i)
        {
            Call(MethodRef.FromDelegate<Func<int>>(() => i * 2));
        }

        public void StaticLocalFunctionFromDelegate()
        {
            Call(MethodRef.FromDelegate<Func<int, int>>(LocalFunction));
            static int LocalFunction(int i) => i * 2;
        }

        public void NonStaticLocalFunctionFromDelegate(int i)
        {
            Call(MethodRef.FromDelegate<Func<int>>(LocalFunction));
            int LocalFunction() => i * 2;
        }

        public void StaticDelegateFromDelegate()
        {
            Call(MethodRef.FromDelegate<Func<int, int>>(delegate(int i) { return i * 2; }));
        }

        public void NonStaticDelegateFromDelegate(int i)
        {
            Call(MethodRef.FromDelegate<Func<int>>(delegate { return i * 2; }));
        }

        public void InvalidEnumValue()
        {
            Call(MethodRef.Operator(typeof(decimal), (UnaryOperator)(-1)));
        }

        public void InvalidUnaryOperator()
        {
            Call(MethodRef.Operator(typeof(ClassWithoutInitializer), UnaryOperator.Decrement));
        }

        public void InvalidBinaryOperator()
        {
            Call(MethodRef.Operator(typeof(ClassWithoutInitializer), BinaryOperator.Addition, typeof(ClassWithoutInitializer), typeof(int)));
        }

        public void InvalidConversionOperator()
        {
            Call(MethodRef.Operator(typeof(ClassWithoutInitializer), ConversionOperator.Implicit, ConversionDirection.From, typeof(int)));
        }

        private static void Foo()
        {
        }

        private static void Foo(int i)
        {
        }

        private static T GenericMethod<T>(T value) => value;

        private static int[]? VarArgMethod(int count, __arglist) => null;

        private class ClassWithoutInitializer
        {
        }

        private class ClassWithNoDefaultConstructor
        {
            public ClassWithNoDefaultConstructor(int foo)
            {
            }
        }

        private class GenericType<T>
        {
            public static void Method(T arg)
            {
            }
        }
    }
}
