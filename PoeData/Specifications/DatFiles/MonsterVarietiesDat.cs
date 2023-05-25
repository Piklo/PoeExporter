// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.DatFiles;

/// <summary>
/// Class containing MonsterVarieties.dat data.
/// </summary>
public sealed partial class MonsterVarietiesDat
{
    /// <summary> Gets Id.</summary>
    public required string Id { get; init; }

    /// <summary> Gets MonsterTypesKey.</summary>
    /// <remarks> references <see cref="MonsterTypesDat"/> on <see cref="Specification.LoadMonsterTypesDat"/> index.</remarks>
    public required int? MonsterTypesKey { get; init; }

    /// <summary> Gets Unknown24.</summary>
    public required int Unknown24 { get; init; }

    /// <summary> Gets ObjectSize.</summary>
    public required int ObjectSize { get; init; }

    /// <summary> Gets MinimumAttackDistance.</summary>
    public required int MinimumAttackDistance { get; init; }

    /// <summary> Gets MaximumAttackDistance.</summary>
    public required int MaximumAttackDistance { get; init; }

    /// <summary> Gets ACTFiles.</summary>
    public required ReadOnlyCollection<string> ACTFiles { get; init; }

    /// <summary> Gets AOFiles.</summary>
    public required ReadOnlyCollection<string> AOFiles { get; init; }

    /// <summary> Gets BaseMonsterTypeIndex.</summary>
    public required string BaseMonsterTypeIndex { get; init; }

    /// <summary> Gets ModsKeys.</summary>
    /// <remarks> references <see cref="ModsDat"/> on <see cref="Specification.LoadModsDat"/> index.</remarks>
    public required ReadOnlyCollection<int> ModsKeys { get; init; }

    /// <summary> Gets Unknown96.</summary>
    public required int Unknown96 { get; init; }

    /// <summary> Gets Unknown100.</summary>
    public required string Unknown100 { get; init; }

    /// <summary> Gets Unknown108.</summary>
    public required string Unknown108 { get; init; }

    /// <summary> Gets ModelSizeMultiplier.</summary>
    public required int ModelSizeMultiplier { get; init; }

    /// <summary> Gets Unknown120.</summary>
    public required int Unknown120 { get; init; }

    /// <summary> Gets Unknown124.</summary>
    public required int Unknown124 { get; init; }

    /// <summary> Gets Unknown128.</summary>
    public required int Unknown128 { get; init; }

    /// <summary> Gets Unknown132.</summary>
    public required int Unknown132 { get; init; }

    /// <summary> Gets Unknown136.</summary>
    public required int Unknown136 { get; init; }

    /// <summary> Gets TagsKeys.</summary>
    /// <remarks> references <see cref="TagsDat"/> on <see cref="Specification.LoadTagsDat"/> index.</remarks>
    public required ReadOnlyCollection<int> TagsKeys { get; init; }

    /// <summary> Gets ExperienceMultiplier.</summary>
    public required int ExperienceMultiplier { get; init; }

    /// <summary> Gets Unknown160.</summary>
    public required ReadOnlyCollection<int> Unknown160 { get; init; }

    /// <summary> Gets Unknown176.</summary>
    public required int Unknown176 { get; init; }

    /// <summary> Gets Unknown180.</summary>
    public required int Unknown180 { get; init; }

    /// <summary> Gets Unknown184.</summary>
    public required int Unknown184 { get; init; }

    /// <summary> Gets CriticalStrikeChance.</summary>
    public required int CriticalStrikeChance { get; init; }

    /// <summary> Gets Unknown192.</summary>
    public required int Unknown192 { get; init; }

    /// <summary> Gets GrantedEffectsKeys.</summary>
    /// <remarks> references <see cref="GrantedEffectsDat"/> on <see cref="Specification.LoadGrantedEffectsDat"/> index.</remarks>
    public required ReadOnlyCollection<int> GrantedEffectsKeys { get; init; }

    /// <summary> Gets AISFile.</summary>
    public required string AISFile { get; init; }

    /// <summary> Gets ModsKeys2.</summary>
    /// <remarks> references <see cref="ModsDat"/> on <see cref="Specification.LoadModsDat"/> index.</remarks>
    public required ReadOnlyCollection<int> ModsKeys2 { get; init; }

    /// <summary> Gets Stance.</summary>
    public required string Stance { get; init; }

    /// <summary> Gets Unknown244.</summary>
    public required int? Unknown244 { get; init; }

    /// <summary> Gets Name.</summary>
    public required string Name { get; init; }

    /// <summary> Gets DamageMultiplier.</summary>
    public required int DamageMultiplier { get; init; }

    /// <summary> Gets LifeMultiplier.</summary>
    public required int LifeMultiplier { get; init; }

    /// <summary> Gets AttackSpeed.</summary>
    public required int AttackSpeed { get; init; }

