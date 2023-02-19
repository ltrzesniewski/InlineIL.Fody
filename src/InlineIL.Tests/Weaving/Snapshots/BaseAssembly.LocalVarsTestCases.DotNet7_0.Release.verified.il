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
		// Code size: 7 (0x7)
		.maxstack 8

		IL_0000: ldarg.0
		IL_0001: call instance void [System.Runtime]System.Object::.ctor()
		IL_0006: ret
	} // end of method LocalVarsTestCases::.ctor

	.method public hidebysig 
		instance int32 DeclareLocalVariablesAtThEnd (
			int32 'value'
		) cil managed 
	{
		// Header size: 12
		// Code size: 9 (0x9)
		.maxstack 2
		.locals init (
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
		// Code size: 71 (0x47)
		.maxstack 3
		.locals init (
			[0] float64,
			[1] float64,
			[2] int32,
			[3] int32,
			[4] int32,
			[5] int32
		)

		IL_0000: ldc.r8 0.7853981633974483
		IL_0009: call float64 [System.Runtime]System.Math::Cos(float64)
		IL_000e: ldc.r8 42
		IL_0017: call float64 [System.Runtime]System.Math::Log(float64)
		IL_001c: stloc.0
		IL_001d: dup
		IL_001e: call float64 [System.Runtime]System.Math::Acos(float64)
		IL_0023: stloc.1
		IL_0024: ldloc.0
		IL_0025: add
		IL_0026: ldloc.1
		IL_0027: add
		IL_0028: box [System.Runtime]System.Double
		IL_002d: call void [System.Runtime]System.GC::KeepAlive(object)
		IL_0032: ldarg.1
		IL_0033: stloc.2
		IL_0034: ldarg.2
		IL_0035: stloc.3
		IL_0036: ldarg.3
		IL_0037: stloc.s 4
		IL_0039: ldarg.s d
		IL_003b: stloc.s 5
		IL_003d: ldloc.2
		IL_003e: ldloc.3
		IL_003f: mul
		IL_0040: ldloc.s 4
		IL_0042: ldloc.s 5
		IL_0044: div
		IL_0045: add
		IL_0046: ret
	} // end of method LocalVarsTestCases::MapLocalIndexes

	.method public hidebysig 
		instance int32 MapLocalIndexesLong (
			int32 a,
			int32 b
		) cil managed 
	{
		// Header size: 12
		// Code size: 58 (0x3a)
		.maxstack 2
		.locals init (
			[0] float64,
			[1] float64,
			[2] int32,
			[3] int32
		)

		IL_0000: ldc.r8 0.7853981633974483
		IL_0009: call float64 [System.Runtime]System.Math::Cos(float64)
		IL_000e: ldc.r8 42
		IL_0017: call float64 [System.Runtime]System.Math::Log(float64)
		IL_001c: stloc.0
		IL_001d: dup
		IL_001e: call float64 [System.Runtime]System.Math::Acos(float64)
		IL_0023: stloc.1
		IL_0024: ldloc.0
		IL_0025: add
		IL_0026: ldloc.1
		IL_0027: add
		IL_0028: box [System.Runtime]System.Double
		IL_002d: call void [System.Runtime]System.GC::KeepAlive(object)
		IL_0032: ldarg.1
		IL_0033: stloc.2
		IL_0034: ldarg.2
		IL_0035: stloc.3
		IL_0036: ldloc.2
		IL_0037: ldloc.3
		IL_0038: add
		IL_0039: ret
	} // end of method LocalVarsTestCases::MapLocalIndexesLong

	.method public hidebysig 
		instance void MixedNamedAndUnnamed () cil managed 
	{
		// Header size: 12
		// Code size: 1 (0x1)
		.maxstack 0
		.locals init (
			[0] int32,
			[1] int32,
			[2] int32
		)

		IL_0000: ret
	} // end of method LocalVarsTestCases::MixedNamedAndUnnamed

	.method public hidebysig 
		instance void TypedReference () cil managed 
	{
		// Header size: 12
		// Code size: 1 (0x1)
		.maxstack 0
		.locals init (
			[0] typedref
		)

		IL_0000: ret
	} // end of method LocalVarsTestCases::TypedReference

	.method public hidebysig 
		instance int32 UseLocalVariables (
			int32 'value'
		) cil managed 
	{
		// Header size: 12
		// Code size: 9 (0x9)
		.maxstack 2
		.locals init (
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
	} // end of method LocalVarsTestCases::UseLocalVariables

	.method public hidebysig 
		instance int32 UseLocalVariablesExplicitInit (
			int32 'value'
		) cil managed 
	{
		// Header size: 12
		// Code size: 9 (0x9)
		.maxstack 2
		.locals init (
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
	} // end of method LocalVarsTestCases::UseLocalVariablesExplicitInit

	.method public hidebysig 
		instance void WithOptionalModifier () cil managed 
	{
		// Header size: 12
		// Code size: 1 (0x1)
		.maxstack 0
		.locals init (
			[0] int32 modopt([System.Runtime]System.Runtime.CompilerServices.IsConst)
		)

		IL_0000: ret
	} // end of method LocalVarsTestCases::WithOptionalModifier

	.method public hidebysig 
		instance void WithRequiredModifier () cil managed 
	{
		// Header size: 12
		// Code size: 1 (0x1)
		.maxstack 0
		.locals init (
			[0] int32 modreq([System.Runtime]System.Runtime.CompilerServices.IsConst)
		)

		IL_0000: ret
	} // end of method LocalVarsTestCases::WithRequiredModifier

} // end of class InlineIL.Tests.AssemblyToProcess.LocalVarsTestCases
