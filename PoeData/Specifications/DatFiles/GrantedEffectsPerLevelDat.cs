// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.DatFiles;

/// <summary>
/// Class containing GrantedEffectsPerLevel.dat data.
/// </summary>
public sealed partial class GrantedEffectsPerLevelDat
{
    /// <summary> Gets GrantedEffect.</summary>
    /// <remarks> references <see cref="GrantedEffectsDat"/> on <see cref="Specification.LoadGrantedEffectsDat"/> index.</remarks>
    public required int? GrantedEffect { get; init; }

    /// <summary> Gets Level.</summary>
    public required int Level { get; init; }

    /// <summary> Gets PlayerLevelReq.</summary>
    public required float PlayerLevelReq { get; init; }

    /// <summary> Gets CostMultiplier.</summary>
    public required int CostMultiplier { get; init; }

    /// <summary> Gets StoredUses.</summary>
    public required int StoredUses { get; init; }

    /// <summary> Gets Cooldown.</summary>
    public required int Cooldown { get; init; }

    /// <summary> Gets CooldownBypassType.</summary>
    /// <remarks> references <see cref="CooldownBypassTypesDat"/> on <see cref="Specification.LoadCooldownBypassTypesDat"/> index.</remarks>
    public required int CooldownBypassType { get; init; }

    /// <summary> Gets VaalSouls.</summary>
    public required int VaalSouls { get; init; }

    /// <summary> Gets VaalStoredUses.</summary>
    public required int VaalStoredUses { get; init; }

    /// <summary> Gets CooldownGroup.</summary>
    public required int CooldownGroup { get; init; }

    /// <summary> Gets Unknown52.</summary>
    public required int Unknown52 { get; init; }

    /// <summary> Gets SoulGainPreventionDuration.</summary>
    public required int SoulGainPreventionDuration { get; init; }

    /// <summary> Gets AttackSpeedMultiplier.</summary>
    public required int AttackSpeedMultiplier { get; init; }

    /// <summary> Gets Unknown64.</summary>
    public required int Unknown64 { get; init; }

    /// <summary> Gets CostAmounts.</summary>
    public required ReadOnlyCollection<int> CostAmounts { get; init; }

    /// <summary> Gets CostTypes.</summary>
    /// <remarks> references <see cref="CostTypesDat"/> on <see cref="Specification.LoadCostTypesDat"/> index.</remarks>
    public required ReadOnlyCollection<int> CostTypes { get; init; }

    /// <summary> Gets ManaReservationFlat.</summary>
    public required int ManaReservationFlat { get; init; }

    /// <summary> Gets ManaReservationPercent.</summary>
    public required int ManaReservationPercent { get; init; }

    /// <summary> Gets LifeReservationFlat.</summary>
    public required int LifeReservationFlat { get; init; }

    /// <summary> Gets LifeReservationPercent.</summary>
    public required int LifeReservationPercent { get; init; }

    /// <summary> Gets AttackTime.</summary>
    public required int AttackTime { get; init; }
}
