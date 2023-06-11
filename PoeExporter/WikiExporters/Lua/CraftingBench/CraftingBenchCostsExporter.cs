using PoeData.Specifications;
using PoeExporter.WikiExporters.Lua.Helpers;

namespace PoeExporter.WikiExporters.Lua.CraftingBench;

/// <summary>
/// Class used to export data for https://www.poewiki.net/wiki/Module:Crafting_bench/crafting_bench_options_costs.
/// </summary>
internal sealed class CraftingBenchCostsExporter : IExporter
{
    private readonly Specification specification;

    /// <inheritdoc/>
    public string PageName { get; } = "crafting_bench_options_costs";

    /// <summary>
    /// Initializes a new instance of the <see cref="CraftingBenchCostsExporter"/> class.
    /// </summary>
    /// <param name="wikiExporterParameters">wiki exporter parameters.</param>
    public CraftingBenchCostsExporter(WikiExporterParameters wikiExporterParameters)
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

    private IReadOnlyList<CraftingBenchCost> GetItems()
    {
        var results = new List<CraftingBenchCost>();

        var benchOptions = specification.LoadCraftingBenchOptionsRepository();

        for (var rowId = 0; rowId < benchOptions.Items.Count; rowId++)
        {
            var option = benchOptions.Items[rowId];
            var baseItems = option.GetItemsForCost_BaseItemTypes();

            for (var i = 0; i < baseItems.Count; i++)
            {
                var baseItem = baseItems[i].Value;

                var obj = new CraftingBenchCost()
                {
                    OptionId = rowId,
                    Name = baseItem.Name,
                    Amount = option.Cost_Values[i],
                };

                results.Add(obj);
            }
        }

        return results;
    }
}
