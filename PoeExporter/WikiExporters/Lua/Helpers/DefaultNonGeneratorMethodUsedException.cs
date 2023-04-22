using System.Runtime.Serialization;

namespace PoeExporter.WikiExporters.Lua.Helpers;

/// <summary>
/// Thrown when default methods are used when source generator was expected to generate type specific overlord method.
/// </summary>
public sealed class DefaultNonGeneratorMethodUsedException : Exception
{
    /// <inheritdoc cref="Exception()"/>
    public DefaultNonGeneratorMethodUsedException()
    {
    }

    /// <inheritdoc cref="Exception(string?)"/>
    public DefaultNonGeneratorMethodUsedException(string? message)
        : base(message)
    {
    }

    /// <inheritdoc cref="Exception(SerializationInfo, StreamingContext)"/>
    public DefaultNonGeneratorMethodUsedException(SerializationInfo info, StreamingContext context)
        : base(info, context)
    {
    }

    /// <inheritdoc cref="Exception(string?, Exception?)"/>
    public DefaultNonGeneratorMethodUsedException(string? message, Exception? innerException)
        : base(message, innerException)
    {
    }
}