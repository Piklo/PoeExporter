using System.Collections.Immutable;

namespace PoeDataGenerator.GeneratorHelpers;

internal readonly record struct ParsedSchemaWithReferenceData
{
    /// <summary>Gets Table.</summary>
    public required ParsedSchemaTable Table { get; init; }

    /// <summary>Gets which columns in a table are referenced by other tables..</summary>
    public required ImmutableHashSet<string> ReferencedColumns { get; init; }
}
