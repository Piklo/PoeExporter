using SpecificationGenerator.SchemaJson;
using System.Text.Json;

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
