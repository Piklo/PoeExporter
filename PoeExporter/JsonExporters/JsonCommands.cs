using Serilog;
using System.CommandLine;

namespace PoeExporter.JsonExporters;

/// <summary>
/// Class which adds json exporter commands to a <see cref="Command"/>.
/// </summary>
internal static class JsonCommands
{
    /// <summary>
    /// Adds json commands.
    /// </summary>
    /// <param name="rootCommand">command to which the method adds subcommands.</param>
    /// <param name="specificationWrapper">specification wrapper.</param>
    /// <param name="config">config.</param>
    /// <param name="logger">logger.</param>
    public static void AddCommands(Command rootCommand, SpecificationWrapper specificationWrapper, IConfig config, ILogger logger)
    {
        var jsonCommand = new Command("json", "exports data to json");
        rootCommand.Add(jsonCommand);

        AddExportAllDatFiles(jsonCommand, specificationWrapper, config, logger);

        AddExportListedDatFiles(jsonCommand, specificationWrapper, config, logger);
    }

    private static void AddExportAllDatFiles(Command jsonCommand, SpecificationWrapper specificationWrapper, IConfig config, ILogger logger)
    {
        var allDatFilesCommand = new Command("allDatFiles", "exports all dat files");
        jsonCommand.Add(allDatFilesCommand);

        allDatFilesCommand.SetHandler(
            () =>
            {
                var datExporter = new DatJsonExporter(specificationWrapper.GetOrCreateSpecification(), config, logger);
                logger.Information("exporing all dat files");
                datExporter.Run();
            });
    }

    private static void AddExportListedDatFiles(Command jsonCommand, SpecificationWrapper specificationWrapper, IConfig config, ILogger logger)
    {
        var listedDatFilesCommand = new Command("datFiles", "exports listed dat files");
        jsonCommand.Add(listedDatFilesCommand);

        var datFiles = new Option<string[]>("--files", "exports listed dat files (space separated)")
        {
            AllowMultipleArgumentsPerToken = true,
            IsRequired = true,
        };

        listedDatFilesCommand.Add(datFiles);

        listedDatFilesCommand.SetHandler(
            (datFiles) =>
            {
                if (datFiles.Length != 0)
                {
                    var datExporter = new DatJsonExporter(specificationWrapper.GetOrCreateSpecification(), config, logger);
                    logger.Information("exporing listed dat files: {files}", string.Join(", ", datFiles));
                    logger.Error("not yet implemented");
                }
                else
                {
                    logger.Error("passed 0 files to export");
                }
            },
            datFiles);
    }
}
