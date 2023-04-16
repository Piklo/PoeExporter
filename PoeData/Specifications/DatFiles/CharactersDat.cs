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
    /// <remarks> references <see cref="SkillGemsDat"/> on <see cref="Specification.GetSkillGemsDat"/> index.</remarks>
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
    /// <remarks> references <see cref="BaseItemTypesDat"/> on <see cref="Specification.GetBaseItemTypesDat"/> index.</remarks>
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
    /// <remarks> references <see cref="MonsterVarietiesDat"/> on <see cref="Specification.GetMonsterVarietiesDat"/> index.</remarks>
    public required int? Mirage { get; init; }

    /// <summary> Gets CloneImmobile.</summary>
    /// <remarks> references <see cref="MonsterVarietiesDat"/> on <see cref="Specification.GetMonsterVarietiesDat"/> index.</remarks>
    public required int? CloneImmobile { get; init; }

    /// <summary> Gets ReplicateClone.</summary>
    /// <remarks> references <see cref="MonsterVarietiesDat"/> on <see cref="Specification.GetMonsterVarietiesDat"/> index.</remarks>
    public required int? ReplicateClone { get; init; }

    /// <summary> Gets LightningClone.</summary>
    /// <remarks> references <see cref="MonsterVarietiesDat"/> on <see cref="Specification.GetMonsterVarietiesDat"/> index.</remarks>
    public required int? LightningClone { get; init; }

    /// <summary> Gets Unknown400.</summary>
    public required float Unknown400 { get; init; }

    /// <summary> Gets Unknown404.</summary>
    public required float Unknown404 { get; init; }

    /// <summary> Gets SkillTreeBackground.</summary>
    public required string SkillTreeBackground { get; init; }

    /// <summary> Gets Clone.</summary>
    /// <remarks> references <see cref="MonsterVarietiesDat"/> on <see cref="Specification.GetMonsterVarietiesDat"/> index.</remarks>
    public required int? Clone { get; init; }

    /// <summary> Gets Double.</summary>
    /// <remarks> references <see cref="MonsterVarietiesDat"/> on <see cref="Specification.GetMonsterVarietiesDat"/> index.</remarks>
    public required int? Double { get; init; }

    /// <summary> Gets MirageWarrior.</summary>
    /// <remarks> references <see cref="MonsterVarietiesDat"/> on <see cref="Specification.GetMonsterVarietiesDat"/> index.</remarks>
    public required int? MirageWarrior { get; init; }

    /// <summary> Gets DoubleTwo.</summary>
    /// <remarks> references <see cref="MonsterVarietiesDat"/> on <see cref="Specification.GetMonsterVarietiesDat"/> index.</remarks>
    public required int? DoubleTwo { get; init; }

    /// <summary> Gets DarkExile.</summary>
    /// <remarks> references <see cref="MonsterVarietiesDat"/> on <see cref="Specification.GetMonsterVarietiesDat"/> index.</remarks>
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

    /// <summary>
    /// Gets CharactersDat data.
    /// </summary>
    /// <param name="dataLoader">data loader.</param>
    /// <returns>array of CharactersDat.</returns>
    internal static CharactersDat[] Load(DataLoader dataLoader)
    {
        if (dataLoader is null)
        {
            throw new ArgumentNullException(nameof(dataLoader));
        }

        const string filePath = "Data/Characters.dat64";
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new CharactersDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading Id
            (var idLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Name
            (var nameLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading AOFile
            (var aofileLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading ACTFile
            (var actfileLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading BaseMaxLife
            (var basemaxlifeLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading BaseMaxMana
            (var basemaxmanaLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading WeaponSpeed
            (var weaponspeedLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading MinDamage
            (var mindamageLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading MaxDamage
            (var maxdamageLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading MaxAttackDistance
            (var maxattackdistanceLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Icon
            (var iconLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading IntegerId
            (var integeridLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading BaseStrength
            (var basestrengthLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading BaseDexterity
            (var basedexterityLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading BaseIntelligence
            (var baseintelligenceLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown80
            (var tempunknown80Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var unknown80Loading = tempunknown80Loading.AsReadOnly();

            // loading Description
            (var descriptionLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading StartSkillGem
            (var startskillgemLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading Unknown120
            (var unknown120Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading Unknown136
            (var unknown136Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown140
            (var unknown140Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading CharacterSize
            (var charactersizeLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading IntroSoundFile
            (var introsoundfileLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading StartWeapons
            (var tempstartweaponsLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var startweaponsLoading = tempstartweaponsLoading.AsReadOnly();

            // loading Gender
            (var genderLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading TraitDescription
            (var traitdescriptionLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Unknown188
            (var unknown188Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading Unknown204
            (var unknown204Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading Unknown220
            (var unknown220Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading Unknown236
            (var unknown236Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading Unknown252
            (var unknown252Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown256
            (var tempunknown256Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var unknown256Loading = tempunknown256Loading.AsReadOnly();

            // loading PassiveTreeImage
            (var passivetreeimageLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Unknown280
            (var unknown280Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown284
            (var unknown284Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading TencentVideo
            (var tencentvideoLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading AttrsAsId
            (var attrsasidLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading LoginScreen
            (var loginscreenLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading PlayerCritter
            (var playercritterLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading PlayerEffect
            (var playereffectLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading AfterImage
            (var afterimageLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Mirage
            (var mirageLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading CloneImmobile
            (var cloneimmobileLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading ReplicateClone
            (var replicatecloneLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading LightningClone
            (var lightningcloneLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading Unknown400
            (var unknown400Loading, offset) = SpecificationFileLoader.LoadFloat(decompressedFile, offset);

            // loading Unknown404
            (var unknown404Loading, offset) = SpecificationFileLoader.LoadFloat(decompressedFile, offset);

            // loading SkillTreeBackground
            (var skilltreebackgroundLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Clone
            (var cloneLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading Double
            (var doubleLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading MirageWarrior
            (var miragewarriorLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading DoubleTwo
            (var doubletwoLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading DarkExile
            (var darkexileLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading Attr
            (var attrLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading AttrLowercase
            (var attrlowercaseLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Script
            (var scriptLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Unknown520
            (var unknown520Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading Unknown536
            (var unknown536Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            if (offset != expectedOffset)
            {
                throw new SchemaMismatchException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new CharactersDat()
            {
                Id = idLoading,
                Name = nameLoading,
                AOFile = aofileLoading,
                ACTFile = actfileLoading,
                BaseMaxLife = basemaxlifeLoading,
                BaseMaxMana = basemaxmanaLoading,
                WeaponSpeed = weaponspeedLoading,
                MinDamage = mindamageLoading,
                MaxDamage = maxdamageLoading,
                MaxAttackDistance = maxattackdistanceLoading,
                Icon = iconLoading,
                IntegerId = integeridLoading,
                BaseStrength = basestrengthLoading,
                BaseDexterity = basedexterityLoading,
                BaseIntelligence = baseintelligenceLoading,
                Unknown80 = unknown80Loading,
                Description = descriptionLoading,
                StartSkillGem = startskillgemLoading,
                Unknown120 = unknown120Loading,
                Unknown136 = unknown136Loading,
                Unknown140 = unknown140Loading,
                CharacterSize = charactersizeLoading,
                IntroSoundFile = introsoundfileLoading,
                StartWeapons = startweaponsLoading,
                Gender = genderLoading,
                TraitDescription = traitdescriptionLoading,
                Unknown188 = unknown188Loading,
                Unknown204 = unknown204Loading,
                Unknown220 = unknown220Loading,
                Unknown236 = unknown236Loading,
                Unknown252 = unknown252Loading,
                Unknown256 = unknown256Loading,
                PassiveTreeImage = passivetreeimageLoading,
                Unknown280 = unknown280Loading,
                Unknown284 = unknown284Loading,
                TencentVideo = tencentvideoLoading,
                AttrsAsId = attrsasidLoading,
                LoginScreen = loginscreenLoading,
                PlayerCritter = playercritterLoading,
                PlayerEffect = playereffectLoading,
                AfterImage = afterimageLoading,
                Mirage = mirageLoading,
                CloneImmobile = cloneimmobileLoading,
                ReplicateClone = replicatecloneLoading,
                LightningClone = lightningcloneLoading,
                Unknown400 = unknown400Loading,
                Unknown404 = unknown404Loading,
                SkillTreeBackground = skilltreebackgroundLoading,
                Clone = cloneLoading,
                Double = doubleLoading,
                MirageWarrior = miragewarriorLoading,
                DoubleTwo = doubletwoLoading,
                DarkExile = darkexileLoading,
                Attr = attrLoading,
                AttrLowercase = attrlowercaseLoading,
                Script = scriptLoading,
                Unknown520 = unknown520Loading,
                Unknown536 = unknown536Loading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
