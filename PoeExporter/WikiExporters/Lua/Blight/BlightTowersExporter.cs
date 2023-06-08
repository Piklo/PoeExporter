using PoeExporter.WikiExporters.Lua.Helpers;
using Serilog;

namespace PoeExporter.WikiExporters.Lua.Blight;

/// <summary>
/// Class used to export data for https://www.poewiki.net/wiki/Module:Blight/blight_towers.
/// </summary>
internal sealed class BlightTowersExporter : IExporter<BlightTowersExporter>
{
    private readonly SpecificationWrapper specificationWrapper;
    private readonly ILogger logger;

    /// <inheritdoc/>
    public string PageName { get; } = "blight_towers";

    /// <summary>
    /// Initializes a new instance of the <see cref="BlightTowersExporter"/> class.
    /// </summary>
    /// <param name="specificationWrapper">specification wrapper.</param>
    /// <param name="logger">logger.</param>
    public BlightTowersExporter(SpecificationWrapper specificationWrapper, ILogger logger)
    {
        this.specificationWrapper = specificationWrapper;
        this.logger = logger;
    }

    /// <inheritdoc cref="IExporter{T}.Create(SpecificationWrapper, ILogger)"/>
    public static BlightTowersExporter Create(SpecificationWrapper specificationWrapper, ILogger logger)
    {
        return new BlightTowersExporter(specificationWrapper, logger);
    }

    /// <inheritdoc/>
    public string Export()
    {
        var recipes = GetBlightTowers();

        var results = LuaConverter.ToLuaString(recipes);

        return results;
    }

    private IReadOnlyList<BlightTower> GetBlightTowers()
    {
        logger.Verbose("running {method}", nameof(GetBlightTowers));
        var results = new List<BlightTower>();
        var specification = specificationWrapper.GetOrCreateSpecification();

        var blightTowersPerLevel = specification.LoadBlightTowersPerLevelRepository();

        foreach (var towerPerLevel in blightTowersPerLevel.Items)
        {
            var tower = towerPerLevel.GetItemForBlightTowersKey() ?? throw new NotImplementedException();

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
