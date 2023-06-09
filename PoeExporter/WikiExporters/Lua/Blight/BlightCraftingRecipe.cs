﻿using PoeExporter.WikiExporters.Lua.Helpers;

namespace PoeExporter.WikiExporters.Lua.Blight;

/// <summary>
/// Class representing values of https://www.poewiki.net/wiki/Module:Blight/blight_crafting_recipes.
/// </summary>
[LuaItem]
internal sealed class BlightCraftingRecipe
{
    /// <summary>Gets id.</summary>
    [LuaPropertyName("id")]
    public required string Id { get; init; }

    /// <summary>Gets passive id.</summary>
    [LuaPropertyName("passive_id")]
    public required string? PassiveId { get; init; }

    /// <summary>Gets modifier id.</summary>
    [LuaPropertyName("modifier_id")]
    public required string? ModifierId { get; init; }

    /// <summary>Gets type.</summary>
    [LuaPropertyName("type")]
    public required string Type { get; init; }
}
