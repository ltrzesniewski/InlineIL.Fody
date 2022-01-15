using Mono.Cecil.Cil;

namespace InlineIL.Fody.Support;

internal class Logger : ILogger
{
    private readonly ModuleWeaver _moduleWeaver;

    public Logger(ModuleWeaver moduleWeaver)
        => _moduleWeaver = moduleWeaver;

    public void Debug(string message)
        => _moduleWeaver.WriteDebug(message);

    public void Info(string message)
        => _moduleWeaver.WriteInfo(message);

    public void Warning(string message, SequencePoint? sequencePoint)
        => _moduleWeaver.WriteWarning(message, sequencePoint);

    public void Error(string message, SequencePoint? sequencePoint)
        => _moduleWeaver.WriteError(message, sequencePoint);
}
