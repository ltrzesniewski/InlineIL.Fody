using System;
using System.Diagnostics.CodeAnalysis;
using InlineIL.Fody;
using Mono.Cecil;

namespace Weavers
{
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public class InlineILInSolution : ModuleWeaver
    {
        // This project enables in-solution weaving
        // https://github.com/Fody/Fody/wiki/InSolutionWeaving

        static InlineILInSolution()
        {
            GC.KeepAlive(typeof(ModuleDefinition));
        }
    }
}
