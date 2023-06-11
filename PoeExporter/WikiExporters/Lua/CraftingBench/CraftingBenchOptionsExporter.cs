using PoeExporter.WikiExporters.Lua.Helpers;
using Serilog;

namespace PoeExporter.WikiExporters.Lua.CraftingBench;

/// <summary>
/// Class used to export data for https://www.poewiki.net/wiki/Module:Crafting_bench/crafting_bench_options.
/// </summary>
internal sealed class CraftingBenchOptionsExporter : IExporter<CraftingBenchOptionsExporter>
{
    private readonly SpecificationWrapper specificationWrapper;
    private readonly ILogger logger;

    /// <inheritdoc/>
    public string PageName { get; } = "crafting_bench_options";

    /// <summary>
    /// Initializes a new instance of the <see cref="CraftingBenchOptionsExporter"/> class.
    /// </summary>
    /// <param name="specificationWrapper">specification wrapper.</param>
    /// <param name="logger">logger.</param>
    public CraftingBenchOptionsExporter(SpecificationWrapper specificationWrapper, ILogger logger)
    {
        this.specificationWrapper = specificationWrapper;
        this.logger = logger;
    }

    /// <inheritdoc cref="IExporter{T}.Create(SpecificationWrapper, ILogger)"/>
    public static CraftingBenchOptionsExporter Create(SpecificationWrapper specificationWrapper, ILogger logger)
    {
        return new CraftingBenchOptionsExporter(specificationWrapper, logger);
    }

    /// <inheritdoc/>
    public string Export()
    {
        var regions = GetCraftingBenchOptions();

        var results = LuaConverter.ToLuaString(regions);

        return results;
    }

    private IReadOnlyList<CraftingBenchOption> GetCraftingBenchOptions()
    {
        logger.Verbose("running {method}", nameof(GetCraftingBenchOptions));
        var results = new List<CraftingBenchOption>();
        var specification = specificationWrapper.GetOrCreateSpecification();

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
                var itemClass = itemClassesTemp[i].Value;
                localItemClasses[i] = itemClassesTemp[i].Value.Name;
                itemClassIds[i] = itemClass.Id;
            }

            var recipeIds = benchOption.GetItemsForRecipeIds();
            var unlockArea = recipeIds.Count != 0 ? benchOption.GetItemsForRecipeIds()[0].Value.GetItemForUnlockArea() : null;

            var itemClassCategoriesTemp = benchOption.GetItemsForCraftingItemClassCategories();
            var itemClassCategories = new string[benchOption.CraftingItemClassCategories.Count];
            for (var i = 0; i < itemClassCategoriesTemp.Count; i++)
            {
                var item = itemClassCategoriesTemp[i].Value;
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
