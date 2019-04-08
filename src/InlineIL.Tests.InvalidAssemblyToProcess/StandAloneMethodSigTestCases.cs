using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using System.Runtime.InteropServices;
using static InlineIL.IL.Emit;

namespace InlineIL.Tests.InvalidAssemblyToProcess
{
    [SuppressMessage("ReSharper", "UnusedMember.Global")]
    public class StandAloneMethodSigTestCases
    {
        public void InvalidCallingConvention()
        {
            Calli(new StandAloneMethodSig(CallingConventions.Standard, typeof(int), typeof(int)).WithOptionalParameters(typeof(int), typeof(int)));
        }

        public void EmptyVarArgParams()
        {
            Calli(new StandAloneMethodSig(CallingConventions.VarArgs, typeof(int), typeof(int)).WithOptionalParameters());
        }

        public void VarArgParamsAlreadySupplied()
        {
            Calli(new StandAloneMethodSig(CallingConventions.VarArgs, typeof(int), typeof(int)).WithOptionalParameters(typeof(int)).WithOptionalParameters(typeof(int)));
        }

        public void VarArgParamsWithNativeCall()
        {
            Calli(new StandAloneMethodSig(CallingConvention.StdCall, typeof(int), typeof(int)).WithOptionalParameters(typeof(int)));
        }

        public void InvalidTailCallInstruction()
        {
            Tail();
            Ldftn(new MethodRef(typeof(StandAloneMethodSigTestCases), nameof(VoidTargetMethod)));
            Calli(new StandAloneMethodSig(CallingConventions.Standard, typeof(void)));
        }

        public void InvalidTailCallRet()
        {
            Ldftn(new MethodRef(typeof(StandAloneMethodSigTestCases), nameof(VoidTargetMethod)));
            Tail();
            Calli(new StandAloneMethodSig(CallingConventions.Standard, typeof(void)));

            Ldc_I4(42);
            Pop();
        }

        private static void VoidTargetMethod()
        {
        }
    }
}
