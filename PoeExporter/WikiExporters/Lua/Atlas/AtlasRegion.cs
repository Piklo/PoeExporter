using PoeExporter.WikiExporters.Lua.Helpers;

namespace PoeExporter.WikiExporters.Lua.Atlas;

/// <summary>
/// Class representing values of https://www.poewiki.net/wiki/Module:Atlas/atlas_regions.
/// </summary>
[LuaItem]
internal sealed class AtlasRegion
{
    /// <summary>Gets id.</summary>
    [LuaPropertyName("id")]
    public required string Id { get; init; }

    /// <summary>Gets name.</summary>
    [LuaPropertyName("name")]
    public required string Name { get; init; }
}
