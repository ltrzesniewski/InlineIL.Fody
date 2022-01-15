using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace InlineIL.Fody.Support;

internal class ReferenceEqualityComparer<T> : IEqualityComparer<T>
    where T : class
{
    public static ReferenceEqualityComparer<T> Instance { get; } = new();

    public bool Equals(T? x, T? y) => ReferenceEquals(x, y);
    public int GetHashCode([DisallowNull] T obj) => RuntimeHelpers.GetHashCode(obj);
}
