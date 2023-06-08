using PoeExporter.WikiExporters.Lua.Helpers;
using Serilog;

namespace PoeExporter.WikiExporters.Lua.Blight;

/// <summary>
/// Class used to export data for https://www.poewiki.net/wiki/Module:Blight/blight_crafting_recipes.
/// </summary>
internal sealed class BlightCraftingRecipesExporter : IExporter<BlightCraftingRecipesExporter>
{
    private readonly SpecificationWrapper specificationWrapper;
    private readonly ILogger logger;

    /// <inheritdoc/>
    public string PageName { get; } = "blight_crafting_recipes";

    /// <summary>
    /// Initializes a new instance of the <see cref="BlightCraftingRecipesExporter"/> class.
    /// </summary>
    /// <param name="specificationWrapper">specification wrapper.</param>
    /// <param name="logger">logger.</param>
    public BlightCraftingRecipesExporter(SpecificationWrapper specificationWrapper, ILogger logger)
    {
        this.specificationWrapper = specificationWrapper;
        this.logger = logger;
    }

    /// <inheritdoc cref="IExporter{T}.Create(SpecificationWrapper, ILogger)"/>
    public static BlightCraftingRecipesExporter Create(SpecificationWrapper specificationWrapper, ILogger logger)
    {
        return new BlightCraftingRecipesExporter(specificationWrapper, logger);
    }

    /// <inheritdoc/>
    public string Export()
    {
        var recipes = GetBlightCraftingRecipes();

        var results = LuaConverter.ToLuaString(recipes);

        return results;
    }

    /// <summary>
    /// Gets <see cref="BlightCraftingRecipe"/>.
    /// </summary>
    /// <returns>list of blight crafting recipes.</returns>
    private IReadOnlyList<BlightCraftingRecipe> GetBlightCraftingRecipes()
    {
        logger.Verbose("running {method}", nameof(GetBlightCraftingRecipes));
        var results = new List<BlightCraftingRecipe>();
        var specification = specificationWrapper.GetOrCreateSpecification();

        var craftingRecipes = specification.LoadBlightCraftingRecipesRepository();

        foreach (var recipe in craftingRecipes.Items)
        {
            var craftingResult = recipe.GetItemForBlightCraftingResultsKey() ?? throw new NotImplementedException();

            var passiveSkill = craftingResult.GetItemForPassiveSkillsKey();

            var modifier = craftingResult.GetItemForModsKey();

            var passiveId = passiveSkill?.Id;

            var modifierId = modifier?.Id;

            var craftingType = recipe.GetItemForBlightCraftingTypesKey() ?? throw new NotImplementedException();

            var type = craftingType.Id;

            var result = new BlightCraftingRecipe()
            {
                Id = recipe.Id,
                PassiveId = passiveId,
                Type = type,
                ModifierId = modifierId,
            };

            results.Add(result);
        }

        return results;
    }
}
