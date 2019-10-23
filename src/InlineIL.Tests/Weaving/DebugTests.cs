extern alias standard;
using System;
using System.Linq;
using InlineIL.Fody.Processing;
using InlineIL.Fody.Support;
using InlineIL.Tests.AssemblyToProcess;
using InlineIL.Tests.Support;
using Mono.Cecil;
using standard::InlineIL.Tests.StandardAssemblyToProcess;
using Xunit;

namespace InlineIL.Tests.Weaving
{
    public class DebugTests
    {
        [DebugTest]
        public void SingleMethod()
        {
            var assemblyPath = FixtureHelper.IsolateAssembly<StandardAssemblyToProcessReference>();
            var type = typeof(MethodRefTestCases);
            var methodName = nameof(MethodRefTestCases.ReturnMethodHandle);

            using var module = ModuleDefinition.ReadModule(assemblyPath);
            var weavingContext = new ModuleWeavingContext(module, new WeaverConfig(null, module));

            var typeDef = module.GetTypes().Single(i => i.FullName == type.FullName);
            var methodDef = typeDef.Methods.Single(i => i.Name == methodName);

            new MethodWeaver(weavingContext, methodDef).Process();
        }

        [Fact]
        public void FindRootCause()
        {
            var assemblyPath = FixtureHelper.IsolateAssembly<StandardAssemblyToProcessReference>("DebugWeavingTest");
            using var module = ModuleDefinition.ReadModule(assemblyPath);

            const string coreLib = "System.Private.CoreLib";
            if (module.AssemblyReferences.Any(i => string.Equals(i.Name, coreLib, StringComparison.OrdinalIgnoreCase)))
                throw new InvalidOperationException($"Contains {coreLib} at start");

            var weavingContext = new ModuleWeavingContext(module, new WeaverConfig(null, module));

            foreach (var typeDefinition in module.GetTypes())
            {
                foreach (var methodDefinition in typeDefinition.Methods)
                {
                    new MethodWeaver(weavingContext, methodDefinition).Process();

                    if (module.AssemblyReferences.Any(i => string.Equals(i.Name, coreLib, StringComparison.OrdinalIgnoreCase)))
                        throw new InvalidOperationException($"Reference to {coreLib} added after weaving method {methodDefinition.FullName} in {typeDefinition.FullName}");
                }
            }
        }
    }
}
