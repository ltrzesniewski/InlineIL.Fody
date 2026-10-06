using JetBrains.Annotations;
using Shouldly;
using Xunit;

namespace InlineIL.Tests.Weaving;

public abstract class StandAloneMethodSigTestsBase() : ClassTestsBase("StandAloneMethodSigTestCases");

public class StandAloneMethodSigTests : StandAloneMethodSigTestsBase
{
    [Fact]
    public void should_call_indirect_static()
    {
        var result = (int)GetUnverifiableInstance().CallIndirectStatic();
        result.ShouldBe(42);
    }

    [Fact]
    public void should_call_indirect_static_alt()
    {
        var result = (int)GetUnverifiableInstance().CallIndirectStaticAlt();
        result.ShouldBe(42);
    }

    [Fact]
    public void should_call_indirect_instance()
    {
        var result = (int)GetUnverifiableInstance().CallIndirectInstance();
        result.ShouldBe(42);
    }

    [Fact]
    public void should_report_mismatched_calling_convention()
    {
        ShouldHaveError("InvalidCallingConvention").ShouldContain("Not a vararg calling convention");
    }

    [Fact]
    public void should_report_empty_vararg_params()
    {
        ShouldHaveError("EmptyVarArgParams").ShouldContain("No optional parameter type supplied");
    }

    [Fact]
    public void should_report_vararg_params_supplied_multiple_times()
    {
        ShouldHaveError("VarArgParamsAlreadySupplied").ShouldContain("have already been supplied");
    }

    [Fact]
    public void should_call_indirect_native_stdcall()
    {
        var result = (int)GetUnverifiableInstance().CallIndirectNativeStdcall();
        result.ShouldBe(42);
    }

    [Fact]
    public void should_call_indirect_native_stdcall_alt()
    {
        var result = (int)GetUnverifiableInstance().CallIndirectNativeStdcallAlt();
        result.ShouldBe(42);
    }

    [Fact]
    public void should_call_indirect_native_cdecl()
    {
        var result = (int)GetUnverifiableInstance().CallIndirectNativeCdecl();
        result.ShouldBe(42);
    }

    [Fact]
    public void should_report_vararg_params_supplied_for_native_call()
    {
        ShouldHaveError("VarArgParamsWithNativeCall").ShouldContain("Not a vararg calling convention");
    }

    [Fact]
    public void should_tail_call_indirect_static()
    {
        var result = (int)GetUnverifiableInstance().TailCallIndirectStatic();
        result.ShouldBe(42);
    }

    [Fact]
    public void should_tail_call_indirect_static_void()
    {
        GetUnverifiableInstance().TailCallIndirectStaticVoid();
    }

    [Fact]
    public void should_branch_over_tail_call()
    {
        var result = (int)GetUnverifiableInstance().BranchOverTailCall(true);
        result.ShouldBe(42);

        result = (int)GetUnverifiableInstance().BranchOverTailCall(false);
        result.ShouldBe(84);
    }

    [Fact]
    public void should_handle_multiple_tail_calls()
    {
        var result = (int)GetUnverifiableInstance().MultipleTailCalls(true);
        result.ShouldBe(1);

        result = (int)GetUnverifiableInstance().MultipleTailCalls(false);
        result.ShouldBe(2);
    }

    [Fact]
    public void should_handle_mixed_non_tail_and_tail_calls()
    {
        var result = (int)GetUnverifiableInstance().MixedNonTailAndTailCall(true);
        result.ShouldBe(1);

        result = (int)GetUnverifiableInstance().MixedNonTailAndTailCall(false);
        result.ShouldBe(2);

        result = (int)GetUnverifiableInstance().MixedNonTailAndTailCall2(true);
        result.ShouldBe(1);

        result = (int)GetUnverifiableInstance().MixedNonTailAndTailCall2(false);
        result.ShouldBe(2);
    }

    [Fact]
    public void should_report_invalid_tail_call_method()
    {
        ShouldHaveError("InvalidTailCallInstruction").ShouldContain("tail. must be followed by call or calli or callvirt");
    }

    [Fact]
    public void should_report_invalid_tail_call_ret()
    {
        ShouldHaveError("InvalidTailCallRet").ShouldContain("A tail call must be immediately followed by ret");
    }
}

#if NETFRAMEWORK
public class StandAloneMethodSigTestsFramework : StandAloneMethodSigTestsBase
{
    [Fact]
    public void should_call_indirect_vararg()
    {
        var result = (int)GetUnverifiableInstance().CallIndirectVarArg();
        result.ShouldBe(42);
    }
}
#endif

[UsedImplicitly]
public class StandAloneMethodSigTestsStandard : StandAloneMethodSigTests
{
    public StandAloneMethodSigTestsStandard()
        => NetStandard = true;
}
