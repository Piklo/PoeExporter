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

        var craftingModifiers = specification.LoadDelveCraftingModifiersDat();
        var baseItems = specification.LoadBaseItemTypesDat();
        var mods = specification.LoadModsDat();
        var delveCraftingTags = specification.LoadDelveCraftingTagsDat();
        var tags = specification.LoadTagsDat();

        foreach (var modifier in craftingModifiers)
        {
            if (modifier.BaseItemTypesKey is null)
            {
                logger.Warning("modifier with id = {id} has null {column}", nameof(modifier.BaseItemTypesKey));
                continue;
            }

            var baseItem = baseItems[modifier.BaseItemTypesKey.Value];

            if (baseItem.Id.Contains("RandomFossilOutcome"))
            {
                continue;
            }

            var addedModIds = new string[modifier.AddedModsKeys.Count];
            for (var i = 0; i < modifier.AddedModsKeys.Count; i++)
            {
                var key = modifier.AddedModsKeys[i];
                var mod = mods[key];
                addedModIds[i] = mod.Id;
            }

            var forcedModIds = new string[modifier.ForcedAddModsKeys.Count];
            for (var i = 0; i < modifier.ForcedAddModsKeys.Count; i++)
            {
                var key = modifier.ForcedAddModsKeys[i];
                var mod = mods[key];
                forcedModIds[i] = mod.Id;
            }

            var sellPriceModIds = new string[modifier.SellPrice_ModsKeys.Count];
            for (var i = 0; i < modifier.SellPrice_ModsKeys.Count; i++)
            {
                var key = modifier.SellPrice_ModsKeys[i];
                var mod = mods[key];
                sellPriceModIds[i] = mod.Id;
            }

            var forbiddenTags = new string[modifier.ForbiddenDelveCraftingTagsKeys.Count];
            for (var i = 0; i < modifier.ForbiddenDelveCraftingTagsKeys.Count; i++)
            {
                var key = modifier.ForbiddenDelveCraftingTagsKeys[i];
                var delveTag = delveCraftingTags[key];
                if (delveTag.TagsKey is null)
                {
                    logger.Warning("{var} has null {prop}", nameof(delveTag), nameof(delveTag.TagsKey));
                    continue;
                }

                var tag = tags[delveTag.TagsKey.Value];
                forbiddenTags[i] = tag.Id;
            }

            var allowedTags = new string[modifier.AllowedDelveCraftingTagsKeys.Count];
            for (var i = 0; i < modifier.AllowedDelveCraftingTagsKeys.Count; i++)
            {
                var key = modifier.AllowedDelveCraftingTagsKeys[i];
                var delveTag = delveCraftingTags[key];
                if (delveTag.TagsKey is null)
                {
                    logger.Warning("{var} has null {prop}", nameof(delveTag), nameof(delveTag.TagsKey));
                    continue;
                }

                var tag = tags[delveTag.TagsKey.Value];
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
