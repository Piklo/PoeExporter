using Serilog;
using Serilog.Core;
using SpecificationGenerator.SchemaJson;
using System.Text;
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
        const string schemaName = "schema.min.json";
        const string SchemaDownloadUri = "https://github.com/poe-tool-dev/dat-schema/releases/download/latest/schema.min.json";
        var projectDir = Path.GetFullPath("../../../");
        var jsonLocation = Path.Combine(projectDir, schemaName);

        if (File.Exists(jsonLocation))
        {
            var existingJson = File.ReadAllText(jsonLocation);
            if (!string.IsNullOrEmpty(existingJson))
            {
                return existingJson;
            }
        }

        using var httpClient = new HttpClient();
        var newJson = await httpClient.GetStringAsync(SchemaDownloadUri);

        File.WriteAllText(jsonLocation, newJson);

        return newJson;
    }

    private static void GenerateSpecification(Schema schema)
    {
        var solutionDir = Path.GetFullPath("../../../../");
        var datFilesDir = new DirectoryInfo(Path.Combine(solutionDir, "PoeData\\Specifications\\Dat"));
        var specificationDirectory = new DirectoryInfo(Path.Combine(solutionDir, "PoeData\\Specifications"));

        var files = datFilesDir.GetFiles();
        var skippable = GetSkippableFiles(files);

        DeleteNotSkippableFiles(files, skippable);

        var skippableFileNames = new HashSet<string>();
        foreach (var file in skippable)
        {
            var name = file.Name;
            skippableFileNames.Add(name);
        }

        var skipDir = new DirectoryInfo("skipped");
        if (skipDir.Exists)
        {
            skipDir.Delete(true);
        }

        skipDir.Create();

        var datFileGenerators = new List<DatFileGenerator>();
        foreach (var table in schema.Tables)
        {
            var datFileGenerator = new DatFileGenerator(table, logger);
            datFileGenerators.Add(datFileGenerator);
            var str = datFileGenerator.Code;
            var fileName = $"{datFileGenerator.ClassName}.cs";

            var skip = skippableFileNames.Contains(fileName);
            if (skip)
            {
                File.WriteAllText(Path.Combine(skipDir.FullName, fileName), str, Encoding.UTF8);
            }
            else
            {
                File.WriteAllText(Path.Combine(datFilesDir.FullName, fileName), str, Encoding.UTF8);
            }
        }

        logger.Information("skipped files {count} - {skipped}", skippable.Count, skippable);

        var specificationGenerator = new SpecificationFileGenerator(logger, datFileGenerators);
        File.WriteAllText(
            Path.Combine(specificationDirectory.FullName, specificationGenerator.FileName),
            specificationGenerator.Code,
            Encoding.UTF8);
    }

    private static void DeleteNotSkippableFiles(FileInfo[] files, HashSet<FileInfo> skippable)
    {
        Parallel.ForEach(files, file =>
        {
            if (skippable.Contains(file))
            {
                return;
            }

            file.Delete();
        });
    }

    private static HashSet<FileInfo> GetSkippableFiles(FileInfo[] files)
    {
        var skippable = new HashSet<FileInfo>();

        Parallel.ForEach(files, file =>
        {
            var lines = File.ReadAllLines(file.FullName);

            var containsSkipComment = false;
            foreach (var line in lines)
            {
                if (line.Contains("GENERATOR_SKIP"))
                {
                    containsSkipComment = true;
                    break;
                }
            }

            if (containsSkipComment)
            {
                skippable.Add(file);
            }
        });

        return skippable;
    }
}
