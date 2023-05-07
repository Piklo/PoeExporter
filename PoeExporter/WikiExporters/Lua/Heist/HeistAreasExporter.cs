using PoeExporter.WikiExporters.Lua.Helpers;
using Serilog;

namespace PoeExporter.WikiExporters.Lua.Heist;

/// <summary>
/// Class used to export data for https://www.poewiki.net/wiki/Module:Heist/heist_areas.
/// </summary>
internal sealed class HeistAreasExporter : IExporter<HeistAreasExporter>
{
    private readonly SpecificationWrapper specificationWrapper;
    private readonly ILogger logger;

    /// <inheritdoc/>
    public string PageName { get; } = "heist_areas";

    /// <summary>
    /// Initializes a new instance of the <see cref="HeistAreasExporter"/> class.
    /// </summary>
    /// <param name="specificationWrapper">specification wrapper.</param>
    /// <param name="logger">logger.</param>
    public HeistAreasExporter(SpecificationWrapper specificationWrapper, ILogger logger)
    {
        this.specificationWrapper = specificationWrapper;
        this.logger = logger;
    }

    /// <inheritdoc cref="IExporter{T}.Create(SpecificationWrapper, ILogger)"/>
    public static HeistAreasExporter Create(SpecificationWrapper specificationWrapper, ILogger logger)
    {
        return new HeistAreasExporter(specificationWrapper, logger);
    }

    /// <inheritdoc/>
    public string Export()
    {
        var costs = GetHeistAreas();

        var results = LuaConverter.ToLuaString(costs);

        return results;
    }

    private IReadOnlyList<HeistArea> GetHeistAreas()
    {
        logger.Verbose("running {method}", nameof(GetHeistAreas));
        var results = new List<HeistArea>();
        var specification = specificationWrapper.GetOrCreateSpecification();

        var heistAreas = specification.LoadHeistAreasDat();
        var worldAreas = specification.LoadWorldAreasDat();
        var heistJobs = specification.LoadHeistJobsDat();
        var baseItems = specification.LoadBaseItemTypesDat();
        var clientStrings = specification.LoadClientStringsDat();

        foreach (var heistArea in heistAreas)
        {
            var worldAreaKeys = heistArea.WorldAreasKeys;
            var areas = new string[worldAreaKeys.Count];
            for (var i = 0; i < worldAreaKeys.Count; i++)
            {
                var key = worldAreaKeys[i];
                var area = worldAreas[key];
                areas[i] = area.Id;
            }

            var jobKeys = heistArea.HeistJobsKeys;
            var jobs = new string[jobKeys.Count];
            for (var i = 0; i < jobKeys.Count; i++)
            {
                var key = jobKeys[i];
                var area = heistJobs[key];
                jobs[i] = area.Id;
            }

            var contractKey = heistArea.Contract_BaseItemTypesKey;
            if (contractKey is null)
            {
                logger.Warning("{column} is null for {id}", nameof(heistArea.Contract_BaseItemTypesKey), heistArea.Id);
                continue;
            }

            var contract = baseItems[contractKey.Value];

            var blueprintKey = heistArea.Blueprint_BaseItemTypesKey;
            if (blueprintKey is null)
            {
                logger.Warning("{column} is null for {id}", nameof(heistArea.Blueprint_BaseItemTypesKey), heistArea.Id);
                continue;
            }

            var blueprint = baseItems[blueprintKey.Value];

            var rewardKey = heistArea.Reward;
            if (rewardKey is null)
            {
                logger.Warning("{column} is null for {id}", nameof(heistArea.Reward), heistArea.Id);
                continue;
            }

            var reward = clientStrings[rewardKey.Value];

            var obj = new HeistArea()
            {
                Id = heistArea.Id,
                AreaIds = string.Join(",", areas),
                JobIds = string.Join(",", jobs),
                ContractId = contract.Id,
                BluePrintId = blueprint.Id,
                RewardText = reward.Text,
            };

            results.Add(obj);
        }

        return results;
    }
}
