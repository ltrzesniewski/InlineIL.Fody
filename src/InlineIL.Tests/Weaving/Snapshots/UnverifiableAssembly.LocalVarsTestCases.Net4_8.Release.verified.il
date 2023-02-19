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
		// Code size: 7 (0x7)
		.maxstack 8

		IL_0000: ldarg.0
		IL_0001: call instance void [mscorlib]System.Object::.ctor()
		IL_0006: ret
	} // end of method LocalVarsTestCases::.ctor

	.method public hidebysig 
		instance int32 UseLocalVariablesNoInit (
			int32 'value'
		) cil managed 
	{
		// Header size: 12
		// Code size: 9 (0x9)
		.maxstack 2
		.locals (
			[0] int32,
			[1] int32
		)

		IL_0000: ldarg.1
		IL_0001: stloc.0
		IL_0002: ldc.i4.s 42
		IL_0004: stloc.1
		IL_0005: ldloc.0
		IL_0006: ldloc.1
		IL_0007: add
		IL_0008: ret
	} // end of method LocalVarsTestCases::UseLocalVariablesNoInit

	.method public hidebysig 
		instance uint8 UsePinnedLocalVariables (
			uint8[] buf,
			int32 offset
		) cil managed 
	{
		// Header size: 12
		// Code size: 14 (0xe)
		.maxstack 2
		.locals init (
			[0] uint8& pinned
		)

		IL_0000: ldarg.1
		IL_0001: ldc.i4.0
		IL_0002: ldelema [mscorlib]System.Byte
		IL_0007: stloc.0
		IL_0008: ldloc.0
		IL_0009: conv.i
		IL_000a: ldarg.2
		IL_000b: add
		IL_000c: ldind.u1
		IL_000d: ret
	} // end of method LocalVarsTestCases::UsePinnedLocalVariables

} // end of class InlineIL.Tests.UnverifiableAssemblyToProcess.LocalVarsTestCases
