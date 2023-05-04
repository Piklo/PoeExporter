using PoeExporter.WikiExporters.Lua.Helpers;

namespace PoeExporter.WikiExporters.Lua.Delve;

/// <summary>
/// Class representing values of https://www.poewiki.net/wiki/Module:Delve/fossil_weights.
/// </summary>
[LuaItem]
internal sealed class DelveFossilWeight
{
    /// <summary>Gets base item id.</summary>
    [LuaPropertyName("base_item_id")]
    public required string BaseItemId { get; init; }

    /// <summary>Gets type.</summary>
    [LuaPropertyName("type")]
    public required string Type { get; init; }

    /// <summary>Gets ordinal.</summary>
    [LuaPropertyName("ordinal")]
    public required int Ordinal { get; init; }

    /// <summary>Gets tag.</summary>
    [LuaPropertyName("tag")]
    public required string Tag { get; init; }

    /// <summary>Gets weight.</summary>
    [LuaPropertyName("weight")]
    public required int Weight { get; init; }
}
