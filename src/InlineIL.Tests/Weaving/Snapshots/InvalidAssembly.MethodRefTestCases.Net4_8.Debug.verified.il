.class public auto ansi beforefieldinit InlineIL.Tests.InvalidAssemblyToProcess.MethodRefTestCases
	extends [mscorlib]System.Object
{
	.custom instance void [InlineIL.Tests.Common]InlineIL.Tests.Common.TestCasesAttribute::.ctor() = (
		01 00 00 00
	)
	// Nested Types
	.class nested private auto ansi sealed serializable beforefieldinit '<>c'
		extends [mscorlib]System.Object
	{
		.custom instance void [mscorlib]System.Runtime.CompilerServices.CompilerGeneratedAttribute::.ctor() = (
			01 00 00 00
		)
		// Fields
		.field public static initonly class InlineIL.Tests.InvalidAssemblyToProcess.MethodRefTestCases/'<>c' '<>9'
		.field public static class [mscorlib]System.Func`2<int32, int32> '<>9__39_0'
		.field public static class [mscorlib]System.Func`2<int32, int32> '<>9__43_0'

		// Methods
		.method private hidebysig specialname rtspecialname static 
			void .cctor () cil managed 
		{
			// Header size: 1
			// Code size: 11 (0xb)
			.maxstack 8

			IL_0000: newobj instance void InlineIL.Tests.InvalidAssemblyToProcess.MethodRefTestCases/'<>c'::.ctor()
			IL_0005: stsfld class InlineIL.Tests.InvalidAssemblyToProcess.MethodRefTestCases/'<>c' InlineIL.Tests.InvalidAssemblyToProcess.MethodRefTestCases/'<>c'::'<>9'
			IL_000a: ret
		} // end of method '<>c'::.cctor

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
		} // end of method '<>c'::.ctor

		.method assembly hidebysig 
			instance int32 '<StaticDelegateFromDelegate>b__43_0' (
				int32 i
			) cil managed 
		{
			// Header size: 12
			// Code size: 9 (0x9)
			.maxstack 2
			.locals init (
				[0] int32
			)

			IL_0000: nop
			IL_0001: ldarg.1
			IL_0002: ldc.i4.2
			IL_0003: mul
			IL_0004: stloc.0
			IL_0005: br.s IL_0007

			IL_0007: ldloc.0
			IL_0008: ret
		} // end of method '<>c'::'<StaticDelegateFromDelegate>b__43_0'

		.method assembly hidebysig 
			instance int32 '<StaticLambdaFromDelegate>b__39_0' (
				int32 i
			) cil managed 
		{
			// Header size: 1
			// Code size: 4 (0x4)
			.maxstack 8

			IL_0000: ldarg.1
			IL_0001: ldc.i4.2
			IL_0002: mul
			IL_0003: ret
		} // end of method '<>c'::'<StaticLambdaFromDelegate>b__39_0'

	} // end of class <>c

	.class nested private auto ansi sealed beforefieldinit '<>c__DisplayClass40_0'
		extends [mscorlib]System.Object
	{
		.custom instance void [mscorlib]System.Runtime.CompilerServices.CompilerGeneratedAttribute::.ctor() = (
			01 00 00 00
		)
		// Fields
		.field public int32 i

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
		} // end of method '<>c__DisplayClass40_0'::.ctor

		.method assembly hidebysig 
			instance int32 '<NonStaticLambdaFromDelegate>b__0' () cil managed 
		{
			// Header size: 1
			// Code size: 9 (0x9)
			.maxstack 8

			IL_0000: ldarg.0
			IL_0001: ldfld int32 InlineIL.Tests.InvalidAssemblyToProcess.MethodRefTestCases/'<>c__DisplayClass40_0'::i
			IL_0006: ldc.i4.2
			IL_0007: mul
			IL_0008: ret
		} // end of method '<>c__DisplayClass40_0'::'<NonStaticLambdaFromDelegate>b__0'

	} // end of class <>c__DisplayClass40_0

	.class nested private auto ansi sealed beforefieldinit '<>c__DisplayClass42_0'
		extends [mscorlib]System.Object
	{
		.custom instance void [mscorlib]System.Runtime.CompilerServices.CompilerGeneratedAttribute::.ctor() = (
			01 00 00 00
		)
		// Fields
		.field public int32 i

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
		} // end of method '<>c__DisplayClass42_0'::.ctor

		.method assembly hidebysig 
			instance int32 '<NonStaticLocalFunctionFromDelegate>g__LocalFunction|0' () cil managed 
		{
			// Header size: 1
			// Code size: 9 (0x9)
			.maxstack 8

			IL_0000: ldarg.0
			IL_0001: ldfld int32 InlineIL.Tests.InvalidAssemblyToProcess.MethodRefTestCases/'<>c__DisplayClass42_0'::i
			IL_0006: ldc.i4.2
			IL_0007: mul
			IL_0008: ret
		} // end of method '<>c__DisplayClass42_0'::'<NonStaticLocalFunctionFromDelegate>g__LocalFunction|0'

	} // end of class <>c__DisplayClass42_0

	.class nested private auto ansi sealed beforefieldinit '<>c__DisplayClass44_0'
		extends [mscorlib]System.Object
	{
		.custom instance void [mscorlib]System.Runtime.CompilerServices.CompilerGeneratedAttribute::.ctor() = (
			01 00 00 00
		)
		// Fields
		.field public int32 i

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
		} // end of method '<>c__DisplayClass44_0'::.ctor

		.method assembly hidebysig 
			instance int32 '<NonStaticDelegateFromDelegate>b__0' () cil managed 
		{
			// Header size: 12
			// Code size: 14 (0xe)
			.maxstack 2
			.locals init (
				[0] int32
			)

			IL_0000: nop
			IL_0001: ldarg.0
			IL_0002: ldfld int32 InlineIL.Tests.InvalidAssemblyToProcess.MethodRefTestCases/'<>c__DisplayClass44_0'::i
			IL_0007: ldc.i4.2
			IL_0008: mul
			IL_0009: stloc.0
			IL_000a: br.s IL_000c

			IL_000c: ldloc.0
			IL_000d: ret
		} // end of method '<>c__DisplayClass44_0'::'<NonStaticDelegateFromDelegate>b__0'

	} // end of class <>c__DisplayClass44_0

	.class nested private auto ansi abstract sealed beforefieldinit '<>O'
		extends [mscorlib]System.Object
	{
		.custom instance void [mscorlib]System.Runtime.CompilerServices.CompilerGeneratedAttribute::.ctor() = (
			01 00 00 00
		)
		// Fields
		.field public static class [mscorlib]System.Func`2<int32, int32> '<0>__LocalFunction'

	} // end of class <>O

	.class nested private auto ansi beforefieldinit ClassWithNoDefaultConstructor
		extends [mscorlib]System.Object
	{
		// Methods
		.method public hidebysig specialname rtspecialname 
			instance void .ctor (
				int32 foo
			) cil managed 
		{
			// Header size: 1
			// Code size: 9 (0x9)
			.maxstack 8

			IL_0000: ldarg.0
			IL_0001: call instance void [mscorlib]System.Object::.ctor()
			IL_0006: nop
			IL_0007: nop
			IL_0008: ret
		} // end of method ClassWithNoDefaultConstructor::.ctor

	} // end of class ClassWithNoDefaultConstructor

	.class nested private auto ansi beforefieldinit ClassWithoutInitializer
		extends [mscorlib]System.Object
	{
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
		} // end of method ClassWithoutInitializer::.ctor

	} // end of class ClassWithoutInitializer

	.class nested private auto ansi beforefieldinit GenericType`1<T>
		extends [mscorlib]System.Object
	{
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
		} // end of method GenericType`1::.ctor

		.method public hidebysig static 
			void Method (
				!T arg
			) cil managed 
		{
			// Header size: 1
			// Code size: 2 (0x2)
			.maxstack 8

			IL_0000: nop
			IL_0001: ret
		} // end of method GenericType`1::Method

	} // end of class GenericType`1


	// Fields
	.field private int32 '<Value>k__BackingField'
	.custom instance void [mscorlib]System.Diagnostics.DebuggerBrowsableAttribute::.ctor(valuetype [mscorlib]System.Diagnostics.DebuggerBrowsableState) = (
		01 00 00 00 00 00 00 00
	)
	.custom instance void [mscorlib]System.Runtime.CompilerServices.CompilerGeneratedAttribute::.ctor() = (
		01 00 00 00
	)
	.field private class [mscorlib]System.Action Event
	.custom instance void [mscorlib]System.Diagnostics.DebuggerBrowsableAttribute::.ctor(valuetype [mscorlib]System.Diagnostics.DebuggerBrowsableState) = (
		01 00 00 00 00 00 00 00
	)
	.custom instance void [mscorlib]System.Runtime.CompilerServices.CompilerGeneratedAttribute::.ctor() = (
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
	} // end of method MethodRefTestCases::.ctor

	.method assembly hidebysig static 
		int32 '<StaticLocalFunctionFromDelegate>g__LocalFunction|41_0' (
			int32 i
		) cil managed 
	{
		.custom instance void [mscorlib]System.Runtime.CompilerServices.CompilerGeneratedAttribute::.ctor() = (
			01 00 00 00
		)
		// Header size: 1
		// Code size: 4 (0x4)
		.maxstack 8

		IL_0000: ldarg.0
		IL_0001: ldc.i4.2
		IL_0002: mul
		IL_0003: ret
	} // end of method MethodRefTestCases::'<StaticLocalFunctionFromDelegate>g__LocalFunction|41_0'

	.method public hidebysig specialname 
		instance void add_Event (
			class [mscorlib]System.Action 'value'
		) cil managed 
	{
		.custom instance void [mscorlib]System.Runtime.CompilerServices.CompilerGeneratedAttribute::.ctor() = (
			01 00 00 00
		)
		// Header size: 12
		// Code size: 41 (0x29)
		.maxstack 3
		.locals init (
			[0] class [mscorlib]System.Action,
			[1] class [mscorlib]System.Action,
			[2] class [mscorlib]System.Action
		)

		IL_0000: ldarg.0
		IL_0001: ldfld class [mscorlib]System.Action InlineIL.Tests.InvalidAssemblyToProcess.MethodRefTestCases::Event
		IL_0006: stloc.0
		// loop start (head: IL_0007)
			IL_0007: ldloc.0
			IL_0008: stloc.1
			IL_0009: ldloc.1
			IL_000a: ldarg.1
			IL_000b: call class [mscorlib]System.Delegate [mscorlib]System.Delegate::Combine(class [mscorlib]System.Delegate, class [mscorlib]System.Delegate)
			IL_0010: castclass [mscorlib]System.Action
			IL_0015: stloc.2
			IL_0016: ldarg.0
			IL_0017: ldflda class [mscorlib]System.Action InlineIL.Tests.InvalidAssemblyToProcess.MethodRefTestCases::Event
			IL_001c: ldloc.2
			IL_001d: ldloc.1
			IL_001e: call !!0 [mscorlib]System.Threading.Interlocked::CompareExchange<class [mscorlib]System.Action>(!!0&, !!0, !!0)
			IL_0023: stloc.0
			IL_0024: ldloc.0
			IL_0025: ldloc.1
			IL_0026: bne.un.s IL_0007
		// end loop
		IL_0028: ret
	} // end of method MethodRefTestCases::add_Event

	.method public hidebysig 
		instance void AmbiguousMethod () cil managed 
	{
		// Header size: 1
		// Code size: 11 (0xb)
		.maxstack 8

		IL_0000: ldstr "InlineIL processing failed: Ambiguous method 'Foo' in type InlineIL.Tests.InvalidAssemblyToProcess.MethodRefTestCases (in System.Void InlineIL.Tests.InvalidAssemblyToProcess.MethodRefTestCases::AmbiguousMethod() at instruction IL_001a: call System.Void InlineIL.IL/Emit::Call(InlineIL.MethodRef))"
		IL_0005: newobj instance void [mscorlib]System.InvalidProgramException::.ctor(string)
		IL_000a: throw
	} // end of method MethodRefTestCases::AmbiguousMethod

	.method public hidebysig 
		instance void EmptyVarArgParams () cil managed 
	{
		// Header size: 1
		// Code size: 11 (0xb)
		.maxstack 8

		IL_0000: ldstr "InlineIL processing failed: No optional parameter type supplied for vararg method call (in System.Void InlineIL.Tests.InvalidAssemblyToProcess.MethodRefTestCases::EmptyVarArgParams() at instruction IL_0024: call System.Void InlineIL.IL/Emit::Call(InlineIL.MethodRef))"
		IL_0005: newobj instance void [mscorlib]System.InvalidProgramException::.ctor(string)
		IL_000a: throw
	} // end of method MethodRefTestCases::EmptyVarArgParams

	.method public hidebysig 
		instance void EventWithoutInvoker () cil managed 
	{
		// Header size: 1
		// Code size: 11 (0xb)
		.maxstack 8

		IL_0000: ldstr "InlineIL processing failed: Event 'Event' in type InlineIL.Tests.InvalidAssemblyToProcess.MethodRefTestCases has no raise method (in System.Void InlineIL.Tests.InvalidAssemblyToProcess.MethodRefTestCases::EventWithoutInvoker() at instruction IL_001a: call System.Void InlineIL.IL/Emit::Call(InlineIL.MethodRef))"
		IL_0005: newobj instance void [mscorlib]System.InvalidProgramException::.ctor(string)
		IL_000a: throw
	} // end of method MethodRefTestCases::EventWithoutInvoker

	.method private hidebysig static 
		void Foo () cil managed 
	{
		// Header size: 1
		// Code size: 2 (0x2)
		.maxstack 8

		IL_0000: nop
		IL_0001: ret
	} // end of method MethodRefTestCases::Foo

	.method private hidebysig static 
		void Foo (
			int32 i
		) cil managed 
	{
		// Header size: 1
		// Code size: 2 (0x2)
		.maxstack 8

		IL_0000: nop
		IL_0001: ret
	} // end of method MethodRefTestCases::Foo

	.method private hidebysig static 
		!!T GenericMethod<T> (
			!!T 'value'
		) cil managed 
	{
		// Header size: 1
		// Code size: 2 (0x2)
		.maxstack 8

		IL_0000: ldarg.0
		IL_0001: ret
	} // end of method MethodRefTestCases::GenericMethod

	.method public hidebysig specialname 
		instance int32 get_Value () cil managed 
	{
		.custom instance void [mscorlib]System.Runtime.CompilerServices.CompilerGeneratedAttribute::.ctor() = (
			01 00 00 00
		)
		// Header size: 1
		// Code size: 7 (0x7)
		.maxstack 8

		IL_0000: ldarg.0
		IL_0001: ldfld int32 InlineIL.Tests.InvalidAssemblyToProcess.MethodRefTestCases::'<Value>k__BackingField'
		IL_0006: ret
	} // end of method MethodRefTestCases::get_Value

	.method public hidebysig specialname 
		instance int32 get_ValueGetOnly () cil managed 
	{
		// Header size: 1
		// Code size: 7 (0x7)
		.maxstack 8

		IL_0000: ldarg.0
		IL_0001: call instance int32 InlineIL.Tests.InvalidAssemblyToProcess.MethodRefTestCases::get_Value()
		IL_0006: ret
	} // end of method MethodRefTestCases::get_ValueGetOnly

	.method public hidebysig 
		instance void InvalidBinaryOperator () cil managed 
	{
		// Header size: 1
		// Code size: 11 (0xb)
		.maxstack 8

		IL_0000: ldstr "InlineIL processing failed: Binary operator op_Addition(InlineIL.Tests.InvalidAssemblyToProcess.MethodRefTestCases/ClassWithoutInitializer, System.Int32) not found in type InlineIL.Tests.InvalidAssemblyToProcess.MethodRefTestCases/ClassWithoutInitializer (in System.Void InlineIL.Tests.InvalidAssemblyToProcess.MethodRefTestCases::InvalidBinaryOperator() at instruction IL_0034: call System.Void InlineIL.IL/Emit::Call(InlineIL.MethodRef))"
		IL_0005: newobj instance void [mscorlib]System.InvalidProgramException::.ctor(string)
		IL_000a: throw
	} // end of method MethodRefTestCases::InvalidBinaryOperator

	.method public hidebysig 
		instance void InvalidConversionOperator () cil managed 
	{
		// Header size: 1
		// Code size: 11 (0xb)
		.maxstack 8

		IL_0000: ldstr "InlineIL processing failed: Conversion operator op_Implicit from System.Int32 not found in type InlineIL.Tests.InvalidAssemblyToProcess.MethodRefTestCases/ClassWithoutInitializer (in System.Void InlineIL.Tests.InvalidAssemblyToProcess.MethodRefTestCases::InvalidConversionOperator() at instruction IL_0026: call System.Void InlineIL.IL/Emit::Call(InlineIL.MethodRef))"
		IL_0005: newobj instance void [mscorlib]System.InvalidProgramException::.ctor(string)
		IL_000a: throw
	} // end of method MethodRefTestCases::InvalidConversionOperator

	.method public hidebysig 
		instance void InvalidEnumValue () cil managed 
	{
		// Header size: 1
		// Code size: 11 (0xb)
		.maxstack 8

		IL_0000: ldstr "InlineIL processing failed: Invalid enum value for UnaryOperator (in System.Void InlineIL.Tests.InvalidAssemblyToProcess.MethodRefTestCases::InvalidEnumValue() at instruction IL_0010: ldc.i4 -1)"
		IL_0005: newobj instance void [mscorlib]System.InvalidProgramException::.ctor(string)
		IL_000a: throw
	} // end of method MethodRefTestCases::InvalidEnumValue

	.method public hidebysig 
		instance void InvalidUnaryOperator () cil managed 
	{
		// Header size: 1
		// Code size: 11 (0xb)
		.maxstack 8

		IL_0000: ldstr "InlineIL processing failed: Unary operator op_Decrement not found in type InlineIL.Tests.InvalidAssemblyToProcess.MethodRefTestCases/ClassWithoutInitializer (in System.Void InlineIL.Tests.InvalidAssemblyToProcess.MethodRefTestCases::InvalidUnaryOperator() at instruction IL_0016: call System.Void InlineIL.IL/Emit::Call(InlineIL.MethodRef))"
		IL_0005: newobj instance void [mscorlib]System.InvalidProgramException::.ctor(string)
		IL_000a: throw
	} // end of method MethodRefTestCases::InvalidUnaryOperator

	.method public hidebysig 
		instance void MethodGenericArgOutOfBounds () cil managed 
	{
		// Header size: 12
		// Code size: 11 (0xb)
		.maxstack 1

		IL_0000: ldstr "InlineIL processing failed: Method GenericMethod(!!42) not found in type InlineIL.Tests.InvalidAssemblyToProcess.MethodRefTestCases (in System.Void InlineIL.Tests.InvalidAssemblyToProcess.MethodRefTestCases::MethodGenericArgOutOfBounds() at instruction IL_004c: call System.Void InlineIL.IL/Emit::Call(InlineIL.MethodRef))"
		IL_0005: newobj instance void [mscorlib]System.InvalidProgramException::.ctor(string)
		IL_000a: throw
	} // end of method MethodRefTestCases::MethodGenericArgOutOfBounds

	.method public hidebysig 
		instance void NoDefaultConstructor () cil managed 
	{
		// Header size: 1
		// Code size: 11 (0xb)
		.maxstack 8

		IL_0000: ldstr "InlineIL processing failed: Type InlineIL.Tests.InvalidAssemblyToProcess.MethodRefTestCases/ClassWithNoDefaultConstructor has no default constructor (in System.Void InlineIL.Tests.InvalidAssemblyToProcess.MethodRefTestCases::NoDefaultConstructor() at instruction IL_001a: call System.Void InlineIL.IL/Emit::Call(InlineIL.MethodRef))"
		IL_0005: newobj instance void [mscorlib]System.InvalidProgramException::.ctor(string)
		IL_000a: throw
	} // end of method MethodRefTestCases::NoDefaultConstructor

	.method public hidebysig 
		instance void NoGenericArgsProvided () cil managed 
	{
		// Header size: 1
		// Code size: 11 (0xb)
		.maxstack 8

		IL_0000: ldstr "InlineIL processing failed: No generic arguments supplied (in System.Void InlineIL.Tests.InvalidAssemblyToProcess.MethodRefTestCases::NoGenericArgsProvided() at instruction IL_0024: call System.Void InlineIL.IL/Emit::Call(InlineIL.MethodRef))"
		IL_0005: newobj instance void [mscorlib]System.InvalidProgramException::.ctor(string)
		IL_000a: throw
	} // end of method MethodRefTestCases::NoGenericArgsProvided

	.method public hidebysig 
		instance void NoMatchingGenericOverload () cil managed 
	{
		// Header size: 12
		// Code size: 11 (0xb)
		.maxstack 1

		IL_0000: ldstr "InlineIL processing failed: Method GenericMethod(System.String) not found in type InlineIL.Tests.InvalidAssemblyToProcess.MethodRefTestCases (in System.Void InlineIL.Tests.InvalidAssemblyToProcess.MethodRefTestCases::NoMatchingGenericOverload() at instruction IL_004f: call System.Void InlineIL.IL/Emit::Call(InlineIL.MethodRef))"
		IL_0005: newobj instance void [mscorlib]System.InvalidProgramException::.ctor(string)
		IL_000a: throw
	} // end of method MethodRefTestCases::NoMatchingGenericOverload

	.method public hidebysig 
		instance void NoMatchingGenericOverloadArray () cil managed 
	{
		// Header size: 12
		// Code size: 11 (0xb)
		.maxstack 1

		IL_0000: ldstr "InlineIL processing failed: Method GenericMethod(System.String[]) not found in type InlineIL.Tests.InvalidAssemblyToProcess.MethodRefTestCases (in System.Void InlineIL.Tests.InvalidAssemblyToProcess.MethodRefTestCases::NoMatchingGenericOverloadArray() at instruction IL_0054: call System.Void InlineIL.IL/Emit::Call(InlineIL.MethodRef))"
		IL_0005: newobj instance void [mscorlib]System.InvalidProgramException::.ctor(string)
		IL_000a: throw
	} // end of method MethodRefTestCases::NoMatchingGenericOverloadArray

	.method public hidebysig 
		instance void NoMatchingGenericOverloadArray2 () cil managed 
	{
		// Header size: 12
		// Code size: 11 (0xb)
		.maxstack 1

		IL_0000: ldstr "InlineIL processing failed: Method GenericMethod(System.String[,]) not found in type InlineIL.Tests.InvalidAssemblyToProcess.MethodRefTestCases (in System.Void InlineIL.Tests.InvalidAssemblyToProcess.MethodRefTestCases::NoMatchingGenericOverloadArray2() at instruction IL_0055: call System.Void InlineIL.IL/Emit::Call(InlineIL.MethodRef))"
		IL_0005: newobj instance void [mscorlib]System.InvalidProgramException::.ctor(string)
		IL_000a: throw
	} // end of method MethodRefTestCases::NoMatchingGenericOverloadArray2

	.method public hidebysig 
		instance void NoMatchingGenericOverloadByRef () cil managed 
	{
		// Header size: 12
		// Code size: 11 (0xb)
		.maxstack 1

		IL_0000: ldstr "InlineIL processing failed: Method GenericMethod(System.String&) not found in type InlineIL.Tests.InvalidAssemblyToProcess.MethodRefTestCases (in System.Void InlineIL.Tests.InvalidAssemblyToProcess.MethodRefTestCases::NoMatchingGenericOverloadByRef() at instruction IL_0054: call System.Void InlineIL.IL/Emit::Call(InlineIL.MethodRef))"
		IL_0005: newobj instance void [mscorlib]System.InvalidProgramException::.ctor(string)
		IL_000a: throw
	} // end of method MethodRefTestCases::NoMatchingGenericOverloadByRef

	.method public hidebysig 
		instance void NoMatchingGenericOverloadGeneric () cil managed 
	{
		// Header size: 12
		// Code size: 11 (0xb)
		.maxstack 1

		IL_0000: ldstr "InlineIL processing failed: Method GenericMethod(System.Collections.Generic.Dictionary`2<System.Int32,System.String>) not found in type InlineIL.Tests.InvalidAssemblyToProcess.MethodRefTestCases (in System.Void InlineIL.Tests.InvalidAssemblyToProcess.MethodRefTestCases::NoMatchingGenericOverloadGeneric() at instruction IL_0074: call System.Void InlineIL.IL/Emit::Call(InlineIL.MethodRef))"
		IL_0005: newobj instance void [mscorlib]System.InvalidProgramException::.ctor(string)
		IL_000a: throw
	} // end of method MethodRefTestCases::NoMatchingGenericOverloadGeneric

	.method public hidebysig 
		instance void NoMatchingGenericOverloadPointer () cil managed 
	{
		// Header size: 12
		// Code size: 11 (0xb)
		.maxstack 1

		IL_0000: ldstr "InlineIL processing failed: Method GenericMethod(System.Int32*) not found in type InlineIL.Tests.InvalidAssemblyToProcess.MethodRefTestCases (in System.Void InlineIL.Tests.InvalidAssemblyToProcess.MethodRefTestCases::NoMatchingGenericOverloadPointer() at instruction IL_0054: call System.Void InlineIL.IL/Emit::Call(InlineIL.MethodRef))"
		IL_0005: newobj instance void [mscorlib]System.InvalidProgramException::.ctor(string)
		IL_000a: throw
	} // end of method MethodRefTestCases::NoMatchingGenericOverloadPointer

	.method public hidebysig 
		instance void NonStaticDelegateFromDelegate (
			int32 i
		) cil managed 
	{
		// Header size: 12
		// Code size: 11 (0xb)
		.maxstack 1

		IL_0000: ldstr "InlineIL processing failed: A compiler-generated method is referenced by the delegate (in System.Void InlineIL.Tests.InvalidAssemblyToProcess.MethodRefTestCases::NonStaticDelegateFromDelegate(System.Int32) at instruction IL_001f: call System.Void InlineIL.IL/Emit::Call(InlineIL.MethodRef))"
		IL_0005: newobj instance void [mscorlib]System.InvalidProgramException::.ctor(string)
		IL_000a: throw
	} // end of method MethodRefTestCases::NonStaticDelegateFromDelegate

	.method public hidebysig 
		instance void NonStaticLambdaFromDelegate (
			int32 i
		) cil managed 
	{
		// Header size: 12
		// Code size: 11 (0xb)
		.maxstack 1

		IL_0000: ldstr "InlineIL processing failed: A compiler-generated method is referenced by the delegate (in System.Void InlineIL.Tests.InvalidAssemblyToProcess.MethodRefTestCases::NonStaticLambdaFromDelegate(System.Int32) at instruction IL_001f: call System.Void InlineIL.IL/Emit::Call(InlineIL.MethodRef))"
		IL_0005: newobj instance void [mscorlib]System.InvalidProgramException::.ctor(string)
		IL_000a: throw
	} // end of method MethodRefTestCases::NonStaticLambdaFromDelegate

	.method public hidebysig 
		instance void NonStaticLocalFunctionFromDelegate (
			int32 i
		) cil managed 
	{
		// Header size: 12
		// Code size: 11 (0xb)
		.maxstack 1

		IL_0000: ldstr "InlineIL processing failed: A compiler-generated method is referenced by the delegate (in System.Void InlineIL.Tests.InvalidAssemblyToProcess.MethodRefTestCases::NonStaticLocalFunctionFromDelegate(System.Int32) at instruction IL_001f: call System.Void InlineIL.IL/Emit::Call(InlineIL.MethodRef))"
		IL_0005: newobj instance void [mscorlib]System.InvalidProgramException::.ctor(string)
		IL_000a: throw
	} // end of method MethodRefTestCases::NonStaticLocalFunctionFromDelegate

	.method public hidebysig 
		instance void NotAGenericMethod () cil managed 
	{
		// Header size: 1
		// Code size: 11 (0xb)
		.maxstack 8

		IL_0000: ldstr "InlineIL processing failed: Not a generic method: System.Void InlineIL.Tests.InvalidAssemblyToProcess.MethodRefTestCases::NotAGenericMethod()"
		IL_0005: newobj instance void [mscorlib]System.InvalidProgramException::.ctor(string)
		IL_000a: throw
	} // end of method MethodRefTestCases::NotAGenericMethod

	.method public hidebysig 
		instance void NotAVarArgMethod () cil managed 
	{
		// Header size: 1
		// Code size: 11 (0xb)
		.maxstack 8

		IL_0000: ldstr "InlineIL processing failed: Not a vararg method: System.Void InlineIL.Tests.InvalidAssemblyToProcess.MethodRefTestCases::NotAVarArgMethod()"
		IL_0005: newobj instance void [mscorlib]System.InvalidProgramException::.ctor(string)
		IL_000a: throw
	} // end of method MethodRefTestCases::NotAVarArgMethod

	.method public hidebysig 
		instance void NoTypeInitializer () cil managed 
	{
		// Header size: 1
		// Code size: 11 (0xb)
		.maxstack 8

		IL_0000: ldstr "InlineIL processing failed: Type InlineIL.Tests.InvalidAssemblyToProcess.MethodRefTestCases/ClassWithoutInitializer has no type initializer (in System.Void InlineIL.Tests.InvalidAssemblyToProcess.MethodRefTestCases::NoTypeInitializer() at instruction IL_0015: call System.Void InlineIL.IL/Emit::Call(InlineIL.MethodRef))"
		IL_0005: newobj instance void [mscorlib]System.InvalidProgramException::.ctor(string)
		IL_000a: throw
	} // end of method MethodRefTestCases::NoTypeInitializer

	.method public hidebysig 
		instance void NullMethod () cil managed 
	{
		// Header size: 1
		// Code size: 11 (0xb)
		.maxstack 8

		IL_0000: ldstr "InlineIL processing failed: A non-null string literal is expected (in System.Void InlineIL.Tests.InvalidAssemblyToProcess.MethodRefTestCases::NullMethod() at instruction IL_0010: ldnull)"
		IL_0005: newobj instance void [mscorlib]System.InvalidProgramException::.ctor(string)
		IL_000a: throw
	} // end of method MethodRefTestCases::NullMethod

	.method public hidebysig 
		instance void NullMethodRef () cil managed 
	{
		// Header size: 1
		// Code size: 11 (0xb)
		.maxstack 8

		IL_0000: ldstr "InlineIL processing failed: Expected a method call but was null (in System.Void InlineIL.Tests.InvalidAssemblyToProcess.MethodRefTestCases::NullMethodRef() at instruction IL_0001: ldnull)"
		IL_0005: newobj instance void [mscorlib]System.InvalidProgramException::.ctor(string)
		IL_000a: throw
	} // end of method MethodRefTestCases::NullMethodRef

	.method public hidebysig 
		instance void PropertyWithoutGetter () cil managed 
	{
		// Header size: 1
		// Code size: 11 (0xb)
		.maxstack 8

		IL_0000: ldstr "InlineIL processing failed: Property 'ValueSetOnly' in type InlineIL.Tests.InvalidAssemblyToProcess.MethodRefTestCases has no getter (in System.Void InlineIL.Tests.InvalidAssemblyToProcess.MethodRefTestCases::PropertyWithoutGetter() at instruction IL_001a: call System.Void InlineIL.IL/Emit::Call(InlineIL.MethodRef))"
		IL_0005: newobj instance void [mscorlib]System.InvalidProgramException::.ctor(string)
		IL_000a: throw
	} // end of method MethodRefTestCases::PropertyWithoutGetter

	.method public hidebysig 
		instance void PropertyWithoutSetter () cil managed 
	{
		// Header size: 1
		// Code size: 11 (0xb)
		.maxstack 8

		IL_0000: ldstr "InlineIL processing failed: Property 'ValueGetOnly' in type InlineIL.Tests.InvalidAssemblyToProcess.MethodRefTestCases has no setter (in System.Void InlineIL.Tests.InvalidAssemblyToProcess.MethodRefTestCases::PropertyWithoutSetter() at instruction IL_001a: call System.Void InlineIL.IL/Emit::Call(InlineIL.MethodRef))"
		IL_0005: newobj instance void [mscorlib]System.InvalidProgramException::.ctor(string)
		IL_000a: throw
	} // end of method MethodRefTestCases::PropertyWithoutSetter

	.method public hidebysig specialname 
		instance void remove_Event (
			class [mscorlib]System.Action 'value'
		) cil managed 
	{
		.custom instance void [mscorlib]System.Runtime.CompilerServices.CompilerGeneratedAttribute::.ctor() = (
			01 00 00 00
		)
		// Header size: 12
		// Code size: 41 (0x29)
		.maxstack 3
		.locals init (
			[0] class [mscorlib]System.Action,
			[1] class [mscorlib]System.Action,
			[2] class [mscorlib]System.Action
		)

		IL_0000: ldarg.0
		IL_0001: ldfld class [mscorlib]System.Action InlineIL.Tests.InvalidAssemblyToProcess.MethodRefTestCases::Event
		IL_0006: stloc.0
		// loop start (head: IL_0007)
			IL_0007: ldloc.0
			IL_0008: stloc.1
			IL_0009: ldloc.1
			IL_000a: ldarg.1
			IL_000b: call class [mscorlib]System.Delegate [mscorlib]System.Delegate::Remove(class [mscorlib]System.Delegate, class [mscorlib]System.Delegate)
			IL_0010: castclass [mscorlib]System.Action
			IL_0015: stloc.2
			IL_0016: ldarg.0
			IL_0017: ldflda class [mscorlib]System.Action InlineIL.Tests.InvalidAssemblyToProcess.MethodRefTestCases::Event
			IL_001c: ldloc.2
			IL_001d: ldloc.1
			IL_001e: call !!0 [mscorlib]System.Threading.Interlocked::CompareExchange<class [mscorlib]System.Action>(!!0&, !!0, !!0)
			IL_0023: stloc.0
			IL_0024: ldloc.0
			IL_0025: ldloc.1
			IL_0026: bne.un.s IL_0007
		// end loop
		IL_0028: ret
	} // end of method MethodRefTestCases::remove_Event

	.method public hidebysig specialname 
		instance void set_Value (
			int32 'value'
		) cil managed 
	{
		.custom instance void [mscorlib]System.Runtime.CompilerServices.CompilerGeneratedAttribute::.ctor() = (
			01 00 00 00
		)
		// Header size: 1
		// Code size: 8 (0x8)
		.maxstack 8

		IL_0000: ldarg.0
		IL_0001: ldarg.1
		IL_0002: stfld int32 InlineIL.Tests.InvalidAssemblyToProcess.MethodRefTestCases::'<Value>k__BackingField'
		IL_0007: ret
	} // end of method MethodRefTestCases::set_Value

	.method public hidebysig specialname 
		instance void set_ValueSetOnly (
			int32 'value'
		) cil managed 
	{
		// Header size: 1
		// Code size: 9 (0x9)
		.maxstack 8

		IL_0000: ldarg.0
		IL_0001: ldarg.1
		IL_0002: call instance void InlineIL.Tests.InvalidAssemblyToProcess.MethodRefTestCases::set_Value(int32)
		IL_0007: nop
		IL_0008: ret
	} // end of method MethodRefTestCases::set_ValueSetOnly

	.method public hidebysig 
		instance void StaticDelegateFromDelegate () cil managed 
	{
		// Header size: 1
		// Code size: 11 (0xb)
		.maxstack 8

		IL_0000: ldstr "InlineIL processing failed: A compiler-generated method is referenced by the delegate (in System.Void InlineIL.Tests.InvalidAssemblyToProcess.MethodRefTestCases::StaticDelegateFromDelegate() at instruction IL_0025: call System.Void InlineIL.IL/Emit::Call(InlineIL.MethodRef))"
		IL_0005: newobj instance void [mscorlib]System.InvalidProgramException::.ctor(string)
		IL_000a: throw
	} // end of method MethodRefTestCases::StaticDelegateFromDelegate

	.method public hidebysig 
		instance void StaticLambdaFromDelegate () cil managed 
	{
		// Header size: 1
		// Code size: 11 (0xb)
		.maxstack 8

		IL_0000: ldstr "InlineIL processing failed: A compiler-generated method is referenced by the delegate (in System.Void InlineIL.Tests.InvalidAssemblyToProcess.MethodRefTestCases::StaticLambdaFromDelegate() at instruction IL_0025: call System.Void InlineIL.IL/Emit::Call(InlineIL.MethodRef))"
		IL_0005: newobj instance void [mscorlib]System.InvalidProgramException::.ctor(string)
		IL_000a: throw
	} // end of method MethodRefTestCases::StaticLambdaFromDelegate

	.method public hidebysig 
		instance void StaticLocalFunctionFromDelegate () cil managed 
	{
		// Header size: 1
		// Code size: 11 (0xb)
		.maxstack 8

		IL_0000: ldstr "InlineIL processing failed: A compiler-generated method is referenced by the delegate (in System.Void InlineIL.Tests.InvalidAssemblyToProcess.MethodRefTestCases::StaticLocalFunctionFromDelegate() at instruction IL_0021: call System.Void InlineIL.IL/Emit::Call(InlineIL.MethodRef))"
		IL_0005: newobj instance void [mscorlib]System.InvalidProgramException::.ctor(string)
		IL_000a: throw
	} // end of method MethodRefTestCases::StaticLocalFunctionFromDelegate

	.method public hidebysig 
		instance void TooManyGenericArgsProvided () cil managed 
	{
		// Header size: 12
		// Code size: 11 (0xb)
		.maxstack 1

		IL_0000: ldstr "InlineIL processing failed: Incorrect number of generic arguments supplied for method T InlineIL.Tests.InvalidAssemblyToProcess.MethodRefTestCases::GenericMethod(T) - expected 1, but got 2 (in System.Void InlineIL.Tests.InvalidAssemblyToProcess.MethodRefTestCases::TooManyGenericArgsProvided() at instruction IL_0049: call System.Void InlineIL.IL/Emit::Call(InlineIL.MethodRef))"
		IL_0005: newobj instance void [mscorlib]System.InvalidProgramException::.ctor(string)
		IL_000a: throw
	} // end of method MethodRefTestCases::TooManyGenericArgsProvided

	.method public hidebysig 
		instance void TypeGenericArgOutOfBounds () cil managed 
	{
		// Header size: 12
		// Code size: 11 (0xb)
		.maxstack 1

		IL_0000: ldstr "InlineIL processing failed: Method Method(!42) not found in type InlineIL.Tests.InvalidAssemblyToProcess.MethodRefTestCases/GenericType`1 (in System.Void InlineIL.Tests.InvalidAssemblyToProcess.MethodRefTestCases::TypeGenericArgOutOfBounds() at instruction IL_004c: call System.Void InlineIL.IL/Emit::Call(InlineIL.MethodRef))"
		IL_0005: newobj instance void [mscorlib]System.InvalidProgramException::.ctor(string)
		IL_000a: throw
	} // end of method MethodRefTestCases::TypeGenericArgOutOfBounds

	.method public hidebysig 
		instance void TypeGenericArgOutsideOfGenericType () cil managed 
	{
		// Header size: 12
		// Code size: 11 (0xb)
		.maxstack 1

		IL_0000: ldstr "InlineIL processing failed: Method GenericMethod(!42) not found in type InlineIL.Tests.InvalidAssemblyToProcess.MethodRefTestCases (in System.Void InlineIL.Tests.InvalidAssemblyToProcess.MethodRefTestCases::TypeGenericArgOutsideOfGenericType() at instruction IL_004c: call System.Void InlineIL.IL/Emit::Call(InlineIL.MethodRef))"
		IL_0005: newobj instance void [mscorlib]System.InvalidProgramException::.ctor(string)
		IL_000a: throw
	} // end of method MethodRefTestCases::TypeGenericArgOutsideOfGenericType

	.method public hidebysig 
		instance void UnknownConstructor () cil managed 
	{
		// Header size: 12
		// Code size: 11 (0xb)
		.maxstack 1

		IL_0000: ldstr "InlineIL processing failed: Type InlineIL.Tests.InvalidAssemblyToProcess.MethodRefTestCases has no constructor with signature (System.Guid, System.Guid) (in System.Void InlineIL.Tests.InvalidAssemblyToProcess.MethodRefTestCases::UnknownConstructor() at instruction IL_003f: call System.Void InlineIL.IL/Emit::Call(InlineIL.MethodRef))"
		IL_0005: newobj instance void [mscorlib]System.InvalidProgramException::.ctor(string)
		IL_000a: throw
	} // end of method MethodRefTestCases::UnknownConstructor

	.method public hidebysig 
		instance void UnknownMethodWithoutParams () cil managed 
	{
		// Header size: 1
		// Code size: 11 (0xb)
		.maxstack 8

		IL_0000: ldstr "InlineIL processing failed: Method 'Nope' not found in type System.Object (in System.Void InlineIL.Tests.InvalidAssemblyToProcess.MethodRefTestCases::UnknownMethodWithoutParams() at instruction IL_001a: call System.Void InlineIL.IL/Emit::Call(InlineIL.MethodRef))"
		IL_0005: newobj instance void [mscorlib]System.InvalidProgramException::.ctor(string)
		IL_000a: throw
	} // end of method MethodRefTestCases::UnknownMethodWithoutParams

	.method public hidebysig 
		instance void UnknownMethodWithParams () cil managed 
	{
		// Header size: 1
		// Code size: 11 (0xb)
		.maxstack 8

		IL_0000: ldstr "InlineIL processing failed: Method Nope(System.Int32) not found in type System.Object (in System.Void InlineIL.Tests.InvalidAssemblyToProcess.MethodRefTestCases::UnknownMethodWithParams() at instruction IL_0032: call System.Void InlineIL.IL/Emit::Call(InlineIL.MethodRef))"
		IL_0005: newobj instance void [mscorlib]System.InvalidProgramException::.ctor(string)
		IL_000a: throw
	} // end of method MethodRefTestCases::UnknownMethodWithParams

	.method public hidebysig 
		instance void UnknownProperty () cil managed 
	{
		// Header size: 1
		// Code size: 11 (0xb)
		.maxstack 8

		IL_0000: ldstr "InlineIL processing failed: Property 'Nope' not found in type InlineIL.Tests.InvalidAssemblyToProcess.MethodRefTestCases (in System.Void InlineIL.Tests.InvalidAssemblyToProcess.MethodRefTestCases::UnknownProperty() at instruction IL_001a: call System.Void InlineIL.IL/Emit::Call(InlineIL.MethodRef))"
		IL_0005: newobj instance void [mscorlib]System.InvalidProgramException::.ctor(string)
		IL_000a: throw
	} // end of method MethodRefTestCases::UnknownProperty

	.method public hidebysig 
		instance void UnusedInstance () cil managed 
	{
		// Header size: 1
		// Code size: 11 (0xb)
		.maxstack 8

		IL_0000: ldstr "InlineIL processing failed: Unconsumed reference to InlineIL (in System.Void InlineIL.Tests.InvalidAssemblyToProcess.MethodRefTestCases::UnusedInstance() at instruction IL_000b: call InlineIL.TypeRef InlineIL.TypeRef::op_Implicit(System.Type))"
		IL_0005: newobj instance void [mscorlib]System.InvalidProgramException::.ctor(string)
		IL_000a: throw
	} // end of method MethodRefTestCases::UnusedInstance

	.method private hidebysig static 
		vararg int32[] VarArgMethod (
			int32 count
		) cil managed 
	{
		// Header size: 1
		// Code size: 2 (0x2)
		.maxstack 8

		IL_0000: ldnull
		IL_0001: ret
	} // end of method MethodRefTestCases::VarArgMethod

	.method public hidebysig 
		instance void VarArgParamsAlreadySupplied () cil managed 
	{
		// Header size: 12
		// Code size: 11 (0xb)
		.maxstack 1

		IL_0000: ldstr "InlineIL processing failed: Optional parameters for vararg call have already been supplied (in System.Void InlineIL.Tests.InvalidAssemblyToProcess.MethodRefTestCases::VarArgParamsAlreadySupplied() at instruction IL_0054: call System.Void InlineIL.IL/Emit::Call(InlineIL.MethodRef))"
		IL_0005: newobj instance void [mscorlib]System.InvalidProgramException::.ctor(string)
		IL_000a: throw
	} // end of method MethodRefTestCases::VarArgParamsAlreadySupplied

	// Events
	.event [mscorlib]System.Action Event
	{
		.addon instance void InlineIL.Tests.InvalidAssemblyToProcess.MethodRefTestCases::add_Event(class [mscorlib]System.Action)
		.removeon instance void InlineIL.Tests.InvalidAssemblyToProcess.MethodRefTestCases::remove_Event(class [mscorlib]System.Action)
	}


	// Properties
	.property instance int32 Value()
	{
		.get instance int32 InlineIL.Tests.InvalidAssemblyToProcess.MethodRefTestCases::get_Value()
		.set instance void InlineIL.Tests.InvalidAssemblyToProcess.MethodRefTestCases::set_Value(int32)
	}
	.property instance int32 ValueGetOnly()
	{
		.get instance int32 InlineIL.Tests.InvalidAssemblyToProcess.MethodRefTestCases::get_ValueGetOnly()
	}
	.property instance int32 ValueSetOnly()
	{
		.set instance void InlineIL.Tests.InvalidAssemblyToProcess.MethodRefTestCases::set_ValueSetOnly(int32)
	}

} // end of class InlineIL.Tests.InvalidAssemblyToProcess.MethodRefTestCases
