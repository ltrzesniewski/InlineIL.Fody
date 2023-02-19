.class public auto ansi beforefieldinit InlineIL.Tests.InvalidAssemblyToProcess.LabelTestCases
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
	} // end of method LabelTestCases::.ctor

	.method public hidebysig 
		instance void NullLabel () cil managed 
	{
		// Header size: 1
		// Code size: 11 (0xb)
		.maxstack 8

		IL_0000: ldstr "InlineIL processing failed: A non-null string literal is expected (in System.Void InlineIL.Tests.InvalidAssemblyToProcess.LabelTestCases::NullLabel() at instruction IL_0000: ldnull)"
		IL_0005: newobj instance void [netstandard]System.InvalidProgramException::.ctor(string)
		IL_000a: throw
	} // end of method LabelTestCases::NullLabel

	.method public hidebysig 
		instance void NullLabelName () cil managed 
	{
		// Header size: 1
		// Code size: 11 (0xb)
		.maxstack 8

		IL_0000: ldstr "InlineIL processing failed: A non-null string literal is expected (in System.Void InlineIL.Tests.InvalidAssemblyToProcess.LabelTestCases::NullLabelName() at instruction IL_0000: ldnull)"
		IL_0005: newobj instance void [netstandard]System.InvalidProgramException::.ctor(string)
		IL_000a: throw
	} // end of method LabelTestCases::NullLabelName

	.method public hidebysig 
		instance void RedefinedLabel () cil managed 
	{
		// Header size: 1
		// Code size: 11 (0xb)
		.maxstack 8

		IL_0000: ldstr "InlineIL processing failed: Label 'foo' is already defined (in System.Void InlineIL.Tests.InvalidAssemblyToProcess.LabelTestCases::RedefinedLabel() at instruction IL_000f: call System.Void InlineIL.IL::MarkLabel(System.String))"
		IL_0005: newobj instance void [netstandard]System.InvalidProgramException::.ctor(string)
		IL_000a: throw
	} // end of method LabelTestCases::RedefinedLabel

	.method public hidebysig 
		instance void UndefinedLabel () cil managed 
	{
		// Header size: 1
		// Code size: 11 (0xb)
		.maxstack 8

		IL_0000: ldstr "InlineIL processing failed: Undefined label: 'foo' (in System.Void InlineIL.Tests.InvalidAssemblyToProcess.LabelTestCases::UndefinedLabel() at instruction IL_0000: br IL_0000)"
		IL_0005: newobj instance void [netstandard]System.InvalidProgramException::.ctor(string)
		IL_000a: throw
	} // end of method LabelTestCases::UndefinedLabel

} // end of class InlineIL.Tests.InvalidAssemblyToProcess.LabelTestCases
