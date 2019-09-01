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

        private MethodDefinition GetMethodDefinition(ModuleDefinition module, string ns, string methodName)
            => module.GetType($"{ns}.{ClassName}").Methods.Single(m => m.Name == methodName);
    }
}
