using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using static InlineIL.IL.Emit;

namespace InlineIL.Tests.AssemblyToProcess
{
    [SuppressMessage("ReSharper", "UnusedMember.Global")]
    public class TypeRefTestCases
    {
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

        [SuppressMessage("ReSharper", "RedundantCast")]
        public RuntimeTypeHandle[] LoadTypeDifferentWays()
        {
            var result = new RuntimeTypeHandle[3];

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

        public RuntimeTypeHandle LoadGenericTypeUsingType()
        {
            Ldtoken(typeof(Dictionary<,>).MakeGenericType(typeof(int), typeof(string)));
            return IL.Return<RuntimeTypeHandle>();
        }

        public Type ReturnNestedTypeUsingRuntimeSyntax()
        {
            Ldtoken(new TypeRef("InlineIL.Tests.AssemblyToProcess", "InlineIL.Tests.AssemblyToProcess." + nameof(TypeRefTestCases) + "+" + nameof(NestedType)));
            Call(new MethodRef(typeof(Type), nameof(Type.GetTypeFromHandle)));
            return IL.Return<Type>();
        }

        public Type ReturnNestedTypeUsingEcmaSyntax()
        {
            Ldtoken(new TypeRef("InlineIL.Tests.AssemblyToProcess", "InlineIL.Tests.AssemblyToProcess." + nameof(TypeRefTestCases) + "/" + nameof(NestedType)));
            Call(new MethodRef(typeof(Type), nameof(Type.GetTypeFromHandle)));
            return IL.Return<Type>();
        }

        public string ReturnCoreLibrary()
        {
            return TypeRef.CoreLibrary;
        }

        [SuppressMessage("ReSharper", "ClassNeverInstantiated.Local")]
        private class NestedType
        {
        }
    }
}
