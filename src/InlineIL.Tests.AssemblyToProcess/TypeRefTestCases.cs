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
        IL.Emit(OpCodes.Ret);
        throw IL.Unreachable();
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
        IL.Emit(OpCodes.Ldtoken, new TypeRef("mscorlib", "System.Int32"));
        IL.Emit(OpCodes.Stelem, new TypeRef("mscorlib", "System.RuntimeTypeHandle"));

        return result;
    }

    public RuntimeTypeHandle LoadPointerType()
    {
        IL.Emit(OpCodes.Ldtoken, new TypeRef(typeof(int)).MakePointerType().MakePointerType());
        IL.Emit(OpCodes.Ret);
        throw IL.Unreachable();
    }

    public RuntimeTypeHandle LoadReferenceType()
    {
        IL.Emit(OpCodes.Ldtoken, new TypeRef(typeof(int)).MakeByRefType());
        IL.Emit(OpCodes.Ret);
        throw IL.Unreachable();
    }

    public RuntimeTypeHandle LoadArrayType()
    {
        IL.Emit(OpCodes.Ldtoken, new TypeRef(typeof(int)).MakeArrayType().MakeArrayType());
        IL.Emit(OpCodes.Ret);
        throw IL.Unreachable();
    }

    public RuntimeTypeHandle LoadArrayTypeWithRank()
    {
        IL.Emit(OpCodes.Ldtoken, new TypeRef(typeof(int)).MakeArrayType(3).MakeArrayType(1));
        IL.Emit(OpCodes.Ret);
        throw IL.Unreachable();
    }

    public RuntimeTypeHandle LoadGenericType()
    {
        IL.Emit(OpCodes.Ldtoken, new TypeRef(typeof(Dictionary<,>)).MakeGenericType(typeof(int), typeof(string)));
        IL.Emit(OpCodes.Ret);
        throw IL.Unreachable();
    }

    public Type ReturnNestedType()
    {
        IL.Emit(OpCodes.Ldtoken, new TypeRef("InlineIL.Tests.AssemblyToProcess", nameof(TypeRefTestCases) + "+" + nameof(NestedType)));
        IL.Emit(OpCodes.Call, new MethodRef(typeof(Type), nameof(Type.GetTypeFromHandle)));
        IL.Emit(OpCodes.Ret);
        throw IL.Unreachable();
    }

    [SuppressMessage("ReSharper", "ClassNeverInstantiated.Local")]
    private class NestedType
    {
    }
}
