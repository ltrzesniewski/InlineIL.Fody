.class public auto ansi beforefieldinit InlineIL.Tests.InvalidAssemblyToProcess.StandAloneMethodSigTestCases
	extends [mscorlib]System.Object
{
	.custom instance void [InlineIL.Tests.Common]InlineIL.Tests.Common.TestCasesAttribute::.ctor() = (
		01 00 00 00
	)
	// Methods
	.method public hidebysig specialname rtspecialname 
		instance void .ctor () cil managed 
	{
		// Header size: 1
		// Code size: 7 (0x7)
		.maxstack 8

		IL_0000: ldarg.0
		IL_0001: call instance void [mscorlib]System.Object::.ctor()
		IL_0006: ret
	} // end of method StandAloneMethodSigTestCases::.ctor

	.method public hidebysig 
		instance void EmptyVarArgParams () cil managed 
	{
		// Header size: 1
		// Code size: 11 (0xb)
		.maxstack 8

		IL_0000: ldstr "InlineIL processing failed: No optional parameter type supplied for vararg call site (in System.Void InlineIL.Tests.InvalidAssemblyToProcess.StandAloneMethodSigTestCases::EmptyVarArgParams() at instruction IL_0037: call System.Void InlineIL.IL/Emit::Calli(InlineIL.StandAloneMethodSig))"
		IL_0005: newobj instance void [mscorlib]System.InvalidProgramException::.ctor(string)
		IL_000a: throw
	} // end of method StandAloneMethodSigTestCases::EmptyVarArgParams

	.method public hidebysig 
		instance void InvalidCallingConvention () cil managed 
	{
		// Header size: 12
		// Code size: 11 (0xb)
		.maxstack 1

		IL_0000: ldstr "InlineIL processing failed: Not a vararg calling convention (in System.Void InlineIL.Tests.InvalidAssemblyToProcess.StandAloneMethodSigTestCases::InvalidCallingConvention() at instruction IL_005c: call System.Void InlineIL.IL/Emit::Calli(InlineIL.StandAloneMethodSig))"
		IL_0005: newobj instance void [mscorlib]System.InvalidProgramException::.ctor(string)
		IL_000a: throw
	} // end of method StandAloneMethodSigTestCases::InvalidCallingConvention

	.method public hidebysig 
		instance void InvalidTailCallInstruction () cil managed 
	{
		// Header size: 12
		// Code size: 11 (0xb)
		.maxstack 1

		IL_0000: ldstr "InlineIL processing failed: tail. must be followed by call or calli or callvirt (in System.Void InlineIL.Tests.InvalidAssemblyToProcess.StandAloneMethodSigTestCases::InvalidTailCallInstruction() at instruction IL_0000: tail.)"
		IL_0005: newobj instance void [mscorlib]System.InvalidProgramException::.ctor(string)
		IL_000a: throw
	} // end of method StandAloneMethodSigTestCases::InvalidTailCallInstruction

	.method public hidebysig 
		instance void InvalidTailCallRet () cil managed 
	{
		// Header size: 12
		// Code size: 11 (0xb)
		.maxstack 1

		IL_0000: ldstr "InlineIL processing failed: A tail call must be immediately followed by ret (in System.Void InlineIL.Tests.InvalidAssemblyToProcess.StandAloneMethodSigTestCases::InvalidTailCallRet() at instruction IL_0000: calli System.Void())"
		IL_0005: newobj instance void [mscorlib]System.InvalidProgramException::.ctor(string)
		IL_000a: throw
	} // end of method StandAloneMethodSigTestCases::InvalidTailCallRet

	.method public hidebysig 
		instance void VarArgParamsAlreadySupplied () cil managed 
	{
		// Header size: 12
		// Code size: 11 (0xb)
		.maxstack 1

		IL_0000: ldstr "InlineIL processing failed: Optional parameters for vararg call site have already been supplied (in System.Void InlineIL.Tests.InvalidAssemblyToProcess.StandAloneMethodSigTestCases::VarArgParamsAlreadySupplied() at instruction IL_0067: call System.Void InlineIL.IL/Emit::Calli(InlineIL.StandAloneMethodSig))"
		IL_0005: newobj instance void [mscorlib]System.InvalidProgramException::.ctor(string)
		IL_000a: throw
	} // end of method StandAloneMethodSigTestCases::VarArgParamsAlreadySupplied

	.method public hidebysig 
		instance void VarArgParamsWithNativeCall () cil managed 
	{
		// Header size: 12
		// Code size: 11 (0xb)
		.maxstack 1

		IL_0000: ldstr "InlineIL processing failed: Not a vararg calling convention (in System.Void InlineIL.Tests.InvalidAssemblyToProcess.StandAloneMethodSigTestCases::VarArgParamsWithNativeCall() at instruction IL_004a: call System.Void InlineIL.IL/Emit::Calli(InlineIL.StandAloneMethodSig))"
		IL_0005: newobj instance void [mscorlib]System.InvalidProgramException::.ctor(string)
		IL_000a: throw
	} // end of method StandAloneMethodSigTestCases::VarArgParamsWithNativeCall

	.method private hidebysig static 
		void VoidTargetMethod () cil managed 
	{
		// Header size: 1
		// Code size: 1 (0x1)
		.maxstack 8

		IL_0000: ret
	} // end of method StandAloneMethodSigTestCases::VoidTargetMethod

} // end of class InlineIL.Tests.InvalidAssemblyToProcess.StandAloneMethodSigTestCases
