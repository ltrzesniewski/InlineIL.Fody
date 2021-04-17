using InlineIL.Fody.Support;
using Mono.Cecil.Cil;

namespace InlineIL.Tests.Support
{
    internal class NoOpLogger : ILogger
    {
        public static NoOpLogger Instance { get; } = new();

        private NoOpLogger()
        {
        }

        public void Debug(string message)
        {
        }

        public void Info(string message)
        {
        }

        public void Warning(string message, SequencePoint? sequencePoint)
        {
        }

        public void Error(string message, SequencePoint? sequencePoint)
        {
        }
    }
}
