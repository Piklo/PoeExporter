using System.Text.Json.Serialization;

namespace SpecificationGenerator.SchemaJson;

public class Table
{
    [JsonPropertyName("name")]
    public string Name { get; set; }

    [JsonPropertyName("columns")]
    public Column[] Columns { get; set; }
}
