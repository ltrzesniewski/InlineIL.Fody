.class public auto ansi beforefieldinit InlineIL.Tests.AssemblyToProcess.FieldRefTestCases
	extends [mscorlib]System.Object
{
	.custom instance void [InlineIL.Tests.Common]InlineIL.Tests.Common.TestCasesAttribute::.ctor() = (
		01 00 00 00
	)
	// Nested Types
	.class nested private auto ansi beforefieldinit GenericType`1<T>
		extends [mscorlib]System.Object
	{
		// Fields
		.field private static initonly int32 _value
		.field private static initonly !T _value2

		// Methods
		.method private hidebysig specialname rtspecialname static 
			void .cctor () cil managed 
		{
			// Header size: 12
			// Code size: 63 (0x3f)
			.maxstack 2
			.locals init (
				[0] !T
			)

			IL_0000: ldc.i4.s 10
			IL_0002: stsfld int32 class InlineIL.Tests.AssemblyToProcess.FieldRefTestCases/GenericType`1<!T>::_value
			IL_0007: ldtoken !T
			IL_000c: call class [mscorlib]System.Type [mscorlib]System.Type::GetTypeFromHandle(valuetype [mscorlib]System.RuntimeTypeHandle)
			IL_0011: ldtoken [mscorlib]System.Int32
			IL_0016: call class [mscorlib]System.Type [mscorlib]System.Type::GetTypeFromHandle(valuetype [mscorlib]System.RuntimeTypeHandle)
			IL_001b: call bool [mscorlib]System.Type::op_Equality(class [mscorlib]System.Type, class [mscorlib]System.Type)
			IL_0020: brtrue.s IL_002d

			IL_0022: ldloca.s 0
			IL_0024: initobj !T
			IL_002a: ldloc.0
			IL_002b: br.s IL_0039

			IL_002d: ldc.i4.s 10
			IL_002f: box [mscorlib]System.Int32
			IL_0034: unbox.any !T

			IL_0039: stsfld !0 class InlineIL.Tests.AssemblyToProcess.FieldRefTestCases/GenericType`1<!T>::_value2
			IL_003e: ret
		} // end of method GenericType`1::.cctor

		.method public hidebysig specialname rtspecialname 
			instance void .ctor () cil managed 
		{
			// Header size: 1
			// Code size: 7 (0x7)
			.maxstack 8

			IL_0000: ldarg.0
			IL_0001: call instance void [mscorlib]System.Object::.ctor()
			IL_0006: ret
		} // end of method GenericType`1::.ctor

		.method public hidebysig static 
			int32 GetValue () cil managed 
		{
			// Header size: 1
			// Code size: 6 (0x6)
			.maxstack 8

			IL_0000: ldsfld int32 class InlineIL.Tests.AssemblyToProcess.FieldRefTestCases/GenericType`1<!T>::_value
			IL_0005: ret
		} // end of method GenericType`1::GetValue

		.method public hidebysig static 
			!T GetValue2 () cil managed 
		{
			// Header size: 1
			// Code size: 6 (0x6)
			.maxstack 8

			IL_0000: ldsfld !0 class InlineIL.Tests.AssemblyToProcess.FieldRefTestCases/GenericType`1<!T>::_value2
			IL_0005: ret
		} // end of method GenericType`1::GetValue2

	} // end of class GenericType`1


	// Fields
	.field public int32 IntField
	.field public static int32 StaticIntField

	// Methods
	.method private hidebysig specialname rtspecialname static 
		void .cctor () cil managed 
	{
		// Header size: 1
		// Code size: 8 (0x8)
		.maxstack 8

		IL_0000: ldc.i4.s 42
		IL_0002: stsfld int32 InlineIL.Tests.AssemblyToProcess.FieldRefTestCases::StaticIntField
		IL_0007: ret
	} // end of method FieldRefTestCases::.cctor

	.method public hidebysig specialname rtspecialname 
		instance void .ctor () cil managed 
	{
		// Header size: 1
		// Code size: 7 (0x7)
		.maxstack 8

		IL_0000: ldarg.0
		IL_0001: call instance void [mscorlib]System.Object::.ctor()
		IL_0006: ret
	} // end of method FieldRefTestCases::.ctor

	.method public hidebysig 
		instance int32 GetValueFromGenericType () cil managed 
	{
		// Header size: 1
		// Code size: 6 (0x6)
		.maxstack 8

		IL_0000: call int32 class InlineIL.Tests.AssemblyToProcess.FieldRefTestCases/GenericType`1<valuetype [mscorlib]System.Guid>::GetValue()
		IL_0005: ret
	} // end of method FieldRefTestCases::GetValueFromGenericType

	.method public hidebysig 
		instance int32 GetValueFromGenericType2 () cil managed 
	{
		// Header size: 1
		// Code size: 6 (0x6)
		.maxstack 8

		IL_0000: call !0 class InlineIL.Tests.AssemblyToProcess.FieldRefTestCases/GenericType`1<int32>::GetValue2()
		IL_0005: ret
	} // end of method FieldRefTestCases::GetValueFromGenericType2

	.method public hidebysig 
		instance int32 GetValueFromImportedGenericType () cil managed 
	{
		// Header size: 1
		// Code size: 15 (0xf)
		.maxstack 8

		IL_0000: ldc.i4.s 42
		IL_0002: ldc.i4.s 10
		IL_0004: newobj instance void valuetype [mscorlib]System.ValueTuple`2<int32, int32>::.ctor(!0, !1)
		IL_0009: ldfld !1 valuetype [mscorlib]System.ValueTuple`2<int32, int32>::Item2
		IL_000e: ret
	} // end of method FieldRefTestCases::GetValueFromImportedGenericType

	.method public hidebysig 
		instance !!T GetValueFromImportedGenericType2<T> (
			!!T 'value'
		) cil managed 
	{
		// Header size: 1
		// Code size: 14 (0xe)
		.maxstack 8

		IL_0000: ldc.i4.s 42
		IL_0002: ldarg.1
		IL_0003: newobj instance void valuetype [mscorlib]System.ValueTuple`2<int32, !!T>::.ctor(!0, !1)
		IL_0008: ldfld !1 valuetype [mscorlib]System.ValueTuple`2<int32, !!T>::Item2
		IL_000d: ret
	} // end of method FieldRefTestCases::GetValueFromImportedGenericType2

	.method public hidebysig 
		instance valuetype [mscorlib]System.RuntimeFieldHandle ReturnFieldHandle () cil managed 
	{
		// Header size: 1
		// Code size: 6 (0x6)
		.maxstack 8

		IL_0000: ldtoken field int32 InlineIL.Tests.AssemblyToProcess.FieldRefTestCases::IntField
		IL_0005: ret
	} // end of method FieldRefTestCases::ReturnFieldHandle

	.method public hidebysig 
		instance int32 ReturnIntField () cil managed 
	{
		// Header size: 1
		// Code size: 7 (0x7)
		.maxstack 8

		IL_0000: ldarg.0
		IL_0001: ldfld int32 InlineIL.Tests.AssemblyToProcess.FieldRefTestCases::IntField
		IL_0006: ret
	} // end of method FieldRefTestCases::ReturnIntField

	.method public hidebysig 
		instance int32[] ReturnStaticIntFieldInDifferentWays () cil managed 
	{
		// Header size: 12
		// Code size: 33 (0x21)
		.maxstack 3
		.locals init (
			[0] int32[]
		)

		IL_0000: ldc.i4.3
		IL_0001: newarr [mscorlib]System.Int32
		IL_0006: stloc.0
		IL_0007: ldloc.0
		IL_0008: ldc.i4.0
		IL_0009: ldsfld int32 InlineIL.Tests.AssemblyToProcess.FieldRefTestCases::StaticIntField
		IL_000e: stelem.i4
		IL_000f: ldloc.0
		IL_0010: ldc.i4.1
		IL_0011: ldsfld int32 InlineIL.Tests.AssemblyToProcess.FieldRefTestCases::StaticIntField
		IL_0016: stelem.i4
		IL_0017: ldloc.0
		IL_0018: ldc.i4.2
		IL_0019: ldsfld int32 InlineIL.Tests.AssemblyToProcess.FieldRefTestCases::StaticIntField
		IL_001e: stelem.i4
		IL_001f: ldloc.0
		IL_0020: ret
	} // end of method FieldRefTestCases::ReturnStaticIntFieldInDifferentWays

} // end of class InlineIL.Tests.AssemblyToProcess.FieldRefTestCases
