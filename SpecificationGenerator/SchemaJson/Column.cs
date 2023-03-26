using System.Text.Json.Serialization;

namespace SpecificationGenerator.SchemaJson;

public class Column
{
    [JsonPropertyName("name")]
    public string Name { get; set; }

    [JsonPropertyName("description")]
    public string Description { get; set; }

    [JsonPropertyName("array")]
    public bool Array { get; set; }

    [JsonPropertyName("type")]
    public string Type { get; set; }

    [JsonPropertyName("unique")]
    public bool Unique { get; set; }

    [JsonPropertyName("localized")]
    public bool Localized { get; set; }

    [JsonPropertyName("references")]
    public References References { get; set; }

    [JsonPropertyName("until")]
    public object Until { get; set; }

    [JsonPropertyName("file")]
    public string File { get; set; }

    [JsonPropertyName("files")]
    public string[] Files { get; set; }
}
