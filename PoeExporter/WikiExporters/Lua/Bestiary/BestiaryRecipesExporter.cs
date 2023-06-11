using PoeData.Specifications;
using PoeData.Specifications.DatFiles;
using PoeExporter.WikiExporters.Lua.Helpers;
using Serilog;

namespace PoeExporter.WikiExporters.Lua.Bestiary;

/// <summary>
/// Class used to export data for https://www.poewiki.net/wiki/Module:Bestiary/recipes.
/// </summary>
internal sealed class BestiaryRecipesExporter : IExporter
{
    private readonly Specification specification;
    private readonly ILogger logger;

    /// <inheritdoc/>
    public string PageName { get; } = "recipes";

    /// <summary>
    /// Initializes a new instance of the <see cref="BestiaryRecipesExporter"/> class.
    /// </summary>
    /// <param name="wikiExporterParameters">wiki exporter parameters.</param>
    public BestiaryRecipesExporter(WikiExporterParameters wikiExporterParameters)
    {
        specification = wikiExporterParameters.SpecificationWrapper.GetOrCreateSpecification();
        logger = wikiExporterParameters.Logger;
    }

    /// <inheritdoc/>
    public string Export()
    {
        var items = GetItems();

        var str = LuaConverter.ToLuaString(items);

        return str;
    }

    private IReadOnlyList<BestiaryRecipe> GetItems()
    {
        var results = new List<BestiaryRecipe>();

        var bestiaryRecipes = specification.LoadBestiaryRecipesRepository();

        foreach (var recipe in bestiaryRecipes.Items)
        {
            var category = recipe.Category;
            if (category is null)
            {
                logger.Warning(
                    "{column} is null for {className}.{idName} = {id}",
                    nameof(recipe.Category),
                    nameof(BestiaryRecipesDat),
                    nameof(BestiaryRecipesDat.Id),
                    recipe.Id);

                continue;
            }

            var obj = new BestiaryRecipe()
            {
                Id = recipe.Id,
                Header = category.Value,
                Subheader = recipe.Description,
                Notes = recipe.Notes,
            };

            results.Add(obj);
        }

        return results;
    }
}
