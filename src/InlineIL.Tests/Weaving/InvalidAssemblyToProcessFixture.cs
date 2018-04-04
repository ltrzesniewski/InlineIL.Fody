using System.Linq;
using Fody;
using InlineIL.Fody;
using InlineIL.Tests.Support;

#pragma warning disable 618

namespace InlineIL.Tests.Weaving
{
    public static class InvalidAssemblyToProcessFixture
    {
        public static TestResult TestResult { get; }

        static InvalidAssemblyToProcessFixture()
        {
            var weavingTask = new ModuleWeaver();
            TestResult = weavingTask.ExecuteTestRun("InlineIL.Tests.InvalidAssemblyToProcess.dll", false);
        }

        public static string ShouldHaveError(string className, string methodName)
        {
            var expectedMessagePart = $" {className}::{methodName}(";
            var errorMessage = TestResult.Errors.SingleOrDefault(err => err.Text.Contains(expectedMessagePart));
            errorMessage.ShouldNotBeNull();
            errorMessage.SequencePoint.ShouldNotBeNull();
            return errorMessage.Text;
        }
    }
}
