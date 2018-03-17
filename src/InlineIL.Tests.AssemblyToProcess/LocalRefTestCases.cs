using System.Diagnostics.CodeAnalysis;
using System.Reflection.Emit;
using InlineIL;

[SuppressMessage("ReSharper", "UnusedMember.Global")]
[SuppressMessage("ReSharper", "UnusedParameter.Global")]
public class LocalRefTestCases
{
    public int UseLocalVariables(int value)
    {
        IL.DeclareLocal("foo", typeof(int));
        IL.DeclareLocal("bar", typeof(int));

        IL.Push(value);
        IL.Emit(OpCodes.Stloc, new LocalRef("foo"));

        IL.Emit(OpCodes.Ldc_I4, 42);
        IL.Emit(OpCodes.Stloc, new LocalRef("bar"));

        IL.Emit(OpCodes.Ldloc, new LocalRef("foo"));
        IL.Emit(OpCodes.Ldloc, new LocalRef("bar"));
        IL.Emit(OpCodes.Add);

        return IL.Return<int>();
    }
}
