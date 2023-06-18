using PoeData.Specifications;
using PoeExporter.WikiExporters.Lua.Helpers;

namespace PoeExporter.WikiExporters.Lua.CraftingBench;

/// <summary>
/// Class used to export data for https://www.poewiki.net/wiki/Module:Crafting_bench/crafting_bench_options.
/// </summary>
internal sealed class CraftingBenchOptionsExporter : IExporter
{
    private readonly Specification specification;

    /// <inheritdoc/>
    public string PageName { get; } = "crafting_bench_options";

    /// <summary>
    /// Initializes a new instance of the <see cref="CraftingBenchOptionsExporter"/> class.
    /// </summary>
    /// <param name="wikiExporterParameters">wiki exporter parameters.</param>
    public CraftingBenchOptionsExporter(WikiExporterParameters wikiExporterParameters)
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

    private IReadOnlyList<CraftingBenchOption> GetItems()
    {
        var results = new List<CraftingBenchOption>();

        var benchOptions = specification.LoadCraftingBenchOptionsRepository();

        for (var rowId = 0; rowId < benchOptions.Items.Count; rowId++)
        {
            var benchOption = benchOptions.Items[rowId];

            var hideoutNpc = benchOption.GetItemForHideoutNPCsKey();

            var npc = hideoutNpc?.GetItemForHideout_NPCsKey();

            var mod = benchOption.GetItemForAddMod();

            var itemClassesTemp = benchOption.GetItemsForItemClasses();
            var localItemClasses = new string[benchOption.ItemClasses.Count];
            var itemClassIds = new string[benchOption.ItemClasses.Count];
            for (var i = 0; i < itemClassesTemp.Count; i++)
            {
                var itemClass = itemClassesTemp[i];
                localItemClasses[i] = itemClass.Name;
                itemClassIds[i] = itemClass.Id;
            }

            var recipeIds = benchOption.GetItemsForRecipeIds();
            var unlockArea = recipeIds.Count != 0 ? benchOption.GetItemsForRecipeIds()[0].GetItemForUnlockArea() : null;

            var itemClassCategoriesTemp = benchOption.GetItemsForCraftingItemClassCategories();
            var itemClassCategories = new string[benchOption.CraftingItemClassCategories.Count];
            for (var i = 0; i < itemClassCategoriesTemp.Count; i++)
            {
                var item = itemClassCategoriesTemp[i];
                itemClassCategories[i] = item.Text;
            }

            var sortCategory = benchOption.GetItemForSortCategory() ?? throw new NullItemException();

            var affixType = sortCategory.Id;

            var obj = new CraftingBenchOption()
            {
                Npc = npc?.ShortName,
                Ordinal = benchOption.Order,
                ModId = mod?.Id,
                Name = benchOption.Name,
                RequiredLevel = benchOption.RequiredLevel != 0 ? benchOption.RequiredLevel : null,
                ItemClasses = localItemClasses,
                ItemClassesIds = itemClassIds,
                Links = benchOption.Links != 0 ? benchOption.Links : null,
                SocketColors = benchOption.SocketColours,
                Sockets = benchOption.Sockets != 0 ? benchOption.Sockets : null,
                Description = benchOption.Description,
                RecipeUnlockLocation = unlockArea?.Name,
                Rank = benchOption.Tier,
                ItemClassCategories = itemClassCategories,
                UnveilsRequired = benchOption.UnveilsRequired != 0 ? benchOption.UnveilsRequired : null,
                AffixType = affixType,
                Id = rowId,
            };

            results.Add(obj);
        }

        return results;
    }
}
