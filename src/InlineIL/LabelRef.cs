using System.Diagnostics.CodeAnalysis;

namespace InlineIL
{
    [SuppressMessage("ReSharper", "UnusedParameter.Local")]
    public sealed class LabelRef
    {
        public LabelRef(string labelName)
            => IL.Throw();
    }
}
