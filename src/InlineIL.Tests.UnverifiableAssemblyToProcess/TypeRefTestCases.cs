using System;
using static InlineIL.IL.Emit;

namespace InlineIL.Tests.UnverifiableAssemblyToProcess;

public class TypeRefTestCases
{
    public int UseInjectedDll()
    {
        Ldc_I4(40);
        Ldc_I4_2();

        Call(
            MethodRef.Method(
#pragma warning disable CS0618
                TypeRef.FromDll(
                    "InjectedDllDir/InlineIL.Tests.InjectedAssembly.dll",
                    "InlineIL.Tests.InjectedAssembly.InjectedType"
                ),
#pragma warning restore CS0618
                "AddInt32"
            )
        );

        return IL.Return<int>();
    }

    public RuntimeTypeHandle ReturnInjectedTypeSpec()
    {
        Ldtoken(
#pragma warning disable CS0618
            TypeRef.FromDll(
                "InjectedDllDir/InlineIL.Tests.InjectedAssembly.dll",
                "InlineIL.Tests.InjectedAssembly.InjectedType"
            ).MakeArrayType()
#pragma warning restore CS0618
        );

        return IL.Return<RuntimeTypeHandle>();
    }
}
