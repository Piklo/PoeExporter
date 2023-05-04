using PoeExporter.WikiExporters.Lua.Helpers;

namespace PoeExporter.WikiExporters.Lua.Delve;

/// <summary>
/// Class representing values of https://www.poewiki.net/wiki/Module:Delve/fossils.
/// </summary>
[LuaItem]
internal sealed class DelveFossil
{
    /// <summary>Gets base item id.</summary>
    [LuaPropertyName("base_item_id")]
    public required string BaseItemId { get; init; }

    /// <summary>Gets added modifier ids.</summary>
    [LuaPropertyName("added_modifier_ids")]
    public required IReadOnlyList<string> AddedModifierIds { get; init; }

    /// <summary>Gets forced modifier ids.</summary>
    [LuaPropertyName("forced_modifier_ids")]
    public required IReadOnlyList<string> ForcedModifierIds { get; init; }

    /// <summary>Gets sell price modifier ids.</summary>
    [LuaPropertyName("sell_price_modifier_ids")]
    public required IReadOnlyList<string> SellPriceModifierIds { get; init; }

    /// <summary>Gets forbidden tags.</summary>
    [LuaPropertyName("forbidden_tags")]
    public required IReadOnlyList<string> ForbiddenTags { get; init; }

    /// <summary>Gets allowed tags.</summary>
    [LuaPropertyName("allowed_tags")]
    public required IReadOnlyList<string> AllowedTags { get; init; }

    /// <summary>Gets corrupted essence chance.</summary>
    [LuaPropertyName("corrupted_essence_chance")]
    public required int CorruptedEssenceChance { get; init; }

    /// <summary>Gets a value indicating whether can mirror.</summary>
    [LuaPropertyName("can_mirror")]
    public required bool CanMirror { get; init; }

    /// <summary>Gets a value indicating whether can enchant.</summary>
    [LuaPropertyName("can_enchant")]
    public required bool CanEnchant { get; init; }

    /// <summary>Gets a value indicating whether can quality.</summary>
    [LuaPropertyName("can_quality")]
    public required bool CanQuality { get; init; }

    /// <summary>Gets a value indicating whether can roll white sockets.</summary>
    [LuaPropertyName("can_roll_white_sockets")]
    public required bool CanRollWhiteSockets { get; init; }

    /// <summary>Gets a value indicating whether is lucky.</summary>
    [LuaPropertyName("is_lucky")]
    public required bool IsLucky { get; init; }
}
