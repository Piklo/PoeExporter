using System;
using System.Runtime.Serialization;

/// <summary>
/// Thrown when the source generator failed to find the namespace.
/// </summary>
public sealed class NamespaceNotFoundException : Exception
{
    /// <inheritdoc cref="Exception()"/>
    public NamespaceNotFoundException()
    {
    }

    /// <inheritdoc cref="Exception(string)"/>
    public NamespaceNotFoundException(string message)
        : base(message)
    {
    }

    /// <inheritdoc cref="Exception(SerializationInfo, StreamingContext)"/>
    public NamespaceNotFoundException(SerializationInfo info, StreamingContext context)
        : base(info, context)
    {
    }

    /// <inheritdoc cref="Exception(string, Exception)"/>
    public NamespaceNotFoundException(string message, Exception innerException)
        : base(message, innerException)
    {
    }
}
