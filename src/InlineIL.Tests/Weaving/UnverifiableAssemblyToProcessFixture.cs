using Fody;
using InlineIL.Fody;

#pragma warning disable 618

namespace InlineIL.Tests.Weaving
{
    public static class UnverifiableAssemblyToProcessFixture
    {
        public static TestResult TestResult { get; }

        static UnverifiableAssemblyToProcessFixture()
        {
            var weavingTask = new ModuleWeaver();
            TestResult = weavingTask.ExecuteTestRun("InlineIL.Tests.UnverifiableAssemblyToProcess.dll", false);
        }
    }
}
