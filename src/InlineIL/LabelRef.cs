using System.Diagnostics.CodeAnalysis;

namespace InlineIL
{
    /// <summary>
    /// Represents a label reference, for use with <see cref="IL.Emit(System.Reflection.Emit.OpCode, LabelRef)" />.
    /// </summary>
    [SuppressMessage("ReSharper", "UnusedParameter.Local")]
    public sealed class LabelRef
    {
        /// <summary>
        /// Constructs a label reference.
        /// </summary>
        /// <param name="labelName">The label name.</param>
        public LabelRef(string labelName)
            => IL.Throw();
    }
}
