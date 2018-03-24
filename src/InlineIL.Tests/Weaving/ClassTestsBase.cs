using System.Linq;
using Mono.Cecil;

namespace InlineIL.Tests.Weaving
{
    public abstract class ClassTestsBase
    {
        private string ClassName { get; }

        protected ClassTestsBase(string className)
        {
            ClassName = className;
        }

        protected dynamic GetInstance()
            => AssemblyToProcessFixture.TestResult.GetInstance(ClassName);

        protected MethodDefinition GetMethodDefinition(string methodName)
            => GetMethodDefinition(AssemblyToProcessFixture.ResultModule, methodName);

        protected dynamic GetUnverifiableInstance()
            => UnverifiableAssemblyToProcessFixture.TestResult.GetInstance(ClassName);

        protected MethodDefinition GetUnverifiableMethodDefinition(string methodName)
            => GetMethodDefinition(UnverifiableAssemblyToProcessFixture.ResultModule, methodName);

        protected string ShouldHaveError(string methodName)
            => InvalidAssemblyToProcessFixture.ShouldHaveError(ClassName, methodName);

        private MethodDefinition GetMethodDefinition(ModuleDefinition module, string methodName)
            => module.GetType(ClassName).Methods.Single(m => m.Name == methodName);
    }
}
