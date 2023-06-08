using System.Text.Json.Serialization;

namespace PoeDataGenerator.SchemaJson;

/// <summary>
/// Class containing data about enumeration in schema json.
/// </summary>
internal readonly record struct Enumeration
{
    /// <summary>Gets name.</summary>
    [JsonPropertyName("name")]
    public required string Name { get; init; }

    /// <summary>Gets indexing.</summary>
    [JsonPropertyName("indexing")]
    public required int Indexing { get; init; }

    /// <summary>Gets enumerators.</summary>
    [JsonPropertyName("enumerators")]
    public required string[] Enumerators { get; init; }
}
