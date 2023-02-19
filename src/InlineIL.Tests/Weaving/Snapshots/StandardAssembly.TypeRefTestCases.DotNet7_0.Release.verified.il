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
			// Code size: 7 (0x7)
			.maxstack 8

			IL_0000: ldarg.0
			IL_0001: call instance void [netstandard]System.Object::.ctor()
			IL_0006: ret
		} // end of method NestedType::.ctor

	} // end of class NestedType


	// Fields
	.field private static literal string _thisAssemblyName = "InlineIL.Tests.StandardAssemblyToProcess"

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
	} // end of method TypeRefTestCases::.ctor

	.method public hidebysig 
		instance bool GenericIsinst<T> (
			object obj
		) cil managed 
	{
		// Header size: 1
		// Code size: 10 (0xa)
		.maxstack 8

		IL_0000: ldarg.1
		IL_0001: isinst !!T
		IL_0006: ldnull
		IL_0007: cgt.un
		IL_0009: ret
	} // end of method TypeRefTestCases::GenericIsinst

	.method public hidebysig 
		instance bool IsString (
			object obj
		) cil managed 
	{
		// Header size: 1
		// Code size: 10 (0xa)
		.maxstack 8

		IL_0000: ldarg.1
		IL_0001: isinst [netstandard]System.String
		IL_0006: ldnull
		IL_0007: cgt.un
		IL_0009: ret
	} // end of method TypeRefTestCases::IsString

	.method public hidebysig 
		instance valuetype [netstandard]System.RuntimeTypeHandle LoadArrayTypeUsingType () cil managed 
	{
		// Header size: 1
		// Code size: 6 (0x6)
		.maxstack 8

		IL_0000: ldtoken int32[][]
		IL_0005: ret
	} // end of method TypeRefTestCases::LoadArrayTypeUsingType

	.method public hidebysig 
		instance valuetype [netstandard]System.RuntimeTypeHandle LoadArrayTypeUsingTypeRef () cil managed 
	{
		// Header size: 1
		// Code size: 6 (0x6)
		.maxstack 8

		IL_0000: ldtoken int32[][]
		IL_0005: ret
	} // end of method TypeRefTestCases::LoadArrayTypeUsingTypeRef

	.method public hidebysig 
		instance valuetype [netstandard]System.RuntimeTypeHandle LoadArrayTypeWithRankUsingType () cil managed 
	{
		// Header size: 1
		// Code size: 6 (0x6)
		.maxstack 8

		IL_0000: ldtoken int32[, , ][]
		IL_0005: ret
	} // end of method TypeRefTestCases::LoadArrayTypeWithRankUsingType

	.method public hidebysig 
		instance valuetype [netstandard]System.RuntimeTypeHandle LoadArrayTypeWithRankUsingTypeRef () cil managed 
	{
		// Header size: 1
		// Code size: 6 (0x6)
		.maxstack 8

		IL_0000: ldtoken int32[, , ][]
		IL_0005: ret
	} // end of method TypeRefTestCases::LoadArrayTypeWithRankUsingTypeRef

	.method public hidebysig 
		instance valuetype [netstandard]System.RuntimeTypeHandle LoadGenericTypeByName () cil managed 
	{
		// Header size: 1
		// Code size: 6 (0x6)
		.maxstack 8

		IL_0000: ldtoken class [netstandard]System.Action`1<int32>
		IL_0005: ret
	} // end of method TypeRefTestCases::LoadGenericTypeByName

	.method public hidebysig 
		instance valuetype [netstandard]System.RuntimeTypeHandle LoadGenericTypeUsingType () cil managed 
	{
		// Header size: 1
		// Code size: 6 (0x6)
		.maxstack 8

		IL_0000: ldtoken class [netstandard]System.Collections.Generic.Dictionary`2<int32, string>
		IL_0005: ret
	} // end of method TypeRefTestCases::LoadGenericTypeUsingType

	.method public hidebysig 
		instance valuetype [netstandard]System.RuntimeTypeHandle LoadGenericTypeUsingTypeRef () cil managed 
	{
		// Header size: 12
		// Code size: 6 (0x6)
		.maxstack 1

		IL_0000: ldtoken class [netstandard]System.Collections.Generic.Dictionary`2<int32, string>
		IL_0005: ret
	} // end of method TypeRefTestCases::LoadGenericTypeUsingTypeRef

	.method public hidebysig 
		instance valuetype [netstandard]System.RuntimeTypeHandle LoadOpenGenericTypeByName () cil managed 
	{
		// Header size: 1
		// Code size: 6 (0x6)
		.maxstack 8

		IL_0000: ldtoken [netstandard]System.Action`1
		IL_0005: ret
	} // end of method TypeRefTestCases::LoadOpenGenericTypeByName

	.method public hidebysig 
		instance valuetype [netstandard]System.RuntimeTypeHandle LoadPointerTypeUsingType () cil managed 
	{
		// Header size: 1
		// Code size: 6 (0x6)
		.maxstack 8

		IL_0000: ldtoken int32**
		IL_0005: ret
	} // end of method TypeRefTestCases::LoadPointerTypeUsingType

	.method public hidebysig 
		instance valuetype [netstandard]System.RuntimeTypeHandle LoadPointerTypeUsingTypeRef () cil managed 
	{
		// Header size: 1
		// Code size: 6 (0x6)
		.maxstack 8

		IL_0000: ldtoken int32**
		IL_0005: ret
	} // end of method TypeRefTestCases::LoadPointerTypeUsingTypeRef

	.method public hidebysig 
		instance valuetype [netstandard]System.RuntimeTypeHandle LoadReferenceTypeUsingType () cil managed 
	{
		// Header size: 1
		// Code size: 6 (0x6)
		.maxstack 8

		IL_0000: ldtoken int32&
		IL_0005: ret
	} // end of method TypeRefTestCases::LoadReferenceTypeUsingType

	.method public hidebysig 
		instance valuetype [netstandard]System.RuntimeTypeHandle LoadReferenceTypeUsingTypeRef () cil managed 
	{
		// Header size: 1
		// Code size: 6 (0x6)
		.maxstack 8

		IL_0000: ldtoken int32&
		IL_0005: ret
	} // end of method TypeRefTestCases::LoadReferenceTypeUsingTypeRef

	.method public hidebysig 
		instance valuetype [netstandard]System.RuntimeTypeHandle[] LoadTypeDifferentWays () cil managed 
	{
		// Header size: 12
		// Code size: 81 (0x51)
		.maxstack 3
		.locals init (
			[0] valuetype [netstandard]System.RuntimeTypeHandle[]
		)

		IL_0000: ldc.i4.6
		IL_0001: newarr [netstandard]System.RuntimeTypeHandle
		IL_0006: stloc.0
		IL_0007: ldloc.0
		IL_0008: ldc.i4.0
		IL_0009: ldtoken [netstandard]System.Int32
		IL_000e: stelem [netstandard]System.RuntimeTypeHandle
		IL_0013: ldloc.0
		IL_0014: ldc.i4.1
		IL_0015: ldtoken [netstandard]System.Int32
		IL_001a: stelem [netstandard]System.RuntimeTypeHandle
		IL_001f: ldloc.0
		IL_0020: ldc.i4.2
		IL_0021: ldtoken [netstandard]System.Int32
		IL_0026: stelem [netstandard]System.RuntimeTypeHandle
		IL_002b: ldloc.0
		IL_002c: ldc.i4.3
		IL_002d: ldtoken [netstandard]System.Int32
		IL_0032: stelem [netstandard]System.RuntimeTypeHandle
		IL_0037: ldloc.0
		IL_0038: ldc.i4.4
		IL_0039: ldtoken [netstandard]System.Int32
		IL_003e: stelem [netstandard]System.RuntimeTypeHandle
		IL_0043: ldloc.0
		IL_0044: ldc.i4.5
		IL_0045: ldtoken [netstandard]System.Int32
		IL_004a: stelem [netstandard]System.RuntimeTypeHandle
		IL_004f: ldloc.0
		IL_0050: ret
	} // end of method TypeRefTestCases::LoadTypeDifferentWays

	.method public hidebysig 
		instance string ReturnCoreLibrary () cil managed 
	{
		// Header size: 1
		// Code size: 6 (0x6)
		.maxstack 8

		IL_0000: ldstr "netstandard"
		IL_0005: ret
	} // end of method TypeRefTestCases::ReturnCoreLibrary

	.method public hidebysig 
		instance class [netstandard]System.Type ReturnNestedTypeUsingEcmaSyntax () cil managed 
	{
		// Header size: 1
		// Code size: 11 (0xb)
		.maxstack 8

		IL_0000: ldtoken InlineIL.Tests.AssemblyToProcess.TypeRefTestCases/NestedType
		IL_0005: call class [netstandard]System.Type [netstandard]System.Type::GetTypeFromHandle(valuetype [netstandard]System.RuntimeTypeHandle)
		IL_000a: ret
	} // end of method TypeRefTestCases::ReturnNestedTypeUsingEcmaSyntax

	.method public hidebysig 
		instance class [netstandard]System.Type ReturnNestedTypeUsingRuntimeSyntax () cil managed 
	{
		// Header size: 1
		// Code size: 11 (0xb)
		.maxstack 8

		IL_0000: ldtoken InlineIL.Tests.AssemblyToProcess.TypeRefTestCases/NestedType
		IL_0005: call class [netstandard]System.Type [netstandard]System.Type::GetTypeFromHandle(valuetype [netstandard]System.RuntimeTypeHandle)
		IL_000a: ret
	} // end of method TypeRefTestCases::ReturnNestedTypeUsingRuntimeSyntax

	.method public hidebysig 
		instance valuetype [netstandard]System.RuntimeTypeHandle ReturnTypeHandle () cil managed 
	{
		// Header size: 1
		// Code size: 6 (0x6)
		.maxstack 8

		IL_0000: ldtoken [netstandard]System.Guid
		IL_0005: ret
	} // end of method TypeRefTestCases::ReturnTypeHandle

	.method public hidebysig 
		instance valuetype [netstandard]System.RuntimeTypeHandle ReturnTypeHandle<T> () cil managed 
	{
		// Header size: 1
		// Code size: 6 (0x6)
		.maxstack 8

		IL_0000: ldtoken !!T
		IL_0005: ret
	} // end of method TypeRefTestCases::ReturnTypeHandle

	.method public hidebysig 
		instance valuetype [netstandard]System.RuntimeTypeHandle ReturnTypeHandleGeneric () cil managed 
	{
		// Header size: 1
		// Code size: 6 (0x6)
		.maxstack 8

		IL_0000: ldtoken [netstandard]System.Guid
		IL_0005: ret
	} // end of method TypeRefTestCases::ReturnTypeHandleGeneric

	.method public hidebysig 
		instance valuetype [netstandard]System.RuntimeTypeHandle ReturnTypeHandleGeneric<T> () cil managed 
	{
		// Header size: 1
		// Code size: 6 (0x6)
		.maxstack 8

		IL_0000: ldtoken !!T
		IL_0005: ret
	} // end of method TypeRefTestCases::ReturnTypeHandleGeneric

} // end of class InlineIL.Tests.AssemblyToProcess.TypeRefTestCases
