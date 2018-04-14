using System;
using System.Diagnostics;
using System.Reflection;
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
        public void should_push_value_alt()
        {
            var result = (int)GetInstance().MultiplyBy3Alt(42);

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

        [Fact]
        public void should_report_invalid_push_usage()
        {
            if (((InvalidAssemblyToProcessFixture.TestResult.Assembly.GetCustomAttribute<DebuggableAttribute>()?.DebuggingFlags ?? DebuggableAttribute.DebuggingModes.Default) & DebuggableAttribute.DebuggingModes.DisableOptimizations) != 0)
                return; // Inconclusive in debug builds

            ShouldHaveError("InvalidPushUsage").ShouldContain("IL.Push cannot be used in this context");
        }

        [Fact]
        public void should_handle_exception_blocks()
        {
            var result = (int)GetInstance().HandleExceptionBlocks();
            result.ShouldEqual(19);
        }

        [Fact]
        public void should_handle_prefix_instructions_in_debug_mode()
        {
            var guid = Guid.NewGuid();
            GetUnverifiableInstance().HandlePrefixesInDebugMode(ref guid);
            guid.ShouldEqual(Guid.Empty);
        }

        [Fact]
        public void should_process_nested_classes()
        {
            var result = (int)GetInstance().NestedClass();
            result.ShouldEqual(3);
        }

        [Fact]
        public void should_handle_return_with_conversions()
        {
            ((float)GetInstance().ReturnWithConversion1()).ShouldEqual(42.0f);
            ((int?)GetInstance().ReturnWithConversion2()).ShouldEqual(42);
        }
    }
}
