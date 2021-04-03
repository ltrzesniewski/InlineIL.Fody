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

            /// <summary>
            /// <c>no.</c> - Indicates that the subsequent instruction need not perform the specified fault check when it is executed. Currently not implemented in the CLR.
            /// <para>0x01 = <c>typecheck</c> (<c>castclass</c>, <c>unbox</c>, <c>ldelema</c>, <c>stelem.*</c>)</para>
            /// <para>0x02 = <c>rangecheck</c> (<c>ldelem.*</c>, <c>ldelema</c>, <c>stelem.*</c>)</para>
            /// <para>0x04 = <c>nullcheck</c> (<c>ldfld</c>, <c>stfld</c>, <c>callvirt</c>, <c>ldvirtftn</c>, <c>ldelem.*</c>, <c>stelem.*</c>, <c>ldelema</c>)</para>
            /// <para>This is a prefix instruction.</para>
            /// </summary>
            /// <param name="operand">The operand.</param>
            public static void No(byte operand)
                => IL.Throw();
        }
    }
}
