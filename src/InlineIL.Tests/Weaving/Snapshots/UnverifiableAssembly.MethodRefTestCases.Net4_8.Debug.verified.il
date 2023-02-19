.class public auto ansi beforefieldinit InlineIL.Tests.UnverifiableAssemblyToProcess.MethodRefTestCases
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
	} // end of method MethodRefTestCases::.ctor

	.method private hidebysig static 
		int32 OverloadedMethod () cil managed 
	{
		// Header size: 1
		// Code size: 3 (0x3)
		.maxstack 8

		IL_0000: ldc.i4.s 10
		IL_0002: ret
	} // end of method MethodRefTestCases::OverloadedMethod

	.method private hidebysig static 
		int32 OverloadedMethod (
			float64 a,
			int32& b
		) cil managed 
	{
		// Header size: 1
		// Code size: 3 (0x3)
		.maxstack 8

		IL_0000: ldc.i4.s 50
		IL_0002: ret
	} // end of method MethodRefTestCases::OverloadedMethod

	.method private hidebysig static 
		int32 OverloadedMethod (
			float64 a,
			int32 b
		) cil managed 
	{
		// Header size: 1
		// Code size: 3 (0x3)
		.maxstack 8

		IL_0000: ldc.i4.s 60
		IL_0002: ret
	} // end of method MethodRefTestCases::OverloadedMethod

	.method private hidebysig static 
		int32 OverloadedMethod (
			int32 a
		) cil managed 
	{
		// Header size: 1
		// Code size: 3 (0x3)
		.maxstack 8

		IL_0000: ldc.i4.s 20
		IL_0002: ret
	} // end of method MethodRefTestCases::OverloadedMethod

	.method private hidebysig static 
		int32 OverloadedMethod (
			int32** a
		) cil managed 
	{
		// Header size: 1
		// Code size: 3 (0x3)
		.maxstack 8

		IL_0000: ldc.i4.s 30
		IL_0002: ret
	} // end of method MethodRefTestCases::OverloadedMethod

	.method private hidebysig static 
		int32 OverloadedMethod (
			int32[] a
		) cil managed 
	{
		// Header size: 1
		// Code size: 3 (0x3)
		.maxstack 8

		IL_0000: ldc.i4.s 40
		IL_0002: ret
	} // end of method MethodRefTestCases::OverloadedMethod

	.method public hidebysig 
		instance int32[] ResolveOverloads () cil managed 
	{
		// Header size: 12
		// Code size: 102 (0x66)
		.maxstack 5
		.locals init (
			[0] int32[],
			[1] int32[]
		)

		IL_0000: nop
		IL_0001: ldc.i4.7
		IL_0002: newarr [mscorlib]System.Int32
		IL_0007: stloc.0
		IL_0008: ldloc.0
		IL_0009: ldc.i4.0
		IL_000a: call int32 InlineIL.Tests.UnverifiableAssemblyToProcess.MethodRefTestCases::OverloadedMethod()
		IL_000f: stelem.i4
		IL_0010: ldloc.0
		IL_0011: ldc.i4.1
		IL_0012: call int32 InlineIL.Tests.UnverifiableAssemblyToProcess.MethodRefTestCases::OverloadedMethod()
		IL_0017: stelem.i4
		IL_0018: ldloc.0
		IL_0019: ldc.i4.2
		IL_001a: ldc.i4.s 42
		IL_001c: call int32 InlineIL.Tests.UnverifiableAssemblyToProcess.MethodRefTestCases::OverloadedMethod(int32)
		IL_0021: stelem.i4
		IL_0022: ldloc.0
		IL_0023: ldc.i4.3
		IL_0024: ldc.i4.0
		IL_0025: conv.u
		IL_0026: call int32 InlineIL.Tests.UnverifiableAssemblyToProcess.MethodRefTestCases::OverloadedMethod(int32**)
		IL_002b: stelem.i4
		IL_002c: ldloc.0
		IL_002d: ldc.i4.4
		IL_002e: ldnull
		IL_002f: call int32 InlineIL.Tests.UnverifiableAssemblyToProcess.MethodRefTestCases::OverloadedMethod(int32[])
		IL_0034: stelem.i4
		IL_0035: ldloc.0
		IL_0036: ldc.i4.5
		IL_0037: ldc.r8 42
		IL_0040: ldloc.0
		IL_0041: ldc.i4.0
		IL_0042: ldelema [mscorlib]System.Int32
		IL_0047: call int32 InlineIL.Tests.UnverifiableAssemblyToProcess.MethodRefTestCases::OverloadedMethod(float64, int32&)
		IL_004c: stelem.i4
		IL_004d: ldloc.0
		IL_004e: ldc.i4.6
		IL_004f: ldc.r8 42
		IL_0058: ldc.i4.s 42
		IL_005a: call int32 InlineIL.Tests.UnverifiableAssemblyToProcess.MethodRefTestCases::OverloadedMethod(float64, int32)
		IL_005f: stelem.i4
		IL_0060: ldloc.0
		IL_0061: stloc.1
		IL_0062: br.s IL_0064

		IL_0064: ldloc.1
		IL_0065: ret
	} // end of method MethodRefTestCases::ResolveOverloads

} // end of class InlineIL.Tests.UnverifiableAssemblyToProcess.MethodRefTestCases
