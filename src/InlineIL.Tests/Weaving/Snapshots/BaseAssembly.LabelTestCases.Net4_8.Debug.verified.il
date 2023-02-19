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
		// Code size: 8 (0x8)
		.maxstack 8

		IL_0000: ldarg.0
		IL_0001: call instance void [mscorlib]System.Object::.ctor()
		IL_0006: nop
		IL_0007: ret
	} // end of method LabelTestCases::.ctor

	.method public hidebysig 
		instance int32 Branch (
			bool returnOne
		) cil managed 
	{
		// Header size: 12
		// Code size: 14 (0xe)
		.maxstack 1
		.locals init (
			[0] int32
		)

		IL_0000: nop
		IL_0001: ldarg.1
		IL_0002: brtrue.s IL_0008

		IL_0004: ldc.i4.s 42
		IL_0006: br.s IL_0009

		IL_0008: ldc.i4.1

		IL_0009: stloc.0
		IL_000a: br.s IL_000c

		IL_000c: ldloc.0
		IL_000d: ret
	} // end of method LabelTestCases::Branch

	.method public hidebysig 
		instance int32 JumpTable (
			uint32 'value'
		) cil managed 
	{
		// Header size: 12
		// Code size: 35 (0x23)
		.maxstack 1
		.locals init (
			[0] int32
		)

		IL_0000: nop
		IL_0001: ldarg.1
		IL_0002: switch (IL_0017, IL_001a, IL_001d)

		IL_0013: ldc.i4.s 42
		IL_0015: br.s IL_001e

		IL_0017: ldc.i4.1
		IL_0018: br.s IL_001e

		IL_001a: ldc.i4.2
		IL_001b: br.s IL_001e

		IL_001d: ldc.i4.3

		IL_001e: stloc.0
		IL_001f: br.s IL_0021

		IL_0021: ldloc.0
		IL_0022: ret
	} // end of method LabelTestCases::JumpTable

	.method public hidebysig 
		instance void UnusedLabel () cil managed 
	{
		// Header size: 1
		// Code size: 2 (0x2)
		.maxstack 8

		IL_0000: nop
		IL_0001: ret
	} // end of method LabelTestCases::UnusedLabel

} // end of class InlineIL.Tests.AssemblyToProcess.LabelTestCases
