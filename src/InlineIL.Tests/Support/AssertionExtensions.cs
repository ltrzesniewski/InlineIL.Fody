using System;
using System.Collections.Generic;
using Xunit;

namespace InlineIL.Tests.Support
{
    internal static class AssertionExtensions
    {
        public static void ShouldEqual<T>(this T actual, T expected)
            => Assert.Equal(expected, actual);

        public static void ShouldAll<T>(this IEnumerable<T> items, Func<T, bool> test)
            => Assert.All(items, item => Assert.True(test(item)));
    }
}
