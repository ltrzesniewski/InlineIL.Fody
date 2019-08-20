using System.Diagnostics.CodeAnalysis;
using System.IO;

#pragma warning disable 618

namespace InlineIL.Tests.Weaving
{
    [SuppressMessage("ReSharper", "AssignNullToNotNullAttribute")]
    internal static class FixtureHelper
    {
        public static string IsolateAssembly(string assemblyFileName)
        {
            var assemblyDir = Path.GetDirectoryName(typeof(FixtureHelper).Assembly.Location);
            var assemblyPath = Path.Combine(assemblyDir, assemblyFileName);
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
                return null;

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
}
