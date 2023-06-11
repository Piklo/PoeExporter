using PoeExporter.WikiExporters.Lua.Helpers;
using Serilog;

namespace PoeExporter.WikiExporters.Lua.Delve;

/// <summary>
/// Class used to export data for https://www.poewiki.net/wiki/Module:Delve/delve_upgrade_stats.
/// </summary>
internal sealed class DelveUpgradeStatsExporter : IExporter<DelveUpgradeStatsExporter>
{
    private readonly SpecificationWrapper specificationWrapper;
    private readonly ILogger logger;

    /// <inheritdoc/>
    public string PageName { get; } = "delve_upgrade_stats";

    /// <summary>
    /// Initializes a new instance of the <see cref="DelveUpgradeStatsExporter"/> class.
    /// </summary>
    /// <param name="specificationWrapper">specification wrapper.</param>
    /// <param name="logger">logger.</param>
    public DelveUpgradeStatsExporter(SpecificationWrapper specificationWrapper, ILogger logger)
    {
        this.specificationWrapper = specificationWrapper;
        this.logger = logger;
    }

    /// <inheritdoc cref="IExporter{T}.Create(SpecificationWrapper, ILogger)"/>
    public static DelveUpgradeStatsExporter Create(SpecificationWrapper specificationWrapper, ILogger logger)
    {
        return new DelveUpgradeStatsExporter(specificationWrapper, logger);
    }

    /// <inheritdoc/>
    public string Export()
    {
        var costs = GetDelveUpgradeStats();

        var results = LuaConverter.ToLuaString(costs);

        return results;
    }

    private IReadOnlyList<DelveUpgradeStat> GetDelveUpgradeStats()
    {
        logger.Verbose("running {method}", nameof(GetDelveUpgradeStats));
        var results = new List<DelveUpgradeStat>();
        var specification = specificationWrapper.GetOrCreateSpecification();

        var upgradeStats = specification.LoadDelveUpgradesRepository();

        foreach (var upgrade in upgradeStats.Items)
        {
            var stats = upgrade.GetItemsForStatsKeys();

            for (var i = 0; i < stats.Count; i++)
            {
                var stat = stats[i].Value;

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
