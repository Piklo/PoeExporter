﻿// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.DatFiles;

/// <summary>
/// Class containing RogueExileLifeScalingPerLevel.dat data.
/// </summary>
public sealed partial class RogueExileLifeScalingPerLevelDat
{
    /// <summary> Gets Level.</summary>
    public required int Level { get; init; }

    /// <summary> Gets AdditionalLife.</summary>
    public required int AdditionalLife { get; init; }

    /// <summary>
    /// Gets RogueExileLifeScalingPerLevelDat data.
    /// </summary>
    /// <param name="dataLoader">data loader.</param>
    /// <returns>array of RogueExileLifeScalingPerLevelDat.</returns>
    internal static RogueExileLifeScalingPerLevelDat[] Load(DataLoader dataLoader)
    {
        if (dataLoader is null)
        {
            throw new ArgumentNullException(nameof(dataLoader));
        }

        const string filePath = "Data/RogueExileLifeScalingPerLevel.dat64";
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new RogueExileLifeScalingPerLevelDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading Level
            (var levelLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading AdditionalLife
            (var additionallifeLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            if (offset != expectedOffset)
            {
                throw new NotImplementedException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new RogueExileLifeScalingPerLevelDat()
            {
                Level = levelLoading,
                AdditionalLife = additionallifeLoading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}