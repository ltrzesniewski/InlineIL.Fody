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
        public void should_handle_nested_types()
        {
            var result = (Type)GetInstance().ReturnNestedType();
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
    }
}
