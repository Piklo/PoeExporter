using System.Runtime.Serialization;

namespace PoeData.Ggpk;

/// <summary>
/// Thrown when <see cref="GgpkLoader"/> fails to find a path.
/// </summary>
public sealed class PathNotFoundException : Exception
{
    /// <inheritdoc cref="Exception()"/>
    public PathNotFoundException()
    {
    }

    /// <inheritdoc cref="Exception(string?)"/>
    public PathNotFoundException(string? message)
        : base(message)
    {
    }

    /// <inheritdoc cref="Exception(SerializationInfo, StreamingContext)"/>
    public PathNotFoundException(SerializationInfo info, StreamingContext context)
        : base(info, context)
    {
    }

    /// <inheritdoc cref="Exception(string?, Exception?)"/>
    public PathNotFoundException(string? message, Exception? innerException)
        : base(message, innerException)
    {
    }
}