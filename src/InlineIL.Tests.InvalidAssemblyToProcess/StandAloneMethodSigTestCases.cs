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
    }
}
