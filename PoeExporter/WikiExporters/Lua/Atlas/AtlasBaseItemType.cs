using PoeExporter.WikiExporters.Lua.Helpers;

namespace PoeExporter.WikiExporters.Lua.Atlas;

/// <summary>
/// Class representing values of https://www.poewiki.net/wiki/Module:Atlas/atlas_base_item_types.
/// </summary>
[LuaItem]
internal sealed class AtlasBaseItemType
{
    /// <summary>Gets region id.</summary>
    [LuaPropertyName("region_id")]
    public required string RegionId { get; init; }

    /// <summary>Gets min tier.</summary>
    [LuaPropertyName("tier_min")]
    public required int TierMin { get; init; }

    /// <summary>Gets max tier.</summary>
    [LuaPropertyName("tier_max")]
    public required int TierMax { get; init; }

    /// <summary>Gets tag.</summary>
    [LuaPropertyName("tag")]
    public required string Tag { get; init; }

    /// <summary>Gets weight.</summary>
    [LuaPropertyName("weight")]
    public required int Weight { get; init; }
}
