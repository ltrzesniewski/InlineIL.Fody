using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;
using Fody;
using InlineIL.Fody;
using Mono.Cecil;

namespace InlineIL.Examples.InSolutionWeaver;

[SuppressMessage("ReSharper", "UnusedType.Global")]
[SuppressMessage("ReSharper", "UnusedMember.Global")]
public class InlineILInSolution : BaseModuleWeaver
{
    // This project enables in-solution weaving
    // https://github.com/Fody/Home/blob/master/pages/in-solution-weaving.md

    private readonly ModuleWeaver _moduleWeaver = new();

    static InlineILInSolution()
    {
        GC.KeepAlive(typeof(ModuleDefinition));
        GC.KeepAlive(typeof(WeavingException));
    }

    public override void Execute()
    {
        var properties = typeof(BaseModuleWeaver).GetProperties(BindingFlags.Instance | BindingFlags.Public)
                                                 .Where(p => p.CanRead && p.CanWrite);

        foreach (var property in properties)
            property.SetValue(_moduleWeaver, property.GetValue(this));

        _moduleWeaver.Execute();
    }

    public override IEnumerable<string> GetAssembliesForScanning()
        => _moduleWeaver.GetAssembliesForScanning();

    public override bool ShouldCleanReference => _moduleWeaver.ShouldCleanReference;
}
