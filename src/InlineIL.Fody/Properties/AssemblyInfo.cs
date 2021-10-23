using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("InlineIL.Tests")]

[assembly: SuppressMessage("Microsoft.Design", "CA1014")] // Mark assemblies with CLSCompliantAttribute
[assembly: SuppressMessage("CodeQuality", "IDE0079:Remove unnecessary suppression", Justification = "VS doesn't know about ReSharper.")]
[assembly: SuppressMessage("Style", "IDE0066:Convert switch statement to expression", Justification = "That doesn't always improve the code.")]
