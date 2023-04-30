using PoeExporter.WikiExporters.Lua.Helpers;

namespace PoeExporter.WikiExporters.Lua.Bestiary;

/// <summary>
/// Class representing values of https://www.poewiki.net/wiki/Module:Bestiary/components.
/// </summary>
[LuaItem]
internal class BestiaryComponent
{
    /// <summary>Gets id.</summary>
    [LuaPropertyName("id")]
    public required string Id { get; init; }

    /// <summary>Gets min level.</summary>
    [LuaPropertyName("min_level")]
    public required int MinLevel { get; init; }

    /// <summary>Gets monster.</summary>
    [LuaPropertyName("monster")]
    public required string? Monster { get; init; }

    /// <summary>Gets family.</summary>
    [LuaPropertyName("family")]
    public required string? Family { get; init; }

    /// <summary>Gets beast group.</summary>
    [LuaPropertyName("beast_group")]
    public required string? BeastGroup { get; init; }

    /// <summary>Gets genus.</summary>
    [LuaPropertyName("genus")]
    public required string? Genus { get; init; }

    /// <summary>Gets mod id.</summary>
    [LuaPropertyName("mod_id")]
    public required string? ModId { get; init; }

    /// <summary>Gets rarity.</summary>
    [LuaPropertyName("rarity")]
    public required string? Rarity { get; init; }
}
