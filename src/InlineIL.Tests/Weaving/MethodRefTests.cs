using System;
using InlineIL.Tests.Support;
using Xunit;

namespace InlineIL.Tests.Weaving
{
    public class MethodRefTests
    {
        private static dynamic GetInstance()
            => AssemblyToProcessFixture.TestResult.GetInstance("MethodRefTestCases");

        private static dynamic GetUnverifiableInstance()
            => UnverifiableAssemblyToProcessFixture.TestResult.GetInstance("MethodRefTestCases");

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
    }
}
