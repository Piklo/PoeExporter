// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.DatFiles;

/// <summary>
/// Class containing DelveCraftingModifiers.dat data.
/// </summary>
public sealed partial class DelveCraftingModifiersDat
{
    /// <summary> Gets BaseItemTypesKey.</summary>
    /// <remarks> references <see cref="BaseItemTypesDat"/> on <see cref="Specification.GetBaseItemTypesDat"/> index.</remarks>
    public required int? BaseItemTypesKey { get; init; }

    /// <summary> Gets AddedModsKeys.</summary>
    /// <remarks> references <see cref="ModsDat"/> on <see cref="Specification.GetModsDat"/> index.</remarks>
    public required ReadOnlyCollection<int> AddedModsKeys { get; init; }

    /// <summary> Gets NegativeWeight_TagsKeys.</summary>
    /// <remarks> references <see cref="TagsDat"/> on <see cref="Specification.GetTagsDat"/> index.</remarks>
    public required ReadOnlyCollection<int> NegativeWeight_TagsKeys { get; init; }

    /// <summary> Gets NegativeWeight_Values.</summary>
    public required ReadOnlyCollection<int> NegativeWeight_Values { get; init; }

    /// <summary> Gets ForcedAddModsKeys.</summary>
    /// <remarks> references <see cref="ModsDat"/> on <see cref="Specification.GetModsDat"/> index.</remarks>
    public required ReadOnlyCollection<int> ForcedAddModsKeys { get; init; }

    /// <summary> Gets ForbiddenDelveCraftingTagsKeys.</summary>
    /// <remarks> references <see cref="DelveCraftingTagsDat"/> on <see cref="Specification.GetDelveCraftingTagsDat"/> index.</remarks>
    public required ReadOnlyCollection<int> ForbiddenDelveCraftingTagsKeys { get; init; }

    /// <summary> Gets AllowedDelveCraftingTagsKeys.</summary>
    /// <remarks> references <see cref="DelveCraftingTagsDat"/> on <see cref="Specification.GetDelveCraftingTagsDat"/> index.</remarks>
    public required ReadOnlyCollection<int> AllowedDelveCraftingTagsKeys { get; init; }

    /// <summary> Gets a value indicating whether CanMirrorItem is set.</summary>
    public required bool CanMirrorItem { get; init; }

    /// <summary> Gets CorruptedEssenceChance.</summary>
    public required int CorruptedEssenceChance { get; init; }

    /// <summary> Gets a value indicating whether CanImproveQuality is set.</summary>
    public required bool CanImproveQuality { get; init; }

    /// <summary> Gets a value indicating whether CanRollEnchant is set.</summary>
    public required bool CanRollEnchant { get; init; }

    /// <summary> Gets a value indicating whether HasLuckyRolls is set.</summary>
    public required bool HasLuckyRolls { get; init; }

    /// <summary> Gets SellPrice_ModsKeys.</summary>
    /// <remarks> references <see cref="ModsDat"/> on <see cref="Specification.GetModsDat"/> index.</remarks>
    public required ReadOnlyCollection<int> SellPrice_ModsKeys { get; init; }

    /// <summary> Gets a value indicating whether CanRollWhiteSockets is set.</summary>
    public required bool CanRollWhiteSockets { get; init; }

    /// <summary> Gets Weight_TagsKeys.</summary>
    /// <remarks> references <see cref="TagsDat"/> on <see cref="Specification.GetTagsDat"/> index.</remarks>
    public required ReadOnlyCollection<int> Weight_TagsKeys { get; init; }

    /// <summary> Gets Weight_Values.</summary>
    public required ReadOnlyCollection<int> Weight_Values { get; init; }

    /// <summary> Gets DelveCraftingModifierDescriptionsKeys.</summary>
    /// <remarks> references <see cref="DelveCraftingModifierDescriptionsDat"/> on <see cref="Specification.GetDelveCraftingModifierDescriptionsDat"/> index.</remarks>
    public required ReadOnlyCollection<int> DelveCraftingModifierDescriptionsKeys { get; init; }

    /// <summary> Gets BlockedDelveCraftingModifierDescriptionsKeys.</summary>
    /// <remarks> references <see cref="DelveCraftingModifierDescriptionsDat"/> on <see cref="Specification.GetDelveCraftingModifierDescriptionsDat"/> index.</remarks>
    public required ReadOnlyCollection<int> BlockedDelveCraftingModifierDescriptionsKeys { get; init; }

    /// <summary> Gets a value indicating whether Unknown201 is set.</summary>
    public required bool Unknown201 { get; init; }

    /// <summary> Gets a value indicating whether Unknown202 is set.</summary>
    public required bool Unknown202 { get; init; }

    /// <summary> Gets Unknown203.</summary>
    public required ReadOnlyCollection<int> Unknown203 { get; init; }

    /// <summary> Gets Unknown219.</summary>
    public required ReadOnlyCollection<int> Unknown219 { get; init; }

