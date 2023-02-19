.class public auto ansi beforefieldinit InlineIL.Tests.AssemblyToProcess.MethodRefTestCases
	extends [netstandard]System.Object
	implements [InlineIL.Tests.Common]InlineIL.Tests.Common.IMethodRefTestCases
{
	.custom instance void [InlineIL.Tests.Common]InlineIL.Tests.Common.TestCasesAttribute::.ctor() = (
		01 00 00 00
	)
	// Nested Types
	.class nested private auto ansi abstract sealed beforefieldinit '<>O'
		extends [netstandard]System.Object
	{
		.custom instance void [netstandard]System.Runtime.CompilerServices.CompilerGeneratedAttribute::.ctor() = (
			01 00 00 00
		)
		// Fields
		.field public static class [netstandard]System.Func`2<int32, int32> '<0>__OverloadedMethod'
		.field public static class [netstandard]System.Func`2<int32, int32> '<1>__StaticMethod'
		.field public static class [netstandard]System.Func`2<int32, int32> '<2>__StaticMethod'
		.field public static class [netstandard]System.Func`1<string> '<3>__NormalMethod'
		.field public static class [netstandard]System.Func`1<string> '<4>__GenericMethod'

	} // end of class <>O

	.class nested private auto ansi beforefieldinit BinaryOperatorsClass
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
		} // end of method BinaryOperatorsClass::.ctor

		.method public hidebysig virtual 
			instance bool Equals (
				object obj
			) cil managed 
		{
			// Header size: 1
			// Code size: 2 (0x2)
			.maxstack 8

			IL_0000: ldc.i4.0
			IL_0001: ret
		} // end of method BinaryOperatorsClass::Equals

		.method public hidebysig virtual 
			instance int32 GetHashCode () cil managed 
		{
			// Header size: 1
			// Code size: 2 (0x2)
			.maxstack 8

			IL_0000: ldc.i4.0
			IL_0001: ret
		} // end of method BinaryOperatorsClass::GetHashCode

		.method public hidebysig specialname static 
			int32 op_Addition (
				class InlineIL.Tests.AssemblyToProcess.MethodRefTestCases/BinaryOperatorsClass a,
				class InlineIL.Tests.AssemblyToProcess.MethodRefTestCases/BinaryOperatorsClass b
			) cil managed 
		{
			// Header size: 1
			// Code size: 2 (0x2)
			.maxstack 8

			IL_0000: ldc.i4.3
			IL_0001: ret
		} // end of method BinaryOperatorsClass::op_Addition

		.method public hidebysig specialname static 
			int32 op_Addition (
				class InlineIL.Tests.AssemblyToProcess.MethodRefTestCases/BinaryOperatorsClass a,
				int32 b
			) cil managed 
		{
			// Header size: 1
			// Code size: 2 (0x2)
			.maxstack 8

			IL_0000: ldc.i4.1
			IL_0001: ret
		} // end of method BinaryOperatorsClass::op_Addition

		.method public hidebysig specialname static 
			int32 op_Addition (
				int32 a,
				class InlineIL.Tests.AssemblyToProcess.MethodRefTestCases/BinaryOperatorsClass b
			) cil managed 
		{
			// Header size: 1
			// Code size: 2 (0x2)
			.maxstack 8

			IL_0000: ldc.i4.2
			IL_0001: ret
		} // end of method BinaryOperatorsClass::op_Addition

		.method public hidebysig specialname static 
			int32 op_BitwiseAnd (
				class InlineIL.Tests.AssemblyToProcess.MethodRefTestCases/BinaryOperatorsClass a,
				int32 b
			) cil managed 
		{
			// Header size: 1
			// Code size: 3 (0x3)
			.maxstack 8

			IL_0000: ldc.i4.s 9
			IL_0002: ret
		} // end of method BinaryOperatorsClass::op_BitwiseAnd

		.method public hidebysig specialname static 
			int32 op_BitwiseOr (
				class InlineIL.Tests.AssemblyToProcess.MethodRefTestCases/BinaryOperatorsClass a,
				int32 b
			) cil managed 
		{
			// Header size: 1
			// Code size: 3 (0x3)
			.maxstack 8

			IL_0000: ldc.i4.s 10
			IL_0002: ret
		} // end of method BinaryOperatorsClass::op_BitwiseOr

		.method public hidebysig specialname static 
			int32 op_Division (
				class InlineIL.Tests.AssemblyToProcess.MethodRefTestCases/BinaryOperatorsClass a,
				int32 b
			) cil managed 
		{
			// Header size: 1
			// Code size: 2 (0x2)
			.maxstack 8

			IL_0000: ldc.i4.6
			IL_0001: ret
		} // end of method BinaryOperatorsClass::op_Division

		.method public hidebysig specialname static 
			int32 op_Equality (
				class InlineIL.Tests.AssemblyToProcess.MethodRefTestCases/BinaryOperatorsClass a,
				int32 b
			) cil managed 
		{
			// Header size: 1
			// Code size: 3 (0x3)
			.maxstack 8

			IL_0000: ldc.i4.s 13
			IL_0002: ret
		} // end of method BinaryOperatorsClass::op_Equality

		.method public hidebysig specialname static 
			int32 op_ExclusiveOr (
				class InlineIL.Tests.AssemblyToProcess.MethodRefTestCases/BinaryOperatorsClass a,
				int32 b
			) cil managed 
		{
			// Header size: 1
			// Code size: 2 (0x2)
			.maxstack 8

			IL_0000: ldc.i4.8
			IL_0001: ret
		} // end of method BinaryOperatorsClass::op_ExclusiveOr

		.method public hidebysig specialname static 
			int32 op_GreaterThan (
				class InlineIL.Tests.AssemblyToProcess.MethodRefTestCases/BinaryOperatorsClass a,
				int32 b
			) cil managed 
		{
			// Header size: 1
			// Code size: 3 (0x3)
			.maxstack 8

			IL_0000: ldc.i4.s 14
			IL_0002: ret
		} // end of method BinaryOperatorsClass::op_GreaterThan

		.method public hidebysig specialname static 
			int32 op_GreaterThanOrEqual (
				class InlineIL.Tests.AssemblyToProcess.MethodRefTestCases/BinaryOperatorsClass a,
				int32 b
			) cil managed 
		{
			// Header size: 1
			// Code size: 3 (0x3)
			.maxstack 8

			IL_0000: ldc.i4.s 17
			IL_0002: ret
		} // end of method BinaryOperatorsClass::op_GreaterThanOrEqual

		.method public hidebysig specialname static 
			int32 op_Inequality (
				class InlineIL.Tests.AssemblyToProcess.MethodRefTestCases/BinaryOperatorsClass a,
				int32 b
			) cil managed 
		{
			// Header size: 1
			// Code size: 3 (0x3)
			.maxstack 8

			IL_0000: ldc.i4.s 16
			IL_0002: ret
		} // end of method BinaryOperatorsClass::op_Inequality

		.method public hidebysig specialname static 
			int32 op_LeftShift (
				class InlineIL.Tests.AssemblyToProcess.MethodRefTestCases/BinaryOperatorsClass a,
				int32 b
			) cil managed 
		{
			// Header size: 1
			// Code size: 3 (0x3)
			.maxstack 8

			IL_0000: ldc.i4.s 11
			IL_0002: ret
		} // end of method BinaryOperatorsClass::op_LeftShift

		.method public hidebysig specialname static 
			int32 op_LessThan (
				class InlineIL.Tests.AssemblyToProcess.MethodRefTestCases/BinaryOperatorsClass a,
				int32 b
			) cil managed 
		{
			// Header size: 1
			// Code size: 3 (0x3)
			.maxstack 8

			IL_0000: ldc.i4.s 15
			IL_0002: ret
		} // end of method BinaryOperatorsClass::op_LessThan

		.method public hidebysig specialname static 
			int32 op_LessThanOrEqual (
				class InlineIL.Tests.AssemblyToProcess.MethodRefTestCases/BinaryOperatorsClass a,
				int32 b
			) cil managed 
		{
			// Header size: 1
			// Code size: 3 (0x3)
			.maxstack 8

			IL_0000: ldc.i4.s 18
			IL_0002: ret
		} // end of method BinaryOperatorsClass::op_LessThanOrEqual

		.method public hidebysig specialname static 
			int32 op_Modulus (
				class InlineIL.Tests.AssemblyToProcess.MethodRefTestCases/BinaryOperatorsClass a,
				int32 b
			) cil managed 
		{
			// Header size: 1
			// Code size: 2 (0x2)
			.maxstack 8

			IL_0000: ldc.i4.7
			IL_0001: ret
		} // end of method BinaryOperatorsClass::op_Modulus

		.method public hidebysig specialname static 
			int32 op_Multiply (
				class InlineIL.Tests.AssemblyToProcess.MethodRefTestCases/BinaryOperatorsClass a,
				int32 b
			) cil managed 
		{
			// Header size: 1
			// Code size: 2 (0x2)
			.maxstack 8

			IL_0000: ldc.i4.5
			IL_0001: ret
		} // end of method BinaryOperatorsClass::op_Multiply

		.method public hidebysig specialname static 
			int32 op_RightShift (
				class InlineIL.Tests.AssemblyToProcess.MethodRefTestCases/BinaryOperatorsClass a,
				int32 b
			) cil managed 
		{
			// Header size: 1
			// Code size: 3 (0x3)
			.maxstack 8

			IL_0000: ldc.i4.s 12
			IL_0002: ret
		} // end of method BinaryOperatorsClass::op_RightShift

		.method public hidebysig specialname static 
			int32 op_Subtraction (
				class InlineIL.Tests.AssemblyToProcess.MethodRefTestCases/BinaryOperatorsClass a,
				int32 b
			) cil managed 
		{
			// Header size: 1
			// Code size: 2 (0x2)
			.maxstack 8

			IL_0000: ldc.i4.4
			IL_0001: ret
		} // end of method BinaryOperatorsClass::op_Subtraction

	} // end of class BinaryOperatorsClass

	.class nested private auto ansi beforefieldinit ConversionOperatorsClass
		extends [netstandard]System.Object
	{
		// Fields
		.field private initonly int32 '<Value>k__BackingField'
		.custom instance void [netstandard]System.Runtime.CompilerServices.CompilerGeneratedAttribute::.ctor() = (
			01 00 00 00
		)

		// Methods
		.method public hidebysig specialname rtspecialname 
			instance void .ctor (
				int32 'value'
			) cil managed 
		{
			// Header size: 1
			// Code size: 14 (0xe)
			.maxstack 8

			IL_0000: ldarg.0
			IL_0001: call instance void [netstandard]System.Object::.ctor()
			IL_0006: ldarg.0
			IL_0007: ldarg.1
			IL_0008: stfld int32 InlineIL.Tests.AssemblyToProcess.MethodRefTestCases/ConversionOperatorsClass::'<Value>k__BackingField'
			IL_000d: ret
		} // end of method ConversionOperatorsClass::.ctor

		.method public hidebysig specialname 
			instance int32 get_Value () cil managed 
		{
			.custom instance void [netstandard]System.Runtime.CompilerServices.CompilerGeneratedAttribute::.ctor() = (
				01 00 00 00
			)
			// Header size: 1
			// Code size: 7 (0x7)
			.maxstack 8

			IL_0000: ldarg.0
			IL_0001: ldfld int32 InlineIL.Tests.AssemblyToProcess.MethodRefTestCases/ConversionOperatorsClass::'<Value>k__BackingField'
			IL_0006: ret
		} // end of method ConversionOperatorsClass::get_Value

		.method public hidebysig specialname static 
			int16 op_Explicit (
				class InlineIL.Tests.AssemblyToProcess.MethodRefTestCases/ConversionOperatorsClass obj
			) cil managed 
		{
			// Header size: 1
			// Code size: 2 (0x2)
			.maxstack 8

			IL_0000: ldc.i4.5
			IL_0001: ret
		} // end of method ConversionOperatorsClass::op_Explicit

		.method public hidebysig specialname static 
			class InlineIL.Tests.AssemblyToProcess.MethodRefTestCases/ConversionOperatorsClass op_Explicit (
				int16 i
			) cil managed 
		{
			// Header size: 1
			// Code size: 7 (0x7)
			.maxstack 8

			IL_0000: ldc.i4.6
			IL_0001: newobj instance void InlineIL.Tests.AssemblyToProcess.MethodRefTestCases/ConversionOperatorsClass::.ctor(int32)
			IL_0006: ret
		} // end of method ConversionOperatorsClass::op_Explicit

		.method public hidebysig specialname static 
			int32 op_Implicit (
				class InlineIL.Tests.AssemblyToProcess.MethodRefTestCases/ConversionOperatorsClass obj
			) cil managed 
		{
			// Header size: 1
			// Code size: 2 (0x2)
			.maxstack 8

			IL_0000: ldc.i4.1
			IL_0001: ret
		} // end of method ConversionOperatorsClass::op_Implicit

		.method public hidebysig specialname static 
			int64 op_Implicit (
				class InlineIL.Tests.AssemblyToProcess.MethodRefTestCases/ConversionOperatorsClass obj
			) cil managed 
		{
			// Header size: 1
			// Code size: 3 (0x3)
			.maxstack 8

			IL_0000: ldc.i4.3
			IL_0001: conv.i8
			IL_0002: ret
		} // end of method ConversionOperatorsClass::op_Implicit

		.method public hidebysig specialname static 
			class InlineIL.Tests.AssemblyToProcess.MethodRefTestCases/ConversionOperatorsClass op_Implicit (
				int32 i
			) cil managed 
		{
			// Header size: 1
			// Code size: 7 (0x7)
			.maxstack 8

			IL_0000: ldc.i4.2
			IL_0001: newobj instance void InlineIL.Tests.AssemblyToProcess.MethodRefTestCases/ConversionOperatorsClass::.ctor(int32)
			IL_0006: ret
		} // end of method ConversionOperatorsClass::op_Implicit

		.method public hidebysig specialname static 
			class InlineIL.Tests.AssemblyToProcess.MethodRefTestCases/ConversionOperatorsClass op_Implicit (
				int64 i
			) cil managed 
		{
			// Header size: 1
			// Code size: 7 (0x7)
			.maxstack 8

			IL_0000: ldc.i4.4
			IL_0001: newobj instance void InlineIL.Tests.AssemblyToProcess.MethodRefTestCases/ConversionOperatorsClass::.ctor(int32)
			IL_0006: ret
		} // end of method ConversionOperatorsClass::op_Implicit

		// Properties
		.property instance int32 Value()
		{
			.get instance int32 InlineIL.Tests.AssemblyToProcess.MethodRefTestCases/ConversionOperatorsClass::get_Value()
		}

	} // end of class ConversionOperatorsClass

	.class nested private auto ansi beforefieldinit GenericOperatorsClass`1<T>
		extends [netstandard]System.Object
	{
		// Fields
		.field private int32 '<Value>k__BackingField'
		.custom instance void [netstandard]System.Runtime.CompilerServices.CompilerGeneratedAttribute::.ctor() = (
			01 00 00 00
		)

		// Methods
		.method public hidebysig specialname rtspecialname 
			instance void .ctor (
				int32 'value'
			) cil managed 
		{
			// Header size: 1
			// Code size: 14 (0xe)
			.maxstack 8

			IL_0000: ldarg.0
			IL_0001: call instance void [netstandard]System.Object::.ctor()
			IL_0006: ldarg.0
			IL_0007: ldarg.1
			IL_0008: call instance void class InlineIL.Tests.AssemblyToProcess.MethodRefTestCases/GenericOperatorsClass`1<!T>::set_Value(int32)
			IL_000d: ret
		} // end of method GenericOperatorsClass`1::.ctor

		.method public hidebysig specialname 
			instance int32 get_Value () cil managed 
		{
			.custom instance void [netstandard]System.Runtime.CompilerServices.CompilerGeneratedAttribute::.ctor() = (
				01 00 00 00
			)
			// Header size: 1
			// Code size: 7 (0x7)
			.maxstack 8

			IL_0000: ldarg.0
			IL_0001: ldfld int32 class InlineIL.Tests.AssemblyToProcess.MethodRefTestCases/GenericOperatorsClass`1<!T>::'<Value>k__BackingField'
			IL_0006: ret
		} // end of method GenericOperatorsClass`1::get_Value

		.method public hidebysig specialname static 
			int32 op_Addition (
				!T a,
				class InlineIL.Tests.AssemblyToProcess.MethodRefTestCases/GenericOperatorsClass`1<!T> b
			) cil managed 
		{
			// Header size: 1
			// Code size: 3 (0x3)
			.maxstack 8

			IL_0000: ldc.i4.s 103
			IL_0002: ret
		} // end of method GenericOperatorsClass`1::op_Addition

		.method public hidebysig specialname static 
			int32 op_Addition (
				class InlineIL.Tests.AssemblyToProcess.MethodRefTestCases/GenericOperatorsClass`1<!T> a,
				!T b
			) cil managed 
		{
			// Header size: 1
			// Code size: 3 (0x3)
			.maxstack 8

			IL_0000: ldc.i4.s 104
			IL_0002: ret
		} // end of method GenericOperatorsClass`1::op_Addition

		.method public hidebysig specialname static 
			class InlineIL.Tests.AssemblyToProcess.MethodRefTestCases/GenericOperatorsClass`1<!T> op_Implicit (
				!T c
			) cil managed 
		{
			// Header size: 1
			// Code size: 8 (0x8)
			.maxstack 8

			IL_0000: ldc.i4.s 101
			IL_0002: newobj instance void class InlineIL.Tests.AssemblyToProcess.MethodRefTestCases/GenericOperatorsClass`1<!T>::.ctor(int32)
			IL_0007: ret
		} // end of method GenericOperatorsClass`1::op_Implicit

		.method public hidebysig specialname static 
			!T op_Implicit (
				class InlineIL.Tests.AssemblyToProcess.MethodRefTestCases/GenericOperatorsClass`1<!T> c
			) cil managed 
		{
			// Header size: 12
			// Code size: 18 (0x12)
			.maxstack 2
			.locals init (
				[0] !T
			)

			IL_0000: ldarg.0
			IL_0001: ldc.i4.s 102
			IL_0003: callvirt instance void class InlineIL.Tests.AssemblyToProcess.MethodRefTestCases/GenericOperatorsClass`1<!T>::set_Value(int32)
			IL_0008: ldloca.s 0
			IL_000a: initobj !T
			IL_0010: ldloc.0
			IL_0011: ret
		} // end of method GenericOperatorsClass`1::op_Implicit

		.method public hidebysig specialname static 
			int32 op_UnaryPlus (
				class InlineIL.Tests.AssemblyToProcess.MethodRefTestCases/GenericOperatorsClass`1<!T> a
			) cil managed 
		{
			// Header size: 1
			// Code size: 3 (0x3)
			.maxstack 8

			IL_0000: ldc.i4.s 105
			IL_0002: ret
		} // end of method GenericOperatorsClass`1::op_UnaryPlus

		.method private hidebysig specialname 
			instance void set_Value (
				int32 'value'
			) cil managed 
		{
			.custom instance void [netstandard]System.Runtime.CompilerServices.CompilerGeneratedAttribute::.ctor() = (
				01 00 00 00
			)
			// Header size: 1
			// Code size: 8 (0x8)
			.maxstack 8

			IL_0000: ldarg.0
			IL_0001: ldarg.1
			IL_0002: stfld int32 class InlineIL.Tests.AssemblyToProcess.MethodRefTestCases/GenericOperatorsClass`1<!T>::'<Value>k__BackingField'
			IL_0007: ret
		} // end of method GenericOperatorsClass`1::set_Value

		// Properties
		.property instance int32 Value()
		{
			.get instance int32 InlineIL.Tests.AssemblyToProcess.MethodRefTestCases/GenericOperatorsClass`1::get_Value()
			.set instance void InlineIL.Tests.AssemblyToProcess.MethodRefTestCases/GenericOperatorsClass`1::set_Value(int32)
		}

	} // end of class GenericOperatorsClass`1

	.class nested private auto ansi beforefieldinit GenericType`1<TClass>
		extends [netstandard]System.Object
	{
		// Nested Types
		.class nested public auto ansi beforefieldinit NestedGenericType`1<TClass, TOther>
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
			} // end of method NestedGenericType`1::.ctor

			.method public hidebysig static 
				int32 GenericOverload<T> (
					!!T[] a,
					class [netstandard]System.Collections.Generic.List`1<!!T> b
				) cil managed 
			{
				// Header size: 1
				// Code size: 2 (0x2)
				.maxstack 8

				IL_0000: ldc.i4.5
				IL_0001: ret
			} // end of method NestedGenericType`1::GenericOverload

			.method public hidebysig static 
				int32 GenericOverload<T> (
					!TClass a,
					!!T b
				) cil managed 
			{
				// Header size: 1
				// Code size: 2 (0x2)
				.maxstack 8

				IL_0000: ldc.i4.3
				IL_0001: ret
			} // end of method NestedGenericType`1::GenericOverload

			.method public hidebysig static 
				int32 GenericOverload<T> (
					!TClass a,
					!TOther b
				) cil managed 
			{
				// Header size: 1
				// Code size: 2 (0x2)
				.maxstack 8

				IL_0000: ldc.i4.1
				IL_0001: ret
			} // end of method NestedGenericType`1::GenericOverload

			.method public hidebysig static 
				int32 GenericOverload<T> (
					!TClass[] a,
					class [netstandard]System.Collections.Generic.List`1<!TOther> b
				) cil managed 
			{
				// Header size: 1
				// Code size: 2 (0x2)
				.maxstack 8

				IL_0000: ldc.i4.4
				IL_0001: ret
			} // end of method NestedGenericType`1::GenericOverload

			.method public hidebysig static 
				int32 GenericOverload<T> (
					!TOther a,
					!TClass b
				) cil managed 
			{
				// Header size: 1
				// Code size: 2 (0x2)
				.maxstack 8

				IL_0000: ldc.i4.2
				IL_0001: ret
			} // end of method NestedGenericType`1::GenericOverload

		} // end of class NestedGenericType`1


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
		} // end of method GenericType`1::.ctor

		.method public hidebysig static 
			string GenericMethod<TMethod> () cil managed 
		{
			// Header size: 1
			// Code size: 41 (0x29)
			.maxstack 8

			IL_0000: ldtoken !TClass
			IL_0005: call class [netstandard]System.Type [netstandard]System.Type::GetTypeFromHandle(valuetype [netstandard]System.RuntimeTypeHandle)
			IL_000a: callvirt instance string [netstandard]System.Type::get_FullName()
			IL_000f: ldstr " "
			IL_0014: ldtoken !!TMethod
			IL_0019: call class [netstandard]System.Type [netstandard]System.Type::GetTypeFromHandle(valuetype [netstandard]System.RuntimeTypeHandle)
			IL_001e: callvirt instance string [netstandard]System.Type::get_FullName()
			IL_0023: call string [netstandard]System.String::Concat(string, string, string)
			IL_0028: ret
		} // end of method GenericType`1::GenericMethod

		.method public hidebysig static 
			string NormalMethod () cil managed 
		{
			// Header size: 1
			// Code size: 25 (0x19)
			.maxstack 8

			IL_0000: ldtoken !TClass
			IL_0005: call class [netstandard]System.Type [netstandard]System.Type::GetTypeFromHandle(valuetype [netstandard]System.RuntimeTypeHandle)
			IL_000a: callvirt instance string [netstandard]System.Type::get_FullName()
			IL_000f: dup
			IL_0010: brtrue.s IL_0018

			IL_0012: pop
			IL_0013: ldsfld string [netstandard]System.String::Empty

			IL_0018: ret
		} // end of method GenericType`1::NormalMethod

	} // end of class GenericType`1

	.class interface nested private auto ansi abstract ISomeInterface
	{
		// Methods
		.method public hidebysig newslot abstract virtual 
			instance int32 InterfaceMethod (
				int32 a
			) cil managed 
		{
		} // end of method ISomeInterface::InterfaceMethod

	} // end of class ISomeInterface

	.class nested private auto ansi beforefieldinit OtherClass
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
		} // end of method OtherClass::.ctor

		.method public hidebysig static 
			void Create (
				[out] class InlineIL.Tests.AssemblyToProcess.MethodRefTestCases/OtherClass& result
			) cil managed 
		{
			// Header size: 1
			// Code size: 8 (0x8)
			.maxstack 8

			IL_0000: ldarg.0
			IL_0001: newobj instance void InlineIL.Tests.AssemblyToProcess.MethodRefTestCases/OtherClass::.ctor()
			IL_0006: stind.ref
			IL_0007: ret
		} // end of method OtherClass::Create

		.method public hidebysig 
			instance int32 InstanceMethod () cil managed 
		{
			// Header size: 1
			// Code size: 3 (0x3)
			.maxstack 8

			IL_0000: ldc.i4.s 100
			IL_0002: ret
		} // end of method OtherClass::InstanceMethod

		.method public hidebysig 
			instance int32 InstanceMethod (
				int32 a
			) cil managed 
		{
			// Header size: 1
			// Code size: 6 (0x6)
			.maxstack 8

			IL_0000: ldc.i4 200
			IL_0005: ret
		} // end of method OtherClass::InstanceMethod

		.method public hidebysig static 
			int32 StaticMethod () cil managed 
		{
			// Header size: 1
			// Code size: 3 (0x3)
			.maxstack 8

			IL_0000: ldc.i4.s 100
			IL_0002: ret
		} // end of method OtherClass::StaticMethod

		.method public hidebysig static 
			int32 StaticMethod (
				int32 a
			) cil managed 
		{
			// Header size: 1
			// Code size: 6 (0x6)
			.maxstack 8

			IL_0000: ldc.i4 200
			IL_0005: ret
		} // end of method OtherClass::StaticMethod

		.method public hidebysig newslot virtual 
			instance int32 VirtualMethod () cil managed 
		{
			// Header size: 1
			// Code size: 6 (0x6)
			.maxstack 8

			IL_0000: ldc.i4 300
			IL_0005: ret
		} // end of method OtherClass::VirtualMethod

	} // end of class OtherClass

	.class nested private auto ansi beforefieldinit OtherClassDerived
		extends InlineIL.Tests.AssemblyToProcess.MethodRefTestCases/OtherClass
	{
		// Methods
		.method public hidebysig specialname rtspecialname 
			instance void .ctor () cil managed 
		{
			// Header size: 1
			// Code size: 7 (0x7)
			.maxstack 8

			IL_0000: ldarg.0
			IL_0001: call instance void InlineIL.Tests.AssemblyToProcess.MethodRefTestCases/OtherClass::.ctor()
			IL_0006: ret
		} // end of method OtherClassDerived::.ctor

		.method public hidebysig virtual 
			instance int32 VirtualMethod () cil managed 
		{
			// Header size: 1
			// Code size: 6 (0x6)
			.maxstack 8

			IL_0000: ldc.i4 400
			IL_0005: ret
		} // end of method OtherClassDerived::VirtualMethod

	} // end of class OtherClassDerived

	.class nested private sequential ansi sealed beforefieldinit OtherStruct
		extends [netstandard]System.ValueType
		implements InlineIL.Tests.AssemblyToProcess.MethodRefTestCases/ISomeInterface
	{
		.pack 0
		.size 1

		// Methods
		.method public hidebysig 
			instance int32 InstanceMethod () cil managed 
		{
			// Header size: 1
			// Code size: 6 (0x6)
			.maxstack 8

			IL_0000: ldc.i4 1000
			IL_0005: ret
		} // end of method OtherStruct::InstanceMethod

		.method public hidebysig 
			instance int32 InstanceMethod (
				int32 a
			) cil managed 
		{
			// Header size: 1
			// Code size: 6 (0x6)
			.maxstack 8

			IL_0000: ldc.i4 2000
			IL_0005: ret
		} // end of method OtherStruct::InstanceMethod

		.method public final hidebysig newslot virtual 
			instance int32 InterfaceMethod (
				int32 a
			) cil managed 
		{
			// Header size: 1
			// Code size: 6 (0x6)
			.maxstack 8

			IL_0000: ldc.i4 3000
			IL_0005: ret
		} // end of method OtherStruct::InterfaceMethod

		.method public hidebysig static 
			int32 StaticMethod () cil managed 
		{
			// Header size: 1
			// Code size: 6 (0x6)
			.maxstack 8

			IL_0000: ldc.i4 1000
			IL_0005: ret
		} // end of method OtherStruct::StaticMethod

		.method public hidebysig static 
			int32 StaticMethod (
				int32 a
			) cil managed 
		{
			// Header size: 1
			// Code size: 6 (0x6)
			.maxstack 8

			IL_0000: ldc.i4 2000
			IL_0005: ret
		} // end of method OtherStruct::StaticMethod

	} // end of class OtherStruct

	.class nested private auto ansi TypeWithInitializer
		extends [netstandard]System.Object
	{
		// Methods
		.method private hidebysig specialname rtspecialname static 
			void .cctor () cil managed 
		{
			// Header size: 1
			// Code size: 7 (0x7)
			.maxstack 8

			IL_0000: ldnull
			IL_0001: call void [netstandard]System.GC::KeepAlive(object)
			IL_0006: ret
		} // end of method TypeWithInitializer::.cctor

		.method public hidebysig specialname rtspecialname 
			instance void .ctor () cil managed 
		{
			// Header size: 1
			// Code size: 7 (0x7)
			.maxstack 8

			IL_0000: ldarg.0
			IL_0001: call instance void [netstandard]System.Object::.ctor()
			IL_0006: ret
		} // end of method TypeWithInitializer::.ctor

	} // end of class TypeWithInitializer

	.class nested private auto ansi beforefieldinit UnaryOperatorsClass
		extends [netstandard]System.Object
	{
		// Fields
		.field private int32 '<Value>k__BackingField'
		.custom instance void [netstandard]System.Runtime.CompilerServices.CompilerGeneratedAttribute::.ctor() = (
			01 00 00 00
		)

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
		} // end of method UnaryOperatorsClass::.ctor

		.method public hidebysig specialname 
			instance int32 get_Value () cil managed 
		{
			.custom instance void [netstandard]System.Runtime.CompilerServices.CompilerGeneratedAttribute::.ctor() = (
				01 00 00 00
			)
			// Header size: 1
			// Code size: 7 (0x7)
			.maxstack 8

			IL_0000: ldarg.0
			IL_0001: ldfld int32 InlineIL.Tests.AssemblyToProcess.MethodRefTestCases/UnaryOperatorsClass::'<Value>k__BackingField'
			IL_0006: ret
		} // end of method UnaryOperatorsClass::get_Value

		.method public hidebysig specialname static 
			class InlineIL.Tests.AssemblyToProcess.MethodRefTestCases/UnaryOperatorsClass op_Decrement (
				class InlineIL.Tests.AssemblyToProcess.MethodRefTestCases/UnaryOperatorsClass s
			) cil managed 
		{
			// Header size: 1
			// Code size: 9 (0x9)
			.maxstack 8

			IL_0000: ldarg.0
			IL_0001: ldc.i4.1
			IL_0002: callvirt instance void InlineIL.Tests.AssemblyToProcess.MethodRefTestCases/UnaryOperatorsClass::set_Value(int32)
			IL_0007: ldarg.0
			IL_0008: ret
		} // end of method UnaryOperatorsClass::op_Decrement

		.method public hidebysig specialname static 
			bool op_False (
				class InlineIL.Tests.AssemblyToProcess.MethodRefTestCases/UnaryOperatorsClass s
			) cil managed 
		{
			// Header size: 1
			// Code size: 9 (0x9)
			.maxstack 8

			IL_0000: ldarg.0
			IL_0001: ldc.i4.7
			IL_0002: callvirt instance void InlineIL.Tests.AssemblyToProcess.MethodRefTestCases/UnaryOperatorsClass::set_Value(int32)
			IL_0007: ldc.i4.1
			IL_0008: ret
		} // end of method UnaryOperatorsClass::op_False

		.method public hidebysig specialname static 
			class InlineIL.Tests.AssemblyToProcess.MethodRefTestCases/UnaryOperatorsClass op_Increment (
				class InlineIL.Tests.AssemblyToProcess.MethodRefTestCases/UnaryOperatorsClass s
			) cil managed 
		{
			// Header size: 1
			// Code size: 9 (0x9)
			.maxstack 8

			IL_0000: ldarg.0
			IL_0001: ldc.i4.2
			IL_0002: callvirt instance void InlineIL.Tests.AssemblyToProcess.MethodRefTestCases/UnaryOperatorsClass::set_Value(int32)
			IL_0007: ldarg.0
			IL_0008: ret
		} // end of method UnaryOperatorsClass::op_Increment

		.method public hidebysig specialname static 
			class InlineIL.Tests.AssemblyToProcess.MethodRefTestCases/UnaryOperatorsClass op_LogicalNot (
				class InlineIL.Tests.AssemblyToProcess.MethodRefTestCases/UnaryOperatorsClass s
			) cil managed 
		{
			// Header size: 1
			// Code size: 9 (0x9)
			.maxstack 8

			IL_0000: ldarg.0
			IL_0001: ldc.i4.5
			IL_0002: callvirt instance void InlineIL.Tests.AssemblyToProcess.MethodRefTestCases/UnaryOperatorsClass::set_Value(int32)
			IL_0007: ldarg.0
			IL_0008: ret
		} // end of method UnaryOperatorsClass::op_LogicalNot

		.method public hidebysig specialname static 
			class InlineIL.Tests.AssemblyToProcess.MethodRefTestCases/UnaryOperatorsClass op_OnesComplement (
				class InlineIL.Tests.AssemblyToProcess.MethodRefTestCases/UnaryOperatorsClass s
			) cil managed 
		{
			// Header size: 1
			// Code size: 9 (0x9)
			.maxstack 8

			IL_0000: ldarg.0
			IL_0001: ldc.i4.8
			IL_0002: callvirt instance void InlineIL.Tests.AssemblyToProcess.MethodRefTestCases/UnaryOperatorsClass::set_Value(int32)
			IL_0007: ldarg.0
			IL_0008: ret
		} // end of method UnaryOperatorsClass::op_OnesComplement

		.method public hidebysig specialname static 
			bool op_True (
				class InlineIL.Tests.AssemblyToProcess.MethodRefTestCases/UnaryOperatorsClass s
			) cil managed 
		{
			// Header size: 1
			// Code size: 9 (0x9)
			.maxstack 8

			IL_0000: ldarg.0
			IL_0001: ldc.i4.6
			IL_0002: callvirt instance void InlineIL.Tests.AssemblyToProcess.MethodRefTestCases/UnaryOperatorsClass::set_Value(int32)
			IL_0007: ldc.i4.1
			IL_0008: ret
		} // end of method UnaryOperatorsClass::op_True

		.method public hidebysig specialname static 
			class InlineIL.Tests.AssemblyToProcess.MethodRefTestCases/UnaryOperatorsClass op_UnaryNegation (
				class InlineIL.Tests.AssemblyToProcess.MethodRefTestCases/UnaryOperatorsClass s
			) cil managed 
		{
			// Header size: 1
			// Code size: 9 (0x9)
			.maxstack 8

			IL_0000: ldarg.0
			IL_0001: ldc.i4.3
			IL_0002: callvirt instance void InlineIL.Tests.AssemblyToProcess.MethodRefTestCases/UnaryOperatorsClass::set_Value(int32)
			IL_0007: ldarg.0
			IL_0008: ret
		} // end of method UnaryOperatorsClass::op_UnaryNegation

		.method public hidebysig specialname static 
			class InlineIL.Tests.AssemblyToProcess.MethodRefTestCases/UnaryOperatorsClass op_UnaryPlus (
				class InlineIL.Tests.AssemblyToProcess.MethodRefTestCases/UnaryOperatorsClass s
			) cil managed 
		{
			// Header size: 1
			// Code size: 9 (0x9)
			.maxstack 8

			IL_0000: ldarg.0
			IL_0001: ldc.i4.4
			IL_0002: callvirt instance void InlineIL.Tests.AssemblyToProcess.MethodRefTestCases/UnaryOperatorsClass::set_Value(int32)
			IL_0007: ldarg.0
			IL_0008: ret
		} // end of method UnaryOperatorsClass::op_UnaryPlus

		.method private hidebysig specialname 
			instance void set_Value (
				int32 'value'
			) cil managed 
		{
			.custom instance void [netstandard]System.Runtime.CompilerServices.CompilerGeneratedAttribute::.ctor() = (
				01 00 00 00
			)
			// Header size: 1
			// Code size: 8 (0x8)
			.maxstack 8

			IL_0000: ldarg.0
			IL_0001: ldarg.1
			IL_0002: stfld int32 InlineIL.Tests.AssemblyToProcess.MethodRefTestCases/UnaryOperatorsClass::'<Value>k__BackingField'
			IL_0007: ret
		} // end of method UnaryOperatorsClass::set_Value

		// Properties
		.property instance int32 Value()
		{
			.get instance int32 InlineIL.Tests.AssemblyToProcess.MethodRefTestCases/UnaryOperatorsClass::get_Value()
			.set instance void InlineIL.Tests.AssemblyToProcess.MethodRefTestCases/UnaryOperatorsClass::set_Value(int32)
		}

	} // end of class UnaryOperatorsClass


	// Fields
	.field private initonly class InlineIL.Tests.AssemblyToProcess.MethodRefTestCases/OtherClass _otherClass
	.field private valuetype InlineIL.Tests.AssemblyToProcess.MethodRefTestCases/OtherStruct _otherStruct
	.field private int32 '<Value>k__BackingField'
	.custom instance void [netstandard]System.Runtime.CompilerServices.CompilerGeneratedAttribute::.ctor() = (
		01 00 00 00
	)
	.field private class [netstandard]System.Action Event
	.custom instance void [netstandard]System.Runtime.CompilerServices.CompilerGeneratedAttribute::.ctor() = (
		01 00 00 00
	)

	// Methods
	.method public hidebysig specialname rtspecialname 
		instance void .ctor () cil managed 
	{
		// Header size: 1
		// Code size: 18 (0x12)
		.maxstack 8

		IL_0000: ldarg.0
		IL_0001: newobj instance void InlineIL.Tests.AssemblyToProcess.MethodRefTestCases/OtherClass::.ctor()
		IL_0006: stfld class InlineIL.Tests.AssemblyToProcess.MethodRefTestCases/OtherClass InlineIL.Tests.AssemblyToProcess.MethodRefTestCases::_otherClass
		IL_000b: ldarg.0
		IL_000c: call instance void [netstandard]System.Object::.ctor()
		IL_0011: ret
	} // end of method MethodRefTestCases::.ctor

	.method assembly hidebysig static 
		int32 '<CallInstanceMethodOfStructFromDelegate4>g__Run|43_0' (
			valuetype InlineIL.Tests.AssemblyToProcess.MethodRefTestCases/OtherStruct& 'value'
		) cil managed 
	{
		.custom instance void [netstandard]System.Runtime.CompilerServices.CompilerGeneratedAttribute::.ctor() = (
			01 00 00 00
		)
		// Header size: 1
		// Code size: 9 (0x9)
		.maxstack 8

		IL_0000: ldarg.0
		IL_0001: ldc.i4.s 42
		IL_0003: call instance int32 InlineIL.Tests.AssemblyToProcess.MethodRefTestCases/OtherStruct::InstanceMethod(int32)
		IL_0008: ret
	} // end of method MethodRefTestCases::'<CallInstanceMethodOfStructFromDelegate4>g__Run|43_0'

	.method public final hidebysig specialname newslot virtual 
		instance void add_Event (
			class [netstandard]System.Action 'value'
		) cil managed 
	{
		.custom instance void [netstandard]System.Runtime.CompilerServices.CompilerGeneratedAttribute::.ctor() = (
			01 00 00 00
		)
		// Header size: 12
		// Code size: 41 (0x29)
		.maxstack 3
		.locals init (
			[0] class [netstandard]System.Action,
			[1] class [netstandard]System.Action,
			[2] class [netstandard]System.Action
		)

		IL_0000: ldarg.0
		IL_0001: ldfld class [netstandard]System.Action InlineIL.Tests.AssemblyToProcess.MethodRefTestCases::Event
		IL_0006: stloc.0
		// loop start (head: IL_0007)
			IL_0007: ldloc.0
			IL_0008: stloc.1
			IL_0009: ldloc.1
			IL_000a: ldarg.1
			IL_000b: call class [netstandard]System.Delegate [netstandard]System.Delegate::Combine(class [netstandard]System.Delegate, class [netstandard]System.Delegate)
			IL_0010: castclass [netstandard]System.Action
			IL_0015: stloc.2
			IL_0016: ldarg.0
			IL_0017: ldflda class [netstandard]System.Action InlineIL.Tests.AssemblyToProcess.MethodRefTestCases::Event
			IL_001c: ldloc.2
			IL_001d: ldloc.1
			IL_001e: call !!0 [netstandard]System.Threading.Interlocked::CompareExchange<class [netstandard]System.Action>(!!0&, !!0, !!0)
			IL_0023: stloc.0
			IL_0024: ldloc.0
			IL_0025: ldloc.1
			IL_0026: bne.un.s IL_0007
		// end loop
		IL_0028: ret
	} // end of method MethodRefTestCases::add_Event

	.method public final hidebysig newslot virtual 
		instance void AddEvent (
			class [netstandard]System.Action callback
		) cil managed 
	{
		// Header size: 1
		// Code size: 8 (0x8)
		.maxstack 8

		IL_0000: ldarg.0
		IL_0001: ldarg.1
		IL_0002: call instance void InlineIL.Tests.AssemblyToProcess.MethodRefTestCases::add_Event(class [netstandard]System.Action)
		IL_0007: ret
	} // end of method MethodRefTestCases::AddEvent

	.method public hidebysig 
		instance int32[] CallBinaryOperators () cil managed 
	{
		// Header size: 12
		// Code size: 424 (0x1a8)
		.maxstack 2
		.locals init (
			[0] class [netstandard]System.Collections.Generic.List`1<int32>,
			[1] int32
		)

		IL_0000: newobj instance void class [netstandard]System.Collections.Generic.List`1<int32>::.ctor()
		IL_0005: stloc.0
		IL_0006: newobj instance void InlineIL.Tests.AssemblyToProcess.MethodRefTestCases/BinaryOperatorsClass::.ctor()
		IL_000b: ldc.i4.s 42
		IL_000d: call int32 InlineIL.Tests.AssemblyToProcess.MethodRefTestCases/BinaryOperatorsClass::op_Addition(class InlineIL.Tests.AssemblyToProcess.MethodRefTestCases/BinaryOperatorsClass, int32)
		IL_0012: stloc.1
		IL_0013: ldloc.0
		IL_0014: ldloc.1
		IL_0015: callvirt instance void class [netstandard]System.Collections.Generic.List`1<int32>::Add(!0)
		IL_001a: ldc.i4.s 42
		IL_001c: newobj instance void InlineIL.Tests.AssemblyToProcess.MethodRefTestCases/BinaryOperatorsClass::.ctor()
		IL_0021: call int32 InlineIL.Tests.AssemblyToProcess.MethodRefTestCases/BinaryOperatorsClass::op_Addition(int32, class InlineIL.Tests.AssemblyToProcess.MethodRefTestCases/BinaryOperatorsClass)
		IL_0026: stloc.1
		IL_0027: ldloc.0
		IL_0028: ldloc.1
		IL_0029: callvirt instance void class [netstandard]System.Collections.Generic.List`1<int32>::Add(!0)
		IL_002e: newobj instance void InlineIL.Tests.AssemblyToProcess.MethodRefTestCases/BinaryOperatorsClass::.ctor()
		IL_0033: newobj instance void InlineIL.Tests.AssemblyToProcess.MethodRefTestCases/BinaryOperatorsClass::.ctor()
		IL_0038: call int32 InlineIL.Tests.AssemblyToProcess.MethodRefTestCases/BinaryOperatorsClass::op_Addition(class InlineIL.Tests.AssemblyToProcess.MethodRefTestCases/BinaryOperatorsClass, class InlineIL.Tests.AssemblyToProcess.MethodRefTestCases/BinaryOperatorsClass)
		IL_003d: stloc.1
		IL_003e: ldloc.0
		IL_003f: ldloc.1
		IL_0040: callvirt instance void class [netstandard]System.Collections.Generic.List`1<int32>::Add(!0)
		IL_0045: newobj instance void InlineIL.Tests.AssemblyToProcess.MethodRefTestCases/BinaryOperatorsClass::.ctor()
		IL_004a: ldc.i4.s 42
		IL_004c: call int32 InlineIL.Tests.AssemblyToProcess.MethodRefTestCases/BinaryOperatorsClass::op_Subtraction(class InlineIL.Tests.AssemblyToProcess.MethodRefTestCases/BinaryOperatorsClass, int32)
		IL_0051: stloc.1
		IL_0052: ldloc.0
		IL_0053: ldloc.1
		IL_0054: callvirt instance void class [netstandard]System.Collections.Generic.List`1<int32>::Add(!0)
		IL_0059: newobj instance void InlineIL.Tests.AssemblyToProcess.MethodRefTestCases/BinaryOperatorsClass::.ctor()
		IL_005e: ldc.i4.s 42
		IL_0060: call int32 InlineIL.Tests.AssemblyToProcess.MethodRefTestCases/BinaryOperatorsClass::op_Multiply(class InlineIL.Tests.AssemblyToProcess.MethodRefTestCases/BinaryOperatorsClass, int32)
		IL_0065: stloc.1
		IL_0066: ldloc.0
		IL_0067: ldloc.1
		IL_0068: callvirt instance void class [netstandard]System.Collections.Generic.List`1<int32>::Add(!0)
		IL_006d: newobj instance void InlineIL.Tests.AssemblyToProcess.MethodRefTestCases/BinaryOperatorsClass::.ctor()
		IL_0072: ldc.i4.s 42
		IL_0074: call int32 InlineIL.Tests.AssemblyToProcess.MethodRefTestCases/BinaryOperatorsClass::op_Division(class InlineIL.Tests.AssemblyToProcess.MethodRefTestCases/BinaryOperatorsClass, int32)
		IL_0079: stloc.1
		IL_007a: ldloc.0
		IL_007b: ldloc.1
		IL_007c: callvirt instance void class [netstandard]System.Collections.Generic.List`1<int32>::Add(!0)
		IL_0081: newobj instance void InlineIL.Tests.AssemblyToProcess.MethodRefTestCases/BinaryOperatorsClass::.ctor()
		IL_0086: ldc.i4.s 42
		IL_0088: call int32 InlineIL.Tests.AssemblyToProcess.MethodRefTestCases/BinaryOperatorsClass::op_Modulus(class InlineIL.Tests.AssemblyToProcess.MethodRefTestCases/BinaryOperatorsClass, int32)
		IL_008d: stloc.1
		IL_008e: ldloc.0
		IL_008f: ldloc.1
		IL_0090: callvirt instance void class [netstandard]System.Collections.Generic.List`1<int32>::Add(!0)
		IL_0095: newobj instance void InlineIL.Tests.AssemblyToProcess.MethodRefTestCases/BinaryOperatorsClass::.ctor()
		IL_009a: ldc.i4.s 42
		IL_009c: call int32 InlineIL.Tests.AssemblyToProcess.MethodRefTestCases/BinaryOperatorsClass::op_ExclusiveOr(class InlineIL.Tests.AssemblyToProcess.MethodRefTestCases/BinaryOperatorsClass, int32)
		IL_00a1: stloc.1
		IL_00a2: ldloc.0
		IL_00a3: ldloc.1
		IL_00a4: callvirt instance void class [netstandard]System.Collections.Generic.List`1<int32>::Add(!0)
		IL_00a9: newobj instance void InlineIL.Tests.AssemblyToProcess.MethodRefTestCases/BinaryOperatorsClass::.ctor()
		IL_00ae: ldc.i4.s 42
		IL_00b0: call int32 InlineIL.Tests.AssemblyToProcess.MethodRefTestCases/BinaryOperatorsClass::op_BitwiseAnd(class InlineIL.Tests.AssemblyToProcess.MethodRefTestCases/BinaryOperatorsClass, int32)
		IL_00b5: stloc.1
		IL_00b6: ldloc.0
		IL_00b7: ldloc.1
		IL_00b8: callvirt instance void class [netstandard]System.Collections.Generic.List`1<int32>::Add(!0)
		IL_00bd: newobj instance void InlineIL.Tests.AssemblyToProcess.MethodRefTestCases/BinaryOperatorsClass::.ctor()
		IL_00c2: ldc.i4.s 42
		IL_00c4: call int32 InlineIL.Tests.AssemblyToProcess.MethodRefTestCases/BinaryOperatorsClass::op_BitwiseOr(class InlineIL.Tests.AssemblyToProcess.MethodRefTestCases/BinaryOperatorsClass, int32)
		IL_00c9: stloc.1
		IL_00ca: ldloc.0
		IL_00cb: ldloc.1
		IL_00cc: callvirt instance void class [netstandard]System.Collections.Generic.List`1<int32>::Add(!0)
		IL_00d1: newobj instance void InlineIL.Tests.AssemblyToProcess.MethodRefTestCases/BinaryOperatorsClass::.ctor()
		IL_00d6: ldc.i4.s 42
		IL_00d8: call int32 InlineIL.Tests.AssemblyToProcess.MethodRefTestCases/BinaryOperatorsClass::op_LeftShift(class InlineIL.Tests.AssemblyToProcess.MethodRefTestCases/BinaryOperatorsClass, int32)
		IL_00dd: stloc.1
		IL_00de: ldloc.0
		IL_00df: ldloc.1
		IL_00e0: callvirt instance void class [netstandard]System.Collections.Generic.List`1<int32>::Add(!0)
		IL_00e5: newobj instance void InlineIL.Tests.AssemblyToProcess.MethodRefTestCases/BinaryOperatorsClass::.ctor()
		IL_00ea: ldc.i4.s 42
		IL_00ec: call int32 InlineIL.Tests.AssemblyToProcess.MethodRefTestCases/BinaryOperatorsClass::op_RightShift(class InlineIL.Tests.AssemblyToProcess.MethodRefTestCases/BinaryOperatorsClass, int32)
		IL_00f1: stloc.1
		IL_00f2: ldloc.0
		IL_00f3: ldloc.1
		IL_00f4: callvirt instance void class [netstandard]System.Collections.Generic.List`1<int32>::Add(!0)
		IL_00f9: newobj instance void InlineIL.Tests.AssemblyToProcess.MethodRefTestCases/BinaryOperatorsClass::.ctor()
		IL_00fe: ldc.i4.s 42
		IL_0100: call int32 InlineIL.Tests.AssemblyToProcess.MethodRefTestCases/BinaryOperatorsClass::op_Equality(class InlineIL.Tests.AssemblyToProcess.MethodRefTestCases/BinaryOperatorsClass, int32)
		IL_0105: stloc.1
		IL_0106: ldloc.0
		IL_0107: ldloc.1
		IL_0108: callvirt instance void class [netstandard]System.Collections.Generic.List`1<int32>::Add(!0)
		IL_010d: newobj instance void InlineIL.Tests.AssemblyToProcess.MethodRefTestCases/BinaryOperatorsClass::.ctor()
		IL_0112: ldc.i4.s 42
		IL_0114: call int32 InlineIL.Tests.AssemblyToProcess.MethodRefTestCases/BinaryOperatorsClass::op_GreaterThan(class InlineIL.Tests.AssemblyToProcess.MethodRefTestCases/BinaryOperatorsClass, int32)
		IL_0119: stloc.1
		IL_011a: ldloc.0
		IL_011b: ldloc.1
		IL_011c: callvirt instance void class [netstandard]System.Collections.Generic.List`1<int32>::Add(!0)
		IL_0121: newobj instance void InlineIL.Tests.AssemblyToProcess.MethodRefTestCases/BinaryOperatorsClass::.ctor()
		IL_0126: ldc.i4.s 42
		IL_0128: call int32 InlineIL.Tests.AssemblyToProcess.MethodRefTestCases/BinaryOperatorsClass::op_LessThan(class InlineIL.Tests.AssemblyToProcess.MethodRefTestCases/BinaryOperatorsClass, int32)
		IL_012d: stloc.1
		IL_012e: ldloc.0
		IL_012f: ldloc.1
		IL_0130: callvirt instance void class [netstandard]System.Collections.Generic.List`1<int32>::Add(!0)
		IL_0135: newobj instance void InlineIL.Tests.AssemblyToProcess.MethodRefTestCases/BinaryOperatorsClass::.ctor()
		IL_013a: ldc.i4.s 42
		IL_013c: call int32 InlineIL.Tests.AssemblyToProcess.MethodRefTestCases/BinaryOperatorsClass::op_Inequality(class InlineIL.Tests.AssemblyToProcess.MethodRefTestCases/BinaryOperatorsClass, int32)
		IL_0141: stloc.1
		IL_0142: ldloc.0
		IL_0143: ldloc.1
		IL_0144: callvirt instance void class [netstandard]System.Collections.Generic.List`1<int32>::Add(!0)
		IL_0149: newobj instance void InlineIL.Tests.AssemblyToProcess.MethodRefTestCases/BinaryOperatorsClass::.ctor()
		IL_014e: ldc.i4.s 42
		IL_0150: call int32 InlineIL.Tests.AssemblyToProcess.MethodRefTestCases/BinaryOperatorsClass::op_GreaterThanOrEqual(class InlineIL.Tests.AssemblyToProcess.MethodRefTestCases/BinaryOperatorsClass, int32)
		IL_0155: stloc.1
		IL_0156: ldloc.0
		IL_0157: ldloc.1
		IL_0158: callvirt instance void class [netstandard]System.Collections.Generic.List`1<int32>::Add(!0)
		IL_015d: newobj instance void InlineIL.Tests.AssemblyToProcess.MethodRefTestCases/BinaryOperatorsClass::.ctor()
		IL_0162: ldc.i4.s 42
		IL_0164: call int32 InlineIL.Tests.AssemblyToProcess.MethodRefTestCases/BinaryOperatorsClass::op_LessThanOrEqual(class InlineIL.Tests.AssemblyToProcess.MethodRefTestCases/BinaryOperatorsClass, int32)
		IL_0169: stloc.1
		IL_016a: ldloc.0
		IL_016b: ldloc.1
		IL_016c: callvirt instance void class [netstandard]System.Collections.Generic.List`1<int32>::Add(!0)
		IL_0171: ldstr "foo"
		IL_0176: ldc.i4.0
		IL_0177: newobj instance void class InlineIL.Tests.AssemblyToProcess.MethodRefTestCases/GenericOperatorsClass`1<string>::.ctor(int32)
		IL_017c: call int32 class InlineIL.Tests.AssemblyToProcess.MethodRefTestCases/GenericOperatorsClass`1<string>::op_Addition(!0, class InlineIL.Tests.AssemblyToProcess.MethodRefTestCases/GenericOperatorsClass`1<!0>)
		IL_0181: stloc.1
		IL_0182: ldloc.0
		IL_0183: ldloc.1
		IL_0184: callvirt instance void class [netstandard]System.Collections.Generic.List`1<int32>::Add(!0)
		IL_0189: ldc.i4.0
		IL_018a: newobj instance void class InlineIL.Tests.AssemblyToProcess.MethodRefTestCases/GenericOperatorsClass`1<string>::.ctor(int32)
		IL_018f: ldstr "foo"
		IL_0194: call int32 class InlineIL.Tests.AssemblyToProcess.MethodRefTestCases/GenericOperatorsClass`1<string>::op_Addition(class InlineIL.Tests.AssemblyToProcess.MethodRefTestCases/GenericOperatorsClass`1<!0>, !0)
		IL_0199: stloc.1
		IL_019a: ldloc.0
		IL_019b: ldloc.1
		IL_019c: callvirt instance void class [netstandard]System.Collections.Generic.List`1<int32>::Add(!0)
		IL_01a1: ldloc.0
		IL_01a2: callvirt instance !0[] class [netstandard]System.Collections.Generic.List`1<int32>::ToArray()
		IL_01a7: ret
	} // end of method MethodRefTestCases::CallBinaryOperators

	.method public hidebysig 
		instance int32[] CallConversionOperators () cil managed 
	{
		// Header size: 12
		// Code size: 253 (0xfd)
		.maxstack 2
		.locals init (
			[0] class [netstandard]System.Collections.Generic.List`1<int32>,
			[1] int32,
			[2] class InlineIL.Tests.AssemblyToProcess.MethodRefTestCases/ConversionOperatorsClass,
			[3] int64,
			[4] int16,
			[5] class InlineIL.Tests.AssemblyToProcess.MethodRefTestCases/GenericOperatorsClass`1<string>
		)

		IL_0000: newobj instance void class [netstandard]System.Collections.Generic.List`1<int32>::.ctor()
		IL_0005: stloc.0
		IL_0006: ldc.i4.0
		IL_0007: newobj instance void InlineIL.Tests.AssemblyToProcess.MethodRefTestCases/ConversionOperatorsClass::.ctor(int32)
		IL_000c: call int32 InlineIL.Tests.AssemblyToProcess.MethodRefTestCases/ConversionOperatorsClass::op_Implicit(class InlineIL.Tests.AssemblyToProcess.MethodRefTestCases/ConversionOperatorsClass)
		IL_0011: stloc.1
		IL_0012: ldloc.0
		IL_0013: ldloc.1
		IL_0014: callvirt instance void class [netstandard]System.Collections.Generic.List`1<int32>::Add(!0)
		IL_0019: ldc.i4.0
		IL_001a: call class InlineIL.Tests.AssemblyToProcess.MethodRefTestCases/ConversionOperatorsClass InlineIL.Tests.AssemblyToProcess.MethodRefTestCases/ConversionOperatorsClass::op_Implicit(int32)
		IL_001f: stloc.2
		IL_0020: ldloc.0
		IL_0021: ldloc.2
		IL_0022: callvirt instance int32 InlineIL.Tests.AssemblyToProcess.MethodRefTestCases/ConversionOperatorsClass::get_Value()
		IL_0027: callvirt instance void class [netstandard]System.Collections.Generic.List`1<int32>::Add(!0)
		IL_002c: ldc.i4.0
		IL_002d: newobj instance void InlineIL.Tests.AssemblyToProcess.MethodRefTestCases/ConversionOperatorsClass::.ctor(int32)
		IL_0032: call int64 InlineIL.Tests.AssemblyToProcess.MethodRefTestCases/ConversionOperatorsClass::op_Implicit(class InlineIL.Tests.AssemblyToProcess.MethodRefTestCases/ConversionOperatorsClass)
		IL_0037: stloc.3
		IL_0038: ldloc.0
		IL_0039: ldloc.3
		IL_003a: conv.i4
		IL_003b: callvirt instance void class [netstandard]System.Collections.Generic.List`1<int32>::Add(!0)
		IL_0040: ldc.i4.0
		IL_0041: conv.i8
		IL_0042: call class InlineIL.Tests.AssemblyToProcess.MethodRefTestCases/ConversionOperatorsClass InlineIL.Tests.AssemblyToProcess.MethodRefTestCases/ConversionOperatorsClass::op_Implicit(int64)
		IL_0047: stloc.2
		IL_0048: ldloc.0
		IL_0049: ldloc.2
		IL_004a: callvirt instance int32 InlineIL.Tests.AssemblyToProcess.MethodRefTestCases/ConversionOperatorsClass::get_Value()
		IL_004f: callvirt instance void class [netstandard]System.Collections.Generic.List`1<int32>::Add(!0)
		IL_0054: ldc.i4.0
		IL_0055: newobj instance void InlineIL.Tests.AssemblyToProcess.MethodRefTestCases/ConversionOperatorsClass::.ctor(int32)
		IL_005a: call int16 InlineIL.Tests.AssemblyToProcess.MethodRefTestCases/ConversionOperatorsClass::op_Explicit(class InlineIL.Tests.AssemblyToProcess.MethodRefTestCases/ConversionOperatorsClass)
		IL_005f: stloc.s 4
		IL_0061: ldloc.0
		IL_0062: ldloc.s 4
		IL_0064: callvirt instance void class [netstandard]System.Collections.Generic.List`1<int32>::Add(!0)
		IL_0069: ldc.i4.0
		IL_006a: call class InlineIL.Tests.AssemblyToProcess.MethodRefTestCases/ConversionOperatorsClass InlineIL.Tests.AssemblyToProcess.MethodRefTestCases/ConversionOperatorsClass::op_Explicit(int16)
		IL_006f: stloc.2
		IL_0070: ldloc.0
		IL_0071: ldloc.2
		IL_0072: callvirt instance int32 InlineIL.Tests.AssemblyToProcess.MethodRefTestCases/ConversionOperatorsClass::get_Value()
		IL_0077: callvirt instance void class [netstandard]System.Collections.Generic.List`1<int32>::Add(!0)
		IL_007c: ldstr "foo"
		IL_0081: call class InlineIL.Tests.AssemblyToProcess.MethodRefTestCases/GenericOperatorsClass`1<!0> class InlineIL.Tests.AssemblyToProcess.MethodRefTestCases/GenericOperatorsClass`1<string>::op_Implicit(!0)
		IL_0086: stloc.s 5
		IL_0088: ldloc.0
		IL_0089: ldloc.s 5
		IL_008b: callvirt instance int32 class InlineIL.Tests.AssemblyToProcess.MethodRefTestCases/GenericOperatorsClass`1<string>::get_Value()
		IL_0090: callvirt instance void class [netstandard]System.Collections.Generic.List`1<int32>::Add(!0)
		IL_0095: ldc.i4.0
		IL_0096: newobj instance void class InlineIL.Tests.AssemblyToProcess.MethodRefTestCases/GenericOperatorsClass`1<string>::.ctor(int32)
		IL_009b: stloc.s 5
		IL_009d: ldloc.s 5
		IL_009f: call !0 class InlineIL.Tests.AssemblyToProcess.MethodRefTestCases/GenericOperatorsClass`1<string>::op_Implicit(class InlineIL.Tests.AssemblyToProcess.MethodRefTestCases/GenericOperatorsClass`1<!0>)
		IL_00a4: pop
		IL_00a5: ldloc.0
		IL_00a6: ldloc.s 5
		IL_00a8: callvirt instance int32 class InlineIL.Tests.AssemblyToProcess.MethodRefTestCases/GenericOperatorsClass`1<string>::get_Value()
		IL_00ad: callvirt instance void class [netstandard]System.Collections.Generic.List`1<int32>::Add(!0)
		IL_00b2: ldc.i4.0
		IL_00b3: newobj instance void InlineIL.Tests.AssemblyToProcess.MethodRefTestCases/ConversionOperatorsClass::.ctor(int32)
		IL_00b8: call int32 InlineIL.Tests.AssemblyToProcess.MethodRefTestCases/ConversionOperatorsClass::op_Implicit(class InlineIL.Tests.AssemblyToProcess.MethodRefTestCases/ConversionOperatorsClass)
		IL_00bd: stloc.1
		IL_00be: ldloc.0
		IL_00bf: ldloc.1
		IL_00c0: callvirt instance void class [netstandard]System.Collections.Generic.List`1<int32>::Add(!0)
		IL_00c5: ldc.i4.0
		IL_00c6: newobj instance void InlineIL.Tests.AssemblyToProcess.MethodRefTestCases/ConversionOperatorsClass::.ctor(int32)
		IL_00cb: call int64 InlineIL.Tests.AssemblyToProcess.MethodRefTestCases/ConversionOperatorsClass::op_Implicit(class InlineIL.Tests.AssemblyToProcess.MethodRefTestCases/ConversionOperatorsClass)
		IL_00d0: stloc.3
		IL_00d1: ldloc.0
		IL_00d2: ldloc.3
		IL_00d3: conv.i4
		IL_00d4: callvirt instance void class [netstandard]System.Collections.Generic.List`1<int32>::Add(!0)
		IL_00d9: ldc.i4.0
		IL_00da: newobj instance void class InlineIL.Tests.AssemblyToProcess.MethodRefTestCases/GenericOperatorsClass`1<string>::.ctor(int32)
		IL_00df: stloc.s 5
		IL_00e1: ldloc.s 5
		IL_00e3: call !0 class InlineIL.Tests.AssemblyToProcess.MethodRefTestCases/GenericOperatorsClass`1<string>::op_Implicit(class InlineIL.Tests.AssemblyToProcess.MethodRefTestCases/GenericOperatorsClass`1<!0>)
		IL_00e8: pop
		IL_00e9: ldloc.0
		IL_00ea: ldloc.s 5
		IL_00ec: callvirt instance int32 class InlineIL.Tests.AssemblyToProcess.MethodRefTestCases/GenericOperatorsClass`1<string>::get_Value()
		IL_00f1: callvirt instance void class [netstandard]System.Collections.Generic.List`1<int32>::Add(!0)
		IL_00f6: ldloc.0
		IL_00f7: callvirt instance !0[] class [netstandard]System.Collections.Generic.List`1<int32>::ToArray()
		IL_00fc: ret
	} // end of method MethodRefTestCases::CallConversionOperators

	.method public hidebysig 
		instance bool CallCoreLibMethod<valuetype .ctor ([netstandard]System.ValueType) T> (
			valuetype [netstandard]System.Nullable`1<!!T> 'value'
		) cil managed 
	{
		// Header size: 1
		// Code size: 8 (0x8)
		.maxstack 8

		IL_0000: ldarga.s 'value'
		IL_0002: call instance bool valuetype [netstandard]System.Nullable`1<!!T>::get_HasValue()
		IL_0007: ret
	} // end of method MethodRefTestCases::CallCoreLibMethod

	.method public hidebysig 
		instance class [netstandard]System.Text.StringBuilder CallDefaultConstructor () cil managed 
	{
		// Header size: 1
		// Code size: 6 (0x6)
		.maxstack 8

		IL_0000: newobj instance void [netstandard]System.Text.StringBuilder::.ctor()
		IL_0005: ret
	} // end of method MethodRefTestCases::CallDefaultConstructor

	.method public hidebysig 
		instance int32 CallGenericMethod () cil managed 
	{
		// Header size: 12
		// Code size: 8 (0x8)
		.maxstack 1

		IL_0000: ldc.i4.s 42
		IL_0002: call !!0 InlineIL.Tests.AssemblyToProcess.MethodRefTestCases::GenericMethod<int32>(!!0)
		IL_0007: ret
	} // end of method MethodRefTestCases::CallGenericMethod

	.method public hidebysig 
		instance string CallGenericMethodInGenericType () cil managed 
	{
		// Header size: 12
		// Code size: 6 (0x6)
		.maxstack 1

		IL_0000: call string class InlineIL.Tests.AssemblyToProcess.MethodRefTestCases/GenericType`1<valuetype [netstandard]System.Guid>::GenericMethod<valuetype [netstandard]System.TimeSpan>()
		IL_0005: ret
	} // end of method MethodRefTestCases::CallGenericMethodInGenericType

	.method public hidebysig 
		instance string CallGenericMethodInGenericTypeArray () cil managed 
	{
		// Header size: 12
		// Code size: 6 (0x6)
		.maxstack 1

		IL_0000: call string class InlineIL.Tests.AssemblyToProcess.MethodRefTestCases/GenericType`1<valuetype [netstandard]System.Guid[]>::GenericMethod<valuetype [netstandard]System.TimeSpan[]>()
		IL_0005: ret
	} // end of method MethodRefTestCases::CallGenericMethodInGenericTypeArray

	.method public hidebysig 
		instance string CallGenericMethodInGenericTypeGeneric<TClass, TMethod> () cil managed 
	{
		// Header size: 12
		// Code size: 6 (0x6)
		.maxstack 1

		IL_0000: call string class InlineIL.Tests.AssemblyToProcess.MethodRefTestCases/GenericType`1<!!TClass>::GenericMethod<!!TMethod>()
		IL_0005: ret
	} // end of method MethodRefTestCases::CallGenericMethodInGenericTypeGeneric

	.method public hidebysig 
		instance string CallGenericStaticMethodOfGenericClassFromDelegate () cil managed 
	{
		// Header size: 1
		// Code size: 6 (0x6)
		.maxstack 8

		IL_0000: call string class InlineIL.Tests.AssemblyToProcess.MethodRefTestCases/GenericType`1<string>::GenericMethod<int32>()
		IL_0005: ret
	} // end of method MethodRefTestCases::CallGenericStaticMethodOfGenericClassFromDelegate

	.method public hidebysig 
		instance int32 CallInstanceMethodFromDelegate () cil managed 
	{
		// Header size: 1
		// Code size: 9 (0x9)
		.maxstack 8

		IL_0000: ldarg.0
		IL_0001: ldc.i4.s 42
		IL_0003: call instance int32 InlineIL.Tests.AssemblyToProcess.MethodRefTestCases::InstanceMethod(int32)
		IL_0008: ret
	} // end of method MethodRefTestCases::CallInstanceMethodFromDelegate

	.method public hidebysig 
		instance string CallInstanceMethodOfDoubleFromDelegate () cil managed 
	{
		// Header size: 1
		// Code size: 20 (0x14)
		.maxstack 8

		IL_0000: ldc.r8 42
		IL_0009: box [netstandard]System.Double
		IL_000e: callvirt instance string [netstandard]System.Object::ToString()
		IL_0013: ret
	} // end of method MethodRefTestCases::CallInstanceMethodOfDoubleFromDelegate

	.method public hidebysig 
		instance string CallInstanceMethodOfFloatFromDelegate () cil managed 
	{
		// Header size: 1
		// Code size: 16 (0x10)
		.maxstack 8

		IL_0000: ldc.r4 42
		IL_0005: box [netstandard]System.Single
		IL_000a: callvirt instance string [netstandard]System.Object::ToString()
		IL_000f: ret
	} // end of method MethodRefTestCases::CallInstanceMethodOfFloatFromDelegate

	.method public hidebysig 
		instance string CallInstanceMethodOfInt32FromDelegate () cil managed 
	{
		// Header size: 1
		// Code size: 13 (0xd)
		.maxstack 8

		IL_0000: ldc.i4.s 42
		IL_0002: box [netstandard]System.Int32
		IL_0007: callvirt instance string [netstandard]System.Object::ToString()
		IL_000c: ret
	} // end of method MethodRefTestCases::CallInstanceMethodOfInt32FromDelegate

	.method public hidebysig 
		instance bool CallInstanceMethodOfInt32FromDelegate2 () cil managed 
	{
		// Header size: 12
		// Code size: 13 (0xd)
		.maxstack 2
		.locals init (
			[0] int32
		)

		IL_0000: ldc.i4.s 42
		IL_0002: stloc.0
		IL_0003: ldloca.s 0
		IL_0005: ldc.i4.s 42
		IL_0007: callvirt instance bool [netstandard]System.Int32::Equals(int32)
		IL_000c: ret
	} // end of method MethodRefTestCases::CallInstanceMethodOfInt32FromDelegate2

	.method public hidebysig 
		instance string CallInstanceMethodOfInt32WithSizeofFromDelegate () cil managed 
	{
		// Header size: 1
		// Code size: 13 (0xd)
		.maxstack 8

		IL_0000: ldc.i4.s 42
		IL_0002: box [netstandard]System.Int32
		IL_0007: callvirt instance string [netstandard]System.Object::ToString()
		IL_000c: ret
	} // end of method MethodRefTestCases::CallInstanceMethodOfInt32WithSizeofFromDelegate

	.method public hidebysig 
		instance string CallInstanceMethodOfInt64FromDelegate () cil managed 
	{
		// Header size: 1
		// Code size: 14 (0xe)
		.maxstack 8

		IL_0000: ldc.i4.s 42
		IL_0002: conv.i8
		IL_0003: box [netstandard]System.Int64
		IL_0008: callvirt instance string [netstandard]System.Object::ToString()
		IL_000d: ret
	} // end of method MethodRefTestCases::CallInstanceMethodOfInt64FromDelegate

	.method public hidebysig 
		instance string CallInstanceMethodOfInt64FromDelegate2 () cil managed 
	{
		// Header size: 1
		// Code size: 20 (0x14)
		.maxstack 8

		IL_0000: ldc.i8 424242424242
		IL_0009: box [netstandard]System.Int64
		IL_000e: callvirt instance string [netstandard]System.Object::ToString()
		IL_0013: ret
	} // end of method MethodRefTestCases::CallInstanceMethodOfInt64FromDelegate2

	.method public hidebysig 
		instance int32 CallInstanceMethodOfOtherClassFromDelegate () cil managed 
	{
		// Header size: 12
		// Code size: 16 (0x10)
		.maxstack 2
		.locals init (
			[0] class InlineIL.Tests.AssemblyToProcess.MethodRefTestCases/OtherClass
		)

		IL_0000: ldloca.s 0
		IL_0002: call void InlineIL.Tests.AssemblyToProcess.MethodRefTestCases/OtherClass::Create(class InlineIL.Tests.AssemblyToProcess.MethodRefTestCases/OtherClass&)
		IL_0007: ldloc.0
		IL_0008: ldc.i4.s 42
		IL_000a: call instance int32 InlineIL.Tests.AssemblyToProcess.MethodRefTestCases/OtherClass::InstanceMethod(int32)
		IL_000f: ret
	} // end of method MethodRefTestCases::CallInstanceMethodOfOtherClassFromDelegate

	.method public hidebysig 
		instance int32 CallInstanceMethodOfOtherClassThroughFieldFromDelegate () cil managed 
	{
		// Header size: 1
		// Code size: 14 (0xe)
		.maxstack 8

		IL_0000: ldarg.0
		IL_0001: ldfld class InlineIL.Tests.AssemblyToProcess.MethodRefTestCases/OtherClass InlineIL.Tests.AssemblyToProcess.MethodRefTestCases::_otherClass
		IL_0006: ldc.i4.s 42
		IL_0008: call instance int32 InlineIL.Tests.AssemblyToProcess.MethodRefTestCases/OtherClass::InstanceMethod(int32)
		IL_000d: ret
	} // end of method MethodRefTestCases::CallInstanceMethodOfOtherClassThroughFieldFromDelegate

	.method public hidebysig 
		instance string CallInstanceMethodOfStringFromDelegate () cil managed 
	{
		// Header size: 1
		// Code size: 11 (0xb)
		.maxstack 8

		IL_0000: ldstr " foo "
		IL_0005: call instance string [netstandard]System.String::Trim()
		IL_000a: ret
	} // end of method MethodRefTestCases::CallInstanceMethodOfStringFromDelegate

	.method public hidebysig 
		instance int32 CallInstanceMethodOfStructFromDelegate () cil managed 
	{
		// Header size: 12
		// Code size: 18 (0x12)
		.maxstack 2
		.locals init (
			[0] valuetype InlineIL.Tests.AssemblyToProcess.MethodRefTestCases/OtherStruct
		)

		IL_0000: ldloca.s 0
		IL_0002: initobj InlineIL.Tests.AssemblyToProcess.MethodRefTestCases/OtherStruct
		IL_0008: ldloca.s 0
		IL_000a: ldc.i4.s 42
		IL_000c: call instance int32 InlineIL.Tests.AssemblyToProcess.MethodRefTestCases/OtherStruct::InstanceMethod(int32)
		IL_0011: ret
	} // end of method MethodRefTestCases::CallInstanceMethodOfStructFromDelegate

	.method public hidebysig 
		instance int32 CallInstanceMethodOfStructFromDelegate2 () cil managed 
	{
		// Header size: 12
		// Code size: 26 (0x1a)
		.maxstack 3
		.locals init (
			[0] valuetype InlineIL.Tests.AssemblyToProcess.MethodRefTestCases/OtherStruct,
			[1] valuetype InlineIL.Tests.AssemblyToProcess.MethodRefTestCases/OtherStruct
		)

		IL_0000: ldloca.s 0
		IL_0002: initobj InlineIL.Tests.AssemblyToProcess.MethodRefTestCases/OtherStruct
		IL_0008: ldloca.s 0
		IL_000a: ldc.i4.s 42
		IL_000c: ldloca.s 1
		IL_000e: initobj InlineIL.Tests.AssemblyToProcess.MethodRefTestCases/OtherStruct
		IL_0014: call instance int32 InlineIL.Tests.AssemblyToProcess.MethodRefTestCases/OtherStruct::InstanceMethod(int32)
		IL_0019: ret
	} // end of method MethodRefTestCases::CallInstanceMethodOfStructFromDelegate2

	.method public hidebysig 
		instance int32 CallInstanceMethodOfStructFromDelegate3 () cil managed 
	{
		// Header size: 12
		// Code size: 26 (0x1a)
		.maxstack 3
		.locals init (
			[0] valuetype InlineIL.Tests.AssemblyToProcess.MethodRefTestCases/OtherStruct,
			[1] valuetype InlineIL.Tests.AssemblyToProcess.MethodRefTestCases/OtherStruct
		)

		IL_0000: ldloca.s 0
		IL_0002: initobj InlineIL.Tests.AssemblyToProcess.MethodRefTestCases/OtherStruct
		IL_0008: ldloca.s 0
		IL_000a: ldc.i4.s 42
		IL_000c: ldloca.s 1
		IL_000e: initobj InlineIL.Tests.AssemblyToProcess.MethodRefTestCases/OtherStruct
		IL_0014: call instance int32 InlineIL.Tests.AssemblyToProcess.MethodRefTestCases/OtherStruct::InstanceMethod(int32)
		IL_0019: ret
	} // end of method MethodRefTestCases::CallInstanceMethodOfStructFromDelegate3

	.method public hidebysig 
		instance int32 CallInstanceMethodOfStructFromDelegate4 () cil managed 
	{
		// Header size: 12
		// Code size: 16 (0x10)
		.maxstack 1
		.locals init (
			[0] valuetype InlineIL.Tests.AssemblyToProcess.MethodRefTestCases/OtherStruct
		)

		IL_0000: ldloca.s 0
		IL_0002: initobj InlineIL.Tests.AssemblyToProcess.MethodRefTestCases/OtherStruct
		IL_0008: ldloca.s 0
		IL_000a: call int32 InlineIL.Tests.AssemblyToProcess.MethodRefTestCases::'<CallInstanceMethodOfStructFromDelegate4>g__Run|43_0'(valuetype InlineIL.Tests.AssemblyToProcess.MethodRefTestCases/OtherStruct&)
		IL_000f: ret
	} // end of method MethodRefTestCases::CallInstanceMethodOfStructFromDelegate4

	.method public hidebysig 
		instance int32 CallInstanceMethodOfStructThroughFieldFromDelegate () cil managed 
	{
		// Header size: 1
		// Code size: 14 (0xe)
		.maxstack 8

		IL_0000: ldarg.0
		IL_0001: ldflda valuetype InlineIL.Tests.AssemblyToProcess.MethodRefTestCases/OtherStruct InlineIL.Tests.AssemblyToProcess.MethodRefTestCases::_otherStruct
		IL_0006: ldc.i4.s 42
		IL_0008: call instance int32 InlineIL.Tests.AssemblyToProcess.MethodRefTestCases/OtherStruct::InstanceMethod(int32)
		IL_000d: ret
	} // end of method MethodRefTestCases::CallInstanceMethodOfStructThroughFieldFromDelegate

	.method public hidebysig 
		instance int32 CallInterfaceMethodOfStructFromDelegate () cil managed 
	{
		// Header size: 12
		// Code size: 18 (0x12)
		.maxstack 2
		.locals init (
			[0] valuetype InlineIL.Tests.AssemblyToProcess.MethodRefTestCases/OtherStruct
		)

		IL_0000: ldloca.s 0
		IL_0002: initobj InlineIL.Tests.AssemblyToProcess.MethodRefTestCases/OtherStruct
		IL_0008: ldloca.s 0
		IL_000a: ldc.i4.s 42
		IL_000c: call instance int32 InlineIL.Tests.AssemblyToProcess.MethodRefTestCases/OtherStruct::InterfaceMethod(int32)
		IL_0011: ret
	} // end of method MethodRefTestCases::CallInterfaceMethodOfStructFromDelegate

	.method public hidebysig 
		instance class [netstandard]System.Type[] CallMethodDifferentWays () cil managed 
	{
		// Header size: 12
		// Code size: 111 (0x6f)
		.maxstack 3
		.locals init (
			[0] class [netstandard]System.Type[]
		)

		IL_0000: ldc.i4.6
		IL_0001: newarr [netstandard]System.Type
		IL_0006: stloc.0
		IL_0007: ldloc.0
		IL_0008: ldc.i4.0
		IL_0009: ldtoken [netstandard]System.Int32
		IL_000e: call class [netstandard]System.Type [netstandard]System.Type::GetTypeFromHandle(valuetype [netstandard]System.RuntimeTypeHandle)
		IL_0013: stelem [netstandard]System.Type
		IL_0018: ldloc.0
		IL_0019: ldc.i4.1
		IL_001a: ldtoken [netstandard]System.Int32
		IL_001f: call class [netstandard]System.Type [netstandard]System.Type::GetTypeFromHandle(valuetype [netstandard]System.RuntimeTypeHandle)
		IL_0024: stelem [netstandard]System.Type
		IL_0029: ldloc.0
		IL_002a: ldc.i4.2
		IL_002b: ldtoken [netstandard]System.Int32
		IL_0030: call class [netstandard]System.Type [netstandard]System.Type::GetTypeFromHandle(valuetype [netstandard]System.RuntimeTypeHandle)
		IL_0035: stelem [netstandard]System.Type
		IL_003a: ldloc.0
		IL_003b: ldc.i4.3
		IL_003c: ldtoken [netstandard]System.Int32
		IL_0041: call class [netstandard]System.Type [netstandard]System.Type::GetTypeFromHandle(valuetype [netstandard]System.RuntimeTypeHandle)
		IL_0046: stelem [netstandard]System.Type
		IL_004b: ldloc.0
		IL_004c: ldc.i4.4
		IL_004d: ldtoken [netstandard]System.Int32
		IL_0052: call class [netstandard]System.Type [netstandard]System.Type::GetTypeFromHandle(valuetype [netstandard]System.RuntimeTypeHandle)
		IL_0057: stelem [netstandard]System.Type
		IL_005c: ldloc.0
		IL_005d: ldc.i4.5
		IL_005e: ldtoken [netstandard]System.Int32
		IL_0063: call class [netstandard]System.Type [netstandard]System.Type::GetTypeFromHandle(valuetype [netstandard]System.RuntimeTypeHandle)
		IL_0068: stelem [netstandard]System.Type
		IL_006d: ldloc.0
		IL_006e: ret
	} // end of method MethodRefTestCases::CallMethodDifferentWays

	.method public hidebysig 
		instance string CallMethodInGenericType () cil managed 
	{
		// Header size: 1
		// Code size: 6 (0x6)
		.maxstack 8

		IL_0000: call string class InlineIL.Tests.AssemblyToProcess.MethodRefTestCases/GenericType`1<valuetype [netstandard]System.Guid>::NormalMethod()
		IL_0005: ret
	} // end of method MethodRefTestCases::CallMethodInGenericType

	.method public hidebysig 
		instance string CallMethodInGenericTypeArray () cil managed 
	{
		// Header size: 1
		// Code size: 6 (0x6)
		.maxstack 8

		IL_0000: call string class InlineIL.Tests.AssemblyToProcess.MethodRefTestCases/GenericType`1<valuetype [netstandard]System.Guid[]>::NormalMethod()
		IL_0005: ret
	} // end of method MethodRefTestCases::CallMethodInGenericTypeArray

	.method public hidebysig 
		instance string CallMethodInGenericTypeGeneric<TClass> () cil managed 
	{
		// Header size: 1
		// Code size: 6 (0x6)
		.maxstack 8

		IL_0000: call string class InlineIL.Tests.AssemblyToProcess.MethodRefTestCases/GenericType`1<!!TClass>::NormalMethod()
		IL_0005: ret
	} // end of method MethodRefTestCases::CallMethodInGenericTypeGeneric

	.method public hidebysig 
		instance class [netstandard]System.Text.StringBuilder CallNonDefaultConstructor () cil managed 
	{
		// Header size: 1
		// Code size: 8 (0x8)
		.maxstack 8

		IL_0000: ldc.i4.s 42
		IL_0002: newobj instance void [netstandard]System.Text.StringBuilder::.ctor(int32)
		IL_0007: ret
	} // end of method MethodRefTestCases::CallNonDefaultConstructor

	.method public hidebysig 
		instance int32 CallStaticMethodFromDelegate () cil managed 
	{
		// Header size: 1
		// Code size: 8 (0x8)
		.maxstack 8

		IL_0000: ldc.i4.s 42
		IL_0002: call int32 InlineIL.Tests.AssemblyToProcess.MethodRefTestCases::OverloadedMethod(int32)
		IL_0007: ret
	} // end of method MethodRefTestCases::CallStaticMethodFromDelegate

	.method public hidebysig 
		instance string CallStaticMethodOfGenericClassFromDelegate () cil managed 
	{
		// Header size: 1
		// Code size: 6 (0x6)
		.maxstack 8

		IL_0000: call string class InlineIL.Tests.AssemblyToProcess.MethodRefTestCases/GenericType`1<string>::NormalMethod()
		IL_0005: ret
	} // end of method MethodRefTestCases::CallStaticMethodOfGenericClassFromDelegate

	.method public hidebysig 
		instance int32 CallStaticMethodOfOtherClassFromDelegate () cil managed 
	{
		// Header size: 1
		// Code size: 8 (0x8)
		.maxstack 8

		IL_0000: ldc.i4.s 42
		IL_0002: call int32 InlineIL.Tests.AssemblyToProcess.MethodRefTestCases/OtherClass::StaticMethod(int32)
		IL_0007: ret
	} // end of method MethodRefTestCases::CallStaticMethodOfOtherClassFromDelegate

	.method public hidebysig 
		instance int32 CallStaticMethodOfStructFromDelegate () cil managed 
	{
		// Header size: 1
		// Code size: 8 (0x8)
		.maxstack 8

		IL_0000: ldc.i4.s 42
		IL_0002: call int32 InlineIL.Tests.AssemblyToProcess.MethodRefTestCases/OtherStruct::StaticMethod(int32)
		IL_0007: ret
	} // end of method MethodRefTestCases::CallStaticMethodOfStructFromDelegate

	.method public hidebysig 
		instance int32[] CallUnaryOperators () cil managed 
	{
		// Header size: 12
		// Code size: 188 (0xbc)
		.maxstack 3
		.locals init (
			[0] class InlineIL.Tests.AssemblyToProcess.MethodRefTestCases/UnaryOperatorsClass,
			[1] int32
		)

		IL_0000: newobj instance void class [netstandard]System.Collections.Generic.List`1<int32>::.ctor()
		IL_0005: newobj instance void InlineIL.Tests.AssemblyToProcess.MethodRefTestCases/UnaryOperatorsClass::.ctor()
		IL_000a: stloc.0
		IL_000b: ldloc.0
		IL_000c: call class InlineIL.Tests.AssemblyToProcess.MethodRefTestCases/UnaryOperatorsClass InlineIL.Tests.AssemblyToProcess.MethodRefTestCases/UnaryOperatorsClass::op_Decrement(class InlineIL.Tests.AssemblyToProcess.MethodRefTestCases/UnaryOperatorsClass)
		IL_0011: pop
		IL_0012: dup
		IL_0013: ldloc.0
		IL_0014: callvirt instance int32 InlineIL.Tests.AssemblyToProcess.MethodRefTestCases/UnaryOperatorsClass::get_Value()
		IL_0019: callvirt instance void class [netstandard]System.Collections.Generic.List`1<int32>::Add(!0)
		IL_001e: ldloc.0
		IL_001f: call class InlineIL.Tests.AssemblyToProcess.MethodRefTestCases/UnaryOperatorsClass InlineIL.Tests.AssemblyToProcess.MethodRefTestCases/UnaryOperatorsClass::op_Increment(class InlineIL.Tests.AssemblyToProcess.MethodRefTestCases/UnaryOperatorsClass)
		IL_0024: pop
		IL_0025: dup
		IL_0026: ldloc.0
		IL_0027: callvirt instance int32 InlineIL.Tests.AssemblyToProcess.MethodRefTestCases/UnaryOperatorsClass::get_Value()
		IL_002c: callvirt instance void class [netstandard]System.Collections.Generic.List`1<int32>::Add(!0)
		IL_0031: ldloc.0
		IL_0032: call class InlineIL.Tests.AssemblyToProcess.MethodRefTestCases/UnaryOperatorsClass InlineIL.Tests.AssemblyToProcess.MethodRefTestCases/UnaryOperatorsClass::op_UnaryNegation(class InlineIL.Tests.AssemblyToProcess.MethodRefTestCases/UnaryOperatorsClass)
		IL_0037: pop
		IL_0038: dup
		IL_0039: ldloc.0
		IL_003a: callvirt instance int32 InlineIL.Tests.AssemblyToProcess.MethodRefTestCases/UnaryOperatorsClass::get_Value()
		IL_003f: callvirt instance void class [netstandard]System.Collections.Generic.List`1<int32>::Add(!0)
		IL_0044: ldloc.0
		IL_0045: call class InlineIL.Tests.AssemblyToProcess.MethodRefTestCases/UnaryOperatorsClass InlineIL.Tests.AssemblyToProcess.MethodRefTestCases/UnaryOperatorsClass::op_UnaryPlus(class InlineIL.Tests.AssemblyToProcess.MethodRefTestCases/UnaryOperatorsClass)
		IL_004a: pop
		IL_004b: dup
		IL_004c: ldloc.0
		IL_004d: callvirt instance int32 InlineIL.Tests.AssemblyToProcess.MethodRefTestCases/UnaryOperatorsClass::get_Value()
		IL_0052: callvirt instance void class [netstandard]System.Collections.Generic.List`1<int32>::Add(!0)
		IL_0057: ldloc.0
		IL_0058: call class InlineIL.Tests.AssemblyToProcess.MethodRefTestCases/UnaryOperatorsClass InlineIL.Tests.AssemblyToProcess.MethodRefTestCases/UnaryOperatorsClass::op_LogicalNot(class InlineIL.Tests.AssemblyToProcess.MethodRefTestCases/UnaryOperatorsClass)
		IL_005d: pop
		IL_005e: dup
		IL_005f: ldloc.0
		IL_0060: callvirt instance int32 InlineIL.Tests.AssemblyToProcess.MethodRefTestCases/UnaryOperatorsClass::get_Value()
		IL_0065: callvirt instance void class [netstandard]System.Collections.Generic.List`1<int32>::Add(!0)
		IL_006a: ldloc.0
		IL_006b: call bool InlineIL.Tests.AssemblyToProcess.MethodRefTestCases/UnaryOperatorsClass::op_True(class InlineIL.Tests.AssemblyToProcess.MethodRefTestCases/UnaryOperatorsClass)
		IL_0070: pop
		IL_0071: dup
		IL_0072: ldloc.0
		IL_0073: callvirt instance int32 InlineIL.Tests.AssemblyToProcess.MethodRefTestCases/UnaryOperatorsClass::get_Value()
		IL_0078: callvirt instance void class [netstandard]System.Collections.Generic.List`1<int32>::Add(!0)
		IL_007d: ldloc.0
		IL_007e: call bool InlineIL.Tests.AssemblyToProcess.MethodRefTestCases/UnaryOperatorsClass::op_False(class InlineIL.Tests.AssemblyToProcess.MethodRefTestCases/UnaryOperatorsClass)
		IL_0083: pop
		IL_0084: dup
		IL_0085: ldloc.0
		IL_0086: callvirt instance int32 InlineIL.Tests.AssemblyToProcess.MethodRefTestCases/UnaryOperatorsClass::get_Value()
		IL_008b: callvirt instance void class [netstandard]System.Collections.Generic.List`1<int32>::Add(!0)
		IL_0090: ldloc.0
		IL_0091: call class InlineIL.Tests.AssemblyToProcess.MethodRefTestCases/UnaryOperatorsClass InlineIL.Tests.AssemblyToProcess.MethodRefTestCases/UnaryOperatorsClass::op_OnesComplement(class InlineIL.Tests.AssemblyToProcess.MethodRefTestCases/UnaryOperatorsClass)
		IL_0096: pop
		IL_0097: dup
		IL_0098: ldloc.0
		IL_0099: callvirt instance int32 InlineIL.Tests.AssemblyToProcess.MethodRefTestCases/UnaryOperatorsClass::get_Value()
		IL_009e: callvirt instance void class [netstandard]System.Collections.Generic.List`1<int32>::Add(!0)
		IL_00a3: ldc.i4.0
		IL_00a4: newobj instance void class InlineIL.Tests.AssemblyToProcess.MethodRefTestCases/GenericOperatorsClass`1<string>::.ctor(int32)
		IL_00a9: call int32 class InlineIL.Tests.AssemblyToProcess.MethodRefTestCases/GenericOperatorsClass`1<string>::op_UnaryPlus(class InlineIL.Tests.AssemblyToProcess.MethodRefTestCases/GenericOperatorsClass`1<!0>)
		IL_00ae: stloc.1
		IL_00af: dup
		IL_00b0: ldloc.1
		IL_00b1: callvirt instance void class [netstandard]System.Collections.Generic.List`1<int32>::Add(!0)
		IL_00b6: callvirt instance !0[] class [netstandard]System.Collections.Generic.List`1<int32>::ToArray()
		IL_00bb: ret
	} // end of method MethodRefTestCases::CallUnaryOperators

	.method public hidebysig 
		instance int32 CallVirtualMethodOfOtherClassFromDelegate () cil managed 
	{
		// Header size: 1
		// Code size: 11 (0xb)
		.maxstack 8

		IL_0000: newobj instance void InlineIL.Tests.AssemblyToProcess.MethodRefTestCases/OtherClass::.ctor()
		IL_0005: call instance int32 InlineIL.Tests.AssemblyToProcess.MethodRefTestCases/OtherClass::VirtualMethod()
		IL_000a: ret
	} // end of method MethodRefTestCases::CallVirtualMethodOfOtherClassFromDelegate

	.method public hidebysig 
		instance int32 CallVirtualMethodOfOtherClassFromDelegate2 () cil managed 
	{
		// Header size: 12
		// Code size: 14 (0xe)
		.maxstack 1
		.locals init (
			[0] class InlineIL.Tests.AssemblyToProcess.MethodRefTestCases/OtherClass
		)

		IL_0000: ldloca.s 0
		IL_0002: call void InlineIL.Tests.AssemblyToProcess.MethodRefTestCases/OtherClass::Create(class InlineIL.Tests.AssemblyToProcess.MethodRefTestCases/OtherClass&)
		IL_0007: ldloc.0
		IL_0008: call instance int32 InlineIL.Tests.AssemblyToProcess.MethodRefTestCases/OtherClass::VirtualMethod()
		IL_000d: ret
	} // end of method MethodRefTestCases::CallVirtualMethodOfOtherClassFromDelegate2

	.method public hidebysig 
		instance int32 CallVirtualMethodOverrideOfOtherClassFromDelegate () cil managed 
	{
		// Header size: 1
		// Code size: 11 (0xb)
		.maxstack 8

		IL_0000: newobj instance void InlineIL.Tests.AssemblyToProcess.MethodRefTestCases/OtherClassDerived::.ctor()
		IL_0005: call instance int32 InlineIL.Tests.AssemblyToProcess.MethodRefTestCases/OtherClass::VirtualMethod()
		IL_000a: ret
	} // end of method MethodRefTestCases::CallVirtualMethodOverrideOfOtherClassFromDelegate

	.method public hidebysig 
		instance int32 CallVirtualMethodOverrideOfOtherClassFromDelegate2 () cil managed 
	{
		// Header size: 1
		// Code size: 11 (0xb)
		.maxstack 8

		IL_0000: newobj instance void InlineIL.Tests.AssemblyToProcess.MethodRefTestCases/OtherClassDerived::.ctor()
		IL_0005: callvirt instance int32 InlineIL.Tests.AssemblyToProcess.MethodRefTestCases/OtherClass::VirtualMethod()
		IL_000a: ret
	} // end of method MethodRefTestCases::CallVirtualMethodOverrideOfOtherClassFromDelegate2

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

	.method private hidebysig static 
		int32 GenericOverload (
			int32 a,
			int32 b
		) cil managed 
	{
		// Header size: 1
		// Code size: 2 (0x2)
		.maxstack 8

		IL_0000: ldc.i4.6
		IL_0001: ret
	} // end of method MethodRefTestCases::GenericOverload

	.method private hidebysig static 
		int32 GenericOverload<T> (
			!!T a,
			int32 b
		) cil managed 
	{
		// Header size: 1
		// Code size: 2 (0x2)
		.maxstack 8

		IL_0000: ldc.i4.1
		IL_0001: ret
	} // end of method MethodRefTestCases::GenericOverload

	.method private hidebysig static 
		int32 GenericOverload<T> (
			!!T a,
			uint32 b
		) cil managed 
	{
		// Header size: 1
		// Code size: 2 (0x2)
		.maxstack 8

		IL_0000: ldc.i4.2
		IL_0001: ret
	} // end of method MethodRefTestCases::GenericOverload

	.method private hidebysig static 
		int32 GenericOverload<T1, T2> (
			!!T1 a,
			!!T2 b
		) cil managed 
	{
		// Header size: 1
		// Code size: 2 (0x2)
		.maxstack 8

		IL_0000: ldc.i4.4
		IL_0001: ret
	} // end of method MethodRefTestCases::GenericOverload

	.method private hidebysig static 
		int32 GenericOverload<T1, T2> (
			!!T1 a,
			uint32 b
		) cil managed 
	{
		// Header size: 1
		// Code size: 2 (0x2)
		.maxstack 8

		IL_0000: ldc.i4.3
		IL_0001: ret
	} // end of method MethodRefTestCases::GenericOverload

	.method private hidebysig static 
		int32 GenericOverload<T1, T2> (
			!!T1[] a,
			class [netstandard]System.Collections.Generic.List`1<!!T2> b
		) cil managed 
	{
		// Header size: 1
		// Code size: 2 (0x2)
		.maxstack 8

		IL_0000: ldc.i4.7
		IL_0001: ret
	} // end of method MethodRefTestCases::GenericOverload

	.method private hidebysig static 
		int32 GenericOverload<T1, T2> (
			!!T2 a,
			!!T1 b
		) cil managed 
	{
		// Header size: 1
		// Code size: 2 (0x2)
		.maxstack 8

		IL_0000: ldc.i4.5
		IL_0001: ret
	} // end of method MethodRefTestCases::GenericOverload

	.method public hidebysig specialname 
		instance int32 get_Value () cil managed 
	{
		.custom instance void [netstandard]System.Runtime.CompilerServices.CompilerGeneratedAttribute::.ctor() = (
			01 00 00 00
		)
		// Header size: 1
		// Code size: 7 (0x7)
		.maxstack 8

		IL_0000: ldarg.0
		IL_0001: ldfld int32 InlineIL.Tests.AssemblyToProcess.MethodRefTestCases::'<Value>k__BackingField'
		IL_0006: ret
	} // end of method MethodRefTestCases::get_Value

	.method public hidebysig 
		instance valuetype [netstandard]System.RuntimeMethodHandle GetTypeInitializer () cil managed 
	{
		// Header size: 1
		// Code size: 6 (0x6)
		.maxstack 8

		IL_0000: ldtoken method void InlineIL.Tests.AssemblyToProcess.MethodRefTestCases/TypeWithInitializer::.cctor()
		IL_0005: ret
	} // end of method MethodRefTestCases::GetTypeInitializer

	.method public hidebysig 
		instance int32 GetValue () cil managed 
	{
		// Header size: 1
		// Code size: 7 (0x7)
		.maxstack 8

		IL_0000: ldarg.0
		IL_0001: call instance int32 InlineIL.Tests.AssemblyToProcess.MethodRefTestCases::get_Value()
		IL_0006: ret
	} // end of method MethodRefTestCases::GetValue

	.method private hidebysig 
		instance int32 InstanceMethod () cil managed 
	{
		// Header size: 1
		// Code size: 3 (0x3)
		.maxstack 8

		IL_0000: ldc.i4.s 10
		IL_0002: ret
	} // end of method MethodRefTestCases::InstanceMethod

	.method private hidebysig 
		instance int32 InstanceMethod (
			int32 a
		) cil managed 
	{
		// Header size: 1
		// Code size: 3 (0x3)
		.maxstack 8

		IL_0000: ldc.i4.s 20
		IL_0002: ret
	} // end of method MethodRefTestCases::InstanceMethod

	.method private hidebysig static 
		int32 OverloadedMethod () cil managed 
	{
		// Header size: 1
		// Code size: 3 (0x3)
		.maxstack 8

		IL_0000: ldc.i4.s 10
		IL_0002: ret
	} // end of method MethodRefTestCases::OverloadedMethod

	.method private hidebysig static 
		int32 OverloadedMethod (
			float64 a,
			int32& b
		) cil managed 
	{
		// Header size: 1
		// Code size: 3 (0x3)
		.maxstack 8

		IL_0000: ldc.i4.s 50
		IL_0002: ret
	} // end of method MethodRefTestCases::OverloadedMethod

	.method private hidebysig static 
		int32 OverloadedMethod (
			float64 a,
			int32 b
		) cil managed 
	{
		// Header size: 1
		// Code size: 3 (0x3)
		.maxstack 8

		IL_0000: ldc.i4.s 60
		IL_0002: ret
	} // end of method MethodRefTestCases::OverloadedMethod

	.method private hidebysig static 
		int32 OverloadedMethod (
			int32& a
		) cil managed 
	{
		// Header size: 1
		// Code size: 3 (0x3)
		.maxstack 8

		IL_0000: ldc.i4.s 30
		IL_0002: ret
	} // end of method MethodRefTestCases::OverloadedMethod

	.method private hidebysig static 
		int32 OverloadedMethod (
			int32 a
		) cil managed 
	{
		// Header size: 1
		// Code size: 3 (0x3)
		.maxstack 8

		IL_0000: ldc.i4.s 20
		IL_0002: ret
	} // end of method MethodRefTestCases::OverloadedMethod

	.method private hidebysig static 
		int32 OverloadedMethod (
			int32[] a
		) cil managed 
	{
		// Header size: 1
		// Code size: 3 (0x3)
		.maxstack 8

		IL_0000: ldc.i4.s 40
		IL_0002: ret
	} // end of method MethodRefTestCases::OverloadedMethod

	.method public final hidebysig newslot virtual 
		instance void RaiseEvent () cil managed 
	{
		// Header size: 1
		// Code size: 17 (0x11)
		.maxstack 8

		IL_0000: ldarg.0
		IL_0001: ldfld class [netstandard]System.Action InlineIL.Tests.AssemblyToProcess.MethodRefTestCases::Event
		IL_0006: dup
		IL_0007: brtrue.s IL_000b

		IL_0009: pop
		IL_000a: ret

		IL_000b: callvirt instance void [netstandard]System.Action::Invoke()
		IL_0010: ret
	} // end of method MethodRefTestCases::RaiseEvent

	.method public final hidebysig specialname newslot virtual 
		instance void remove_Event (
			class [netstandard]System.Action 'value'
		) cil managed 
	{
		.custom instance void [netstandard]System.Runtime.CompilerServices.CompilerGeneratedAttribute::.ctor() = (
			01 00 00 00
		)
		// Header size: 12
		// Code size: 41 (0x29)
		.maxstack 3
		.locals init (
			[0] class [netstandard]System.Action,
			[1] class [netstandard]System.Action,
			[2] class [netstandard]System.Action
		)

		IL_0000: ldarg.0
		IL_0001: ldfld class [netstandard]System.Action InlineIL.Tests.AssemblyToProcess.MethodRefTestCases::Event
		IL_0006: stloc.0
		// loop start (head: IL_0007)
			IL_0007: ldloc.0
			IL_0008: stloc.1
			IL_0009: ldloc.1
			IL_000a: ldarg.1
			IL_000b: call class [netstandard]System.Delegate [netstandard]System.Delegate::Remove(class [netstandard]System.Delegate, class [netstandard]System.Delegate)
			IL_0010: castclass [netstandard]System.Action
			IL_0015: stloc.2
			IL_0016: ldarg.0
			IL_0017: ldflda class [netstandard]System.Action InlineIL.Tests.AssemblyToProcess.MethodRefTestCases::Event
			IL_001c: ldloc.2
			IL_001d: ldloc.1
			IL_001e: call !!0 [netstandard]System.Threading.Interlocked::CompareExchange<class [netstandard]System.Action>(!!0&, !!0, !!0)
			IL_0023: stloc.0
			IL_0024: ldloc.0
			IL_0025: ldloc.1
			IL_0026: bne.un.s IL_0007
		// end loop
		IL_0028: ret
	} // end of method MethodRefTestCases::remove_Event

	.method public final hidebysig newslot virtual 
		instance void RemoveEvent (
			class [netstandard]System.Action callback
		) cil managed 
	{
		// Header size: 1
		// Code size: 8 (0x8)
		.maxstack 8

		IL_0000: ldarg.0
		IL_0001: ldarg.1
		IL_0002: call instance void InlineIL.Tests.AssemblyToProcess.MethodRefTestCases::remove_Event(class [netstandard]System.Action)
		IL_0007: ret
	} // end of method MethodRefTestCases::RemoveEvent

	.method public hidebysig 
		instance int32[] ResolveGenericOverloads () cil managed 
	{
		// Header size: 12
		// Code size: 133 (0x85)
		.maxstack 2
		.locals init (
			[0] class [netstandard]System.Collections.Generic.List`1<int32>,
			[1] int32
		)

		IL_0000: newobj instance void class [netstandard]System.Collections.Generic.List`1<int32>::.ctor()
		IL_0005: stloc.0
		IL_0006: ldc.i4.0
		IL_0007: ldc.i4.0
		IL_0008: call int32 InlineIL.Tests.AssemblyToProcess.MethodRefTestCases::GenericOverload<int32>(!!0, int32)
		IL_000d: stloc.1
		IL_000e: ldloc.0
		IL_000f: ldloc.1
		IL_0010: callvirt instance void class [netstandard]System.Collections.Generic.List`1<int32>::Add(!0)
		IL_0015: ldc.i4.0
		IL_0016: ldc.i4.0
		IL_0017: call int32 InlineIL.Tests.AssemblyToProcess.MethodRefTestCases::GenericOverload<int32>(!!0, uint32)
		IL_001c: stloc.1
		IL_001d: ldloc.0
		IL_001e: ldloc.1
		IL_001f: callvirt instance void class [netstandard]System.Collections.Generic.List`1<int32>::Add(!0)
		IL_0024: ldc.i4.0
		IL_0025: ldc.i4.0
		IL_0026: call int32 InlineIL.Tests.AssemblyToProcess.MethodRefTestCases::GenericOverload<int32, int32>(!!0, uint32)
		IL_002b: stloc.1
		IL_002c: ldloc.0
		IL_002d: ldloc.1
		IL_002e: callvirt instance void class [netstandard]System.Collections.Generic.List`1<int32>::Add(!0)
		IL_0033: ldc.i4.0
		IL_0034: ldc.i4.0
		IL_0035: call int32 InlineIL.Tests.AssemblyToProcess.MethodRefTestCases::GenericOverload<int32, int32>(!!0, !!1)
		IL_003a: stloc.1
		IL_003b: ldloc.0
		IL_003c: ldloc.1
		IL_003d: callvirt instance void class [netstandard]System.Collections.Generic.List`1<int32>::Add(!0)
		IL_0042: ldc.i4.0
		IL_0043: ldc.i4.0
		IL_0044: call int32 InlineIL.Tests.AssemblyToProcess.MethodRefTestCases::GenericOverload<int32, int32>(!!1, !!0)
		IL_0049: stloc.1
		IL_004a: ldloc.0
		IL_004b: ldloc.1
		IL_004c: callvirt instance void class [netstandard]System.Collections.Generic.List`1<int32>::Add(!0)
		IL_0051: ldc.i4.0
		IL_0052: ldc.i4.0
		IL_0053: call int32 InlineIL.Tests.AssemblyToProcess.MethodRefTestCases::GenericOverload(int32, int32)
		IL_0058: stloc.1
		IL_0059: ldloc.0
		IL_005a: ldloc.1
		IL_005b: callvirt instance void class [netstandard]System.Collections.Generic.List`1<int32>::Add(!0)
		IL_0060: ldc.i4.0
		IL_0061: ldc.i4.0
		IL_0062: call int32 InlineIL.Tests.AssemblyToProcess.MethodRefTestCases::GenericOverload(int32, int32)
		IL_0067: stloc.1
		IL_0068: ldloc.0
		IL_0069: ldloc.1
		IL_006a: callvirt instance void class [netstandard]System.Collections.Generic.List`1<int32>::Add(!0)
		IL_006f: ldnull
		IL_0070: ldnull
		IL_0071: call int32 InlineIL.Tests.AssemblyToProcess.MethodRefTestCases::GenericOverload<int32, int32>(!!0[], class [netstandard]System.Collections.Generic.List`1<!!1>)
		IL_0076: stloc.1
		IL_0077: ldloc.0
		IL_0078: ldloc.1
		IL_0079: callvirt instance void class [netstandard]System.Collections.Generic.List`1<int32>::Add(!0)
		IL_007e: ldloc.0
		IL_007f: callvirt instance !0[] class [netstandard]System.Collections.Generic.List`1<int32>::ToArray()
		IL_0084: ret
	} // end of method MethodRefTestCases::ResolveGenericOverloads

	.method public hidebysig 
		instance int32[] ResolveGenericOverloadsInGenericType () cil managed 
	{
		// Header size: 12
		// Code size: 88 (0x58)
		.maxstack 2
		.locals init (
			[0] class [netstandard]System.Collections.Generic.List`1<int32>,
			[1] int32
		)

		IL_0000: newobj instance void class [netstandard]System.Collections.Generic.List`1<int32>::.ctor()
		IL_0005: stloc.0
		IL_0006: ldc.i4.0
		IL_0007: ldc.i4.0
		IL_0008: call int32 class InlineIL.Tests.AssemblyToProcess.MethodRefTestCases/GenericType`1/NestedGenericType`1<int32, int32>::GenericOverload<int32>(!0, !1)
		IL_000d: stloc.1
		IL_000e: ldloc.0
		IL_000f: ldloc.1
		IL_0010: callvirt instance void class [netstandard]System.Collections.Generic.List`1<int32>::Add(!0)
		IL_0015: ldc.i4.0
		IL_0016: ldc.i4.0
		IL_0017: call int32 class InlineIL.Tests.AssemblyToProcess.MethodRefTestCases/GenericType`1/NestedGenericType`1<int32, int32>::GenericOverload<int32>(!1, !0)
		IL_001c: stloc.1
		IL_001d: ldloc.0
		IL_001e: ldloc.1
		IL_001f: callvirt instance void class [netstandard]System.Collections.Generic.List`1<int32>::Add(!0)
		IL_0024: ldc.i4.0
		IL_0025: ldc.i4.0
		IL_0026: call int32 class InlineIL.Tests.AssemblyToProcess.MethodRefTestCases/GenericType`1/NestedGenericType`1<int32, int32>::GenericOverload<int32>(!0, !!0)
		IL_002b: stloc.1
		IL_002c: ldloc.0
		IL_002d: ldloc.1
		IL_002e: callvirt instance void class [netstandard]System.Collections.Generic.List`1<int32>::Add(!0)
		IL_0033: ldnull
		IL_0034: ldnull
		IL_0035: call int32 class InlineIL.Tests.AssemblyToProcess.MethodRefTestCases/GenericType`1/NestedGenericType`1<int32, int32>::GenericOverload<int32>(!0[], class [netstandard]System.Collections.Generic.List`1<!1>)
		IL_003a: stloc.1
		IL_003b: ldloc.0
		IL_003c: ldloc.1
		IL_003d: callvirt instance void class [netstandard]System.Collections.Generic.List`1<int32>::Add(!0)
		IL_0042: ldnull
		IL_0043: ldnull
		IL_0044: call int32 class InlineIL.Tests.AssemblyToProcess.MethodRefTestCases/GenericType`1/NestedGenericType`1<int32, int32>::GenericOverload<int32>(!!0[], class [netstandard]System.Collections.Generic.List`1<!!0>)
		IL_0049: stloc.1
		IL_004a: ldloc.0
		IL_004b: ldloc.1
		IL_004c: callvirt instance void class [netstandard]System.Collections.Generic.List`1<int32>::Add(!0)
		IL_0051: ldloc.0
		IL_0052: callvirt instance !0[] class [netstandard]System.Collections.Generic.List`1<int32>::ToArray()
		IL_0057: ret
	} // end of method MethodRefTestCases::ResolveGenericOverloadsInGenericType

	.method public hidebysig 
		instance int32[] ResolveOverloads () cil managed 
	{
		// Header size: 12
		// Code size: 102 (0x66)
		.maxstack 5
		.locals init (
			[0] int32[]
		)

		IL_0000: ldc.i4.7
		IL_0001: newarr [netstandard]System.Int32
		IL_0006: stloc.0
		IL_0007: ldloc.0
		IL_0008: ldc.i4.0
		IL_0009: call int32 InlineIL.Tests.AssemblyToProcess.MethodRefTestCases::OverloadedMethod()
		IL_000e: stelem.i4
		IL_000f: ldloc.0
		IL_0010: ldc.i4.1
		IL_0011: call int32 InlineIL.Tests.AssemblyToProcess.MethodRefTestCases::OverloadedMethod()
		IL_0016: stelem.i4
		IL_0017: ldloc.0
		IL_0018: ldc.i4.2
		IL_0019: ldc.i4.s 42
		IL_001b: call int32 InlineIL.Tests.AssemblyToProcess.MethodRefTestCases::OverloadedMethod(int32)
		IL_0020: stelem.i4
		IL_0021: ldloc.0
		IL_0022: ldc.i4.3
		IL_0023: ldloc.0
		IL_0024: ldc.i4.0
		IL_0025: ldelema [netstandard]System.Int32
		IL_002a: call int32 InlineIL.Tests.AssemblyToProcess.MethodRefTestCases::OverloadedMethod(int32&)
		IL_002f: stelem.i4
		IL_0030: ldloc.0
		IL_0031: ldc.i4.4
		IL_0032: ldnull
		IL_0033: call int32 InlineIL.Tests.AssemblyToProcess.MethodRefTestCases::OverloadedMethod(int32[])
		IL_0038: stelem.i4
		IL_0039: ldloc.0
		IL_003a: ldc.i4.5
		IL_003b: ldc.r8 42
		IL_0044: ldloc.0
		IL_0045: ldc.i4.0
		IL_0046: ldelema [netstandard]System.Int32
		IL_004b: call int32 InlineIL.Tests.AssemblyToProcess.MethodRefTestCases::OverloadedMethod(float64, int32&)
		IL_0050: stelem.i4
		IL_0051: ldloc.0
		IL_0052: ldc.i4.6
		IL_0053: ldc.r8 42
		IL_005c: ldc.i4.s 42
		IL_005e: call int32 InlineIL.Tests.AssemblyToProcess.MethodRefTestCases::OverloadedMethod(float64, int32)
		IL_0063: stelem.i4
		IL_0064: ldloc.0
		IL_0065: ret
	} // end of method MethodRefTestCases::ResolveOverloads

	.method public hidebysig 
		instance valuetype [netstandard]System.RuntimeMethodHandle ReturnMethodHandle () cil managed 
	{
		// Header size: 1
		// Code size: 6 (0x6)
		.maxstack 8

		IL_0000: ldtoken method class [netstandard]System.Type [netstandard]System.Type::GetTypeFromHandle(valuetype [netstandard]System.RuntimeTypeHandle)
		IL_0005: ret
	} // end of method MethodRefTestCases::ReturnMethodHandle

	.method public hidebysig 
		instance class [netstandard]System.Type ReturnType<T> () cil managed 
	{
		// Header size: 1
		// Code size: 11 (0xb)
		.maxstack 8

		IL_0000: ldtoken !!T
		IL_0005: call class [netstandard]System.Type [netstandard]System.Type::GetTypeFromHandle(valuetype [netstandard]System.RuntimeTypeHandle)
		IL_000a: ret
	} // end of method MethodRefTestCases::ReturnType

	.method public hidebysig specialname 
		instance void set_Value (
			int32 'value'
		) cil managed 
	{
		.custom instance void [netstandard]System.Runtime.CompilerServices.CompilerGeneratedAttribute::.ctor() = (
			01 00 00 00
		)
		// Header size: 1
		// Code size: 8 (0x8)
		.maxstack 8

		IL_0000: ldarg.0
		IL_0001: ldarg.1
		IL_0002: stfld int32 InlineIL.Tests.AssemblyToProcess.MethodRefTestCases::'<Value>k__BackingField'
		IL_0007: ret
	} // end of method MethodRefTestCases::set_Value

	.method public hidebysig 
		instance void SetValue (
			int32 'value'
		) cil managed 
	{
		// Header size: 1
		// Code size: 8 (0x8)
		.maxstack 8

		IL_0000: ldarg.0
		IL_0001: ldarg.1
		IL_0002: call instance void InlineIL.Tests.AssemblyToProcess.MethodRefTestCases::set_Value(int32)
		IL_0007: ret
	} // end of method MethodRefTestCases::SetValue

	// Events
	.event [netstandard]System.Action Event
	{
		.addon instance void InlineIL.Tests.AssemblyToProcess.MethodRefTestCases::add_Event(class [netstandard]System.Action)
		.removeon instance void InlineIL.Tests.AssemblyToProcess.MethodRefTestCases::remove_Event(class [netstandard]System.Action)
	}


	// Properties
	.property instance int32 Value()
	{
		.get instance int32 InlineIL.Tests.AssemblyToProcess.MethodRefTestCases::get_Value()
		.set instance void InlineIL.Tests.AssemblyToProcess.MethodRefTestCases::set_Value(int32)
	}

} // end of class InlineIL.Tests.AssemblyToProcess.MethodRefTestCases
