using System.Text.Json.Serialization;

namespace SpecificationGenerator.SchemaJson;

/// <summary>
/// Class containing data about all of schema json.
/// </summary>
public class Schema
{
    /// <summary>Gets version.</summary>
    [JsonPropertyName("version")]
    public required int Version { get; init; }

    /// <summary>Gets creation timestamp.</summary>
    [JsonPropertyName("createdAt")]
    public required int CreatedAt { get; init; }

    /// <summary>Gets tables.</summary>
    [JsonPropertyName("tables")]
    public required Table[] Tables { get; init; }

    /// <summary>Gets enumerations.</summary>
    [JsonPropertyName("enumerations")]
    public required Enumeration[] Enumerations { get; init; }
}
