.class public auto ansi beforefieldinit InlineIL.Tests.AssemblyToProcess.LocalVarsTestCases
	extends [System.Runtime]System.Object
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
		IL_0001: call instance void [System.Runtime]System.Object::.ctor()
		IL_0006: nop
		IL_0007: ret
	} // end of method LocalVarsTestCases::.ctor

	.method public hidebysig 
		instance int32 DeclareLocalVariablesAtThEnd (
			int32 'value'
		) cil managed 
	{
		// Header size: 12
		// Code size: 14 (0xe)
		.maxstack 2
		.locals init (
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
	} // end of method LocalVarsTestCases::DeclareLocalVariablesAtThEnd

	.method public hidebysig 
		instance int32 MapLocalIndexes (
			int32 a,
			int32 b,
			int32 c,
			int32 d
		) cil managed 
	{
		// Header size: 12
		// Code size: 83 (0x53)
		.maxstack 3
		.locals init (
			[0] float64,
			[1] float64,
			[2] float64,
			[3] int32,
			[4] int32,
			[5] int32,
			[6] int32,
			[7] int32
		)

		IL_0000: nop
		IL_0001: ldc.r8 0.7853981633974483
		IL_000a: call float64 [System.Runtime]System.Math::Cos(float64)
		IL_000f: stloc.0
		IL_0010: ldc.r8 42
		IL_0019: call float64 [System.Runtime]System.Math::Log(float64)
		IL_001e: stloc.1
		IL_001f: ldloc.0
		IL_0020: call float64 [System.Runtime]System.Math::Acos(float64)
		IL_0025: stloc.2
		IL_0026: ldloc.0
		IL_0027: ldloc.1
		IL_0028: add
		IL_0029: ldloc.2
		IL_002a: add
		IL_002b: box [System.Runtime]System.Double
		IL_0030: call void [System.Runtime]System.GC::KeepAlive(object)
		IL_0035: nop
		IL_0036: ldarg.1
		IL_0037: stloc.s 4
		IL_0039: ldarg.2
		IL_003a: stloc.s 5
		IL_003c: ldarg.3
		IL_003d: stloc.s 6
		IL_003f: ldarg.s d
		IL_0041: stloc.s 7
		IL_0043: ldloc.s 4
		IL_0045: ldloc.s 5
		IL_0047: mul
		IL_0048: ldloc.s 6
		IL_004a: ldloc.s 7
		IL_004c: div
		IL_004d: add
		IL_004e: stloc.3
		IL_004f: br.s IL_0051

		IL_0051: ldloc.3
		IL_0052: ret
	} // end of method LocalVarsTestCases::MapLocalIndexes

	.method public hidebysig 
		instance int32 MapLocalIndexesLong (
			int32 a,
			int32 b
		) cil managed 
	{
		// Header size: 12
		// Code size: 70 (0x46)
		.maxstack 2
		.locals init (
			[0] float64,
			[1] float64,
			[2] float64,
			[3] int32,
			[4] int32,
			[5] int32
		)

		IL_0000: nop
		IL_0001: ldc.r8 0.7853981633974483
		IL_000a: call float64 [System.Runtime]System.Math::Cos(float64)
		IL_000f: stloc.0
		IL_0010: ldc.r8 42
		IL_0019: call float64 [System.Runtime]System.Math::Log(float64)
		IL_001e: stloc.1
		IL_001f: ldloc.0
		IL_0020: call float64 [System.Runtime]System.Math::Acos(float64)
		IL_0025: stloc.2
		IL_0026: ldloc.0
		IL_0027: ldloc.1
		IL_0028: add
		IL_0029: ldloc.2
		IL_002a: add
		IL_002b: box [System.Runtime]System.Double
		IL_0030: call void [System.Runtime]System.GC::KeepAlive(object)
		IL_0035: nop
		IL_0036: ldarg.1
		IL_0037: stloc.s 4
		IL_0039: ldarg.2
		IL_003a: stloc.s 5
		IL_003c: ldloc.s 4
		IL_003e: ldloc.s 5
		IL_0040: add
		IL_0041: stloc.3
		IL_0042: br.s IL_0044

		IL_0044: ldloc.3
		IL_0045: ret
	} // end of method LocalVarsTestCases::MapLocalIndexesLong

	.method public hidebysig 
		instance void MixedNamedAndUnnamed () cil managed 
	{
		// Header size: 12
		// Code size: 2 (0x2)
		.maxstack 0
		.locals init (
			[0] int32,
			[1] int32,
			[2] int32
		)

		IL_0000: nop
		IL_0001: ret
	} // end of method LocalVarsTestCases::MixedNamedAndUnnamed

	.method public hidebysig 
		instance void TypedReference () cil managed 
	{
		// Header size: 12
		// Code size: 2 (0x2)
		.maxstack 0
		.locals init (
			[0] typedref
		)

		IL_0000: nop
		IL_0001: ret
	} // end of method LocalVarsTestCases::TypedReference

	.method public hidebysig 
		instance int32 UseLocalVariables (
			int32 'value'
		) cil managed 
	{
		// Header size: 12
		// Code size: 14 (0xe)
		.maxstack 2
		.locals init (
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
	} // end of method LocalVarsTestCases::UseLocalVariables

	.method public hidebysig 
		instance int32 UseLocalVariablesExplicitInit (
			int32 'value'
		) cil managed 
	{
		// Header size: 12
		// Code size: 14 (0xe)
		.maxstack 2
		.locals init (
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
	} // end of method LocalVarsTestCases::UseLocalVariablesExplicitInit

	.method public hidebysig 
		instance void WithOptionalModifier () cil managed 
	{
		// Header size: 12
		// Code size: 2 (0x2)
		.maxstack 0
		.locals init (
			[0] int32 modopt([System.Runtime]System.Runtime.CompilerServices.IsConst)
		)

		IL_0000: nop
		IL_0001: ret
	} // end of method LocalVarsTestCases::WithOptionalModifier

	.method public hidebysig 
		instance void WithRequiredModifier () cil managed 
	{
		// Header size: 12
		// Code size: 2 (0x2)
		.maxstack 0
		.locals init (
			[0] int32 modreq([System.Runtime]System.Runtime.CompilerServices.IsConst)
		)

		IL_0000: nop
		IL_0001: ret
	} // end of method LocalVarsTestCases::WithRequiredModifier

} // end of class InlineIL.Tests.AssemblyToProcess.LocalVarsTestCases
