using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using Xunit;

namespace InlineIL.Tests.Support
{
    internal static class AssertionExtensions
    {
        public static void ShouldEqual<T>(this T actual, T expected)
            => Assert.Equal(expected, actual);

        [ContractAnnotation("null => halt")]
        public static void ShouldNotBeNull(this object actual)
            => Assert.NotNull(actual);

        public static void ShouldAll<T>(this IEnumerable<T> items, Func<T, bool> test)
            => Assert.All(items, item => Assert.True(test(item)));

        public static void ShouldContain<T>(this IEnumerable<T> items, Func<T, bool> predicate)
            => Assert.Contains(items, item => predicate(item));

        public static void ShouldContain(this string str, string expectedSubstring)
            => Assert.Contains(expectedSubstring, str);
    }
}
