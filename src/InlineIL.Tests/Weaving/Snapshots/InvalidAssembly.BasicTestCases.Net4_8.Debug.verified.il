.class public auto ansi beforefieldinit InlineIL.Tests.InvalidAssemblyToProcess.BasicTestCases
	extends [mscorlib]System.Object
{
	.custom instance void [InlineIL.Tests.Common]InlineIL.Tests.Common.TestCasesAttribute::.ctor() = (
		01 00 00 00
	)
	// Fields
	.field private int32 _intField

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
	} // end of method BasicTestCases::.ctor

	.method assembly hidebysig static 
		valuetype [mscorlib]System.ValueTuple`2<int32, int32>& '<InvalidPushUsage2>g__GetRefToStruct|5_0' (
			int32 _
		) cil managed 
	{
		.custom instance void [mscorlib]System.Runtime.CompilerServices.CompilerGeneratedAttribute::.ctor() = (
			01 00 00 00
		)
		.param [0]
			.custom instance void [mscorlib]System.Runtime.CompilerServices.TupleElementNamesAttribute::.ctor(string[]) = (
				01 00 02 00 00 00 03 66 6f 6f 03 62 61 72 00 00
			)
		// Header size: 12
		// Code size: 6 (0x6)
		.maxstack 1
		.locals init (
			[0] valuetype [mscorlib]System.ValueTuple`2<int32, int32>
		)

		IL_0000: newobj instance void [mscorlib]System.InvalidOperationException::.ctor()
		IL_0005: throw
	} // end of method BasicTestCases::'<InvalidPushUsage2>g__GetRefToStruct|5_0'

	.method public hidebysig 
		instance void InvalidEnsureLocalUsage () cil managed 
	{
		// Header size: 12
		// Code size: 11 (0xb)
		.maxstack 1

		IL_0000: ldstr "InlineIL processing failed: Unexpected argument type, expected a non-ref local variable. (in System.Void InlineIL.Tests.InvalidAssemblyToProcess.BasicTestCases::InvalidEnsureLocalUsage() at instruction IL_0009: call System.Void InlineIL.IL::EnsureLocal<System.Int32>(!!0&))"
		IL_0005: newobj instance void [mscorlib]System.InvalidProgramException::.ctor(string)
		IL_000a: throw
	} // end of method BasicTestCases::InvalidEnsureLocalUsage

	.method public hidebysig 
		instance void InvalidEnsureLocalUsage2 (
			int32 foo
		) cil managed 
	{
		// Header size: 1
		// Code size: 11 (0xb)
		.maxstack 8

		IL_0000: ldstr "InlineIL processing failed: Unexpected argument type, expected a non-ref local variable. (in System.Void InlineIL.Tests.InvalidAssemblyToProcess.BasicTestCases::InvalidEnsureLocalUsage2(System.Int32) at instruction IL_0003: call System.Void InlineIL.IL::EnsureLocal<System.Int32>(!!0&))"
		IL_0005: newobj instance void [mscorlib]System.InvalidProgramException::.ctor(string)
		IL_000a: throw
	} // end of method BasicTestCases::InvalidEnsureLocalUsage2

	.method public hidebysig 
		instance void InvalidEnsureLocalUsage3 () cil managed 
	{
		// Header size: 1
		// Code size: 11 (0xb)
		.maxstack 8

		IL_0000: ldstr "InlineIL processing failed: Unexpected argument type, expected a non-ref local variable. (in System.Void InlineIL.Tests.InvalidAssemblyToProcess.BasicTestCases::InvalidEnsureLocalUsage3() at instruction IL_0007: call System.Void InlineIL.IL::EnsureLocal<System.Int32>(!!0&))"
		IL_0005: newobj instance void [mscorlib]System.InvalidProgramException::.ctor(string)
		IL_000a: throw
	} // end of method BasicTestCases::InvalidEnsureLocalUsage3

	.method public hidebysig 
		instance void InvalidPushUsage () cil managed 
	{
		// Header size: 12
		// Code size: 11 (0xb)
		.maxstack 2
		.locals init (
			[0] valuetype [mscorlib]System.Guid
		)

		IL_0000: nop
		IL_0001: call valuetype [mscorlib]System.Guid [mscorlib]System.Guid::NewGuid()
		IL_0006: stloc.0
		IL_0007: ldc.i4.s 42
		IL_0009: ldloc.0
		IL_000a: ret
	} // end of method BasicTestCases::InvalidPushUsage

	.method public hidebysig 
		instance void InvalidPushUsage2 () cil managed 
	{
		// Header size: 12
		// Code size: 38 (0x26)
		.maxstack 2
		.locals init (
			[0] valuetype [mscorlib]System.ValueTuple`2<int32, int32>&,
			[1] valuetype [mscorlib]System.ValueTuple`2<int32, int32>&,
			[2] int32
		)

		IL_0000: nop
		IL_0001: ldc.i4.m1
		IL_0002: call valuetype [mscorlib]System.ValueTuple`2<int32, int32>& InlineIL.Tests.InvalidAssemblyToProcess.BasicTestCases::'<InvalidPushUsage2>g__GetRefToStruct|5_0'(int32)
		IL_0007: stloc.0
		IL_0008: ldc.i4.0
		IL_0009: call valuetype [mscorlib]System.ValueTuple`2<int32, int32>& InlineIL.Tests.InvalidAssemblyToProcess.BasicTestCases::'<InvalidPushUsage2>g__GetRefToStruct|5_0'(int32)
		IL_000e: stloc.1
		IL_000f: ldloc.0
		IL_0010: ldfld !0 valuetype [mscorlib]System.ValueTuple`2<int32, int32>::Item1
		IL_0015: ldloc.1
		IL_0016: ldfld !0 valuetype [mscorlib]System.ValueTuple`2<int32, int32>::Item1
		IL_001b: add
		IL_001c: stloc.2
		IL_001d: ldloc.0
		IL_001e: ldloc.2
		IL_001f: stfld !0 valuetype [mscorlib]System.ValueTuple`2<int32, int32>::Item1
		IL_0024: nop
		IL_0025: ret
	} // end of method BasicTestCases::InvalidPushUsage2

	.method public hidebysig 
		instance void InvalidPushUsage3 () cil managed 
	{
		// Header size: 12
		// Code size: 23 (0x17)
		.maxstack 7
		.locals init (
			[0] int32[]
		)

		IL_0000: nop
		IL_0001: ldc.i4.4
		IL_0002: newarr [mscorlib]System.Int32
		IL_0007: stloc.0
		IL_0008: ldloc.0
		IL_0009: ldc.i4.0
		IL_000a: ldc.i4.s 42
		IL_000c: stelem.i4
		IL_000d: ldloc.0
		IL_000e: ldc.i4.1
		IL_000f: ldloc.0
		IL_0010: ldc.i4.2
		IL_0011: ldloc.0
		IL_0012: ldc.i4.0
		IL_0013: ldc.i4.s 42
		IL_0015: stelem.i4
		IL_0016: ret
	} // end of method BasicTestCases::InvalidPushUsage3

	.method public hidebysig 
		instance void InvalidPushUsage4 () cil managed 
	{
		// Header size: 12
		// Code size: 18 (0x12)
		.maxstack 4
		.locals init (
			[0] int32[]
		)

		IL_0000: nop
		IL_0001: ldc.i4.4
		IL_0002: newarr [mscorlib]System.Int32
		IL_0007: stloc.0
		IL_0008: ldloc.0
		IL_0009: ldc.i4.0
		IL_000a: ldc.i4.s 42
		IL_000c: stelem.i4
		IL_000d: ldloc.0
		IL_000e: ldc.i4.1
		IL_000f: ldloc.0
		IL_0010: ldc.i4.2
		IL_0011: ret
	} // end of method BasicTestCases::InvalidPushUsage4

	.method public hidebysig 
		instance void InvalidReturn () cil managed 
	{
		// Header size: 1
		// Code size: 11 (0xb)
		.maxstack 8

		IL_0000: ldstr "InlineIL processing failed: The result of the IL.Return method should be immediately returned: return IL.Return<T>(); (in System.Void InlineIL.Tests.InvalidAssemblyToProcess.BasicTestCases::InvalidReturn() at instruction IL_0001: call !!0 InlineIL.IL::Return<System.Int32>())"
		IL_0005: newobj instance void [mscorlib]System.InvalidProgramException::.ctor(string)
		IL_000a: throw
	} // end of method BasicTestCases::InvalidReturn

	.method public hidebysig 
		instance void InvalidUnreachable () cil managed 
	{
		// Header size: 1
		// Code size: 11 (0xb)
		.maxstack 8

		IL_0000: ldstr "InlineIL processing failed: The result of the IL.Unreachable method should be immediately thrown: throw IL.Unreachable(); (in System.Void InlineIL.Tests.InvalidAssemblyToProcess.BasicTestCases::InvalidUnreachable() at instruction IL_0001: call System.Exception InlineIL.IL::Unreachable())"
		IL_0005: newobj instance void [mscorlib]System.InvalidProgramException::.ctor(string)
		IL_000a: throw
	} // end of method BasicTestCases::InvalidUnreachable

	.method public hidebysig 
		instance void NonExistingParameter () cil managed 
	{
		// Header size: 1
		// Code size: 11 (0xb)
		.maxstack 8

		IL_0000: ldstr "InlineIL processing failed: Parameter 'foo' is not defined (in System.Void InlineIL.Tests.InvalidAssemblyToProcess.BasicTestCases::NonExistingParameter() at instruction IL_0001: ldstr \"foo\")"
		IL_0005: newobj instance void [mscorlib]System.InvalidProgramException::.ctor(string)
		IL_000a: throw
	} // end of method BasicTestCases::NonExistingParameter

	.method public hidebysig 
		instance void NotSameBasicBlock (
			bool a
		) cil managed 
	{
		// Header size: 1
		// Code size: 11 (0xb)
		.maxstack 8

		IL_0000: ldstr "InlineIL processing failed: An unconditional expression was expected. (in System.Void InlineIL.Tests.InvalidAssemblyToProcess.BasicTestCases::NotSameBasicBlock(System.Boolean) at instruction IL_0008: ldc.i4 42)"
		IL_0005: newobj instance void [mscorlib]System.InvalidProgramException::.ctor(string)
		IL_000a: throw
	} // end of method BasicTestCases::NotSameBasicBlock

	.method public hidebysig 
		instance void NotSameBasicBlock2 () cil managed 
	{
		// Header size: 1
		// Code size: 11 (0xb)
		.maxstack 8

		IL_0000: ldstr "InlineIL processing failed: An unconditional expression was expected. (in System.Void InlineIL.Tests.InvalidAssemblyToProcess.BasicTestCases::NotSameBasicBlock2() at instruction IL_0032: call InlineIL.MethodRef InlineIL.MethodRef::Constructor(InlineIL.TypeRef,InlineIL.TypeRef[]))"
		IL_0005: newobj instance void [mscorlib]System.InvalidProgramException::.ctor(string)
		IL_000a: throw
	} // end of method BasicTestCases::NotSameBasicBlock2

	.method public hidebysig 
		instance void NotSameBasicBlockArray (
			bool a
		) cil managed 
	{
		// Header size: 1
		// Code size: 11 (0xb)
		.maxstack 8

		IL_0000: ldstr "InlineIL processing failed: An unconditional expression was expected. (in System.Void InlineIL.Tests.InvalidAssemblyToProcess.BasicTestCases::NotSameBasicBlockArray(System.Boolean) at instruction IL_0007: ldc.i4 1)"
		IL_0005: newobj instance void [mscorlib]System.InvalidProgramException::.ctor(string)
		IL_000a: throw
	} // end of method BasicTestCases::NotSameBasicBlockArray

	.method public hidebysig 
		instance void NotSameBasicBlockArray2 (
			bool a
		) cil managed 
	{
		// Header size: 1
		// Code size: 11 (0xb)
		.maxstack 8

		IL_0000: ldstr "InlineIL processing failed: Could not locate call argument due to IL_0011: br IL_0018 (in System.Void InlineIL.Tests.InvalidAssemblyToProcess.BasicTestCases::NotSameBasicBlockArray2(System.Boolean) at instruction IL_0018: stelem.ref)"
		IL_0005: newobj instance void [mscorlib]System.InvalidProgramException::.ctor(string)
		IL_000a: throw
	} // end of method BasicTestCases::NotSameBasicBlockArray2

	.method public hidebysig 
		instance void PopToArray () cil managed 
	{
		// Header size: 12
		// Code size: 11 (0xb)
		.maxstack 1

		IL_0000: ldstr "InlineIL processing failed: IL.Pop does not support array references. Emit a stelem instruction instead. (in System.Void InlineIL.Tests.InvalidAssemblyToProcess.BasicTestCases::PopToArray() at instruction IL_000f: call System.Void InlineIL.IL::Pop<System.Int32>(!!0&))"
		IL_0005: newobj instance void [mscorlib]System.InvalidProgramException::.ctor(string)
		IL_000a: throw
	} // end of method BasicTestCases::PopToArray

	.method public hidebysig 
		instance void PopToField () cil managed 
	{
		// Header size: 1
		// Code size: 11 (0xb)
		.maxstack 8

		IL_0000: ldstr "InlineIL processing failed: IL.Pop does not support instance field references. Emit a stfld instruction instead. (in System.Void InlineIL.Tests.InvalidAssemblyToProcess.BasicTestCases::PopToField() at instruction IL_0007: call System.Void InlineIL.IL::Pop<System.Int32>(!!0&))"
		IL_0005: newobj instance void [mscorlib]System.InvalidProgramException::.ctor(string)
		IL_000a: throw
	} // end of method BasicTestCases::PopToField

	.method public hidebysig 
		instance void UnusedInstance () cil managed 
	{
		// Header size: 1
		// Code size: 11 (0xb)
		.maxstack 8

		IL_0000: ldstr "InlineIL processing failed: Unconsumed reference to InlineIL (in System.Void InlineIL.Tests.InvalidAssemblyToProcess.BasicTestCases::UnusedInstance() at instruction IL_0001: ldtoken InlineIL.IL)"
		IL_0005: newobj instance void [mscorlib]System.InvalidProgramException::.ctor(string)
		IL_000a: throw
	} // end of method BasicTestCases::UnusedInstance

} // end of class InlineIL.Tests.InvalidAssemblyToProcess.BasicTestCases
