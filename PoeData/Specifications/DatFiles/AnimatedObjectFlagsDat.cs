﻿// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.DatFiles;

/// <summary>
/// Class containing AnimatedObjectFlags.dat data.
/// </summary>
public sealed partial class AnimatedObjectFlagsDat
{
    /// <summary> Gets AOFile.</summary>
    public required string AOFile { get; init; }

    /// <summary> Gets Unknown8.</summary>
    public required int Unknown8 { get; init; }

    /// <summary> Gets a value indicating whether Unknown12 is set.</summary>
    public required bool Unknown12 { get; init; }

    /// <summary> Gets a value indicating whether Unknown13 is set.</summary>
    public required bool Unknown13 { get; init; }

    /// <summary>
    /// Gets AnimatedObjectFlagsDat data.
    /// </summary>
    /// <param name="dataLoader">data loader.</param>
    /// <returns>array of AnimatedObjectFlagsDat.</returns>
    internal static AnimatedObjectFlagsDat[] Load(DataLoader dataLoader)
    {
        if (dataLoader is null)
        {
            throw new ArgumentNullException(nameof(dataLoader));
        }

        const string filePath = "Data/AnimatedObjectFlags.dat64";
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new AnimatedObjectFlagsDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading AOFile
            (var aofileLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Unknown8
            (var unknown8Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown12
            (var unknown12Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown13
            (var unknown13Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            if (offset != expectedOffset)
            {
                throw new NotImplementedException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new AnimatedObjectFlagsDat()
            {
                AOFile = aofileLoading,
                Unknown8 = unknown8Loading,
                Unknown12 = unknown12Loading,
                Unknown13 = unknown13Loading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}