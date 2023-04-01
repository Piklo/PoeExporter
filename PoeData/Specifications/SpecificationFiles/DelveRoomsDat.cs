// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.SpecificationFiles;

/// <summary>
/// Class containing DelveRooms.dat data.
/// </summary>
public sealed partial class DelveRoomsDat : ISpecificationFile<DelveRoomsDat>
{
    /// <summary> Gets DelveBiomesKey.</summary>
    public required int? DelveBiomesKey { get; init; }

    /// <summary> Gets DelveFeaturesKey.</summary>
    public required int? DelveFeaturesKey { get; init; }

    /// <summary> Gets ARMFile.</summary>
    public required string ARMFile { get; init; }

    /// <inheritdoc/>
    public static DelveRoomsDat[] Load(Specification specification)
    {
        if (specification is null)
        {
            throw new ArgumentNullException(nameof(specification));
        }

        var fileToFind = Encoding.ASCII.GetBytes("Data/DelveRooms.dat64");
        var fileRecord = specification.DataLoader.GetFileRecord(fileToFind);
        var decompressedFile = specification.DataLoader.GetFileBytes(fileRecord);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new DelveRoomsDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading referenced tables if any
            // specification.GetDelveBiomesDat();
            // specification.GetDelveFeaturesDat();

            // loading DelveBiomesKey
            (var delvebiomeskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading DelveFeaturesKey
            (var delvefeatureskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading ARMFile
            (var armfileLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            if (offset != expectedOffset)
            {
                throw new NotImplementedException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new DelveRoomsDat()
            {
                DelveBiomesKey = delvebiomeskeyLoading,
                DelveFeaturesKey = delvefeatureskeyLoading,
                ARMFile = armfileLoading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
