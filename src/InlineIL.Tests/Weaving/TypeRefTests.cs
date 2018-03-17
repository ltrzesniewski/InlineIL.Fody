using System;
using System.Collections.Generic;
using InlineIL.Tests.Support;
using Xunit;

#pragma warning disable 618

namespace InlineIL.Tests.Weaving
{
    public class TypeRefTests
    {
        private static dynamic GetInstance()
            => AssemblyToProcessFixture.TestResult.GetInstance("TypeRefTestCases");

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
        public void should_handle_pointer_types()
        {
            var result = (RuntimeTypeHandle)GetInstance().LoadPointerType();
            Type.GetTypeFromHandle(result).ShouldEqual(typeof(int**));
        }

        [Fact]
        public void should_handle_reference_types()
        {
            var result = (RuntimeTypeHandle)GetInstance().LoadReferenceType();
            Type.GetTypeFromHandle(result).ShouldEqual(typeof(int).MakeByRefType());
        }

        [Fact]
        public void should_handle_array_types()
        {
            var result = (RuntimeTypeHandle)GetInstance().LoadArrayType();
            Type.GetTypeFromHandle(result).ShouldEqual(typeof(int[][]));
        }

        [Fact]
        public void should_handle_array_types_with_rank()
        {
            var result = (RuntimeTypeHandle)GetInstance().LoadArrayTypeWithRank();
            Type.GetTypeFromHandle(result).ShouldEqual(typeof(int[][,,]));
        }

        [Fact]
        public void should_handle_generic_types()
        {
            var result = (RuntimeTypeHandle)GetInstance().LoadGenericType();
            Type.GetTypeFromHandle(result).ShouldEqual(typeof(Dictionary<int, string>));
        }

        [Fact]
        public void should_handle_nested_types()
        {
            var result = (Type)GetInstance().ReturnNestedType();
            result.FullName.ShouldEqual("TypeRefTestCases+NestedType");
        }
    }
}
