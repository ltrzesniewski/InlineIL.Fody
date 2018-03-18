using System.Diagnostics.CodeAnalysis;
using System.Reflection.Emit;
using InlineIL;

[SuppressMessage("ReSharper", "UnusedMember.Global")]
public class LocalRefTestCases
{
    public void UndefinedLocal()
    {
        IL.Emit(OpCodes.Ldloc, new LocalRef("foo"));
    }

    public void RedefinedLocal()
    {
        IL.DeclareLocal("foo", typeof(int));
        IL.DeclareLocal("foo", typeof(int));
    }

    public void NullLocal()
    {
        IL.DeclareLocal(null, typeof(int));
    }

    public void NullLocalRefName()
    {
        IL.Emit(OpCodes.Ldloc, new LocalRef(null));
    }

    public void NullLocalRef()
    {
        IL.Emit(OpCodes.Ldloc, (LocalRef)null);
    }
}
