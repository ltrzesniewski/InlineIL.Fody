extern alias standard;
using Fody;
using Mono.Cecil;
using standard::InlineIL.Tests.StandardAssemblyToProcess;

namespace InlineIL.Tests.Weaving
{
    public static class StandardAssemblyToProcessFixture
    {
        public static TestResult TestResult { get; }

        public static ModuleDefinition OriginalModule { get; }
        public static ModuleDefinition ResultModule { get; }

        static StandardAssemblyToProcessFixture()
        {
            (TestResult, OriginalModule, ResultModule) = AssemblyToProcessFixture.Process<StandardAssemblyToProcessReference>();
        }
    }
}
