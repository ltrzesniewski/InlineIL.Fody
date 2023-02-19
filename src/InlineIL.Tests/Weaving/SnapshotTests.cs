using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DiffEngine;
using ICSharpCode.Decompiler;
using ICSharpCode.Decompiler.Metadata;
using InlineIL.Tests.Common;
using VerifyTests;
using VerifyTests.ICSharpCode.Decompiler;
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
        VerifyICSharpCodeDecompiler.Initialize();
    }

    [Theory]
    [MemberData(nameof(GetTestCases))]
    public async Task check_snapshot(TestAssemblyId assemblyId, string type)
    {
        var assemblyPath = assemblyId switch
        {
            TestAssemblyId.BaseAssembly         => AssemblyToProcessFixture.TestResult.AssemblyPath,
            TestAssemblyId.StandardAssembly     => StandardAssemblyToProcessFixture.TestResult.AssemblyPath,
            TestAssemblyId.UnverifiableAssembly => UnverifiableAssemblyToProcessFixture.TestResult.AssemblyPath,
            TestAssemblyId.InvalidAssembly      => InvalidAssemblyToProcessFixture.TestResult.AssemblyPath,
            _                                   => throw new ArgumentException()
        };

        using var file = new PEFile(assemblyPath);

        var typeDefHandle = file.Metadata
                                .TypeDefinitions
                                .Single(handle => handle.GetFullTypeName(file.Metadata).Name == type);

        await Verifier.Verify(new TypeToDisassemble(file, typeDefHandle))
                      .UseDirectory("Snapshots")
                      .UseFileName($"{assemblyId}.{type}")
                      .UniqueForAssemblyConfiguration()
                      .UniqueForTargetFrameworkAndVersion();
    }

    public static IEnumerable<object[]> GetTestCases()
    {
        var inputs = new[]
        {
            (TestAssemblyId.BaseAssembly, AssemblyToProcessFixture.ResultModule),
            (TestAssemblyId.StandardAssembly, StandardAssemblyToProcessFixture.ResultModule),
            (TestAssemblyId.UnverifiableAssembly, UnverifiableAssemblyToProcessFixture.ResultModule),
            (TestAssemblyId.InvalidAssembly, InvalidAssemblyToProcessFixture.ResultModule),
        };

        foreach (var (assemblyId, moduleDefinition) in inputs)
        {
            foreach (var typeDefinition in moduleDefinition.GetTypes())
            {
                var testCasesAttr = typeDefinition.CustomAttributes.FirstOrDefault(i => i.AttributeType.FullName == typeof(TestCasesAttribute).FullName);
                if (testCasesAttr is null)
                    continue;

                if (testCasesAttr.Properties.Any(i => i.Name == nameof(TestCasesAttribute.SnapshotTest) && i.Argument.Value is false))
                    continue;

                yield return new object[]
                {
                    assemblyId,
                    typeDefinition.Name
                };
            }
        }
    }

    public enum TestAssemblyId
    {
        BaseAssembly,
        StandardAssembly,
        UnverifiableAssembly,
        InvalidAssembly
    }
}
