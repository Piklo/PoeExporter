﻿using PoeExporter.WikiExporters.Lua.Helpers;
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

        var blightTowers = specification.LoadBlightTowersDat();
        var blightTowersPerLevel = specification.LoadBlightTowersPerLevelDat();

        foreach (var towerPerLevel in blightTowersPerLevel)
        {
            var key = towerPerLevel.BlightTowersKey;

            if (key is null)
            {
                logger.Warning("{columnName} is null", nameof(towerPerLevel.BlightTowersKey));
                continue;
            }

            var tower = blightTowers[key.Value];

            var replacedTier = string.IsNullOrEmpty(tower.Tier) ? null : tower.Tier;

            var replacedIcon = tower.Icon.StartsWith("Art/2DArt/UIImages/InGame/Blight/Tower Icons")
                ? $"""File:{tower.Icon.Replace("Art/2DArt/UIImages/InGame/Blight/Tower Icons/Icon", string.Empty)} tower icon.png""" : null;

            var res = new BlightTower()
            {
                Id = tower.Id,
                Name = tower.Name,
                Description = tower.Description,
                Tier = replacedTier,
                Radius = tower.Radius,
                Icon = replacedIcon,
                Cost = towerPerLevel.Cost,
            };

            results.Add(res);
        }

        return results;
    }
}
