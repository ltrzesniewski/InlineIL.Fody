.class public auto ansi beforefieldinit InlineIL.Tests.AssemblyToProcess.TypeRefTestCases
	extends [netstandard]System.Object
{
	.custom instance void [InlineIL.Tests.Common]InlineIL.Tests.Common.TestCasesAttribute::.ctor() = (
		01 00 00 00
	)
	// Nested Types
	.class nested private auto ansi beforefieldinit NestedType
		extends [netstandard]System.Object
	{
		// Methods
		.method public hidebysig specialname rtspecialname 
			instance void .ctor () cil managed 
		{
			// Header size: 1
			// Code size: 8 (0x8)
			.maxstack 8

			IL_0000: ldarg.0
			IL_0001: call instance void [netstandard]System.Object::.ctor()
			IL_0006: nop
			IL_0007: ret
		} // end of method NestedType::.ctor

	} // end of class NestedType


	// Fields
	.field private static literal string _thisAssemblyName = "InlineIL.Tests.StandardAssemblyToProcess"

	// Methods
	.method public hidebysig specialname rtspecialname 
		instance void .ctor () cil managed 
	{
		// Header size: 1
		// Code size: 8 (0x8)
		.maxstack 8

		IL_0000: ldarg.0
		IL_0001: call instance void [netstandard]System.Object::.ctor()
		IL_0006: nop
		IL_0007: ret
	} // end of method TypeRefTestCases::.ctor

	.method public hidebysig 
		instance bool GenericIsinst<T> (
			object obj
		) cil managed 
	{
		// Header size: 12
		// Code size: 15 (0xf)
		.maxstack 2
		.locals init (
			[0] bool
		)

		IL_0000: nop
		IL_0001: ldarg.1
		IL_0002: isinst !!T
		IL_0007: ldnull
		IL_0008: cgt.un
		IL_000a: stloc.0
		IL_000b: br.s IL_000d

		IL_000d: ldloc.0
		IL_000e: ret
	} // end of method TypeRefTestCases::GenericIsinst

	.method public hidebysig 
		instance bool IsString (
			object obj
		) cil managed 
	{
		// Header size: 12
		// Code size: 15 (0xf)
		.maxstack 2
		.locals init (
			[0] bool
		)

		IL_0000: nop
		IL_0001: ldarg.1
		IL_0002: isinst [netstandard]System.String
		IL_0007: ldnull
		IL_0008: cgt.un
		IL_000a: stloc.0
		IL_000b: br.s IL_000d

		IL_000d: ldloc.0
		IL_000e: ret
	} // end of method TypeRefTestCases::IsString

	.method public hidebysig 
		instance valuetype [netstandard]System.RuntimeTypeHandle LoadArrayTypeUsingType () cil managed 
	{
		// Header size: 12
		// Code size: 11 (0xb)
		.maxstack 1
		.locals init (
			[0] valuetype [netstandard]System.RuntimeTypeHandle
		)

		IL_0000: nop
		IL_0001: ldtoken int32[][]
		IL_0006: stloc.0
		IL_0007: br.s IL_0009

		IL_0009: ldloc.0
		IL_000a: ret
	} // end of method TypeRefTestCases::LoadArrayTypeUsingType

	.method public hidebysig 
		instance valuetype [netstandard]System.RuntimeTypeHandle LoadArrayTypeUsingTypeRef () cil managed 
	{
		// Header size: 12
		// Code size: 11 (0xb)
		.maxstack 1
		.locals init (
			[0] valuetype [netstandard]System.RuntimeTypeHandle
		)

		IL_0000: nop
		IL_0001: ldtoken int32[][]
		IL_0006: stloc.0
		IL_0007: br.s IL_0009

		IL_0009: ldloc.0
		IL_000a: ret
	} // end of method TypeRefTestCases::LoadArrayTypeUsingTypeRef

	.method public hidebysig 
		instance valuetype [netstandard]System.RuntimeTypeHandle LoadArrayTypeWithRankUsingType () cil managed 
	{
		// Header size: 12
		// Code size: 11 (0xb)
		.maxstack 1
		.locals init (
			[0] valuetype [netstandard]System.RuntimeTypeHandle
		)

		IL_0000: nop
		IL_0001: ldtoken int32[, , ][]
		IL_0006: stloc.0
		IL_0007: br.s IL_0009

		IL_0009: ldloc.0
		IL_000a: ret
	} // end of method TypeRefTestCases::LoadArrayTypeWithRankUsingType

	.method public hidebysig 
		instance valuetype [netstandard]System.RuntimeTypeHandle LoadArrayTypeWithRankUsingTypeRef () cil managed 
	{
		// Header size: 12
		// Code size: 11 (0xb)
		.maxstack 1
		.locals init (
			[0] valuetype [netstandard]System.RuntimeTypeHandle
		)

		IL_0000: nop
		IL_0001: ldtoken int32[, , ][]
		IL_0006: stloc.0
		IL_0007: br.s IL_0009

		IL_0009: ldloc.0
		IL_000a: ret
	} // end of method TypeRefTestCases::LoadArrayTypeWithRankUsingTypeRef

	.method public hidebysig 
		instance valuetype [netstandard]System.RuntimeTypeHandle LoadGenericTypeByName () cil managed 
	{
		// Header size: 12
		// Code size: 11 (0xb)
		.maxstack 1
		.locals init (
			[0] valuetype [netstandard]System.RuntimeTypeHandle
		)

		IL_0000: nop
		IL_0001: ldtoken class [netstandard]System.Action`1<int32>
		IL_0006: stloc.0
		IL_0007: br.s IL_0009

		IL_0009: ldloc.0
		IL_000a: ret
	} // end of method TypeRefTestCases::LoadGenericTypeByName

	.method public hidebysig 
		instance valuetype [netstandard]System.RuntimeTypeHandle LoadGenericTypeUsingType () cil managed 
	{
		// Header size: 12
		// Code size: 11 (0xb)
		.maxstack 1
		.locals init (
			[0] valuetype [netstandard]System.RuntimeTypeHandle
		)

		IL_0000: nop
		IL_0001: ldtoken class [netstandard]System.Collections.Generic.Dictionary`2<int32, string>
		IL_0006: stloc.0
		IL_0007: br.s IL_0009

		IL_0009: ldloc.0
		IL_000a: ret
	} // end of method TypeRefTestCases::LoadGenericTypeUsingType

	.method public hidebysig 
		instance valuetype [netstandard]System.RuntimeTypeHandle LoadGenericTypeUsingTypeRef () cil managed 
	{
		// Header size: 12
		// Code size: 11 (0xb)
		.maxstack 1
		.locals init (
			[0] valuetype [netstandard]System.RuntimeTypeHandle
		)

		IL_0000: nop
		IL_0001: ldtoken class [netstandard]System.Collections.Generic.Dictionary`2<int32, string>
		IL_0006: stloc.0
		IL_0007: br.s IL_0009

		IL_0009: ldloc.0
		IL_000a: ret
	} // end of method TypeRefTestCases::LoadGenericTypeUsingTypeRef

	.method public hidebysig 
		instance valuetype [netstandard]System.RuntimeTypeHandle LoadOpenGenericTypeByName () cil managed 
	{
		// Header size: 12
		// Code size: 11 (0xb)
		.maxstack 1
		.locals init (
			[0] valuetype [netstandard]System.RuntimeTypeHandle
		)

		IL_0000: nop
		IL_0001: ldtoken [netstandard]System.Action`1
		IL_0006: stloc.0
		IL_0007: br.s IL_0009

		IL_0009: ldloc.0
		IL_000a: ret
	} // end of method TypeRefTestCases::LoadOpenGenericTypeByName

	.method public hidebysig 
		instance valuetype [netstandard]System.RuntimeTypeHandle LoadPointerTypeUsingType () cil managed 
	{
		// Header size: 12
		// Code size: 11 (0xb)
		.maxstack 1
		.locals init (
			[0] valuetype [netstandard]System.RuntimeTypeHandle
		)

		IL_0000: nop
		IL_0001: ldtoken int32**
		IL_0006: stloc.0
		IL_0007: br.s IL_0009

		IL_0009: ldloc.0
		IL_000a: ret
	} // end of method TypeRefTestCases::LoadPointerTypeUsingType

	.method public hidebysig 
		instance valuetype [netstandard]System.RuntimeTypeHandle LoadPointerTypeUsingTypeRef () cil managed 
	{
		// Header size: 12
		// Code size: 11 (0xb)
		.maxstack 1
		.locals init (
			[0] valuetype [netstandard]System.RuntimeTypeHandle
		)

		IL_0000: nop
		IL_0001: ldtoken int32**
		IL_0006: stloc.0
		IL_0007: br.s IL_0009

		IL_0009: ldloc.0
		IL_000a: ret
	} // end of method TypeRefTestCases::LoadPointerTypeUsingTypeRef

	.method public hidebysig 
		instance valuetype [netstandard]System.RuntimeTypeHandle LoadReferenceTypeUsingType () cil managed 
	{
		// Header size: 12
		// Code size: 11 (0xb)
		.maxstack 1
		.locals init (
			[0] valuetype [netstandard]System.RuntimeTypeHandle
		)

		IL_0000: nop
		IL_0001: ldtoken int32&
		IL_0006: stloc.0
		IL_0007: br.s IL_0009

		IL_0009: ldloc.0
		IL_000a: ret
	} // end of method TypeRefTestCases::LoadReferenceTypeUsingType

	.method public hidebysig 
		instance valuetype [netstandard]System.RuntimeTypeHandle LoadReferenceTypeUsingTypeRef () cil managed 
	{
		// Header size: 12
		// Code size: 11 (0xb)
		.maxstack 1
		.locals init (
			[0] valuetype [netstandard]System.RuntimeTypeHandle
		)

		IL_0000: nop
		IL_0001: ldtoken int32&
		IL_0006: stloc.0
		IL_0007: br.s IL_0009

		IL_0009: ldloc.0
		IL_000a: ret
	} // end of method TypeRefTestCases::LoadReferenceTypeUsingTypeRef

	.method public hidebysig 
		instance valuetype [netstandard]System.RuntimeTypeHandle[] LoadTypeDifferentWays () cil managed 
	{
		// Header size: 12
		// Code size: 86 (0x56)
		.maxstack 3
		.locals init (
			[0] valuetype [netstandard]System.RuntimeTypeHandle[],
			[1] valuetype [netstandard]System.RuntimeTypeHandle[]
		)

		IL_0000: nop
		IL_0001: ldc.i4.6
		IL_0002: newarr [netstandard]System.RuntimeTypeHandle
		IL_0007: stloc.0
		IL_0008: ldloc.0
		IL_0009: ldc.i4.0
		IL_000a: ldtoken [netstandard]System.Int32
		IL_000f: stelem [netstandard]System.RuntimeTypeHandle
		IL_0014: ldloc.0
		IL_0015: ldc.i4.1
		IL_0016: ldtoken [netstandard]System.Int32
		IL_001b: stelem [netstandard]System.RuntimeTypeHandle
		IL_0020: ldloc.0
		IL_0021: ldc.i4.2
		IL_0022: ldtoken [netstandard]System.Int32
		IL_0027: stelem [netstandard]System.RuntimeTypeHandle
		IL_002c: ldloc.0
		IL_002d: ldc.i4.3
		IL_002e: ldtoken [netstandard]System.Int32
		IL_0033: stelem [netstandard]System.RuntimeTypeHandle
		IL_0038: ldloc.0
		IL_0039: ldc.i4.4
		IL_003a: ldtoken [netstandard]System.Int32
		IL_003f: stelem [netstandard]System.RuntimeTypeHandle
		IL_0044: ldloc.0
		IL_0045: ldc.i4.5
		IL_0046: ldtoken [netstandard]System.Int32
		IL_004b: stelem [netstandard]System.RuntimeTypeHandle
		IL_0050: ldloc.0
		IL_0051: stloc.1
		IL_0052: br.s IL_0054

		IL_0054: ldloc.1
		IL_0055: ret
	} // end of method TypeRefTestCases::LoadTypeDifferentWays

	.method public hidebysig 
		instance string ReturnCoreLibrary () cil managed 
	{
		// Header size: 12
		// Code size: 11 (0xb)
		.maxstack 1
		.locals init (
			[0] string
		)

		IL_0000: nop
		IL_0001: ldstr "netstandard"
		IL_0006: stloc.0
		IL_0007: br.s IL_0009

		IL_0009: ldloc.0
		IL_000a: ret
	} // end of method TypeRefTestCases::ReturnCoreLibrary

	.method public hidebysig 
		instance class [netstandard]System.Type ReturnNestedTypeUsingEcmaSyntax () cil managed 
	{
		// Header size: 12
		// Code size: 16 (0x10)
		.maxstack 1
		.locals init (
			[0] class [netstandard]System.Type
		)

		IL_0000: nop
		IL_0001: ldtoken InlineIL.Tests.AssemblyToProcess.TypeRefTestCases/NestedType
		IL_0006: call class [netstandard]System.Type [netstandard]System.Type::GetTypeFromHandle(valuetype [netstandard]System.RuntimeTypeHandle)
		IL_000b: stloc.0
		IL_000c: br.s IL_000e

		IL_000e: ldloc.0
		IL_000f: ret
	} // end of method TypeRefTestCases::ReturnNestedTypeUsingEcmaSyntax

	.method public hidebysig 
		instance class [netstandard]System.Type ReturnNestedTypeUsingRuntimeSyntax () cil managed 
	{
		// Header size: 12
		// Code size: 16 (0x10)
		.maxstack 1
		.locals init (
			[0] class [netstandard]System.Type
		)

		IL_0000: nop
		IL_0001: ldtoken InlineIL.Tests.AssemblyToProcess.TypeRefTestCases/NestedType
		IL_0006: call class [netstandard]System.Type [netstandard]System.Type::GetTypeFromHandle(valuetype [netstandard]System.RuntimeTypeHandle)
		IL_000b: stloc.0
		IL_000c: br.s IL_000e

		IL_000e: ldloc.0
		IL_000f: ret
	} // end of method TypeRefTestCases::ReturnNestedTypeUsingRuntimeSyntax

	.method public hidebysig 
		instance valuetype [netstandard]System.RuntimeTypeHandle ReturnTypeHandle () cil managed 
	{
		// Header size: 12
		// Code size: 11 (0xb)
		.maxstack 1
		.locals init (
			[0] valuetype [netstandard]System.RuntimeTypeHandle
		)

		IL_0000: nop
		IL_0001: ldtoken [netstandard]System.Guid
		IL_0006: stloc.0
		IL_0007: br.s IL_0009

		IL_0009: ldloc.0
		IL_000a: ret
	} // end of method TypeRefTestCases::ReturnTypeHandle

	.method public hidebysig 
		instance valuetype [netstandard]System.RuntimeTypeHandle ReturnTypeHandle<T> () cil managed 
	{
		// Header size: 12
		// Code size: 11 (0xb)
		.maxstack 1
		.locals init (
			[0] valuetype [netstandard]System.RuntimeTypeHandle
		)

		IL_0000: nop
		IL_0001: ldtoken !!T
		IL_0006: stloc.0
		IL_0007: br.s IL_0009

		IL_0009: ldloc.0
		IL_000a: ret
	} // end of method TypeRefTestCases::ReturnTypeHandle

	.method public hidebysig 
		instance valuetype [netstandard]System.RuntimeTypeHandle ReturnTypeHandleGeneric () cil managed 
	{
		// Header size: 12
		// Code size: 11 (0xb)
		.maxstack 1
		.locals init (
			[0] valuetype [netstandard]System.RuntimeTypeHandle
		)

		IL_0000: nop
		IL_0001: ldtoken [netstandard]System.Guid
		IL_0006: stloc.0
		IL_0007: br.s IL_0009

		IL_0009: ldloc.0
		IL_000a: ret
	} // end of method TypeRefTestCases::ReturnTypeHandleGeneric

	.method public hidebysig 
		instance valuetype [netstandard]System.RuntimeTypeHandle ReturnTypeHandleGeneric<T> () cil managed 
	{
		// Header size: 12
		// Code size: 11 (0xb)
		.maxstack 1
		.locals init (
			[0] valuetype [netstandard]System.RuntimeTypeHandle
		)

		IL_0000: nop
		IL_0001: ldtoken !!T
		IL_0006: stloc.0
		IL_0007: br.s IL_0009

		IL_0009: ldloc.0
		IL_000a: ret
	} // end of method TypeRefTestCases::ReturnTypeHandleGeneric

} // end of class InlineIL.Tests.AssemblyToProcess.TypeRefTestCases
