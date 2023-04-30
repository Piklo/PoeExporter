using PoeExporter.WikiExporters.Lua.Helpers;

namespace PoeExporter.WikiExporters.Lua.CraftingBench;

/// <summary>
/// Class representing values of https://www.poewiki.net/wiki/Module:Crafting_bench/crafting_bench_options.
/// </summary>
[LuaItem]
internal sealed class CraftingBenchOption
{
    /// <summary>Gets npc.</summary>
    [LuaPropertyName("npc")]
    public required string? Npc { get; init; }

    /// <summary>Gets ordinal.</summary>
    [LuaPropertyName("ordinal")]
    public required int Ordinal { get; init; }

    /// <summary>Gets mod id.</summary>
    [LuaPropertyName("mod_id")]
    public required string? ModId { get; init; }

    /// <summary>Gets required level.</summary>
    [LuaPropertyName("required_level")]
    public required int? RequiredLevel { get; init; }

    /// <summary>Gets name.</summary>
    [LuaPropertyName("name")]
    public required string? Name { get; init; }

    /// <summary>Gets item classes.</summary>
    [LuaPropertyName("item_classes")]
    public required IReadOnlyList<string> ItemClasses { get; init; }

    /// <summary>Gets item classes ids.</summary>
    [LuaPropertyName("item_classes_ids")]
    public required IReadOnlyList<string> ItemClassesIds { get; init; }

    /// <summary>Gets links.</summary>
    [LuaPropertyName("links")]
    public required int? Links { get; init; }

    /// <summary>Gets socket colours.</summary>
    [LuaPropertyName("socket_colours")]
    public required string? SocketColors { get; init; }

    /// <summary>Gets sockets.</summary>
    [LuaPropertyName("sockets")]
    public required int? Sockets { get; init; }

    /// <summary>Gets description.</summary>
    [LuaPropertyName("description")]
    public required string? Description { get; init; }

    /// <summary>Gets recipe unlock location.</summary>
    [LuaPropertyName("recipe_unlock_location")]
    public required string? RecipeUnlockLocation { get; init; }

    /// <summary>Gets rank.</summary>
    [LuaPropertyName("rank")]
    public required int Rank { get; init; }

    /// <summary>Gets item class categories.</summary>
    [LuaPropertyName("item_class_categories")]
    public required IReadOnlyList<string> ItemClassCategories { get; init; }

    /// <summary>Gets unveils required.</summary>
    [LuaPropertyName("unveils_required")]
    public required int? UnveilsRequired { get; init; }

    /// <summary>Gets affix type.</summary>
    [LuaPropertyName("affix_type")]
    public required string AffixType { get; init; }

    /// <summary>Gets id.</summary>
    [LuaPropertyName("id")]
    public required int Id { get; init; }
}
