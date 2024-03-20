using System.Diagnostics.CodeAnalysis;

namespace InlineIL.Tests.InjectedAssembly;

[SuppressMessage("ReSharper", "UnusedMember.Global")]
public class InjectedType
{
    public static int AddInt32(int a, int b)
        => a + b;
}