    /// <summary>
    /// Gets DelveCraftingModifiersDat data.
    /// </summary>
    /// <param name="dataLoader">data loader.</param>
    /// <returns>array of DelveCraftingModifiersDat.</returns>
    internal static DelveCraftingModifiersDat[] Load(DataLoader dataLoader)
    {
        if (dataLoader is null)
        {
            throw new ArgumentNullException(nameof(dataLoader));
        }

        const string filePath = "Data/DelveCraftingModifiers.dat64";
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new DelveCraftingModifiersDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading BaseItemTypesKey
            (var baseitemtypeskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading AddedModsKeys
            (var tempaddedmodskeysLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var addedmodskeysLoading = tempaddedmodskeysLoading.AsReadOnly();

            // loading NegativeWeight_TagsKeys
            (var tempnegativeweight_tagskeysLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var negativeweight_tagskeysLoading = tempnegativeweight_tagskeysLoading.AsReadOnly();

            // loading NegativeWeight_Values
            (var tempnegativeweight_valuesLoading, offset) = SpecificationFileLoader.LoadIntArray(decompressedFile, offset, dataOffset);
            var negativeweight_valuesLoading = tempnegativeweight_valuesLoading.AsReadOnly();

            // loading ForcedAddModsKeys
            (var tempforcedaddmodskeysLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var forcedaddmodskeysLoading = tempforcedaddmodskeysLoading.AsReadOnly();

            // loading ForbiddenDelveCraftingTagsKeys
            (var tempforbiddendelvecraftingtagskeysLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var forbiddendelvecraftingtagskeysLoading = tempforbiddendelvecraftingtagskeysLoading.AsReadOnly();

            // loading AllowedDelveCraftingTagsKeys
            (var tempalloweddelvecraftingtagskeysLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var alloweddelvecraftingtagskeysLoading = tempalloweddelvecraftingtagskeysLoading.AsReadOnly();

            // loading CanMirrorItem
            (var canmirroritemLoading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading CorruptedEssenceChance
            (var corruptedessencechanceLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading CanImproveQuality
            (var canimprovequalityLoading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading CanRollEnchant
            (var canrollenchantLoading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading HasLuckyRolls
            (var hasluckyrollsLoading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading SellPrice_ModsKeys
            (var tempsellprice_modskeysLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var sellprice_modskeysLoading = tempsellprice_modskeysLoading.AsReadOnly();

            // loading CanRollWhiteSockets
            (var canrollwhitesocketsLoading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Weight_TagsKeys
            (var tempweight_tagskeysLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var weight_tagskeysLoading = tempweight_tagskeysLoading.AsReadOnly();

            // loading Weight_Values
            (var tempweight_valuesLoading, offset) = SpecificationFileLoader.LoadIntArray(decompressedFile, offset, dataOffset);
            var weight_valuesLoading = tempweight_valuesLoading.AsReadOnly();

            // loading DelveCraftingModifierDescriptionsKeys
            (var tempdelvecraftingmodifierdescriptionskeysLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var delvecraftingmodifierdescriptionskeysLoading = tempdelvecraftingmodifierdescriptionskeysLoading.AsReadOnly();

            // loading BlockedDelveCraftingModifierDescriptionsKeys
            (var tempblockeddelvecraftingmodifierdescriptionskeysLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var blockeddelvecraftingmodifierdescriptionskeysLoading = tempblockeddelvecraftingmodifierdescriptionskeysLoading.AsReadOnly();

            // loading Unknown201
            (var unknown201Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown202
            (var unknown202Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown203
            (var tempunknown203Loading, offset) = SpecificationFileLoader.LoadIntArray(decompressedFile, offset, dataOffset);
            var unknown203Loading = tempunknown203Loading.AsReadOnly();

            // loading Unknown219
            (var tempunknown219Loading, offset) = SpecificationFileLoader.LoadIntArray(decompressedFile, offset, dataOffset);
            var unknown219Loading = tempunknown219Loading.AsReadOnly();

            if (offset != expectedOffset)
            {
                throw new NotImplementedException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new DelveCraftingModifiersDat()
            {
                BaseItemTypesKey = baseitemtypeskeyLoading,
                AddedModsKeys = addedmodskeysLoading,
                NegativeWeight_TagsKeys = negativeweight_tagskeysLoading,
                NegativeWeight_Values = negativeweight_valuesLoading,
                ForcedAddModsKeys = forcedaddmodskeysLoading,
                ForbiddenDelveCraftingTagsKeys = forbiddendelvecraftingtagskeysLoading,
                AllowedDelveCraftingTagsKeys = alloweddelvecraftingtagskeysLoading,
                CanMirrorItem = canmirroritemLoading,
                CorruptedEssenceChance = corruptedessencechanceLoading,
                CanImproveQuality = canimprovequalityLoading,
                CanRollEnchant = canrollenchantLoading,
                HasLuckyRolls = hasluckyrollsLoading,
                SellPrice_ModsKeys = sellprice_modskeysLoading,
                CanRollWhiteSockets = canrollwhitesocketsLoading,
                Weight_TagsKeys = weight_tagskeysLoading,
                Weight_Values = weight_valuesLoading,
                DelveCraftingModifierDescriptionsKeys = delvecraftingmodifierdescriptionskeysLoading,
                BlockedDelveCraftingModifierDescriptionsKeys = blockeddelvecraftingmodifierdescriptionskeysLoading,
                Unknown201 = unknown201Loading,
                Unknown202 = unknown202Loading,
                Unknown203 = unknown203Loading,
                Unknown219 = unknown219Loading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
