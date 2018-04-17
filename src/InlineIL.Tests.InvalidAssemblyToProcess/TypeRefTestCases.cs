using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using InlineIL;
using static InlineIL.IL.Emit;

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
        Ldtoken(new TypeRef("AssemblyThatDonestExist", "TypeThatDoesntExist"));
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
}
