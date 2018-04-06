using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Reflection.Emit;
using InlineIL;

[SuppressMessage("ReSharper", "UnusedMember.Global")]
public class TypeRefTestCases
{
    public RuntimeTypeHandle ReturnTypeHandle<T>()
    {
        IL.Emit(OpCodes.Ldtoken, typeof(T));
        return IL.Return<RuntimeTypeHandle>();
    }

    [SuppressMessage("ReSharper", "RedundantCast")]
    public RuntimeTypeHandle[] LoadTypeDifferentWays()
    {
        var result = new RuntimeTypeHandle[3];

        IL.Push(result);
        IL.Emit(OpCodes.Ldc_I4_0);
        IL.Emit(OpCodes.Ldtoken, typeof(int));
        IL.Emit(OpCodes.Stelem, typeof(RuntimeTypeHandle));

        IL.Push(result);
        IL.Emit(OpCodes.Ldc_I4_1);
        IL.Emit(OpCodes.Ldtoken, new TypeRef(typeof(int)));
        IL.Emit(OpCodes.Stelem, new TypeRef(typeof(RuntimeTypeHandle)));

        IL.Push(result);
        IL.Emit(OpCodes.Ldc_I4_2);

        IL.Emit(OpCodes.Ldtoken, new TypeRef(TypeRef.CoreLibrary, "System.Int32"));
        IL.Emit(OpCodes.Stelem, new TypeRef(TypeRef.CoreLibrary, "System.RuntimeTypeHandle"));

        return result;
    }

    public RuntimeTypeHandle LoadPointerTypeUsingTypeRef()
    {
        IL.Emit(OpCodes.Ldtoken, new TypeRef(typeof(int)).MakePointerType().MakePointerType());
        return IL.Return<RuntimeTypeHandle>();
    }

    public RuntimeTypeHandle LoadPointerTypeUsingType()
    {
        IL.Emit(OpCodes.Ldtoken, typeof(int).MakePointerType().MakePointerType());
        return IL.Return<RuntimeTypeHandle>();
    }

    public RuntimeTypeHandle LoadReferenceTypeUsingTypeRef()
    {
        IL.Emit(OpCodes.Ldtoken, new TypeRef(typeof(int)).MakeByRefType());
        return IL.Return<RuntimeTypeHandle>();
    }

    public RuntimeTypeHandle LoadReferenceTypeUsingType()
    {
        IL.Emit(OpCodes.Ldtoken, typeof(int).MakeByRefType());
        return IL.Return<RuntimeTypeHandle>();
    }

    public RuntimeTypeHandle LoadArrayTypeUsingTypeRef()
    {
        IL.Emit(OpCodes.Ldtoken, new TypeRef(typeof(int)).MakeArrayType().MakeArrayType());
        return IL.Return<RuntimeTypeHandle>();
    }

    public RuntimeTypeHandle LoadArrayTypeUsingType()
    {
        IL.Emit(OpCodes.Ldtoken, typeof(int).MakeArrayType().MakeArrayType());
        return IL.Return<RuntimeTypeHandle>();
    }

    public RuntimeTypeHandle LoadArrayTypeWithRankUsingTypeRef()
    {
        IL.Emit(OpCodes.Ldtoken, new TypeRef(typeof(int)).MakeArrayType(3).MakeArrayType(1));
        return IL.Return<RuntimeTypeHandle>();
    }

    public RuntimeTypeHandle LoadArrayTypeWithRankUsingType()
    {
        IL.Emit(OpCodes.Ldtoken, typeof(int).MakeArrayType(3).MakeArrayType(1));
        return IL.Return<RuntimeTypeHandle>();
    }

    public RuntimeTypeHandle LoadGenericTypeUsingTypeRef()
    {
        IL.Emit(OpCodes.Ldtoken, new TypeRef(typeof(Dictionary<,>)).MakeGenericType(typeof(int), typeof(string)));
        return IL.Return<RuntimeTypeHandle>();
    }

    public RuntimeTypeHandle LoadGenericTypeUsingType()
    {
        IL.Emit(OpCodes.Ldtoken, typeof(Dictionary<,>).MakeGenericType(typeof(int), typeof(string)));
        return IL.Return<RuntimeTypeHandle>();
    }

    public Type ReturnNestedType()
    {
        IL.Emit(OpCodes.Ldtoken, new TypeRef("InlineIL.Tests.AssemblyToProcess", nameof(TypeRefTestCases) + "+" + nameof(NestedType)));
        IL.Emit(OpCodes.Call, new MethodRef(typeof(Type), nameof(Type.GetTypeFromHandle)));
        return IL.Return<Type>();
    }

    [SuppressMessage("ReSharper", "ClassNeverInstantiated.Local")]
    private class NestedType
    {
    }
}
