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
    /// <param name="specificationWrapper">specification wrapper.</param>
    /// <param name="logger">logger.</param>
    /// <param name="command">command to which the method adds subcommands.</param>
    public static void AddCommands(SpecificationWrapper specificationWrapper, ILogger logger, Command command)
    {
        var jsonCommand = new Command("json", "exports data to json");
        command.Add(jsonCommand);

        AddExportAllDatFiles(specificationWrapper, logger, jsonCommand);

        AddExportListedDatFiles(specificationWrapper, logger, jsonCommand);
    }

    private static void AddExportAllDatFiles(SpecificationWrapper specificationWrapper, ILogger logger, Command jsonCommand)
    {
        var allDatFilesCommand = new Command("allDatFiles", "exports all dat files");
        jsonCommand.Add(allDatFilesCommand);

        allDatFilesCommand.SetHandler(
            () =>
            {
                var datExporter = new DatJsonExporter(logger, specificationWrapper.GetOrCreateSpecification());
                logger.Information("exporing all dat files");
                datExporter.Run();
            });
    }

    private static void AddExportListedDatFiles(SpecificationWrapper specificationWrapper, ILogger logger, Command jsonCommand)
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
                    var datExporter = new DatJsonExporter(logger, specificationWrapper.GetOrCreateSpecification());
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
