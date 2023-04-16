using System.Runtime.Serialization;

namespace PoeData.Specifications;

/// <summary>
/// Throw when current schema does not match the expected size.
/// </summary>
public sealed class SchemaMismatchException : Exception
{
    /// <inheritdoc cref="Exception()"/>
    public SchemaMismatchException()
    {
    }

    /// <inheritdoc cref="Exception(string?)"/>
    public SchemaMismatchException(string? message)
        : base(message)
    {
    }

    /// <inheritdoc cref="Exception(SerializationInfo, StreamingContext)"/>
    public SchemaMismatchException(SerializationInfo info, StreamingContext context)
        : base(info, context)
    {
    }

    /// <inheritdoc cref="Exception(string?, Exception?)"/>
    public SchemaMismatchException(string? message, Exception? innerException)
        : base(message, innerException)
    {
    }
}
