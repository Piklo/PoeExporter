using PoeExporter.WikiExporters.Lua.Helpers;

namespace PoeExporter.WikiExporters.Lua.Delve;

/// <summary>
/// Class representing values of https://www.poewiki.net/wiki/Module:Delve/delve_upgrade_stats.
/// </summary>
[LuaItem]
internal sealed class DelveUpgradeStat
{
    /// <summary>Gets type.</summary>
    [LuaPropertyName("type")]
    public required string Type { get; init; }

    /// <summary>Gets level.</summary>
    [LuaPropertyName("level")]
    public required int Level { get; init; }

    /// <summary>Gets id.</summary>
    [LuaPropertyName("id")]
    public required string Id { get; init; }

    /// <summary>Gets value.</summary>
    [LuaPropertyName("value")]
    public required int Value { get; init; }
}
