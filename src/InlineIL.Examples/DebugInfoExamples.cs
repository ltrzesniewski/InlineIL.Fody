using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace InlineIL.Examples;

public static class DebugInfoExamples
{
    public static void LocalVariables()
    {
        var csharpVar = int.Parse("1");

        IL.DeclareLocals(
            typeof(int),
            new LocalVar("namedVar", typeof(int))
        );

        IL.Push(int.Parse("2"));
        IL.Emit.Stloc(0);

        IL.Push(int.Parse("3"));
        IL.Emit.Stloc("namedVar");

        DoNothing(csharpVar);

        IL.Emit.Ldloc(0);
        IL.Emit.Call(new MethodRef(typeof(DebugInfoExamples), nameof(DoNothing)));

        IL.Emit.Ldloc("namedVar");
        IL.Emit.Call(new MethodRef(typeof(DebugInfoExamples), nameof(DoNothing)));
    }

    [MethodImpl(MethodImplOptions.NoInlining)]
    [SuppressMessage("ReSharper", "UnusedParameter.Local")]
    private static void DoNothing(int _)
    {
    }
}
