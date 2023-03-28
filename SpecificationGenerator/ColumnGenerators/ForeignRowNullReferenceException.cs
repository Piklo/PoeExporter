using System.Runtime.Serialization;

namespace SpecificationGenerator.ColumnGenerators;

/// <summary>
/// Throw when <see cref="ForeignrowNonArrayColumn"/> is being parsed with references set to null.
/// </summary>
[System.Diagnostics.CodeAnalysis.SuppressMessage(
    "StyleCop.CSharp.DocumentationRules",
    "SA1600:Elements should be documented",
    Justification = "those are exception constructors bruh")]
public class ForeignRowNullReferenceException : Exception
{
    public ForeignRowNullReferenceException()
    {
    }

    public ForeignRowNullReferenceException(string? message)
        : base(message)
    {
    }

    public ForeignRowNullReferenceException(string? message, Exception? innerException)
        : base(message, innerException)
    {
    }

    protected ForeignRowNullReferenceException(SerializationInfo info, StreamingContext context)
        : base(info, context)
    {
    }
}