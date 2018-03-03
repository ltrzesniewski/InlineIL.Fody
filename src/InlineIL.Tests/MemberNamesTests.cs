using System.Linq;
using Mono.Cecil;
using Xunit;
using Xunit.Abstractions;

namespace InlineIL.Tests
{
    public class MemberNamesTests
    {
        private readonly ITestOutputHelper _output;

        public MemberNamesTests(ITestOutputHelper output)
        {
            _output = output;
        }

        [Fact(Skip = "Manual test")]
        public void PrintMemberNames()
        {
            using (var asm = AssemblyDefinition.ReadAssembly("InlineIL.dll"))
            {
                foreach (var method in asm.Modules.Single().Types.Single(t => t.Name == "IL").Methods)
                    _output.WriteLine(method.FullName);
            }
        }
    }
}
