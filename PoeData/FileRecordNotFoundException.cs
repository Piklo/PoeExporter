using System.Runtime.Serialization;

namespace PoeData;

/// <summary>
/// Throw when <see cref="Records.FileRecord"/> was not found.
/// </summary>
public sealed class FileRecordNotFoundException : Exception
{

    /// <inheritdoc cref="Exception()"/>
    public FileRecordNotFoundException()
    {
    }

    /// <inheritdoc cref="Exception(string)"/>
    public FileRecordNotFoundException(string? message)
        : base(message)
    {
    }

    /// <inheritdoc cref="Exception(SerializationInfo, StreamingContext)"/>
    public FileRecordNotFoundException(SerializationInfo info, StreamingContext context)
        : base(info, context)
    {
    }

    /// <inheritdoc cref="Exception(string?, Exception?)"/>
    public FileRecordNotFoundException(string? message, Exception? innerException)
        : base(message, innerException)
    {
    }
}