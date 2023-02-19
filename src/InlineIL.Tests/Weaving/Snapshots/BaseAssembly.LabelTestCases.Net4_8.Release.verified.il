.class public auto ansi beforefieldinit InlineIL.Tests.AssemblyToProcess.LabelTestCases
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
	} // end of method LabelTestCases::.ctor

	.method public hidebysig 
		instance int32 Branch (
			bool returnOne
		) cil managed 
	{
		// Header size: 12
		// Code size: 9 (0x9)
		.maxstack 1

		IL_0000: ldarg.1
		IL_0001: brtrue.s IL_0007

		IL_0003: ldc.i4.s 42
		IL_0005: br.s IL_0008

		IL_0007: ldc.i4.1

		IL_0008: ret
	} // end of method LabelTestCases::Branch

	.method public hidebysig 
		instance int32 JumpTable (
			uint32 'value'
		) cil managed 
	{
		// Header size: 12
		// Code size: 30 (0x1e)
		.maxstack 1

		IL_0000: ldarg.1
		IL_0001: switch (IL_0016, IL_0019, IL_001c)

		IL_0012: ldc.i4.s 42
		IL_0014: br.s IL_001d

		IL_0016: ldc.i4.1
		IL_0017: br.s IL_001d

		IL_0019: ldc.i4.2
		IL_001a: br.s IL_001d

		IL_001c: ldc.i4.3

		IL_001d: ret
	} // end of method LabelTestCases::JumpTable

	.method public hidebysig 
		instance void UnusedLabel () cil managed 
	{
		// Header size: 1
		// Code size: 1 (0x1)
		.maxstack 8

		IL_0000: ret
	} // end of method LabelTestCases::UnusedLabel

} // end of class InlineIL.Tests.AssemblyToProcess.LabelTestCases
