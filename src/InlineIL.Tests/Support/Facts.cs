using System;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Xunit;

namespace InlineIL.Tests.Support;

public abstract class SkippableFactAttribute : FactAttribute
{
    protected SkippableFactAttribute(
        [CallerFilePath] string? sourceFilePath = null,
        [CallerLineNumber] int sourceLineNumber = -1)
        : base(sourceFilePath, sourceLineNumber)
    {
        // ReSharper disable once VirtualMemberCallInConstructor
        Skip = GetSkipMessage();
    }

    protected abstract string? GetSkipMessage();
}

public class DebugTestAttribute(
    [CallerFilePath] string? sourceFilePath = null,
    [CallerLineNumber] int sourceLineNumber = -1
) : SkippableFactAttribute(sourceFilePath, sourceLineNumber)
{
    protected override string? GetSkipMessage()
        => Debugger.IsAttached ? null : "Debug test";
}

public class ReleaseFactAttribute(
    Type typeFromAssembly,
    [CallerFilePath] string? sourceFilePath = null,
    [CallerLineNumber] int sourceLineNumber = -1
) : SkippableFactAttribute(sourceFilePath, sourceLineNumber)
{
    protected override string? GetSkipMessage()
    {
        if (((typeFromAssembly.Assembly.GetCustomAttribute<DebuggableAttribute>()?.DebuggingFlags ?? DebuggableAttribute.DebuggingModes.Default) & DebuggableAttribute.DebuggingModes.DisableOptimizations) != 0)
            return "Inconclusive in debug builds";

        return null;
    }
}

public class VarargFactAttribute(
    [CallerFilePath] string? sourceFilePath = null,
    [CallerLineNumber] int sourceLineNumber = -1
) : SkippableFactAttribute(sourceFilePath, sourceLineNumber)
{
    protected override string? GetSkipMessage()
    {
        if (!RuntimeInformation.IsOSPlatform(OSPlatform.Windows) || RuntimeInformation.ProcessArchitecture is not (Architecture.X86 or Architecture.X64))
            return "Varargs are not supported on this platform";

        return null;
    }
}
