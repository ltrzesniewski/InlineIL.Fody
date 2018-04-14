using InlineIL.Tests.Support;
using Xunit;

namespace InlineIL.Tests.Weaving
{
    public class FieldRefTests : ClassTestsBase
    {
        public FieldRefTests()
            : base("FieldRefTestCases")
        {
        }

        [Fact]
        public void should_handle_field_references()
        {
            var instance = GetInstance();
            instance.IntField = 42;
            var result = (int)instance.ReturnIntField();
            result.ShouldEqual(42);
        }

        [Fact]
        public void should_handle_field_references_alt()
        {
            var instance = GetInstance();
            instance.IntField = 42;
            var result = (int)instance.ReturnIntFieldAlt();
            result.ShouldEqual(42);
        }

        [Fact]
        public void should_report_null_field()
        {
            ShouldHaveError("NullField").ShouldContain("ldnull");
            ShouldHaveError("NullFieldRef").ShouldContain("ldnull");
        }

        [Fact]
        public void should_report_unknown_field()
        {
            ShouldHaveError("UnknownField").ShouldContain("Field 'Nope' not found");
        }

        [Fact]
        public void should_report_unconsumed_reference()
        {
            ShouldHaveError("UnusedInstance");
        }
    }
}
