using System;

namespace InlineIL.Tests.Common;

[AttributeUsage(AttributeTargets.Class)]
public class TestCasesAttribute : Attribute
{
}

[AttributeUsage(AttributeTargets.Method)]
public class SnapshotTest : Attribute
{
}
