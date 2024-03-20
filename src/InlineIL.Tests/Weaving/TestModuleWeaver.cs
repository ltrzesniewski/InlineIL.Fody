using System.IO;
using InlineIL.Fody;
using InlineIL.Tests.Support;

namespace InlineIL.Tests.Weaving;

internal class TestModuleWeaver : ModuleWeaver
{
    public override void Execute()
    {
        CopyInjectedAssemblies();
        base.Execute();
    }

    private void CopyInjectedAssemblies()
    {
        ProjectDirectoryPath.ShouldNotBeNull();

        var sourceDir = Path.GetDirectoryName(typeof(AssemblyToProcessFixture).Assembly.Location)!;

        var targetDir = Path.Combine(ProjectDirectoryPath, "InjectedDllDir");
        Directory.CreateDirectory(targetDir);

        Copy("InlineIL.Tests.InjectedAssembly.dll");
        Copy("InlineIL.Tests.InjectedAssembly.Alternative.dll");

        void Copy(string fileName)
        {
            File.Copy(
                Path.Combine(sourceDir, fileName),
                Path.Combine(targetDir, fileName),
                true
            );
        }
    }
}
