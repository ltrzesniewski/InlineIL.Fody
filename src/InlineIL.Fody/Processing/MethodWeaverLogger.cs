using InlineIL.Fody.Support;
using Mono.Cecil;
using Mono.Cecil.Cil;

namespace InlineIL.Fody.Processing
{
    internal class MethodWeaverLogger : ILogger
    {
        private readonly ILogger _log;
        private readonly MethodDefinition _method;
        private readonly SequencePointMapper _sequencePoints;

        public MethodWeaverLogger(ILogger log, MethodDefinition method, SequencePointMapper sequencePoints)
        {
            _log = log;
            _method = method;
            _sequencePoints = sequencePoints;
        }

        public void Debug(string message)
            => _log.Debug(ProcessMessage(message));

        public void Info(string message)
            => _log.Info(ProcessMessage(message));

        public void Warning(string message, SequencePoint? sequencePoint)
            => _log.Warning(ProcessMessage(message), sequencePoint);

        public void Error(string message, SequencePoint? sequencePoint)
            => _log.Error(ProcessMessage(message), sequencePoint);

        private string ProcessMessage(string message)
        {
            if (message.Contains(_method.FullName))
                return message;

            return $"{message} (in {_method.FullName})";
        }
    }
}
