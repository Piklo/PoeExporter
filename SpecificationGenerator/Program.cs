﻿using Serilog;
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
        var specificationFilesDir = GetSpecificationFilesDirectory();
        var specificationDirectory = specificationFilesDir.Parent;
        if (specificationDirectory == null)
        {
            throw new Exception(nameof(specificationDirectory));
        }

        var files = specificationFilesDir.GetFiles();
        var skippable = GetSkippableFiles(files);

        DeleteNotSkippableFiles(files, skippable);

        var skippableFileNames = new HashSet<string>();
        foreach (var file in skippable)
        {
            var name = file.Name;
            skippableFileNames.Add(name);
        }

        var outputDirectory = Directory.CreateDirectory("Output");
        var skipDir = outputDirectory.CreateSubdirectory("skipped");

        var specificationFiles = new List<SpecificationFilesGenerator>();
        foreach (var table in schema.Tables)
        {
            var specificationFile = new SpecificationFilesGenerator(table, logger);
            specificationFiles.Add(specificationFile);
            var str = specificationFile.GetFileString();
            var fileName = $"{specificationFile.ClassName}.cs";

            var skip = skippableFileNames.Contains(fileName);
            if (skip)
            {
                File.WriteAllText(Path.Combine(skipDir.FullName, fileName), str);
            }
            else
            {
                File.WriteAllText(Path.Combine(outputDirectory.FullName, fileName), str, Encoding.UTF8);
                File.WriteAllText(Path.Combine(specificationFilesDir.FullName, fileName), str, Encoding.UTF8);
            }
        }

        var specificationGenerator = new SpecificationFileGenerator(logger, specificationFiles);
        var specificationStr = specificationGenerator.Generate();
        var specificationName = "Specification.cs";
        File.WriteAllText(Path.Combine(outputDirectory.FullName, specificationName), specificationStr, Encoding.UTF8);
        File.WriteAllText(Path.Combine(specificationDirectory.FullName, specificationName), specificationStr, Encoding.UTF8);
    }

    private static void DeleteNotSkippableFiles(FileInfo[] files, HashSet<FileInfo> skippable)
    {
        foreach (var file in files)
        {
            if (skippable.Contains(file))
            {
                continue;
            }

            file.Delete();
        }
    }

    private static HashSet<FileInfo> GetSkippableFiles(FileInfo[] files)
    {
        var skippable = new HashSet<FileInfo>();

        foreach (var file in files)
        {
            var lines = File.ReadAllLines(file.FullName);

            var containsSkipComment = false;
            foreach (var line in lines)
            {
                if (line.Contains("// GENERATOR_SKIP"))
                {
                    containsSkipComment = true;
                    break;
                }
            }

            if (containsSkipComment)
            {
                skippable.Add(file);
            }
        }

        return skippable;
    }

    private static DirectoryInfo GetSpecificationFilesDirectory()
    {
        var solution = TryGetSolutionDirectoryInfo();
        var path = Path.Combine(solution.FullName, "PoeData\\Specifications\\SpecificationFiles");

        var dir = new DirectoryInfo(path);

        return dir;
    }

    private static DirectoryInfo TryGetSolutionDirectoryInfo()
    {
        var directory = new DirectoryInfo(Directory.GetCurrentDirectory());

        while (directory != null && directory.GetFiles("*.sln").Length == 0)
        {
            directory = directory.Parent;
        }

        if (directory == null)
        {
            throw new Exception("project not found");
        }

        return directory;
    }
}
