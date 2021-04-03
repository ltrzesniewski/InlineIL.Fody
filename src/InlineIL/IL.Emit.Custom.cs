using System.Diagnostics.CodeAnalysis;

namespace InlineIL
{
    partial class IL
    {
        [SuppressMessage("ReSharper", "UnusedParameter.Global")]
        partial class Emit
        {
            /// <summary>
            /// <c>no.</c> - Indicates that the subsequent instruction need not perform the specified fault check when it is executed. Currently not implemented in the CLR.
            /// <para>This is a prefix instruction.</para>
            /// </summary>
            /// <param name="operand">The operand.</param>
            public static void No(NoArg operand)
                => IL.Throw();
        }
    }
}
