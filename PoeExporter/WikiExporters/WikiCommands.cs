using PoeExporter.WikiExporters.Lua.Bestiary;
using PoeExporter.WikiExporters.Lua.Blight;
using PoeExporter.WikiExporters.Lua.CraftingBench;
using Serilog;
using System.CommandLine;

namespace PoeExporter.WikiExporters;

/// <summary>
/// Class which adds wiki exporter commands to a <see cref="Command"/>.
/// </summary>
[AddWikiExporter(
    new Type[]
    {
        typeof(BlightCraftingRecipesExporter),
        typeof(BlightCraftingRecipesItemsExporter),
        typeof(BlightTowersExporter),
    },
    new string[] { "--luablight", "--blight" },
    "Exports lua blight data")]
[AddWikiExporter(
    new Type[]
    {
        typeof(BestiaryRecipesExporter),
        typeof(BestiaryComponentsExporter),
    },
    new string[] { "--luabestiary", "--bestiary" },
    "Exports lua bestiary data")]
[AddWikiExporter(
    new Type[]
    {
        typeof(CraftingBenchOptionsExporter),
        typeof(CraftingBenchCostsExporter),
    },
    new string[] { "--luacraftingbench", "--craftingbench" },
    "Exports lua crafting bench data")]
internal sealed partial class WikiCommands
{
    /// <summary>
    /// Adds wiki commands.
    /// </summary>
    /// <param name="rootCommand">command to which the method adds subcommands.</param>
    /// <param name="specificationWrapper">specification wrapper.</param>
    /// <param name="config">config.</param>
    /// <param name="logger">logger.</param>
    public static void AddCommands(Command rootCommand, SpecificationWrapper specificationWrapper, IConfig config, ILogger logger)
    {
        _ = new WikiCommands(rootCommand, specificationWrapper, config, logger);
    }
}
