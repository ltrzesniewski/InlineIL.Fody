using System;
using System.Collections.Generic;
using System.Text;
using Fody;
using InlineIL.Fody;
using Mono.Cecil;
using Mono.Cecil.Cil;

#pragma warning disable 618

namespace InlineIL.Tests.Weaving
{
    public static class AssemblyToProcessFixture
    {
        public static TestResult TestResult { get; }

        public static ModuleDefinition OriginalModule { get; }
        public static ModuleDefinition ResultModule { get; }

        static AssemblyToProcessFixture()
        {
            const string assemblyPath = "InlineIL.Tests.AssemblyToProcess.dll";

            var weavingTask = new GuardedWeaver();

            TestResult = weavingTask.ExecuteTestRun(
                assemblyPath,
                ignoreCodes: new[]
                {
                    "0x801312da" // VLDTR_E_MR_VARARGCALLINGCONV
                }
            );

            OriginalModule = ModuleDefinition.ReadModule(assemblyPath, new ReaderParameters(ReadingMode.Immediate));
            ResultModule = ModuleDefinition.ReadModule(TestResult.AssemblyPath, new ReaderParameters(ReadingMode.Immediate));
        }

        internal class GuardedWeaver : ModuleWeaver
        {
            private readonly List<string> _errors = new List<string>();

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

            protected override void AddError(string message, SequencePoint sequencePoint)
            {
                _errors.Add(message);
                base.AddError(message, sequencePoint);
            }
        }
    }
}
