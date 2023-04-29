using PoeExporter.WikiExporters.Lua.Blight;
using Serilog;
using System.CommandLine;

namespace PoeExporter.WikiExporters;

/// <summary>
/// Class which adds wiki exporter commands to a <see cref="Command"/>.
/// </summary>
[AddWikiExporter(
    new Type[] { typeof(BlightCraftingRecipesExporter) },
    new string[] { "--luablight", "--blight" },
    "Exports lua blight data")]
internal sealed partial class WikiCommands
{
    /// <summary>
    /// Adds wiki commands.
    /// </summary>
    /// <param name="specificationWrapper">specification wrapper.</param>
    /// <param name="logger">logger.</param>
    /// <param name="rootCommand">command to which the method adds subcommands.</param>
    public static void AddCommands(SpecificationWrapper specificationWrapper, ILogger logger, Command rootCommand)
    {
        _ = new WikiCommands(specificationWrapper, logger, rootCommand);
    }
}
