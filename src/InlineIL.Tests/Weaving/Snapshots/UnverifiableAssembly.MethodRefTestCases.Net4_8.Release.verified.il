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
		// Code size: 7 (0x7)
		.maxstack 8

		IL_0000: ldarg.0
		IL_0001: call instance void [mscorlib]System.Object::.ctor()
		IL_0006: ret
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
		// Code size: 97 (0x61)
		.maxstack 5
		.locals init (
			[0] int32[]
		)

		IL_0000: ldc.i4.7
		IL_0001: newarr [mscorlib]System.Int32
		IL_0006: stloc.0
		IL_0007: ldloc.0
		IL_0008: ldc.i4.0
		IL_0009: call int32 InlineIL.Tests.UnverifiableAssemblyToProcess.MethodRefTestCases::OverloadedMethod()
		IL_000e: stelem.i4
		IL_000f: ldloc.0
		IL_0010: ldc.i4.1
		IL_0011: call int32 InlineIL.Tests.UnverifiableAssemblyToProcess.MethodRefTestCases::OverloadedMethod()
		IL_0016: stelem.i4
		IL_0017: ldloc.0
		IL_0018: ldc.i4.2
		IL_0019: ldc.i4.s 42
		IL_001b: call int32 InlineIL.Tests.UnverifiableAssemblyToProcess.MethodRefTestCases::OverloadedMethod(int32)
		IL_0020: stelem.i4
		IL_0021: ldloc.0
		IL_0022: ldc.i4.3
		IL_0023: ldc.i4.0
		IL_0024: conv.u
		IL_0025: call int32 InlineIL.Tests.UnverifiableAssemblyToProcess.MethodRefTestCases::OverloadedMethod(int32**)
		IL_002a: stelem.i4
		IL_002b: ldloc.0
		IL_002c: ldc.i4.4
		IL_002d: ldnull
		IL_002e: call int32 InlineIL.Tests.UnverifiableAssemblyToProcess.MethodRefTestCases::OverloadedMethod(int32[])
		IL_0033: stelem.i4
		IL_0034: ldloc.0
		IL_0035: ldc.i4.5
		IL_0036: ldc.r8 42
		IL_003f: ldloc.0
		IL_0040: ldc.i4.0
		IL_0041: ldelema [mscorlib]System.Int32
		IL_0046: call int32 InlineIL.Tests.UnverifiableAssemblyToProcess.MethodRefTestCases::OverloadedMethod(float64, int32&)
		IL_004b: stelem.i4
		IL_004c: ldloc.0
		IL_004d: ldc.i4.6
		IL_004e: ldc.r8 42
		IL_0057: ldc.i4.s 42
		IL_0059: call int32 InlineIL.Tests.UnverifiableAssemblyToProcess.MethodRefTestCases::OverloadedMethod(float64, int32)
		IL_005e: stelem.i4
		IL_005f: ldloc.0
		IL_0060: ret
	} // end of method MethodRefTestCases::ResolveOverloads

} // end of class InlineIL.Tests.UnverifiableAssemblyToProcess.MethodRefTestCases
