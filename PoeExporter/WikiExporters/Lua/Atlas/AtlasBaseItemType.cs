using PoeExporter.WikiExporters.Lua.Helpers;

namespace PoeExporter.WikiExporters.Lua.Atlas;

/// <summary>
/// Class representing values of https://www.poewiki.net/wiki/Module:Atlas/atlas_base_item_types.
/// </summary>
internal sealed class AtlasBaseItemType : ILuaExporter<AtlasBaseItemType>
{
    /// <summary>Gets region id.</summary>
    [LuaPropertyName("region_id")]
    public required string RegionId { get; init; }

    /// <summary>Gets name.</summary>
    [LuaPropertyName("name")]
    public required int TierMin { get; init; }

    /// <summary>Gets name.</summary>
    [LuaPropertyName("name")]
    public required int TierMax { get; init; }

    /// <summary>Gets name.</summary>
    [LuaPropertyName("name")]
    public required string Tag { get; init; }

    /// <summary>Gets name.</summary>
    [LuaPropertyName("name")]
    public required int Weight { get; init; }
}
