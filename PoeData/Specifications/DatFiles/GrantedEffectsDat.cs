// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.DatFiles;

/// <summary>
/// Class containing GrantedEffects.dat data.
/// </summary>
public sealed partial class GrantedEffectsDat
{
    /// <summary> Gets Id.</summary>
    public required string Id { get; init; }

    /// <summary> Gets a value indicating whether IsSupport is set.</summary>
    public required bool IsSupport { get; init; }

    /// <summary> Gets AllowedActiveSkillTypes.</summary>
    /// <remarks> references <see cref="ActiveSkillTypeDat"/> on <see cref="Specification.LoadActiveSkillTypeDat"/> index.</remarks>
    public required ReadOnlyCollection<int> AllowedActiveSkillTypes { get; init; }

    /// <summary> Gets SupportGemLetter.</summary>
    public required string SupportGemLetter { get; init; }

    /// <summary> Gets Attribute.</summary>
    /// <remarks> references <see cref="AttributesDat"/> on <see cref="Specification.LoadAttributesDat"/> index.</remarks>
    public required int Attribute { get; init; }

    /// <summary> Gets AddedActiveSkillTypes.</summary>
    /// <remarks> references <see cref="ActiveSkillTypeDat"/> on <see cref="Specification.LoadActiveSkillTypeDat"/> index.</remarks>
    public required ReadOnlyCollection<int> AddedActiveSkillTypes { get; init; }

    /// <summary> Gets ExcludedActiveSkillTypes.</summary>
    /// <remarks> references <see cref="ActiveSkillTypeDat"/> on <see cref="Specification.LoadActiveSkillTypeDat"/> index.</remarks>
    public required ReadOnlyCollection<int> ExcludedActiveSkillTypes { get; init; }

    /// <summary> Gets a value indicating whether SupportsGemsOnly is set.</summary>
    public required bool SupportsGemsOnly { get; init; }

    /// <summary> Gets Unknown70.</summary>
    public required int Unknown70 { get; init; }

    /// <summary> Gets Unknown74.</summary>
    public required ReadOnlyCollection<int> Unknown74 { get; init; }

    /// <summary> Gets a value indicating whether CannotBeSupported is set.</summary>
    public required bool CannotBeSupported { get; init; }

    /// <summary> Gets Unknown91.</summary>
    public required int Unknown91 { get; init; }

    /// <summary> Gets CastTime.</summary>
    public required int CastTime { get; init; }

    /// <summary> Gets ActiveSkill.</summary>
    /// <remarks> references <see cref="ActiveSkillsDat"/> on <see cref="Specification.LoadActiveSkillsDat"/> index.</remarks>
    public required int? ActiveSkill { get; init; }

    /// <summary> Gets a value indicating whether IgnoreMinionTypes is set.</summary>
    public required bool IgnoreMinionTypes { get; init; }

    /// <summary> Gets a value indicating whether Unknown116 is set.</summary>
    public required bool Unknown116 { get; init; }

    /// <summary> Gets AddedMinionActiveSkillTypes.</summary>
    /// <remarks> references <see cref="ActiveSkillTypeDat"/> on <see cref="Specification.LoadActiveSkillTypeDat"/> index.</remarks>
    public required ReadOnlyCollection<int> AddedMinionActiveSkillTypes { get; init; }

    /// <summary> Gets Animation.</summary>
    /// <remarks> references <see cref="AnimationDat"/> on <see cref="Specification.LoadAnimationDat"/> index.</remarks>
    public required int? Animation { get; init; }

    /// <summary> Gets MultiPartAchievement.</summary>
    /// <remarks> references <see cref="MultiPartAchievementsDat"/> on <see cref="Specification.LoadMultiPartAchievementsDat"/> index.</remarks>
    public required int? MultiPartAchievement { get; init; }

    /// <summary> Gets a value indicating whether Unknown165 is set.</summary>
    public required bool Unknown165 { get; init; }

    /// <summary> Gets SupportWeaponRestrictions.</summary>
    /// <remarks> references <see cref="ItemClassesDat"/> on <see cref="Specification.LoadItemClassesDat"/> index.</remarks>
    public required ReadOnlyCollection<int> SupportWeaponRestrictions { get; init; }

    /// <summary> Gets RegularVariant.</summary>
    /// <remarks> references <see cref="GrantedEffectsDat"/> on <see cref="Specification.LoadGrantedEffectsDat"/> index.</remarks>
    public required int? RegularVariant { get; init; }

    /// <summary> Gets Unknown190.</summary>
    public required int Unknown190 { get; init; }

    /// <summary> Gets Unknown194.</summary>
    public required int Unknown194 { get; init; }

    /// <summary> Gets Unknown198.</summary>
    public required int Unknown198 { get; init; }

    /// <summary> Gets a value indicating whether Unknown202 is set.</summary>
    public required bool Unknown202 { get; init; }

    /// <summary> Gets StatSet.</summary>
    /// <remarks> references <see cref="GrantedEffectStatSetsDat"/> on <see cref="Specification.LoadGrantedEffectStatSetsDat"/> index.</remarks>
    public required int? StatSet { get; init; }

    /// <summary> Gets Unknown219.</summary>
    public required ReadOnlyCollection<int> Unknown219 { get; init; }
}
