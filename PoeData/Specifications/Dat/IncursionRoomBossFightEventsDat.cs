// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.Dat;

/// <summary>
/// Class containing IncursionRoomBossFightEvents.dat data.
/// </summary>
public sealed partial class IncursionRoomBossFightEventsDat : IDat<IncursionRoomBossFightEventsDat>
{
    /// <summary> Gets Room.</summary>
    /// <remarks> references <see cref="IncursionRoomsDat"/> on <see cref="Specification.GetIncursionRoomsDat"/> index.</remarks>
    public required int? Room { get; init; }

    /// <summary> Gets Unknown16.</summary>
    public required string Unknown16 { get; init; }

    /// <summary> Gets Unknown24.</summary>
    public required string Unknown24 { get; init; }

    /// <summary> Gets Unknown32.</summary>
    public required string Unknown32 { get; init; }

    /// <summary> Gets Unknown40.</summary>
    public required string Unknown40 { get; init; }

    /// <summary> Gets Unknown48.</summary>
    public required string Unknown48 { get; init; }

    /// <summary> Gets Unknown56.</summary>
    public required int? Unknown56 { get; init; }

    /// <inheritdoc/>
    public static IncursionRoomBossFightEventsDat[] Load(Specification specification)
    {
        if (specification is null)
        {
            throw new ArgumentNullException(nameof(specification));
        }

        const string filePath = "Data/IncursionRoomBossFightEvents.dat64";
        var decompressedFile = specification.DataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new IncursionRoomBossFightEventsDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading Room
            (var roomLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading Unknown16
            (var unknown16Loading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Unknown24
            (var unknown24Loading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Unknown32
            (var unknown32Loading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Unknown40
            (var unknown40Loading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Unknown48
            (var unknown48Loading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Unknown56
            (var unknown56Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            if (offset != expectedOffset)
            {
                throw new NotImplementedException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new IncursionRoomBossFightEventsDat()
            {
                Room = roomLoading,
                Unknown16 = unknown16Loading,
                Unknown24 = unknown24Loading,
                Unknown32 = unknown32Loading,
                Unknown40 = unknown40Loading,
                Unknown48 = unknown48Loading,
                Unknown56 = unknown56Loading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
