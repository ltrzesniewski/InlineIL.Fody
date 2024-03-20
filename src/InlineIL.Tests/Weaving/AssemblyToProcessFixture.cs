using System;
using System.Collections.Generic;
using System.Text;
using Fody;
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

    internal static TestRunResult Process<T>()
    {
        return WeaverRunner.ExecuteTestRun(
            typeof(T).Assembly,
            new GuardedWeaver(),
            ignoreCodes: new[]
            {
                "0x801312da" // VLDTR_E_MR_VARARGCALLINGCONV
            }
        );
    }

    private class GuardedWeaver : TestModuleWeaver
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
