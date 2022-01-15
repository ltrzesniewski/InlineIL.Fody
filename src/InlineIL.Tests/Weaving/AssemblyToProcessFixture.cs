using System;
using System.Collections.Generic;
using System.Text;
using Fody;
using InlineIL.Fody;
using InlineIL.Fody.Extensions;
using InlineIL.Tests.AssemblyToProcess;
using Mono.Cecil;
using Mono.Cecil.Cil;

#pragma warning disable 618

namespace InlineIL.Tests.Weaving;

public static class AssemblyToProcessFixture
{
    public static TestResult TestResult { get; }

    public static ModuleDefinition OriginalModule { get; }
    public static ModuleDefinition ResultModule { get; }

    static AssemblyToProcessFixture()
    {
        (TestResult, OriginalModule, ResultModule) = Process<AssemblyToProcessReference>();
    }

    internal static (TestResult testResult, ModuleDefinition originalModule, ModuleDefinition resultModule) Process<T>()
    {
        var assemblyPath = FixtureHelper.IsolateAssembly<T>();

        var weavingTask = new GuardedWeaver();

        var testResult = weavingTask.ExecuteTestRun(
            assemblyPath,
            ignoreCodes: new[]
            {
                "0x801312da" // VLDTR_E_MR_VARARGCALLINGCONV
            },
            writeSymbols: true,
            beforeExecuteCallback: BeforeExecuteCallback
        );

        using var assemblyResolver = new TestAssemblyResolver();

        var readerParams = new ReaderParameters(ReadingMode.Immediate)
        {
            ReadSymbols = true,
            AssemblyResolver = assemblyResolver
        };

        var originalModule = ModuleDefinition.ReadModule(assemblyPath, readerParams);
        var resultModule = ModuleDefinition.ReadModule(testResult.AssemblyPath, readerParams);

        return (testResult, originalModule, resultModule);
    }

    internal static void BeforeExecuteCallback(ModuleDefinition module)
    {
        // This reference is added by Fody, it's not supposed to be there
        module.AssemblyReferences.RemoveWhere(i => string.Equals(i.Name, "System.Private.CoreLib", StringComparison.OrdinalIgnoreCase));
    }

    internal class GuardedWeaver : ModuleWeaver
    {
        private readonly List<string> _errors = new();

        public override void Execute()
        {
            try
            {
                base.Execute();
            }
            catch (Exception ex)
            {
                var str = new StringBuilder();
                foreach (var error in _errors)
                    str.AppendLine(error);

                str.AppendLine(ex.Message);
                throw new InvalidOperationException(str.ToString());
            }
        }

        protected override void AddError(string message, SequencePoint? sequencePoint)
        {
            _errors.Add(message);
            base.AddError(message, sequencePoint);
        }
    }
}
