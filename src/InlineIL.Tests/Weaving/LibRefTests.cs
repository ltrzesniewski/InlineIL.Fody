using Xunit;

namespace InlineIL.Tests.Weaving
{
    public class LibRefTests : ClassTestsBase
    {
        public LibRefTests()
            : base("LibRefTestCases")
        {
        }

        [Fact]
        public void should_report_lib_ref_local()
        {
            ShouldHaveErrorNoSeqPoint("Local");
            ShouldHaveErrorNoSeqPoint("Local2");
        }

        [Fact]
        public void should_report_lib_ref_param()
        {
            ShouldHaveErrorNoSeqPoint("Param");
            ShouldHaveErrorNoSeqPoint("Param2");
        }

        [Fact]
        public void should_report_lib_ref_to_bad_field()
        {
            ShouldHaveError("BadFieldRef");
        }

        [Fact]
        public void should_report_lib_ref_to_bad_property()
        {
            ShouldHaveError("BadPropRef");
        }

        [Fact]
        public void should_report_lib_ref_to_bad_event()
        {
            ShouldHaveError("BadEventRef");
        }

        [Fact(Skip = "Compatibility fix for Fody v6")] // TODO Activate for Fody v6
        public void should_report_lib_ref_generic_constraint()
        {
            ShouldHaveErrorNoSeqPoint("GenericConstraint");
        }

        [Fact]
        public void should_report_lib_ref_generic_param_attributes()
        {
            ShouldHaveErrorNoSeqPoint("GenericParamAttribute");
        }

        [Fact]
        public void should_report_lib_ref_generic_call()
        {
            ShouldHaveErrorNoSeqPoint("GenericCall");
            ShouldHaveErrorNoSeqPoint("GenericCall2");
        }

        [Fact]
        public void should_report_lib_ref_in_attributes()
        {
            ShouldHaveErrorNoSeqPoint("AttributeCtor");
            ShouldHaveErrorNoSeqPoint("AttributeParam");
            ShouldHaveErrorNoSeqPoint("AttributeMethodParam");
        }

        [Fact]
        public void should_report_lib_ref_in_type_attributes()
        {
            ShouldHaveErrorInType("TypeAttr");
        }

        [Fact]
        public void should_report_lib_ref_in_field_attributes()
        {
            ShouldHaveErrorInType("FieldAttr");
        }

        [Fact]
        public void should_report_lib_ref_in_property_attributes()
        {
            ShouldHaveErrorInType("PropAttr");
        }

        [Fact]
        public void should_report_lib_ref_in_event_attributes()
        {
            ShouldHaveErrorInType("EventAttr");
        }

        [Fact]
        public void should_report_lib_ref_in_field_type()
        {
            ShouldHaveErrorInType("FieldType");
        }

        [Fact]
        public void should_report_lib_ref_in_property_type()
        {
            ShouldHaveErrorInType("PropType");
        }

        [Fact]
        public void should_report_lib_ref_in_event_type()
        {
            ShouldHaveErrorInType("EventType");
        }

        [Fact]
        public void should_report_lib_ref_in_base_type()
        {
            ShouldHaveErrorInType("BaseType");
        }

        [Fact]
        public void should_report_lib_ref_in_interface_type()
        {
            ShouldHaveErrorInType("InterfaceType");
        }
    }
}
