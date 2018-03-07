using Fody;
using InlineIL.Fody;
using Xunit;

#pragma warning disable 618

namespace InlineIL.Tests
{
    public class UnverifiableModuleWeaverTests
    {
        private static readonly TestResult _testResult;

        static UnverifiableModuleWeaverTests()
        {
            var weavingTask = new ModuleWeaver();
            _testResult = weavingTask.ExecuteTestRun("InlineIL.Tests.UnverifiableAssemblyToProcess.dll", false);
        }

        [Fact]
        public void should_resolve_overloads()
        {
            var result = (int[])_testResult.GetInstance("BasicClass").ResolveOverloads();
            Assert.Equal(new[] { 10, 10, 20, 30, 40, 50, 60 }, result);
        }
    }
}
