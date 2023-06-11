using PoeData.Specifications;
using PoeExporter.WikiExporters.Lua.Helpers;

namespace PoeExporter.WikiExporters.Lua.Heist;

/// <summary>
/// Class used to export data for https://www.poewiki.net/wiki/Module:Heist/heist_jobs.
/// </summary>
internal sealed class HeistJobsExporter : IExporter
{
    private readonly Specification specification;

    /// <inheritdoc/>
    public string PageName { get; } = "heist_jobs";

    /// <summary>
    /// Initializes a new instance of the <see cref="HeistJobsExporter"/> class.
    /// </summary>
    /// <param name="wikiExporterParameters">wiki exporter parameters.</param>
    public HeistJobsExporter(WikiExporterParameters wikiExporterParameters)
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

    private IReadOnlyList<HeistJob> GetItems()
    {
        var results = new List<HeistJob>();

        var jobs = specification.LoadHeistJobsRepository();

        foreach (var job in jobs.Items)
        {
            var obj = new HeistJob()
            {
                Id = job.Id,
                Name = job.Name,
            };

            results.Add(obj);
        }

        return results;
    }
}
