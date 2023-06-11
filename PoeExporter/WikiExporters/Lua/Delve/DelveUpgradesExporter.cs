using PoeExporter.WikiExporters.Lua.Helpers;
using Serilog;

namespace PoeExporter.WikiExporters.Lua.Delve;

/// <summary>
/// Class used to export data for https://www.poewiki.net/wiki/Module:Delve/delve_upgrades.
/// </summary>
internal sealed class DelveUpgradesExporter : IExporter<DelveUpgradesExporter>
{
    private readonly SpecificationWrapper specificationWrapper;
    private readonly ILogger logger;

    /// <inheritdoc/>
    public string PageName { get; } = "delve_upgrades";

    /// <summary>
    /// Initializes a new instance of the <see cref="DelveUpgradesExporter"/> class.
    /// </summary>
    /// <param name="specificationWrapper">specification wrapper.</param>
    /// <param name="logger">logger.</param>
    public DelveUpgradesExporter(SpecificationWrapper specificationWrapper, ILogger logger)
    {
        this.specificationWrapper = specificationWrapper;
        this.logger = logger;
    }

    /// <inheritdoc cref="IExporter{T}.Create(SpecificationWrapper, ILogger)"/>
    public static DelveUpgradesExporter Create(SpecificationWrapper specificationWrapper, ILogger logger)
    {
        return new DelveUpgradesExporter(specificationWrapper, logger);
    }

    /// <inheritdoc/>
    public string Export()
    {
        var costs = GetDelveUpgrades();

        var results = LuaConverter.ToLuaString(costs);

        return results;
    }

    private IReadOnlyList<DelveUpgrade> GetDelveUpgrades()
    {
        logger.Verbose("running {method}", nameof(GetDelveUpgrades));
        var results = new List<DelveUpgrade>();
        var specification = specificationWrapper.GetOrCreateSpecification();

        var delveUpgrades = specification.LoadDelveUpgradesRepository();

        foreach (var upgrade in delveUpgrades.Items)
        {
            var obj = new DelveUpgrade()
            {
                Type = ((DelveUpgradeTypes)upgrade.DelveUpgradeTypeKey).ToString().ToLower(),
                Level = upgrade.UpgradeLevel,
                Cost = upgrade.Cost,
            };

            results.Add(obj);
        }

        return results;
    }
}
