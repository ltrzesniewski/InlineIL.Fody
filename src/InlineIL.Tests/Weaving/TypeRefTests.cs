using System;
using System.Collections.Generic;
using InlineIL.Tests.Support;
using Xunit;

#pragma warning disable 618

namespace InlineIL.Tests.Weaving
{
    public class TypeRefTests : ClassTestsBase
    {
        public TypeRefTests()
            : base("TypeRefTestCases")
        {
        }

        [Fact]
        public void should_handle_type_arg()
        {
            var result = (RuntimeTypeHandle)GetInstance().ReturnTypeHandle<Guid>();
            result.ShouldEqual(typeof(Guid).TypeHandle);
        }

        [Fact]
        public void should_handle_type_token_load()
        {
            var handle = (RuntimeTypeHandle)GetInstance().ReturnTypeHandle();
            Type.GetTypeFromHandle(handle).ShouldEqual(typeof(Guid));
        }

        [Fact]
        public void should_handle_type_arg_different_ways()
        {
            var result = (RuntimeTypeHandle[])GetInstance().LoadTypeDifferentWays();
            result.ShouldAll(i => Equals(i, result[0]));
        }

        [Fact]
        public void should_handle_pointer_types_using_TypeRef()
        {
            var result = (RuntimeTypeHandle)GetInstance().LoadPointerTypeUsingTypeRef();
            Type.GetTypeFromHandle(result).ShouldEqual(typeof(int**));
        }

        [Fact]
        public void should_handle_pointer_types_usingType()
        {
            var result = (RuntimeTypeHandle)GetInstance().LoadPointerTypeUsingType();
            Type.GetTypeFromHandle(result).ShouldEqual(typeof(int**));
        }

        [Fact]
        public void should_handle_reference_types_using_TypeRef()
        {
            var result = (RuntimeTypeHandle)GetInstance().LoadReferenceTypeUsingTypeRef();
            Type.GetTypeFromHandle(result).ShouldEqual(typeof(int).MakeByRefType());
        }

        [Fact]
        public void should_handle_reference_types_using_Type()
        {
            var result = (RuntimeTypeHandle)GetInstance().LoadReferenceTypeUsingType();
            Type.GetTypeFromHandle(result).ShouldEqual(typeof(int).MakeByRefType());
        }

        [Fact]
        public void should_handle_array_types_using_TypeRef()
        {
            var result = (RuntimeTypeHandle)GetInstance().LoadArrayTypeUsingTypeRef();
            Type.GetTypeFromHandle(result).ShouldEqual(typeof(int[][]));
        }

        [Fact]
        public void should_handle_array_types_using_Type()
        {
            var result = (RuntimeTypeHandle)GetInstance().LoadArrayTypeUsingType();
            Type.GetTypeFromHandle(result).ShouldEqual(typeof(int[][]));
        }

        [Fact]
        public void should_handle_array_types_with_rank_using_TypeRef()
        {
            var result = (RuntimeTypeHandle)GetInstance().LoadArrayTypeWithRankUsingTypeRef();
            Type.GetTypeFromHandle(result).ShouldEqual(typeof(int[][,,]));
        }

        [Fact]
        public void should_handle_array_types_with_rank_using_Type()
        {
            var result = (RuntimeTypeHandle)GetInstance().LoadArrayTypeWithRankUsingType();
            Type.GetTypeFromHandle(result).ShouldEqual(typeof(int[][,,]));
        }

        [Fact]
        public void should_handle_generic_types_using_TypeRef()
        {
            var result = (RuntimeTypeHandle)GetInstance().LoadGenericTypeUsingTypeRef();
            Type.GetTypeFromHandle(result).ShouldEqual(typeof(Dictionary<int, string>));
        }

        [Fact]
        public void should_handle_generic_types_using_Type()
        {
            var result = (RuntimeTypeHandle)GetInstance().LoadGenericTypeUsingType();
            Type.GetTypeFromHandle(result).ShouldEqual(typeof(Dictionary<int, string>));
        }

        [Fact]
        public void should_handle_nested_types_using_runtime_syntax()
        {
            var result = (Type)GetInstance().ReturnNestedTypeUsingRuntimeSyntax();
            result.FullName.ShouldEqual("TypeRefTestCases+NestedType");
        }

        [Fact]
        public void should_handle_nested_types_using_ecma_syntax()
        {
            var result = (Type)GetInstance().ReturnNestedTypeUsingEcmaSyntax();
            result.FullName.ShouldEqual("TypeRefTestCases+NestedType");
        }

        [Fact]
        public void should_report_null_type()
        {
            ShouldHaveError("LoadNullType").ShouldContain("ldnull");
            ShouldHaveError("LoadNullTypeRef").ShouldContain("ldnull");
        }

        [Fact]
        public void should_report_unresolvable_assembly()
        {
            ShouldHaveError("InvalidAssembly").ShouldContain("Could not resolve assembly");
        }

        [Fact]
        public void should_report_unknown_type()
        {
            ShouldHaveError("InvalidType").ShouldContain("Could not find type");
        }

        [Fact]
        public void should_report_invalid_array_rank()
        {
            ShouldHaveError("InvalidArrayRank").ShouldContain("Invalid array rank");
        }

        [Fact]
        public void should_report_unconsumed_reference()
        {
            ShouldHaveError("UnusedInstance");
        }

        [Fact]
        public void should_report_generic_args_on_normal_type()
        {
            ShouldHaveError("NotAGenericType").ShouldContain("Not a generic type");
        }

        [Fact]
        public void should_report_empty_generic_args()
        {
            ShouldHaveError("NoGenericTypeArgs").ShouldContain("No generic arguments supplied");
        }

        [Fact]
        public void should_report_invalid_generic_args_count()
        {
            ShouldHaveError("InvalidGenericArgsCount").ShouldContain("Incorrect number of generic arguments");
        }

        [Fact]
        public void should_report_generic_params_on_generic_instance()
        {
            ShouldHaveError("GenericParamsOnGenericInstance").ShouldContain("Type is already a generic instance");
        }

        [Fact]
        public void should_report_byref_of_byref()
        {
            ShouldHaveError("ByRefOfByRef").ShouldContain("Type is already a ByRef type");
        }

        [Fact]
        public void should_report_pointer_to_byref()
        {
            ShouldHaveError("PointerToByRef").ShouldContain("Cannot make a pointer to a ByRef type");
        }

        [Fact]
        public void should_report_array_of_byref()
        {
            ShouldHaveError("ArrayOfByRef").ShouldContain("Cannot make an array of a ByRef type");
        }

        [Fact]
        public void should_report_incorrect_use_of_generics_and_byref()
        {
            ShouldHaveError("GenericOfByRef").ShouldContain("Cannot make a generic instance");
        }
    }
}
