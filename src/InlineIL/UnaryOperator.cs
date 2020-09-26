namespace InlineIL
{
    /// <summary>
    /// Unary operator
    /// </summary>
    public enum UnaryOperator
    {
        /// <summary>
        /// <c>op_Decrement</c>
        /// </summary>
        Decrement,

        /// <summary>
        /// <c>op_Increment</c>
        /// </summary>
        Increment,

        /// <summary>
        /// <c>op_UnaryNegation</c>
        /// </summary>
        UnaryNegation,

        /// <summary>
        /// <c>op_UnaryPlus</c>
        /// </summary>
        UnaryPlus,

        /// <summary>
        /// <c>op_LogicalNot</c>
        /// </summary>
        LogicalNot,

        /// <summary>
        /// <c>op_True</c>
        /// </summary>
        True,

        /// <summary>
        /// <c>op_False</c>
        /// </summary>
        False,

        /// <summary>
        /// <c>op_AddressOf</c>
        /// </summary>
        AddressOf,

        /// <summary>
        /// <c>op_OnesComplement</c>
        /// </summary>
        OnesComplement,

        /// <summary>
        /// <c>op_PointerDereference</c>
        /// </summary>
        PointerDereference
    }
}
