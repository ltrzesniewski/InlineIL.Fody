using Fody;
using Mono.Cecil;

#pragma warning disable 618

namespace InlineIL.Tests.Weaving
{
    public static class UnverifiableAssemblyToProcessFixture
    {
        public static TestResult TestResult { get; }

        public static ModuleDefinition ResultModule { get; }

        static UnverifiableAssemblyToProcessFixture()
        {
            var weavingTask = new AssemblyToProcessFixture.GuardedWeaver();

            TestResult = weavingTask.ExecuteTestRun(FixtureHelper.IsolateAssembly("InlineIL.Tests.UnverifiableAssemblyToProcess.dll"), false);

            using (var assemblyResolver = new TestAssemblyResolver())
            {
                ResultModule = ModuleDefinition.ReadModule(TestResult.AssemblyPath, new ReaderParameters(ReadingMode.Immediate)
                {
                    AssemblyResolver = assemblyResolver
                });
            }
        }
    }
}
