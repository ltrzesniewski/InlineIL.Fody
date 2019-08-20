﻿using System;
using System.Collections.Generic;
using System.Text;
using Fody;
using InlineIL.Fody;
using InlineIL.Tests.AssemblyToProcess;
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
            var assemblyPath = FixtureHelper.IsolateAssembly<AssemblyToProcessReference>();

            var weavingTask = new GuardedWeaver();

            TestResult = weavingTask.ExecuteTestRun(
                assemblyPath,
                ignoreCodes: new[]
                {
                    "0x801312da" // VLDTR_E_MR_VARARGCALLINGCONV
                },
                writeSymbols: true
            );

            using (var assemblyResolver = new TestAssemblyResolver())
            {
                var readerParams = new ReaderParameters(ReadingMode.Immediate)
                {
                    ReadSymbols = true,
                    AssemblyResolver = assemblyResolver
                };

                OriginalModule = ModuleDefinition.ReadModule(assemblyPath, readerParams);
                ResultModule = ModuleDefinition.ReadModule(TestResult.AssemblyPath, readerParams);
            }
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
