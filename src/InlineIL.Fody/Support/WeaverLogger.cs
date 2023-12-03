using Mono.Cecil.Cil;

namespace InlineIL.Fody.Support;

internal class WeaverLogger(ILogger log, WeaverConfigOptions config) : ILogger
{
    public void Debug(string message)
        => log.Debug(message);

    public void Info(string message)
        => log.Info(message);

    public void Warning(string message, SequencePoint? sequencePoint)
    {
        switch (config.Warnings)
        {
            case WeaverConfigOptions.WarningsBehavior.Ignore:
                log.Debug($"Ignored warning: {message}");
                break;

            case WeaverConfigOptions.WarningsBehavior.Errors:
                log.Error($"Warning as error: {message}", sequencePoint);
                break;

            default:
                log.Warning(message, sequencePoint);
                break;
        }
    }

    public void Error(string message, SequencePoint? sequencePoint)
        => log.Error(message, sequencePoint);
}
