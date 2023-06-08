using PoeExporter.WikiExporters.Lua.Helpers;
using Serilog;

namespace PoeExporter.WikiExporters.Lua.CraftingBench;

/// <summary>
/// Class used to export data for https://www.poewiki.net/wiki/Module:Crafting_bench/crafting_bench_options_costs.
/// </summary>
internal sealed class CraftingBenchCostsExporter : IExporter<CraftingBenchCostsExporter>
{
    private readonly SpecificationWrapper specificationWrapper;
    private readonly ILogger logger;

    /// <inheritdoc/>
    public string PageName { get; } = "crafting_bench_options_costs";

    /// <summary>
    /// Initializes a new instance of the <see cref="CraftingBenchCostsExporter"/> class.
    /// </summary>
    /// <param name="specificationWrapper">specification wrapper.</param>
    /// <param name="logger">logger.</param>
    public CraftingBenchCostsExporter(SpecificationWrapper specificationWrapper, ILogger logger)
    {
        this.specificationWrapper = specificationWrapper;
        this.logger = logger;
    }

    /// <inheritdoc cref="IExporter{T}.Create(SpecificationWrapper, ILogger)"/>
    public static CraftingBenchCostsExporter Create(SpecificationWrapper specificationWrapper, ILogger logger)
    {
        return new CraftingBenchCostsExporter(specificationWrapper, logger);
    }

    /// <inheritdoc/>
    public string Export()
    {
        var costs = GetCraftingBenchCosts();

        var results = LuaConverter.ToLuaString(costs);

        return results;
    }

    private IReadOnlyList<CraftingBenchCost> GetCraftingBenchCosts()
    {
        logger.Verbose("running {method}", nameof(GetCraftingBenchCosts));
        var results = new List<CraftingBenchCost>();
        var specification = specificationWrapper.GetOrCreateSpecification();

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
