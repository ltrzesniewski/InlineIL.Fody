.class public auto ansi beforefieldinit InlineIL.Tests.AssemblyToProcess.BasicTestCases
	extends [System.Runtime]System.Object
	implements [InlineIL.Tests.Common]InlineIL.Tests.Common.IBasicTestCases
{
	.custom instance void [InlineIL.Tests.Common]InlineIL.Tests.Common.TestCasesAttribute::.ctor() = (
		01 00 00 00
	)
	// Nested Types
	.class nested private auto ansi abstract sealed beforefieldinit NestedClassA
		extends [System.Runtime]System.Object
	{
		// Nested Types
		.class nested public auto ansi abstract sealed beforefieldinit NestedClassB
			extends [System.Runtime]System.Object
		{
			// Methods
			.method public hidebysig static 
				int32 Call () cil managed 
			{
				// Header size: 12
				// Code size: 9 (0x9)
				.maxstack 2
				.locals init (
					[0] int32
				)

				IL_0000: nop
				IL_0001: ldc.i4.1
				IL_0002: ldc.i4.2
				IL_0003: add
				IL_0004: stloc.0
				IL_0005: br.s IL_0007

				IL_0007: ldloc.0
				IL_0008: ret
			} // end of method NestedClassB::Call

		} // end of class NestedClassB


	} // end of class NestedClassA


	// Fields
	.field public static int32 StaticIntField

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
	} // end of method BasicTestCases::.ctor

	.method public hidebysig 
		instance void AddAssign (
			int32& a,
			int32 b
		) cil managed 
	{
		// Header size: 1
		// Code size: 8 (0x8)
		.maxstack 8

		IL_0000: nop
		IL_0001: ldarg.1
		IL_0002: ldarg.1
		IL_0003: ldind.i4
		IL_0004: ldarg.2
		IL_0005: add
		IL_0006: stind.i4
		IL_0007: ret
	} // end of method BasicTestCases::AddAssign

	.method public hidebysig 
		instance void EnsureLocal () cil managed 
	{
		// Header size: 12
		// Code size: 8 (0x8)
		.maxstack 1
		.locals init (
			[0] valuetype [System.Runtime]System.Guid
		)

		IL_0000: nop
		IL_0001: call valuetype [System.Runtime]System.Guid [System.Runtime]System.Guid::NewGuid()
		IL_0006: stloc.0
		IL_0007: ret
	} // end of method BasicTestCases::EnsureLocal

	.method public hidebysig 
		instance void ExplicitEndFinally () cil managed 
	{
		// Header size: 12
		// Code size: 9 (0x9)
		.maxstack 0

		IL_0000: nop
		.try
		{
			IL_0001: nop
			IL_0002: nop
			IL_0003: leave.s IL_0008
		} // end .try
		finally
		{
			IL_0005: nop
			IL_0006: nop
			IL_0007: endfinally
		} // end handler

		IL_0008: ret
	} // end of method BasicTestCases::ExplicitEndFinally

	.method public hidebysig 
		instance void ExplicitLeave () cil managed 
	{
		// Header size: 12
		// Code size: 9 (0x9)
		.maxstack 1

		IL_0000: nop
		.try
		{
			IL_0001: nop
			IL_0002: leave.s IL_0008
		} // end .try
		catch [System.Runtime]System.Object
		{
			IL_0004: pop
			IL_0005: nop
			IL_0006: leave.s IL_0008
		} // end handler

		IL_0008: ret
	} // end of method BasicTestCases::ExplicitLeave

	.method public hidebysig 
		instance void ExplicitRet () cil managed 
	{
		// Header size: 1
		// Code size: 2 (0x2)
		.maxstack 8

		IL_0000: nop
		IL_0001: ret
	} // end of method BasicTestCases::ExplicitRet

	.method public hidebysig 
		instance int32 HandleExceptionBlocks () cil managed 
	{
		// Header size: 12
		// Code size: 86 (0x56)
		.maxstack 2
		.locals init (
			[0] class [System.Runtime]System.InvalidOperationException,
			[1] bool,
			[2] int32
		)

		IL_0000: nop
		IL_0001: ldc.i4.1
		IL_0002: ldc.i4.2
		IL_0003: add
		IL_0004: pop
		.try
		{
			.try
			{
				IL_0005: nop
				IL_0006: ldc.i4.3
				IL_0007: ldc.i4.4
				IL_0008: add
				IL_0009: ldstr "foo"
				IL_000e: newobj instance void [System.Runtime]System.InvalidOperationException::.ctor(string)
				IL_0013: throw
			} // end .try
			filter
			{
				IL_0014: isinst [System.Runtime]System.InvalidOperationException
				IL_0019: dup
				IL_001a: brtrue.s IL_0020

				IL_001c: pop
				IL_001d: ldc.i4.0
				IL_001e: br.s IL_0036

				IL_0020: stloc.0
				IL_0021: ldloc.0
				IL_0022: callvirt instance string [System.Runtime]System.Exception::get_Message()
				IL_0027: ldstr "foo"
				IL_002c: callvirt instance bool [System.Runtime]System.String::Contains(string)
				IL_0031: stloc.1
				IL_0032: ldloc.1
				IL_0033: ldc.i4.0
				IL_0034: cgt.un

				IL_0036: endfilter
			} // end filter
			catch
			{
				IL_0038: pop
				IL_0039: nop
				IL_003a: ldloc.0
				IL_003b: pop
				IL_003c: ldc.i4.5
				IL_003d: ldc.i4.6
				IL_003e: add
				IL_003f: pop
				IL_0040: nop
				IL_0041: leave.s IL_0043
			} // end handler

			IL_0043: leave.s IL_004c
		} // end .try
		finally
		{
			IL_0045: nop
			IL_0046: ldc.i4.7
			IL_0047: ldc.i4.8
			IL_0048: add
			IL_0049: pop
			IL_004a: nop
			IL_004b: endfinally
		} // end handler

		IL_004c: ldc.i4.s 9
		IL_004e: ldc.i4.s 10
		IL_0050: add
		IL_0051: stloc.2
		IL_0052: br.s IL_0054

		IL_0054: ldloc.2
		IL_0055: ret
	} // end of method BasicTestCases::HandleExceptionBlocks

	.method public hidebysig 
		instance void HandleNop () cil managed 
	{
		// Header size: 1
		// Code size: 7 (0x7)
		.maxstack 8

		IL_0000: nop
		IL_0001: ldnull
		IL_0002: nop
		IL_0003: ldnull
		IL_0004: pop
		IL_0005: pop
		IL_0006: ret
	} // end of method BasicTestCases::HandleNop

	.method private final hidebysig newslot virtual 
		instance void InlineIL.Tests.Common.IBasicTestCases.PushInRef (
			[in] int32& modreq([System.Runtime]System.Runtime.InteropServices.InAttribute) 'value'
		) cil managed 
	{
		.override method instance void [InlineIL.Tests.Common]InlineIL.Tests.Common.IBasicTestCases::PushInRef(int32& modreq([System.Runtime]System.Runtime.InteropServices.InAttribute))
		.param [1]
			.custom instance void [System.Runtime]System.Runtime.CompilerServices.IsReadOnlyAttribute::.ctor() = (
				01 00 00 00
			)
		// Header size: 1
		// Code size: 8 (0x8)
		.maxstack 8

		IL_0000: ldarg.0
		IL_0001: ldarg.1
		IL_0002: call instance void InlineIL.Tests.AssemblyToProcess.BasicTestCases::PushInRef(int32&)
		IL_0007: ret
	} // end of method BasicTestCases::InlineIL.Tests.Common.IBasicTestCases.PushInRef

	.method public hidebysig 
		instance void LdargaS (
			object obj
		) cil managed 
	{
		// Header size: 1
		// Code size: 5 (0x5)
		.maxstack 8

		IL_0000: nop
		IL_0001: ldarga.s obj
		IL_0003: pop
		IL_0004: ret
	} // end of method BasicTestCases::LdargaS

	.method public hidebysig 
		instance int8 LdcI4S () cil managed 
	{
		// Header size: 12
		// Code size: 8 (0x8)
		.maxstack 1
		.locals init (
			[0] int8
		)

		IL_0000: nop
		IL_0001: ldc.i4.s -42
		IL_0003: stloc.0
		IL_0004: br.s IL_0006

		IL_0006: ldloc.0
		IL_0007: ret
	} // end of method BasicTestCases::LdcI4S

	.method public hidebysig 
		instance int32 MultiplyBy3 (
			int32 'value'
		) cil managed 
	{
		// Header size: 1
		// Code size: 7 (0x7)
		.maxstack 8

		IL_0000: nop
		IL_0001: ldarg.1
		IL_0002: dup
		IL_0003: dup
		IL_0004: add
		IL_0005: add
		IL_0006: ret
	} // end of method BasicTestCases::MultiplyBy3

	.method public hidebysig 
		instance int32 NestedClass () cil managed 
	{
		// Header size: 1
		// Code size: 6 (0x6)
		.maxstack 8

		IL_0000: call int32 InlineIL.Tests.AssemblyToProcess.BasicTestCases/NestedClassA/NestedClassB::Call()
		IL_0005: ret
	} // end of method BasicTestCases::NestedClass

	.method public hidebysig 
		instance void NoLeaveAfterThrowOrRethrow () cil managed 
	{
		// Header size: 12
		// Code size: 14 (0xe)
		.maxstack 1

		IL_0000: nop
		.try
		{
			IL_0001: nop
			IL_0002: newobj instance void [System.Runtime]System.InvalidOperationException::.ctor()
			IL_0007: throw
		} // end .try
		catch [System.Runtime]System.InvalidOperationException
		{
			IL_0008: pop
			IL_0009: nop
			IL_000a: nop
			IL_000b: rethrow
		} // end handler
		IL_000d: ret
	} // end of method BasicTestCases::NoLeaveAfterThrowOrRethrow

	.method public hidebysig 
		instance int32 PopArgs (
			int32 arg
		) cil managed 
	{
		// Header size: 12
		// Code size: 14 (0xe)
		.maxstack 2
		.locals init (
			[0] int32
		)

		IL_0000: nop
		IL_0001: ldarg.1
		IL_0002: dup
		IL_0003: add
		IL_0004: starg arg
		IL_0008: ldarg.1
		IL_0009: stloc.0
		IL_000a: br.s IL_000c

		IL_000c: ldloc.0
		IL_000d: ret
	} // end of method BasicTestCases::PopArgs

	.method public hidebysig 
		instance int32 PopLocals () cil managed 
	{
		// Header size: 12
		// Code size: 18 (0x12)
		.maxstack 3
		.locals init (
			[0] int32,
			[1] int32,
			[2] int32,
			[3] int32
		)

		IL_0000: nop
		IL_0001: ldc.i4.2
		IL_0002: ldc.i4.s 10
		IL_0004: ldc.i4.4
		IL_0005: stloc.0
		IL_0006: stloc.1
		IL_0007: stloc.2
		IL_0008: ldloc.0
		IL_0009: ldloc.1
		IL_000a: mul
		IL_000b: ldloc.2
		IL_000c: add
		IL_000d: stloc.3
		IL_000e: br.s IL_0010

		IL_0010: ldloc.3
		IL_0011: ret
	} // end of method BasicTestCases::PopLocals

	.method public hidebysig 
		instance int32 PopStaticField (
			int32 arg
		) cil managed 
	{
		// Header size: 12
		// Code size: 19 (0x13)
		.maxstack 2
		.locals init (
			[0] int32
		)

		IL_0000: nop
		IL_0001: ldarg.1
		IL_0002: dup
		IL_0003: add
		IL_0004: stsfld int32 InlineIL.Tests.AssemblyToProcess.BasicTestCases::StaticIntField
		IL_0009: ldsfld int32 InlineIL.Tests.AssemblyToProcess.BasicTestCases::StaticIntField
		IL_000e: stloc.0
		IL_000f: br.s IL_0011

		IL_0011: ldloc.0
		IL_0012: ret
	} // end of method BasicTestCases::PopStaticField

	.method public hidebysig 
		instance void PushInRef (
			[in] int32& i
		) cil managed 
	{
		.param [1]
			.custom instance void [System.Runtime]System.Runtime.CompilerServices.IsReadOnlyAttribute::.ctor() = (
				01 00 00 00
			)
		// Header size: 1
		// Code size: 6 (0x6)
		.maxstack 8

		IL_0000: nop
		IL_0001: ldarg.1
		IL_0002: ldc.i4.s 42
		IL_0004: stind.i4
		IL_0005: ret
	} // end of method BasicTestCases::PushInRef

	.method public hidebysig 
		instance string PushNullValue () cil managed 
	{
		// Header size: 12
		// Code size: 7 (0x7)
		.maxstack 1
		.locals init (
			[0] string
		)

		IL_0000: nop
		IL_0001: ldnull
		IL_0002: stloc.0
		IL_0003: br.s IL_0005

		IL_0005: ldloc.0
		IL_0006: ret
	} // end of method BasicTestCases::PushNullValue

	.method public hidebysig 
		instance void PushOutRef (
			[out] int32& i
		) cil managed 
	{
		// Header size: 1
		// Code size: 6 (0x6)
		.maxstack 8

		IL_0000: nop
		IL_0001: ldarg.1
		IL_0002: ldc.i4.s 42
		IL_0004: stind.i4
		IL_0005: ret
	} // end of method BasicTestCases::PushOutRef

	.method public hidebysig 
		instance void PushRef (
			int32& i
		) cil managed 
	{
		// Header size: 1
		// Code size: 6 (0x6)
		.maxstack 8

		IL_0000: nop
		IL_0001: ldarg.1
		IL_0002: ldc.i4.s 42
		IL_0004: stind.i4
		IL_0005: ret
	} // end of method BasicTestCases::PushRef

	.method public hidebysig 
		instance string PushValue () cil managed 
	{
		// Header size: 12
		// Code size: 11 (0xb)
		.maxstack 1
		.locals init (
			[0] string
		)

		IL_0000: nop
		IL_0001: ldstr "Hello"
		IL_0006: stloc.0
		IL_0007: br.s IL_0009

		IL_0009: ldloc.0
		IL_000a: ret
	} // end of method BasicTestCases::PushValue

	.method public hidebysig 
		instance int32 ReturnArg (
			int32 'value'
		) cil managed 
	{
		// Header size: 12
		// Code size: 7 (0x7)
		.maxstack 1
		.locals init (
			[0] int32
		)

		IL_0000: nop
		IL_0001: ldarg.1
		IL_0002: stloc.0
		IL_0003: br.s IL_0005

		IL_0005: ldloc.0
		IL_0006: ret
	} // end of method BasicTestCases::ReturnArg

	.method public hidebysig 
		instance float32 ReturnWithConversion1 () cil managed 
	{
		// Header size: 12
		// Code size: 9 (0x9)
		.maxstack 1
		.locals init (
			[0] float32
		)

		IL_0000: nop
		IL_0001: ldc.i4.s 42
		IL_0003: conv.r4
		IL_0004: stloc.0
		IL_0005: br.s IL_0007

		IL_0007: ldloc.0
		IL_0008: ret
	} // end of method BasicTestCases::ReturnWithConversion1

	.method public hidebysig 
		instance valuetype [System.Runtime]System.Nullable`1<int32> ReturnWithConversion2 () cil managed 
	{
		// Header size: 12
		// Code size: 13 (0xd)
		.maxstack 1
		.locals init (
			[0] valuetype [System.Runtime]System.Nullable`1<int32>
		)

		IL_0000: nop
		IL_0001: ldc.i4.s 42
		IL_0003: newobj instance void valuetype [System.Runtime]System.Nullable`1<int32>::.ctor(!0)
		IL_0008: stloc.0
		IL_0009: br.s IL_000b

		IL_000b: ldloc.0
		IL_000c: ret
	} // end of method BasicTestCases::ReturnWithConversion2

	.method public hidebysig 
		instance string SayHi () cil managed 
	{
		// Header size: 1
		// Code size: 7 (0x7)
		.maxstack 8

		IL_0000: nop
		IL_0001: ldstr "Hello!"
		IL_0006: ret
	} // end of method BasicTestCases::SayHi

	.method public hidebysig 
		instance void ShortenInstructions (
			int32 arg
		) cil managed 
	{
		// Header size: 12
		// Code size: 12 (0xc)
		.maxstack 1

		IL_0000: nop
		IL_0001: ldarg.1
		IL_0002: pop
		IL_0003: ldarg.1
		IL_0004: pop
		IL_0005: ldarg.1
		IL_0006: pop
		IL_0007: ldarg.1
		IL_0008: pop
		IL_0009: ldarg.1
		IL_000a: pop
		IL_000b: ret
	} // end of method BasicTestCases::ShortenInstructions

	.method public hidebysig 
		instance int32 TwoPlusTwo () cil managed 
	{
		// Header size: 1
		// Code size: 15 (0xf)
		.maxstack 8

		IL_0000: nop
		IL_0001: ldc.i4.2
		IL_0002: conv.i8
		IL_0003: ldc.i8 2
		IL_000c: add
		IL_000d: conv.i4
		IL_000e: ret
	} // end of method BasicTestCases::TwoPlusTwo

	.method public hidebysig 
		instance int32 TwoPlusTwoByte () cil managed 
	{
		// Header size: 1
		// Code size: 5 (0x5)
		.maxstack 8

		IL_0000: nop
		IL_0001: ldc.i4.2
		IL_0002: ldc.i4.2
		IL_0003: add
		IL_0004: ret
	} // end of method BasicTestCases::TwoPlusTwoByte

	.method public hidebysig 
		instance float64 TwoPlusTwoFloat () cil managed 
	{
		// Header size: 1
		// Code size: 17 (0x11)
		.maxstack 8

		IL_0000: nop
		IL_0001: ldc.r4 2
		IL_0006: ldc.r8 2
		IL_000f: add
		IL_0010: ret
	} // end of method BasicTestCases::TwoPlusTwoFloat

	.method private hidebysig static 
		void ValidatePushUsageWithEnsureLocal (
			int32 'value',
			valuetype [System.Runtime]System.Guid guid
		) cil managed 
	{
		// Header size: 12
		// Code size: 36 (0x24)
		.maxstack 2
		.locals init (
			[0] bool
		)

		IL_0000: nop
		IL_0001: ldarg.0
		IL_0002: ldc.i4.s 42
		IL_0004: bne.un.s IL_0013

		IL_0006: ldarg.1
		IL_0007: ldsfld valuetype [System.Runtime]System.Guid [System.Runtime]System.Guid::Empty
		IL_000c: call bool [System.Runtime]System.Guid::op_Equality(valuetype [System.Runtime]System.Guid, valuetype [System.Runtime]System.Guid)
		IL_0011: br.s IL_0014

		IL_0013: ldc.i4.1

		IL_0014: stloc.0
		IL_0015: ldloc.0
		IL_0016: brfalse.s IL_0023

		IL_0018: ldstr "Invalid value"
		IL_001d: newobj instance void [System.Runtime]System.InvalidOperationException::.ctor(string)
		IL_0022: throw

		IL_0023: ret
	} // end of method BasicTestCases::ValidatePushUsageWithEnsureLocal

	.method public hidebysig 
		instance void ValidPushUsageWithEnsureLocal () cil managed 
	{
		// Header size: 12
		// Code size: 16 (0x10)
		.maxstack 2
		.locals init (
			[0] valuetype [System.Runtime]System.Guid
		)

		IL_0000: nop
		IL_0001: call valuetype [System.Runtime]System.Guid [System.Runtime]System.Guid::NewGuid()
		IL_0006: stloc.0
		IL_0007: ldc.i4.s 42
		IL_0009: ldloc.0
		IL_000a: call void InlineIL.Tests.AssemblyToProcess.BasicTestCases::ValidatePushUsageWithEnsureLocal(int32, valuetype [System.Runtime]System.Guid)
		IL_000f: ret
	} // end of method BasicTestCases::ValidPushUsageWithEnsureLocal

} // end of class InlineIL.Tests.AssemblyToProcess.BasicTestCases
