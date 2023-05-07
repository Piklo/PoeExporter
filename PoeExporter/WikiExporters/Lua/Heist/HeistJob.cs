using PoeExporter.WikiExporters.Lua.Helpers;

namespace PoeExporter.WikiExporters.Lua.Heist;

/// <summary>
/// Class representing values of https://www.poewiki.net/wiki/Module:Heist/heist_jobs.
/// </summary>
[LuaItem]
internal sealed class HeistJob
{
    /// <summary>Gets id.</summary>
    [LuaPropertyName("id")]
    public required string Id { get; init; }

    /// <summary>Gets name.</summary>
    [LuaPropertyName("name")]
    public required string Name { get; init; }
}
