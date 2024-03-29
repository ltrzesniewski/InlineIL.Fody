using System.Diagnostics.CodeAnalysis;

[assembly: SuppressMessage("Microsoft.Design", "CA1014")] // Mark assemblies with CLSCompliantAttribute
[assembly: SuppressMessage("Microsoft.Design", "CA1034")] // Nested types should not be visible
[assembly: SuppressMessage("Microsoft.Design", "CA1028")] // Enum storage should be Int32
[assembly: SuppressMessage("Microsoft.Design", "CA1065")] // Do not raise exceptions in unexpected locations
[assembly: SuppressMessage("Usage", "CA1801")] // Review unused parameters
[assembly: SuppressMessage("Performance", "CA1822")] // Mark members as static
[assembly: SuppressMessage("Usage", "CA2225")] // Operator overloads have named alternates
[assembly: SuppressMessage("Style", "IDE0060:Remove unused parameter")]
[assembly: SuppressMessage("Style", "IDE0021:Use block body for constructors")]
[assembly: SuppressMessage("CodeQuality", "IDE0079:Remove unnecessary suppression", Justification = "VS doesn't know about ReSharper.")]
