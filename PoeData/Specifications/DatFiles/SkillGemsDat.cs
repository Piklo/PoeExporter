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
    /// <remarks> references <see cref="BaseItemTypesDat"/> on <see cref="Specification.GetBaseItemTypesDat"/> index.</remarks>
    public required int? BaseItemTypesKey { get; init; }

    /// <summary> Gets GrantedEffectsKey.</summary>
    /// <remarks> references <see cref="GrantedEffectsDat"/> on <see cref="Specification.GetGrantedEffectsDat"/> index.</remarks>
    public required int? GrantedEffectsKey { get; init; }

    /// <summary> Gets Str.</summary>
    public required int Str { get; init; }

    /// <summary> Gets Dex.</summary>
    public required int Dex { get; init; }

    /// <summary> Gets Int.</summary>
    public required int Int { get; init; }

    /// <summary> Gets GemTagsKeys.</summary>
    /// <remarks> references <see cref="GemTagsDat"/> on <see cref="Specification.GetGemTagsDat"/> index.</remarks>
    public required ReadOnlyCollection<int> GemTagsKeys { get; init; }

    /// <summary> Gets VaalVariant_BaseItemTypesKey.</summary>
    /// <remarks> references <see cref="BaseItemTypesDat"/> on <see cref="Specification.GetBaseItemTypesDat"/> index.</remarks>
    public required int? VaalVariant_BaseItemTypesKey { get; init; }

    /// <summary> Gets a value indicating whether IsVaalVariant is set.</summary>
    public required bool IsVaalVariant { get; init; }

    /// <summary> Gets Description.</summary>
    public required string Description { get; init; }

    /// <summary> Gets Consumed_ModsKey.</summary>
    /// <remarks> references <see cref="ModsDat"/> on <see cref="Specification.GetModsDat"/> index.</remarks>
    public required int? Consumed_ModsKey { get; init; }

    /// <summary> Gets GrantedEffectsKey2.</summary>
    /// <remarks> references <see cref="GrantedEffectsDat"/> on <see cref="Specification.GetGrantedEffectsDat"/> index.</remarks>
    public required int? GrantedEffectsKey2 { get; init; }

    /// <summary> Gets MinionGlobalSkillLevelStat.</summary>
    /// <remarks> references <see cref="StatsDat"/> on <see cref="Specification.GetStatsDat"/> index.</remarks>
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
    /// <remarks> references <see cref="SkillGemsDat"/> on <see cref="Specification.GetSkillGemsDat"/> index.</remarks>
    public required int? AwakenedVariant { get; init; }

    /// <summary> Gets RegularVariant.</summary>
    /// <remarks> references <see cref="SkillGemsDat"/> on <see cref="Specification.GetSkillGemsDat"/> index.</remarks>
    public required int? RegularVariant { get; init; }

    /// <summary> Gets GrantedEffectHardMode.</summary>
    /// <remarks> references <see cref="GrantedEffectsDat"/> on <see cref="Specification.GetGrantedEffectsDat"/> index.</remarks>
    public required int? GrantedEffectHardMode { get; init; }

    /// <summary> Gets Unknown179.</summary>
    public required int? Unknown179 { get; init; }

    /// <summary> Gets Unknown195.</summary>
    public required int Unknown195 { get; init; }

    /// <summary> Gets ItemExperienceType.</summary>
    /// <remarks> references <see cref="ItemExperienceTypesDat"/> on <see cref="Specification.GetItemExperienceTypesDat"/> index.</remarks>
    public required int? ItemExperienceType { get; init; }

    /// <summary>
    /// Gets SkillGemsDat data.
    /// </summary>
    /// <param name="dataLoader">data loader.</param>
    /// <returns>array of SkillGemsDat.</returns>
    internal static SkillGemsDat[] Load(DataLoader dataLoader)
    {
        if (dataLoader is null)
        {
            throw new ArgumentNullException(nameof(dataLoader));
        }

        const string filePath = "Data/SkillGems.dat64";
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new SkillGemsDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading BaseItemTypesKey
            (var baseitemtypeskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading GrantedEffectsKey
            (var grantedeffectskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading Str
            (var strLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Dex
            (var dexLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Int
            (var intLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading GemTagsKeys
            (var tempgemtagskeysLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var gemtagskeysLoading = tempgemtagskeysLoading.AsReadOnly();

            // loading VaalVariant_BaseItemTypesKey
            (var vaalvariant_baseitemtypeskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading IsVaalVariant
            (var isvaalvariantLoading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Description
            (var descriptionLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Consumed_ModsKey
            (var consumed_modskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading GrantedEffectsKey2
            (var grantedeffectskey2Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading MinionGlobalSkillLevelStat
            (var minionglobalskilllevelstatLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading SupportSkillName
            (var supportskillnameLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading IsSupport
            (var issupportLoading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown142
            (var unknown142Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown143
            (var unknown143Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown144
            (var unknown144Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown145
            (var unknown145Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown146
            (var unknown146Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading AwakenedVariant
            (var awakenedvariantLoading, offset) = SpecificationFileLoader.LoadRowPrimaryKey(decompressedFile, offset);

            // loading RegularVariant
            (var regularvariantLoading, offset) = SpecificationFileLoader.LoadRowPrimaryKey(decompressedFile, offset);

            // loading GrantedEffectHardMode
            (var grantedeffecthardmodeLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading Unknown179
            (var unknown179Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading Unknown195
            (var unknown195Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading ItemExperienceType
            (var itemexperiencetypeLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            if (offset != expectedOffset)
            {
                throw new SchemaMismatchException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new SkillGemsDat()
            {
                BaseItemTypesKey = baseitemtypeskeyLoading,
                GrantedEffectsKey = grantedeffectskeyLoading,
                Str = strLoading,
                Dex = dexLoading,
                Int = intLoading,
                GemTagsKeys = gemtagskeysLoading,
                VaalVariant_BaseItemTypesKey = vaalvariant_baseitemtypeskeyLoading,
                IsVaalVariant = isvaalvariantLoading,
                Description = descriptionLoading,
                Consumed_ModsKey = consumed_modskeyLoading,
                GrantedEffectsKey2 = grantedeffectskey2Loading,
                MinionGlobalSkillLevelStat = minionglobalskilllevelstatLoading,
                SupportSkillName = supportskillnameLoading,
                IsSupport = issupportLoading,
                Unknown142 = unknown142Loading,
                Unknown143 = unknown143Loading,
                Unknown144 = unknown144Loading,
                Unknown145 = unknown145Loading,
                Unknown146 = unknown146Loading,
                AwakenedVariant = awakenedvariantLoading,
                RegularVariant = regularvariantLoading,
                GrantedEffectHardMode = grantedeffecthardmodeLoading,
                Unknown179 = unknown179Loading,
                Unknown195 = unknown195Loading,
                ItemExperienceType = itemexperiencetypeLoading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
