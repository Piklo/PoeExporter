// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.DatFiles;

/// <summary>
/// Class containing ActiveSkills.dat data.
/// </summary>
public sealed partial class ActiveSkillsDat
{
    /// <summary> Gets Id.</summary>
    public required string Id { get; init; }

    /// <summary> Gets DisplayedName.</summary>
    public required string DisplayedName { get; init; }

    /// <summary> Gets Description.</summary>
    public required string Description { get; init; }

    /// <summary> Gets Unknown24.</summary>
    public required string Unknown24 { get; init; }

    /// <summary> Gets Icon_DDSFile.</summary>
    public required string Icon_DDSFile { get; init; }

    /// <summary> Gets ActiveSkillTargetTypes.</summary>
    /// <remarks> references <see cref="ActiveSkillTargetTypesDat"/> on <see cref="Specification.LoadActiveSkillTargetTypesDat"/> index.</remarks>
    public required ReadOnlyCollection<int> ActiveSkillTargetTypes { get; init; }

    /// <summary> Gets ActiveSkillTypes.</summary>
    /// <remarks> references <see cref="ActiveSkillTypeDat"/> on <see cref="Specification.LoadActiveSkillTypeDat"/> index.</remarks>
    public required ReadOnlyCollection<int> ActiveSkillTypes { get; init; }

    /// <summary> Gets WeaponRestriction_ItemClassesKeys.</summary>
    /// <remarks> references <see cref="ItemClassesDat"/> on <see cref="Specification.LoadItemClassesDat"/> index.</remarks>
    public required ReadOnlyCollection<int> WeaponRestriction_ItemClassesKeys { get; init; }

    /// <summary> Gets WebsiteDescription.</summary>
    public required string WebsiteDescription { get; init; }

    /// <summary> Gets WebsiteImage.</summary>
    public required string WebsiteImage { get; init; }

    /// <summary> Gets a value indicating whether Unknown104 is set.</summary>
    public required bool Unknown104 { get; init; }

    /// <summary> Gets Unknown105.</summary>
    public required string Unknown105 { get; init; }

    /// <summary> Gets a value indicating whether Unknown113 is set.</summary>
    public required bool Unknown113 { get; init; }

    /// <summary> Gets SkillTotemId.</summary>
    /// <remarks> references <see cref="SkillTotemsDat"/> on <see cref="Specification.LoadSkillTotemsDat"/> index.</remarks>
    public required int SkillTotemId { get; init; }

    /// <summary> Gets a value indicating whether IsManuallyCasted is set.</summary>
    public required bool IsManuallyCasted { get; init; }

    /// <summary> Gets Input_StatKeys.</summary>
    /// <remarks> references <see cref="StatsDat"/> on <see cref="Specification.LoadStatsDat"/> index.</remarks>
    public required ReadOnlyCollection<int> Input_StatKeys { get; init; }

    /// <summary> Gets Output_StatKeys.</summary>
    /// <remarks> references <see cref="StatsDat"/> on <see cref="Specification.LoadStatsDat"/> index.</remarks>
    public required ReadOnlyCollection<int> Output_StatKeys { get; init; }

    /// <summary> Gets MinionActiveSkillTypes.</summary>
    /// <remarks> references <see cref="ActiveSkillTypeDat"/> on <see cref="Specification.LoadActiveSkillTypeDat"/> index.</remarks>
    public required ReadOnlyCollection<int> MinionActiveSkillTypes { get; init; }

    /// <summary> Gets a value indicating whether Unknown167 is set.</summary>
    public required bool Unknown167 { get; init; }

    /// <summary> Gets a value indicating whether Unknown168 is set.</summary>
    public required bool Unknown168 { get; init; }

    /// <summary> Gets Unknown169.</summary>
    /// <remarks> references <see cref="StatsDat"/> on <see cref="Specification.LoadStatsDat"/> index.</remarks>
    public required ReadOnlyCollection<int> Unknown169 { get; init; }

    /// <summary> Gets Unknown185.</summary>
    public required int Unknown185 { get; init; }

    /// <summary> Gets AlternateSkillTargetingBehavioursKey.</summary>
    /// <remarks> references <see cref="AlternateSkillTargetingBehavioursDat"/> on <see cref="Specification.LoadAlternateSkillTargetingBehavioursDat"/> index.</remarks>
    public required int? AlternateSkillTargetingBehavioursKey { get; init; }

    /// <summary> Gets a value indicating whether Unknown205 is set.</summary>
    public required bool Unknown205 { get; init; }

    /// <summary> Gets AIFile.</summary>
    public required string AIFile { get; init; }

    /// <summary> Gets Unknown214.</summary>
    public required ReadOnlyCollection<int> Unknown214 { get; init; }

    /// <summary> Gets a value indicating whether Unknown230 is set.</summary>
    public required bool Unknown230 { get; init; }

    /// <summary> Gets a value indicating whether Unknown231 is set.</summary>
    public required bool Unknown231 { get; init; }

    /// <summary> Gets a value indicating whether Unknown232 is set.</summary>
    public required bool Unknown232 { get; init; }
}
