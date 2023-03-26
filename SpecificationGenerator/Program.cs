using System.Text.Json;
using System.Text.Json.Serialization;

namespace SpecificationGenerator;

#pragma warning disable SA1600 // Elements should be documented
internal sealed class Program
#pragma warning restore SA1600 // Elements should be documented
{
    private static async Task Main()
    {
        using var httpClient = new HttpClient();
        var jsonString = await httpClient.GetStringAsync("https://github.com/poe-tool-dev/dat-schema/releases/download/latest/schema.min.json");
        var schema = JsonSerializer.Deserialize<Schema>(jsonString);
    }
}

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

public class Table
{
    [JsonPropertyName("name")]
    public string Name { get; set; }

    [JsonPropertyName("columns")]
    public Column[] Columns { get; set; }
}

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

public class References
{
    [JsonPropertyName("table")]
    public string Table { get; set; }

    [JsonPropertyName("column")]
    public string Column { get; set; }
}

public class Enumeration
{
    [JsonPropertyName("name")]
    public string Name { get; set; }

    [JsonPropertyName("indexing")]
    public int Indexing { get; set; }

    [JsonPropertyName("enumerators")]
    public string[] Enumerators { get; set; }
}
