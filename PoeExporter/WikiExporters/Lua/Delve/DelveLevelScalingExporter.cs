using PoeExporter.WikiExporters.Lua.Helpers;
using Serilog;

namespace PoeExporter.WikiExporters.Lua.Delve;

/// <summary>
/// Class used to export data for https://www.poewiki.net/wiki/Module:Delve/delve_level_scaling.
/// </summary>
internal sealed class DelveLevelScalingExporter : IExporter<DelveLevelScalingExporter>
{
    private readonly SpecificationWrapper specificationWrapper;
    private readonly ILogger logger;

    /// <inheritdoc/>
    public string PageName { get; } = "delve_level_scaling";

    /// <summary>
    /// Initializes a new instance of the <see cref="DelveLevelScalingExporter"/> class.
    /// </summary>
    /// <param name="specificationWrapper">specification wrapper.</param>
    /// <param name="logger">logger.</param>
    public DelveLevelScalingExporter(SpecificationWrapper specificationWrapper, ILogger logger)
    {
        this.specificationWrapper = specificationWrapper;
        this.logger = logger;
    }

    /// <inheritdoc cref="IExporter{T}.Create(SpecificationWrapper, ILogger)"/>
    public static DelveLevelScalingExporter Create(SpecificationWrapper specificationWrapper, ILogger logger)
    {
        return new DelveLevelScalingExporter(specificationWrapper, logger);
    }

    /// <inheritdoc/>
    public string Export()
    {
        var costs = GetDelveLevelScaling();

        var results = LuaConverter.ToLuaString(costs);

        return results;
    }

    private IReadOnlyList<DelveLevelScale> GetDelveLevelScaling()
    {
        logger.Verbose("running {method}", nameof(GetDelveLevelScaling));
        var results = new List<DelveLevelScale>();
        var specification = specificationWrapper.GetOrCreateSpecification();

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
