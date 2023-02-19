using System;

namespace InlineIL.Tests.Common;

[AttributeUsage(AttributeTargets.Class)]
public class TestCasesAttribute : Attribute
{
    public bool SnapshotTest { get; set; } = true;
}
