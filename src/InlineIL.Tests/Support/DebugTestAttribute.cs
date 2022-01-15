using System.Diagnostics;
using Xunit;

namespace InlineIL.Tests.Support;

public class DebugTestAttribute : FactAttribute
{
    public override string? Skip
    {
        get
        {
            if (base.Skip != null)
                return base.Skip;

            if (!Debugger.IsAttached)
                return "Debug test";

            return null;
        }
        set => base.Skip = value;
    }
}
