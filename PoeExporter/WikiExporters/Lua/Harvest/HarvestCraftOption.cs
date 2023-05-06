using PoeExporter.WikiExporters.Lua.Helpers;

namespace PoeExporter.WikiExporters.Lua.Harvest;

/// <summary>
/// Class representing values of https://www.poewiki.net/wiki/Module:Harvest/harvest_craft_options.
/// </summary>
[LuaItem]
internal sealed class HarvestCraftOption
{
    /// <summary>Gets id.</summary>
    [LuaPropertyName("id")]
    public required string Id { get; init; }

    /// <summary>Gets text.</summary>
    [LuaPropertyName("text")]
    public required string Text { get; init; }

    /// <summary>Gets tier.</summary>
    [LuaPropertyName("tier")]
    public required int Tier { get; init; }

    /// <summary>Gets effect.</summary>
    [LuaPropertyName("effect")]
    public required string Effect { get; init; }

    /// <summary>Gets a value indicating whether is enchant.</summary>
    [LuaPropertyName("is_enchant")]
    public required bool IsEnchant { get; init; }

    /// <summary>Gets cost lifeforce type.</summary>
    [LuaPropertyName("cost_lifeforce_type")]
    public required int CostLifeforceType { get; init; }

    /// <summary>Gets cost lifeforce.</summary>
    [LuaPropertyName("cost_lifeforce")]
    public required int CostLifeforce { get; init; }

    /// <summary>Gets cost sacred.</summary>
    [LuaPropertyName("cost_sacred")]
    public required int CostSacred { get; init; }

    /// <summary>Gets command.</summary>
    [LuaPropertyName("command")]
    public required string Command { get; init; }

    /// <summary>Gets parameters.</summary>
    [LuaPropertyName("parameters")]
    public required IReadOnlyList<string>? Parameters { get; init; }
}
