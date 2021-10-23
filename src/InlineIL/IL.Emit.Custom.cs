using System.Diagnostics.CodeAnalysis;

namespace InlineIL
{
    partial class IL
    {
        [SuppressMessage("ReSharper", "UnusedParameter.Global")]
        [SuppressMessage("Naming", "CA1724:Type names should not match namespaces")]
        partial class Emit
        {
            /// <summary>
            /// <c>no.</c> - Indicates that the subsequent instruction need not perform the specified fault check when it is executed.
            /// </summary>
            /// <remarks>
            /// <para>This is a prefix instruction. Currently not implemented in the CLR.</para>
            /// </remarks>
            /// <param name="operand">The operand.</param>
            public static void No(NoArg operand)
                => IL.Throw();

            /// <summary>
            /// <c>no.</c> - Indicates that the subsequent instruction need not perform the specified fault check when it is executed.
            /// </summary>
            /// <remarks>
            /// <para>This is a prefix instruction. Currently not implemented in the CLR.</para>
            /// <para>
            /// The following <paramref name="operand"/> values are defined for the following instructions:
            /// <list type="table">
            /// <item>
            ///     <term>0x01: typecheck</term>
            ///     <description><c>castclass</c>, <c>unbox</c>, <c>ldelema</c>, <c>stelem.*</c></description>
            /// </item>
            /// <item>
            ///     <term>0x02: rangecheck</term>
            ///     <description><c>ldelem.*</c>, <c>ldelema</c>, <c>stelem.*</c></description>
            /// </item>
            /// <item>
            ///     <term>0x04: nullcheck</term>
            ///     <description><c>ldfld</c>, <c>stfld</c>, <c>callvirt</c>, <c>ldvirtftn</c>, <c>ldelem.*</c>, <c>stelem.*</c>, <c>ldelema</c></description>
            /// </item>
            /// </list>
            /// </para>
            /// </remarks>
            /// <param name="operand">The operand.</param>
            public static void No(byte operand)
                => IL.Throw();
        }
    }
}
