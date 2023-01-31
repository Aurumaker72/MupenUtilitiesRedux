namespace MupenUtilitiesRedux.Models.Options;

/// <summary>
///     A <see langword="struct" /> which contains options for the <see cref="Movie" /> deserialization
/// </summary>
public struct MovieDeserializationOptions
{
    public MovieDeserializationOptions()
    {
    }

    /// <summary>
    ///     Whether strings' contents get destroyed after the first <c>0x00</c>
    ///     <para>
    ///         This is intended to fix issues regarding various text renderers and loggers reading past the first null
    ///         terminator
    ///     </para>
    /// </summary>
    /// <remarks>
    ///     This might cause the serialized output to not perfectly match the original movie if <c>EmptyValue != 0</c>
    /// </remarks>
    public bool SimplifyNullTerminators { internal get; init; } = false;
}