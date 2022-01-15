using System;
using System.IO;

#pragma warning disable 618

namespace InlineIL.Tests.Weaving;

internal static class FixtureHelper
{
    public static string IsolateAssembly<T>()
    {
        var assembly = typeof(T).Assembly;
        var assemblyPath = assembly.Location;
        var assemblyDir = Path.GetDirectoryName(assemblyPath)!;
        var rootTestDir = Path.Combine(assemblyDir, "WeavingTest");
        var asmTestDir = Path.Combine(rootTestDir, Path.GetFileNameWithoutExtension(assemblyPath));

        EmptyDirectory(asmTestDir);
        Directory.CreateDirectory(asmTestDir);

        var destFile = CopyFile(assemblyPath, asmTestDir);
        CopyFile(Path.ChangeExtension(assemblyPath, ".pdb"), asmTestDir);
        CopyFile(Path.Combine(assemblyDir, "InlineIL.dll"), asmTestDir);

        return destFile;
    }

    private static string CopyFile(string fileName, string targetDir)
    {
        if (!File.Exists(fileName))
            throw new InvalidOperationException($"File not found: {fileName}");

        var dest = Path.Combine(targetDir, Path.GetFileName(fileName));
        File.Copy(fileName, dest);
        return dest;
    }

    private static void EmptyDirectory(string path)
    {
        var directoryInfo = new DirectoryInfo(path);
        if (!directoryInfo.Exists)
            return;

        foreach (var file in directoryInfo.GetFiles())
            file.Delete();

        foreach (var dir in directoryInfo.GetDirectories())
            dir.Delete(true);
    }
}
