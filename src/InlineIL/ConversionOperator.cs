namespace InlineIL;

/// <summary>
/// Conversion operator.
/// </summary>
#if INLINEIL_LIBRARY
public
#else
internal
#endif
    enum ConversionOperator
{
    /// <summary>
    /// <c>op_Implicit</c>
    /// </summary>
    Implicit,

    /// <summary>
    /// <c>op_Explicit</c>
    /// </summary>
    Explicit
}
