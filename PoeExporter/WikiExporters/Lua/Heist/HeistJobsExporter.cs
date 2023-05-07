using PoeExporter.WikiExporters.Lua.Helpers;
using Serilog;

namespace PoeExporter.WikiExporters.Lua.Heist;

/// <summary>
/// Class used to export data for https://www.poewiki.net/wiki/Module:Heist/heist_jobs.
/// </summary>
internal sealed class HeistJobsExporter : IExporter<HeistJobsExporter>
{
    private readonly SpecificationWrapper specificationWrapper;
    private readonly ILogger logger;

    /// <inheritdoc/>
    public string PageName { get; } = "heist_jobs";

    /// <summary>
    /// Initializes a new instance of the <see cref="HeistJobsExporter"/> class.
    /// </summary>
    /// <param name="specificationWrapper">specification wrapper.</param>
    /// <param name="logger">logger.</param>
    public HeistJobsExporter(SpecificationWrapper specificationWrapper, ILogger logger)
    {
        this.specificationWrapper = specificationWrapper;
        this.logger = logger;
    }

    /// <inheritdoc cref="IExporter{T}.Create(SpecificationWrapper, ILogger)"/>
    public static HeistJobsExporter Create(SpecificationWrapper specificationWrapper, ILogger logger)
    {
        return new HeistJobsExporter(specificationWrapper, logger);
    }

    /// <inheritdoc/>
    public string Export()
    {
        var results = GetHeistJobs();

        var str = LuaConverter.ToLuaString(results);

        return str;
    }

    private IReadOnlyList<HeistJob> GetHeistJobs()
    {
        logger.Verbose("running {method}", nameof(GetHeistJobs));
        var results = new List<HeistJob>();
        var specification = specificationWrapper.GetOrCreateSpecification();

        var jobs = specification.LoadHeistJobsDat();

        foreach (var job in jobs)
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
