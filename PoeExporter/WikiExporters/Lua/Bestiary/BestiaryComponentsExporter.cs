using PoeExporter.WikiExporters.Lua.Helpers;
using Serilog;
using System.Globalization;

namespace PoeExporter.WikiExporters.Lua.Bestiary;

/// <summary>
/// Class used to export data for https://www.poewiki.net/wiki/Module:Bestiary/components.
/// </summary>
internal sealed class BestiaryComponentsExporter : IExporter<BestiaryComponentsExporter>
{
    private readonly SpecificationWrapper specificationWrapper;
    private readonly ILogger logger;

    /// <inheritdoc/>
    public string PageName => "components";

    /// <summary>
    /// Initializes a new instance of the <see cref="BestiaryComponentsExporter"/> class.
    /// </summary>
    /// <param name="specificationWrapper">specification wrapper.</param>
    /// <param name="logger">logger.</param>
    public BestiaryComponentsExporter(SpecificationWrapper specificationWrapper, ILogger logger)
    {
        this.specificationWrapper = specificationWrapper;
        this.logger = logger;
    }

    /// <inheritdoc cref="IExporter{T}.Create(SpecificationWrapper, ILogger)"/>
    public static BestiaryComponentsExporter Create(SpecificationWrapper specificationWrapper, ILogger logger)
    {
        return new BestiaryComponentsExporter(specificationWrapper, logger);
    }

    /// <inheritdoc/>
    public string Export()
    {
        var components = GetBestiaryComponents();

        var results = LuaConverter.ToLuaString(components);

        return results;
    }

    private IReadOnlyList<BestiaryComponent> GetBestiaryComponents()
    {
        logger.Verbose("running {method}", nameof(GetBestiaryComponents));
        var results = new List<BestiaryComponent>();
        var specification = specificationWrapper.GetOrCreateSpecification();

        var bestiaryRecipeComponents = specification.LoadBestiaryRecipeComponentDat();
        var bestiaryFamilies = specification.LoadBestiaryFamiliesDat();
        var bestiaryGroups = specification.LoadBestiaryGroupsDat();
        var mods = specification.LoadModsDat();
        var bestiaryCapturableMonsters = specification.LoadBestiaryCapturableMonstersDat();
        var rarities = specification.LoadRarityDat();
        var bestiaryGenus = specification.LoadBestiaryGenusDat();
        var clientStrings = specification.LoadClientStringsDat().ToDictionary(x => x.Id);

        foreach (var bestiaryRecipeComponent in bestiaryRecipeComponents)
        {
            var monsterKey = bestiaryRecipeComponent.BestiaryCapturableMonstersKey;
            var monster = monsterKey is not null ? bestiaryCapturableMonsters[monsterKey.Value] : null;

            var familyKey = bestiaryRecipeComponent.BestiaryFamiliesKey;
            var family = familyKey is not null ? bestiaryFamilies[familyKey.Value] : null;

            var groupKey = bestiaryRecipeComponent.BestiaryGroupsKey;
            var group = groupKey is not null ? bestiaryGroups[groupKey.Value] : null;

            var modKey = bestiaryRecipeComponent.ModsKey;
            var mod = modKey is not null ? mods[modKey.Value] : null;

            var genusKey = bestiaryRecipeComponent.BestiaryGenusKey;
            var genus = genusKey is not null ? bestiaryGenus[genusKey.Value] : null;

            var rarityKey = bestiaryRecipeComponent.BeastRarity;
            var rarity = rarityKey is not null ? rarities[rarityKey.Value] : null;

            var displayRarity =
                rarity?.Id is not null ? $"ItemDisplayString{CultureInfo.InvariantCulture.TextInfo.ToTitleCase(rarity.Id.ToLower())}" : null;

            var clientString = displayRarity is not null ? clientStrings[displayRarity] : null;

            var obj = new BestiaryComponent()
            {
                Id = bestiaryRecipeComponent.Id,
                MinLevel = bestiaryRecipeComponent.MinLevel,
                Monster = monster?.Name,
                Family = family?.Name,
                BeastGroup = group?.Name,
                ModId = mod?.Id,
                Genus = genus?.Name,
                Rarity = clientString?.Text,
            };

            results.Add(obj);
        }

        return results;
    }
}
