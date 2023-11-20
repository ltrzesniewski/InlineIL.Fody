namespace InlineIL;

/// <summary>
/// Conversion direction.
/// </summary>
#if INLINEIL_LIBRARY
public
#else
internal
#endif
    enum ConversionDirection
{
    /// <summary>
    /// Convert from the other type.
    /// </summary>
    From,

    /// <summary>
    /// Convert to the other type.
    /// </summary>
    To
}
