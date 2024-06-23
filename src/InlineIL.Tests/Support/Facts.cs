using System;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.InteropServices;
using Xunit;

namespace InlineIL.Tests.Support;

public abstract class SkippableFactAttribute : FactAttribute
{
    public sealed override string? Skip
    {
        get => base.Skip ?? GetSkipMessage();
        set => base.Skip = value;
    }

    protected abstract string? GetSkipMessage();
}

public class DebugTestAttribute : SkippableFactAttribute
{
    protected override string? GetSkipMessage()
        => Debugger.IsAttached ? null : "Debug test";
}

public class ReleaseFactAttribute : SkippableFactAttribute
{
    private readonly Type _typeFromAssembly;

    public ReleaseFactAttribute(Type typeFromAssembly)
        => _typeFromAssembly = typeFromAssembly;

    protected override string? GetSkipMessage()
    {
        if (((_typeFromAssembly.Assembly.GetCustomAttribute<DebuggableAttribute>()?.DebuggingFlags ?? DebuggableAttribute.DebuggingModes.Default) & DebuggableAttribute.DebuggingModes.DisableOptimizations) != 0)
            return "Inconclusive in debug builds";

        return null;
    }
}

public class VarargFactAttribute : SkippableFactAttribute
{
    protected override string? GetSkipMessage()
    {
        if (!RuntimeInformation.IsOSPlatform(OSPlatform.Windows) || RuntimeInformation.ProcessArchitecture is not (Architecture.X86 or Architecture.X64))
            return "Varargs are not supported on this platform";

        return null;
    }
}
