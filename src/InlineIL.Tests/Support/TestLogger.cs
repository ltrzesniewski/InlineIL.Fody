using System.Collections.Generic;
using InlineIL.Fody.Support;
using Mono.Cecil.Cil;

namespace InlineIL.Tests.Support;

internal class TestLogger : ILogger
{
    public IList<string> LoggedDebug { get; } = new List<string>();
    public IList<string> LoggedInfos { get; } = new List<string>();
    public IList<(string message, SequencePoint? sequencePoint)> LoggedWarnings { get; } = new List<(string, SequencePoint?)>();
    public IList<(string message, SequencePoint? sequencePoint)> LoggedErrors { get; } = new List<(string, SequencePoint?)>();

    public void Debug(string message)
        => LoggedDebug.Add(message);

    public void Info(string message)
        => LoggedInfos.Add(message);

    public void Warning(string message, SequencePoint? sequencePoint)
        => LoggedWarnings.Add((message, sequencePoint));

    public void Error(string message, SequencePoint? sequencePoint)
        => LoggedErrors.Add((message, sequencePoint));
}
