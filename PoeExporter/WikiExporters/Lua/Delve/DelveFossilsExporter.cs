using PoeExporter.WikiExporters.Lua.Helpers;
using Serilog;

namespace PoeExporter.WikiExporters.Lua.Delve;

/// <summary>
/// Class used to export data for https://www.poewiki.net/wiki/Module:Delve/fossils.
/// </summary>
internal sealed class DelveFossilsExporter : IExporter<DelveFossilsExporter>
{
    private readonly SpecificationWrapper specificationWrapper;
    private readonly ILogger logger;

    /// <inheritdoc/>
    public string PageName { get; } = "fossils";

    /// <summary>
    /// Initializes a new instance of the <see cref="DelveFossilsExporter"/> class.
    /// </summary>
    /// <param name="specificationWrapper">specification wrapper.</param>
    /// <param name="logger">logger.</param>
    public DelveFossilsExporter(SpecificationWrapper specificationWrapper, ILogger logger)
    {
        this.specificationWrapper = specificationWrapper;
        this.logger = logger;
    }

    /// <inheritdoc cref="IExporter{T}.Create(SpecificationWrapper, ILogger)"/>
    public static DelveFossilsExporter Create(SpecificationWrapper specificationWrapper, ILogger logger)
    {
        return new DelveFossilsExporter(specificationWrapper, logger);
    }

    /// <inheritdoc/>
    public string Export()
    {
        var costs = GetDelveFossils();

        var results = LuaConverter.ToLuaString(costs);

        return results;
    }

    private IReadOnlyList<DelveFossil> GetDelveFossils()
    {
        logger.Verbose("running {method}", nameof(GetDelveFossils));
        var results = new List<DelveFossil>();
        var specification = specificationWrapper.GetOrCreateSpecification();

        var craftingModifiers = specification.LoadDelveCraftingModifiersRepository();

        foreach (var modifier in craftingModifiers.Items)
        {
            var baseItem = modifier.GetItemForBaseItemTypesKey() ?? throw new NullItemException();

            if (baseItem.Id.Contains("RandomFossilOutcome"))
            {
                continue;
            }

            var addedMods = modifier.GetItemsForAddedModsKeys();
            var addedModIds = new string[addedMods.Count];
            for (var i = 0; i < addedMods.Count; i++)
            {
                var mod = addedMods[i].Value;
                addedModIds[i] = mod.Id;
            }

            var forcedMods = modifier.GetItemsForForcedAddModsKeys();
            var forcedModIds = new string[forcedMods.Count];
            for (var i = 0; i < forcedMods.Count; i++)
            {
                var mod = forcedMods[i].Value;
                forcedModIds[i] = mod.Id;
            }

            var sellPriceMods = modifier.GetItemsForSellPrice_ModsKeys();
            var sellPriceModIds = new string[sellPriceMods.Count];
            for (var i = 0; i < sellPriceMods.Count; i++)
            {
                var mod = sellPriceMods[i].Value;
                sellPriceModIds[i] = mod.Id;
            }

            var forbiddenCraftingTags = modifier.GetItemsForForbiddenDelveCraftingTagsKeys();
            var forbiddenTags = new string[forbiddenCraftingTags.Count];
            for (var i = 0; i < forbiddenCraftingTags.Count; i++)
            {
                var delveTag = forbiddenCraftingTags[i].Value;
                var tag = delveTag.GetItemForTagsKey() ?? throw new NullItemException();
                forbiddenTags[i] = tag.Id;
            }

            var allowedCraftingTags = modifier.GetItemsForAllowedDelveCraftingTagsKeys();
            var allowedTags = new string[allowedCraftingTags.Count];
            for (var i = 0; i < allowedCraftingTags.Count; i++)
            {
                var delveTag = allowedCraftingTags[i].Value;
                var tag = delveTag.GetItemForTagsKey() ?? throw new NullItemException();
                allowedTags[i] = tag.Id;
            }

            var obj = new DelveFossil()
            {
                BaseItemId = baseItem.Id,
                AddedModifierIds = addedModIds,
                ForcedModifierIds = forcedModIds,
                SellPriceModifierIds = sellPriceModIds,
                ForbiddenTags = forbiddenTags,
                AllowedTags = allowedTags,
                CorruptedEssenceChance = modifier.CorruptedEssenceChance,
                CanMirror = modifier.CanMirrorItem,
                CanEnchant = modifier.CanRollEnchant,
                CanQuality = modifier.CanImproveQuality,
                CanRollWhiteSockets = modifier.CanRollWhiteSockets,
                IsLucky = modifier.HasLuckyRolls,
            };

            results.Add(obj);
        }

        return results;
    }
}
