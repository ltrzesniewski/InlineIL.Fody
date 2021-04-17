using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;
using InlineIL.Tests.Common;
using static InlineIL.IL.Emit;

namespace InlineIL.Tests.AssemblyToProcess
{
    [SuppressMessage("ReSharper", "UnusedType.Global")]
    [SuppressMessage("ReSharper", "UnusedMember.Global")]
    [SuppressMessage("ReSharper", "UnusedParameter.Local")]
    [SuppressMessage("ReSharper", "UnusedTypeParameter")]
    [SuppressMessage("ReSharper", "UnusedParameter.Global")]
    [SuppressMessage("ReSharper", "InlineOutVariableDeclaration")]
    [SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
    [SuppressMessage("ReSharper", "UnusedAutoPropertyAccessor.Global")]
    [SuppressMessage("ReSharper", "EventNeverSubscribedTo.Global")]
    [SuppressMessage("ReSharper", "UnusedMember.Local")]
    [SuppressMessage("ReSharper", "MemberCanBeMadeStatic.Local")]
    [SuppressMessage("ReSharper", "UnusedMemberInSuper.Global")]
    [SuppressMessage("ReSharper", "EntityNameCapturedOnly.Global")]
    public class MethodRefTestCases : IMethodRefTestCases
    {
        private readonly OtherClass _otherClass = new();
        private OtherStruct _otherStruct;

        public int Value { get; set; }

        public event Action? Event;

        public Type ReturnType<T>()
        {
            Ldtoken(typeof(T));
            Call(new MethodRef(typeof(Type), nameof(Type.GetTypeFromHandle)));
            Ret();
            throw IL.Unreachable();
        }

        public Type[] CallMethodDifferentWays()
        {
            var result = new Type[6];

            IL.Push(result);
            IL.Push(0);
            Ldtoken<int>();
            Call(new MethodRef(typeof(Type), nameof(Type.GetTypeFromHandle)));
            Stelem_Any<Type>();

            IL.Push(result);
            IL.Push(1);
            Ldtoken<int>();
            Call(new MethodRef(typeof(Type), nameof(Type.GetTypeFromHandle), typeof(RuntimeTypeHandle)));
            Stelem_Any<Type>();

            IL.Push(result);
            IL.Push(2);
            Ldtoken<int>();
            Call(new MethodRef(typeof(Type), nameof(Type.GetTypeFromHandle), 0, typeof(RuntimeTypeHandle)));
            Stelem_Any<Type>();

            IL.Push(result);
            IL.Push(3);
            Ldtoken<int>();
            Call(MethodRef.Method(typeof(Type), nameof(Type.GetTypeFromHandle)));
            Stelem_Any<Type>();

            IL.Push(result);
            IL.Push(4);
            Ldtoken<int>();
            Call(MethodRef.Method(typeof(Type), nameof(Type.GetTypeFromHandle), typeof(RuntimeTypeHandle)));
            Stelem_Any<Type>();

            IL.Push(result);
            IL.Push(5);
            Ldtoken<int>();
            Call(MethodRef.Method(typeof(Type), nameof(Type.GetTypeFromHandle), 0, typeof(RuntimeTypeHandle)));
            Stelem_Any<Type>();

            return result;
        }

        public int[] ResolveOverloads()
        {
            var result = new int[7];

            IL.Push(result);
            Ldc_I4_0();
            Call(new MethodRef(typeof(MethodRefTestCases), nameof(OverloadedMethod), new TypeRef[0]));
            Stelem_I4();

            IL.Push(result);
            Ldc_I4_1();
            Call(new MethodRef(typeof(MethodRefTestCases), nameof(OverloadedMethod), Array.Empty<TypeRef>()));
            Stelem_I4();

            IL.Push(result);
            Ldc_I4_2();
            Ldc_I4(42);
            Call(new MethodRef(typeof(MethodRefTestCases), nameof(OverloadedMethod), typeof(int)));
            Stelem_I4();

            IL.Push(result);
            Ldc_I4_3();
            IL.Push(result);
            Ldc_I4_0();
            Ldelema(typeof(int));
            Call(new MethodRef(typeof(MethodRefTestCases), nameof(OverloadedMethod), new TypeRef(typeof(int)).MakeByRefType()));
            Stelem_I4();

            IL.Push(result);
            Ldc_I4_4();
            Ldnull();
            Call(new MethodRef(typeof(MethodRefTestCases), nameof(OverloadedMethod), typeof(int[])));
            Stelem_I4();

            IL.Push(result);
            Ldc_I4_5();
            Ldc_R8(42.0);
            IL.Push(result);
            Ldc_I4_0();
            Ldelema(typeof(int));
            Call(new MethodRef(typeof(MethodRefTestCases), nameof(OverloadedMethod), typeof(double), new TypeRef(typeof(int)).MakeByRefType()));
            Stelem_I4();

            IL.Push(result);
            Ldc_I4_6();
            Ldc_R8(42.0);
            Ldc_I4(42);
            Call(new MethodRef(typeof(MethodRefTestCases), nameof(OverloadedMethod), typeof(double), typeof(int)));
            Stelem_I4();

            IL.Push(result);
            return IL.Return<int[]>();
        }

        public int[] ResolveGenericOverloads()
        {
            var result = new List<int>();
            int item;

            Ldc_I4_0();
            Ldc_I4_0();
            Call(new MethodRef(typeof(MethodRefTestCases), nameof(GenericOverload), TypeRef.MethodGenericParameters[0], typeof(int)).MakeGenericMethod(typeof(int)));
            IL.Pop(out item);
            result.Add(item);

            Ldc_I4_0();
            Ldc_I4_0();
            Call(new MethodRef(typeof(MethodRefTestCases), nameof(GenericOverload), 1, TypeRef.MethodGenericParameters[0], typeof(uint)).MakeGenericMethod(typeof(int)));
            IL.Pop(out item);
            result.Add(item);

            Ldc_I4_0();
            Ldc_I4_0();
            Call(new MethodRef(typeof(MethodRefTestCases), nameof(GenericOverload), 2, TypeRef.MethodGenericParameters[0], typeof(uint)).MakeGenericMethod(typeof(int), typeof(int)));
            IL.Pop(out item);
            result.Add(item);

            Ldc_I4_0();
            Ldc_I4_0();
            Call(new MethodRef(typeof(MethodRefTestCases), nameof(GenericOverload), TypeRef.MethodGenericParameters[0], TypeRef.MethodGenericParameters[1]).MakeGenericMethod(typeof(int), typeof(int)));
            IL.Pop(out item);
            result.Add(item);

            Ldc_I4_0();
            Ldc_I4_0();
            Call(new MethodRef(typeof(MethodRefTestCases), nameof(GenericOverload), TypeRef.MethodGenericParameters[1], TypeRef.MethodGenericParameters[0]).MakeGenericMethod(typeof(int), typeof(int)));
            IL.Pop(out item);
            result.Add(item);

            Ldc_I4_0();
            Ldc_I4_0();
            Call(new MethodRef(typeof(MethodRefTestCases), nameof(GenericOverload), typeof(int), typeof(int)));
            IL.Pop(out item);
            result.Add(item);

            Ldc_I4_0();
            Ldc_I4_0();
            Call(new MethodRef(typeof(MethodRefTestCases), nameof(GenericOverload), 0, typeof(int), typeof(int)));
            IL.Pop(out item);
            result.Add(item);

            Ldnull();
            Ldnull();
            Call(new MethodRef(typeof(MethodRefTestCases), nameof(GenericOverload), TypeRef.MethodGenericParameters[0].MakeArrayType(), new TypeRef(typeof(List<>)).MakeGenericType(TypeRef.MethodGenericParameters[1])).MakeGenericMethod(typeof(int), typeof(int)));
            IL.Pop(out item);
            result.Add(item);

            return result.ToArray();
        }

#if NETCOREAPP
        public int[] ResolveGenericOverloadsUsingTypeApi()
        {
            var result = new List<int>();
            int item;

            Ldc_I4_0();
            Ldc_I4_0();
            Call(new MethodRef(typeof(MethodRefTestCases), nameof(GenericOverload), Type.MakeGenericMethodParameter(0), typeof(int)).MakeGenericMethod(typeof(int)));
            IL.Pop(out item);
            result.Add(item);

            Ldc_I4_0();
            Ldc_I4_0();
            Call(new MethodRef(typeof(MethodRefTestCases), nameof(GenericOverload), 1, Type.MakeGenericMethodParameter(0), typeof(uint)).MakeGenericMethod(typeof(int)));
            IL.Pop(out item);
            result.Add(item);

            Ldc_I4_0();
            Ldc_I4_0();
            Call(new MethodRef(typeof(MethodRefTestCases), nameof(GenericOverload), 2, Type.MakeGenericMethodParameter(0), typeof(uint)).MakeGenericMethod(typeof(int), typeof(int)));
            IL.Pop(out item);
            result.Add(item);

            Ldc_I4_0();
            Ldc_I4_0();
            Call(new MethodRef(typeof(MethodRefTestCases), nameof(GenericOverload), Type.MakeGenericMethodParameter(0), Type.MakeGenericMethodParameter(1)).MakeGenericMethod(typeof(int), typeof(int)));
            IL.Pop(out item);
            result.Add(item);

            Ldc_I4_0();
            Ldc_I4_0();
            Call(new MethodRef(typeof(MethodRefTestCases), nameof(GenericOverload), Type.MakeGenericMethodParameter(1), Type.MakeGenericMethodParameter(0)).MakeGenericMethod(typeof(int), typeof(int)));
            IL.Pop(out item);
            result.Add(item);

            Ldc_I4_0();
            Ldc_I4_0();
            Call(new MethodRef(typeof(MethodRefTestCases), nameof(GenericOverload), typeof(int), typeof(int)));
            IL.Pop(out item);
            result.Add(item);

            Ldc_I4_0();
            Ldc_I4_0();
            Call(new MethodRef(typeof(MethodRefTestCases), nameof(GenericOverload), 0, typeof(int), typeof(int)));
            IL.Pop(out item);
            result.Add(item);

            Ldnull();
            Ldnull();
            Call(new MethodRef(typeof(MethodRefTestCases), nameof(GenericOverload), Type.MakeGenericMethodParameter(0).MakeArrayType(), new TypeRef(typeof(List<>)).MakeGenericType(Type.MakeGenericMethodParameter(1))).MakeGenericMethod(typeof(int), typeof(int)));
            IL.Pop(out item);
            result.Add(item);

            return result.ToArray();
        }

#endif

        public int[] ResolveGenericOverloadsInGenericType()
        {
            var result = new List<int>();
            int item;

            Ldc_I4_0();
            Ldc_I4_0();
            Call(new MethodRef(typeof(GenericType<int>.NestedGenericType<int>), nameof(GenericType<int>.NestedGenericType<int>.GenericOverload), TypeRef.TypeGenericParameters[0], TypeRef.TypeGenericParameters[1]).MakeGenericMethod(typeof(int)));
            IL.Pop(out item);
            result.Add(item);

            Ldc_I4_0();
            Ldc_I4_0();
            Call(new MethodRef(typeof(GenericType<int>.NestedGenericType<int>), nameof(GenericType<int>.NestedGenericType<int>.GenericOverload), TypeRef.TypeGenericParameters[1], TypeRef.TypeGenericParameters[0]).MakeGenericMethod(typeof(int)));
            IL.Pop(out item);
            result.Add(item);

            Ldc_I4_0();
            Ldc_I4_0();
            Call(new MethodRef(typeof(GenericType<int>.NestedGenericType<int>), nameof(GenericType<int>.NestedGenericType<int>.GenericOverload), TypeRef.TypeGenericParameters[0], TypeRef.MethodGenericParameters[0]).MakeGenericMethod(typeof(int)));
            IL.Pop(out item);
            result.Add(item);

            Ldnull();
            Ldnull();
            Call(new MethodRef(typeof(GenericType<int>.NestedGenericType<int>), nameof(GenericType<int>.NestedGenericType<int>.GenericOverload), TypeRef.TypeGenericParameters[0].MakeArrayType(), new TypeRef(typeof(List<>)).MakeGenericType(TypeRef.TypeGenericParameters[1])).MakeGenericMethod(typeof(int)));
            IL.Pop(out item);
            result.Add(item);

            Ldnull();
            Ldnull();
            Call(new MethodRef(typeof(GenericType<int>.NestedGenericType<int>), nameof(GenericType<int>.NestedGenericType<int>.GenericOverload), TypeRef.MethodGenericParameters[0].MakeArrayType(), new TypeRef(typeof(List<>)).MakeGenericType(TypeRef.MethodGenericParameters[0])).MakeGenericMethod(typeof(int)));
            IL.Pop(out item);
            result.Add(item);

            return result.ToArray();
        }

        public int CallGenericMethod()
        {
            IL.Push(42);
            Call(new MethodRef(typeof(MethodRefTestCases), nameof(GenericMethod)).MakeGenericMethod(typeof(int)));
            return IL.Return<int>();
        }

        public string CallMethodInGenericType()
        {
            Call(new MethodRef(typeof(GenericType<>).MakeGenericType(typeof(Guid)), nameof(GenericType<object>.NormalMethod)));
            return IL.Return<string>();
        }

        public string CallMethodInGenericTypeArray()
        {
            Call(new MethodRef(typeof(GenericType<>).MakeGenericType(typeof(Guid[])), nameof(GenericType<object>.NormalMethod)));
            return IL.Return<string>();
        }

        public string CallMethodInGenericTypeGeneric<TClass>()
        {
            Call(new MethodRef(typeof(GenericType<>).MakeGenericType(typeof(TClass)), nameof(GenericType<object>.NormalMethod)));
            return IL.Return<string>();
        }

        public string CallGenericMethodInGenericType()
        {
            Call(new MethodRef(typeof(GenericType<>).MakeGenericType(typeof(Guid)), nameof(GenericType<object>.GenericMethod)).MakeGenericMethod(typeof(TimeSpan)));
            return IL.Return<string>();
        }

        public string CallGenericMethodInGenericTypeArray()
        {
            Call(new MethodRef(typeof(GenericType<>).MakeGenericType(typeof(Guid[])), nameof(GenericType<object>.GenericMethod)).MakeGenericMethod(typeof(TimeSpan[])));
            return IL.Return<string>();
        }

        public string CallGenericMethodInGenericTypeGeneric<TClass, TMethod>()
        {
            Call(new MethodRef(typeof(GenericType<>).MakeGenericType(typeof(TClass)), nameof(GenericType<object>.GenericMethod)).MakeGenericMethod(typeof(TMethod)));
            return IL.Return<string>();
        }

        public bool CallCoreLibMethod<T>(T? value)
            where T : struct
        {
            Ldarga(1);
            Call(new MethodRef(typeof(T?), "get_HasValue"));
            return IL.Return<bool>();
        }

        public RuntimeMethodHandle ReturnMethodHandle()
        {
            Ldtoken(new MethodRef(typeof(Type), nameof(Type.GetTypeFromHandle)));
            return IL.Return<RuntimeMethodHandle>();
        }

        public int GetValue()
        {
            Ldarg_0();
            Call(MethodRef.PropertyGet(typeof(MethodRefTestCases), nameof(Value)));
            return IL.Return<int>();
        }

        public void SetValue(int value)
        {
            Ldarg_0();
            Ldarg(nameof(value));
            Call(MethodRef.PropertySet(typeof(MethodRefTestCases), nameof(Value)));
        }

        public void AddEvent(Action callback)
        {
            Ldarg_0();
            Ldarg(nameof(callback));
            Call(MethodRef.EventAdd(typeof(MethodRefTestCases), nameof(Event)));
        }

        public void RemoveEvent(Action callback)
        {
            Ldarg_0();
            Ldarg(nameof(callback));
            Call(MethodRef.EventRemove(typeof(MethodRefTestCases), nameof(Event)));
        }

        public StringBuilder CallDefaultConstructor()
        {
            Newobj(MethodRef.Constructor(typeof(StringBuilder)));
            return IL.Return<StringBuilder>();
        }

        public StringBuilder CallNonDefaultConstructor()
        {
            Ldc_I4(42);
            Newobj(MethodRef.Constructor(typeof(StringBuilder), typeof(int)));
            return IL.Return<StringBuilder>();
        }

        public RuntimeMethodHandle GetTypeInitializer()
        {
            Ldtoken(MethodRef.TypeInitializer(typeof(TypeWithInitializer)));
            return IL.Return<RuntimeMethodHandle>();
        }

        public int CallStaticMethodFromDelegate()
        {
            Ldc_I4(42);
            Call(MethodRef.FromDelegate<Func<int, int>>(OverloadedMethod));
            return IL.Return<int>();
        }

        public int CallStaticMethodOfOtherClassFromDelegate()
        {
            Ldc_I4(42);
            Call(MethodRef.FromDelegate<Func<int, int>>(OtherClass.StaticMethod));
            return IL.Return<int>();
        }

        public int CallStaticMethodOfStructFromDelegate()
        {
            Ldc_I4(42);
            Call(MethodRef.FromDelegate<Func<int, int>>(OtherStruct.StaticMethod));
            return IL.Return<int>();
        }

        public int CallInstanceMethodFromDelegate()
        {
            Ldarg_0();
            Ldc_I4(42);
            Call(MethodRef.FromDelegate<Func<int, int>>(InstanceMethod));
            return IL.Return<int>();
        }

        public int CallInstanceMethodOfOtherClassFromDelegate()
        {
            OtherClass.Create(out var instance);
            IL.Push(instance);
            Ldc_I4(42);
            Call(MethodRef.FromDelegate<Func<int, int>>(instance.InstanceMethod));
            return IL.Return<int>();
        }

        public int CallInstanceMethodOfOtherClassThroughFieldFromDelegate()
        {
            IL.Push(_otherClass);
            Ldc_I4(42);
            Call(MethodRef.FromDelegate<Func<int, int>>(_otherClass.InstanceMethod));
            return IL.Return<int>();
        }

        public int CallVirtualMethodOfOtherClassFromDelegate()
        {
            var instance = new OtherClass();
            IL.Push(instance);
            Call(MethodRef.FromDelegate<Func<int>>(instance.VirtualMethod));
            return IL.Return<int>();
        }

        public int CallVirtualMethodOfOtherClassFromDelegate2()
        {
            OtherClass.Create(out var instance);
            IL.Push(instance);
            Call(MethodRef.FromDelegate<Func<int>>(instance.VirtualMethod));
            return IL.Return<int>();
        }

        public int CallVirtualMethodOverrideOfOtherClassFromDelegate()
        {
            var instance = new OtherClassDerived();
            IL.Push(instance);
            Call(MethodRef.FromDelegate<Func<int>>(instance.VirtualMethod));
            return IL.Return<int>();
        }

        public int CallVirtualMethodOverrideOfOtherClassFromDelegate2()
        {
            var instance = new OtherClassDerived();
            IL.Push(instance);
            Callvirt(MethodRef.FromDelegate<Func<int>>(instance.VirtualMethod));
            return IL.Return<int>();
        }

        public int CallInstanceMethodOfStructFromDelegate()
        {
            var value = new OtherStruct();
            IL.Push(ref value);
            Ldc_I4(42);
            Call(MethodRef.FromDelegate<Func<int, int>>(value.InstanceMethod));
            return IL.Return<int>();
        }

        public int CallInstanceMethodOfStructFromDelegate2()
        {
            var value = new OtherStruct();
            IL.Push(ref value);
            Ldc_I4(42);
            Call(MethodRef.FromDelegate<Func<int, int>>(default(OtherStruct).InstanceMethod));
            return IL.Return<int>();
        }

        public int CallInstanceMethodOfStructFromDelegate3()
        {
            var value = new OtherStruct();
            IL.Push(ref value);
            Ldc_I4(42);
            Call(MethodRef.FromDelegate<Func<int, int>>(new OtherStruct().InstanceMethod));
            return IL.Return<int>();
        }

        public int CallInstanceMethodOfStructFromDelegate4()
        {
            var value = new OtherStruct();
            return Run(ref value);

            static int Run(ref OtherStruct value)
            {
                IL.Push(ref value);
                Ldc_I4(42);
                Call(MethodRef.FromDelegate<Func<int, int>>(value.InstanceMethod));
                return IL.Return<int>();
            }
        }

        public int CallInterfaceMethodOfStructFromDelegate()
        {
            var value = new OtherStruct();
            IL.Push(ref value);
            Ldc_I4(42);
            Call(MethodRef.FromDelegate<Func<int, int>>(value.InterfaceMethod));
            return IL.Return<int>();
        }

        public string CallInstanceMethodOfStringFromDelegate()
        {
            IL.Push(" foo ");
            Call(MethodRef.FromDelegate<Func<string>>("bar".Trim));
            return IL.Return<string>();
        }

        public int CallInstanceMethodOfStructThroughFieldFromDelegate()
        {
            IL.Push(ref _otherStruct);
            Ldc_I4(42);
            Call(MethodRef.FromDelegate<Func<int, int>>(_otherStruct.InstanceMethod));
            return IL.Return<int>();
        }

        public string CallInstanceMethodOfInt32FromDelegate()
        {
            IL.Push((object)42);
            Callvirt(MethodRef.FromDelegate<Func<string>>(0.ToString));
            return IL.Return<string>();
        }

        public bool CallInstanceMethodOfInt32FromDelegate2()
        {
            var value = 42;
            IL.Push(ref value);
            IL.Push(42);
            Callvirt(MethodRef.FromDelegate<Func<int, bool>>(0.Equals));
            return IL.Return<bool>();
        }

        public unsafe string CallInstanceMethodOfInt32WithSizeofFromDelegate()
        {
            IL.Push((object)42);
            Callvirt(MethodRef.FromDelegate<Func<string>>(sizeof(OtherStruct).ToString));
            return IL.Return<string>();
        }

        public string CallInstanceMethodOfInt64FromDelegate()
        {
            IL.Push((object)42L);
            Callvirt(MethodRef.FromDelegate<Func<string>>(0L.ToString));
            return IL.Return<string>();
        }

        public string CallInstanceMethodOfInt64FromDelegate2()
        {
            IL.Push((object)424242424242L);
            Callvirt(MethodRef.FromDelegate<Func<string>>(0L.ToString));
            return IL.Return<string>();
        }

        public string CallInstanceMethodOfFloatFromDelegate()
        {
            IL.Push((object)42f);
            Callvirt(MethodRef.FromDelegate<Func<string>>(0f.ToString));
            return IL.Return<string>();
        }

        public string CallInstanceMethodOfDoubleFromDelegate()
        {
            IL.Push((object)42.0);
            Callvirt(MethodRef.FromDelegate<Func<string>>(0.0.ToString));
            return IL.Return<string>();
        }

        public string CallStaticMethodOfGenericClassFromDelegate()
        {
            Call(MethodRef.FromDelegate<Func<string>>(GenericType<string>.NormalMethod));
            return IL.Return<string>();
        }

        public string CallGenericStaticMethodOfGenericClassFromDelegate()
        {
            Call(MethodRef.FromDelegate<Func<string>>(GenericType<string>.GenericMethod<int>));
            return IL.Return<string>();
        }

        public int[] CallUnaryOperators()
        {
            var results = new List<int>();
            var obj = new UnaryOperatorsClass();

            IL.Push(obj);
            Call(MethodRef.Operator(typeof(UnaryOperatorsClass), UnaryOperator.Decrement));
            Pop();
            results.Add(obj.Value);

            IL.Push(obj);
            Call(MethodRef.Operator(typeof(UnaryOperatorsClass), UnaryOperator.Increment));
            Pop();
            results.Add(obj.Value);

            IL.Push(obj);
            Call(MethodRef.Operator(typeof(UnaryOperatorsClass), UnaryOperator.UnaryNegation));
            Pop();
            results.Add(obj.Value);

            IL.Push(obj);
            Call(MethodRef.Operator(typeof(UnaryOperatorsClass), UnaryOperator.UnaryPlus));
            Pop();
            results.Add(obj.Value);

            IL.Push(obj);
            Call(MethodRef.Operator(typeof(UnaryOperatorsClass), UnaryOperator.LogicalNot));
            Pop();
            results.Add(obj.Value);

            IL.Push(obj);
            Call(MethodRef.Operator(typeof(UnaryOperatorsClass), UnaryOperator.True));
            Pop();
            results.Add(obj.Value);

            IL.Push(obj);
            Call(MethodRef.Operator(typeof(UnaryOperatorsClass), UnaryOperator.False));
            Pop();
            results.Add(obj.Value);

            IL.Push(obj);
            Call(MethodRef.Operator(typeof(UnaryOperatorsClass), UnaryOperator.OnesComplement));
            Pop();
            results.Add(obj.Value);

            IL.Push(new GenericOperatorsClass<string>(0));
            Call(MethodRef.Operator(typeof(GenericOperatorsClass<string>), UnaryOperator.UnaryPlus));
            IL.Pop(out int intResult);
            results.Add(intResult);

            return results.ToArray();
        }

        public int[] CallBinaryOperators()
        {
            var results = new List<int>();

            IL.Push(new BinaryOperatorsClass());
            IL.Push(42);
            Call(MethodRef.Operator(typeof(BinaryOperatorsClass), BinaryOperator.Addition, typeof(BinaryOperatorsClass), typeof(int)));
            IL.Pop(out int result);
            results.Add(result);

            IL.Push(42);
            IL.Push(new BinaryOperatorsClass());
            Call(MethodRef.Operator(typeof(BinaryOperatorsClass), BinaryOperator.Addition, typeof(int), typeof(BinaryOperatorsClass)));
            IL.Pop(out result);
            results.Add(result);

            IL.Push(new BinaryOperatorsClass());
            IL.Push(new BinaryOperatorsClass());
            Call(MethodRef.Operator(typeof(BinaryOperatorsClass), BinaryOperator.Addition, typeof(BinaryOperatorsClass), typeof(BinaryOperatorsClass)));
            IL.Pop(out result);
            results.Add(result);

            IL.Push(new BinaryOperatorsClass());
            IL.Push(42);
            Call(MethodRef.Operator(typeof(BinaryOperatorsClass), BinaryOperator.Subtraction, typeof(BinaryOperatorsClass), typeof(int)));
            IL.Pop(out result);
            results.Add(result);

            IL.Push(new BinaryOperatorsClass());
            IL.Push(42);
            Call(MethodRef.Operator(typeof(BinaryOperatorsClass), BinaryOperator.Multiply, typeof(BinaryOperatorsClass), typeof(int)));
            IL.Pop(out result);
            results.Add(result);

            IL.Push(new BinaryOperatorsClass());
            IL.Push(42);
            Call(MethodRef.Operator(typeof(BinaryOperatorsClass), BinaryOperator.Division, typeof(BinaryOperatorsClass), typeof(int)));
            IL.Pop(out result);
            results.Add(result);

            IL.Push(new BinaryOperatorsClass());
            IL.Push(42);
            Call(MethodRef.Operator(typeof(BinaryOperatorsClass), BinaryOperator.Modulus, typeof(BinaryOperatorsClass), typeof(int)));
            IL.Pop(out result);
            results.Add(result);

            IL.Push(new BinaryOperatorsClass());
            IL.Push(42);
            Call(MethodRef.Operator(typeof(BinaryOperatorsClass), BinaryOperator.ExclusiveOr, typeof(BinaryOperatorsClass), typeof(int)));
            IL.Pop(out result);
            results.Add(result);

            IL.Push(new BinaryOperatorsClass());
            IL.Push(42);
            Call(MethodRef.Operator(typeof(BinaryOperatorsClass), BinaryOperator.BitwiseAnd, typeof(BinaryOperatorsClass), typeof(int)));
            IL.Pop(out result);
            results.Add(result);

            IL.Push(new BinaryOperatorsClass());
            IL.Push(42);
            Call(MethodRef.Operator(typeof(BinaryOperatorsClass), BinaryOperator.BitwiseOr, typeof(BinaryOperatorsClass), typeof(int)));
            IL.Pop(out result);
            results.Add(result);

            IL.Push(new BinaryOperatorsClass());
            IL.Push(42);
            Call(MethodRef.Operator(typeof(BinaryOperatorsClass), BinaryOperator.LeftShift, typeof(BinaryOperatorsClass), typeof(int)));
            IL.Pop(out result);
            results.Add(result);

            IL.Push(new BinaryOperatorsClass());
            IL.Push(42);
            Call(MethodRef.Operator(typeof(BinaryOperatorsClass), BinaryOperator.RightShift, typeof(BinaryOperatorsClass), typeof(int)));
            IL.Pop(out result);
            results.Add(result);

            IL.Push(new BinaryOperatorsClass());
            IL.Push(42);
            Call(MethodRef.Operator(typeof(BinaryOperatorsClass), BinaryOperator.Equality, typeof(BinaryOperatorsClass), typeof(int)));
            IL.Pop(out result);
            results.Add(result);

            IL.Push(new BinaryOperatorsClass());
            IL.Push(42);
            Call(MethodRef.Operator(typeof(BinaryOperatorsClass), BinaryOperator.GreaterThan, typeof(BinaryOperatorsClass), typeof(int)));
            IL.Pop(out result);
            results.Add(result);

            IL.Push(new BinaryOperatorsClass());
            IL.Push(42);
            Call(MethodRef.Operator(typeof(BinaryOperatorsClass), BinaryOperator.LessThan, typeof(BinaryOperatorsClass), typeof(int)));
            IL.Pop(out result);
            results.Add(result);

            IL.Push(new BinaryOperatorsClass());
            IL.Push(42);
            Call(MethodRef.Operator(typeof(BinaryOperatorsClass), BinaryOperator.Inequality, typeof(BinaryOperatorsClass), typeof(int)));
            IL.Pop(out result);
            results.Add(result);

            IL.Push(new BinaryOperatorsClass());
            IL.Push(42);
            Call(MethodRef.Operator(typeof(BinaryOperatorsClass), BinaryOperator.GreaterThanOrEqual, typeof(BinaryOperatorsClass), typeof(int)));
            IL.Pop(out result);
            results.Add(result);

            IL.Push(new BinaryOperatorsClass());
            IL.Push(42);
            Call(MethodRef.Operator(typeof(BinaryOperatorsClass), BinaryOperator.LessThanOrEqual, typeof(BinaryOperatorsClass), typeof(int)));
            IL.Pop(out result);
            results.Add(result);

            IL.Push("foo");
            IL.Push(new GenericOperatorsClass<string>(0));
            Call(MethodRef.Operator(typeof(GenericOperatorsClass<string>), BinaryOperator.Addition, TypeRef.TypeGenericParameters[0], new TypeRef(typeof(GenericOperatorsClass<>)).MakeGenericType(TypeRef.TypeGenericParameters[0])));
            IL.Pop(out result);
            results.Add(result);

            IL.Push(new GenericOperatorsClass<string>(0));
            IL.Push("foo");
            Call(MethodRef.Operator(typeof(GenericOperatorsClass<string>), BinaryOperator.Addition, new TypeRef(typeof(GenericOperatorsClass<>)).MakeGenericType(TypeRef.TypeGenericParameters[0]), TypeRef.TypeGenericParameters[0]));
            IL.Pop(out result);
            results.Add(result);

            return results.ToArray();
        }

        public int[] CallConversionOperators()
        {
            var results = new List<int>();

            IL.Push(new ConversionOperatorsClass(0));
            Call(MethodRef.Operator(typeof(ConversionOperatorsClass), ConversionOperator.Implicit, ConversionDirection.To, typeof(int)));
            IL.Pop(out int intResult);
            results.Add(intResult);

            IL.Push(0);
            Call(MethodRef.Operator(typeof(ConversionOperatorsClass), ConversionOperator.Implicit, ConversionDirection.From, typeof(int)));
            IL.Pop(out ConversionOperatorsClass objResult);
            results.Add(objResult.Value);

            IL.Push(new ConversionOperatorsClass(0));
            Call(MethodRef.Operator(typeof(ConversionOperatorsClass), ConversionOperator.Implicit, ConversionDirection.To, typeof(long)));
            IL.Pop(out long longResult);
            results.Add((int)longResult);

            IL.Push(0L);
            Call(MethodRef.Operator(typeof(ConversionOperatorsClass), ConversionOperator.Implicit, ConversionDirection.From, typeof(long)));
            IL.Pop(out objResult);
            results.Add(objResult.Value);

            IL.Push(new ConversionOperatorsClass(0));
            Call(MethodRef.Operator(typeof(ConversionOperatorsClass), ConversionOperator.Explicit, ConversionDirection.To, typeof(short)));
            IL.Pop(out short shortResult);
            results.Add(shortResult);

            IL.Push((short)0);
            Call(MethodRef.Operator(typeof(ConversionOperatorsClass), ConversionOperator.Explicit, ConversionDirection.From, typeof(short)));
            IL.Pop(out objResult);
            results.Add(objResult.Value);

            IL.Push("foo");
            Call(MethodRef.Operator(typeof(GenericOperatorsClass<string>), ConversionOperator.Implicit, ConversionDirection.From, TypeRef.TypeGenericParameters[0]));
            IL.Pop(out GenericOperatorsClass<string> genericStrResult);
            results.Add(genericStrResult.Value);

            genericStrResult = new GenericOperatorsClass<string>(0);
            IL.Push(genericStrResult);
            Call(MethodRef.Operator(typeof(GenericOperatorsClass<string>), ConversionOperator.Implicit, ConversionDirection.To, TypeRef.TypeGenericParameters[0]));
            Pop();
            results.Add(genericStrResult.Value);

            IL.Push(new ConversionOperatorsClass(0));
            Call(MethodRef.Method(typeof(ConversionOperatorsClass), "op_Implicit", typeof(int), 0, typeof(ConversionOperatorsClass)));
            IL.Pop(out intResult);
            results.Add(intResult);

            IL.Push(new ConversionOperatorsClass(0));
            Call(new MethodRef(typeof(ConversionOperatorsClass), "op_Implicit", typeof(long), 0, typeof(ConversionOperatorsClass)));
            IL.Pop(out longResult);
            results.Add((int)longResult);

            genericStrResult = new GenericOperatorsClass<string>(0);
            IL.Push(genericStrResult);
            Call(MethodRef.Method(typeof(GenericOperatorsClass<string>), "op_Implicit", TypeRef.TypeGenericParameters[0], 0, new TypeRef(typeof(GenericOperatorsClass<>)).MakeGenericType(TypeRef.TypeGenericParameters[0])));
            Pop();
            results.Add(genericStrResult.Value);

            return results.ToArray();
        }

        public void RaiseEvent()
            => Event?.Invoke();

#if NETFRAMEWORK
        public int[] CallVarArgMethod()
        {
            IL.Push(5);
            IL.Push(1);
            IL.Push(2);
            IL.Push(3);
            Call(new MethodRef(typeof(MethodRefTestCases), nameof(VarArgMethod)).WithOptionalParameters(typeof(int), typeof(int), typeof(int)));
            return IL.Return<int[]>();
        }
#endif

        private int InstanceMethod() => 10;
        private int InstanceMethod(int a) => 20;

        private static int OverloadedMethod() => 10;
        private static int OverloadedMethod(int a) => 20;
        private static int OverloadedMethod(ref int a) => 30;
        private static int OverloadedMethod(int[] a) => 40;
        private static int OverloadedMethod(double a, ref int b) => 50;
        private static int OverloadedMethod(double a, int b) => 60;

        private static T GenericMethod<T>(T value) => value;

        private static int GenericOverload<T>(T a, int b) => 1;
        private static int GenericOverload<T>(T a, uint b) => 2;
        private static int GenericOverload<T1, T2>(T1 a, uint b) => 3;
        private static int GenericOverload<T1, T2>(T1 a, T2 b) => 4;
        private static int GenericOverload<T1, T2>(T2 a, T1 b) => 5;
        private static int GenericOverload(int a, int b) => 6;
        private static int GenericOverload<T1, T2>(T1[] a, List<T2> b) => 7;

        private class GenericType<TClass>
        {
            public static string NormalMethod()
                => typeof(TClass).FullName ?? string.Empty;

            public static string GenericMethod<TMethod>()
                => $"{typeof(TClass).FullName} {typeof(TMethod).FullName}";

            public class NestedGenericType<TOther>
            {
                public static int GenericOverload<T>(TClass a, TOther b) => 1;
                public static int GenericOverload<T>(TOther a, TClass b) => 2;
                public static int GenericOverload<T>(TClass a, T b) => 3;
                public static int GenericOverload<T>(TClass[] a, List<TOther> b) => 4;
                public static int GenericOverload<T>(T[] a, List<T> b) => 5;
            }
        }

#if NETFRAMEWORK
        private static int[] VarArgMethod(int count, __arglist)
        {
            var it = new ArgIterator(__arglist);
            var result = new int[count];

            for (var i = 0; i < count; ++i)
                result[i] = it.GetRemainingCount() > 0 ? __refvalue(it.GetNextArg(), int) : 0;

            return result;
        }
#endif

        private class TypeWithInitializer
        {
            static TypeWithInitializer()
            {
                GC.KeepAlive(null);
            }
        }

        private class OtherClass
        {
            public int InstanceMethod() => 100;
            public int InstanceMethod(int a) => 200;

            public virtual int VirtualMethod() => 300;

            public static int StaticMethod() => 100;
            public static int StaticMethod(int a) => 200;

            public static void Create(out OtherClass result)
                => result = new OtherClass();
        }

        private class OtherClassDerived : OtherClass
        {
            public override int VirtualMethod() => 400;
        }

        private struct OtherStruct : ISomeInterface
        {
            public int InstanceMethod() => 1000;
            public int InstanceMethod(int a) => 2000;

            public int InterfaceMethod(int a) => 3000;

            public static int StaticMethod() => 1000;
            public static int StaticMethod(int a) => 2000;
        }

        private interface ISomeInterface
        {
            public int InterfaceMethod(int a);
        }

        private class UnaryOperatorsClass
        {
            public int Value { get; private set; }

            public static UnaryOperatorsClass operator --(UnaryOperatorsClass s)
            {
                s.Value = 1;
                return s;
            }

            public static UnaryOperatorsClass operator ++(UnaryOperatorsClass s)
            {
                s.Value = 2;
                return s;
            }

            public static UnaryOperatorsClass operator -(UnaryOperatorsClass s)
            {
                s.Value = 3;
                return s;
            }

            public static UnaryOperatorsClass operator +(UnaryOperatorsClass s)
            {
                s.Value = 4;
                return s;
            }

            public static UnaryOperatorsClass operator !(UnaryOperatorsClass s)
            {
                s.Value = 5;
                return s;
            }

            public static bool operator true(UnaryOperatorsClass s)
            {
                s.Value = 6;
                return true;
            }

            public static bool operator false(UnaryOperatorsClass s)
            {
                s.Value = 7;
                return true;
            }

            public static UnaryOperatorsClass operator ~(UnaryOperatorsClass s)
            {
                s.Value = 8;
                return s;
            }
        }

        private class BinaryOperatorsClass
        {
            public static int operator +(BinaryOperatorsClass a, int b) => 1;
            public static int operator +(int a, BinaryOperatorsClass b) => 2;
            public static int operator +(BinaryOperatorsClass a, BinaryOperatorsClass b) => 3;

            public static int operator -(BinaryOperatorsClass a, int b) => 4;
            public static int operator *(BinaryOperatorsClass a, int b) => 5;
            public static int operator /(BinaryOperatorsClass a, int b) => 6;
            public static int operator %(BinaryOperatorsClass a, int b) => 7;
            public static int operator ^(BinaryOperatorsClass a, int b) => 8;
            public static int operator &(BinaryOperatorsClass a, int b) => 9;
            public static int operator |(BinaryOperatorsClass a, int b) => 10;
            public static int operator <<(BinaryOperatorsClass a, int b) => 11;
            public static int operator >>(BinaryOperatorsClass a, int b) => 12;
            public static int operator ==(BinaryOperatorsClass a, int b) => 13;
            public static int operator >(BinaryOperatorsClass a, int b) => 14;
            public static int operator <(BinaryOperatorsClass a, int b) => 15;
            public static int operator !=(BinaryOperatorsClass a, int b) => 16;
            public static int operator >=(BinaryOperatorsClass a, int b) => 17;
            public static int operator <=(BinaryOperatorsClass a, int b) => 18;

            public override bool Equals(object? obj) => false;
            public override int GetHashCode() => 0;
        }

        private class ConversionOperatorsClass
        {
            public int Value { get; }

            public ConversionOperatorsClass(int value)
                => Value = value;

            public static implicit operator int(ConversionOperatorsClass obj) => 1;
            public static implicit operator ConversionOperatorsClass(int i) => new(2);

            public static implicit operator long(ConversionOperatorsClass obj) => 3;
            public static implicit operator ConversionOperatorsClass(long i) => new(4);

            public static explicit operator short(ConversionOperatorsClass obj) => 5;
            public static explicit operator ConversionOperatorsClass(short i) => new(6);
        }

        private class GenericOperatorsClass<T>
        {
            public int Value { get; private set; }

            public GenericOperatorsClass(int value)
                => Value = value;

            public static implicit operator GenericOperatorsClass<T>(T c)
                => new GenericOperatorsClass<T>(101);

            public static implicit operator T(GenericOperatorsClass<T> c)
            {
                c.Value = 102;
                return default!;
            }

            public static int operator +(T a, GenericOperatorsClass<T> b) => 103;
            public static int operator +(GenericOperatorsClass<T> a, T b) => 104;

            public static int operator +(GenericOperatorsClass<T> a) => 105;
        }
    }
}
