using Mono.Cecil.Cil;

namespace InlineIL.Fody.Support
{
    internal class Logger
    {
        private readonly ModuleWeaver _moduleWeaver;

        public Logger(ModuleWeaver moduleWeaver)
        {
            _moduleWeaver = moduleWeaver;
        }

        public void Debug(string message)
            => _moduleWeaver.LogDebug?.Invoke(message);

        public void Info(string message)
            => _moduleWeaver.LogInfo?.Invoke(message);

        public void Warning(string message, SequencePoint? sequencePoint)
        {
            if (_moduleWeaver.LogWarningPoint != null)
            {
                _moduleWeaver.LogWarningPoint.Invoke(message, sequencePoint);
                return;
            }

            _moduleWeaver.LogWarning?.Invoke(message);
        }

        public void Error(string message, SequencePoint? sequencePoint)
        {
            if (_moduleWeaver.LogErrorPoint != null)
            {
                _moduleWeaver.LogErrorPoint.Invoke(message, sequencePoint);
                return;
            }

            _moduleWeaver.LogError?.Invoke(message);
        }
    }
}
