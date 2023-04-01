using System.Text.Json.Serialization;

namespace SpecificationGenerator.SchemaJson;

public class Enumeration
{
    [JsonPropertyName("name")]
    public required string Name { get; init; }

    [JsonPropertyName("indexing")]
    public required int Indexing { get; init; }

    [JsonPropertyName("enumerators")]
    public required string[] Enumerators { get; init; }
}
