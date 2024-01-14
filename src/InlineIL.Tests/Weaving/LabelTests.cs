using InlineIL.Tests.Support;
using JetBrains.Annotations;
using Xunit;

namespace InlineIL.Tests.Weaving;

public class LabelTests() : ClassTestsBase("LabelTestCases")
{
    [Fact]
    public void should_handle_labels()
    {
        var result = (int)GetInstance().Branch(false);
        result.ShouldEqual(42);

        result = (int)GetInstance().Branch(true);
        result.ShouldEqual(1);

        ShouldNotHaveWarnings("Branch");
    }

    [Fact]
    public void should_handle_switch()
    {
        var result = (int)GetInstance().JumpTable(0);
        result.ShouldEqual(1);

        result = (int)GetInstance().JumpTable(1);
        result.ShouldEqual(2);

        result = (int)GetInstance().JumpTable(2);
        result.ShouldEqual(3);

        result = (int)GetInstance().JumpTable(3);
        result.ShouldEqual(42);

        ShouldNotHaveWarnings("JumpTable");
    }

#if CSHARP_12_OR_GREATER
    [Fact]
    public void should_handle_switch_with_collection_expression()
    {
        var result = (int)GetInstance().JumpTableCollectionExpression(0);
        result.ShouldEqual(1);

        result = (int)GetInstance().JumpTableCollectionExpression(1);
        result.ShouldEqual(2);

        result = (int)GetInstance().JumpTableCollectionExpression(2);
        result.ShouldEqual(3);

        result = (int)GetInstance().JumpTableCollectionExpression(3);
        result.ShouldEqual(42);

        ShouldNotHaveWarnings("JumpTableCollectionExpression");
    }
#endif

    [Fact]
    public void should_report_null_label_reference()
    {
        ShouldHaveError("NullLabelName").ShouldContain("ldnull");
    }

    [Fact]
    public void should_report_null_label_definition()
    {
        ShouldHaveError("NullLabel").ShouldContain("ldnull");
    }

    [Fact]
    public void should_report_undefined_label()
    {
        ShouldHaveError("UndefinedLabel").ShouldContain("Undefined label");
    }

    [Fact]
    public void should_report_redefined_label()
    {
        ShouldHaveError("RedefinedLabel").ShouldContain("already defined");
    }

    [Fact]
    public void should_report_unused_label()
    {
        ShouldHaveWarning("UnusedLabel", "Unused label: 'SomeLabel'");
    }
}

[UsedImplicitly]
public class LabelTestsStandard : LabelTests
{
    public LabelTestsStandard()
        => NetStandard = true;
}
