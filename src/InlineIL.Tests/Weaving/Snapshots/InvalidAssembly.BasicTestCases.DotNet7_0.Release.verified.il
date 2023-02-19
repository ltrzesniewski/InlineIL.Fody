.class public auto ansi beforefieldinit InlineIL.Tests.InvalidAssemblyToProcess.BasicTestCases
	extends [netstandard]System.Object
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
		// Code size: 7 (0x7)
		.maxstack 8

		IL_0000: ldarg.0
		IL_0001: call instance void [netstandard]System.Object::.ctor()
		IL_0006: ret
	} // end of method BasicTestCases::.ctor

	.method assembly hidebysig static 
		valuetype [netstandard]System.ValueTuple`2<int32, int32>& '<InvalidPushUsage2>g__GetRefToStruct|5_0' (
			int32 _
		) cil managed 
	{
		.custom instance void [netstandard]System.Runtime.CompilerServices.CompilerGeneratedAttribute::.ctor() = (
			01 00 00 00
		)
		.param [0]
			.custom instance void [netstandard]System.Runtime.CompilerServices.TupleElementNamesAttribute::.ctor(string[]) = (
				01 00 02 00 00 00 03 66 6f 6f 03 62 61 72 00 00
			)
		// Header size: 12
		// Code size: 6 (0x6)
		.maxstack 1
		.locals init (
			[0] valuetype [netstandard]System.ValueTuple`2<int32, int32>
		)

		IL_0000: newobj instance void [netstandard]System.InvalidOperationException::.ctor()
		IL_0005: throw
	} // end of method BasicTestCases::'<InvalidPushUsage2>g__GetRefToStruct|5_0'

	.method public hidebysig 
		instance void InvalidEnsureLocalUsage () cil managed 
	{
		// Header size: 1
		// Code size: 11 (0xb)
		.maxstack 8

		IL_0000: ldstr "InlineIL processing failed: Unexpected argument type, expected a non-ref local variable. (in System.Void InlineIL.Tests.InvalidAssemblyToProcess.BasicTestCases::InvalidEnsureLocalUsage() at instruction IL_0006: call System.Void InlineIL.IL::EnsureLocal<System.Int32>(!!0&))"
		IL_0005: newobj instance void [netstandard]System.InvalidProgramException::.ctor(string)
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

		IL_0000: ldstr "InlineIL processing failed: Unexpected argument type, expected a non-ref local variable. (in System.Void InlineIL.Tests.InvalidAssemblyToProcess.BasicTestCases::InvalidEnsureLocalUsage2(System.Int32) at instruction IL_0002: call System.Void InlineIL.IL::EnsureLocal<System.Int32>(!!0&))"
		IL_0005: newobj instance void [netstandard]System.InvalidProgramException::.ctor(string)
		IL_000a: throw
	} // end of method BasicTestCases::InvalidEnsureLocalUsage2

	.method public hidebysig 
		instance void InvalidEnsureLocalUsage3 () cil managed 
	{
		// Header size: 1
		// Code size: 11 (0xb)
		.maxstack 8

		IL_0000: ldstr "InlineIL processing failed: Unexpected argument type, expected a non-ref local variable. (in System.Void InlineIL.Tests.InvalidAssemblyToProcess.BasicTestCases::InvalidEnsureLocalUsage3() at instruction IL_0006: call System.Void InlineIL.IL::EnsureLocal<System.Int32>(!!0&))"
		IL_0005: newobj instance void [netstandard]System.InvalidProgramException::.ctor(string)
		IL_000a: throw
	} // end of method BasicTestCases::InvalidEnsureLocalUsage3

	.method public hidebysig 
		instance void InvalidPushUsage () cil managed 
	{
		// Header size: 1
		// Code size: 11 (0xb)
		.maxstack 8

		IL_0000: ldstr "InlineIL processing failed: IL.Push cannot be used in this context, as the IL layout makes it unsafe to process. You may be able to make the IL layout safe by using IL.EnsureLocal. (in System.Void InlineIL.Tests.InvalidAssemblyToProcess.BasicTestCases::InvalidPushUsage() at instruction IL_000c: call System.Void InlineIL.IL::Push<System.Guid>(!!0))"
		IL_0005: newobj instance void [netstandard]System.InvalidProgramException::.ctor(string)
		IL_000a: throw
	} // end of method BasicTestCases::InvalidPushUsage

	.method public hidebysig 
		instance void InvalidPushUsage2 () cil managed 
	{
		// Header size: 12
		// Code size: 11 (0xb)
		.maxstack 1

		IL_0000: ldstr "InlineIL processing failed: IL.Push cannot be used in this context, as the IL layout makes it unsafe to process. You may be able to make the IL layout safe by using IL.EnsureLocal. (in System.Void InlineIL.Tests.InvalidAssemblyToProcess.BasicTestCases::InvalidPushUsage2() at instruction IL_001d: call System.Void InlineIL.IL::Push<System.Int32>(!!0))"
		IL_0005: newobj instance void [netstandard]System.InvalidProgramException::.ctor(string)
		IL_000a: throw
	} // end of method BasicTestCases::InvalidPushUsage2

	.method public hidebysig 
		instance void InvalidPushUsage3 () cil managed 
	{
		// Header size: 1
		// Code size: 11 (0xb)
		.maxstack 8

		IL_0000: ldstr "InlineIL processing failed: IL.Push cannot be used in this context, as the IL layout makes it unsafe to process. You may be able to make the IL layout safe by using IL.EnsureLocal. (in System.Void InlineIL.Tests.InvalidAssemblyToProcess.BasicTestCases::InvalidPushUsage3() at instruction IL_0018: call System.Void InlineIL.IL::Push<System.Int32[]>(!!0))"
		IL_0005: newobj instance void [netstandard]System.InvalidProgramException::.ctor(string)
		IL_000a: throw
	} // end of method BasicTestCases::InvalidPushUsage3

	.method public hidebysig 
		instance void InvalidPushUsage4 () cil managed 
	{
		// Header size: 1
		// Code size: 11 (0xb)
		.maxstack 8

		IL_0000: ldstr "InlineIL processing failed: IL.Push cannot be used in this context, as the IL layout makes it unsafe to process. You may be able to make the IL layout safe by using IL.EnsureLocal. (in System.Void InlineIL.Tests.InvalidAssemblyToProcess.BasicTestCases::InvalidPushUsage4() at instruction IL_0017: call System.Void InlineIL.IL::Push<System.Int32[]>(!!0))"
		IL_0005: newobj instance void [netstandard]System.InvalidProgramException::.ctor(string)
		IL_000a: throw
	} // end of method BasicTestCases::InvalidPushUsage4

	.method public hidebysig 
		instance void InvalidReturn () cil managed 
	{
		// Header size: 1
		// Code size: 11 (0xb)
		.maxstack 8

		IL_0000: ldstr "InlineIL processing failed: The result of the IL.Return method should be immediately returned: return IL.Return<T>(); (in System.Void InlineIL.Tests.InvalidAssemblyToProcess.BasicTestCases::InvalidReturn() at instruction IL_0000: call !!0 InlineIL.IL::Return<System.Int32>())"
		IL_0005: newobj instance void [netstandard]System.InvalidProgramException::.ctor(string)
		IL_000a: throw
	} // end of method BasicTestCases::InvalidReturn

	.method public hidebysig 
		instance void InvalidUnreachable () cil managed 
	{
		// Header size: 1
		// Code size: 11 (0xb)
		.maxstack 8

		IL_0000: ldstr "InlineIL processing failed: The result of the IL.Unreachable method should be immediately thrown: throw IL.Unreachable(); (in System.Void InlineIL.Tests.InvalidAssemblyToProcess.BasicTestCases::InvalidUnreachable() at instruction IL_0000: call System.Exception InlineIL.IL::Unreachable())"
		IL_0005: newobj instance void [netstandard]System.InvalidProgramException::.ctor(string)
		IL_000a: throw
	} // end of method BasicTestCases::InvalidUnreachable

	.method public hidebysig 
		instance void NonExistingParameter () cil managed 
	{
		// Header size: 1
		// Code size: 11 (0xb)
		.maxstack 8

		IL_0000: ldstr "InlineIL processing failed: Parameter 'foo' is not defined (in System.Void InlineIL.Tests.InvalidAssemblyToProcess.BasicTestCases::NonExistingParameter() at instruction IL_0000: ldstr \"foo\")"
		IL_0005: newobj instance void [netstandard]System.InvalidProgramException::.ctor(string)
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

		IL_0000: ldstr "InlineIL processing failed: An unconditional expression was expected. (in System.Void InlineIL.Tests.InvalidAssemblyToProcess.BasicTestCases::NotSameBasicBlock(System.Boolean) at instruction IL_0007: ldc.i4 42)"
		IL_0005: newobj instance void [netstandard]System.InvalidProgramException::.ctor(string)
		IL_000a: throw
	} // end of method BasicTestCases::NotSameBasicBlock

	.method public hidebysig 
		instance void NotSameBasicBlock2 () cil managed 
	{
		// Header size: 1
		// Code size: 11 (0xb)
		.maxstack 8

		IL_0000: ldstr "InlineIL processing failed: An unconditional expression was expected. (in System.Void InlineIL.Tests.InvalidAssemblyToProcess.BasicTestCases::NotSameBasicBlock2() at instruction IL_0031: call InlineIL.MethodRef InlineIL.MethodRef::Constructor(InlineIL.TypeRef,InlineIL.TypeRef[]))"
		IL_0005: newobj instance void [netstandard]System.InvalidProgramException::.ctor(string)
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

		IL_0000: ldstr "InlineIL processing failed: An unconditional expression was expected. (in System.Void InlineIL.Tests.InvalidAssemblyToProcess.BasicTestCases::NotSameBasicBlockArray(System.Boolean) at instruction IL_0006: ldc.i4 1)"
		IL_0005: newobj instance void [netstandard]System.InvalidProgramException::.ctor(string)
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

		IL_0000: ldstr "InlineIL processing failed: Could not locate call argument due to IL_0010: br IL_0017 (in System.Void InlineIL.Tests.InvalidAssemblyToProcess.BasicTestCases::NotSameBasicBlockArray2(System.Boolean) at instruction IL_0017: stelem.ref)"
		IL_0005: newobj instance void [netstandard]System.InvalidProgramException::.ctor(string)
		IL_000a: throw
	} // end of method BasicTestCases::NotSameBasicBlockArray2

	.method public hidebysig 
		instance void PopToArray () cil managed 
	{
		// Header size: 1
		// Code size: 11 (0xb)
		.maxstack 8

		IL_0000: ldstr "InlineIL processing failed: IL.Pop does not support array references. Emit a stelem instruction instead. (in System.Void InlineIL.Tests.InvalidAssemblyToProcess.BasicTestCases::PopToArray() at instruction IL_000c: call System.Void InlineIL.IL::Pop<System.Int32>(!!0&))"
		IL_0005: newobj instance void [netstandard]System.InvalidProgramException::.ctor(string)
		IL_000a: throw
	} // end of method BasicTestCases::PopToArray

	.method public hidebysig 
		instance void PopToField () cil managed 
	{
		// Header size: 1
		// Code size: 11 (0xb)
		.maxstack 8

		IL_0000: ldstr "InlineIL processing failed: IL.Pop does not support instance field references. Emit a stfld instruction instead. (in System.Void InlineIL.Tests.InvalidAssemblyToProcess.BasicTestCases::PopToField() at instruction IL_0006: call System.Void InlineIL.IL::Pop<System.Int32>(!!0&))"
		IL_0005: newobj instance void [netstandard]System.InvalidProgramException::.ctor(string)
		IL_000a: throw
	} // end of method BasicTestCases::PopToField

	.method public hidebysig 
		instance void UnusedInstance () cil managed 
	{
		// Header size: 1
		// Code size: 11 (0xb)
		.maxstack 8

		IL_0000: ldstr "InlineIL processing failed: Unconsumed reference to InlineIL (in System.Void InlineIL.Tests.InvalidAssemblyToProcess.BasicTestCases::UnusedInstance() at instruction IL_0000: ldtoken InlineIL.IL)"
		IL_0005: newobj instance void [netstandard]System.InvalidProgramException::.ctor(string)
		IL_000a: throw
	} // end of method BasicTestCases::UnusedInstance

} // end of class InlineIL.Tests.InvalidAssemblyToProcess.BasicTestCases
