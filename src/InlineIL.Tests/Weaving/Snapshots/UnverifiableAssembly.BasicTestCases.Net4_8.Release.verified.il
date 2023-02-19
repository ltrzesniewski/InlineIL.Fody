.class public auto ansi beforefieldinit InlineIL.Tests.UnverifiableAssemblyToProcess.BasicTestCases
	extends [mscorlib]System.Object
	implements [InlineIL.Tests.Common]InlineIL.Tests.Common.IUnverifiableBasicTestCases
{
	.custom instance void [InlineIL.Tests.Common]InlineIL.Tests.Common.TestCasesAttribute::.ctor() = (
		01 00 00 00
	)
	// Fields
	.field public static initonly int32[] IntArrayField
	.field public static int32* StaticIntPtrField
	.field public static void* StaticVoidPtrField

	// Methods
	.method private hidebysig specialname rtspecialname static 
		void .cctor () cil managed 
	{
		// Header size: 1
		// Code size: 17 (0x11)
		.maxstack 8

		IL_0000: ldc.i4.1
		IL_0001: newarr [mscorlib]System.Int32
		IL_0006: dup
		IL_0007: ldc.i4.0
		IL_0008: ldc.i4.s 42
		IL_000a: stelem.i4
		IL_000b: stsfld int32[] InlineIL.Tests.UnverifiableAssemblyToProcess.BasicTestCases::IntArrayField
		IL_0010: ret
	} // end of method BasicTestCases::.cctor

	.method public hidebysig specialname rtspecialname 
		instance void .ctor () cil managed 
	{
		// Header size: 1
		// Code size: 7 (0x7)
		.maxstack 8

		IL_0000: ldarg.0
		IL_0001: call instance void [mscorlib]System.Object::.ctor()
		IL_0006: ret
	} // end of method BasicTestCases::.ctor

	.method public hidebysig 
		instance void HandlePrefixesInDebugMode (
			valuetype [mscorlib]System.Guid& 'value'
		) cil managed 
	{
		// Header size: 1
		// Code size: 15 (0xf)
		.maxstack 8

		IL_0000: ldarg.1
		IL_0001: ldc.i4.0
		IL_0002: conv.u1
		IL_0003: sizeof [mscorlib]System.Guid
		IL_0009: unaligned. 1
		IL_000c: initblk
		IL_000e: ret
	} // end of method BasicTestCases::HandlePrefixesInDebugMode

	.method public hidebysig 
		instance void NoOpCodeByte () cil managed 
	{
		// Header size: 1
		// Code size: 16 (0x10)
		.maxstack 8

		IL_0000: ldsfld int32[] InlineIL.Tests.UnverifiableAssemblyToProcess.BasicTestCases::IntArrayField
		IL_0005: ldc.i4.0
		IL_0006: .emitbyte 0xfe
		IL_0007: .emitbyte 0x19
		IL_0008: ldarg.3
		IL_0009: ldelema [mscorlib]System.Int32
		IL_000e: pop
		IL_000f: ret
	} // end of method BasicTestCases::NoOpCodeByte

	.method public hidebysig 
		instance void NoOpCodeEnum () cil managed 
	{
		// Header size: 1
		// Code size: 16 (0x10)
		.maxstack 8

		IL_0000: ldsfld int32[] InlineIL.Tests.UnverifiableAssemblyToProcess.BasicTestCases::IntArrayField
		IL_0005: ldc.i4.0
		IL_0006: .emitbyte 0xfe
		IL_0007: .emitbyte 0x19
		IL_0008: ldloc.1
		IL_0009: ldelema [mscorlib]System.Int32
		IL_000e: pop
		IL_000f: ret
	} // end of method BasicTestCases::NoOpCodeEnum

	.method public final hidebysig newslot virtual 
		instance int32 PopPointerArg (
			int32* arg,
			int32 offset
		) cil managed 
	{
		// Header size: 1
		// Code size: 13 (0xd)
		.maxstack 8

		IL_0000: ldarg.1
		IL_0001: ldarg.2
		IL_0002: conv.i
		IL_0003: ldc.i4.4
		IL_0004: mul
		IL_0005: add
		IL_0006: starg arg
		IL_000a: ldarg.1
		IL_000b: ldind.i4
		IL_000c: ret
	} // end of method BasicTestCases::PopPointerArg

	.method public final hidebysig newslot virtual 
		instance int32 PopPointerLocal (
			int32* arg,
			int32 offset
		) cil managed 
	{
		// Header size: 12
		// Code size: 10 (0xa)
		.maxstack 3
		.locals init (
			[0] int32*
		)

		IL_0000: ldarg.1
		IL_0001: ldarg.2
		IL_0002: conv.i
		IL_0003: ldc.i4.4
		IL_0004: mul
		IL_0005: add
		IL_0006: stloc.0
		IL_0007: ldloc.0
		IL_0008: ldind.i4
		IL_0009: ret
	} // end of method BasicTestCases::PopPointerLocal

	.method public final hidebysig newslot virtual 
		instance int32 PopPointerStatic (
			int32* arg,
			int32 offset
		) cil managed 
	{
		// Header size: 1
		// Code size: 18 (0x12)
		.maxstack 8

		IL_0000: ldarg.1
		IL_0001: ldarg.2
		IL_0002: conv.i
		IL_0003: ldc.i4.4
		IL_0004: mul
		IL_0005: add
		IL_0006: stsfld int32* InlineIL.Tests.UnverifiableAssemblyToProcess.BasicTestCases::StaticIntPtrField
		IL_000b: ldsfld int32* InlineIL.Tests.UnverifiableAssemblyToProcess.BasicTestCases::StaticIntPtrField
		IL_0010: ldind.i4
		IL_0011: ret
	} // end of method BasicTestCases::PopPointerStatic

	.method public final hidebysig newslot virtual 
		instance int32 PopVoidPointerArg (
			void* arg,
			int32 offset
		) cil managed 
	{
		// Header size: 1
		// Code size: 13 (0xd)
		.maxstack 8

		IL_0000: ldarg.1
		IL_0001: ldarg.2
		IL_0002: conv.i
		IL_0003: ldc.i4.4
		IL_0004: mul
		IL_0005: add
		IL_0006: starg arg
		IL_000a: ldarg.1
		IL_000b: ldind.i4
		IL_000c: ret
	} // end of method BasicTestCases::PopVoidPointerArg

	.method public final hidebysig newslot virtual 
		instance int32 PopVoidPointerLocal (
			int32* arg,
			int32 offset
		) cil managed 
	{
		// Header size: 12
		// Code size: 10 (0xa)
		.maxstack 3
		.locals init (
			[0] void*
		)

		IL_0000: ldarg.1
		IL_0001: ldarg.2
		IL_0002: conv.i
		IL_0003: ldc.i4.4
		IL_0004: mul
		IL_0005: add
		IL_0006: stloc.0
		IL_0007: ldloc.0
		IL_0008: ldind.i4
		IL_0009: ret
	} // end of method BasicTestCases::PopVoidPointerLocal

	.method public final hidebysig newslot virtual 
		instance int32 PopVoidPointerStatic (
			int32* arg,
			int32 offset
		) cil managed 
	{
		// Header size: 1
		// Code size: 18 (0x12)
		.maxstack 8

		IL_0000: ldarg.1
		IL_0001: ldarg.2
		IL_0002: conv.i
		IL_0003: ldc.i4.4
		IL_0004: mul
		IL_0005: add
		IL_0006: stsfld void* InlineIL.Tests.UnverifiableAssemblyToProcess.BasicTestCases::StaticVoidPtrField
		IL_000b: ldsfld void* InlineIL.Tests.UnverifiableAssemblyToProcess.BasicTestCases::StaticVoidPtrField
		IL_0010: ldind.i4
		IL_0011: ret
	} // end of method BasicTestCases::PopVoidPointerStatic

	.method public final hidebysig newslot virtual 
		instance void PushPointer (
			int32* 'value'
		) cil managed 
	{
		// Header size: 1
		// Code size: 5 (0x5)
		.maxstack 8

		IL_0000: ldarg.1
		IL_0001: ldc.i4.s 42
		IL_0003: stind.i4
		IL_0004: ret
	} // end of method BasicTestCases::PushPointer

	.method public final hidebysig newslot virtual 
		instance int32* ReturnPointer (
			int32[] values,
			int32 offset
		) cil managed 
	{
		// Header size: 1
		// Code size: 8 (0x8)
		.maxstack 8

		IL_0000: ldarg.1
		IL_0001: ldarg.2
		IL_0002: ldelema [mscorlib]System.Int32
		IL_0007: ret
	} // end of method BasicTestCases::ReturnPointer

	.method public final hidebysig newslot virtual 
		instance void* ReturnPointerWithConversion (
			int32[] values,
			int32 offset
		) cil managed 
	{
		// Header size: 1
		// Code size: 8 (0x8)
		.maxstack 8

		IL_0000: ldarg.1
		IL_0001: ldarg.2
		IL_0002: ldelema [mscorlib]System.Int32
		IL_0007: ret
	} // end of method BasicTestCases::ReturnPointerWithConversion

	.method public hidebysig 
		instance int32 ReturnPointerWithDereference (
			int32[] values,
			int32 offset
		) cil managed 
	{
		// Header size: 1
		// Code size: 9 (0x9)
		.maxstack 8

		IL_0000: ldarg.1
		IL_0001: ldarg.2
		IL_0002: ldelema [mscorlib]System.Int32
		IL_0007: ldind.i4
		IL_0008: ret
	} // end of method BasicTestCases::ReturnPointerWithDereference

	.method public final hidebysig newslot virtual 
		instance int32& ReturnRef (
			int32[] values,
			int32 offset
		) cil managed 
	{
		// Header size: 1
		// Code size: 8 (0x8)
		.maxstack 8

		IL_0000: ldarg.1
		IL_0001: ldarg.2
		IL_0002: ldelema [mscorlib]System.Int32
		IL_0007: ret
	} // end of method BasicTestCases::ReturnRef

	.method public hidebysig 
		instance int32 ReturnRefWithDereference (
			int32[] values,
			int32 offset
		) cil managed 
	{
		// Header size: 1
		// Code size: 9 (0x9)
		.maxstack 8

		IL_0000: ldarg.1
		IL_0001: ldarg.2
		IL_0002: ldelema [mscorlib]System.Int32
		IL_0007: ldind.i4
		IL_0008: ret
	} // end of method BasicTestCases::ReturnRefWithDereference

	.method public hidebysig 
		instance float64 ReturnRefWithDereferenceAndConversion (
			int32[] values,
			int32 offset
		) cil managed 
	{
		// Header size: 1
		// Code size: 10 (0xa)
		.maxstack 8

		IL_0000: ldarg.1
		IL_0001: ldarg.2
		IL_0002: ldelema [mscorlib]System.Int32
		IL_0007: ldind.i4
		IL_0008: conv.r8
		IL_0009: ret
	} // end of method BasicTestCases::ReturnRefWithDereferenceAndConversion

	.method public final hidebysig newslot virtual 
		instance void* ReturnVoidPointer (
			int32[] values,
			int32 offset
		) cil managed 
	{
		// Header size: 1
		// Code size: 8 (0x8)
		.maxstack 8

		IL_0000: ldarg.1
		IL_0001: ldarg.2
		IL_0002: ldelema [mscorlib]System.Int32
		IL_0007: ret
	} // end of method BasicTestCases::ReturnVoidPointer

} // end of class InlineIL.Tests.UnverifiableAssemblyToProcess.BasicTestCases
