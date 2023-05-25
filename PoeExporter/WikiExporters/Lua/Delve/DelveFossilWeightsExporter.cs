using PoeData.Specifications.DatFiles;
using PoeExporter.WikiExporters.Lua.Helpers;
using Serilog;

namespace PoeExporter.WikiExporters.Lua.Delve;

/// <summary>
/// Class used to export data for https://www.poewiki.net/wiki/Module:Delve/fossil_weights.
/// </summary>
internal sealed class DelveFossilWeightsExporter : IExporter<DelveFossilWeightsExporter>
{
    private readonly SpecificationWrapper specificationWrapper;
    private readonly ILogger logger;

    /// <inheritdoc/>
    public string PageName { get; } = "fossil_weights";

    /// <summary>
    /// Initializes a new instance of the <see cref="DelveFossilWeightsExporter"/> class.
    /// </summary>
    /// <param name="specificationWrapper">specification wrapper.</param>
    /// <param name="logger">logger.</param>
    public DelveFossilWeightsExporter(SpecificationWrapper specificationWrapper, ILogger logger)
    {
        this.specificationWrapper = specificationWrapper;
        this.logger = logger;
    }

    /// <inheritdoc cref="IExporter{T}.Create(SpecificationWrapper, ILogger)"/>
    public static DelveFossilWeightsExporter Create(SpecificationWrapper specificationWrapper, ILogger logger)
    {
        return new DelveFossilWeightsExporter(specificationWrapper, logger);
    }

    /// <inheritdoc/>
    public string Export()
    {
        var costs = GetDelveFossilWeights();

        var results = LuaConverter.ToLuaString(costs);

        return results;
    }

    private IReadOnlyList<DelveFossilWeight> GetDelveFossilWeights()
    {
        logger.Verbose("running {method}", nameof(GetDelveFossilWeights));
        var results = new List<DelveFossilWeight>();
        var specification = specificationWrapper.GetOrCreateSpecification();

        var delveCraftingMods = specification.LoadDelveCraftingModifiersRepository();
        var baseItems = specification.LoadBaseItemTypesRepository();
        var tags = specification.LoadTagsRepository();

        var additionalDatas = new AdditionalData[]
        {
            new AdditionalData("NegativeWeight", "override"),
            new AdditionalData("Weight", "added"),
        };
        foreach (var modifier in delveCraftingMods.Items)
        {
            foreach (var additionalData in additionalDatas)
            {
                if (modifier.BaseItemTypesKey is null)
                {
                    logger.Warning("modifier with id = {id} has null {column}", nameof(modifier.BaseItemTypesKey));
                    continue;
                }

                var baseItem = baseItems.Items[modifier.BaseItemTypesKey.Value];

                if (baseItem.Id.Contains("RandomFossilOutcome"))
                {
                    continue;
                }

                var (weightKeys, weightValues) = GetWeightKeysAndValues(modifier, additionalData.DataPrefix);

                for (var i = 0; i < weightKeys.Count; i++)
                {
                    var weightKey = weightKeys[i];
                    var weightValue = weightValues[i];
                    var tag = tags.Items[weightKey];

                    var obj = new DelveFossilWeight()
                    {
                        BaseItemId = baseItem.Id,
                        Type = additionalData.DataType,
                        Ordinal = i,
                        Tag = tag.Id,
                        Weight = weightValue,
                    };

                    results.Add(obj);
                }
            }
        }

        return results;
    }

    private readonly record struct AdditionalData(string DataPrefix, string DataType);

    private static (IReadOnlyList<int> keys, IReadOnlyList<int> values) GetWeightKeysAndValues(DelveCraftingModifiersDat modifier, string dataPrefix)
    {
        if (dataPrefix == "NegativeWeight")
        {
            return (modifier.NegativeWeight_TagsKeys, modifier.NegativeWeight_Values);
        }
        else if (dataPrefix == "Weight")
        {
            return (modifier.Weight_TagsKeys, modifier.Weight_Values);
        }

        throw new NotImplementedException($"unknown {nameof(dataPrefix)} = {dataPrefix}");
    }
}
