using PoeData.Specifications.DatFiles;
using PoeExporter.WikiExporters.Lua.Helpers;
using Serilog;

namespace PoeExporter.WikiExporters.Lua.Bestiary;

/// <summary>
/// Class used to export data for https://www.poewiki.net/wiki/Module:Bestiary/recipes.
/// </summary>
internal sealed class BestiaryRecipesExporter : IExporter<BestiaryRecipesExporter>
{
    private readonly SpecificationWrapper specificationWrapper;
    private readonly ILogger logger;

    /// <inheritdoc/>
    public string PageName { get; } = "recipes";

    /// <summary>
    /// Initializes a new instance of the <see cref="BestiaryRecipesExporter"/> class.
    /// </summary>
    /// <param name="specificationWrapper">specification wrapper.</param>
    /// <param name="logger">logger.</param>
    public BestiaryRecipesExporter(SpecificationWrapper specificationWrapper, ILogger logger)
    {
        this.specificationWrapper = specificationWrapper;
        this.logger = logger;
    }

    /// <inheritdoc cref="IExporter{T}.Create(SpecificationWrapper, ILogger)"/>
    public static BestiaryRecipesExporter Create(SpecificationWrapper specificationWrapper, ILogger logger)
    {
        return new BestiaryRecipesExporter(specificationWrapper, logger);
    }

    /// <inheritdoc/>
    public string Export()
    {
        var regions = GetBestiaryRecipes();

        var results = LuaConverter.ToLuaString(regions);

        return results;
    }

    private IReadOnlyList<BestiaryRecipe> GetBestiaryRecipes()
    {
        logger.Verbose("running {method}", nameof(GetBestiaryRecipes));
        var results = new List<BestiaryRecipe>();
        var specification = specificationWrapper.GetOrCreateSpecification();

        var bestiaryRecipes = specification.LoadBestiaryRecipesDat();

        foreach (var recipe in bestiaryRecipes)
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
