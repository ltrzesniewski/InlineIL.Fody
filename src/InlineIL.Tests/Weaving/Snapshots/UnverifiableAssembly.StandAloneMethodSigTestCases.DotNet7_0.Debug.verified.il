.class public auto ansi beforefieldinit InlineIL.Tests.UnverifiableAssemblyToProcess.StandAloneMethodSigTestCases
	extends [netstandard]System.Object
{
	.custom instance void [InlineIL.Tests.Common]InlineIL.Tests.Common.TestCasesAttribute::.ctor() = (
		01 00 00 00
	)
	// Nested Types
	.class nested private auto ansi sealed IntToIntCdecl
		extends [netstandard]System.MulticastDelegate
	{
		.custom instance void [netstandard]System.Runtime.InteropServices.UnmanagedFunctionPointerAttribute::.ctor(valuetype [netstandard]System.Runtime.InteropServices.CallingConvention) = (
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
			instance class [netstandard]System.IAsyncResult BeginInvoke (
				int32 a,
				int32 b,
				int32 c,
				int32 d,
				int32 e,
				int32 f,
				int32 g,
				int32 h,
				class [netstandard]System.AsyncCallback callback,
				object 'object'
			) runtime managed 
		{
		} // end of method IntToIntCdecl::BeginInvoke

		.method public hidebysig newslot virtual 
			instance int32 EndInvoke (
				class [netstandard]System.IAsyncResult result
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
		extends [netstandard]System.MulticastDelegate
	{
		.custom instance void [netstandard]System.Runtime.InteropServices.UnmanagedFunctionPointerAttribute::.ctor(valuetype [netstandard]System.Runtime.InteropServices.CallingConvention) = (
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
			instance class [netstandard]System.IAsyncResult BeginInvoke (
				int32 a,
				int32 b,
				int32 c,
				int32 d,
				int32 e,
				int32 f,
				int32 g,
				int32 h,
				class [netstandard]System.AsyncCallback callback,
				object 'object'
			) runtime managed 
		{
		} // end of method IntToIntStdcall::BeginInvoke

		.method public hidebysig newslot virtual 
			instance int32 EndInvoke (
				class [netstandard]System.IAsyncResult result
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
		// Code size: 8 (0x8)
		.maxstack 8

		IL_0000: ldarg.0
		IL_0001: call instance void [netstandard]System.Object::.ctor()
		IL_0006: nop
		IL_0007: ret
	} // end of method StandAloneMethodSigTestCases::.ctor

	.method public hidebysig 
		instance int32 BranchOverTailCall (
			bool branch
		) cil managed 
	{
		// Header size: 12
		// Code size: 22 (0x16)
		.maxstack 2
		.locals init (
			[0] int32
		)

		IL_0000: nop
		IL_0001: ldc.i4.s 42
		IL_0003: ldarg.1
		IL_0004: brtrue.s IL_0015

		IL_0006: dup
		IL_0007: add
		IL_0008: ldftn int32 InlineIL.Tests.UnverifiableAssemblyToProcess.StandAloneMethodSigTestCases::IndirectCallStaticTargetMethod(int32)
		IL_000e: tail.
		IL_0010: calli int32(int32)

		IL_0015: ret
	} // end of method StandAloneMethodSigTestCases::BranchOverTailCall

	.method public hidebysig 
		instance int32 CallIndirectInstance () cil managed 
	{
		// Header size: 12
		// Code size: 20 (0x14)
		.maxstack 3
		.locals init (
			[0] int32
		)

		IL_0000: nop
		IL_0001: ldarg.0
		IL_0002: ldc.i4.s 42
		IL_0004: ldftn instance int32 InlineIL.Tests.UnverifiableAssemblyToProcess.StandAloneMethodSigTestCases::IndirectCallInstanceTargetMethod(int32)
		IL_000a: calli instance int32(int32)
		IL_000f: stloc.0
		IL_0010: br.s IL_0012

		IL_0012: ldloc.0
		IL_0013: ret
	} // end of method StandAloneMethodSigTestCases::CallIndirectInstance

	.method public hidebysig 
		instance int32 CallIndirectNativeCdecl () cil managed 
	{
		// Header size: 12
		// Code size: 59 (0x3b)
		.maxstack 9
		.locals init (
			[0] class InlineIL.Tests.UnverifiableAssemblyToProcess.StandAloneMethodSigTestCases/IntToIntCdecl,
			[1] native int,
			[2] int32
		)

		IL_0000: nop
		IL_0001: ldnull
		IL_0002: ldftn int32 InlineIL.Tests.UnverifiableAssemblyToProcess.StandAloneMethodSigTestCases::IndirectCallNativeTargetMethod(int32, int32, int32, int32, int32, int32, int32, int32)
		IL_0008: newobj instance void InlineIL.Tests.UnverifiableAssemblyToProcess.StandAloneMethodSigTestCases/IntToIntCdecl::.ctor(object, native int)
		IL_000d: stloc.0
		IL_000e: ldc.i4.s 40
		IL_0010: ldc.i4.2
		IL_0011: ldc.i4.s 40
		IL_0013: ldc.i4.s -20
		IL_0015: ldc.i4.s -20
		IL_0017: ldc.i4.s -20
		IL_0019: ldc.i4.5
		IL_001a: ldc.i4.s 15
		IL_001c: ldloc.0
		IL_001d: call native int [netstandard]System.Runtime.InteropServices.Marshal::GetFunctionPointerForDelegate<class InlineIL.Tests.UnverifiableAssemblyToProcess.StandAloneMethodSigTestCases/IntToIntCdecl>(!!0)
		IL_0022: stloc.1
		IL_0023: ldloca.s 1
		IL_0025: call instance void* [netstandard]System.IntPtr::ToPointer()
		IL_002a: calli unmanaged cdecl int32(int32, int32, int32, int32, int32, int32, int32, int32)
		IL_002f: ldloc.0
		IL_0030: call void [netstandard]System.GC::KeepAlive(object)
		IL_0035: nop
		IL_0036: stloc.2
		IL_0037: br.s IL_0039

		IL_0039: ldloc.2
		IL_003a: ret
	} // end of method StandAloneMethodSigTestCases::CallIndirectNativeCdecl

	.method public hidebysig 
		instance int32 CallIndirectNativeStdcall () cil managed 
	{
		// Header size: 12
		// Code size: 59 (0x3b)
		.maxstack 9
		.locals init (
			[0] class InlineIL.Tests.UnverifiableAssemblyToProcess.StandAloneMethodSigTestCases/IntToIntStdcall,
			[1] native int,
			[2] int32
		)

		IL_0000: nop
		IL_0001: ldnull
		IL_0002: ldftn int32 InlineIL.Tests.UnverifiableAssemblyToProcess.StandAloneMethodSigTestCases::IndirectCallNativeTargetMethod(int32, int32, int32, int32, int32, int32, int32, int32)
		IL_0008: newobj instance void InlineIL.Tests.UnverifiableAssemblyToProcess.StandAloneMethodSigTestCases/IntToIntStdcall::.ctor(object, native int)
		IL_000d: stloc.0
		IL_000e: ldc.i4.s 40
		IL_0010: ldc.i4.2
		IL_0011: ldc.i4.s 40
		IL_0013: ldc.i4.s -20
		IL_0015: ldc.i4.s -20
		IL_0017: ldc.i4.s -20
		IL_0019: ldc.i4.5
		IL_001a: ldc.i4.s 15
		IL_001c: ldloc.0
		IL_001d: call native int [netstandard]System.Runtime.InteropServices.Marshal::GetFunctionPointerForDelegate<class InlineIL.Tests.UnverifiableAssemblyToProcess.StandAloneMethodSigTestCases/IntToIntStdcall>(!!0)
		IL_0022: stloc.1
		IL_0023: ldloca.s 1
		IL_0025: call instance void* [netstandard]System.IntPtr::ToPointer()
		IL_002a: calli unmanaged stdcall int32(int32, int32, int32, int32, int32, int32, int32, int32)
		IL_002f: ldloc.0
		IL_0030: call void [netstandard]System.GC::KeepAlive(object)
		IL_0035: nop
		IL_0036: stloc.2
		IL_0037: br.s IL_0039

		IL_0039: ldloc.2
		IL_003a: ret
	} // end of method StandAloneMethodSigTestCases::CallIndirectNativeStdcall

	.method public hidebysig 
		instance int32 CallIndirectNativeStdcallAlt () cil managed 
	{
		// Header size: 12
		// Code size: 59 (0x3b)
		.maxstack 9
		.locals init (
			[0] class InlineIL.Tests.UnverifiableAssemblyToProcess.StandAloneMethodSigTestCases/IntToIntStdcall,
			[1] native int,
			[2] int32
		)

		IL_0000: nop
		IL_0001: ldnull
		IL_0002: ldftn int32 InlineIL.Tests.UnverifiableAssemblyToProcess.StandAloneMethodSigTestCases::IndirectCallNativeTargetMethod(int32, int32, int32, int32, int32, int32, int32, int32)
		IL_0008: newobj instance void InlineIL.Tests.UnverifiableAssemblyToProcess.StandAloneMethodSigTestCases/IntToIntStdcall::.ctor(object, native int)
		IL_000d: stloc.0
		IL_000e: ldc.i4.s 40
		IL_0010: ldc.i4.2
		IL_0011: ldc.i4.s 40
		IL_0013: ldc.i4.s -20
		IL_0015: ldc.i4.s -20
		IL_0017: ldc.i4.s -20
		IL_0019: ldc.i4.5
		IL_001a: ldc.i4.s 15
		IL_001c: ldloc.0
		IL_001d: call native int [netstandard]System.Runtime.InteropServices.Marshal::GetFunctionPointerForDelegate<class InlineIL.Tests.UnverifiableAssemblyToProcess.StandAloneMethodSigTestCases/IntToIntStdcall>(!!0)
		IL_0022: stloc.1
		IL_0023: ldloca.s 1
		IL_0025: call instance void* [netstandard]System.IntPtr::ToPointer()
		IL_002a: calli unmanaged stdcall int32(int32, int32, int32, int32, int32, int32, int32, int32)
		IL_002f: ldloc.0
		IL_0030: call void [netstandard]System.GC::KeepAlive(object)
		IL_0035: nop
		IL_0036: stloc.2
		IL_0037: br.s IL_0039

		IL_0039: ldloc.2
		IL_003a: ret
	} // end of method StandAloneMethodSigTestCases::CallIndirectNativeStdcallAlt

	.method public hidebysig 
		instance int32 CallIndirectStatic () cil managed 
	{
		// Header size: 12
		// Code size: 19 (0x13)
		.maxstack 2
		.locals init (
			[0] int32
		)

		IL_0000: nop
		IL_0001: ldc.i4.s 42
		IL_0003: ldftn int32 InlineIL.Tests.UnverifiableAssemblyToProcess.StandAloneMethodSigTestCases::IndirectCallStaticTargetMethod(int32)
		IL_0009: calli int32(int32)
		IL_000e: stloc.0
		IL_000f: br.s IL_0011

		IL_0011: ldloc.0
		IL_0012: ret
	} // end of method StandAloneMethodSigTestCases::CallIndirectStatic

	.method public hidebysig 
		instance int32 CallIndirectStaticAlt () cil managed 
	{
		// Header size: 12
		// Code size: 19 (0x13)
		.maxstack 2
		.locals init (
			[0] int32
		)

		IL_0000: nop
		IL_0001: ldc.i4.s 42
		IL_0003: ldftn int32 InlineIL.Tests.UnverifiableAssemblyToProcess.StandAloneMethodSigTestCases::IndirectCallStaticTargetMethod(int32)
		IL_0009: calli int32(int32)
		IL_000e: stloc.0
		IL_000f: br.s IL_0011

		IL_0011: ldloc.0
		IL_0012: ret
	} // end of method StandAloneMethodSigTestCases::CallIndirectStaticAlt

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
		// Code size: 2 (0x2)
		.maxstack 8

		IL_0000: nop
		IL_0001: ret
	} // end of method StandAloneMethodSigTestCases::IndirectCallStaticVoidTargetMethod

	.method public hidebysig 
		instance int32 MixedNonTailAndTailCall (
			bool branch
		) cil managed 
	{
		// Header size: 12
		// Code size: 24 (0x18)
		.maxstack 2
		.locals init (
			[0] bool,
			[1] int32
		)

		IL_0000: nop
		IL_0001: ldarg.1
		IL_0002: stloc.0
		IL_0003: ldloc.0
		IL_0004: brfalse.s IL_0016

		IL_0006: nop
		IL_0007: ldc.i4.1
		IL_0008: ldftn int32 InlineIL.Tests.UnverifiableAssemblyToProcess.StandAloneMethodSigTestCases::IndirectCallStaticTargetMethod(int32)
		IL_000e: tail.
		IL_0010: calli int32(int32)
		IL_0015: ret

		IL_0016: ldc.i4.2
		IL_0017: ret
	} // end of method StandAloneMethodSigTestCases::MixedNonTailAndTailCall

	.method public hidebysig 
		instance int32 MixedNonTailAndTailCall2 (
			bool branch
		) cil managed 
	{
		// Header size: 12
		// Code size: 24 (0x18)
		.maxstack 2
		.locals init (
			[0] bool,
			[1] int32
		)

		IL_0000: nop
		IL_0001: ldarg.1
		IL_0002: stloc.0
		IL_0003: ldloc.0
		IL_0004: brfalse.s IL_0009

		IL_0006: nop
		IL_0007: ldc.i4.1
		IL_0008: ret

		IL_0009: ldc.i4.2
		IL_000a: ldftn int32 InlineIL.Tests.UnverifiableAssemblyToProcess.StandAloneMethodSigTestCases::IndirectCallStaticTargetMethod(int32)
		IL_0010: tail.
		IL_0012: calli int32(int32)
		IL_0017: ret
	} // end of method StandAloneMethodSigTestCases::MixedNonTailAndTailCall2

	.method public hidebysig 
		instance int32 MultipleTailCalls (
			bool branch
		) cil managed 
	{
		// Header size: 12
		// Code size: 37 (0x25)
		.maxstack 2
		.locals init (
			[0] bool,
			[1] int32
		)

		IL_0000: nop
		IL_0001: ldarg.1
		IL_0002: stloc.0
		IL_0003: ldloc.0
		IL_0004: brfalse.s IL_0016

		IL_0006: nop
		IL_0007: ldc.i4.1
		IL_0008: ldftn int32 InlineIL.Tests.UnverifiableAssemblyToProcess.StandAloneMethodSigTestCases::IndirectCallStaticTargetMethod(int32)
		IL_000e: tail.
		IL_0010: calli int32(int32)
		IL_0015: ret

		IL_0016: ldc.i4.2
		IL_0017: ldftn int32 InlineIL.Tests.UnverifiableAssemblyToProcess.StandAloneMethodSigTestCases::IndirectCallStaticTargetMethod(int32)
		IL_001d: tail.
		IL_001f: calli int32(int32)
		IL_0024: ret
	} // end of method StandAloneMethodSigTestCases::MultipleTailCalls

	.method public hidebysig 
		instance int32 TailCallIndirectStatic () cil managed 
	{
		// Header size: 12
		// Code size: 17 (0x11)
		.maxstack 2
		.locals init (
			[0] int32
		)

		IL_0000: nop
		IL_0001: ldc.i4.s 42
		IL_0003: ldftn int32 InlineIL.Tests.UnverifiableAssemblyToProcess.StandAloneMethodSigTestCases::IndirectCallStaticTargetMethod(int32)
		IL_0009: tail.
		IL_000b: calli int32(int32)
		IL_0010: ret
	} // end of method StandAloneMethodSigTestCases::TailCallIndirectStatic

	.method public hidebysig 
		instance void TailCallIndirectStaticVoid () cil managed 
	{
		// Header size: 12
		// Code size: 15 (0xf)
		.maxstack 1

		IL_0000: nop
		IL_0001: ldftn instance void InlineIL.Tests.UnverifiableAssemblyToProcess.StandAloneMethodSigTestCases::IndirectCallStaticVoidTargetMethod()
		IL_0007: tail.
		IL_0009: calli void()
		IL_000e: ret
	} // end of method StandAloneMethodSigTestCases::TailCallIndirectStaticVoid

} // end of class InlineIL.Tests.UnverifiableAssemblyToProcess.StandAloneMethodSigTestCases
