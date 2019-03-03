using System.Linq;
using Mono.Cecil;

namespace InlineIL.Tests.Weaving
{
    public abstract class ClassTestsBase
    {
        protected const string VerifiableAssembly = "InlineIL.Tests.AssemblyToProcess";
        protected const string UnverifiableAssembly = "InlineIL.Tests.UnverifiableAssemblyToProcess";
        protected const string InvalidAssembly = "InlineIL.Tests.InvalidAssemblyToProcess";

        private string ClassName { get; }

        protected ClassTestsBase(string className)
        {
            ClassName = className;
        }

        protected dynamic GetInstance()
            => AssemblyToProcessFixture.TestResult.GetInstance($"{VerifiableAssembly}.{ClassName}");

        protected MethodDefinition GetMethodDefinition(string methodName)
            => GetMethodDefinition(AssemblyToProcessFixture.ResultModule, VerifiableAssembly, methodName);

        protected MethodDefinition GetOriginalMethodDefinition(string methodName)
            => GetMethodDefinition(AssemblyToProcessFixture.OriginalModule, VerifiableAssembly, methodName);

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

        private MethodDefinition GetMethodDefinition(ModuleDefinition module, string ns, string methodName)
            => module.GetType($"{ns}.{ClassName}").Methods.Single(m => m.Name == methodName);
    }
}
