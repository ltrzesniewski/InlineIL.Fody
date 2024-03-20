using System;
using System.Diagnostics.CodeAnalysis;
using static InlineIL.IL.Emit;

namespace InlineIL.Tests.UnverifiableAssemblyToProcess;

#pragma warning disable CS0618

[SuppressMessage("ReSharper", "UnusedType.Global")]
[SuppressMessage("ReSharper", "UnusedMember.Global")]
public class TypeRefTestCases
{
    private const string _injectedAssemblyPath = "InjectedDllDir/InlineIL.Tests.InjectedAssembly.dll";

    public int UseInjectedDll()
    {
        Ldc_I4(40);
        Ldc_I4_2();

        Call(
            MethodRef.Method(
                TypeRef.FromDllFile(_injectedAssemblyPath, "InlineIL.Tests.InjectedAssembly.InjectedType"),
                "AddInt32"
            )
        );

        return IL.Return<int>();
    }

    public RuntimeTypeHandle ReturnInjectedTypeSpec()
    {
        Ldtoken(
            TypeRef.FromDllFile(_injectedAssemblyPath, "InlineIL.Tests.InjectedAssembly.InjectedType")
                   .MakeArrayType()
        );

        return IL.Return<RuntimeTypeHandle>();
    }

    public RuntimeTypeHandle ReturnInjectedGenericTypeSpec()
    {
        Ldtoken(
            TypeRef.FromDllFile(_injectedAssemblyPath, "InlineIL.Tests.InjectedAssembly.InjectedGenericType`1")
        );

        return IL.Return<RuntimeTypeHandle>();
    }

    public RuntimeTypeHandle ReturnInjectedGenericTypeSpec2()
    {
        Ldtoken(
            TypeRef.FromDllFile(_injectedAssemblyPath, "InlineIL.Tests.InjectedAssembly.InjectedGenericType`2")
        );

        return IL.Return<RuntimeTypeHandle>();
    }

    public RuntimeTypeHandle ReturnInjectedConstructedGenericTypeSpec()
    {
        Ldtoken(
            TypeRef.FromDllFile(_injectedAssemblyPath, "InlineIL.Tests.InjectedAssembly.InjectedGenericType`1")
                   .MakeGenericType(
                       TypeRef.FromDllFile(_injectedAssemblyPath, "InlineIL.Tests.InjectedAssembly.InjectedType")
                   )
        );

        return IL.Return<RuntimeTypeHandle>();
    }
}
