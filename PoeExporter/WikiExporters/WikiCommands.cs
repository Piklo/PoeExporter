using PoeExporter.WikiExporters.Lua.Bestiary;
using PoeExporter.WikiExporters.Lua.Blight;
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

// [AddWikiExporter(
//    new Type[]
//    {
//        // typeof(AtlasRegionsExporter), // atlas regions are no longer a thing
//        typeof(AtlasBaseItemTypesExporter), // AtlasBaseTypeDrops.dat is no longer a thing
//    },
//    new string[] { "--luaatlas", "--atlas" },
//    "Exports lua atlas data")]
[AddWikiExporter(
    new Type[]
    {
        typeof(BestiaryRecipesExporter),
    },
    new string[] { "--luabestiary", "--bestiary" },
    "Exports lua bestiary data")]
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
