.class public auto ansi beforefieldinit InlineIL.Tests.InvalidAssemblyToProcess.LocalVarsTestCases
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
		// Code size: 8 (0x8)
		.maxstack 8

		IL_0000: ldarg.0
		IL_0001: call instance void [mscorlib]System.Object::.ctor()
		IL_0006: nop
		IL_0007: ret
	} // end of method LocalVarsTestCases::.ctor

	.method public hidebysig 
		instance void LocalOutOfRange () cil managed 
	{
		// Header size: 12
		// Code size: 11 (0xb)
		.maxstack 1

		IL_0000: ldstr "InlineIL processing failed: Local index 1 is out of range (in System.Void InlineIL.Tests.InvalidAssemblyToProcess.LocalVarsTestCases::LocalOutOfRange() at instruction IL_002a: call System.Void InlineIL.IL/Emit::Ldloc(System.UInt16))"
		IL_0005: newobj instance void [mscorlib]System.InvalidProgramException::.ctor(string)
		IL_000a: throw
	} // end of method LocalVarsTestCases::LocalOutOfRange

	.method public hidebysig 
		instance void LocalOutOfRangeMacro () cil managed 
	{
		// Header size: 12
		// Code size: 11 (0xb)
		.maxstack 1

		IL_0000: ldstr "InlineIL processing failed: Local index 1 is out of range (in System.Void InlineIL.Tests.InvalidAssemblyToProcess.LocalVarsTestCases::LocalOutOfRangeMacro() at instruction IL_0029: call System.Void InlineIL.IL/Emit::Ldloc_1())"
		IL_0005: newobj instance void [mscorlib]System.InvalidProgramException::.ctor(string)
		IL_000a: throw
	} // end of method LocalVarsTestCases::LocalOutOfRangeMacro

	.method public hidebysig 
		instance void MultipleDeclarations () cil managed 
	{
		// Header size: 12
		// Code size: 11 (0xb)
		.maxstack 1

		IL_0000: ldstr "InlineIL processing failed: Local variables have already been declared for this method (in System.Void InlineIL.Tests.InvalidAssemblyToProcess.LocalVarsTestCases::MultipleDeclarations() at instruction IL_004b: call System.Void InlineIL.IL::DeclareLocals(InlineIL.LocalVar[]))"
		IL_0005: newobj instance void [mscorlib]System.InvalidProgramException::.ctor(string)
		IL_000a: throw
	} // end of method LocalVarsTestCases::MultipleDeclarations

	.method public hidebysig 
		instance void NullLocal () cil managed 
	{
		// Header size: 12
		// Code size: 11 (0xb)
		.maxstack 1

		IL_0000: ldstr "InlineIL processing failed: Expected an array creation but was null (in System.Void InlineIL.Tests.InvalidAssemblyToProcess.LocalVarsTestCases::NullLocal() at instruction IL_0001: ldnull)"
		IL_0005: newobj instance void [mscorlib]System.InvalidProgramException::.ctor(string)
		IL_000a: throw
	} // end of method LocalVarsTestCases::NullLocal

	.method public hidebysig 
		instance void NullLocalName () cil managed 
	{
		// Header size: 12
		// Code size: 11 (0xb)
		.maxstack 1

		IL_0000: ldstr "InlineIL processing failed: A non-null string literal is expected (in System.Void InlineIL.Tests.InvalidAssemblyToProcess.LocalVarsTestCases::NullLocalName() at instruction IL_0009: ldnull)"
		IL_0005: newobj instance void [mscorlib]System.InvalidProgramException::.ctor(string)
		IL_000a: throw
	} // end of method LocalVarsTestCases::NullLocalName

	.method public hidebysig 
		instance void NullLocalRefName () cil managed 
	{
		// Header size: 1
		// Code size: 11 (0xb)
		.maxstack 8

		IL_0000: ldstr "InlineIL processing failed: A non-null string literal is expected (in System.Void InlineIL.Tests.InvalidAssemblyToProcess.LocalVarsTestCases::NullLocalRefName() at instruction IL_0001: ldnull)"
		IL_0005: newobj instance void [mscorlib]System.InvalidProgramException::.ctor(string)
		IL_000a: throw
	} // end of method LocalVarsTestCases::NullLocalRefName

	.method public hidebysig 
		instance void RedefinedLocal () cil managed 
	{
		// Header size: 12
		// Code size: 11 (0xb)
		.maxstack 1

		IL_0000: ldstr "InlineIL processing failed: Local foo is already defined (in System.Void InlineIL.Tests.InvalidAssemblyToProcess.LocalVarsTestCases::RedefinedLocal() at instruction IL_003f: call System.Void InlineIL.IL::DeclareLocals(InlineIL.LocalVar[]))"
		IL_0005: newobj instance void [mscorlib]System.InvalidProgramException::.ctor(string)
		IL_000a: throw
	} // end of method LocalVarsTestCases::RedefinedLocal

	.method public hidebysig 
		instance void UndefinedLocal () cil managed 
	{
		// Header size: 1
		// Code size: 11 (0xb)
		.maxstack 8

		IL_0000: ldstr "InlineIL processing failed: IL local 'foo' is not defined, call IL.DeclareLocals to declare IL locals, or IL.Push/IL.Pop to reference locals declared in the source code. (in System.Void InlineIL.Tests.InvalidAssemblyToProcess.LocalVarsTestCases::UndefinedLocal() at instruction IL_0001: ldstr \"foo\")"
		IL_0005: newobj instance void [mscorlib]System.InvalidProgramException::.ctor(string)
		IL_000a: throw
	} // end of method LocalVarsTestCases::UndefinedLocal

	.method public hidebysig 
		instance void UndefinedLocal2 () cil managed 
	{
		// Header size: 12
		// Code size: 11 (0xb)
		.maxstack 1

		IL_0000: ldstr "InlineIL processing failed: IL local 'foo' is not defined. If it is a local declared in the source code, use IL.Push/IL.Pop to reference it. (in System.Void InlineIL.Tests.InvalidAssemblyToProcess.LocalVarsTestCases::UndefinedLocal2() at instruction IL_0029: ldstr \"foo\")"
		IL_0005: newobj instance void [mscorlib]System.InvalidProgramException::.ctor(string)
		IL_000a: throw
	} // end of method LocalVarsTestCases::UndefinedLocal2

	.method public hidebysig 
		instance void UndefinedLocalByIndex () cil managed 
	{
		// Header size: 1
		// Code size: 11 (0xb)
		.maxstack 8

		IL_0000: ldstr "InlineIL processing failed: No locals are defined (in System.Void InlineIL.Tests.InvalidAssemblyToProcess.LocalVarsTestCases::UndefinedLocalByIndex() at instruction IL_0002: call System.Void InlineIL.IL/Emit::Ldloc(System.UInt16))"
		IL_0005: newobj instance void [mscorlib]System.InvalidProgramException::.ctor(string)
		IL_000a: throw
	} // end of method LocalVarsTestCases::UndefinedLocalByIndex

	.method public hidebysig 
		instance void UndefinedLocalByIndexMacro () cil managed 
	{
		// Header size: 1
		// Code size: 11 (0xb)
		.maxstack 8

		IL_0000: ldstr "InlineIL processing failed: No locals are defined (in System.Void InlineIL.Tests.InvalidAssemblyToProcess.LocalVarsTestCases::UndefinedLocalByIndexMacro() at instruction IL_0001: call System.Void InlineIL.IL/Emit::Ldloc_0())"
		IL_0005: newobj instance void [mscorlib]System.InvalidProgramException::.ctor(string)
		IL_000a: throw
	} // end of method LocalVarsTestCases::UndefinedLocalByIndexMacro

} // end of class InlineIL.Tests.InvalidAssemblyToProcess.LocalVarsTestCases
