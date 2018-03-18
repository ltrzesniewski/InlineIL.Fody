using System;
using System.Collections.Generic;
using System.Text;
using Fody;
using InlineIL.Fody;
using Mono.Cecil.Cil;

#pragma warning disable 618

namespace InlineIL.Tests.Weaving
{
    public static class AssemblyToProcessFixture
    {
        public static TestResult TestResult { get; }

        static AssemblyToProcessFixture()
        {
            var weavingTask = new GuardedWeaver();
            TestResult = weavingTask.ExecuteTestRun("InlineIL.Tests.AssemblyToProcess.dll");
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
