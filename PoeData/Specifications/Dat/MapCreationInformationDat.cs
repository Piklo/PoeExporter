// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.Dat;

/// <summary>
/// Class containing MapCreationInformation.dat data.
/// </summary>
public sealed partial class MapCreationInformationDat : ISpecificationFile<MapCreationInformationDat>
{
    /// <summary> Gets MapsKey.</summary>
    /// <remarks> references <see cref="MapsDat"/> on <see cref="Specification.GetMapsDat"/> index.</remarks>
    public required int? MapsKey { get; init; }

    /// <summary> Gets Tier.</summary>
    public required int Tier { get; init; }

    /// <inheritdoc/>
    public static MapCreationInformationDat[] Load(Specification specification)
    {
        if (specification is null)
        {
            throw new ArgumentNullException(nameof(specification));
        }

        var fileToFind = Encoding.ASCII.GetBytes("Data/MapCreationInformation.dat64");
        var fileRecord = specification.DataLoader.GetFileRecord(fileToFind);
        var decompressedFile = specification.DataLoader.GetFileBytes(fileRecord);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new MapCreationInformationDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading MapsKey
            (var mapskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading Tier
            (var tierLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            if (offset != expectedOffset)
            {
                throw new NotImplementedException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new MapCreationInformationDat()
            {
                MapsKey = mapskeyLoading,
                Tier = tierLoading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
