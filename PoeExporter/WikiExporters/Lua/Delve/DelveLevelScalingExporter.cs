using PoeData.Specifications;
using PoeExporter.WikiExporters.Lua.Helpers;

namespace PoeExporter.WikiExporters.Lua.Delve;

/// <summary>
/// Class used to export data for https://www.poewiki.net/wiki/Module:Delve/delve_level_scaling.
/// </summary>
internal sealed class DelveLevelScalingExporter : IExporter
{
    private readonly Specification specification;

    /// <inheritdoc/>
    public string PageName { get; } = "delve_level_scaling";

    /// <summary>
    /// Initializes a new instance of the <see cref="DelveLevelScalingExporter"/> class.
    /// </summary>
    /// <param name="wikiExporterParameters">wiki exporter parameters.</param>
    public DelveLevelScalingExporter(WikiExporterParameters wikiExporterParameters)
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

    private IReadOnlyList<DelveLevelScale> GetItems()
    {
        var results = new List<DelveLevelScale>();

        var delveScaling = specification.LoadDelveLevelScalingRepository();

        foreach (var delveScale in delveScaling.Items)
        {
            var obj = new DelveLevelScale()
            {
                Depth = delveScale.Depth,
                MonsterLevel = delveScale.MonsterLevel,
                SulphiteCost = delveScale.SulphiteCost,
                DarknessResistance = delveScale.DarknessResistance,
                LightRadius = delveScale.LightRadius,
                MonsterLife = delveScale.MoreMonsterLife,
                MonsterDamage = delveScale.MoreMonsterDamage,
            };

            results.Add(obj);
        }

        return results;
    }
}
