﻿// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.DatFiles;

/// <summary>
/// Class containing AtlasExiles.dat data.
/// </summary>
public sealed partial class AtlasExilesDat
{
    /// <summary> Gets Id.</summary>
    public required string Id { get; init; }

    /// <summary> Gets Unknown8.</summary>
    public required int Unknown8 { get; init; }

    /// <summary> Gets InfluencedItemIncrStat.</summary>
    /// <remarks> references <see cref="StatsDat"/> on <see cref="Specification.LoadStatsDat"/> index.</remarks>
    public required int? InfluencedItemIncrStat { get; init; }

    /// <summary> Gets MapIcon.</summary>
    public required string MapIcon { get; init; }

    /// <summary> Gets MapIcon2.</summary>
    public required string MapIcon2 { get; init; }

    /// <summary>
    /// Gets AtlasExilesDat data.
    /// </summary>
    /// <param name="dataLoader">data loader.</param>
    /// <returns>array of AtlasExilesDat.</returns>
    internal static AtlasExilesDat[] Load(DataLoader dataLoader)
    {
        if (dataLoader is null)
        {
            throw new ArgumentNullException(nameof(dataLoader));
        }

        const string filePath = "Data/AtlasExiles.dat64";
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new AtlasExilesDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading Id
            (var idLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Unknown8
            (var unknown8Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading InfluencedItemIncrStat
            (var influenceditemincrstatLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading MapIcon
            (var mapiconLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading MapIcon2
            (var mapicon2Loading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            if (offset != expectedOffset)
            {
                throw new SchemaMismatchException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new AtlasExilesDat()
            {
                Id = idLoading,
                Unknown8 = unknown8Loading,
                InfluencedItemIncrStat = influenceditemincrstatLoading,
                MapIcon = mapiconLoading,
                MapIcon2 = mapicon2Loading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