    /// <summary> Gets Weapon1_ItemVisualIdentityKeys.</summary>
    /// <remarks> references <see cref="ItemVisualIdentityDat"/> on <see cref="Specification.LoadItemVisualIdentityDat"/> index.</remarks>
    public required ReadOnlyCollection<int> Weapon1_ItemVisualIdentityKeys { get; init; }

    /// <summary> Gets Weapon2_ItemVisualIdentityKeys.</summary>
    /// <remarks> references <see cref="ItemVisualIdentityDat"/> on <see cref="Specification.LoadItemVisualIdentityDat"/> index.</remarks>
    public required ReadOnlyCollection<int> Weapon2_ItemVisualIdentityKeys { get; init; }

    /// <summary> Gets Back_ItemVisualIdentityKey.</summary>
    /// <remarks> references <see cref="ItemVisualIdentityDat"/> on <see cref="Specification.LoadItemVisualIdentityDat"/> index.</remarks>
    public required int? Back_ItemVisualIdentityKey { get; init; }

    /// <summary> Gets MainHand_ItemClassesKey.</summary>
    /// <remarks> references <see cref="ItemClassesDat"/> on <see cref="Specification.LoadItemClassesDat"/> index.</remarks>
    public required int? MainHand_ItemClassesKey { get; init; }

    /// <summary> Gets OffHand_ItemClassesKey.</summary>
    /// <remarks> references <see cref="ItemClassesDat"/> on <see cref="Specification.LoadItemClassesDat"/> index.</remarks>
    public required int? OffHand_ItemClassesKey { get; init; }

    /// <summary> Gets Helmet_ItemVisualIdentityKey.</summary>
    /// <remarks> references <see cref="ItemVisualIdentityDat"/> on <see cref="Specification.LoadItemVisualIdentityDat"/> index.</remarks>
    public required int? Helmet_ItemVisualIdentityKey { get; init; }

    /// <summary> Gets Unknown376.</summary>
    public required int Unknown376 { get; init; }

    /// <summary> Gets KillSpecificMonsterCount_AchievementItemsKeys.</summary>
    /// <remarks> references <see cref="AchievementItemsDat"/> on <see cref="Specification.LoadAchievementItemsDat"/> index.</remarks>
    public required ReadOnlyCollection<int> KillSpecificMonsterCount_AchievementItemsKeys { get; init; }

    /// <summary> Gets Special_ModsKeys.</summary>
    /// <remarks> references <see cref="ModsDat"/> on <see cref="Specification.LoadModsDat"/> index.</remarks>
    public required ReadOnlyCollection<int> Special_ModsKeys { get; init; }

    /// <summary> Gets KillRare_AchievementItemsKeys.</summary>
    /// <remarks> references <see cref="AchievementItemsDat"/> on <see cref="Specification.LoadAchievementItemsDat"/> index.</remarks>
    public required ReadOnlyCollection<int> KillRare_AchievementItemsKeys { get; init; }

    /// <summary> Gets a value indicating whether Unknown428 is set.</summary>
    public required bool Unknown428 { get; init; }

    /// <summary> Gets Unknown429.</summary>
    public required int Unknown429 { get; init; }

    /// <summary> Gets Unknown433.</summary>
    public required int Unknown433 { get; init; }

    /// <summary> Gets Unknown437.</summary>
    public required int Unknown437 { get; init; }

    /// <summary> Gets Unknown441.</summary>
    public required int Unknown441 { get; init; }

    /// <summary> Gets Unknown445.</summary>
    public required int Unknown445 { get; init; }

    /// <summary> Gets Unknown449.</summary>
    public required int Unknown449 { get; init; }

    /// <summary> Gets Unknown453.</summary>
    public required int Unknown453 { get; init; }

    /// <summary> Gets a value indicating whether Unknown457 is set.</summary>
    public required bool Unknown457 { get; init; }

    /// <summary> Gets Unknown458.</summary>
    public required string Unknown458 { get; init; }

    /// <summary> Gets KillWhileOnslaughtIsActive_AchievementItemsKey.</summary>
    /// <remarks> references <see cref="AchievementItemsDat"/> on <see cref="Specification.LoadAchievementItemsDat"/> index.</remarks>
    public required int? KillWhileOnslaughtIsActive_AchievementItemsKey { get; init; }

    /// <summary> Gets MonsterSegmentsKey.</summary>
    /// <remarks> references <see cref="MonsterSegmentsDat"/> on <see cref="Specification.LoadMonsterSegmentsDat"/> index.</remarks>
    public required int? MonsterSegmentsKey { get; init; }

    /// <summary> Gets MonsterArmoursKey.</summary>
    /// <remarks> references <see cref="MonsterArmoursDat"/> on <see cref="Specification.LoadMonsterArmoursDat"/> index.</remarks>
    public required int? MonsterArmoursKey { get; init; }

    /// <summary> Gets KillWhileTalismanIsActive_AchievementItemsKey.</summary>
    /// <remarks> references <see cref="AchievementItemsDat"/> on <see cref="Specification.LoadAchievementItemsDat"/> index.</remarks>
    public required int? KillWhileTalismanIsActive_AchievementItemsKey { get; init; }

