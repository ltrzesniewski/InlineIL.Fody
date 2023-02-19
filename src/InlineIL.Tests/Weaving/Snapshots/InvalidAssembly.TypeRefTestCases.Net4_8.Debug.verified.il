.class public auto ansi beforefieldinit InlineIL.Tests.InvalidAssemblyToProcess.TypeRefTestCases
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
	} // end of method TypeRefTestCases::.ctor

	.method public hidebysig 
		instance void ArrayOfByRef () cil managed 
	{
		// Header size: 1
		// Code size: 11 (0xb)
		.maxstack 8

		IL_0000: ldstr "InlineIL processing failed: Cannot make an array of a ByRef type (in System.Void InlineIL.Tests.InvalidAssemblyToProcess.TypeRefTestCases::ArrayOfByRef() at instruction IL_001a: call System.Void InlineIL.IL/Emit::Ldtoken(InlineIL.TypeRef))"
		IL_0005: newobj instance void [mscorlib]System.InvalidProgramException::.ctor(string)
		IL_000a: throw
	} // end of method TypeRefTestCases::ArrayOfByRef

	.method public hidebysig 
		instance void ArrayOfTypedReference () cil managed 
	{
		// Header size: 1
		// Code size: 11 (0xb)
		.maxstack 8

		IL_0000: ldstr "InlineIL processing failed: Cannot create an array, pointer or ByRef to TypedReference (in System.Void InlineIL.Tests.InvalidAssemblyToProcess.TypeRefTestCases::ArrayOfTypedReference() at instruction IL_0015: call System.Void InlineIL.IL/Emit::Ldtoken(InlineIL.TypeRef))"
		IL_0005: newobj instance void [mscorlib]System.InvalidProgramException::.ctor(string)
		IL_000a: throw
	} // end of method TypeRefTestCases::ArrayOfTypedReference

	.method public hidebysig 
		instance void ByRefOfByRef () cil managed 
	{
		// Header size: 1
		// Code size: 11 (0xb)
		.maxstack 8

		IL_0000: ldstr "InlineIL processing failed: Type is already a ByRef type (in System.Void InlineIL.Tests.InvalidAssemblyToProcess.TypeRefTestCases::ByRefOfByRef() at instruction IL_001a: call System.Void InlineIL.IL/Emit::Ldtoken(InlineIL.TypeRef))"
		IL_0005: newobj instance void [mscorlib]System.InvalidProgramException::.ctor(string)
		IL_000a: throw
	} // end of method TypeRefTestCases::ByRefOfByRef

	.method public hidebysig 
		instance void ByRefOfTypedReference () cil managed 
	{
		// Header size: 1
		// Code size: 11 (0xb)
		.maxstack 8

		IL_0000: ldstr "InlineIL processing failed: Cannot create an array, pointer or ByRef to TypedReference (in System.Void InlineIL.Tests.InvalidAssemblyToProcess.TypeRefTestCases::ByRefOfTypedReference() at instruction IL_0015: call System.Void InlineIL.IL/Emit::Ldtoken(InlineIL.TypeRef))"
		IL_0005: newobj instance void [mscorlib]System.InvalidProgramException::.ctor(string)
		IL_000a: throw
	} // end of method TypeRefTestCases::ByRefOfTypedReference

	.method public hidebysig 
		instance void GenericOfByRef () cil managed 
	{
		// Header size: 1
		// Code size: 11 (0xb)
		.maxstack 8

		IL_0000: ldstr "InlineIL processing failed: Cannot make a generic instance of a ByRef, pointer or array type (in System.Void InlineIL.Tests.InvalidAssemblyToProcess.TypeRefTestCases::GenericOfByRef() at instruction IL_002d: call System.Void InlineIL.IL/Emit::Ldtoken(InlineIL.TypeRef))"
		IL_0005: newobj instance void [mscorlib]System.InvalidProgramException::.ctor(string)
		IL_000a: throw
	} // end of method TypeRefTestCases::GenericOfByRef

	.method public hidebysig 
		instance void GenericParamsOnGenericInstance () cil managed 
	{
		// Header size: 12
		// Code size: 11 (0xb)
		.maxstack 1

		IL_0000: ldstr "InlineIL processing failed: Type is already a generic instance: System.Collections.Generic.List`1<System.Int32> (in System.Void InlineIL.Tests.InvalidAssemblyToProcess.TypeRefTestCases::GenericParamsOnGenericInstance() at instruction IL_0040: call System.Void InlineIL.IL/Emit::Ldtoken(InlineIL.TypeRef))"
		IL_0005: newobj instance void [mscorlib]System.InvalidProgramException::.ctor(string)
		IL_000a: throw
	} // end of method TypeRefTestCases::GenericParamsOnGenericInstance

	.method public hidebysig 
		instance void InvalidArrayRank () cil managed 
	{
		// Header size: 1
		// Code size: 11 (0xb)
		.maxstack 8

		IL_0000: ldstr "InlineIL processing failed: Invalid array rank: -1, must be at least 1 (in System.Void InlineIL.Tests.InvalidAssemblyToProcess.TypeRefTestCases::InvalidArrayRank() at instruction IL_0016: call System.Void InlineIL.IL/Emit::Ldtoken(InlineIL.TypeRef))"
		IL_0005: newobj instance void [mscorlib]System.InvalidProgramException::.ctor(string)
		IL_000a: throw
	} // end of method TypeRefTestCases::InvalidArrayRank

	.method public hidebysig 
		instance void InvalidAssembly () cil managed 
	{
		// Header size: 1
		// Code size: 11 (0xb)
		.maxstack 8

		IL_0000: ldstr "InlineIL processing failed: Could not resolve assembly 'AssemblyThatDoesntExist' (in System.Void InlineIL.Tests.InvalidAssemblyToProcess.TypeRefTestCases::InvalidAssembly() at instruction IL_0010: call System.Void InlineIL.IL/Emit::Ldtoken(InlineIL.TypeRef))"
		IL_0005: newobj instance void [mscorlib]System.InvalidProgramException::.ctor(string)
		IL_000a: throw
	} // end of method TypeRefTestCases::InvalidAssembly

	.method public hidebysig 
		instance void InvalidGenericArgsCount () cil managed 
	{
		// Header size: 12
		// Code size: 11 (0xb)
		.maxstack 1

		IL_0000: ldstr "InlineIL processing failed: Incorrect number of generic arguments supplied for type System.Collections.Generic.Dictionary`2 - expected 0, but got 3 (in System.Void InlineIL.Tests.InvalidAssemblyToProcess.TypeRefTestCases::InvalidGenericArgsCount() at instruction IL_0042: call System.Void InlineIL.IL/Emit::Ldtoken(InlineIL.TypeRef))"
		IL_0005: newobj instance void [mscorlib]System.InvalidProgramException::.ctor(string)
		IL_000a: throw
	} // end of method TypeRefTestCases::InvalidGenericArgsCount

	.method public hidebysig 
		instance void InvalidGenericParameterIndex () cil managed 
	{
		// Header size: 1
		// Code size: 11 (0xb)
		.maxstack 8

		IL_0000: ldstr "InlineIL processing failed: Invalid generic parameter index: -1 (in System.Void InlineIL.Tests.InvalidAssemblyToProcess.TypeRefTestCases::InvalidGenericParameterIndex() at instruction IL_000c: call System.Void InlineIL.IL/Emit::Ldtoken(InlineIL.TypeRef))"
		IL_0005: newobj instance void [mscorlib]System.InvalidProgramException::.ctor(string)
		IL_000a: throw
	} // end of method TypeRefTestCases::InvalidGenericParameterIndex

	.method public hidebysig 
		instance void InvalidType () cil managed 
	{
		// Header size: 1
		// Code size: 11 (0xb)
		.maxstack 8

		IL_0000: ldstr "InlineIL processing failed: Could not find type 'TypeThatDoesntExist' in assembly 'System' (in System.Void InlineIL.Tests.InvalidAssemblyToProcess.TypeRefTestCases::InvalidType() at instruction IL_0010: call System.Void InlineIL.IL/Emit::Ldtoken(InlineIL.TypeRef))"
		IL_0005: newobj instance void [mscorlib]System.InvalidProgramException::.ctor(string)
		IL_000a: throw
	} // end of method TypeRefTestCases::InvalidType

	.method public hidebysig 
		instance void LoadNullType () cil managed 
	{
		// Header size: 1
		// Code size: 11 (0xb)
		.maxstack 8

		IL_0000: ldstr "InlineIL processing failed: Expected a method call but was null (in System.Void InlineIL.Tests.InvalidAssemblyToProcess.TypeRefTestCases::LoadNullType() at instruction IL_0001: ldnull)"
		IL_0005: newobj instance void [mscorlib]System.InvalidProgramException::.ctor(string)
		IL_000a: throw
	} // end of method TypeRefTestCases::LoadNullType

	.method public hidebysig 
		instance void LoadNullTypeRef () cil managed 
	{
		// Header size: 1
		// Code size: 11 (0xb)
		.maxstack 8

		IL_0000: ldstr "InlineIL processing failed: Expected a method call but was null (in System.Void InlineIL.Tests.InvalidAssemblyToProcess.TypeRefTestCases::LoadNullTypeRef() at instruction IL_0001: ldnull)"
		IL_0005: newobj instance void [mscorlib]System.InvalidProgramException::.ctor(string)
		IL_000a: throw
	} // end of method TypeRefTestCases::LoadNullTypeRef

	.method public hidebysig 
		instance void MethodGenericParameter () cil managed 
	{
		// Header size: 1
		// Code size: 11 (0xb)
		.maxstack 8

		IL_0000: ldstr "InlineIL processing failed: TypeRef.MethodGenericParameters can only be used in MethodRef definitions for overload resolution (in System.Void InlineIL.Tests.InvalidAssemblyToProcess.TypeRefTestCases::MethodGenericParameter() at instruction IL_000c: call System.Void InlineIL.IL/Emit::Ldtoken(InlineIL.TypeRef))"
		IL_0005: newobj instance void [mscorlib]System.InvalidProgramException::.ctor(string)
		IL_000a: throw
	} // end of method TypeRefTestCases::MethodGenericParameter

	.method public hidebysig 
		instance void NoGenericTypeArgs () cil managed 
	{
		// Header size: 1
		// Code size: 11 (0xb)
		.maxstack 8

		IL_0000: ldstr "InlineIL processing failed: No generic arguments supplied (in System.Void InlineIL.Tests.InvalidAssemblyToProcess.TypeRefTestCases::NoGenericTypeArgs() at instruction IL_001a: call System.Void InlineIL.IL/Emit::Ldtoken(InlineIL.TypeRef))"
		IL_0005: newobj instance void [mscorlib]System.InvalidProgramException::.ctor(string)
		IL_000a: throw
	} // end of method TypeRefTestCases::NoGenericTypeArgs

	.method public hidebysig 
		instance void NotAGenericType () cil managed 
	{
		// Header size: 1
		// Code size: 11 (0xb)
		.maxstack 8

		IL_0000: ldstr "InlineIL processing failed: Not a generic type definition: System.String (in System.Void InlineIL.Tests.InvalidAssemblyToProcess.TypeRefTestCases::NotAGenericType() at instruction IL_0028: call System.Void InlineIL.IL/Emit::Ldtoken(InlineIL.TypeRef))"
		IL_0005: newobj instance void [mscorlib]System.InvalidProgramException::.ctor(string)
		IL_000a: throw
	} // end of method TypeRefTestCases::NotAGenericType

	.method public hidebysig 
		instance void PointerToByRef () cil managed 
	{
		// Header size: 1
		// Code size: 11 (0xb)
		.maxstack 8

		IL_0000: ldstr "InlineIL processing failed: Cannot make a pointer to a ByRef type (in System.Void InlineIL.Tests.InvalidAssemblyToProcess.TypeRefTestCases::PointerToByRef() at instruction IL_001a: call System.Void InlineIL.IL/Emit::Ldtoken(InlineIL.TypeRef))"
		IL_0005: newobj instance void [mscorlib]System.InvalidProgramException::.ctor(string)
		IL_000a: throw
	} // end of method TypeRefTestCases::PointerToByRef

	.method public hidebysig 
		instance void PointerToTypedReference () cil managed 
	{
		// Header size: 1
		// Code size: 11 (0xb)
		.maxstack 8

		IL_0000: ldstr "InlineIL processing failed: Cannot create an array, pointer or ByRef to TypedReference (in System.Void InlineIL.Tests.InvalidAssemblyToProcess.TypeRefTestCases::PointerToTypedReference() at instruction IL_0015: call System.Void InlineIL.IL/Emit::Ldtoken(InlineIL.TypeRef))"
		IL_0005: newobj instance void [mscorlib]System.InvalidProgramException::.ctor(string)
		IL_000a: throw
	} // end of method TypeRefTestCases::PointerToTypedReference

	.method public hidebysig 
		instance void TypeGenericParameter () cil managed 
	{
		// Header size: 1
		// Code size: 11 (0xb)
		.maxstack 8

		IL_0000: ldstr "InlineIL processing failed: TypeRef.TypeGenericParameters can only be used in MethodRef definitions for overload resolution (in System.Void InlineIL.Tests.InvalidAssemblyToProcess.TypeRefTestCases::TypeGenericParameter() at instruction IL_000c: call System.Void InlineIL.IL/Emit::Ldtoken(InlineIL.TypeRef))"
		IL_0005: newobj instance void [mscorlib]System.InvalidProgramException::.ctor(string)
		IL_000a: throw
	} // end of method TypeRefTestCases::TypeGenericParameter

	.method public hidebysig 
		instance void UnusedInstance () cil managed 
	{
		// Header size: 1
		// Code size: 11 (0xb)
		.maxstack 8

		IL_0000: ldstr "InlineIL processing failed: Unconsumed reference to InlineIL (in System.Void InlineIL.Tests.InvalidAssemblyToProcess.TypeRefTestCases::UnusedInstance() at instruction IL_000b: newobj System.Void InlineIL.TypeRef::.ctor(System.Type))"
		IL_0005: newobj instance void [mscorlib]System.InvalidProgramException::.ctor(string)
		IL_000a: throw
	} // end of method TypeRefTestCases::UnusedInstance

} // end of class InlineIL.Tests.InvalidAssemblyToProcess.TypeRefTestCases
