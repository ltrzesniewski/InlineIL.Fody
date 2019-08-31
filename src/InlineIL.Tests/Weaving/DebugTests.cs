using System.Linq;
using InlineIL.Fody.Processing;
using InlineIL.Fody.Support;
using InlineIL.Tests.AssemblyToProcess;
using InlineIL.Tests.Support;
using Mono.Cecil;

namespace InlineIL.Tests.Weaving
{
    public class DebugTests
    {
        [DebugTest]
        public void SingleMethod()
        {
            var assemblyPath = FixtureHelper.IsolateAssembly<AssemblyToProcessReference>();
            var type = typeof(MethodRefTestCases);
            var methodName = nameof(MethodRefTestCases.ReturnType);

            using (var module = ModuleDefinition.ReadModule(assemblyPath))
            {
                var weavingContext = new ModuleWeavingContext(module, new WeaverConfig(null, module));

                var typeDef = module.GetTypes().Single(i => i.FullName == type.FullName);
                var methodDef = typeDef.Methods.Single(i => i.Name == methodName);

                new MethodWeaver(weavingContext, methodDef).Process();
            }
        }
    }
}
