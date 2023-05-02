using PoeExporter.WikiExporters.Lua.Helpers;

namespace PoeExporter.WikiExporters.Lua.Delve;

/// <summary>
/// Class representing values of https://www.poewiki.net/wiki/Module:Delve/delve_resources_per_level.
/// </summary>
[LuaItem]
internal sealed class DelveResourcePerLevel
{
    /// <summary>Gets area level.</summary>
    [LuaPropertyName("area_level")]
    public required int AreaLevel { get; init; }

    /// <summary>Gets sulphite.</summary>
    [LuaPropertyName("sulphite")]
    public required int Sulphite { get; init; }
}
