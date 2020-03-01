using Mono.Cecil.Cil;

namespace InlineIL.Fody.Support
{
    internal class WeaverLogger : ILogger
    {
        private readonly ILogger _log;
        private readonly WeaverConfigOptions _config;

        public WeaverLogger(ILogger log, WeaverConfigOptions config)
        {
            _log = log;
            _config = config;
        }

        public void Debug(string message)
            => _log.Debug(message);

        public void Info(string message)
            => _log.Info(message);

        public void Warning(string message, SequencePoint? sequencePoint)
        {
            switch (_config.Warnings)
            {
                case WeaverConfigOptions.WarningsBehavior.Ignore:
                    _log.Debug($"Ignored warning: {message}");
                    break;

                case WeaverConfigOptions.WarningsBehavior.Errors:
                    _log.Error($"Warning as error: {message}", sequencePoint);
                    break;

                default:
                    _log.Warning(message, sequencePoint);
                    break;
            }
        }

        public void Error(string message, SequencePoint? sequencePoint)
            => _log.Error(message, sequencePoint);
    }
}
