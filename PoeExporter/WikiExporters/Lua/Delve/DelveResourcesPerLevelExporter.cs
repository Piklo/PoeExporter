using PoeExporter.WikiExporters.Lua.Helpers;
using Serilog;

namespace PoeExporter.WikiExporters.Lua.Delve;

/// <summary>
/// Class used to export data for https://www.poewiki.net/wiki/Module:Delve/delve_resources_per_level.
/// </summary>
internal sealed class DelveResourcesPerLevelExporter : IExporter<DelveResourcesPerLevelExporter>
{
    private readonly SpecificationWrapper specificationWrapper;
    private readonly ILogger logger;

    /// <inheritdoc/>
    public string PageName { get; } = "delve_resources_per_level";

    /// <summary>
    /// Initializes a new instance of the <see cref="DelveResourcesPerLevelExporter"/> class.
    /// </summary>
    /// <param name="specificationWrapper">specification wrapper.</param>
    /// <param name="logger">logger.</param>
    public DelveResourcesPerLevelExporter(SpecificationWrapper specificationWrapper, ILogger logger)
    {
        this.specificationWrapper = specificationWrapper;
        this.logger = logger;
    }

    /// <inheritdoc cref="IExporter{T}.Create(SpecificationWrapper, ILogger)"/>
    public static DelveResourcesPerLevelExporter Create(SpecificationWrapper specificationWrapper, ILogger logger)
    {
        return new DelveResourcesPerLevelExporter(specificationWrapper, logger);
    }

    /// <inheritdoc/>
    public string Export()
    {
        var costs = GetResourcesPerLevel();

        var results = LuaConverter.ToLuaString(costs);

        return results;
    }

    private IReadOnlyList<DelveResourcePerLevel> GetResourcesPerLevel()
    {
        logger.Verbose("running {method}", nameof(GetResourcesPerLevel));
        var results = new List<DelveResourcePerLevel>();
        var specification = specificationWrapper.GetOrCreateSpecification();

        var resourcesPerLevel = specification.LoadDelveResourcePerLevelDat();

        foreach (var resource in resourcesPerLevel)
        {
            var obj = new DelveResourcePerLevel()
            {
                AreaLevel = resource.AreaLevel,
                Sulphite = resource.Sulphite,
            };

            results.Add(obj);
        }

        return results;
    }
}
