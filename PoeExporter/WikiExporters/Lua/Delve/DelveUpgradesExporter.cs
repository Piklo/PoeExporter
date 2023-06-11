using PoeData.Specifications;
using PoeExporter.WikiExporters.Lua.Helpers;

namespace PoeExporter.WikiExporters.Lua.Delve;

/// <summary>
/// Class used to export data for https://www.poewiki.net/wiki/Module:Delve/delve_upgrades.
/// </summary>
internal sealed class DelveUpgradesExporter : IExporter
{
    private readonly Specification specification;

    /// <inheritdoc/>
    public string PageName { get; } = "delve_upgrades";

    /// <summary>
    /// Initializes a new instance of the <see cref="DelveUpgradesExporter"/> class.
    /// </summary>
    /// <param name="wikiExporterParameters">wiki exporter parameters.</param>
    public DelveUpgradesExporter(WikiExporterParameters wikiExporterParameters)
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

    private IReadOnlyList<DelveUpgrade> GetItems()
    {
        var results = new List<DelveUpgrade>();

        var delveUpgrades = specification.LoadDelveUpgradesRepository();

        foreach (var upgrade in delveUpgrades.Items)
        {
            var obj = new DelveUpgrade()
            {
                Type = DelveExporterHelper.GetDelveUpgradeStatString(upgrade.DelveUpgradeTypeKey),
                Level = upgrade.UpgradeLevel,
                Cost = upgrade.Cost,
            };

            results.Add(obj);
        }

        return results;
    }
}
