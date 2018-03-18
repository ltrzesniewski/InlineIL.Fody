using InlineIL.Tests.Support;
using Xunit;

namespace InlineIL.Tests.Weaving
{
    public class LocalRefTests : ClassTestsBase
    {
        public LocalRefTests()
            : base("LocalRefTestCases")
        {
        }

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

        [Fact]
        public void should_report_undefined_local()
        {
            ShouldHaveError("UndefinedLocal").ShouldContain("is not defined");
        }

        [Fact]
        public void should_report_redefined_local()
        {
            ShouldHaveError("RedefinedLocal").ShouldContain("already defined");
        }

        [Fact]
        public void should_report_null_local_definition()
        {
            ShouldHaveError("NullLocal").ShouldContain("ldnull");
        }

        [Fact]
        public void should_report_null_local_reference()
        {
            ShouldHaveError("NullLocalRefName").ShouldContain("ldnull");
            ShouldHaveError("NullLocalRef").ShouldContain("ldnull");
        }
    }
}
