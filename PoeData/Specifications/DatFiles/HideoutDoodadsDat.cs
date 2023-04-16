// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.DatFiles;

/// <summary>
/// Class containing HideoutDoodads.dat data.
/// </summary>
public sealed partial class HideoutDoodadsDat
{
    /// <summary> Gets BaseItemTypesKey.</summary>
    /// <remarks> references <see cref="BaseItemTypesDat"/> on <see cref="Specification.GetBaseItemTypesDat"/> index.</remarks>
    public required int? BaseItemTypesKey { get; init; }

    /// <summary> Gets Variation_AOFiles.</summary>
    public required ReadOnlyCollection<string> Variation_AOFiles { get; init; }

    /// <summary> Gets a value indicating whether IsNonMasterDoodad is set.</summary>
    public required bool IsNonMasterDoodad { get; init; }

    /// <summary> Gets InheritsFrom.</summary>
    public required string InheritsFrom { get; init; }

    /// <summary> Gets a value indicating whether Unknown41 is set.</summary>
    public required bool Unknown41 { get; init; }

    /// <summary> Gets a value indicating whether IsCraftingBench is set.</summary>
    public required bool IsCraftingBench { get; init; }

    /// <summary> Gets Tags.</summary>
    /// <remarks> references <see cref="HideoutDoodadTagsDat"/> on <see cref="Specification.GetHideoutDoodadTagsDat"/> index.</remarks>
    public required ReadOnlyCollection<int> Tags { get; init; }

    /// <summary> Gets a value indicating whether Unknown59 is set.</summary>
    public required bool Unknown59 { get; init; }

    /// <summary> Gets Unknown60.</summary>
    public required int? Unknown60 { get; init; }

    /// <summary> Gets Category.</summary>
    /// <remarks> references <see cref="HideoutDoodadCategoryDat"/> on <see cref="Specification.GetHideoutDoodadCategoryDat"/> index.</remarks>
    public required int? Category { get; init; }

    /// <summary> Gets Unknown92.</summary>
    public required int Unknown92 { get; init; }

    /// <summary> Gets a value indicating whether Unknown96 is set.</summary>
    public required bool Unknown96 { get; init; }

    /// <summary> Gets Unknown97.</summary>
    public required int? Unknown97 { get; init; }

    /// <summary> Gets a value indicating whether Unknown113 is set.</summary>
    public required bool Unknown113 { get; init; }

    /// <summary> Gets Unknown114.</summary>
    public required int? Unknown114 { get; init; }

    /// <summary>
    /// Gets HideoutDoodadsDat data.
    /// </summary>
    /// <param name="dataLoader">data loader.</param>
    /// <returns>array of HideoutDoodadsDat.</returns>
    internal static HideoutDoodadsDat[] Load(DataLoader dataLoader)
    {
        if (dataLoader is null)
        {
            throw new ArgumentNullException(nameof(dataLoader));
        }

        const string filePath = "Data/HideoutDoodads.dat64";
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new HideoutDoodadsDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading BaseItemTypesKey
            (var baseitemtypeskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading Variation_AOFiles
            (var tempvariation_aofilesLoading, offset) = SpecificationFileLoader.LoadStringArray(decompressedFile, offset, dataOffset);
            var variation_aofilesLoading = tempvariation_aofilesLoading.AsReadOnly();

            // loading IsNonMasterDoodad
            (var isnonmasterdoodadLoading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading InheritsFrom
            (var inheritsfromLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Unknown41
            (var unknown41Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading IsCraftingBench
            (var iscraftingbenchLoading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Tags
            (var temptagsLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var tagsLoading = temptagsLoading.AsReadOnly();

            // loading Unknown59
            (var unknown59Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown60
            (var unknown60Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading Category
            (var categoryLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading Unknown92
            (var unknown92Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown96
            (var unknown96Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown97
            (var unknown97Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading Unknown113
            (var unknown113Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown114
            (var unknown114Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            if (offset != expectedOffset)
            {
                throw new NotImplementedException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new HideoutDoodadsDat()
            {
                BaseItemTypesKey = baseitemtypeskeyLoading,
                Variation_AOFiles = variation_aofilesLoading,
                IsNonMasterDoodad = isnonmasterdoodadLoading,
                InheritsFrom = inheritsfromLoading,
                Unknown41 = unknown41Loading,
                IsCraftingBench = iscraftingbenchLoading,
                Tags = tagsLoading,
                Unknown59 = unknown59Loading,
                Unknown60 = unknown60Loading,
                Category = categoryLoading,
                Unknown92 = unknown92Loading,
                Unknown96 = unknown96Loading,
                Unknown97 = unknown97Loading,
                Unknown113 = unknown113Loading,
                Unknown114 = unknown114Loading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
