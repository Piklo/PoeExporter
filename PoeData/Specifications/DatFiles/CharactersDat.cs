// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.DatFiles;

/// <summary>
/// Class containing Characters.dat data.
/// </summary>
public sealed partial class CharactersDat
{
    /// <summary> Gets Id.</summary>
    public required string Id { get; init; }

    /// <summary> Gets Name.</summary>
    public required string Name { get; init; }

    /// <summary> Gets AOFile.</summary>
    public required string AOFile { get; init; }

    /// <summary> Gets ACTFile.</summary>
    public required string ACTFile { get; init; }

    /// <summary> Gets BaseMaxLife.</summary>
    public required int BaseMaxLife { get; init; }

    /// <summary> Gets BaseMaxMana.</summary>
    public required int BaseMaxMana { get; init; }

    /// <summary> Gets WeaponSpeed.</summary>
    public required int WeaponSpeed { get; init; }

    /// <summary> Gets MinDamage.</summary>
    public required int MinDamage { get; init; }

    /// <summary> Gets MaxDamage.</summary>
    public required int MaxDamage { get; init; }

    /// <summary> Gets MaxAttackDistance.</summary>
    public required int MaxAttackDistance { get; init; }

    /// <summary> Gets Icon.</summary>
    public required string Icon { get; init; }

    /// <summary> Gets IntegerId.</summary>
    public required int IntegerId { get; init; }

    /// <summary> Gets BaseStrength.</summary>
    public required int BaseStrength { get; init; }

    /// <summary> Gets BaseDexterity.</summary>
    public required int BaseDexterity { get; init; }

    /// <summary> Gets BaseIntelligence.</summary>
    public required int BaseIntelligence { get; init; }

    /// <summary> Gets Unknown80.</summary>
    public required ReadOnlyCollection<int> Unknown80 { get; init; }

    /// <summary> Gets Description.</summary>
    public required string Description { get; init; }

    /// <summary> Gets StartSkillGem.</summary>
    /// <remarks> references <see cref="SkillGemsDat"/> on <see cref="Specification.LoadSkillGemsDat"/> index.</remarks>
    public required int? StartSkillGem { get; init; }

    /// <summary> Gets Unknown120.</summary>
    public required int? Unknown120 { get; init; }

    /// <summary> Gets Unknown136.</summary>
    public required int Unknown136 { get; init; }

    /// <summary> Gets Unknown140.</summary>
    public required int Unknown140 { get; init; }

    /// <summary> Gets CharacterSize.</summary>
    public required int CharacterSize { get; init; }

    /// <summary> Gets IntroSoundFile.</summary>
    public required string IntroSoundFile { get; init; }

    /// <summary> Gets StartWeapons.</summary>
    /// <remarks> references <see cref="BaseItemTypesDat"/> on <see cref="Specification.LoadBaseItemTypesDat"/> index.</remarks>
    public required ReadOnlyCollection<int> StartWeapons { get; init; }

    /// <summary> Gets Gender.</summary>
    public required string Gender { get; init; }

    /// <summary> Gets TraitDescription.</summary>
    public required string TraitDescription { get; init; }

    /// <summary> Gets Unknown188.</summary>
    public required int? Unknown188 { get; init; }

    /// <summary> Gets Unknown204.</summary>
    public required int? Unknown204 { get; init; }

    /// <summary> Gets Unknown220.</summary>
    public required int? Unknown220 { get; init; }

    /// <summary> Gets Unknown236.</summary>
    public required int? Unknown236 { get; init; }

    /// <summary> Gets Unknown252.</summary>
    public required int Unknown252 { get; init; }

    /// <summary> Gets Unknown256.</summary>
    public required ReadOnlyCollection<int> Unknown256 { get; init; }

    /// <summary> Gets PassiveTreeImage.</summary>
    public required string PassiveTreeImage { get; init; }

    /// <summary> Gets Unknown280.</summary>
    public required int Unknown280 { get; init; }

    /// <summary> Gets Unknown284.</summary>
    public required int Unknown284 { get; init; }

    /// <summary> Gets TencentVideo.</summary>
    public required string TencentVideo { get; init; }

    /// <summary> Gets AttrsAsId.</summary>
    public required string AttrsAsId { get; init; }

    /// <summary> Gets LoginScreen.</summary>
    public required string LoginScreen { get; init; }

    /// <summary> Gets PlayerCritter.</summary>
    public required string PlayerCritter { get; init; }

    /// <summary> Gets PlayerEffect.</summary>
    public required string PlayerEffect { get; init; }

    /// <summary> Gets AfterImage.</summary>
    public required string AfterImage { get; init; }

    /// <summary> Gets Mirage.</summary>
    /// <remarks> references <see cref="MonsterVarietiesDat"/> on <see cref="Specification.LoadMonsterVarietiesDat"/> index.</remarks>
    public required int? Mirage { get; init; }

    /// <summary> Gets CloneImmobile.</summary>
    /// <remarks> references <see cref="MonsterVarietiesDat"/> on <see cref="Specification.LoadMonsterVarietiesDat"/> index.</remarks>
    public required int? CloneImmobile { get; init; }

    /// <summary> Gets ReplicateClone.</summary>
    /// <remarks> references <see cref="MonsterVarietiesDat"/> on <see cref="Specification.LoadMonsterVarietiesDat"/> index.</remarks>
    public required int? ReplicateClone { get; init; }

    /// <summary> Gets LightningClone.</summary>
    /// <remarks> references <see cref="MonsterVarietiesDat"/> on <see cref="Specification.LoadMonsterVarietiesDat"/> index.</remarks>
    public required int? LightningClone { get; init; }

    /// <summary> Gets Unknown400.</summary>
    public required float Unknown400 { get; init; }

    /// <summary> Gets Unknown404.</summary>
    public required float Unknown404 { get; init; }

    /// <summary> Gets SkillTreeBackground.</summary>
    public required string SkillTreeBackground { get; init; }

    /// <summary> Gets Clone.</summary>
    /// <remarks> references <see cref="MonsterVarietiesDat"/> on <see cref="Specification.LoadMonsterVarietiesDat"/> index.</remarks>
    public required int? Clone { get; init; }

    /// <summary> Gets Double.</summary>
    /// <remarks> references <see cref="MonsterVarietiesDat"/> on <see cref="Specification.LoadMonsterVarietiesDat"/> index.</remarks>
    public required int? Double { get; init; }

    /// <summary> Gets MirageWarrior.</summary>
    /// <remarks> references <see cref="MonsterVarietiesDat"/> on <see cref="Specification.LoadMonsterVarietiesDat"/> index.</remarks>
    public required int? MirageWarrior { get; init; }

    /// <summary> Gets DoubleTwo.</summary>
    /// <remarks> references <see cref="MonsterVarietiesDat"/> on <see cref="Specification.LoadMonsterVarietiesDat"/> index.</remarks>
    public required int? DoubleTwo { get; init; }

    /// <summary> Gets DarkExile.</summary>
    /// <remarks> references <see cref="MonsterVarietiesDat"/> on <see cref="Specification.LoadMonsterVarietiesDat"/> index.</remarks>
    public required int? DarkExile { get; init; }

    /// <summary> Gets Attr.</summary>
    public required string Attr { get; init; }

    /// <summary> Gets AttrLowercase.</summary>
    public required string AttrLowercase { get; init; }

    /// <summary> Gets Script.</summary>
    public required string Script { get; init; }

    /// <summary> Gets Unknown520.</summary>
    public required int? Unknown520 { get; init; }

    /// <summary> Gets Unknown536.</summary>
    public required int Unknown536 { get; init; }
}
