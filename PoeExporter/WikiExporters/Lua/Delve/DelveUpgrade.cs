using PoeExporter.WikiExporters.Lua.Helpers;

namespace PoeExporter.WikiExporters.Lua.Delve;

/// <summary>
/// Class representing values of https://www.poewiki.net/wiki/Module:Delve/delve_upgrades.
/// </summary>
[LuaItem]
internal sealed class DelveUpgrade
{
    /// <summary>Gets type.</summary>
    [LuaPropertyName("type")]
    public required string Type { get; init; }

    /// <summary>Gets level.</summary>
    [LuaPropertyName("level")]
    public required int Level { get; init; }

    /// <summary>Gets cost.</summary>
    [LuaPropertyName("cost")]
    public required int Cost { get; init; }
}
