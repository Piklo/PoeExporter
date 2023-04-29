using PoeExporter.WikiExporters.Lua.Helpers;
using Serilog;

namespace PoeExporter.WikiExporters.Lua.Atlas;

/// <summary>
/// Class used to export data for https://www.poewiki.net/wiki/Module:Atlas/atlas_regions.
/// </summary>
internal sealed class AtlasRegionsExporter : IExporter<AtlasRegionsExporter>
{
    private readonly SpecificationWrapper specificationWrapper;
    private readonly ILogger logger;

    /// <inheritdoc/>
    public string PageName { get; } = "atlas_base_item_types";

    /// <summary>
    /// Initializes a new instance of the <see cref="AtlasRegionsExporter"/> class.
    /// </summary>
    /// <param name="specificationWrapper">specification wrapper.</param>
    /// <param name="logger">logger.</param>
    public AtlasRegionsExporter(SpecificationWrapper specificationWrapper, ILogger logger)
    {
        this.specificationWrapper = specificationWrapper;
        this.logger = logger;
    }

    /// <inheritdoc cref="IExporter{T}.Create(SpecificationWrapper, ILogger)"/>
    public static AtlasRegionsExporter Create(SpecificationWrapper specificationWrapper, ILogger logger)
    {
        return new AtlasRegionsExporter(specificationWrapper, logger);
    }

    /// <inheritdoc/>
    public string Export()
    {
        var regions = GetAtlasRegions();

        var results = LuaConverter.ToLuaString(regions);

        return results;
    }

    private IReadOnlyList<AtlasRegion> GetAtlasRegions()
    {
        logger.Verbose("running {method}", nameof(GetAtlasRegions));
        var results = new List<AtlasRegion>();
        var specification = specificationWrapper.GetOrCreateSpecification();

        var atlasRegions = specification.LoadAtlasRegionsDat();

        foreach (var region in atlasRegions)
        {
            var obj = new AtlasRegion()
            {
                Id = region.Id,
                Name = region.Name,
            };

            results.Add(obj);
        }

        return results;
    }
}
