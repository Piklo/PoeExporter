using System.Text.Json.Serialization;

namespace SpecificationGenerator.SchemaJson;

public class Schema
{
    [JsonPropertyName("version")]
    public int Version { get; set; }

    [JsonPropertyName("createdAt")]
    public int CreatedAt { get; set; }

    [JsonPropertyName("tables")]
    public Table[] Tables { get; set; }

    [JsonPropertyName("enumerations")]
    public Enumeration[] Enumerations { get; set; }
}
