using System;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using System.Text;
using InlineIL.Tests.Support;
using Xunit;

namespace InlineIL.Tests.Weaving
{
    public class MethodRefTests : ClassTestsBase
    {
        public MethodRefTests()
            : base("MethodRefTestCases")
        {
        }

        [Fact]
        public void should_handle_method_call()
        {
            var result = (Type)GetInstance().ReturnType<Guid>();
            result.ShouldEqual(typeof(Guid));
        }

        [Fact]
        public void should_resolve_overloads()
        {
            var result = (int[])GetInstance().ResolveOverloads();
            result.ShouldEqual(new[] { 10, 10, 20, 30, 40, 50, 60 });
        }

        [Fact]
        public void should_resolve_generic_overloads()
        {
            var result = (int[])GetInstance().ResolveGenericOverloads();
            result.ShouldEqual(new[] { 1, 2, 3, 4, 5, 6, 6, 7 });
        }

        [Fact]
        public void should_resolve_generic_overloads_in_nested_generic_types()
        {
            var result = (int[])GetInstance().ResolveGenericOverloadsInGenericType();
            result.ShouldEqual(new[] { 1, 2, 3, 4, 5 });
        }

        [Fact]
        public void should_resolve_overloads_unverifiable()
        {
            var result = (int[])GetUnverifiableInstance().ResolveOverloads();
            result.ShouldEqual(new[] { 10, 10, 20, 30, 40, 50, 60 });
        }

        [Fact]
        public void should_report_null_method()
        {
            ShouldHaveError("NullMethod").ShouldContain("ldnull");
            ShouldHaveError("NullMethodRef").ShouldContain("ldnull");
        }

        [Fact]
        public void should_report_unknown_method()
        {
            ShouldHaveError("UnknownMethodWithoutParams").ShouldContain("Method 'Nope' not found");
            ShouldHaveError("UnknownMethodWithParams").ShouldContain("Method Nope(System.Int32) not found");
        }

        [Fact]
        public void should_report_ambiguous_method()
        {
            ShouldHaveError("AmbiguousMethod").ShouldContain("Ambiguous method");
        }

        [Fact]
        public void should_report_unconsumed_reference()
        {
            ShouldHaveError("UnusedInstance");
        }

        [Fact]
        public void should_call_generic_method()
        {
            var result = (int)GetInstance().CallGenericMethod();
            result.ShouldEqual(42);
        }

        [Fact]
        public void should_call_method_in_generic_type()
        {
            var result = (string)GetInstance().CallMethodInGenericType();
            result.ShouldEqual(typeof(Guid).FullName);
        }

        [Fact]
        public void should_call_method_in_generic_type_array()
        {
            var result = (string)GetInstance().CallMethodInGenericTypeArray();
            result.ShouldEqual(typeof(Guid[]).FullName);
        }

        [Fact]
        public void should_call_method_in_generic_type_generic()
        {
            var result = (string)GetInstance().CallMethodInGenericTypeGeneric<DayOfWeek>();
            result.ShouldEqual(typeof(DayOfWeek).FullName);
        }

        [Fact]
        public void should_call_generic_method_in_generic_type()
        {
            var result = (string)GetInstance().CallGenericMethodInGenericType();
            result.ShouldEqual($"{typeof(Guid).FullName} {typeof(TimeSpan).FullName}");
        }

        [Fact]
        public void should_call_generic_method_in_generic_type_array()
        {
            var result = (string)GetInstance().CallGenericMethodInGenericTypeArray();
            result.ShouldEqual($"{typeof(Guid[]).FullName} {typeof(TimeSpan[]).FullName}");
        }

        [Fact]
        public void should_call_generic_method_in_generic_type_generic()
        {
            var result = (string)GetInstance().CallGenericMethodInGenericTypeGeneric<DayOfWeek, ConsoleColor>();
            result.ShouldEqual($"{typeof(DayOfWeek).FullName} {typeof(ConsoleColor).FullName}");
        }

        [Fact]
        public void should_call_corelib_method()
        {
            ((bool)GetInstance().CallCoreLibMethod<Guid>(null)).ShouldBeFalse();
            ((bool)GetInstance().CallCoreLibMethod<Guid>(Guid.Empty)).ShouldBeTrue();
        }

        [Fact]
        public void should_handle_method_token_load()
        {
            var handle = (RuntimeMethodHandle)GetInstance().ReturnMethodHandle();
            MethodBase.GetMethodFromHandle(handle).Name.ShouldEqual(nameof(Type.GetTypeFromHandle));
        }

        [Fact]
        public void should_call_property_getter()
        {
            var instance = GetInstance();
            instance.Value = 42;
            var result = (int)instance.GetValue();
            result.ShouldEqual(42);
        }

        [Fact]
        public void should_call_property_setter()
        {
            var instance = GetInstance();
            instance.SetValue(42);
            var result = (int)instance.Value;
            result.ShouldEqual(42);
        }

        [Fact]
        [SuppressMessage("ReSharper", "ConvertToLocalFunction")]
        public void should_subscribe_to_event()
        {
            var callCount = 0;
            Action callback = () => ++callCount;
            var instance = GetInstance();
            instance.AddEvent(callback);
            instance.RaiseEvent();
            callCount.ShouldEqual(1);
        }

        [Fact]
        [SuppressMessage("ReSharper", "ConvertToLocalFunction")]
        public void should_unsubscribe_from_event()
        {
            var callCount = 0;
            Action callback = () => ++callCount;
            var instance = GetInstance();
            instance.Event += callback;
            instance.RaiseEvent();
            instance.RemoveEvent(callback);
            instance.RaiseEvent();
            callCount.ShouldEqual(1);
        }

