using PoeData.Specifications;
using PoeExporter.WikiExporters.Lua.Helpers;

namespace PoeExporter.WikiExporters.Lua.Heist;

/// <summary>
/// Class used to export data for https://www.poewiki.net/wiki/Module:Heist/heist_areas.
/// </summary>
internal sealed class HeistAreasExporter : IExporter
{
    private readonly Specification specification;

    /// <inheritdoc/>
    public string PageName { get; } = "heist_areas";

    /// <summary>
    /// Initializes a new instance of the <see cref="HeistAreasExporter"/> class.
    /// </summary>
    /// <param name="wikiExporterParameters">wiki exporter parameters.</param>
    public HeistAreasExporter(WikiExporterParameters wikiExporterParameters)
    {
        specification = wikiExporterParameters.SpecificationWrapper.GetOrCreateSpecification();
    }

    /// <inheritdoc/>
    public string Export()
    {
        var costs = GetHeistAreas();

        var str = LuaConverter.ToLuaString(costs);

        return str;
    }

    private IReadOnlyList<HeistArea> GetHeistAreas()
    {
        var results = new List<HeistArea>();

        var heistAreas = specification.LoadHeistAreasRepository();

        foreach (var heistArea in heistAreas.Items)
        {
            var worldAreas = heistArea.GetItemsForWorldAreasKeys();
            var areas = new string[worldAreas.Count];
            for (var i = 0; i < worldAreas.Count; i++)
            {
                var worldArea = worldAreas[i].Value;
                areas[i] = worldArea.Id;
            }

            var heistJobs = heistArea.GetItemsForHeistJobsKeys();
            var jobs = new string[heistJobs.Count];
            for (var i = 0; i < heistJobs.Count; i++)
            {
                var heistJob = heistJobs[i].Value;
                jobs[i] = heistJob.Id;
            }

            var contract = heistArea.GetItemForContract_BaseItemTypesKey() ?? throw new NullItemException();

            var blueprint = heistArea.GetItemForBlueprint_BaseItemTypesKey() ?? throw new NullItemException();

            var reward = heistArea.GetItemForReward() ?? throw new NullItemException();

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
