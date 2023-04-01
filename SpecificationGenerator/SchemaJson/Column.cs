using System.Text.Json.Serialization;

namespace SpecificationGenerator.SchemaJson;

public class Column
{
    [JsonPropertyName("name")]
    public string? Name { get; init; }

    [JsonPropertyName("description")]
    public string? Description { get; init; }

    [JsonPropertyName("array")]
    public bool Array { get; init; }

    [JsonPropertyName("type")]
    public required string Type { get; init; }

    [JsonPropertyName("unique")]
    public bool Unique { get; init; }

    [JsonPropertyName("localized")]
    public bool Localized { get; init; }

    [JsonPropertyName("references")]
    public References? References { get; init; }

    [JsonPropertyName("until")]
    public object? Until { get; init; }

    [JsonPropertyName("file")]
    public string? File { get; init; }

    [JsonPropertyName("files")]
    public string[]? Files { get; init; }
}
