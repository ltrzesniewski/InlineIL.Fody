using InlineIL.Tests.InjectedAssembly;
using static InlineIL.IL.Emit;

namespace InlineIL.Tests.InvalidAssemblyToProcess;

#pragma warning disable CS0618

public partial class TypeRefTestCases
{
    private const string _injectedAltAssemblyPath = "InjectedDllDir/InlineIL.Tests.InjectedAssembly.Alternative.dll";

    public void UseMethodsFromDifferentVersionsOfDll()
    {
        // Use referenced DLL
        InjectedType.AddInt32(40, 2);

        // Use alternative version of the referenced DLL
        Ldc_I4(40);
        Ldc_I4_2();

        Call(
            MethodRef.Method(
                TypeRef.FromDllFile(_injectedAltAssemblyPath, "InlineIL.Tests.InjectedAssembly.InjectedType"),
                "MultiplyInt32"
            )
        );

        Pop();
    }

    public void UseMethodsFromDifferentVersionsOfDllUsingTypeReference()
    {
        // Use referenced DLL
        InjectedType.AddInt32(40, 2);

        // Use alternative version of the referenced DLL
        Ldc_I4(40);
        Ldc_I4_2();

        Call(
            MethodRef.Method(
                TypeRef.FromDllFile(_injectedAltAssemblyPath, typeof(InjectedType)),
                "MultiplyInt32"
            )
        );

        Pop();
    }

    public void InvalidInjectedDllFile()
    {
        Ldtoken(
            TypeRef.FromDllFile("InjectedDllDir/DoesNotExist.dll", "SomeType")
        );
    }

    public void InvalidInjectedTypeName()
    {
        Ldtoken(
            TypeRef.FromDllFile(_injectedAltAssemblyPath, "DoesNotExist")
        );
    }

    public void InvalidInjectedTypeSpec()
    {
        Ldtoken(
            TypeRef.FromDllFile(_injectedAltAssemblyPath, typeof(InjectedType[]))
        );
    }

    public void InvalidInjectedTypeSpec2()
    {
        Ldtoken(
            TypeRef.FromDllFile(_injectedAltAssemblyPath, typeof(InjectedType).MakeByRefType())
        );
    }

    public void InvalidInjectedFnPtr()
    {
        Ldtoken(
            TypeRef.FromDllFile(_injectedAltAssemblyPath, typeof(delegate*<int, void>))
        );
    }

    public void InvalidInjectedGenericParam<T>()
    {
        Ldtoken(
            TypeRef.FromDllFile(_injectedAltAssemblyPath, typeof(T))
        );
    }
}
