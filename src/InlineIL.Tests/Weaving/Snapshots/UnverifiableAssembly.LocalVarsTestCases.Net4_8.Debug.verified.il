.class public auto ansi beforefieldinit InlineIL.Tests.UnverifiableAssemblyToProcess.LocalVarsTestCases
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
		instance int32 UseLocalVariablesNoInit (
			int32 'value'
		) cil managed 
	{
		// Header size: 12
		// Code size: 14 (0xe)
		.maxstack 2
		.locals (
			[0] int32,
			[1] int32,
			[2] int32
		)

		IL_0000: nop
		IL_0001: ldarg.1
		IL_0002: stloc.1
		IL_0003: ldc.i4.s 42
		IL_0005: stloc.2
		IL_0006: ldloc.1
		IL_0007: ldloc.2
		IL_0008: add
		IL_0009: stloc.0
		IL_000a: br.s IL_000c

		IL_000c: ldloc.0
		IL_000d: ret
	} // end of method LocalVarsTestCases::UseLocalVariablesNoInit

	.method public hidebysig 
		instance uint8 UsePinnedLocalVariables (
			uint8[] buf,
			int32 offset
		) cil managed 
	{
		// Header size: 12
		// Code size: 19 (0x13)
		.maxstack 2
		.locals init (
			[0] uint8,
			[1] uint8& pinned
		)

		IL_0000: nop
		IL_0001: ldarg.1
		IL_0002: ldc.i4.0
		IL_0003: ldelema [mscorlib]System.Byte
		IL_0008: stloc.1
		IL_0009: ldloc.1
		IL_000a: conv.i
		IL_000b: ldarg.2
		IL_000c: add
		IL_000d: ldind.u1
		IL_000e: stloc.0
		IL_000f: br.s IL_0011

		IL_0011: ldloc.0
		IL_0012: ret
	} // end of method LocalVarsTestCases::UsePinnedLocalVariables

} // end of class InlineIL.Tests.UnverifiableAssemblyToProcess.LocalVarsTestCases
