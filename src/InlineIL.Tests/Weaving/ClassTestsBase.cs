using System.Linq;
using InlineIL.Tests.Support;
using Mono.Cecil;

namespace InlineIL.Tests.Weaving
{
    public abstract class ClassTestsBase
    {
        protected const string VerifiableAssembly = "InlineIL.Tests.AssemblyToProcess";
        protected const string UnverifiableAssembly = "InlineIL.Tests.UnverifiableAssemblyToProcess";
        protected const string InvalidAssembly = "InlineIL.Tests.InvalidAssemblyToProcess";

        private string ClassName { get; }
        protected bool NetStandard { get; set; }

        protected ClassTestsBase(string className)
        {
            ClassName = className;
        }

        protected dynamic GetInstance()
        {
            return NetStandard
                ? StandardAssemblyToProcessFixture.TestResult.GetInstance($"{VerifiableAssembly}.{ClassName}")
                : AssemblyToProcessFixture.TestResult.GetInstance($"{VerifiableAssembly}.{ClassName}");
        }

        protected MethodDefinition GetMethodDefinition(string methodName)
        {
            return NetStandard
                ? GetMethodDefinition(StandardAssemblyToProcessFixture.ResultModule, VerifiableAssembly, methodName)
                : GetMethodDefinition(AssemblyToProcessFixture.ResultModule, VerifiableAssembly, methodName);
        }

        protected MethodDefinition GetOriginalMethodDefinition(string methodName)
        {
            return NetStandard
                ? GetMethodDefinition(StandardAssemblyToProcessFixture.OriginalModule, VerifiableAssembly, methodName)
                : GetMethodDefinition(AssemblyToProcessFixture.OriginalModule, VerifiableAssembly, methodName);
        }

        protected dynamic GetUnverifiableInstance()
            => UnverifiableAssemblyToProcessFixture.TestResult.GetInstance($"{UnverifiableAssembly}.{ClassName}");

        protected MethodDefinition GetUnverifiableMethodDefinition(string methodName)
            => GetMethodDefinition(UnverifiableAssemblyToProcessFixture.ResultModule, UnverifiableAssembly, methodName);

        protected string ShouldHaveError(string methodName)
            => InvalidAssemblyToProcessFixture.ShouldHaveError($"{InvalidAssembly}.{ClassName}", methodName, true);

        protected string ShouldHaveErrorNoSeqPoint(string methodName)
            => InvalidAssemblyToProcessFixture.ShouldHaveError($"{InvalidAssembly}.{ClassName}", methodName, false);

        protected void ShouldHaveErrorInType(string nestedTypeName)
            => InvalidAssemblyToProcessFixture.ShouldHaveErrorInType($"{InvalidAssembly}.{ClassName}", nestedTypeName);

        protected string ShouldHaveWarning(string methodName, string expectedText)
        {
            var testResult = NetStandard
                ? StandardAssemblyToProcessFixture.TestResult
                : AssemblyToProcessFixture.TestResult;

            var expectedMessagePart = $" {VerifiableAssembly}.{ClassName}::{methodName}(";
            var warning = testResult.Warnings.FirstOrDefault(warn => warn.Text.Contains(expectedMessagePart) && warn.Text.Contains(expectedText));
            warning.ShouldNotBeNull();
            warning.SequencePoint.ShouldNotBeNull();

            return warning.Text;
        }

        protected void ShouldNotHaveWarnings(string methodName)
        {
            var testResult = NetStandard
                ? StandardAssemblyToProcessFixture.TestResult
                : AssemblyToProcessFixture.TestResult;

            var expectedMessagePart = $" {VerifiableAssembly}.{ClassName}::{methodName}(";
            testResult.Warnings.ShouldNotContain(warn => warn.Text.Contains(expectedMessagePart));
        }

        private MethodDefinition GetMethodDefinition(ModuleDefinition module, string ns, string methodName)
            => module.GetType($"{ns}.{ClassName}").Methods.Single(m => m.Name == methodName);
    }
}
