using System.Text.Json.Serialization;

namespace SpecificationGenerator.SchemaJson;

public class References
{
    [JsonPropertyName("table")]
    public string Table { get; set; }

    [JsonPropertyName("column")]
    public string Column { get; set; }
}
