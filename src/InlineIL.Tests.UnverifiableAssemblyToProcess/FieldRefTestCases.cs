using Microsoft.Diagnostics.Tracing;
using static InlineIL.IL.Emit;

namespace InlineIL.Tests.UnverifiableAssemblyToProcess;

public class FieldRefTestCases
{
    public static nint ReadEventRecord(TraceEvent traceEvent)
    {
        // Repro of issue #33

        IL.Push(traceEvent);
        Ldfld(FieldRef.Field(typeof(TraceEvent), "eventRecord"));
        return IL.Return<nint>();
    }
}
