﻿// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.SpecificationFiles;

/// <summary>
/// Class containing ArchitectLifeScalingPerLevel.dat data.
/// </summary>
public sealed partial class ArchitectLifeScalingPerLevelDat : ISpecificationFile<ArchitectLifeScalingPerLevelDat>
{
    /// <summary> Gets Level.</summary>
    public required int Level { get; init; }

    /// <summary> Gets MoreLife.</summary>
    public required int MoreLife { get; init; }

    /// <inheritdoc/>
    public static ArchitectLifeScalingPerLevelDat[] Load(Specification specification)
    {
        if (specification is null)
        {
            throw new ArgumentNullException(nameof(specification));
        }

        var fileToFind = Encoding.ASCII.GetBytes("Data/ArchitectLifeScalingPerLevel.dat64");
        var fileRecord = specification.DataLoader.GetFileRecord(fileToFind);
        var decompressedFile = specification.DataLoader.GetFileBytes(fileRecord);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new ArchitectLifeScalingPerLevelDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading referenced tables if any
            // loading Level
            (var levelLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading MoreLife
            (var morelifeLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            if (offset != expectedOffset)
            {
                throw new NotImplementedException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new ArchitectLifeScalingPerLevelDat()
            {
                Level = levelLoading,
                MoreLife = morelifeLoading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}