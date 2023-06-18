using PoeData.Specifications;
using PoeExporter.WikiExporters.Lua.Helpers;

namespace PoeExporter.WikiExporters.Lua.Delve;

/// <summary>
/// Class used to export data for https://www.poewiki.net/wiki/Module:Delve/delve_upgrade_stats.
/// </summary>
internal sealed class DelveUpgradeStatsExporter : IExporter
{
    private readonly Specification specification;

    /// <inheritdoc/>
    public string PageName { get; } = "delve_upgrade_stats";

    /// <summary>
    /// Initializes a new instance of the <see cref="DelveUpgradeStatsExporter"/> class.
    /// </summary>
    /// <param name="wikiExporterParameters">wiki exporter parameters.</param>
    public DelveUpgradeStatsExporter(WikiExporterParameters wikiExporterParameters)
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

    private IReadOnlyList<DelveUpgradeStat> GetItems()
    {
        var results = new List<DelveUpgradeStat>();

        var upgradeStats = specification.LoadDelveUpgradesRepository();

        foreach (var upgrade in upgradeStats.Items)
        {
            var stats = upgrade.GetItemsForStatsKeys();

            for (var i = 0; i < stats.Count; i++)
            {
                var stat = stats[i];

                var obj = new DelveUpgradeStat()
                {
                    Id = stat.Id,
                    Level = upgrade.UpgradeLevel,
                    Type = DelveExporterHelper.GetDelveUpgradeStatString(upgrade.DelveUpgradeTypeKey),
                    Value = upgrade.StatValues[i],
                };

                results.Add(obj);
            }
        }

        return results;
    }
}
