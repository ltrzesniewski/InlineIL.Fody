using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DiffEngine;
using ICSharpCode.Decompiler;
using ICSharpCode.Decompiler.Metadata;
using InlineIL.Tests.Common;
using Mono.Cecil.Rocks;
using VerifyTests;
using VerifyXunit;
using Xunit;

namespace InlineIL.Tests.Weaving;

[UsesVerify]
public class SnapshotTests
{
    static SnapshotTests()
    {
        DiffRunner.Disabled = true;
        VerifyDiffPlex.Initialize();
        VerifyICSharpCodeDecompiler.Enable();
    }

    [Theory]
    [MemberData(nameof(GetTestCases))]
    public async Task check_snapshot(TestAssemblyId assemblyId, string type, string method)
    {
        var assemblyPath = assemblyId switch
        {
            TestAssemblyId.AssemblyToProcess             => AssemblyToProcessFixture.TestResult.AssemblyPath,
            TestAssemblyId.StandardAssemblyToProcess     => StandardAssemblyToProcessFixture.TestResult.AssemblyPath,
            TestAssemblyId.UnverifiableAssemblyToProcess => UnverifiableAssemblyToProcessFixture.TestResult.AssemblyPath,
            _                                            => throw new ArgumentException()
        };

        using var file = new PEFile(assemblyPath);

        var typeDefHandle = file.Metadata
                                .TypeDefinitions
                                .Single(handle => handle.GetFullTypeName(file.Metadata).Name == type);

        var typeDef = file.Metadata.GetTypeDefinition(typeDefHandle);

        var methodDefHandle = typeDef.GetMethods().Single(handle =>
        {
            var methodDef = file.Metadata.GetMethodDefinition(handle);
            return file.Metadata.GetString(methodDef.Name) == method;
        });

        await Verifier.Verify(new MethodToDisassemble(file, methodDefHandle))
                      .UseDirectory("Snapshots")
                      .UseFileName($"{assemblyId}.{type}.{method}")
                      .UniqueForAssemblyConfiguration()
                      .UniqueForTargetFrameworkAndVersion();
    }

    public static IEnumerable<object[]> GetTestCases()
    {
        var inputs = new[]
        {
            (TestAssemblyId.AssemblyToProcess, AssemblyToProcessFixture.ResultModule),
            (TestAssemblyId.StandardAssemblyToProcess, StandardAssemblyToProcessFixture.ResultModule),
            (TestAssemblyId.UnverifiableAssemblyToProcess, UnverifiableAssemblyToProcessFixture.ResultModule),
        };

        foreach (var (assemblyId, moduleDefinition) in inputs)
        {
            foreach (var typeDefinition in moduleDefinition.GetTypes())
            {
                if (typeDefinition.CustomAttributes.Any(i => i.AttributeType.FullName == typeof(TestCasesAttribute).FullName))
                {
                    foreach (var methodDefinition in typeDefinition.GetMethods())
                    {
                        if (methodDefinition.CustomAttributes.Any(i => i.AttributeType.FullName == typeof(SnapshotTest).FullName))
                        {
                            yield return new object[]
                            {
                                assemblyId,
                                typeDefinition.Name,
                                methodDefinition.Name
                            };
                        }
                    }
                }
            }
        }
    }

    public enum TestAssemblyId
    {
        AssemblyToProcess,
        StandardAssemblyToProcess,
        UnverifiableAssemblyToProcess
    }
}
