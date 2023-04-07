// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.Dat;

/// <summary>
/// Class containing UltimatumEncounters.dat data.
/// </summary>
public sealed partial class UltimatumEncountersDat
{
    /// <summary> Gets Id.</summary>
    public required string Id { get; init; }

    /// <summary> Gets Description.</summary>
    public required string Description { get; init; }

    /// <summary> Gets ModTypes.</summary>
    /// <remarks> references <see cref="UltimatumModifierTypesDat"/> on <see cref="Specification.GetUltimatumModifierTypesDat"/> index.</remarks>
    public required ReadOnlyCollection<int> ModTypes { get; init; }

    /// <summary> Gets BossARMFile.</summary>
    public required string BossARMFile { get; init; }

    /// <summary> Gets Type.</summary>
    /// <remarks> references <see cref="UltimatumEncounterTypesDat"/> on <see cref="Specification.GetUltimatumEncounterTypesDat"/> index.</remarks>
    public required int? Type { get; init; }

    /// <summary> Gets Icon.</summary>
    public required string Icon { get; init; }

    /// <summary> Gets HASH16.</summary>
    public required int HASH16 { get; init; }

    /// <summary> Gets a value indicating whether Unknown68 is set.</summary>
    public required bool Unknown68 { get; init; }

    /// <summary> Gets Unknown69.</summary>
    public required int Unknown69 { get; init; }

    /// <inheritdoc/>
    public static UltimatumEncountersDat[] Load(Specification specification)
    {
        if (specification is null)
        {
            throw new ArgumentNullException(nameof(specification));
        }

        const string filePath = "Data/UltimatumEncounters.dat64";
        var decompressedFile = specification.DataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new UltimatumEncountersDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading Id
            (var idLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Description
            (var descriptionLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading ModTypes
            (var tempmodtypesLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var modtypesLoading = tempmodtypesLoading.AsReadOnly();

            // loading BossARMFile
            (var bossarmfileLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Type
            (var typeLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading Icon
            (var iconLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading HASH16
            (var hash16Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown68
            (var unknown68Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown69
            (var unknown69Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            if (offset != expectedOffset)
            {
                throw new NotImplementedException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new UltimatumEncountersDat()
            {
                Id = idLoading,
                Description = descriptionLoading,
                ModTypes = modtypesLoading,
                BossARMFile = bossarmfileLoading,
                Type = typeLoading,
                Icon = iconLoading,
                HASH16 = hash16Loading,
                Unknown68 = unknown68Loading,
                Unknown69 = unknown69Loading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
