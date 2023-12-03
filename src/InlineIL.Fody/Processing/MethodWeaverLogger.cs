using InlineIL.Fody.Support;
using Mono.Cecil;
using Mono.Cecil.Cil;

namespace InlineIL.Fody.Processing;

internal class MethodWeaverLogger(ILogger log, MethodDefinition method) : ILogger
{
    public void Debug(string message)
        => log.Debug(QualifyMessage(message));

    public void Info(string message)
        => log.Info(QualifyMessage(message));

    public void Warning(string message, SequencePoint? sequencePoint)
        => log.Warning(QualifyMessage(message), sequencePoint);

    public void Error(string message, SequencePoint? sequencePoint)
        => log.Error(QualifyMessage(message), sequencePoint);

    public string QualifyMessage(string message, Instruction? instruction = null)
    {
        if (message.Contains(method.FullName))
            return message;

        return instruction != null
            ? $"{message} (in {method.FullName} at instruction {instruction})"
            : $"{message} (in {method.FullName})";
    }
}
