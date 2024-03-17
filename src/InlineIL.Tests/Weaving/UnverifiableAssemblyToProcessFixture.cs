using System.IO;
using Fody;
using InlineIL.Tests.InjectedAssembly;
using InlineIL.Tests.Support;
using InlineIL.Tests.UnverifiableAssemblyToProcess;
using Mono.Cecil;

#pragma warning disable 618

namespace InlineIL.Tests.Weaving;

public static class UnverifiableAssemblyToProcessFixture
{
    public static TestResult TestResult { get; }

    public static ModuleDefinition OriginalModule { get; }
    public static ModuleDefinition ResultModule { get; }

    static UnverifiableAssemblyToProcessFixture()
    {
        (TestResult, OriginalModule, ResultModule) = WeaverRunner.ExecuteTestRun(
            typeof(UnverifiableAssemblyToProcessReference).Assembly,
            new UnverifiableAssemblyWeaver(),
            false
        );
    }

    private class UnverifiableAssemblyWeaver : AssemblyToProcessFixture.GuardedWeaver
    {
        public override void Execute()
        {
            PrepareTypeRefFromDll();
            base.Execute();
        }

        private void PrepareTypeRefFromDll()
        {
            ProjectDirectoryPath.ShouldNotBeNull();

            var injectedDllDir = Path.Combine(ProjectDirectoryPath, "InjectedDllDir");
            Directory.CreateDirectory(injectedDllDir);

            File.Copy(
                typeof(InjectedType).Assembly.Location,
                Path.Combine(injectedDllDir, Path.GetFileName(typeof(InjectedType).Assembly.Location)),
                true
            );
        }
    }
}
