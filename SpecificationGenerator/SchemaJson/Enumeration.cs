using System.Text.Json.Serialization;

namespace SpecificationGenerator.SchemaJson;

public class Enumeration
{
    [JsonPropertyName("name")]
    public string Name { get; set; }

    [JsonPropertyName("indexing")]
    public int Indexing { get; set; }

    [JsonPropertyName("enumerators")]
    public string[] Enumerators { get; set; }
}
