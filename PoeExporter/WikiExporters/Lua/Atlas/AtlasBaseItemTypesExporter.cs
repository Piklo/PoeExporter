using PoeExporter.WikiExporters.Lua.Helpers;
using Serilog;

namespace PoeExporter.WikiExporters.Lua.Atlas;

/// <summary>
/// Class used to export data for https://www.poewiki.net/wiki/Module:Atlas/atlas_base_item_types.
/// </summary>
internal sealed class AtlasBaseItemTypesExporter : IExporter<AtlasBaseItemTypesExporter>
{
    private readonly SpecificationWrapper specificationWrapper;
    private readonly ILogger logger;

    /// <inheritdoc/>
    public string PageName { get; } = "atlas_regions";

    /// <summary>
    /// Initializes a new instance of the <see cref="AtlasBaseItemTypesExporter"/> class.
    /// </summary>
    /// <param name="specificationWrapper">specification wrapper.</param>
    /// <param name="logger">logger.</param>
    public AtlasBaseItemTypesExporter(SpecificationWrapper specificationWrapper, ILogger logger)
    {
        this.specificationWrapper = specificationWrapper;
        this.logger = logger;
    }

    /// <inheritdoc cref="IExporter{T}.Create(SpecificationWrapper, ILogger)"/>
    public static AtlasBaseItemTypesExporter Create(SpecificationWrapper specificationWrapper, ILogger logger)
    {
        return new AtlasBaseItemTypesExporter(specificationWrapper, logger);
    }

    /// <inheritdoc/>
    public string Export()
    {
        var regions = GetAtlasBaseItemTypes();

        var results = LuaConverter.ToLuaString(regions);

        return results;
    }

    private IReadOnlyList<AtlasBaseItemType> GetAtlasBaseItemTypes()
    {
        logger.Verbose("running {method}", nameof(GetAtlasBaseItemTypes));
        var results = new List<AtlasBaseItemType>();
        var specification = specificationWrapper.GetOrCreateSpecification();

        var atlasBaseTypeDrops = specification.LoadAtlasBaseTypeDropsDat();

        foreach (var atlasBaseTypeDrop in atlasBaseTypeDrops)
        {
            for (var i = 0; i < atlasBaseTypeDrop.SpawnWeight_TagsKeys.Count; i++)
            {
                var key = atlasBaseTypeDrop.SpawnWeight_TagsKeys[i];

                // not finished because its depracted
                // var obj = new AtlasBaseItemType()
                // {
                //    RegionId = atlasBaseTypeDrop.Id,
                //    TierMin = atlasBaseTypeDrop.MinTier,
                //    TierMax = atlasBaseTypeDrop.MaxTier,
                // };
                // results.Add(obj);
            }
        }

        return results;
    }
}
