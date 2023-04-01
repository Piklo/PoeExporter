﻿// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.SpecificationFiles;

/// <summary>
/// Class containing MicrotransactionRarityDisplay.dat data.
/// </summary>
public sealed partial class MicrotransactionRarityDisplayDat : ISpecificationFile<MicrotransactionRarityDisplayDat>
{
    /// <summary> Gets Rarity.</summary>
    public required string Rarity { get; init; }

    /// <summary> Gets ImageFile.</summary>
    public required string ImageFile { get; init; }

    /// <inheritdoc/>
    public static MicrotransactionRarityDisplayDat[] Load(Specification specification)
    {
        if (specification is null)
        {
            throw new ArgumentNullException(nameof(specification));
        }

        var fileToFind = Encoding.ASCII.GetBytes("Data/MicrotransactionRarityDisplay.dat64");
        var fileRecord = specification.DataLoader.GetFileRecord(fileToFind);
        var decompressedFile = specification.DataLoader.GetFileBytes(fileRecord);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new MicrotransactionRarityDisplayDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading referenced tables if any
            // loading Rarity
            (var rarityLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading ImageFile
            (var imagefileLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            if (offset != expectedOffset)
            {
                throw new NotImplementedException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new MicrotransactionRarityDisplayDat()
            {
                Rarity = rarityLoading,
                ImageFile = imagefileLoading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
