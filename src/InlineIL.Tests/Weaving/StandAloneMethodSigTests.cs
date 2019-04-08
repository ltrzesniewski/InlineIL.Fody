using InlineIL.Tests.Support;
using Xunit;

namespace InlineIL.Tests.Weaving
{
    public class StandAloneMethodSigTests : ClassTestsBase
    {
        public StandAloneMethodSigTests()
            : base("StandAloneMethodSigTestCases")
        {
        }

        [Fact]
        public void should_call_indirect_static()
        {
            var result = (int)GetUnverifiableInstance().CallIndirectStatic();
            result.ShouldEqual(42);
        }

        [Fact]
        public void should_call_indirect_instance()
        {
            var result = (int)GetUnverifiableInstance().CallIndirectInstance();
            result.ShouldEqual(42);
        }

#if NETFRAMEWORK
        [Fact]
        public void should_call_indirect_vararg()
        {
            var result = (int)GetUnverifiableInstance().CallIndirectVarArg();
            result.ShouldEqual(42);
        }
#endif

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
            result.ShouldEqual(42);
        }

        [Fact]
        public void should_call_indirect_native_cdecl()
        {
            var result = (int)GetUnverifiableInstance().CallIndirectNativeCdecl();
            result.ShouldEqual(42);
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
            result.ShouldEqual(42);
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
            result.ShouldEqual(42);

            result = (int)GetUnverifiableInstance().BranchOverTailCall(false);
            result.ShouldEqual(84);
        }

        [Fact]
        public void should_handle_multiple_tail_calls()
        {
            var result = (int)GetUnverifiableInstance().MultipleTailCalls(true);
            result.ShouldEqual(1);

            result = (int)GetUnverifiableInstance().MultipleTailCalls(false);
            result.ShouldEqual(2);
        }

        [Fact]
        public void should_handle_mixed_non_tail_and_tail_calls()
        {
            var result = (int)GetUnverifiableInstance().MixedNonTailAndTailCall(true);
            result.ShouldEqual(1);

            result = (int)GetUnverifiableInstance().MixedNonTailAndTailCall(false);
            result.ShouldEqual(2);

            result = (int)GetUnverifiableInstance().MixedNonTailAndTailCall2(true);
            result.ShouldEqual(1);

            result = (int)GetUnverifiableInstance().MixedNonTailAndTailCall2(false);
            result.ShouldEqual(2);
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
}
