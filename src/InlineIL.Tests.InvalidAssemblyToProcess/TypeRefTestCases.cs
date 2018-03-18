using System.Diagnostics.CodeAnalysis;
using System.Reflection.Emit;
using InlineIL;

[SuppressMessage("ReSharper", "UnusedMember.Global")]
public class TypeRefTestCases
{
    public void LoadNullType()
    {
        IL.Emit(OpCodes.Ldtoken, new TypeRef(null));
        IL.Emit(OpCodes.Pop);
    }
}
