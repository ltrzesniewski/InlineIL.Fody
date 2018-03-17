using Fody;
using InlineIL.Fody;

#pragma warning disable 618

namespace InlineIL.Tests.Weaving
{
    public static class AssemblyToProcessFixture
    {
        public static TestResult TestResult { get; }

        static AssemblyToProcessFixture()
        {
            var weavingTask = new ModuleWeaver();
            TestResult = weavingTask.ExecuteTestRun("InlineIL.Tests.AssemblyToProcess.dll");
        }
    }
}
