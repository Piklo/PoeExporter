using PoeExporter.WikiExporters.Lua.Helpers;

namespace PoeExporter.WikiExporters.Lua.CraftingBench;

/// <summary>
/// Class representing values of https://www.poewiki.net/wiki/Module:Crafting_bench/crafting_bench_options_costs.
/// </summary>
[LuaItem]
internal sealed class CraftingBenchCost
{
    /// <summary>Gets option id.</summary>
    [LuaPropertyName("option_id")]
    public required int OptionId { get; init; }

    /// <summary>Gets name.</summary>
    [LuaPropertyName("name")]
    public required string Name { get; init; }

    /// <summary>Gets amount.</summary>
    [LuaPropertyName("amount")]
    public required int Amount { get; init; }
}
