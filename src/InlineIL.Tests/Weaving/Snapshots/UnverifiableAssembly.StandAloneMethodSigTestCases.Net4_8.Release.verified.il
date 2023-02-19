.class public auto ansi beforefieldinit InlineIL.Tests.UnverifiableAssemblyToProcess.StandAloneMethodSigTestCases
	extends [mscorlib]System.Object
{
	.custom instance void [InlineIL.Tests.Common]InlineIL.Tests.Common.TestCasesAttribute::.ctor() = (
		01 00 00 00
	)
	// Nested Types
	.class nested private auto ansi sealed IntToIntCdecl
		extends [mscorlib]System.MulticastDelegate
	{
		.custom instance void [mscorlib]System.Runtime.InteropServices.UnmanagedFunctionPointerAttribute::.ctor(valuetype [mscorlib]System.Runtime.InteropServices.CallingConvention) = (
			01 00 02 00 00 00 00 00
		)
		// Methods
		.method public hidebysig specialname rtspecialname 
			instance void .ctor (
				object 'object',
				native int 'method'
			) runtime managed 
		{
		} // end of method IntToIntCdecl::.ctor

		.method public hidebysig newslot virtual 
			instance class [mscorlib]System.IAsyncResult BeginInvoke (
				int32 a,
				int32 b,
				int32 c,
				int32 d,
				int32 e,
				int32 f,
				int32 g,
				int32 h,
				class [mscorlib]System.AsyncCallback callback,
				object 'object'
			) runtime managed 
		{
		} // end of method IntToIntCdecl::BeginInvoke

		.method public hidebysig newslot virtual 
			instance int32 EndInvoke (
				class [mscorlib]System.IAsyncResult result
			) runtime managed 
		{
		} // end of method IntToIntCdecl::EndInvoke

		.method public hidebysig newslot virtual 
			instance int32 Invoke (
				int32 a,
				int32 b,
				int32 c,
				int32 d,
				int32 e,
				int32 f,
				int32 g,
				int32 h
			) runtime managed 
		{
		} // end of method IntToIntCdecl::Invoke

	} // end of class IntToIntCdecl

	.class nested private auto ansi sealed IntToIntStdcall
		extends [mscorlib]System.MulticastDelegate
	{
		.custom instance void [mscorlib]System.Runtime.InteropServices.UnmanagedFunctionPointerAttribute::.ctor(valuetype [mscorlib]System.Runtime.InteropServices.CallingConvention) = (
			01 00 03 00 00 00 00 00
		)
		// Methods
		.method public hidebysig specialname rtspecialname 
			instance void .ctor (
				object 'object',
				native int 'method'
			) runtime managed 
		{
		} // end of method IntToIntStdcall::.ctor

		.method public hidebysig newslot virtual 
			instance class [mscorlib]System.IAsyncResult BeginInvoke (
				int32 a,
				int32 b,
				int32 c,
				int32 d,
				int32 e,
				int32 f,
				int32 g,
				int32 h,
				class [mscorlib]System.AsyncCallback callback,
				object 'object'
			) runtime managed 
		{
		} // end of method IntToIntStdcall::BeginInvoke

		.method public hidebysig newslot virtual 
			instance int32 EndInvoke (
				class [mscorlib]System.IAsyncResult result
			) runtime managed 
		{
		} // end of method IntToIntStdcall::EndInvoke

		.method public hidebysig newslot virtual 
			instance int32 Invoke (
				int32 a,
				int32 b,
				int32 c,
				int32 d,
				int32 e,
				int32 f,
				int32 g,
				int32 h
			) runtime managed 
		{
		} // end of method IntToIntStdcall::Invoke

	} // end of class IntToIntStdcall


	// Methods
	.method public hidebysig specialname rtspecialname 
		instance void .ctor () cil managed 
	{
		// Header size: 1
		// Code size: 7 (0x7)
		.maxstack 8

		IL_0000: ldarg.0
		IL_0001: call instance void [mscorlib]System.Object::.ctor()
		IL_0006: ret
	} // end of method StandAloneMethodSigTestCases::.ctor

	.method public hidebysig 
		instance int32 BranchOverTailCall (
			bool branch
		) cil managed 
	{
		// Header size: 12
		// Code size: 21 (0x15)
		.maxstack 2

		IL_0000: ldc.i4.s 42
		IL_0002: ldarg.1
		IL_0003: brtrue.s IL_0014

		IL_0005: dup
		IL_0006: add
		IL_0007: ldftn int32 InlineIL.Tests.UnverifiableAssemblyToProcess.StandAloneMethodSigTestCases::IndirectCallStaticTargetMethod(int32)
		IL_000d: tail.
		IL_000f: calli int32(int32)

		IL_0014: ret
	} // end of method StandAloneMethodSigTestCases::BranchOverTailCall

	.method public hidebysig 
		instance int32 CallIndirectInstance () cil managed 
	{
		// Header size: 12
		// Code size: 15 (0xf)
		.maxstack 3

		IL_0000: ldarg.0
		IL_0001: ldc.i4.s 42
		IL_0003: ldftn instance int32 InlineIL.Tests.UnverifiableAssemblyToProcess.StandAloneMethodSigTestCases::IndirectCallInstanceTargetMethod(int32)
		IL_0009: calli instance int32(int32)
		IL_000e: ret
	} // end of method StandAloneMethodSigTestCases::CallIndirectInstance

	.method public hidebysig 
		instance int32 CallIndirectNativeCdecl () cil managed 
	{
		// Header size: 12
		// Code size: 53 (0x35)
		.maxstack 9
		.locals init (
			[0] class InlineIL.Tests.UnverifiableAssemblyToProcess.StandAloneMethodSigTestCases/IntToIntCdecl,
			[1] native int
		)

		IL_0000: ldnull
		IL_0001: ldftn int32 InlineIL.Tests.UnverifiableAssemblyToProcess.StandAloneMethodSigTestCases::IndirectCallNativeTargetMethod(int32, int32, int32, int32, int32, int32, int32, int32)
		IL_0007: newobj instance void InlineIL.Tests.UnverifiableAssemblyToProcess.StandAloneMethodSigTestCases/IntToIntCdecl::.ctor(object, native int)
		IL_000c: stloc.0
		IL_000d: ldc.i4.s 40
		IL_000f: ldc.i4.2
		IL_0010: ldc.i4.s 40
		IL_0012: ldc.i4.s -20
		IL_0014: ldc.i4.s -20
		IL_0016: ldc.i4.s -20
		IL_0018: ldc.i4.5
		IL_0019: ldc.i4.s 15
		IL_001b: ldloc.0
		IL_001c: call native int [mscorlib]System.Runtime.InteropServices.Marshal::GetFunctionPointerForDelegate<class InlineIL.Tests.UnverifiableAssemblyToProcess.StandAloneMethodSigTestCases/IntToIntCdecl>(!!0)
		IL_0021: stloc.1
		IL_0022: ldloca.s 1
		IL_0024: call instance void* [mscorlib]System.IntPtr::ToPointer()
		IL_0029: calli unmanaged cdecl int32(int32, int32, int32, int32, int32, int32, int32, int32)
		IL_002e: ldloc.0
		IL_002f: call void [mscorlib]System.GC::KeepAlive(object)
		IL_0034: ret
	} // end of method StandAloneMethodSigTestCases::CallIndirectNativeCdecl

	.method public hidebysig 
		instance int32 CallIndirectNativeStdcall () cil managed 
	{
		// Header size: 12
		// Code size: 53 (0x35)
		.maxstack 9
		.locals init (
			[0] class InlineIL.Tests.UnverifiableAssemblyToProcess.StandAloneMethodSigTestCases/IntToIntStdcall,
			[1] native int
		)

		IL_0000: ldnull
		IL_0001: ldftn int32 InlineIL.Tests.UnverifiableAssemblyToProcess.StandAloneMethodSigTestCases::IndirectCallNativeTargetMethod(int32, int32, int32, int32, int32, int32, int32, int32)
		IL_0007: newobj instance void InlineIL.Tests.UnverifiableAssemblyToProcess.StandAloneMethodSigTestCases/IntToIntStdcall::.ctor(object, native int)
		IL_000c: stloc.0
		IL_000d: ldc.i4.s 40
		IL_000f: ldc.i4.2
		IL_0010: ldc.i4.s 40
		IL_0012: ldc.i4.s -20
		IL_0014: ldc.i4.s -20
		IL_0016: ldc.i4.s -20
		IL_0018: ldc.i4.5
		IL_0019: ldc.i4.s 15
		IL_001b: ldloc.0
		IL_001c: call native int [mscorlib]System.Runtime.InteropServices.Marshal::GetFunctionPointerForDelegate<class InlineIL.Tests.UnverifiableAssemblyToProcess.StandAloneMethodSigTestCases/IntToIntStdcall>(!!0)
		IL_0021: stloc.1
		IL_0022: ldloca.s 1
		IL_0024: call instance void* [mscorlib]System.IntPtr::ToPointer()
		IL_0029: calli unmanaged stdcall int32(int32, int32, int32, int32, int32, int32, int32, int32)
		IL_002e: ldloc.0
		IL_002f: call void [mscorlib]System.GC::KeepAlive(object)
		IL_0034: ret
	} // end of method StandAloneMethodSigTestCases::CallIndirectNativeStdcall

	.method public hidebysig 
		instance int32 CallIndirectNativeStdcallAlt () cil managed 
	{
		// Header size: 12
		// Code size: 53 (0x35)
		.maxstack 9
		.locals init (
			[0] class InlineIL.Tests.UnverifiableAssemblyToProcess.StandAloneMethodSigTestCases/IntToIntStdcall,
			[1] native int
		)

		IL_0000: ldnull
		IL_0001: ldftn int32 InlineIL.Tests.UnverifiableAssemblyToProcess.StandAloneMethodSigTestCases::IndirectCallNativeTargetMethod(int32, int32, int32, int32, int32, int32, int32, int32)
		IL_0007: newobj instance void InlineIL.Tests.UnverifiableAssemblyToProcess.StandAloneMethodSigTestCases/IntToIntStdcall::.ctor(object, native int)
		IL_000c: stloc.0
		IL_000d: ldc.i4.s 40
		IL_000f: ldc.i4.2
		IL_0010: ldc.i4.s 40
		IL_0012: ldc.i4.s -20
		IL_0014: ldc.i4.s -20
		IL_0016: ldc.i4.s -20
		IL_0018: ldc.i4.5
		IL_0019: ldc.i4.s 15
		IL_001b: ldloc.0
		IL_001c: call native int [mscorlib]System.Runtime.InteropServices.Marshal::GetFunctionPointerForDelegate<class InlineIL.Tests.UnverifiableAssemblyToProcess.StandAloneMethodSigTestCases/IntToIntStdcall>(!!0)
		IL_0021: stloc.1
		IL_0022: ldloca.s 1
		IL_0024: call instance void* [mscorlib]System.IntPtr::ToPointer()
		IL_0029: calli unmanaged stdcall int32(int32, int32, int32, int32, int32, int32, int32, int32)
		IL_002e: ldloc.0
		IL_002f: call void [mscorlib]System.GC::KeepAlive(object)
		IL_0034: ret
	} // end of method StandAloneMethodSigTestCases::CallIndirectNativeStdcallAlt

	.method public hidebysig 
		instance int32 CallIndirectStatic () cil managed 
	{
		// Header size: 12
		// Code size: 14 (0xe)
		.maxstack 2

		IL_0000: ldc.i4.s 42
		IL_0002: ldftn int32 InlineIL.Tests.UnverifiableAssemblyToProcess.StandAloneMethodSigTestCases::IndirectCallStaticTargetMethod(int32)
		IL_0008: calli int32(int32)
		IL_000d: ret
	} // end of method StandAloneMethodSigTestCases::CallIndirectStatic

	.method public hidebysig 
		instance int32 CallIndirectStaticAlt () cil managed 
	{
		// Header size: 12
		// Code size: 14 (0xe)
		.maxstack 2

		IL_0000: ldc.i4.s 42
		IL_0002: ldftn int32 InlineIL.Tests.UnverifiableAssemblyToProcess.StandAloneMethodSigTestCases::IndirectCallStaticTargetMethod(int32)
		IL_0008: calli int32(int32)
		IL_000d: ret
	} // end of method StandAloneMethodSigTestCases::CallIndirectStaticAlt

	.method public hidebysig 
		instance int32 CallIndirectVarArg () cil managed 
	{
		// Header size: 12
		// Code size: 18 (0x12)
		.maxstack 4

		IL_0000: ldc.i4.s 40
		IL_0002: ldc.i4.s 10
		IL_0004: ldc.i4.s 20
		IL_0006: ldftn vararg int32 InlineIL.Tests.UnverifiableAssemblyToProcess.StandAloneMethodSigTestCases::IndirectCallVarArgTargetMethod(int32)
		IL_000c: calli vararg int32(int32, ..., int32, int32)
		IL_0011: ret
	} // end of method StandAloneMethodSigTestCases::CallIndirectVarArg

	.method private hidebysig 
		instance int32 IndirectCallInstanceTargetMethod (
			int32 'value'
		) cil managed 
	{
		// Header size: 1
		// Code size: 2 (0x2)
		.maxstack 8

		IL_0000: ldarg.1
		IL_0001: ret
	} // end of method StandAloneMethodSigTestCases::IndirectCallInstanceTargetMethod

	.method private hidebysig static 
		int32 IndirectCallNativeTargetMethod (
			int32 a,
			int32 b,
			int32 c,
			int32 d,
			int32 e,
			int32 f,
			int32 g,
			int32 h
		) cil managed 
	{
		// Header size: 1
		// Code size: 20 (0x14)
		.maxstack 8

		IL_0000: ldarg.0
		IL_0001: ldarg.1
		IL_0002: add
		IL_0003: ldarg.2
		IL_0004: add
		IL_0005: ldarg.3
		IL_0006: add
		IL_0007: ldarg.s e
		IL_0009: add
		IL_000a: ldarg.s f
		IL_000c: add
		IL_000d: ldarg.s g
		IL_000f: add
		IL_0010: ldarg.s h
		IL_0012: add
		IL_0013: ret
	} // end of method StandAloneMethodSigTestCases::IndirectCallNativeTargetMethod

	.method private hidebysig static 
		int32 IndirectCallStaticTargetMethod (
			int32 'value'
		) cil managed 
	{
		// Header size: 1
		// Code size: 2 (0x2)
		.maxstack 8

		IL_0000: ldarg.0
		IL_0001: ret
	} // end of method StandAloneMethodSigTestCases::IndirectCallStaticTargetMethod

	.method private hidebysig 
		instance void IndirectCallStaticVoidTargetMethod () cil managed 
	{
		// Header size: 1
		// Code size: 1 (0x1)
		.maxstack 8

		IL_0000: ret
	} // end of method StandAloneMethodSigTestCases::IndirectCallStaticVoidTargetMethod

	.method private hidebysig static 
		vararg int32 IndirectCallVarArgTargetMethod (
			int32 'value'
		) cil managed 
	{
		// Header size: 12
		// Code size: 19 (0x13)
		.maxstack 2
		.locals init (
			[0] valuetype [mscorlib]System.ArgIterator
		)

		IL_0000: ldloca.s 0
		IL_0002: arglist
		IL_0004: call instance void [mscorlib]System.ArgIterator::.ctor(valuetype [mscorlib]System.RuntimeArgumentHandle)
		IL_0009: ldarg.0
		IL_000a: ldloca.s 0
		IL_000c: call instance int32 [mscorlib]System.ArgIterator::GetRemainingCount()
		IL_0011: add
		IL_0012: ret
	} // end of method StandAloneMethodSigTestCases::IndirectCallVarArgTargetMethod

	.method public hidebysig 
		instance int32 MixedNonTailAndTailCall (
			bool branch
		) cil managed 
	{
		// Header size: 12
		// Code size: 20 (0x14)
		.maxstack 2

		IL_0000: ldarg.1
		IL_0001: brfalse.s IL_0012

		IL_0003: ldc.i4.1
		IL_0004: ldftn int32 InlineIL.Tests.UnverifiableAssemblyToProcess.StandAloneMethodSigTestCases::IndirectCallStaticTargetMethod(int32)
		IL_000a: tail.
		IL_000c: calli int32(int32)
		IL_0011: ret

		IL_0012: ldc.i4.2
		IL_0013: ret
	} // end of method StandAloneMethodSigTestCases::MixedNonTailAndTailCall

	.method public hidebysig 
		instance int32 MixedNonTailAndTailCall2 (
			bool branch
		) cil managed 
	{
		// Header size: 12
		// Code size: 20 (0x14)
		.maxstack 2

		IL_0000: ldarg.1
		IL_0001: brfalse.s IL_0005

		IL_0003: ldc.i4.1
		IL_0004: ret

		IL_0005: ldc.i4.2
		IL_0006: ldftn int32 InlineIL.Tests.UnverifiableAssemblyToProcess.StandAloneMethodSigTestCases::IndirectCallStaticTargetMethod(int32)
		IL_000c: tail.
		IL_000e: calli int32(int32)
		IL_0013: ret
	} // end of method StandAloneMethodSigTestCases::MixedNonTailAndTailCall2

	.method public hidebysig 
		instance int32 MultipleTailCalls (
			bool branch
		) cil managed 
	{
		// Header size: 12
		// Code size: 33 (0x21)
		.maxstack 2

		IL_0000: ldarg.1
		IL_0001: brfalse.s IL_0012

		IL_0003: ldc.i4.1
		IL_0004: ldftn int32 InlineIL.Tests.UnverifiableAssemblyToProcess.StandAloneMethodSigTestCases::IndirectCallStaticTargetMethod(int32)
		IL_000a: tail.
		IL_000c: calli int32(int32)
		IL_0011: ret

		IL_0012: ldc.i4.2
		IL_0013: ldftn int32 InlineIL.Tests.UnverifiableAssemblyToProcess.StandAloneMethodSigTestCases::IndirectCallStaticTargetMethod(int32)
		IL_0019: tail.
		IL_001b: calli int32(int32)
		IL_0020: ret
	} // end of method StandAloneMethodSigTestCases::MultipleTailCalls

	.method public hidebysig 
		instance int32 TailCallIndirectStatic () cil managed 
	{
		// Header size: 12
		// Code size: 16 (0x10)
		.maxstack 2

		IL_0000: ldc.i4.s 42
		IL_0002: ldftn int32 InlineIL.Tests.UnverifiableAssemblyToProcess.StandAloneMethodSigTestCases::IndirectCallStaticTargetMethod(int32)
		IL_0008: tail.
		IL_000a: calli int32(int32)
		IL_000f: ret
	} // end of method StandAloneMethodSigTestCases::TailCallIndirectStatic

	.method public hidebysig 
		instance void TailCallIndirectStaticVoid () cil managed 
	{
		// Header size: 12
		// Code size: 14 (0xe)
		.maxstack 1

		IL_0000: ldftn instance void InlineIL.Tests.UnverifiableAssemblyToProcess.StandAloneMethodSigTestCases::IndirectCallStaticVoidTargetMethod()
		IL_0006: tail.
		IL_0008: calli void()
		IL_000d: ret
	} // end of method StandAloneMethodSigTestCases::TailCallIndirectStaticVoid

} // end of class InlineIL.Tests.UnverifiableAssemblyToProcess.StandAloneMethodSigTestCases
