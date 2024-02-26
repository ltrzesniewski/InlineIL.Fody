using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using Fody;
using Mono.Cecil;
using Mono.Cecil.Cil;

#pragma warning disable CS0618

namespace InlineIL.Tests.Weaving;

internal static class WeaverRunner
{
    // This runner provides an assembly resolver that targets reference assemblies,
    // just like in the real weaving task.
    // Fody's WeaverTestHelper resolves references to implementation assemblies.

    public static TestRunResult ExecuteTestRun(Assembly assembly,
                                               BaseModuleWeaver weaver,
                                               bool runPeVerify = true,
                                               IEnumerable<string>? ignoreCodes = null)
    {
        var referencePaths = GetReferencePaths(assembly);
        var (inputFile, outputFile) = PrepareDirectories(assembly, referencePaths);

        using var assemblyResolver = new AssemblyResolver(referencePaths);

        var typeCache = new TypeCache(assemblyResolver.Resolve);
        typeCache.BuildAssembliesToScan(weaver);

        var testResult = new TestResult();
        WireLogging(weaver, testResult);

        weaver.FindType = typeCache.FindType;
        weaver.TryFindType = typeCache.TryFindType;
        weaver.AssemblyFilePath = inputFile;
        weaver.AssemblyResolver = assemblyResolver;

        var readerParameters = new ReaderParameters
        {
            AssemblyResolver = assemblyResolver,
            SymbolReaderProvider = new DefaultSymbolReaderProvider(),
            ReadWrite = false,
            ReadSymbols = true
        };

        using (var module = ModuleDefinition.ReadModule(inputFile, readerParameters))
        {
            weaver.ModuleDefinition = module;
            weaver.TypeSystem = new(typeCache.FindType, module);

            weaver.Execute();

            var writerParameters = new WriterParameters
            {
                WriteSymbols = true,
                SymbolWriterProvider = new EmbeddedPortablePdbWriterProvider()
            };

            module.Write(outputFile, writerParameters);
        }

        if (runPeVerify && RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
        {
            var ignoreList = ignoreCodes == null ? new() : ignoreCodes.ToList();
            PeVerifier.ThrowIfDifferent(inputFile, outputFile, ignoreList, Path.GetDirectoryName(inputFile));
        }

        typeof(TestResult).GetProperty(nameof(TestResult.Assembly))!.SetValue(testResult, Assembly.LoadFile(outputFile));
        typeof(TestResult).GetProperty(nameof(TestResult.AssemblyPath))!.SetValue(testResult, outputFile);

        var resultReaderParams = new ReaderParameters(ReadingMode.Immediate)
        {
            ReadSymbols = true,
            AssemblyResolver = assemblyResolver
        };

        var inputModule = ModuleDefinition.ReadModule(inputFile, resultReaderParams);
        var outputModule = ModuleDefinition.ReadModule(testResult.AssemblyPath, resultReaderParams);

        return new TestRunResult(testResult, inputModule, outputModule);
    }

    public static IAssemblyResolver CreateAssemblyResolver(Assembly assembly)
        => new AssemblyResolver(GetReferencePaths(assembly));

    private static IReadOnlyCollection<string> GetReferencePaths(Assembly assembly)
        => File.ReadAllLines(Path.ChangeExtension(assembly.Location, ".refs.txt"));

    private static (string inputFile, string outputFile) PrepareDirectories(Assembly assembly, IReadOnlyCollection<string> referencePaths)
    {
        var rootTestDir = Path.Combine(
            Path.GetDirectoryName(typeof(WeaverRunner).Assembly.Location)!,
            "WeavingTest",
            assembly.GetName().Name!
        );

        if (Directory.Exists(rootTestDir))
            Directory.Delete(rootTestDir, true);

        var inputDir = Path.Combine(rootTestDir, "Input");
        var outputDir = Path.Combine(rootTestDir, "Output");

        Directory.CreateDirectory(inputDir);
        Directory.CreateDirectory(outputDir);

        var assemblyPath = CopyFile(assembly.Location, inputDir);
        CopyFile(Path.ChangeExtension(assembly.Location, ".pdb"), inputDir);
        CopyFile(referencePaths.Single(i => Path.GetFileName(i) == "InlineIL.dll"), inputDir); // Necessary for PEVerify

        var outputPath = Path.Combine(outputDir, Path.GetFileName(assembly.Location));

        return (assemblyPath, outputPath);

        static string CopyFile(string fileName, string targetDir)
        {
            if (!File.Exists(fileName))
                throw new InvalidOperationException($"File not found: {fileName}");

            var dest = Path.Combine(targetDir, Path.GetFileName(fileName));
            File.Copy(fileName, dest);
            return dest;
        }
    }

    private static void WireLogging(BaseModuleWeaver weaver, TestResult testResult)
    {
        var messages = (List<LogMessage>)testResult.Messages;
        var warnings = (List<SequencePointMessage>)testResult.Warnings;
        var errors = (List<SequencePointMessage>)testResult.Errors;

        weaver.LogDebug = text => messages.Add(new(text, MessageImportanceDefaults.Debug));
        weaver.LogInfo = text => messages.Add(new(text, MessageImportanceDefaults.Info));
        weaver.LogMessage = (text, messageImportance) => messages.Add(new(text, messageImportance));
        weaver.LogWarning = text => warnings.Add(new(text, null));
        weaver.LogWarningPoint = (text, sequencePoint) => warnings.Add(new(text, sequencePoint));
        weaver.LogError = text => errors.Add(new(text, null));
        weaver.LogErrorPoint = (text, sequencePoint) => errors.Add(new(text, sequencePoint));
    }

    private class AssemblyResolver : IAssemblyResolver
    {
        private readonly Dictionary<string, AssemblyDefinition> _assemblies = new();

        public AssemblyResolver(IEnumerable<string> referencePaths)
        {
            var readerParams = new ReaderParameters(ReadingMode.Deferred)
            {
                AssemblyResolver = this
            };

            foreach (var referencePath in referencePaths)
            {
                var assembly = AssemblyDefinition.ReadAssembly(referencePath, readerParams);
                _assemblies.Add(assembly.Name.Name, assembly);
            }
        }

        public void Dispose()
        {
            foreach (var assembly in _assemblies.Values)
                assembly.Dispose();
        }

        public AssemblyDefinition? Resolve(string name)
            => _assemblies.TryGetValue(name, out var assembly) ? assembly : null;

        public AssemblyDefinition? Resolve(AssemblyNameReference name)
            => Resolve(name.Name);

        public AssemblyDefinition? Resolve(AssemblyNameReference name, ReaderParameters parameters)
            => Resolve(name);
    }
}

internal record TestRunResult(TestResult TestResult, ModuleDefinition InputModule, ModuleDefinition OutputModule);
