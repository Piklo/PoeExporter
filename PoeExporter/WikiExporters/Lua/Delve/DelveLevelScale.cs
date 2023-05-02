using PoeExporter.WikiExporters.Lua.Helpers;

namespace PoeExporter.WikiExporters.Lua.Delve;

/// <summary>
/// Class representing values of https://www.poewiki.net/wiki/Module:Delve/delve_level_scaling.
/// </summary>
[LuaItem]
internal sealed class DelveLevelScale
{
    /// <summary>Gets depth.</summary>
    [LuaPropertyName("depth")]
    public required int Depth { get; init; }

    /// <summary>Gets monster level.</summary>
    [LuaPropertyName("monster_level")]
    public required int MonsterLevel { get; init; }

    /// <summary>Gets sulphite cost.</summary>
    [LuaPropertyName("sulphite_cost")]
    public required int SulphiteCost { get; init; }

    /// <summary>Gets darkness resistance.</summary>
    [LuaPropertyName("darkness_resistance")]
    public required int DarknessResistance { get; init; }

    /// <summary>Gets light radius.</summary>
    [LuaPropertyName("light_radius")]
    public required int LightRadius { get; init; }

    /// <summary>Gets monster life.</summary>
    [LuaPropertyName("monster_life")]
    public required int MonsterLife { get; init; }

    /// <summary>Gets monster damage.</summary>
    [LuaPropertyName("monster_damage")]
    public required int MonsterDamage { get; init; }
}
