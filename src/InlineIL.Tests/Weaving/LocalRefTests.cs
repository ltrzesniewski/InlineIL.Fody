using InlineIL.Tests.Support;
using Xunit;

namespace InlineIL.Tests.Weaving
{
    public class LocalRefTests
    {
        private static dynamic GetInstance()
            => AssemblyToProcessFixture.TestResult.GetInstance("LocalRefTestCases");

        private static dynamic GetUnverifiableInstance()
            => UnverifiableAssemblyToProcessFixture.TestResult.GetInstance("LocalRefTestCases");

        [Fact]
        public void should_handle_local_variables()
        {
            var instance = GetInstance();
            var result = (int)instance.UseLocalVariables(8);
            result.ShouldEqual(50);
        }

        [Fact]
        public void should_handle_pinned_local_variables()
        {
            var buf = new byte[] { 0, 0, 42, 0 };
            var instance = GetUnverifiableInstance();
            var result = (int)instance.UsePinnedLocalVariables(buf, 2);
            result.ShouldEqual(42);
        }
    }
}
