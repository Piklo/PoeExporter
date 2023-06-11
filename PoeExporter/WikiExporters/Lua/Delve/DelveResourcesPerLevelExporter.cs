using PoeData.Specifications;
using PoeExporter.WikiExporters.Lua.Helpers;

namespace PoeExporter.WikiExporters.Lua.Delve;

/// <summary>
/// Class used to export data for https://www.poewiki.net/wiki/Module:Delve/delve_resources_per_level.
/// </summary>
internal sealed class DelveResourcesPerLevelExporter : IExporter
{
    private readonly Specification specification;

    /// <inheritdoc/>
    public string PageName { get; } = "delve_resources_per_level";

    /// <summary>
    /// Initializes a new instance of the <see cref="DelveResourcesPerLevelExporter"/> class.
    /// </summary>
    /// <param name="wikiExporterParameters">wiki exporter parameters.</param>
    public DelveResourcesPerLevelExporter(WikiExporterParameters wikiExporterParameters)
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

    private IReadOnlyList<DelveResourcePerLevel> GetItems()
    {
        var results = new List<DelveResourcePerLevel>();

        var resourcesPerLevel = specification.LoadDelveResourcePerLevelRepository();

        foreach (var resource in resourcesPerLevel.Items)
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
