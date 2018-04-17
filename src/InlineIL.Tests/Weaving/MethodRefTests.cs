using System;
using System.Reflection;
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
        public void should_report_unknown_mehtod()
        {
            ShouldHaveError("UnknownMethodWithoutParams").ShouldContain("Method 'Nope' not found");
            ShouldHaveError("UnknownMethodWithParams").ShouldContain("Method Nope(System.Int32) not found");
        }

        [Fact]
        public void should_report_ambiguous_mehtod()
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

#if NETFWK
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
    }
}
