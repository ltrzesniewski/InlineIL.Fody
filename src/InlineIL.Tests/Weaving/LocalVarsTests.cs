using System.Linq;
using InlineIL.Tests.Support;
using Xunit;

namespace InlineIL.Tests.Weaving
{
    public class LocalVarsTests : ClassTestsBase
    {
        public LocalVarsTests()
            : base("LocalVarsTestCases")
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
        public void should_handle_local_variables_with_explicit_init()
        {
            var instance = GetInstance();
            var result = (int)instance.UseLocalVariablesExplicitInit(8);
            result.ShouldEqual(50);

            GetMethodDefinition("UseLocalVariablesExplicitInit").Body.InitLocals.ShouldBeTrue();
        }

        [Fact]
        public void should_handle_local_variables_with_no_init()
        {
            var instance = GetUnverifiableInstance();
            var result = (int)instance.UseLocalVariablesNoInit(8);
            result.ShouldEqual(50);

            GetUnverifiableMethodDefinition("UseLocalVariablesNoInit").Body.InitLocals.ShouldBeFalse();
        }

        [Fact]
        public void should_handle_pinned_local_variables()
        {
            var buf = new byte[] { 0, 0, 42, 0 };
            var instance = GetUnverifiableInstance();
            var result = (int)instance.UsePinnedLocalVariables(buf, 2);
            result.ShouldEqual(42);

            GetUnverifiableMethodDefinition("UsePinnedLocalVariables").Body.Variables.ShouldContain(v => v.IsPinned);
        }

        [Fact]
        public void should_map_local_indexes()
        {
            var instance = GetInstance();
            var result = (int)instance.MapLocalIndexes(3, 12, 54, 9);
            result.ShouldEqual(42);
        }

        [Fact]
        public void should_map_local_indexes_long_form()
        {
            var instance = GetInstance();
            var result = (int)instance.MapLocalIndexesLong(38, 4);
            result.ShouldEqual(42);
        }

        [Fact]
        public void should_handle_optional_modifiers()
        {
            var localType = GetMethodDefinition("WithOptionalModifier").Body.Variables.Single().VariableType;
            localType.FullName.ShouldEqual("System.Int32 modopt(System.Runtime.CompilerServices.IsConst)");
        }

        [Fact]
        public void should_handle_required_modifiers()
        {
            var localType = GetMethodDefinition("WithRequiredModifier").Body.Variables.Single().VariableType;
            localType.FullName.ShouldEqual("System.Int32 modreq(System.Runtime.CompilerServices.IsConst)");
        }

        [Fact]
        public void should_handle_typedbyref()
        {
            var localType = GetMethodDefinition("TypedReference").Body.Variables.Single().VariableType;
            localType.FullName.ShouldEqual("System.TypedReference");
        }

        [Fact]
        public void should_report_undefined_local()
        {
            ShouldHaveError("UndefinedLocal").ShouldContain("is not defined");
        }

        [Fact]
        public void should_report_undefined_local_2()
        {
            ShouldHaveError("UndefinedLocal2").ShouldContain("is not defined");
        }

        [Fact]
        public void should_report_redefined_local()
        {
            ShouldHaveError("RedefinedLocal").ShouldContain("already defined");
        }

        [Fact]
        public void should_report_multiple_declarations()
        {
            ShouldHaveError("MultipleDeclarations").ShouldContain("Local variables have already been declared");
        }

        [Fact]
        public void should_report_null_local_definition()
        {
            ShouldHaveError("NullLocal").ShouldContain("ldnull");
        }

        [Fact]
        public void should_report_null_local_name()
        {
            ShouldHaveError("NullLocalName").ShouldContain("ldnull");
        }

        [Fact]
        public void should_report_null_local_reference()
        {
            ShouldHaveError("NullLocalRefName").ShouldContain("ldnull");
        }

        [Fact]
        public void should_report_undefined_local_usage_by_index_macro_when_no_locals_are_defined()
        {
            ShouldHaveError("UndefinedLocalByIndexMacro").ShouldContain("No locals are defined");
        }

        [Fact]
        public void should_report_undefined_local_usage_by_index_when_no_locals_are_defined()
        {
            ShouldHaveError("UndefinedLocalByIndex").ShouldContain("No locals are defined");
        }

        [Fact]
        public void should_report_local_out_of_range_macro()
        {
            ShouldHaveError("LocalOutOfRangeMacro").ShouldContain("out of range");
        }

        [Fact]
        public void should_report_local_out_of_range()
        {
            ShouldHaveError("LocalOutOfRange").ShouldContain("out of range");
        }
    }
}
