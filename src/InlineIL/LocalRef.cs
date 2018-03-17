using System.Diagnostics.CodeAnalysis;

namespace InlineIL
{
    [SuppressMessage("ReSharper", "UnusedParameter.Local")]
    public sealed class LocalRef
    {
        public LocalRef(string localName)
            => IL.Throw();
    }
}
