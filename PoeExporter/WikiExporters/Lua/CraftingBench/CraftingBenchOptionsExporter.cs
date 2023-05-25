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
        var hideoutNpcs = specification.LoadHideoutNPCsRepository();
        var npcs = specification.LoadNPCsRepository();
        var mods = specification.LoadModsRepository();
        var itemClasses = specification.LoadItemClassesRepository();
        var recipeUnlocks = specification.LoadRecipeUnlockDisplayRepository()
            .Items.ToDictionary(x => x.RecipeId);
        var worldAreas = specification.LoadWorldAreasRepository();
        var craftingClassCategories = specification.LoadCraftingItemClassCategoriesRepository();
        var craftingBenchSortCategories = specification.LoadCraftingBenchSortCategoriesRepository();

        for (var rowId = 0; rowId < benchOptions.Items.Count; rowId++)
        {
            var benchOption = benchOptions.Items[rowId];
            var hideoutNpc = benchOption.HideoutNPCsKey is not null ? hideoutNpcs.Items[benchOption.HideoutNPCsKey.Value] : null;

            var npc =
                hideoutNpc is not null && hideoutNpc.Hideout_NPCsKey is not null ? npcs.Items[hideoutNpc.Hideout_NPCsKey.Value] : null;

            var mod = benchOption.AddMod is not null ? mods.Items[benchOption.AddMod.Value] : null;

            var localItemClasses = new string[benchOption.ItemClasses.Count];
            var itemClassIds = new string[benchOption.ItemClasses.Count];
            for (var i = 0; i < benchOption.ItemClasses.Count; i++)
            {
                var itemClassKey = benchOption.ItemClasses[i];
                var itemClass = itemClasses.Items[itemClassKey];
                localItemClasses[i] = itemClass.Name;
                itemClassIds[i] = itemClass.Id;
            }

            if (benchOption.RecipeIds.Count > 1)
            {
                logger.Warning("bench option has more recipe ids than 1");
                continue;
            }

            int? recipeId = benchOption.RecipeIds.Count != 0 ? benchOption.RecipeIds[0] : null;
            var recipeUnlock = recipeId is not null ? recipeUnlocks[recipeId.Value] : null;
            var area =
                recipeUnlock is not null && recipeUnlock.UnlockArea is not null ? worldAreas.Items[recipeUnlock.UnlockArea.Value] : null;

            var itemClassCategories = new string[benchOption.CraftingItemClassCategories.Count];
            for (var i = 0; i < benchOption.CraftingItemClassCategories.Count; i++)
            {
                var key = benchOption.CraftingItemClassCategories[i];
                var item = craftingClassCategories.Items[key];
                itemClassCategories[i] = item.Text;
            }

            if (benchOption.SortCategory is null)
            {
                logger.Warning("null benchOption.SortCategory");
                continue;
            }

            var affixType = craftingBenchSortCategories.Items[benchOption.SortCategory.Value].Id;

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
                RecipeUnlockLocation = area?.Name,
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
