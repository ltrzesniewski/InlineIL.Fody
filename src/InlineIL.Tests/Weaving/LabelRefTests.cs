using InlineIL.Tests.Support;
using Xunit;

namespace InlineIL.Tests.Weaving
{
    public class LabelRefTests
    {
        private static dynamic GetInstance()
            => AssemblyToProcessFixture.TestResult.GetInstance("LabelRefTestCases");

        [Fact]
        public void should_handle_labels()
        {
            var result = (int)GetInstance().Branch(false);
            result.ShouldEqual(42);

            result = (int)GetInstance().Branch(true);
            result.ShouldEqual(1);
        }

        [Fact]
        public void should_handle_switch()
        {
            var result = (int)GetInstance().JumpTable(0);
            result.ShouldEqual(1);

            result = (int)GetInstance().JumpTable(1);
            result.ShouldEqual(2);

            result = (int)GetInstance().JumpTable(2);
            result.ShouldEqual(3);

            result = (int)GetInstance().JumpTable(3);
            result.ShouldEqual(42);
        }
    }
}
