using Mono.Cecil.Cil;

namespace InlineIL.Fody.Support;

internal interface ILogger
{
    void Debug(string message);
    void Info(string message);
    void Warning(string message, SequencePoint? sequencePoint);
    void Error(string message, SequencePoint? sequencePoint);
}

internal class Logger(ModuleWeaver moduleWeaver) : ILogger
{
    public void Debug(string message)
        => moduleWeaver.WriteDebug(message);

    public void Info(string message)
        => moduleWeaver.WriteInfo(message);

    public void Warning(string message, SequencePoint? sequencePoint)
        => moduleWeaver.WriteWarning(message, sequencePoint);

    public void Error(string message, SequencePoint? sequencePoint)
        => moduleWeaver.WriteError(message, sequencePoint);
}
