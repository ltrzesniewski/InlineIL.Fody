using System;
using System.IO;
using System.Linq;
using System.Reflection.Metadata;
using System.Reflection.PortableExecutable;
using InlineIL.Fody;
using InlineIL.Tests.Support;
using Xunit;

namespace InlineIL.Tests;

public class AssemblyTests
{
    [Fact]
    public void should_not_reference_value_tuple()
    {
        // System.ValueTuple may cause issues in some configurations, avoid using it.

        using var fileStream = File.OpenRead(typeof(ModuleWeaver).Assembly.Location);
        using var peReader = new PEReader(fileStream);
        var metadataReader = peReader.GetMetadataReader();

        foreach (var typeRefHandle in metadataReader.TypeReferences)
        {
            var typeRef = metadataReader.GetTypeReference(typeRefHandle);

            var typeNamespace = metadataReader.GetString(typeRef.Namespace);
            if (typeNamespace != typeof(ValueTuple).Namespace)
                continue;

            var typeName = metadataReader.GetString(typeRef.Name);
            typeName.ShouldNotContain(nameof(ValueTuple));
        }
    }

    [Fact]
    public void should_not_export_unexpected_namespaces_in_library()
    {
        typeof(IL).Assembly
                  .GetExportedTypes()
                  .Select(i => i.Namespace)
                  .Distinct(StringComparer.Ordinal)
                  .OrderBy(i => i, StringComparer.Ordinal)
                  .ToList()
                  .ShouldEqual([
                      "InlineIL"
                  ]);
    }

    [Fact]
    public void should_not_export_unexpected_namespaces_in_weaver()
    {
        typeof(ModuleWeaver).Assembly
                            .GetExportedTypes()
                            .Select(i => i.Namespace)
                            .Distinct(StringComparer.Ordinal)
                            .OrderBy(i => i, StringComparer.Ordinal)
                            .ToList()
                            .ShouldEqual([
                                "InlineIL.Fody"
                            ]);
    }
}
