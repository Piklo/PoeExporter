using PoeExporter.WikiExporters.Lua.Helpers;

namespace PoeExporter.WikiExporters.Lua.Heist;

/// <summary>
/// Class representing values of https://www.poewiki.net/wiki/Module:Heist/heist_areas.
/// </summary>
[LuaItem]
internal sealed class HeistArea
{
    /// <summary>Gets id.</summary>
    [LuaPropertyName("id")]
    public required string Id { get; init; }

    /// <summary>Gets area ids.</summary>
    [LuaPropertyName("area_ids")]
    public required string AreaIds { get; init; }

    /// <summary>Gets job ids.</summary>
    [LuaPropertyName("job_ids")]
    public required string JobIds { get; init; }

    /// <summary>Gets contract id.</summary>
    [LuaPropertyName("contract_id")]
    public required string ContractId { get; init; }

    /// <summary>Gets blueprint id.</summary>
    [LuaPropertyName("blueprint_id")]
    public required string BluePrintId { get; init; }

    /// <summary>Gets reward text.</summary>
    [LuaPropertyName("reward_text")]
    public required string RewardText { get; init; }
}