        [Fact]
        public void should_call_default_constructor()
        {
            var result = (StringBuilder)GetInstance().CallDefaultConstructor();
            result.ShouldNotBeNull();
            result.Capacity.ShouldEqual(new StringBuilder().Capacity);
        }

        [Fact]
        public void should_call_non_default_constructor()
        {
            var result = (StringBuilder)GetInstance().CallNonDefaultConstructor();
            result.ShouldNotBeNull();
            result.Capacity.ShouldEqual(42);
        }

        [Fact]
        public void should_access_type_initializer()
        {
            var result = (RuntimeMethodHandle)GetInstance().GetTypeInitializer();
            MethodBase.GetMethodFromHandle(result).Name.ShouldEqual(".cctor");
        }

        [Fact]
        public void should_report_generic_args_on_normal_method()
        {
            ShouldHaveError("NotAGenericMethod").ShouldContain("Not a generic method");
        }

        [Fact]
        public void should_report_empty_generic_args()
        {
            ShouldHaveError("NoGenericArgsProvided").ShouldContain("No generic arguments supplied");
        }

        [Fact]
        public void should_report_invalid_generic_args_count()
        {
            ShouldHaveError("TooManyGenericArgsProvided").ShouldContain("Incorrect number of generic arguments");
        }

        [Fact]
        public void should_report_no_matching_generic_overload()
        {
            ShouldHaveError("NoMatchingGenericOverload").ShouldContain("Method GenericMethod(System.String) not found");
        }

        [Fact]
        public void should_report_no_matching_generic_overload_with_type_generic_arg_outside_of_generic_type()
        {
            ShouldHaveError("TypeGenericArgOutsideOfGenericType").ShouldContain("Method GenericMethod(!42) not found");
        }

        [Fact]
        public void should_report_no_matching_generic_overload_with_type_generic_arg_out_of_bounds()
        {
            ShouldHaveError("TypeGenericArgOutOfBounds").ShouldContain("Method Method(!42) not found");
        }

        [Fact]
        public void should_report_no_matching_generic_overload_with_method_generic_arg_out_of_bounds()
        {
            ShouldHaveError("MethodGenericArgOutOfBounds").ShouldContain("Method GenericMethod(!!42) not found");
        }

        [Fact]
        public void should_report_no_matching_generic_overload_with_constructed_array_type()
        {
            ShouldHaveError("NoMatchingGenericOverloadArray").ShouldContain("Method GenericMethod(System.String[]) not found");
        }

        [Fact]
        public void should_report_no_matching_generic_overload_with_constructed_multi_dimensional_array_type()
        {
            ShouldHaveError("NoMatchingGenericOverloadArray2").ShouldContain("Method GenericMethod(System.String[,]) not found");
        }

        [Fact]
        public void should_report_no_matching_generic_overload_with_constructed_by_ref_type()
        {
            ShouldHaveError("NoMatchingGenericOverloadByRef").ShouldContain("Method GenericMethod(System.String&) not found");
        }

        [Fact]
        public void should_report_no_matching_generic_overload_with_constructed_pointer_type()
        {
            ShouldHaveError("NoMatchingGenericOverloadPointer").ShouldContain("Method GenericMethod(System.Int32*) not found");
        }

        [Fact]
        public void should_report_no_matching_generic_overload_with_constructed_generic_type()
        {
            ShouldHaveError("NoMatchingGenericOverloadGeneric").ShouldContain("Method GenericMethod(System.Collections.Generic.Dictionary`2<System.Int32,System.String>) not found");
        }

#if NETFRAMEWORK
        [Fact]
        public void should_call_vararg_method()
        {
            var result = (int[])GetInstance().CallVarArgMethod();
            result.ShouldEqual(new[] { 1, 2, 3, 0, 0 });
        }
#endif

        [Fact]
        public void should_report_vararg_usage_on_normal_method()
        {
            ShouldHaveError("NotAVarArgMethod").ShouldContain("Not a vararg method");
        }

        [Fact]
        public void should_report_vararg_params_supplied_multiple_times()
        {
            ShouldHaveError("VarArgParamsAlreadySupplied").ShouldContain("have already been supplied");
        }

        [Fact]
        public void should_report_empty_vararg_params()
        {
            ShouldHaveError("EmptyVarArgParams").ShouldContain("No optional parameter type supplied");
        }

        [Fact]
        public void should_report_unknown_property()
        {
            ShouldHaveError("UnknownProperty").ShouldContain("Property 'Nope' not found");
        }

        [Fact]
        public void should_report_property_without_getter()
        {
            ShouldHaveError("PropertyWithoutGetter").ShouldContain("has no getter");
        }

        [Fact]
        public void should_report_property_without_setter()
        {
            ShouldHaveError("PropertyWithoutSetter").ShouldContain("has no setter");
        }

        [Fact]
        public void should_report_event_without_invoker()
        {
            ShouldHaveError("EventWithoutInvoker").ShouldContain("has no raise method");
        }

        [Fact]
        public void should_report_unknown_constructor()
        {
            ShouldHaveError("UnknownConstructor").ShouldContain("has no constructor with signature");
        }

        [Fact]
        public void should_report_no_default_constructor()
        {
            ShouldHaveError("NoDefaultConstructor").ShouldContain("has no default constructor");
        }

        [Fact]
        public void should_report_no_type_initializer()
        {
            ShouldHaveError("NoTypeInitializer").ShouldContain("has no type initializer");
        }
    }
}
