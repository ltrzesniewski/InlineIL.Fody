.class public auto ansi beforefieldinit InlineIL.Tests.AssemblyToProcess.BasicTestCases
	extends [netstandard]System.Object
	implements [InlineIL.Tests.Common]InlineIL.Tests.Common.IBasicTestCases
{
	.custom instance void [InlineIL.Tests.Common]InlineIL.Tests.Common.TestCasesAttribute::.ctor() = (
		01 00 00 00
	)
	// Nested Types
	.class nested private auto ansi abstract sealed beforefieldinit NestedClassA
		extends [netstandard]System.Object
	{
		// Nested Types
		.class nested public auto ansi abstract sealed beforefieldinit NestedClassB
			extends [netstandard]System.Object
		{
			// Methods
			.method public hidebysig static 
				int32 Call () cil managed 
			{
				// Header size: 1
				// Code size: 4 (0x4)
				.maxstack 8

				IL_0000: ldc.i4.1
				IL_0001: ldc.i4.2
				IL_0002: add
				IL_0003: ret
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
		// Code size: 7 (0x7)
		.maxstack 8

		IL_0000: ldarg.0
		IL_0001: call instance void [netstandard]System.Object::.ctor()
		IL_0006: ret
	} // end of method BasicTestCases::.ctor

	.method public hidebysig 
		instance void AddAssign (
			int32& a,
			int32 b
		) cil managed 
	{
		// Header size: 1
		// Code size: 7 (0x7)
		.maxstack 8

		IL_0000: ldarg.1
		IL_0001: ldarg.1
		IL_0002: ldind.i4
		IL_0003: ldarg.2
		IL_0004: add
		IL_0005: stind.i4
		IL_0006: ret
	} // end of method BasicTestCases::AddAssign

	.method public hidebysig 
		instance void EnsureLocal () cil managed 
	{
		// Header size: 12
		// Code size: 7 (0x7)
		.maxstack 1
		.locals init (
			[0] valuetype [netstandard]System.Guid
		)

		IL_0000: call valuetype [netstandard]System.Guid [netstandard]System.Guid::NewGuid()
		IL_0005: stloc.0
		IL_0006: ret
	} // end of method BasicTestCases::EnsureLocal

	.method public hidebysig 
		instance void ExplicitEndFinally () cil managed 
	{
		// Header size: 12
		// Code size: 4 (0x4)
		.maxstack 0

		.try
		{
			IL_0000: leave.s IL_0003
		} // end .try
		finally
		{
			IL_0002: endfinally
		} // end handler

		IL_0003: ret
	} // end of method BasicTestCases::ExplicitEndFinally

	.method public hidebysig 
		instance void ExplicitLeave () cil managed 
	{
		// Header size: 12
		// Code size: 6 (0x6)
		.maxstack 1

		.try
		{
			IL_0000: leave.s IL_0005
		} // end .try
		catch [netstandard]System.Object
		{
			IL_0002: pop
			IL_0003: leave.s IL_0005
		} // end handler

		IL_0005: ret
	} // end of method BasicTestCases::ExplicitLeave

	.method public hidebysig 
		instance void ExplicitRet () cil managed 
	{
		// Header size: 1
		// Code size: 1 (0x1)
		.maxstack 8

		IL_0000: ret
	} // end of method BasicTestCases::ExplicitRet

	.method public hidebysig 
		instance int32 HandleExceptionBlocks () cil managed 
	{
		// Header size: 12
		// Code size: 72 (0x48)
		.maxstack 2
		.locals init (
			[0] class [netstandard]System.InvalidOperationException
		)

		IL_0000: ldc.i4.1
		IL_0001: ldc.i4.2
		IL_0002: add
		IL_0003: pop
		.try
		{
			.try
			{
				IL_0004: ldc.i4.3
				IL_0005: ldc.i4.4
				IL_0006: add
				IL_0007: ldstr "foo"
				IL_000c: newobj instance void [netstandard]System.InvalidOperationException::.ctor(string)
				IL_0011: throw
			} // end .try
			filter
			{
				IL_0012: isinst [netstandard]System.InvalidOperationException
				IL_0017: dup
				IL_0018: brtrue.s IL_001e

				IL_001a: pop
				IL_001b: ldc.i4.0
				IL_001c: br.s IL_0032

				IL_001e: stloc.0
				IL_001f: ldloc.0
				IL_0020: callvirt instance string [netstandard]System.Exception::get_Message()
				IL_0025: ldstr "foo"
				IL_002a: callvirt instance bool [netstandard]System.String::Contains(string)
				IL_002f: ldc.i4.0
				IL_0030: cgt.un

				IL_0032: endfilter
			} // end filter
			catch
			{
				IL_0034: pop
				IL_0035: ldloc.0
				IL_0036: pop
				IL_0037: ldc.i4.5
				IL_0038: ldc.i4.6
				IL_0039: add
				IL_003a: pop
				IL_003b: leave.s IL_0042
			} // end handler
		} // end .try
		finally
		{
			IL_003d: ldc.i4.7
			IL_003e: ldc.i4.8
			IL_003f: add
			IL_0040: pop
			IL_0041: endfinally
		} // end handler

		IL_0042: ldc.i4.s 9
		IL_0044: ldc.i4.s 10
		IL_0046: add
		IL_0047: ret
	} // end of method BasicTestCases::HandleExceptionBlocks

	.method public hidebysig 
		instance void HandleNop () cil managed 
	{
		// Header size: 1
		// Code size: 6 (0x6)
		.maxstack 8

		IL_0000: ldnull
		IL_0001: nop
		IL_0002: ldnull
		IL_0003: pop
		IL_0004: pop
		IL_0005: ret
	} // end of method BasicTestCases::HandleNop

	.method private final hidebysig newslot virtual 
		instance void InlineIL.Tests.Common.IBasicTestCases.PushInRef (
			[in] int32& modreq([netstandard]System.Runtime.InteropServices.InAttribute) 'value'
		) cil managed 
	{
		.override method instance void [InlineIL.Tests.Common]InlineIL.Tests.Common.IBasicTestCases::PushInRef(int32& modreq([netstandard]System.Runtime.InteropServices.InAttribute))
		.param [1]
			.custom instance void System.Runtime.CompilerServices.IsReadOnlyAttribute::.ctor() = (
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
		// Code size: 4 (0x4)
		.maxstack 8

		IL_0000: ldarga.s obj
		IL_0002: pop
		IL_0003: ret
	} // end of method BasicTestCases::LdargaS

	.method public hidebysig 
		instance int8 LdcI4S () cil managed 
	{
		// Header size: 1
		// Code size: 3 (0x3)
		.maxstack 8

		IL_0000: ldc.i4.s -42
		IL_0002: ret
	} // end of method BasicTestCases::LdcI4S

	.method public hidebysig 
		instance int32 MultiplyBy3 (
			int32 'value'
		) cil managed 
	{
		// Header size: 1
		// Code size: 6 (0x6)
		.maxstack 8

		IL_0000: ldarg.1
		IL_0001: dup
		IL_0002: dup
		IL_0003: add
		IL_0004: add
		IL_0005: ret
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
		// Code size: 11 (0xb)
		.maxstack 1

		.try
		{
			IL_0000: newobj instance void [netstandard]System.InvalidOperationException::.ctor()
			IL_0005: throw
		} // end .try
		catch [netstandard]System.InvalidOperationException
		{
			IL_0006: pop
			IL_0007: nop
			IL_0008: rethrow
		} // end handler
		IL_000a: ret
	} // end of method BasicTestCases::NoLeaveAfterThrowOrRethrow

	.method public hidebysig 
		instance int32 PopArgs (
			int32 arg
		) cil managed 
	{
		// Header size: 1
		// Code size: 9 (0x9)
		.maxstack 8

		IL_0000: ldarg.1
		IL_0001: dup
		IL_0002: add
		IL_0003: starg arg
		IL_0007: ldarg.1
		IL_0008: ret
	} // end of method BasicTestCases::PopArgs

	.method public hidebysig 
		instance int32 PopLocals () cil managed 
	{
		// Header size: 12
		// Code size: 13 (0xd)
		.maxstack 3
		.locals init (
			[0] int32,
			[1] int32,
			[2] int32
		)

		IL_0000: ldc.i4.2
		IL_0001: ldc.i4.s 10
		IL_0003: ldc.i4.4
		IL_0004: stloc.0
		IL_0005: stloc.1
		IL_0006: stloc.2
		IL_0007: ldloc.0
		IL_0008: ldloc.1
		IL_0009: mul
		IL_000a: ldloc.2
		IL_000b: add
		IL_000c: ret
	} // end of method BasicTestCases::PopLocals

	.method public hidebysig 
		instance int32 PopStaticField (
			int32 arg
		) cil managed 
	{
		// Header size: 1
		// Code size: 14 (0xe)
		.maxstack 8

		IL_0000: ldarg.1
		IL_0001: dup
		IL_0002: add
		IL_0003: stsfld int32 InlineIL.Tests.AssemblyToProcess.BasicTestCases::StaticIntField
		IL_0008: ldsfld int32 InlineIL.Tests.AssemblyToProcess.BasicTestCases::StaticIntField
		IL_000d: ret
	} // end of method BasicTestCases::PopStaticField

	.method public hidebysig 
		instance void PushInRef (
			[in] int32& i
		) cil managed 
	{
		.param [1]
			.custom instance void System.Runtime.CompilerServices.IsReadOnlyAttribute::.ctor() = (
				01 00 00 00
			)
		// Header size: 1
		// Code size: 5 (0x5)
		.maxstack 8

		IL_0000: ldarg.1
		IL_0001: ldc.i4.s 42
		IL_0003: stind.i4
		IL_0004: ret
	} // end of method BasicTestCases::PushInRef

	.method public hidebysig 
		instance string PushNullValue () cil managed 
	{
		// Header size: 1
		// Code size: 2 (0x2)
		.maxstack 8

		IL_0000: ldnull
		IL_0001: ret
	} // end of method BasicTestCases::PushNullValue

	.method public hidebysig 
		instance void PushOutRef (
			[out] int32& i
		) cil managed 
	{
		// Header size: 1
		// Code size: 5 (0x5)
		.maxstack 8

		IL_0000: ldarg.1
		IL_0001: ldc.i4.s 42
		IL_0003: stind.i4
		IL_0004: ret
	} // end of method BasicTestCases::PushOutRef

	.method public hidebysig 
		instance void PushRef (
			int32& i
		) cil managed 
	{
		// Header size: 1
		// Code size: 5 (0x5)
		.maxstack 8

		IL_0000: ldarg.1
		IL_0001: ldc.i4.s 42
		IL_0003: stind.i4
		IL_0004: ret
	} // end of method BasicTestCases::PushRef

	.method public hidebysig 
		instance string PushValue () cil managed 
	{
		// Header size: 1
		// Code size: 6 (0x6)
		.maxstack 8

		IL_0000: ldstr "Hello"
		IL_0005: ret
	} // end of method BasicTestCases::PushValue

	.method public hidebysig 
		instance int32 ReturnArg (
			int32 'value'
		) cil managed 
	{
		// Header size: 1
		// Code size: 2 (0x2)
		.maxstack 8

		IL_0000: ldarg.1
		IL_0001: ret
	} // end of method BasicTestCases::ReturnArg

	.method public hidebysig 
		instance float32 ReturnWithConversion1 () cil managed 
	{
		// Header size: 1
		// Code size: 4 (0x4)
		.maxstack 8

		IL_0000: ldc.i4.s 42
		IL_0002: conv.r4
		IL_0003: ret
	} // end of method BasicTestCases::ReturnWithConversion1

	.method public hidebysig 
		instance valuetype [netstandard]System.Nullable`1<int32> ReturnWithConversion2 () cil managed 
	{
		// Header size: 1
		// Code size: 8 (0x8)
		.maxstack 8

		IL_0000: ldc.i4.s 42
		IL_0002: newobj instance void valuetype [netstandard]System.Nullable`1<int32>::.ctor(!0)
		IL_0007: ret
	} // end of method BasicTestCases::ReturnWithConversion2

	.method public hidebysig 
		instance string SayHi () cil managed 
	{
		// Header size: 1
		// Code size: 6 (0x6)
		.maxstack 8

		IL_0000: ldstr "Hello!"
		IL_0005: ret
	} // end of method BasicTestCases::SayHi

	.method public hidebysig 
		instance void ShortenInstructions (
			int32 arg
		) cil managed 
	{
		// Header size: 1
		// Code size: 11 (0xb)
		.maxstack 8

		IL_0000: ldarg.1
		IL_0001: pop
		IL_0002: ldarg.1
		IL_0003: pop
		IL_0004: ldarg.1
		IL_0005: pop
		IL_0006: ldarg.1
		IL_0007: pop
		IL_0008: ldarg.1
		IL_0009: pop
		IL_000a: ret
	} // end of method BasicTestCases::ShortenInstructions

	.method public hidebysig 
		instance int32 TwoPlusTwo () cil managed 
	{
		// Header size: 1
		// Code size: 14 (0xe)
		.maxstack 8

		IL_0000: ldc.i4.2
		IL_0001: conv.i8
		IL_0002: ldc.i8 2
		IL_000b: add
		IL_000c: conv.i4
		IL_000d: ret
	} // end of method BasicTestCases::TwoPlusTwo

	.method public hidebysig 
		instance int32 TwoPlusTwoByte () cil managed 
	{
		// Header size: 1
		// Code size: 4 (0x4)
		.maxstack 8

		IL_0000: ldc.i4.2
		IL_0001: ldc.i4.2
		IL_0002: add
		IL_0003: ret
	} // end of method BasicTestCases::TwoPlusTwoByte

	.method public hidebysig 
		instance float64 TwoPlusTwoFloat () cil managed 
	{
		// Header size: 1
		// Code size: 16 (0x10)
		.maxstack 8

		IL_0000: ldc.r4 2
		IL_0005: ldc.r8 2
		IL_000e: add
		IL_000f: ret
	} // end of method BasicTestCases::TwoPlusTwoFloat

	.method private hidebysig static 
		void ValidatePushUsageWithEnsureLocal (
			int32 'value',
			valuetype [netstandard]System.Guid guid
		) cil managed 
	{
		// Header size: 1
		// Code size: 30 (0x1e)
		.maxstack 8

		IL_0000: ldarg.0
		IL_0001: ldc.i4.s 42
		IL_0003: bne.un.s IL_0012

		IL_0005: ldarg.1
		IL_0006: ldsfld valuetype [netstandard]System.Guid [netstandard]System.Guid::Empty
		IL_000b: call bool [netstandard]System.Guid::op_Equality(valuetype [netstandard]System.Guid, valuetype [netstandard]System.Guid)
		IL_0010: brfalse.s IL_001d

		IL_0012: ldstr "Invalid value"
		IL_0017: newobj instance void [netstandard]System.InvalidOperationException::.ctor(string)
		IL_001c: throw

		IL_001d: ret
	} // end of method BasicTestCases::ValidatePushUsageWithEnsureLocal

	.method public hidebysig 
		instance void ValidPushUsageWithEnsureLocal () cil managed 
	{
		// Header size: 12
		// Code size: 15 (0xf)
		.maxstack 2
		.locals init (
			[0] valuetype [netstandard]System.Guid
		)

		IL_0000: call valuetype [netstandard]System.Guid [netstandard]System.Guid::NewGuid()
		IL_0005: stloc.0
		IL_0006: ldc.i4.s 42
		IL_0008: ldloc.0
		IL_0009: call void InlineIL.Tests.AssemblyToProcess.BasicTestCases::ValidatePushUsageWithEnsureLocal(int32, valuetype [netstandard]System.Guid)
		IL_000e: ret
	} // end of method BasicTestCases::ValidPushUsageWithEnsureLocal

} // end of class InlineIL.Tests.AssemblyToProcess.BasicTestCases
