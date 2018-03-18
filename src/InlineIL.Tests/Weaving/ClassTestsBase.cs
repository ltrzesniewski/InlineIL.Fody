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

        protected dynamic GetUnverifiableInstance()
            => UnverifiableAssemblyToProcessFixture.TestResult.GetInstance(ClassName);

        protected string ShouldHaveError(string methodName)
            => InvalidAssemblyToProcessFixture.ShouldHaveError(ClassName, methodName);
    }
}
