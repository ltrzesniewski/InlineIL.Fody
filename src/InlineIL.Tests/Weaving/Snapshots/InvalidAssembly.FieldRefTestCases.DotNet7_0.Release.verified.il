.class public auto ansi beforefieldinit InlineIL.Tests.InvalidAssemblyToProcess.FieldRefTestCases
	extends [netstandard]System.Object
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
		IL_0001: call instance void [netstandard]System.Object::.ctor()
		IL_0006: ret
	} // end of method FieldRefTestCases::.ctor

	.method public hidebysig 
		instance void NullField () cil managed 
	{
		// Header size: 1
		// Code size: 11 (0xb)
		.maxstack 8

		IL_0000: ldstr "InlineIL processing failed: A non-null string literal is expected (in System.Void InlineIL.Tests.InvalidAssemblyToProcess.FieldRefTestCases::NullField() at instruction IL_000f: ldnull)"
		IL_0005: newobj instance void [netstandard]System.InvalidProgramException::.ctor(string)
		IL_000a: throw
	} // end of method FieldRefTestCases::NullField

	.method public hidebysig 
		instance void NullFieldRef () cil managed 
	{
		// Header size: 1
		// Code size: 11 (0xb)
		.maxstack 8

		IL_0000: ldstr "InlineIL processing failed: Expected a method call but was null (in System.Void InlineIL.Tests.InvalidAssemblyToProcess.FieldRefTestCases::NullFieldRef() at instruction IL_0000: ldnull)"
		IL_0005: newobj instance void [netstandard]System.InvalidProgramException::.ctor(string)
		IL_000a: throw
	} // end of method FieldRefTestCases::NullFieldRef

	.method public hidebysig 
		instance void UnknownField () cil managed 
	{
		// Header size: 1
		// Code size: 11 (0xb)
		.maxstack 8

		IL_0000: ldstr "InlineIL processing failed: Field 'Nope' not found in type InlineIL.Tests.InvalidAssemblyToProcess.FieldRefTestCases (in System.Void InlineIL.Tests.InvalidAssemblyToProcess.FieldRefTestCases::UnknownField() at instruction IL_0019: call System.Void InlineIL.IL/Emit::Ldfld(InlineIL.FieldRef))"
		IL_0005: newobj instance void [netstandard]System.InvalidProgramException::.ctor(string)
		IL_000a: throw
	} // end of method FieldRefTestCases::UnknownField

	.method public hidebysig 
		instance void UnusedInstance () cil managed 
	{
		// Header size: 1
		// Code size: 11 (0xb)
		.maxstack 8

		IL_0000: ldstr "InlineIL processing failed: Unconsumed reference to InlineIL (in System.Void InlineIL.Tests.InvalidAssemblyToProcess.FieldRefTestCases::UnusedInstance() at instruction IL_000a: call InlineIL.TypeRef InlineIL.TypeRef::op_Implicit(System.Type))"
		IL_0005: newobj instance void [netstandard]System.InvalidProgramException::.ctor(string)
		IL_000a: throw
	} // end of method FieldRefTestCases::UnusedInstance

} // end of class InlineIL.Tests.InvalidAssemblyToProcess.FieldRefTestCases
