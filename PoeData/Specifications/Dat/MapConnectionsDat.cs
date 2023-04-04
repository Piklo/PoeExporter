// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.Dat;

/// <summary>
/// Class containing MapConnections.dat data.
/// </summary>
public sealed partial class MapConnectionsDat : ISpecificationFile<MapConnectionsDat>
{
    /// <summary> Gets MapPinsKey0.</summary>
    /// <remarks> references <see cref="MapPinsDat"/> on <see cref="Specification.GetMapPinsDat"/> index.</remarks>
    public required int? MapPinsKey0 { get; init; }

    /// <summary> Gets MapPinsKey1.</summary>
    /// <remarks> references <see cref="MapPinsDat"/> on <see cref="Specification.GetMapPinsDat"/> index.</remarks>
    public required int? MapPinsKey1 { get; init; }

    /// <summary> Gets Unknown32.</summary>
    public required int? Unknown32 { get; init; }

    /// <summary> Gets RestrictedAreaText.</summary>
    public required string RestrictedAreaText { get; init; }

    /// <summary> Gets Unknown56.</summary>
    public required int? Unknown56 { get; init; }

    /// <summary> Gets Unknown72.</summary>
    public required int? Unknown72 { get; init; }

    /// <summary> Gets Unknown88.</summary>
    public required int? Unknown88 { get; init; }

    /// <summary> Gets Unknown104.</summary>
    public required ReadOnlyCollection<int> Unknown104 { get; init; }

    /// <inheritdoc/>
    public static MapConnectionsDat[] Load(Specification specification)
    {
        if (specification is null)
        {
            throw new ArgumentNullException(nameof(specification));
        }

        var fileToFind = Encoding.ASCII.GetBytes("Data/MapConnections.dat64");
        var fileRecord = specification.DataLoader.GetFileRecord(fileToFind);
        var decompressedFile = specification.DataLoader.GetFileBytes(fileRecord);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new MapConnectionsDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading MapPinsKey0
            (var mappinskey0Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading MapPinsKey1
            (var mappinskey1Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading Unknown32
            (var unknown32Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading RestrictedAreaText
            (var restrictedareatextLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Unknown56
            (var unknown56Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading Unknown72
            (var unknown72Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading Unknown88
            (var unknown88Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading Unknown104
            (var tempunknown104Loading, offset) = SpecificationFileLoader.LoadIntArray(decompressedFile, offset, dataOffset);
            var unknown104Loading = tempunknown104Loading.AsReadOnly();

            if (offset != expectedOffset)
            {
                throw new NotImplementedException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new MapConnectionsDat()
            {
                MapPinsKey0 = mappinskey0Loading,
                MapPinsKey1 = mappinskey1Loading,
                Unknown32 = unknown32Loading,
                RestrictedAreaText = restrictedareatextLoading,
                Unknown56 = unknown56Loading,
                Unknown72 = unknown72Loading,
                Unknown88 = unknown88Loading,
                Unknown104 = unknown104Loading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
