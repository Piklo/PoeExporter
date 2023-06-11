using PoeData.Specifications;
using PoeExporter.WikiExporters.Lua.Helpers;
using System.Globalization;

namespace PoeExporter.WikiExporters.Lua.Bestiary;

/// <summary>
/// Class used to export data for https://www.poewiki.net/wiki/Module:Bestiary/components.
/// </summary>
internal sealed class BestiaryComponentsExporter : IExporter
{
    private readonly Specification specification;

    /// <inheritdoc/>
    public string PageName => "components";

    /// <summary>
    /// Initializes a new instance of the <see cref="BestiaryComponentsExporter"/> class.
    /// </summary>
    /// <param name="wikiExporterParameters">wiki exporter parameters.</param>
    public BestiaryComponentsExporter(WikiExporterParameters wikiExporterParameters)
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

    private IReadOnlyList<BestiaryComponent> GetItems()
    {
        var results = new List<BestiaryComponent>();

        var bestiaryRecipeComponents = specification.LoadBestiaryRecipeComponentRepository();
        var clientStrings = specification.LoadClientStringsRepository().Items.ToDictionary(x => x.Id);

        foreach (var bestiaryRecipeComponent in bestiaryRecipeComponents.Items)
        {
            var monster = bestiaryRecipeComponent.GetItemForBestiaryCapturableMonstersKey();

            var family = bestiaryRecipeComponent.GetItemForBestiaryFamiliesKey();

            var group = bestiaryRecipeComponent.GetItemForBestiaryGroupsKey();

            var mod = bestiaryRecipeComponent.GetItemForModsKey();

            var genus = bestiaryRecipeComponent.GetItemForBestiaryGenusKey();

            var rarity = bestiaryRecipeComponent.GetItemForBeastRarity();

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
