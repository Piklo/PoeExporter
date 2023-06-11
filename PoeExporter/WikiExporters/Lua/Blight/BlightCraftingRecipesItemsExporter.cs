using PoeData.Specifications;
using PoeExporter.WikiExporters.Lua.Helpers;

namespace PoeExporter.WikiExporters.Lua.Blight;

/// <summary>
/// Class used to export data for https://www.poewiki.net/wiki/Module:Blight/blight_crafting_recipes_items.
/// </summary>
internal sealed class BlightCraftingRecipesItemsExporter : IExporter
{
    private readonly Specification specification;

    /// <inheritdoc/>
    public string PageName { get; } = "blight_crafting_recipes_items";

    /// <summary>
    /// Initializes a new instance of the <see cref="BlightCraftingRecipesItemsExporter"/> class.
    /// </summary>
    /// <param name="wikiExporterParameters">wiki exporter parameters.</param>
    public BlightCraftingRecipesItemsExporter(WikiExporterParameters wikiExporterParameters)
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

    private IReadOnlyList<BlightCraftingRecipesItem> GetItems()
    {
        var results = new List<BlightCraftingRecipesItem>();

        var recipes = specification.LoadBlightCraftingRecipesRepository();

        foreach (var recipe in recipes.Items)
        {
            var craftingItemsTemp = recipe.GetItemsForBlightCraftingItemsKeys();
            for (var i = 0; i < craftingItemsTemp.Count; i++)
            {
                var ordinal = i + 1;

                var recipeId = recipe.Id;

                var blightCraftingItem = craftingItemsTemp[i].Value;

                var item = blightCraftingItem.GetItemForOil() ?? throw new NullItemException();

                var itemId = item.Id;

                var res = new BlightCraftingRecipesItem()
                {
                    ItemId = itemId,
                    Ordinal = ordinal,
                    RecipeId = recipeId,
                };

                results.Add(res);
            }
        }

        return results;
    }
}
