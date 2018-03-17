using InlineIL.Tests.Support;
using Xunit;

namespace InlineIL.Tests.Weaving
{
    public class FieldRefTests
    {
        private static dynamic GetInstance()
            => AssemblyToProcessFixture.TestResult.GetInstance("FieldRefTestCases");

        [Fact]
        public void should_handle_field_references()
        {
            var instance = GetInstance();
            instance.IntField = 42;
            var result = (int)instance.ReturnIntField();
            result.ShouldEqual(42);
        }
    }
}
