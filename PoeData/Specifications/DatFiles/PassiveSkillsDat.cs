// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.DatFiles;

/// <summary>
/// Class containing PassiveSkills.dat data.
/// </summary>
public sealed partial class PassiveSkillsDat
{
    /// <summary> Gets Id.</summary>
    public required string Id { get; init; }

    /// <summary> Gets Icon_DDSFile.</summary>
    public required string Icon_DDSFile { get; init; }

    /// <summary> Gets Stats.</summary>
    /// <remarks> references <see cref="StatsDat"/> on <see cref="Specification.LoadStatsDat"/> index.</remarks>
    public required ReadOnlyCollection<int> Stats { get; init; }

    /// <summary> Gets Stat1Value.</summary>
    public required int Stat1Value { get; init; }

    /// <summary> Gets Stat2Value.</summary>
    public required int Stat2Value { get; init; }

    /// <summary> Gets Stat3Value.</summary>
    public required int Stat3Value { get; init; }

    /// <summary> Gets Stat4Value.</summary>
    public required int Stat4Value { get; init; }

    /// <summary> Gets PassiveSkillGraphId.</summary>
    public required int PassiveSkillGraphId { get; init; }

    /// <summary> Gets Name.</summary>
    public required string Name { get; init; }

    /// <summary> Gets Characters.</summary>
    /// <remarks> references <see cref="CharactersDat"/> on <see cref="Specification.LoadCharactersDat"/> index.</remarks>
    public required ReadOnlyCollection<int> Characters { get; init; }

    /// <summary> Gets a value indicating whether IsKeystone is set.</summary>
    public required bool IsKeystone { get; init; }

    /// <summary> Gets a value indicating whether IsNotable is set.</summary>
    public required bool IsNotable { get; init; }

    /// <summary> Gets FlavourText.</summary>
    public required string FlavourText { get; init; }

    /// <summary> Gets a value indicating whether IsJustIcon is set.</summary>
    public required bool IsJustIcon { get; init; }

    /// <summary> Gets AchievementItem.</summary>
    /// <remarks> references <see cref="AchievementItemsDat"/> on <see cref="Specification.LoadAchievementItemsDat"/> index.</remarks>
    public required int? AchievementItem { get; init; }

    /// <summary> Gets a value indicating whether IsJewelSocket is set.</summary>
    public required bool IsJewelSocket { get; init; }

    /// <summary> Gets AscendancyKey.</summary>
    /// <remarks> references <see cref="AscendancyDat"/> on <see cref="Specification.LoadAscendancyDat"/> index.</remarks>
    public required int? AscendancyKey { get; init; }

    /// <summary> Gets a value indicating whether IsAscendancyStartingNode is set.</summary>
    public required bool IsAscendancyStartingNode { get; init; }

    /// <summary> Gets ReminderStrings.</summary>
    /// <remarks> references <see cref="ClientStringsDat"/> on <see cref="Specification.LoadClientStringsDat"/> index.</remarks>
    public required ReadOnlyCollection<int> ReminderStrings { get; init; }

    /// <summary> Gets SkillPointsGranted.</summary>
    public required int SkillPointsGranted { get; init; }

    /// <summary> Gets a value indicating whether IsMultipleChoice is set.</summary>
    public required bool IsMultipleChoice { get; init; }

    /// <summary> Gets a value indicating whether IsMultipleChoiceOption is set.</summary>
    public required bool IsMultipleChoiceOption { get; init; }

    /// <summary> Gets Stat5Value.</summary>
    public required int Stat5Value { get; init; }

    /// <summary> Gets PassiveSkillBuffs.</summary>
    /// <remarks> references <see cref="BuffTemplatesDat"/> on <see cref="Specification.LoadBuffTemplatesDat"/> index.</remarks>
    public required ReadOnlyCollection<int> PassiveSkillBuffs { get; init; }

    /// <summary> Gets GrantedEffectsPerLevel.</summary>
    /// <remarks> references <see cref="GrantedEffectsPerLevelDat"/> on <see cref="Specification.LoadGrantedEffectsPerLevelDat"/> index.</remarks>
    public required int? GrantedEffectsPerLevel { get; init; }

    /// <summary> Gets a value indicating whether IsAnointmentOnly is set.</summary>
    public required bool IsAnointmentOnly { get; init; }

    /// <summary> Gets Unknown180.</summary>
    public required int Unknown180 { get; init; }

    /// <summary> Gets a value indicating whether IsExpansion is set.</summary>
    public required bool IsExpansion { get; init; }

    /// <summary> Gets a value indicating whether IsProxyPassive is set.</summary>
    public required bool IsProxyPassive { get; init; }

    /// <summary> Gets SkillType.</summary>
    /// <remarks> references <see cref="PassiveSkillTypesDat"/> on <see cref="Specification.LoadPassiveSkillTypesDat"/> index.</remarks>
    public required int SkillType { get; init; }

    /// <summary> Gets MasteryGroup.</summary>
    /// <remarks> references <see cref="PassiveSkillMasteryGroupsDat"/> on <see cref="Specification.LoadPassiveSkillMasteryGroupsDat"/> index.</remarks>
    public required int? MasteryGroup { get; init; }

    /// <summary> Gets Unknown206.</summary>
    public required int? Unknown206 { get; init; }

    /// <summary> Gets SoundEffect.</summary>
    /// <remarks> references <see cref="SoundEffectsDat"/> on <see cref="Specification.LoadSoundEffectsDat"/> index.</remarks>
    public required int? SoundEffect { get; init; }

    /// <summary> Gets Unknown238.</summary>
    public required string Unknown238 { get; init; }

    /// <summary> Gets Unknown246.</summary>
    public required int Unknown246 { get; init; }

    /// <summary> Gets Unknown250.</summary>
    public required int Unknown250 { get; init; }

    /// <summary> Gets Unknown254.</summary>
    public required int Unknown254 { get; init; }

    /// <summary> Gets Unknown258.</summary>
    public required int Unknown258 { get; init; }

    /// <summary> Gets Unknown262.</summary>
    public required int Unknown262 { get; init; }

    /// <summary> Gets a value indicating whether Unknown266 is set.</summary>
    public required bool Unknown266 { get; init; }

    /// <summary> Gets Unknown267.</summary>
    public required ReadOnlyCollection<int> Unknown267 { get; init; }

    /// <summary> Gets Unknown283.</summary>
    public required int Unknown283 { get; init; }

    /// <summary> Gets Unknown287.</summary>
    public required ReadOnlyCollection<int> Unknown287 { get; init; }

    /// <summary> Gets a value indicating whether Unknown303 is set.</summary>
    public required bool Unknown303 { get; init; }
}
