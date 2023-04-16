﻿// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.DatFiles;

/// <summary>
/// Class containing GlobalAudioConfig.dat data.
/// </summary>
public sealed partial class GlobalAudioConfigDat
{
    /// <summary> Gets Id.</summary>
    public required string Id { get; init; }

    /// <summary> Gets Value.</summary>
    public required int Value { get; init; }

    /// <summary> Gets a value indicating whether Unknown12 is set.</summary>
    public required bool Unknown12 { get; init; }

    /// <summary>
    /// Gets GlobalAudioConfigDat data.
    /// </summary>
    /// <param name="dataLoader">data loader.</param>
    /// <returns>array of GlobalAudioConfigDat.</returns>
    internal static GlobalAudioConfigDat[] Load(DataLoader dataLoader)
    {
        if (dataLoader is null)
        {
            throw new ArgumentNullException(nameof(dataLoader));
        }

        const string filePath = "Data/GlobalAudioConfig.dat64";
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new GlobalAudioConfigDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading Id
            (var idLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Value
            (var valueLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown12
            (var unknown12Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            if (offset != expectedOffset)
            {
                throw new NotImplementedException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new GlobalAudioConfigDat()
            {
                Id = idLoading,
                Value = valueLoading,
                Unknown12 = unknown12Loading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}