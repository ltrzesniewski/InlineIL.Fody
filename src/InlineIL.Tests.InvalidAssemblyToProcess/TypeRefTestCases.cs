using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using static InlineIL.IL.Emit;

namespace InlineIL.Tests.InvalidAssemblyToProcess
{
    [SuppressMessage("ReSharper", "UnusedMember.Global")]
    public class TypeRefTestCases
    {
        public void LoadNullType()
        {
            Ldtoken(new TypeRef(null));
        }

        public void LoadNullTypeRef()
        {
            Ldtoken((TypeRef)null);
        }

        public void InvalidAssembly()
        {
            Ldtoken(new TypeRef("AssemblyThatDoesntExist", "TypeThatDoesntExist"));
        }

        public void InvalidType()
        {
            Ldtoken(new TypeRef("System", "TypeThatDoesntExist"));
        }

        public void InvalidArrayRank()
        {
            Ldtoken(typeof(int).MakeArrayType(-1));
        }

        public void UnusedInstance()
        {
            GC.KeepAlive(new TypeRef(typeof(int)));
        }

        public void NotAGenericType()
        {
            Ldtoken(typeof(string).MakeGenericType(typeof(int)));
        }

        public void NoGenericTypeArgs()
        {
            Ldtoken(typeof(Dictionary<,>).MakeGenericType());
        }

        public void InvalidGenericArgsCount()
        {
            Ldtoken(typeof(Dictionary<,>).MakeGenericType(typeof(int), typeof(double), typeof(string)));
        }

        public void GenericParamsOnGenericInstance()
        {
            Ldtoken(typeof(List<>).MakeGenericType(typeof(int)).MakeGenericType(typeof(int)));
        }

        public void ByRefOfByRef()
        {
            Ldtoken(typeof(int).MakeByRefType().MakeByRefType());
        }

        public void PointerToByRef()
        {
            Ldtoken(typeof(int).MakeByRefType().MakePointerType());
        }

        public void ArrayOfByRef()
        {
            Ldtoken(typeof(int).MakeByRefType().MakeArrayType());
        }

        public void GenericOfByRef()
        {
            Ldtoken(typeof(List<>).MakeByRefType().MakeGenericType(typeof(int)));
        }

        public void ByRefOfTypedReference()
        {
            Ldtoken(typeof(TypedReference).MakeByRefType());
        }

        public void PointerToTypedReference()
        {
            Ldtoken(typeof(TypedReference).MakeByRefType());
        }

        public void ArrayOfTypedReference()
        {
            Ldtoken(typeof(TypedReference).MakeByRefType());
        }
    }
}
