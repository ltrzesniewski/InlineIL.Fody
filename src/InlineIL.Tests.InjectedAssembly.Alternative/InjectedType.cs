using System.Diagnostics.CodeAnalysis;

namespace InlineIL.Tests.InjectedAssembly;

[SuppressMessage("ReSharper", "UnusedType.Global")]
[SuppressMessage("ReSharper", "UnusedMember.Global")]
public class InjectedType
{
    public static int MultiplyInt32(int a, int b)
        => a * b;
}