    /// <summary> Gets Part1_ModsKeys.</summary>
    /// <remarks> references <see cref="ModsDat"/> on <see cref="Specification.LoadModsDat"/> index.</remarks>
    public required ReadOnlyCollection<int> Part1_ModsKeys { get; init; }

    /// <summary> Gets Part2_ModsKeys.</summary>
    /// <remarks> references <see cref="ModsDat"/> on <see cref="Specification.LoadModsDat"/> index.</remarks>
    public required ReadOnlyCollection<int> Part2_ModsKeys { get; init; }

    /// <summary> Gets Endgame_ModsKeys.</summary>
    /// <remarks> references <see cref="ModsDat"/> on <see cref="Specification.LoadModsDat"/> index.</remarks>
    public required ReadOnlyCollection<int> Endgame_ModsKeys { get; init; }

    /// <summary> Gets Unknown578.</summary>
    public required int? Unknown578 { get; init; }

    /// <summary> Gets Unknown594.</summary>
    public required int Unknown594 { get; init; }

    /// <summary> Gets Unknown598.</summary>
    public required int Unknown598 { get; init; }

    /// <summary> Gets Unknown602.</summary>
    public required ReadOnlyCollection<int> Unknown602 { get; init; }

    /// <summary> Gets Unknown618.</summary>
    public required ReadOnlyCollection<int> Unknown618 { get; init; }

    /// <summary> Gets Unknown634.</summary>
    public required int Unknown634 { get; init; }

    /// <summary> Gets SinkAnimation_AOFile.</summary>
    public required string SinkAnimation_AOFile { get; init; }

    /// <summary> Gets a value indicating whether Unknown646 is set.</summary>
    public required bool Unknown646 { get; init; }

    /// <summary> Gets Unknown647.</summary>
    public required ReadOnlyCollection<int> Unknown647 { get; init; }

    /// <summary> Gets a value indicating whether Unknown663 is set.</summary>
    public required bool Unknown663 { get; init; }

    /// <summary> Gets a value indicating whether Unknown664 is set.</summary>
    public required bool Unknown664 { get; init; }

    /// <summary> Gets a value indicating whether Unknown665 is set.</summary>
    public required bool Unknown665 { get; init; }

    /// <summary> Gets Unknown666.</summary>
    public required int Unknown666 { get; init; }

    /// <summary> Gets Unknown670.</summary>
    public required int Unknown670 { get; init; }

    /// <summary> Gets Unknown674.</summary>
    public required float Unknown674 { get; init; }

    /// <summary> Gets Unknown678.</summary>
    public required int Unknown678 { get; init; }

    /// <summary> Gets EPKFile.</summary>
    public required string EPKFile { get; init; }

    /// <summary> Gets Unknown690.</summary>
    public required int Unknown690 { get; init; }

    /// <summary> Gets MonsterConditionalEffectPacksKey.</summary>
    /// <remarks> references <see cref="MonsterConditionalEffectPacksDat"/> on <see cref="Specification.LoadMonsterConditionalEffectPacksDat"/> index.</remarks>
    public required int? MonsterConditionalEffectPacksKey { get; init; }

    /// <summary> Gets a value indicating whether Unknown710 is set.</summary>
    public required bool Unknown710 { get; init; }

    /// <summary> Gets a value indicating whether Unknown711 is set.</summary>
    public required bool Unknown711 { get; init; }

    /// <summary> Gets Unknown712.</summary>
    public required int Unknown712 { get; init; }

    /// <summary> Gets a value indicating whether Unknown716 is set.</summary>
    public required bool Unknown716 { get; init; }

    /// <summary> Gets Unknown717.</summary>
    public required int Unknown717 { get; init; }

    /// <summary> Gets Unknown721.</summary>
    public required int Unknown721 { get; init; }

    /// <summary> Gets Unknown725.</summary>
    public required int Unknown725 { get; init; }

    /// <summary> Gets Unknown729.</summary>
    public required int Unknown729 { get; init; }

    /// <summary> Gets Unknown733.</summary>
    public required int Unknown733 { get; init; }

    /// <summary> Gets Unknown737.</summary>
    public required int Unknown737 { get; init; }

    /// <summary> Gets Unknown741.</summary>
    public required ReadOnlyCollection<int> Unknown741 { get; init; }

    /// <summary> Gets Unknown757.</summary>
    public required int Unknown757 { get; init; }

    /// <summary> Gets Unknown761.</summary>
    public required int Unknown761 { get; init; }

    /// <summary> Gets Unknown765.</summary>
    public required int Unknown765 { get; init; }

    /// <summary> Gets Unknown769.</summary>
    public required int Unknown769 { get; init; }

    /// <summary> Gets Unknown773.</summary>
    public required int Unknown773 { get; init; }

    /// <summary> Gets a value indicating whether Unknown777 is set.</summary>
    public required bool Unknown777 { get; init; }
}
