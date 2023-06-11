using System.Runtime.Serialization;

namespace PoeExporter.WikiExporters.Lua.Helpers;

/// <summary>
/// Exception thrown when found item is null.
/// </summary>
public class NullItemException : Exception
{
    /// <inheritdoc cref="Exception()"/>
    public NullItemException()
    {
    }

    /// <inheritdoc cref="Exception(string)"/>
    public NullItemException(string? message)
        : base(message)
    {
    }

    /// <inheritdoc cref="Exception(string, Exception)"/>
    public NullItemException(string? message, Exception? innerException)
        : base(message, innerException)
    {
    }

    /// <inheritdoc cref="Exception(SerializationInfo, StreamingContext)"/>
    protected NullItemException(SerializationInfo info, StreamingContext context)
        : base(info, context)
    {
    }
}
