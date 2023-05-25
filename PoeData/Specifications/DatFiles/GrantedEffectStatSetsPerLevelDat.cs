// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.DatFiles;

/// <summary>
/// Class containing GrantedEffectStatSetsPerLevel.dat data.
/// </summary>
public sealed partial class GrantedEffectStatSetsPerLevelDat
{
    /// <summary> Gets StatSet.</summary>
    /// <remarks> references <see cref="GrantedEffectStatSetsDat"/> on <see cref="Specification.LoadGrantedEffectStatSetsDat"/> index.</remarks>
    public required int? StatSet { get; init; }

    /// <summary> Gets GemLevel.</summary>
    public required int GemLevel { get; init; }

    /// <summary> Gets PlayerLevelReq.</summary>
    public required float PlayerLevelReq { get; init; }

    /// <summary> Gets SpellCritChance.</summary>
    public required int SpellCritChance { get; init; }

    /// <summary> Gets AttackCritChance.</summary>
    public required int AttackCritChance { get; init; }

    /// <summary> Gets BaseMultiplier.</summary>
    public required int BaseMultiplier { get; init; }

    /// <summary> Gets DamageEffectiveness.</summary>
    public required int DamageEffectiveness { get; init; }

    /// <summary> Gets AdditionalFlags.</summary>
    /// <remarks> references <see cref="StatsDat"/> on <see cref="Specification.LoadStatsDat"/> index.</remarks>
    public required ReadOnlyCollection<int> AdditionalFlags { get; init; }

    /// <summary> Gets FloatStats.</summary>
    /// <remarks> references <see cref="StatsDat"/> on <see cref="Specification.LoadStatsDat"/> index.</remarks>
    public required ReadOnlyCollection<int> FloatStats { get; init; }

    /// <summary> Gets InterpolationBases.</summary>
    /// <remarks> references <see cref="StatsDat"/> on <see cref="Specification.LoadStatsDat"/> index.</remarks>
    public required ReadOnlyCollection<int> InterpolationBases { get; init; }

    /// <summary> Gets AdditionalStats.</summary>
    /// <remarks> references <see cref="StatsDat"/> on <see cref="Specification.LoadStatsDat"/> index.</remarks>
    public required ReadOnlyCollection<int> AdditionalStats { get; init; }

    /// <summary> Gets StatInterpolations.</summary>
    /// <remarks> references <see cref="StatInterpolationTypesDat"/> on <see cref="Specification.LoadStatInterpolationTypesDat"/> index.</remarks>
    public required ReadOnlyCollection<int> StatInterpolations { get; init; }

    /// <summary> Gets FloatStatsValues.</summary>
    public required ReadOnlyCollection<float> FloatStatsValues { get; init; }

    /// <summary> Gets BaseResolvedValues.</summary>
    public required ReadOnlyCollection<int> BaseResolvedValues { get; init; }

    /// <summary> Gets AdditionalStatsValues.</summary>
    public required ReadOnlyCollection<int> AdditionalStatsValues { get; init; }

    /// <summary> Gets GrantedEffects.</summary>
    /// <remarks> references <see cref="GrantedEffectsDat"/> on <see cref="Specification.LoadGrantedEffectsDat"/> index.</remarks>
    public required ReadOnlyCollection<int> GrantedEffects { get; init; }
}
