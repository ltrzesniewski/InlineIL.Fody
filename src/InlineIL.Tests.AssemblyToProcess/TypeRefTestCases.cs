using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using static InlineIL.IL.Emit;

namespace InlineIL.Tests.AssemblyToProcess;

[SuppressMessage("ReSharper", "UnusedType.Global")]
[SuppressMessage("ReSharper", "UnusedMember.Global")]
[SuppressMessage("ReSharper", "UnusedParameter.Global")]
[SuppressMessage("ReSharper", "EntityNameCapturedOnly.Global")]
public class TypeRefTestCases
{
    private const string _thisAssemblyName =
#if NETSTANDARD
        "InlineIL.Tests.StandardAssemblyToProcess";
#else
        "InlineIL.Tests.AssemblyToProcess";
#endif

    public RuntimeTypeHandle ReturnTypeHandle<T>()
    {
        Ldtoken(typeof(T));
        return IL.Return<RuntimeTypeHandle>();
    }

    public RuntimeTypeHandle ReturnTypeHandle()
    {
        Ldtoken(typeof(Guid));
        return IL.Return<RuntimeTypeHandle>();
    }

    public RuntimeTypeHandle ReturnTypeHandleGeneric<T>()
    {
        Ldtoken<T>();
        return IL.Return<RuntimeTypeHandle>();
    }

    public RuntimeTypeHandle ReturnTypeHandleGeneric()
    {
        Ldtoken<Guid>();
        return IL.Return<RuntimeTypeHandle>();
    }

#if NET9_0_OR_GREATER && CSHARP_13_OR_GREATER
    public RuntimeTypeHandle ReturnTypeHandleGenericRefStruct()
    {
        Ldtoken<RefStruct>();
        return IL.Return<RuntimeTypeHandle>();
    }
#endif

    public bool IsString(object obj)
    {
        Ldarg(nameof(obj));
        Isinst<string>();
        Ldnull();
        Cgt_Un();
        return IL.Return<bool>();
    }

    public bool GenericIsinst<T>(object obj)
    {
        Ldarg(nameof(obj));
        Isinst<T>();
        Ldnull();
        Cgt_Un();
        return IL.Return<bool>();
    }

    public RuntimeTypeHandle[] LoadTypeDifferentWays()
    {
        var result = new RuntimeTypeHandle[6];
        IL.EnsureLocal(result);

        IL.Push(result);
        Ldc_I4_0();
        Ldtoken(typeof(int));
        Stelem_Any(typeof(RuntimeTypeHandle));

        IL.Push(result);
        Ldc_I4_1();
        Ldtoken(new TypeRef(typeof(int)));
        Stelem_Any(new TypeRef(typeof(RuntimeTypeHandle)));

        IL.Push(result);
        Ldc_I4_2();
        Ldtoken(new TypeRef(TypeRef.CoreLibrary, "System.Int32"));
        Stelem_Any(new TypeRef(TypeRef.CoreLibrary, "System.RuntimeTypeHandle"));

        IL.Push(result);
        Ldc_I4_3();
        Ldtoken<int>();
        Stelem_Any<RuntimeTypeHandle>();

        IL.Push(result);
        Ldc_I4_4();
        Ldtoken(TypeRef.Type(typeof(int)));
        Stelem_Any<RuntimeTypeHandle>();

        IL.Push(result);
        Ldc_I4_5();
        Ldtoken(TypeRef.Type<int>());
        Stelem_Any<RuntimeTypeHandle>();

        return result;
    }

    public RuntimeTypeHandle LoadPointerTypeUsingTypeRef()
    {
        Ldtoken(new TypeRef(typeof(int)).MakePointerType().MakePointerType());
        return IL.Return<RuntimeTypeHandle>();
    }

    public RuntimeTypeHandle LoadPointerTypeUsingType()
    {
        Ldtoken(typeof(int).MakePointerType().MakePointerType());
        return IL.Return<RuntimeTypeHandle>();
    }

    public RuntimeTypeHandle LoadReferenceTypeUsingTypeRef()
    {
        Ldtoken(new TypeRef(typeof(int)).MakeByRefType());
        return IL.Return<RuntimeTypeHandle>();
    }

