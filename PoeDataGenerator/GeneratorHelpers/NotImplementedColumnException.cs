using System.Runtime.Serialization;

namespace PoeDataGenerator.GeneratorHelpers;

/// <summary>
/// Exception thrown when not implemented column is being parsed.
/// </summary>
[System.Diagnostics.CodeAnalysis.SuppressMessage(
    "StyleCop.CSharp.DocumentationRules",
    "SA1600:Elements should be documented",
    Justification = "those are exception constructors bruh")]
public class NotImplementedColumnException : NotImplementedException
{
    public NotImplementedColumnException()
    {
    }

    public NotImplementedColumnException(string? message)
        : base(message)
    {
    }

    public NotImplementedColumnException(string? message, Exception? inner)
        : base(message, inner)
    {
    }

    protected NotImplementedColumnException(SerializationInfo info, StreamingContext context)
        : base(info, context)
    {
    }
}