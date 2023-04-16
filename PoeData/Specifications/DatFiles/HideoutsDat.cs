// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.DatFiles;

/// <summary>
/// Class containing Hideouts.dat data.
/// </summary>
public sealed partial class HideoutsDat
{
    /// <summary> Gets Id.</summary>
    public required string Id { get; init; }

    /// <summary> Gets HideoutArea.</summary>
    /// <remarks> references <see cref="WorldAreasDat"/> on <see cref="Specification.LoadWorldAreasDat"/> index.</remarks>
    public required int? HideoutArea { get; init; }

    /// <summary> Gets HASH16.</summary>
    public required int HASH16 { get; init; }

    /// <summary> Gets HideoutFile.</summary>
    public required string HideoutFile { get; init; }

    /// <summary> Gets SpawnAreas.</summary>
    /// <remarks> references <see cref="WorldAreasDat"/> on <see cref="Specification.LoadWorldAreasDat"/> index.</remarks>
    public required ReadOnlyCollection<int> SpawnAreas { get; init; }

    /// <summary> Gets ClaimSideArea.</summary>
    /// <remarks> references <see cref="WorldAreasDat"/> on <see cref="Specification.LoadWorldAreasDat"/> index.</remarks>
    public required int? ClaimSideArea { get; init; }

    /// <summary> Gets HideoutImage.</summary>
    public required string HideoutImage { get; init; }

    /// <summary> Gets a value indicating whether IsEnabled is set.</summary>
    public required bool IsEnabled { get; init; }

    /// <summary> Gets Weight.</summary>
    public required int Weight { get; init; }

    /// <summary> Gets Rarity.</summary>
    /// <remarks> references <see cref="HideoutRarityDat"/> on <see cref="Specification.LoadHideoutRarityDat"/> index.</remarks>
    public required int? Rarity { get; init; }

    /// <summary> Gets a value indicating whether NotActsArea is set.</summary>
    public required bool NotActsArea { get; init; }

    /// <summary> Gets Name.</summary>
    public required string Name { get; init; }

    /// <summary> Gets Unknown106.</summary>
    public required ReadOnlyCollection<int> Unknown106 { get; init; }

    /// <summary> Gets a value indicating whether Unknown122 is set.</summary>
    public required bool Unknown122 { get; init; }

    /// <summary> Gets a value indicating whether Unknown123 is set.</summary>
    public required bool Unknown123 { get; init; }

    /// <summary> Gets a value indicating whether Unknown124 is set.</summary>
    public required bool Unknown124 { get; init; }

    /// <summary> Gets a value indicating whether Unknown125 is set.</summary>
    public required bool Unknown125 { get; init; }

    /// <summary>
    /// Gets HideoutsDat data.
    /// </summary>
    /// <param name="dataLoader">data loader.</param>
    /// <returns>array of HideoutsDat.</returns>
    internal static HideoutsDat[] Load(DataLoader dataLoader)
    {
        if (dataLoader is null)
        {
            throw new ArgumentNullException(nameof(dataLoader));
        }

        const string filePath = "Data/Hideouts.dat64";
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new HideoutsDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading Id
            (var idLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading HideoutArea
            (var hideoutareaLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading HASH16
            (var hash16Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading HideoutFile
            (var hideoutfileLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading SpawnAreas
            (var tempspawnareasLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var spawnareasLoading = tempspawnareasLoading.AsReadOnly();

            // loading ClaimSideArea
            (var claimsideareaLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading HideoutImage
            (var hideoutimageLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading IsEnabled
            (var isenabledLoading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Weight
            (var weightLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Rarity
            (var rarityLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading NotActsArea
            (var notactsareaLoading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Name
            (var nameLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Unknown106
            (var tempunknown106Loading, offset) = SpecificationFileLoader.LoadIntArray(decompressedFile, offset, dataOffset);
            var unknown106Loading = tempunknown106Loading.AsReadOnly();

            // loading Unknown122
            (var unknown122Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown123
            (var unknown123Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown124
            (var unknown124Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown125
            (var unknown125Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            if (offset != expectedOffset)
            {
                throw new SchemaMismatchException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new HideoutsDat()
            {
                Id = idLoading,
                HideoutArea = hideoutareaLoading,
                HASH16 = hash16Loading,
                HideoutFile = hideoutfileLoading,
                SpawnAreas = spawnareasLoading,
                ClaimSideArea = claimsideareaLoading,
                HideoutImage = hideoutimageLoading,
                IsEnabled = isenabledLoading,
                Weight = weightLoading,
                Rarity = rarityLoading,
                NotActsArea = notactsareaLoading,
                Name = nameLoading,
                Unknown106 = unknown106Loading,
                Unknown122 = unknown122Loading,
                Unknown123 = unknown123Loading,
                Unknown124 = unknown124Loading,
                Unknown125 = unknown125Loading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
