using System;
using System.Collections.Generic;

namespace InlineIL.Fody.Extensions;

internal static class FrameworkExtensions
{
    public static void Deconstruct<TKey, TValue>(this KeyValuePair<TKey, TValue> pair, out TKey key, out TValue value)
        where TKey : notnull
    {
        key = pair.Key;
        value = pair.Value;
    }

    public static TValue? GetValueOrDefault<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key)
        where TKey : notnull
        => dictionary.TryGetValue(key, out var value) ? value : default;

    public static TValue GetOrAddNew<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key)
        where TKey : notnull
        where TValue : new()
    {
        if (!dictionary.TryGetValue(key, out var value))
        {
            value = new TValue();
            dictionary.Add(key, value);
        }

        return value;
    }

    public static void AddRange<T>(this ICollection<T> collection, params IEnumerable<T> items)
    {
        foreach (var item in items)
            collection.Add(item);
    }

    public static int IndexOfFirst<T>(this IEnumerable<T> items, Func<T, bool> predicate)
    {
        var index = 0;

        foreach (var item in items)
        {
            if (predicate(item))
                return index;

            ++index;
        }

        return -1;
    }

    public static void RemoveWhere<T>(this IList<T> list, Func<T, bool> predicate)
    {
        for (var i = list.Count - 1; i >= 0; --i)
        {
            if (predicate(list[i]))
                list.RemoveAt(i);
        }
    }

    public static HashSet<T> ToHashSet<T>(this IEnumerable<T> items)
        => new(items);
}
