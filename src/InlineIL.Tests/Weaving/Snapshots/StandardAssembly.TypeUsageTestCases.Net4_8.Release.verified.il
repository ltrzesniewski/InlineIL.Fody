.class public auto ansi abstract sealed beforefieldinit InlineIL.Tests.AssemblyToProcess.TypeUsageTestCases
	extends [netstandard]System.Object
{
	.custom instance void [InlineIL.Tests.Common]InlineIL.Tests.Common.TestCasesAttribute::.ctor() = (
		01 00 00 00
	)
	// Nested Types
	.class nested public auto ansi beforefieldinit SelfReferencingAttribute
		extends [netstandard]System.Attribute
	{
		.custom instance void InlineIL.Tests.AssemblyToProcess.TypeUsageTestCases/SelfReferencingAttribute::.ctor() = (
			01 00 00 00
		)
		// Methods
		.method public hidebysig specialname rtspecialname 
			instance void .ctor () cil managed 
		{
			.custom instance void InlineIL.Tests.AssemblyToProcess.TypeUsageTestCases/SelfReferencingAttribute::.ctor() = (
				01 00 00 00
			)
			// Header size: 1
			// Code size: 8 (0x8)
			.maxstack 8

			IL_0000: ldarg.0
			IL_0001: call instance void [netstandard]System.Attribute::.ctor()
			IL_0006: nop
			IL_0007: ret
		} // end of method SelfReferencingAttribute::.ctor

	} // end of class SelfReferencingAttribute

	.class nested public auto ansi beforefieldinit SelfReferencingConstraint`1<(class InlineIL.Tests.AssemblyToProcess.TypeUsageTestCases/SelfReferencingConstraint`1<!T>) T>
		extends [netstandard]System.Object
	{
		// Methods
		.method public hidebysig specialname rtspecialname 
			instance void .ctor () cil managed 
		{
			// Header size: 1
			// Code size: 8 (0x8)
			.maxstack 8

			IL_0000: ldarg.0
			IL_0001: call instance void [netstandard]System.Object::.ctor()
			IL_0006: nop
			IL_0007: ret
		} // end of method SelfReferencingConstraint`1::.ctor

	} // end of class SelfReferencingConstraint`1

	.class nested public auto ansi beforefieldinit SelfReferencingConstraintIndirect`1<(class InlineIL.Tests.AssemblyToProcess.TypeUsageTestCases/SelfReferencingConstraintIndirectBase`1<!T>) T>
		extends class InlineIL.Tests.AssemblyToProcess.TypeUsageTestCases/SelfReferencingConstraintIndirectBase`1<!T>
	{
		// Methods
		.method public hidebysig specialname rtspecialname 
			instance void .ctor () cil managed 
		{
			// Header size: 1
			// Code size: 8 (0x8)
			.maxstack 8

			IL_0000: ldarg.0
			IL_0001: call instance void class InlineIL.Tests.AssemblyToProcess.TypeUsageTestCases/SelfReferencingConstraintIndirectBase`1<!T>::.ctor()
			IL_0006: nop
			IL_0007: ret
		} // end of method SelfReferencingConstraintIndirect`1::.ctor

	} // end of class SelfReferencingConstraintIndirect`1

	.class nested public auto ansi beforefieldinit SelfReferencingConstraintIndirectBase`1<T>
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
		} // end of method SelfReferencingConstraintIndirectBase`1::.ctor

	} // end of class SelfReferencingConstraintIndirectBase`1


} // end of class InlineIL.Tests.AssemblyToProcess.TypeUsageTestCases
