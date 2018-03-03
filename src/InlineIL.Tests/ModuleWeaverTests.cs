using Fody;
using InlineIL.Fody;
using Xunit;

#pragma warning disable 618

namespace InlineIL.Tests
{

    public class ModuleWeaverTests
    {
        private static readonly TestResult _testResult;

        static ModuleWeaverTests()
        {
            var weavingTask = new ModuleWeaver();
            _testResult = weavingTask.ExecuteTestRun("InlineIL.Tests.AssemblyToProcess.dll");
        }
        
        [Fact]
        public void should_process_assembly()
        {
            // Not a real test
            Assert.NotNull(_testResult.GetInstance("InlineIL.Tests.AssemblyToProcess.BasicClass"));
        }
    }
}
