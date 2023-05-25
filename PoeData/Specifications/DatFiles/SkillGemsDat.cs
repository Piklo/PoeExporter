// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.DatFiles;

/// <summary>
/// Class containing SkillGems.dat data.
/// </summary>
public sealed partial class SkillGemsDat
{
    /// <summary> Gets BaseItemTypesKey.</summary>
    /// <remarks> references <see cref="BaseItemTypesDat"/> on <see cref="Specification.LoadBaseItemTypesDat"/> index.</remarks>
    public required int? BaseItemTypesKey { get; init; }

    /// <summary> Gets GrantedEffectsKey.</summary>
    /// <remarks> references <see cref="GrantedEffectsDat"/> on <see cref="Specification.LoadGrantedEffectsDat"/> index.</remarks>
    public required int? GrantedEffectsKey { get; init; }

    /// <summary> Gets Str.</summary>
    public required int Str { get; init; }

    /// <summary> Gets Dex.</summary>
    public required int Dex { get; init; }

    /// <summary> Gets Int.</summary>
    public required int Int { get; init; }

    /// <summary> Gets GemTagsKeys.</summary>
    /// <remarks> references <see cref="GemTagsDat"/> on <see cref="Specification.LoadGemTagsDat"/> index.</remarks>
    public required ReadOnlyCollection<int> GemTagsKeys { get; init; }

    /// <summary> Gets VaalVariant_BaseItemTypesKey.</summary>
    /// <remarks> references <see cref="BaseItemTypesDat"/> on <see cref="Specification.LoadBaseItemTypesDat"/> index.</remarks>
    public required int? VaalVariant_BaseItemTypesKey { get; init; }

    /// <summary> Gets a value indicating whether IsVaalVariant is set.</summary>
    public required bool IsVaalVariant { get; init; }

    /// <summary> Gets Description.</summary>
    public required string Description { get; init; }

    /// <summary> Gets Consumed_ModsKey.</summary>
    /// <remarks> references <see cref="ModsDat"/> on <see cref="Specification.LoadModsDat"/> index.</remarks>
    public required int? Consumed_ModsKey { get; init; }

    /// <summary> Gets GrantedEffectsKey2.</summary>
    /// <remarks> references <see cref="GrantedEffectsDat"/> on <see cref="Specification.LoadGrantedEffectsDat"/> index.</remarks>
    public required int? GrantedEffectsKey2 { get; init; }

    /// <summary> Gets MinionGlobalSkillLevelStat.</summary>
    /// <remarks> references <see cref="StatsDat"/> on <see cref="Specification.LoadStatsDat"/> index.</remarks>
    public required int? MinionGlobalSkillLevelStat { get; init; }

    /// <summary> Gets SupportSkillName.</summary>
    public required string SupportSkillName { get; init; }

    /// <summary> Gets a value indicating whether IsSupport is set.</summary>
    public required bool IsSupport { get; init; }

    /// <summary> Gets a value indicating whether Unknown142 is set.</summary>
    public required bool Unknown142 { get; init; }

    /// <summary> Gets a value indicating whether Unknown143 is set.</summary>
    public required bool Unknown143 { get; init; }

    /// <summary> Gets a value indicating whether Unknown144 is set.</summary>
    public required bool Unknown144 { get; init; }

    /// <summary> Gets a value indicating whether Unknown145 is set.</summary>
    public required bool Unknown145 { get; init; }

    /// <summary> Gets a value indicating whether Unknown146 is set.</summary>
    public required bool Unknown146 { get; init; }

    /// <summary> Gets AwakenedVariant.</summary>
    /// <remarks> references <see cref="SkillGemsDat"/> on <see cref="Specification.LoadSkillGemsDat"/> index.</remarks>
    public required int? AwakenedVariant { get; init; }

    /// <summary> Gets RegularVariant.</summary>
    /// <remarks> references <see cref="SkillGemsDat"/> on <see cref="Specification.LoadSkillGemsDat"/> index.</remarks>
    public required int? RegularVariant { get; init; }

    /// <summary> Gets GrantedEffectHardMode.</summary>
    /// <remarks> references <see cref="GrantedEffectsDat"/> on <see cref="Specification.LoadGrantedEffectsDat"/> index.</remarks>
    public required int? GrantedEffectHardMode { get; init; }

    /// <summary> Gets Unknown179.</summary>
    public required int? Unknown179 { get; init; }

    /// <summary> Gets Unknown195.</summary>
    public required int Unknown195 { get; init; }

    /// <summary> Gets ItemExperienceType.</summary>
    /// <remarks> references <see cref="ItemExperienceTypesDat"/> on <see cref="Specification.LoadItemExperienceTypesDat"/> index.</remarks>
    public required int? ItemExperienceType { get; init; }
}
