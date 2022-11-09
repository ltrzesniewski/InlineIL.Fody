using System.Linq;
using InlineIL.Tests.Support;
using Mono.Cecil;

namespace InlineIL.Tests.Weaving;

public abstract class ClassTestsBase
{
    protected const string _verifiableAssembly = "InlineIL.Tests.AssemblyToProcess";
    protected const string _unverifiableAssembly = "InlineIL.Tests.UnverifiableAssemblyToProcess";
    protected const string _invalidAssembly = "InlineIL.Tests.InvalidAssemblyToProcess";

    private string ClassName { get; }
    protected bool NetStandard { get; set; }

    protected ClassTestsBase(string className)
    {
        ClassName = className;
    }

    protected dynamic GetInstance()
    {
        return NetStandard
            ? StandardAssemblyToProcessFixture.TestResult.GetInstance($"{_verifiableAssembly}.{ClassName}")
            : AssemblyToProcessFixture.TestResult.GetInstance($"{_verifiableAssembly}.{ClassName}");
    }

    protected MethodDefinition GetMethodDefinition(string methodName)
    {
        return NetStandard
            ? GetMethodDefinition(StandardAssemblyToProcessFixture.ResultModule, _verifiableAssembly, methodName)
            : GetMethodDefinition(AssemblyToProcessFixture.ResultModule, _verifiableAssembly, methodName);
    }

    protected MethodDefinition GetOriginalMethodDefinition(string methodName)
    {
        return NetStandard
            ? GetMethodDefinition(StandardAssemblyToProcessFixture.OriginalModule, _verifiableAssembly, methodName)
            : GetMethodDefinition(AssemblyToProcessFixture.OriginalModule, _verifiableAssembly, methodName);
    }

    protected dynamic GetUnverifiableInstance()
        => UnverifiableAssemblyToProcessFixture.TestResult.GetInstance($"{_unverifiableAssembly}.{ClassName}");

    protected MethodDefinition GetUnverifiableMethodDefinition(string methodName)
        => GetMethodDefinition(UnverifiableAssemblyToProcessFixture.ResultModule, _unverifiableAssembly, methodName);

    protected string ShouldHaveError(string methodName)
        => InvalidAssemblyToProcessFixture.ShouldHaveError($"{_invalidAssembly}.{ClassName}", methodName, true);

    protected string ShouldHaveErrorNoSeqPoint(string methodName)
        => InvalidAssemblyToProcessFixture.ShouldHaveError($"{_invalidAssembly}.{ClassName}", methodName, false);

    protected void ShouldHaveErrorInType(string nestedTypeName)
        => InvalidAssemblyToProcessFixture.ShouldHaveErrorInType($"{_invalidAssembly}.{ClassName}", nestedTypeName);

    protected string ShouldHaveWarning(string methodName, string expectedText)
    {
        var testResult = NetStandard
            ? StandardAssemblyToProcessFixture.TestResult
            : AssemblyToProcessFixture.TestResult;

        var expectedMessagePart = $" {_verifiableAssembly}.{ClassName}::{methodName}(";
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

        var expectedMessagePart = $" {_verifiableAssembly}.{ClassName}::{methodName}(";
        testResult.Warnings.ShouldNotContain(warn => warn.Text.Contains(expectedMessagePart));
    }

    private MethodDefinition GetMethodDefinition(ModuleDefinition module, string ns, string methodName)
        => module.GetType($"{ns}.{ClassName}").Methods.Single(m => m.Name == methodName);
}
