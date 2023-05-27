using System.Text.Json.Serialization;

namespace PoeDataGenerator.SchemaJson;

/// <summary>
/// Class containing data about referenced table in schema json columns.
/// </summary>
internal sealed class References
{
    /// <summary>Gets table name.</summary>
    [JsonPropertyName("table")]
    public required string Table { get; init; }

    /// <summary>Gets column on which the reference is joined.</summary>
    [JsonPropertyName("column")]
    public string? Column { get; init; }
}
