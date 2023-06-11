using PoeData.Specifications;
using PoeExporter.WikiExporters.Lua.Helpers;

namespace PoeExporter.WikiExporters.Lua.Blight;

/// <summary>
/// Class used to export data for https://www.poewiki.net/wiki/Module:Blight/blight_crafting_recipes.
/// </summary>
internal sealed class BlightCraftingRecipesExporter : IExporter
{
    private readonly Specification specification;

    /// <inheritdoc/>
    public string PageName { get; } = "blight_crafting_recipes";

    /// <summary>
    /// Initializes a new instance of the <see cref="BlightCraftingRecipesExporter"/> class.
    /// </summary>
    /// <param name="wikiExporterParameters">wiki exporter parameters.</param>
    public BlightCraftingRecipesExporter(WikiExporterParameters wikiExporterParameters)
    {
        specification = wikiExporterParameters.SpecificationWrapper.GetOrCreateSpecification();
    }

    /// <inheritdoc/>
    public string Export()
    {
        var items = GetItems();

        var str = LuaConverter.ToLuaString(items);

        return str;
    }

    private IReadOnlyList<BlightCraftingRecipe> GetItems()
    {
        var results = new List<BlightCraftingRecipe>();

        var craftingRecipes = specification.LoadBlightCraftingRecipesRepository();

        foreach (var recipe in craftingRecipes.Items)
        {
            var craftingResult = recipe.GetItemForBlightCraftingResultsKey() ?? throw new NullItemException();

            var passiveSkill = craftingResult.GetItemForPassiveSkillsKey();

            var modifier = craftingResult.GetItemForModsKey();

            var passiveId = passiveSkill?.Id;

            var modifierId = modifier?.Id;

            var craftingType = recipe.GetItemForBlightCraftingTypesKey() ?? throw new NullItemException();

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
