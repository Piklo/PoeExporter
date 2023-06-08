using System.Text.Json.Serialization;

namespace PoeDataGenerator.SchemaJson;

/// <summary>
/// Class containing data about tables in schema json.
/// </summary>
internal readonly record struct Table
{
    /// <summary>Gets name.</summary>
    [JsonPropertyName("name")]
    public required string Name { get; init; }

    /// <summary>Gets columns.</summary>
    [JsonPropertyName("columns")]
    public required Column[] Columns { get; init; }
}
