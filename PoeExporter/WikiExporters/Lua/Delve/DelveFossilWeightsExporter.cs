using PoeData.Specifications;
using PoeData.Specifications.DatFiles;
using PoeExporter.WikiExporters.Lua.Helpers;

namespace PoeExporter.WikiExporters.Lua.Delve;

/// <summary>
/// Class used to export data for https://www.poewiki.net/wiki/Module:Delve/fossil_weights.
/// </summary>
internal sealed class DelveFossilWeightsExporter : IExporter
{
    private readonly Specification specification;

    /// <inheritdoc/>
    public string PageName { get; } = "fossil_weights";

    /// <summary>
    /// Initializes a new instance of the <see cref="DelveFossilWeightsExporter"/> class.
    /// </summary>
    /// <param name="wikiExporterParameters">wiki exporter parameters.</param>
    public DelveFossilWeightsExporter(WikiExporterParameters wikiExporterParameters)
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

    private IReadOnlyList<DelveFossilWeight> GetItems()
    {
        var results = new List<DelveFossilWeight>();

        var delveCraftingMods = specification.LoadDelveCraftingModifiersRepository();
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
                var baseItem = modifier.GetItemForBaseItemTypesKey() ?? throw new NullItemException();

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
