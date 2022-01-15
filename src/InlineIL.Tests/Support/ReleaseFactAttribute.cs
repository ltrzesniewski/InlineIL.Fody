using System;
using System.Diagnostics;
using System.Reflection;
using Xunit;

namespace InlineIL.Tests.Support;

public class ReleaseFactAttribute : FactAttribute
{
    private readonly Type _typeFromAssembly;

    public ReleaseFactAttribute(Type typeFromAssembly)
    {
        _typeFromAssembly = typeFromAssembly;
    }

    public override string? Skip
    {
        get
        {
            if (base.Skip != null)
                return base.Skip;

            if (((_typeFromAssembly.Assembly.GetCustomAttribute<DebuggableAttribute>()?.DebuggingFlags ?? DebuggableAttribute.DebuggingModes.Default) & DebuggableAttribute.DebuggingModes.DisableOptimizations) != 0)
                return "Inconclusive in debug builds";

            return null;
        }
        set => base.Skip = value;
    }
}
