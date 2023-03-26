using Serilog;
using Serilog.Core;
using SpecificationGenerator.SchemaJson;
using System.Text.Json;

namespace SpecificationGenerator;

#pragma warning disable SA1600 // Elements should be documented
internal sealed class Program
#pragma warning restore SA1600 // Elements should be documented
{
    private static ILogger logger = default!;

    private static async Task Main()
    {
        var jsonString = await GetSchemaStringAsync();
        var schema = JsonSerializer.Deserialize<Schema>(jsonString);

        var levelSwitch = new LoggingLevelSwitch
        {
            MinimumLevel = Serilog.Events.LogEventLevel.Verbose,
        };
        logger = new LoggerConfiguration()
           .MinimumLevel.ControlledBy(levelSwitch)
           .WriteTo.Console()
           .CreateLogger();

        if (schema is not null)
        {
            GetPossibleColumnTypes(schema);
            GenerateSpecification(schema);
        }
        else
        {
            logger.Error("schema is null");
        }
    }

    private static HashSet<string> GetPossibleColumnTypes(Schema schema)
    {
        var set = new HashSet<string>();

        foreach (var table in schema.Tables)
        {
            foreach (var column in table.Columns)
            {
                set.Add(column.Type);
            }
        }

        return set;
    }

    private static async Task<string> GetSchemaStringAsync()
    {
        var jsonString = File.ReadAllText("schema.min.json");
        if (!string.IsNullOrEmpty(jsonString))
        {
            return jsonString;
        }

        using var httpClient = new HttpClient();
        jsonString = await httpClient.GetStringAsync("https://github.com/poe-tool-dev/dat-schema/releases/download/latest/schema.min.json");

        return jsonString;
    }

    private static void GenerateSpecification(Schema schema)
    {
        foreach (var table in schema.Tables)
        {
            //if (table.Name != "AbyssObjects")
            //{
            //    continue;
            //}

            var specificationFile = new SpecificationFileGenerator(table, logger);
            var str = specificationFile.GetFileString();
        }
    }
}
