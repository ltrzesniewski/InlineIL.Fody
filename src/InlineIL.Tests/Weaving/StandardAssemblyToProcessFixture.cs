extern alias standard;

using Fody;
using standard::InlineIL.Tests.StandardAssemblyToProcess;
using Mono.Cecil;

namespace InlineIL.Tests.Weaving
{
    public class StandardAssemblyToProcessFixture
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
