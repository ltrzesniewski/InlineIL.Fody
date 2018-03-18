using InlineIL.Tests.Support;
using Xunit;

#pragma warning disable 618

namespace InlineIL.Tests.Weaving
{
    public class BasicTests : ClassTestsBase
    {
        public BasicTests()
            : base("BasicTestCases")
        {
        }

        [Fact]
        public void should_push_value()
        {
            var result = (int)GetInstance().MultiplyBy3(42);

            result.ShouldEqual(42 * 3);
        }

        [Fact]
        public void should_push_value_by_ref()
        {
            var a = 42;
            GetInstance().AddAssign(ref a, 8);
            a.ShouldEqual(50);
        }

        [Fact]
        public void should_handle_const_operand_int()
        {
            var result = (int)GetInstance().TwoPlusTwo();
            result.ShouldEqual(4);
        }

        [Fact]
        public void should_handle_const_operand_float()
        {
            var result = (double)GetInstance().TwoPlusTwoFloat();
            result.ShouldEqual(4.0);
        }

        [Fact]
        public void should_handle_const_operand_byte()
        {
            var result = (int)GetInstance().TwoPlusTwoByte();
            result.ShouldEqual(4);
        }

        [Fact]
        public void should_handle_const_operand_string()
        {
            var result = (string)GetInstance().SayHi();
            result.ShouldEqual("Hello!");
        }

        [Fact]
        public void should_handle_const_operand_on_arg()
        {
            var result = (int)GetInstance().ReturnArg(42);
            result.ShouldEqual(42);
        }

        [Fact]
        public void should_report_unvalid_use_of_Unreachable()
        {
            ShouldHaveError("InvalidUnreachable");
        }

        [Fact]
        public void should_report_unvalid_use_of_Return()
        {
            ShouldHaveError("InvalidReturn");
        }

        [Fact]
        public void should_report_unconsumed_reference()
        {
            ShouldHaveError("UnusedInstance");
        }
    }
}
