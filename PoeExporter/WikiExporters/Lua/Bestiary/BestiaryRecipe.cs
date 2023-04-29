using PoeExporter.WikiExporters.Lua.Helpers;

namespace PoeExporter.WikiExporters.Lua.Bestiary;

/// <summary>
/// Class representing values of https://www.poewiki.net/wiki/Module:Bestiary/recipes.
/// </summary>
internal sealed class BestiaryRecipe : ILuaExporter<BestiaryRecipe>
{
    /// <summary>Gets id.</summary>
    [LuaPropertyName("id")]
    public required string Id { get; init; }

    /// <summary>Gets header.</summary>
    [LuaPropertyName("header")]
    public required int Header { get; init; }

    /// <summary>Gets subheader.</summary>
    [LuaPropertyName("subheader")]
    public required string Subheader { get; init; }

    /// <summary>Gets notes.</summary>
    [LuaPropertyName("notes")]
    public required string? Notes { get; init; }
}
