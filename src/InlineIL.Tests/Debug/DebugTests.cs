extern alias standard;
using System;
using System.IO;
using System.Linq;
using System.Text;
using InlineIL.Fody.Processing;
using InlineIL.Fody.Support;
using InlineIL.Tests.AssemblyToProcess;
using InlineIL.Tests.Support;
using InlineIL.Tests.Weaving;
using Mono.Cecil;
using Mono.Cecil.Cil;
using Xunit.Abstractions;

namespace InlineIL.Tests.Debug;

public class DebugTests
{
    private readonly ITestOutputHelper _output;

    public DebugTests(ITestOutputHelper output)
    {
        _output = output;
    }

    [DebugTest]
    public void SingleMethod()
    {
        var assembly = typeof(AssemblyToProcessReference).Assembly;
        var type = typeof(MethodRefTestCases);
        var methodName = nameof(MethodRefTestCases.CallGenericArrayReturnType);

        using var assemblyResolver = WeaverRunner.CreateAssemblyResolver(assembly);
        var readerParams = new ReaderParameters { AssemblyResolver = assemblyResolver };

        using var module = ModuleDefinition.ReadModule(assembly.Location, readerParams);
        var weavingContext = new ModuleWeavingContext(module, new WeaverConfig(null, module));

        var typeDef = module.GetTypes().Single(i => i.FullName == type.FullName);
        var methodDef = typeDef.Methods.Single(i => i.Name == methodName);

        new MethodWeaver(weavingContext, methodDef, NoOpLogger.Instance).Process();
    }

    [DebugTest]
    public void SequencePoints()
    {
        using var assemblyStream = File.OpenRead(typeof(DebugTests).Assembly.Location);
        using var symbolsStream = File.OpenRead(Path.ChangeExtension(typeof(DebugTests).Assembly.Location, ".pdb"));

        var readerParams = new ReaderParameters
        {
            ReadSymbols = true,
            SymbolStream = symbolsStream,
            SymbolReaderProvider = new PortablePdbReaderProvider()
        };

        var assembly = AssemblyDefinition.ReadAssembly(assemblyStream, readerParams);

        var type = assembly.MainModule.GetType(typeof(DebugTests).FullName);
        var method = type.Methods.Single(i => i.Name == nameof(SequencePoints));

        var sequencePoints = method.DebugInformation.SequencePoints.ToDictionary(i => i.Offset);
        string[]? sourceLines = null;

        foreach (var instruction in method.Body.Instructions)
        {
            if (sequencePoints.TryGetValue(instruction.Offset, out var sequencePoint))
            {
                _output.WriteLine("");

                if (sequencePoint.IsHidden)
                {
                    _output.WriteLine("// Hidden");
                }
                else
                {
                    sourceLines ??= File.ReadAllLines(sequencePoint.Document.Url, Encoding.UTF8);

                    var text = sequencePoint.EndLine == sequencePoint.StartLine
                        ? sourceLines[sequencePoint.StartLine - 1].Substring(sequencePoint.StartColumn - 1, sequencePoint.EndColumn - sequencePoint.StartColumn)
                        : sourceLines[sequencePoint.StartLine - 1].Substring(sequencePoint.StartColumn - 1);

                    _output.WriteLine($"// {sequencePoint.StartLine}:{sequencePoint.StartColumn} - {sequencePoint.EndLine}:{sequencePoint.EndColumn} - {text}");
                }
            }

            _output.WriteLine(instruction.ToString());
        }

        // Examples
        GC.KeepAlive(0);
        GC.KeepAlive(1);
        GC.KeepAlive(2);
        {
            GC.KeepAlive(3);
            GC.KeepAlive(4);
            GC.KeepAlive(5);
        }
        GC.KeepAlive(6);
        GC.KeepAlive(7);
        GC.KeepAlive(8);
    }
}
