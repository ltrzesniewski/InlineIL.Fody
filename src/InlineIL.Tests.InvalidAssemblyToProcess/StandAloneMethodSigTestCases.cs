using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using System.Reflection.Emit;
using System.Runtime.InteropServices;
using InlineIL;

[SuppressMessage("ReSharper", "UnusedMember.Global")]
public class StandAloneMethodSigTestCases
{
    public void InvalidCallingConvention()
    {
        IL.Emit(OpCodes.Calli, new StandAloneMethodSig(CallingConventions.Standard, typeof(int), typeof(int)).WithOptionalParameters(typeof(int), typeof(int)));
    }

    public void EmptyVarArgParams()
    {
        IL.Emit(OpCodes.Calli, new StandAloneMethodSig(CallingConventions.VarArgs, typeof(int), typeof(int)).WithOptionalParameters());
    }

    public void VarArgParamsAlreadySupplied()
    {
        IL.Emit(OpCodes.Calli, new StandAloneMethodSig(CallingConventions.VarArgs, typeof(int), typeof(int)).WithOptionalParameters(typeof(int)).WithOptionalParameters(typeof(int)));
    }

    public void VarArgParamsWithNativeCall()
    {
        IL.Emit(OpCodes.Calli, new StandAloneMethodSig(CallingConvention.StdCall, typeof(int), typeof(int)).WithOptionalParameters(typeof(int)));
    }
}
