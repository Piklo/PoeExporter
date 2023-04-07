// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.Dat;

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
    /// <remarks> references <see cref="ActiveSkillTypeDat"/> on <see cref="Specification.GetActiveSkillTypeDat"/> index.</remarks>
    public required ReadOnlyCollection<int> AllowedActiveSkillTypes { get; init; }

    /// <summary> Gets SupportGemLetter.</summary>
    public required string SupportGemLetter { get; init; }

    /// <summary> Gets Attribute.</summary>
    /// <remarks> references <see cref="AttributesDat"/> on <see cref="Specification.GetAttributesDat"/> index.</remarks>
    public required int Attribute { get; init; }

    /// <summary> Gets AddedActiveSkillTypes.</summary>
    /// <remarks> references <see cref="ActiveSkillTypeDat"/> on <see cref="Specification.GetActiveSkillTypeDat"/> index.</remarks>
    public required ReadOnlyCollection<int> AddedActiveSkillTypes { get; init; }

    /// <summary> Gets ExcludedActiveSkillTypes.</summary>
    /// <remarks> references <see cref="ActiveSkillTypeDat"/> on <see cref="Specification.GetActiveSkillTypeDat"/> index.</remarks>
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
    /// <remarks> references <see cref="ActiveSkillsDat"/> on <see cref="Specification.GetActiveSkillsDat"/> index.</remarks>
    public required int? ActiveSkill { get; init; }

    /// <summary> Gets a value indicating whether IgnoreMinionTypes is set.</summary>
    public required bool IgnoreMinionTypes { get; init; }

    /// <summary> Gets a value indicating whether Unknown116 is set.</summary>
    public required bool Unknown116 { get; init; }

    /// <summary> Gets AddedMinionActiveSkillTypes.</summary>
    /// <remarks> references <see cref="ActiveSkillTypeDat"/> on <see cref="Specification.GetActiveSkillTypeDat"/> index.</remarks>
    public required ReadOnlyCollection<int> AddedMinionActiveSkillTypes { get; init; }

    /// <summary> Gets Animation.</summary>
    /// <remarks> references <see cref="AnimationDat"/> on <see cref="Specification.GetAnimationDat"/> index.</remarks>
    public required int? Animation { get; init; }

    /// <summary> Gets MultiPartAchievement.</summary>
    /// <remarks> references <see cref="MultiPartAchievementsDat"/> on <see cref="Specification.GetMultiPartAchievementsDat"/> index.</remarks>
    public required int? MultiPartAchievement { get; init; }

    /// <summary> Gets a value indicating whether Unknown165 is set.</summary>
    public required bool Unknown165 { get; init; }

    /// <summary> Gets SupportWeaponRestrictions.</summary>
    /// <remarks> references <see cref="ItemClassesDat"/> on <see cref="Specification.GetItemClassesDat"/> index.</remarks>
    public required ReadOnlyCollection<int> SupportWeaponRestrictions { get; init; }

    /// <summary> Gets RegularVariant.</summary>
    /// <remarks> references <see cref="GrantedEffectsDat"/> on <see cref="Specification.GetGrantedEffectsDat"/> index.</remarks>
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
    /// <remarks> references <see cref="GrantedEffectStatSetsDat"/> on <see cref="Specification.GetGrantedEffectStatSetsDat"/> index.</remarks>
    public required int? StatSet { get; init; }

    /// <summary> Gets Unknown219.</summary>
    public required ReadOnlyCollection<int> Unknown219 { get; init; }

    /// <summary>
    /// Gets GrantedEffectsDat data.
    /// </summary>
    /// <param name="dataLoader">data loader.</param>
    /// <returns>array of GrantedEffectsDat.</returns>
    internal static GrantedEffectsDat[] Load(DataLoader dataLoader)
    {
        if (dataLoader is null)
        {
            throw new ArgumentNullException(nameof(dataLoader));
        }

        const string filePath = "Data/GrantedEffects.dat64";
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new GrantedEffectsDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading Id
            (var idLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading IsSupport
            (var issupportLoading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading AllowedActiveSkillTypes
            (var tempallowedactiveskilltypesLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var allowedactiveskilltypesLoading = tempallowedactiveskilltypesLoading.AsReadOnly();

            // loading SupportGemLetter
            (var supportgemletterLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Attribute
            (var attributeLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading AddedActiveSkillTypes
            (var tempaddedactiveskilltypesLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var addedactiveskilltypesLoading = tempaddedactiveskilltypesLoading.AsReadOnly();

            // loading ExcludedActiveSkillTypes
            (var tempexcludedactiveskilltypesLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var excludedactiveskilltypesLoading = tempexcludedactiveskilltypesLoading.AsReadOnly();

            // loading SupportsGemsOnly
            (var supportsgemsonlyLoading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown70
            (var unknown70Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown74
            (var tempunknown74Loading, offset) = SpecificationFileLoader.LoadIntArray(decompressedFile, offset, dataOffset);
            var unknown74Loading = tempunknown74Loading.AsReadOnly();

            // loading CannotBeSupported
            (var cannotbesupportedLoading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown91
            (var unknown91Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading CastTime
            (var casttimeLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading ActiveSkill
            (var activeskillLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading IgnoreMinionTypes
            (var ignoreminiontypesLoading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown116
            (var unknown116Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading AddedMinionActiveSkillTypes
            (var tempaddedminionactiveskilltypesLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var addedminionactiveskilltypesLoading = tempaddedminionactiveskilltypesLoading.AsReadOnly();

            // loading Animation
            (var animationLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading MultiPartAchievement
            (var multipartachievementLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading Unknown165
            (var unknown165Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading SupportWeaponRestrictions
            (var tempsupportweaponrestrictionsLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var supportweaponrestrictionsLoading = tempsupportweaponrestrictionsLoading.AsReadOnly();

            // loading RegularVariant
            (var regularvariantLoading, offset) = SpecificationFileLoader.LoadRowPrimaryKey(decompressedFile, offset);

            // loading Unknown190
            (var unknown190Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown194
            (var unknown194Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown198
            (var unknown198Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown202
            (var unknown202Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading StatSet
            (var statsetLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading Unknown219
            (var tempunknown219Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var unknown219Loading = tempunknown219Loading.AsReadOnly();

            if (offset != expectedOffset)
            {
                throw new NotImplementedException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new GrantedEffectsDat()
            {
                Id = idLoading,
                IsSupport = issupportLoading,
                AllowedActiveSkillTypes = allowedactiveskilltypesLoading,
                SupportGemLetter = supportgemletterLoading,
                Attribute = attributeLoading,
                AddedActiveSkillTypes = addedactiveskilltypesLoading,
                ExcludedActiveSkillTypes = excludedactiveskilltypesLoading,
                SupportsGemsOnly = supportsgemsonlyLoading,
                Unknown70 = unknown70Loading,
                Unknown74 = unknown74Loading,
                CannotBeSupported = cannotbesupportedLoading,
                Unknown91 = unknown91Loading,
                CastTime = casttimeLoading,
                ActiveSkill = activeskillLoading,
                IgnoreMinionTypes = ignoreminiontypesLoading,
                Unknown116 = unknown116Loading,
                AddedMinionActiveSkillTypes = addedminionactiveskilltypesLoading,
                Animation = animationLoading,
                MultiPartAchievement = multipartachievementLoading,
                Unknown165 = unknown165Loading,
                SupportWeaponRestrictions = supportweaponrestrictionsLoading,
                RegularVariant = regularvariantLoading,
                Unknown190 = unknown190Loading,
                Unknown194 = unknown194Loading,
                Unknown198 = unknown198Loading,
                Unknown202 = unknown202Loading,
                StatSet = statsetLoading,
                Unknown219 = unknown219Loading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
