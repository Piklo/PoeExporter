﻿// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.SpecificationFiles;

/// <summary>
/// Class containing AtlasExileBossArenas.dat data.
/// </summary>
public sealed partial class AtlasExileBossArenasDat : ISpecificationFile<AtlasExileBossArenasDat>
{
    /// <summary> Gets Conqueror.</summary>
    public required int? Conqueror { get; init; }

    /// <summary> Gets WorldArea.</summary>
    public required int? WorldArea { get; init; }

    /// <inheritdoc/>
    public static AtlasExileBossArenasDat[] Load(Specification specification)
    {
        if (specification is null)
        {
            throw new ArgumentNullException(nameof(specification));
        }

        var fileToFind = Encoding.ASCII.GetBytes("Data/AtlasExileBossArenas.dat64");
        var fileRecord = specification.DataLoader.GetFileRecord(fileToFind);
        var decompressedFile = specification.DataLoader.GetFileBytes(fileRecord);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new AtlasExileBossArenasDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading referenced tables if any
            // specification.GetAtlasExilesDat();
            // specification.GetWorldAreasDat();

            // loading Conqueror
            (var conquerorLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading WorldArea
            (var worldareaLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            if (offset != expectedOffset)
            {
                throw new NotImplementedException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new AtlasExileBossArenasDat()
            {
                Conqueror = conquerorLoading,
                WorldArea = worldareaLoading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}