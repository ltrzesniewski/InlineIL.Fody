using System.Linq;
using Fody;
using InlineIL.Fody;
using InlineIL.Tests.InvalidAssemblyToProcess;
using InlineIL.Tests.Support;
using Mono.Cecil;

#pragma warning disable 618

namespace InlineIL.Tests.Weaving
{
    public static class InvalidAssemblyToProcessFixture
    {
        public static TestResult TestResult { get; }

        public static ModuleDefinition ResultModule { get; }

        static InvalidAssemblyToProcessFixture()
        {
            var weavingTask = new ModuleWeaver();
            TestResult = weavingTask.ExecuteTestRun(FixtureHelper.IsolateAssembly<InvalidAssemblyToProcessReference>(), false);

            using var assemblyResolver = new TestAssemblyResolver();

            ResultModule = ModuleDefinition.ReadModule(TestResult.AssemblyPath, new ReaderParameters(ReadingMode.Immediate)
            {
                AssemblyResolver = assemblyResolver
            });
        }

        public static string ShouldHaveError(string className, string methodName, bool sequencePointRequired)
        {
            var expectedMessagePart = $" {className}::{methodName}(";
            var errorMessage = TestResult.Errors.SingleOrDefault(err => err.Text.Contains(expectedMessagePart));
            errorMessage.ShouldNotBeNull();

            if (sequencePointRequired)
                errorMessage.SequencePoint.ShouldNotBeNull();

            return errorMessage.Text;
        }

        public static void ShouldHaveErrorInType(string className, string nestedTypeName)
        {
            var expectedMessagePart = $" {className}/{nestedTypeName}";
            TestResult.Errors.ShouldAny(err => err.Text.Contains(expectedMessagePart));
        }
    }
}
