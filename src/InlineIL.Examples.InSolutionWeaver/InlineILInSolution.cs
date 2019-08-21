using System;
using System.Diagnostics.CodeAnalysis;
using Fody;
using InlineIL.Fody;
using Mono.Cecil;

namespace InlineIL.Examples.InSolutionWeaver
{
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    [SuppressMessage("ReSharper", "UnusedMember.Global")]
    public class InlineILInSolution : ModuleWeaver
    {
        // This project enables in-solution weaving
        // https://github.com/Fody/Home/blob/master/pages/in-solution-weaving.md

        static InlineILInSolution()
        {
            GC.KeepAlive(typeof(ModuleDefinition));
            GC.KeepAlive(typeof(WeavingException));
        }
    }
}
