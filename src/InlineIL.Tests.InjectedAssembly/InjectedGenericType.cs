using System.Diagnostics.CodeAnalysis;

namespace InlineIL.Tests.InjectedAssembly;

[SuppressMessage("ReSharper", "UnusedTypeParameter")]
public class InjectedGenericType<T>
{
}

[SuppressMessage("ReSharper", "UnusedTypeParameter")]
public class InjectedGenericType<T1, T2>
{
}
