using Serilog;
using SpecificationGenerator.SchemaJson;
using System.Text;
using System.Text.Json;

namespace SpecificationGenerator;

/// <summary>
/// Class generating Specification file and Dat files.
/// </summary>
internal sealed class Generator
{
    private readonly ILogger logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="Generator"/> class.
    /// </summary>
    /// <param name="logger">logger.</param>
    public Generator(ILogger logger)
    {
        this.logger = logger;
    }

    /// <summary>
    /// Runs generator.
    /// </summary>
    /// <returns>A task representing whether generator finished running.</returns>
    public async Task RunAsync()
    {
        var jsonString = await GetSchemaStringAsync();
        var schema = JsonSerializer.Deserialize<Schema>(jsonString);

        if (schema is not null)
        {
            GenerateSpecification(schema);
        }
        else
        {
            logger.Error("schema is null");
        }
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

    private void GenerateSpecification(Schema schema)
    {
        var solutionDir = Path.GetFullPath("../../../../");
        var datFilesDir = new DirectoryInfo(Path.Combine(solutionDir, "PoeData\\Specifications\\DatFiles"));
        var repositoriesDir = new DirectoryInfo(Path.Combine(solutionDir, "PoeData\\Specifications\\Repositories"));
        var specificationDirectory = new DirectoryInfo(Path.Combine(solutionDir, "PoeData\\Specifications"));

        var datFileFileInfos = datFilesDir.GetFiles();
        var skippableDatFiles = GetSkippableFiles(datFileFileInfos);
        DeleteNotSkippableFiles(datFileFileInfos, skippableDatFiles);

        var repositoryFileInfos = repositoriesDir.GetFiles();
        var skippableRepositories = GetSkippableFiles(repositoryFileInfos);
        DeleteNotSkippableFiles(repositoryFileInfos, skippableRepositories);

        var skippableDatFileNames = new HashSet<string>();
        foreach (var file in skippableDatFiles)
        {
            var name = file.Name;
            skippableDatFileNames.Add(name);
        }

        var skippableRepositoryFileNames = new HashSet<string>();
        foreach (var file in skippableRepositories)
        {
            var name = file.Name;
            skippableRepositoryFileNames.Add(name);
        }

        var skipDir = new DirectoryInfo("skipped");
        if (skipDir.Exists)
        {
            skipDir.Delete(true);
        }

        skipDir.Create();

        var datFileGenerators = new List<DatFileGenerator>();
        var repositoryGenerators = new List<RepositoryGenerator>();
        foreach (var table in schema.Tables)
        {
            var parsedTable = new ParsedSchemaTable(logger, table);

            var datFileGenerator = new DatFileGenerator(logger, parsedTable);
            datFileGenerators.Add(datFileGenerator);

            var repositoryGenerator = new RepositoryGenerator(logger, parsedTable);
            repositoryGenerators.Add(repositoryGenerator);

            if (skippableDatFileNames.Contains(datFileGenerator.FileName))
            {
                File.WriteAllText(Path.Combine(skipDir.FullName, datFileGenerator.FileName), datFileGenerator.Code, Encoding.UTF8);
            }
            else
            {
                File.WriteAllText(Path.Combine(datFilesDir.FullName, datFileGenerator.FileName), datFileGenerator.Code, Encoding.UTF8);
            }

            if (skippableRepositoryFileNames.Contains(repositoryGenerator.FileName))
            {
                File.WriteAllText(Path.Combine(skipDir.FullName, repositoryGenerator.FileName), repositoryGenerator.Code, Encoding.UTF8);
            }
            else
            {
                File.WriteAllText(Path.Combine(datFilesDir.FullName, repositoryGenerator.FileName), repositoryGenerator.Code, Encoding.UTF8);
            }
        }

        logger.Information("skipped files {count} - {skipped}", skippableDatFiles.Count, skippableDatFiles);

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
