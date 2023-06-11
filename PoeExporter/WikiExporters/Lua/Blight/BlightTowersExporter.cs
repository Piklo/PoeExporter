using PoeData.Specifications;
using PoeExporter.WikiExporters.Lua.Helpers;

namespace PoeExporter.WikiExporters.Lua.Blight;

/// <summary>
/// Class used to export data for https://www.poewiki.net/wiki/Module:Blight/blight_towers.
/// </summary>
internal sealed class BlightTowersExporter : IExporter
{
    private readonly Specification specification;

    /// <inheritdoc/>
    public string PageName { get; } = "blight_towers";

    /// <summary>
    /// Initializes a new instance of the <see cref="BlightTowersExporter"/> class.
    /// </summary>
    /// <param name="wikiExporterParameters">wiki exporter parameters.</param>
    public BlightTowersExporter(WikiExporterParameters wikiExporterParameters)
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

    private IReadOnlyList<BlightTower> GetItems()
    {
        var results = new List<BlightTower>();

        var blightTowersPerLevel = specification.LoadBlightTowersPerLevelRepository();

        foreach (var towerPerLevel in blightTowersPerLevel.Items)
        {
            var tower = towerPerLevel.GetItemForBlightTowersKey() ?? throw new NullItemException();

            var replacedIcon = tower.Icon.StartsWith("Art/2DArt/UIImages/InGame/Blight/Tower Icons")
                ? $"""File:{tower.Icon.Replace("Art/2DArt/UIImages/InGame/Blight/Tower Icons/Icon", string.Empty)} tower icon.png""" : null;

            var res = new BlightTower()
            {
                Id = tower.Id,
                Name = tower.Name,
                Description = tower.Description,
                Tier = tower.Tier,
                Radius = tower.Radius,
                Icon = replacedIcon,
                Cost = towerPerLevel.Cost,
            };

            results.Add(res);
        }

        return results;
    }
}
