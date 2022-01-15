using System;

namespace InlineIL;

/// <summary>
/// Argument for the <c>no</c> opcode. Multiple values can be OR-ed together.
/// </summary>
[Flags]
public enum NoArg : byte
{
    /// <summary>
    /// Optionally skip any type checks normally performed as part of the execution of the subsequent instruction.
    /// </summary>
    /// <remarks>
    /// <para><see cref="InvalidCastException"/> can optionally still be thrown if the check would fail.</para>
    /// <para>Valid for: <c>castclass</c>, <c>unbox</c>, <c>ldelema</c>, <c>stelem.*</c></para>
    /// </remarks>
    TypeCheck = 0x01,

    /// <summary>
    /// Optionally skip any array range checks normally performed as part of the execution of the subsequent instruction.
    /// </summary>
    /// <remarks>
    /// <para><see cref="IndexOutOfRangeException"/> can optionally still be thrown if the check would fail.</para>
    /// <para>Valid for: <c>ldelem.*</c>, <c>ldelema</c>, <c>stelem.*</c></para>
    /// </remarks>
    RangeCheck = 0x02,

    /// <summary>
    /// Optionally skip any null-reference checks normally performed as part of the execution of the subsequent instruction.
    /// </summary>
    /// <remarks>
    /// <para><see cref="NullReferenceException"/> can optionally still be thrown if the check would fail.</para>
    /// <para>Valid for: <c>ldfld</c>, <c>stfld</c>, <c>callvirt</c>, <c>ldvirtftn</c>, <c>ldelem.*</c>, <c>stelem.*</c>, <c>ldelema</c></para>
    /// </remarks>
    NullCheck = 0x04
}
