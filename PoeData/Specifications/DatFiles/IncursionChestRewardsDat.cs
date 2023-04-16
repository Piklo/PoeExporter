// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.DatFiles;

/// <summary>
/// Class containing IncursionChestRewards.dat data.
/// </summary>
public sealed partial class IncursionChestRewardsDat
{
    /// <summary> Gets IncursionRoomsKey.</summary>
    /// <remarks> references <see cref="IncursionRoomsDat"/> on <see cref="Specification.LoadIncursionRoomsDat"/> index.</remarks>
    public required int? IncursionRoomsKey { get; init; }

    /// <summary> Gets IncursionChestsKeys.</summary>
    /// <remarks> references <see cref="IncursionChestsDat"/> on <see cref="Specification.LoadIncursionChestsDat"/> index.</remarks>
    public required ReadOnlyCollection<int> IncursionChestsKeys { get; init; }

    /// <summary> Gets ChestMarkerMetadata.</summary>
    public required string ChestMarkerMetadata { get; init; }

    /// <summary> Gets Unknown40.</summary>
    public required int Unknown40 { get; init; }

    /// <summary> Gets Unknown44.</summary>
    public required int Unknown44 { get; init; }

    /// <summary> Gets Unknown48.</summary>
    public required int Unknown48 { get; init; }

    /// <summary>
    /// Gets IncursionChestRewardsDat data.
    /// </summary>
    /// <param name="dataLoader">data loader.</param>
    /// <returns>array of IncursionChestRewardsDat.</returns>
    internal static IncursionChestRewardsDat[] Load(DataLoader dataLoader)
    {
        if (dataLoader is null)
        {
            throw new ArgumentNullException(nameof(dataLoader));
        }

        const string filePath = "Data/IncursionChestRewards.dat64";
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new IncursionChestRewardsDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading IncursionRoomsKey
            (var incursionroomskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading IncursionChestsKeys
            (var tempincursionchestskeysLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var incursionchestskeysLoading = tempincursionchestskeysLoading.AsReadOnly();

            // loading ChestMarkerMetadata
            (var chestmarkermetadataLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Unknown40
            (var unknown40Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown44
            (var unknown44Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown48
            (var unknown48Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            if (offset != expectedOffset)
            {
                throw new SchemaMismatchException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new IncursionChestRewardsDat()
            {
                IncursionRoomsKey = incursionroomskeyLoading,
                IncursionChestsKeys = incursionchestskeysLoading,
                ChestMarkerMetadata = chestmarkermetadataLoading,
                Unknown40 = unknown40Loading,
                Unknown44 = unknown44Loading,
                Unknown48 = unknown48Loading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
