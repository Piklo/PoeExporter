using PoeExporter.WikiExporters.Lua.Helpers;

namespace PoeExporter.WikiExporters.Lua.Blight;

/// <summary>
/// Class representing values of https://www.poewiki.net/wiki/Module:Blight/blight_towers.
/// </summary>
internal class BlightTowers : ILuaExporter<BlightTowers>
{
    /// <inheritdoc />
    public string PageName { get; } = "blight_towers";

    /// <summary>Gets id.</summary>
    [LuaPropertyName("id")]
    public required string Id { get; init; }

    /// <summary>Gets name.</summary>
    [LuaPropertyName("name")]
    public required string Name { get; init; }

    /// <summary>Gets description.</summary>
    [LuaPropertyName("description")]
    public required string Description { get; init; }

    /// <summary>Gets tier.</summary>
    [LuaPropertyName("tier")]
    public required string? Tier { get; init; }

    /// <summary>Gets radius.</summary>
    [LuaPropertyName("radius")]
    public required int Radius { get; init; }

    /// <summary>Gets icon.</summary>
    [LuaPropertyName("icon")]
    public required string? Icon { get; init; }

    /// <summary>Gets cost.</summary>
    [LuaPropertyName("cost")]
    public required int Cost { get; init; }
}
