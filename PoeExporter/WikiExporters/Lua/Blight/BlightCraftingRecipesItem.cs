using PoeExporter.WikiExporters.Lua.Helpers;

namespace PoeExporter.WikiExporters.Lua;

/// <summary>
/// Class representing values of https://www.poewiki.net/wiki/Module:Blight/blight_crafting_recipes_items.
/// </summary>
internal sealed class BlightCraftingRecipesItem : ILuaExporter<BlightCraftingRecipesItem>
{
    /// <summary>Gets Ordinal.</summary>
    [LuaPropertyName("ordinal")]
    public required int Ordinal { get; init; }

    /// <summary>Gets RecipeId.</summary>
    [LuaPropertyName("recipe_id")]
    public required string RecipeId { get; init; }

    /// <summary>Gets ItemId.</summary>
    [LuaPropertyName("item_id")]
    public required string ItemId { get; init; }
}