    public RuntimeTypeHandle LoadReferenceTypeUsingType()
    {
        Ldtoken(typeof(int).MakeByRefType());
        return IL.Return<RuntimeTypeHandle>();
    }

    public RuntimeTypeHandle LoadArrayTypeUsingTypeRef()
    {
        Ldtoken(new TypeRef(typeof(int)).MakeArrayType().MakeArrayType());
        return IL.Return<RuntimeTypeHandle>();
    }

    public RuntimeTypeHandle LoadArrayTypeUsingType()
    {
        Ldtoken(typeof(int).MakeArrayType().MakeArrayType());
        return IL.Return<RuntimeTypeHandle>();
    }

    public RuntimeTypeHandle LoadArrayTypeWithRankUsingTypeRef()
    {
        Ldtoken(new TypeRef(typeof(int)).MakeArrayType(3).MakeArrayType(1));
        return IL.Return<RuntimeTypeHandle>();
    }

    public RuntimeTypeHandle LoadArrayTypeWithRankUsingType()
    {
        Ldtoken(typeof(int).MakeArrayType(3).MakeArrayType(1));
        return IL.Return<RuntimeTypeHandle>();
    }

    public RuntimeTypeHandle LoadGenericTypeUsingTypeRef()
    {
        Ldtoken(new TypeRef(typeof(Dictionary<,>)).MakeGenericType(typeof(int), typeof(string)));
        return IL.Return<RuntimeTypeHandle>();
    }

    public RuntimeTypeHandle LoadGenericTypeByName()
    {
        Ldtoken(new TypeRef(TypeRef.CoreLibrary, "System.Action`1").MakeGenericType(typeof(int)));
        return IL.Return<RuntimeTypeHandle>();
    }

    public RuntimeTypeHandle LoadOpenGenericTypeByName()
    {
        Ldtoken(new TypeRef(TypeRef.CoreLibrary, "System.Action`1"));
        return IL.Return<RuntimeTypeHandle>();
    }

    public RuntimeTypeHandle LoadGenericTypeUsingType()
    {
        Ldtoken(typeof(Dictionary<,>).MakeGenericType(typeof(int), typeof(string)));
        return IL.Return<RuntimeTypeHandle>();
    }

    public Type ReturnNestedTypeUsingRuntimeSyntax()
    {
        Ldtoken(new TypeRef(_thisAssemblyName, "InlineIL.Tests.AssemblyToProcess." + nameof(TypeRefTestCases) + "+" + nameof(NestedType)));
        Call(new MethodRef(typeof(Type), nameof(Type.GetTypeFromHandle)));
        return IL.Return<Type>();
    }

    public Type ReturnNestedTypeUsingEcmaSyntax()
    {
        Ldtoken(new TypeRef(_thisAssemblyName, "InlineIL.Tests.AssemblyToProcess." + nameof(TypeRefTestCases) + "/" + nameof(NestedType)));
        Call(new MethodRef(typeof(Type), nameof(Type.GetTypeFromHandle)));
        return IL.Return<Type>();
    }

#if NET
    public Type ReturnNestedForwardedTypeUsingRuntimeSyntax()
    {
        Ldtoken(new TypeRef(TypeRef.CoreLibrary, "System.Span`1+Enumerator"));
        Call(new MethodRef(typeof(Type), nameof(Type.GetTypeFromHandle)));
        return IL.Return<Type>();
    }

    public Type ReturnNestedForwardedTypeUsingEcmaSyntax()
    {
        Ldtoken(new TypeRef(TypeRef.CoreLibrary, "System.Span`1/Enumerator"));
        Call(new MethodRef(typeof(Type), nameof(Type.GetTypeFromHandle)));
        return IL.Return<Type>();
    }

    public Type ReturnForwardedType()
    {
        Ldtoken(new TypeRef("System.Runtime.Extensions", "System.Math"));
        Call(new MethodRef(typeof(Type), nameof(Type.GetTypeFromHandle)));
        return IL.Return<Type>();
    }
#endif

    public string ReturnCoreLibrary()
    {
        return TypeRef.CoreLibrary;
    }

    [SuppressMessage("ReSharper", "ClassNeverInstantiated.Local")]
    private class NestedType
    {
    }

    [SuppressMessage("ReSharper", "UnusedType.Local")]
    private ref struct RefStruct
    {
    }
}
