using Fody;
using InlineIL.Tests.UnverifiableAssemblyToProcess;
using Mono.Cecil;

#pragma warning disable 618

namespace InlineIL.Tests.Weaving;

public static class UnverifiableAssemblyToProcessFixture
{
    public static TestResult TestResult { get; }

    public static ModuleDefinition ResultModule { get; }

    static UnverifiableAssemblyToProcessFixture()
    {
        (TestResult, _, ResultModule) = WeaverRunner.ExecuteTestRun(
            typeof(UnverifiableAssemblyToProcessReference).Assembly,
            new AssemblyToProcessFixture.GuardedWeaver(),
            false
        );
    }
}
