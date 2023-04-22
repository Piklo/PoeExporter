﻿// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.DatFiles;

/// <summary>
/// Class containing DelveRooms.dat data.
/// </summary>
public sealed partial class DelveRoomsDat
{
    /// <summary> Gets DelveBiomesKey.</summary>
    /// <remarks> references <see cref="DelveBiomesDat"/> on <see cref="Specification.LoadDelveBiomesDat"/> index.</remarks>
    public required int? DelveBiomesKey { get; init; }

    /// <summary> Gets DelveFeaturesKey.</summary>
    /// <remarks> references <see cref="DelveFeaturesDat"/> on <see cref="Specification.LoadDelveFeaturesDat"/> index.</remarks>
    public required int? DelveFeaturesKey { get; init; }

    /// <summary> Gets ARMFile.</summary>
    public required string ARMFile { get; init; }

    /// <summary>
    /// Gets DelveRoomsDat data.
    /// </summary>
    /// <param name="dataLoader">data loader.</param>
    /// <returns>array of DelveRoomsDat.</returns>
    internal static DelveRoomsDat[] Load(DataLoader dataLoader)
    {
        if (dataLoader is null)
        {
            throw new ArgumentNullException(nameof(dataLoader));
        }

        const string filePath = "Data/DelveRooms.dat64";
        var decompressedFile = dataLoader.GetFileBytes(filePath);

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

            // loading DelveBiomesKey
            (var delvebiomeskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading DelveFeaturesKey
            (var delvefeatureskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading ARMFile
            (var armfileLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            if (offset != expectedOffset)
            {
                throw new SchemaMismatchException($"offset {offset} != expectedOffset {expectedOffset}");
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
