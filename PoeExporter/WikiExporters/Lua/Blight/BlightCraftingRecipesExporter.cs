using PoeData.Specifications.DatFiles;
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
        var craftingResults = specification.LoadBlightCraftingResultsRepository();
        var craftingTypes = specification.LoadBlightCraftingTypesRepository();
        var passiveSkills = specification.LoadPassiveSkillsRepository();
        var mods = specification.LoadModsRepository();

        foreach (var recipe in craftingRecipes.Items)
        {
            var resultKey = recipe.BlightCraftingResultsKey;
            if (resultKey is null)
            {
                logger.Warning("recipe with id = {id} has null {column}", recipe.Id, nameof(recipe.BlightCraftingResultsKey));
                continue;
            }

            var craftingResult = craftingResults.Items[resultKey.Value];
            var passiveSkillsKey = craftingResult.PassiveSkillsKey;
            var modsKey = craftingResult.ModsKey;

            if (passiveSkillsKey is null && modsKey is null)
            {
                logger.Warning(
                    "crafting result with id = {id} has null {column1} and {column2}",
                    craftingResult.Id,
                    nameof(craftingResult.PassiveSkillsKey),
                    nameof(craftingResult.ModsKey));
                continue;
            }

            PassiveSkillsDat? passiveSkill = null;
            if (passiveSkillsKey is not null)
            {
                passiveSkill = passiveSkills.Items[passiveSkillsKey.Value];
            }

            ModsDat? modifier = null;
            if (modsKey is not null)
            {
                modifier = mods.Items[modsKey.Value];
            }

            var passiveId = passiveSkill?.Id;
            var modifierId = modifier?.Id;

            var typesKey = recipe.BlightCraftingTypesKey;
            if (typesKey is null)
            {
                logger.Warning("recipe with id = {id} has null {column}", recipe.Id, nameof(recipe.BlightCraftingTypesKey));
                continue;
            }

            var craftingType = craftingTypes.Items[typesKey.Value];
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
