// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.Dat;

/// <summary>
/// Class containing LakeBossLifeScalingPerLevel.dat data.
/// </summary>
public sealed partial class LakeBossLifeScalingPerLevelDat : ISpecificationFile<LakeBossLifeScalingPerLevelDat>
{
    /// <summary> Gets Level.</summary>
    public required int Level { get; init; }

    /// <summary> Gets Scaling.</summary>
    public required int Scaling { get; init; }

    /// <inheritdoc/>
    public static LakeBossLifeScalingPerLevelDat[] Load(Specification specification)
    {
        if (specification is null)
        {
            throw new ArgumentNullException(nameof(specification));
        }

        var fileToFind = Encoding.ASCII.GetBytes("Data/LakeBossLifeScalingPerLevel.dat64");
        var fileRecord = specification.DataLoader.GetFileRecord(fileToFind);
        var decompressedFile = specification.DataLoader.GetFileBytes(fileRecord);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new LakeBossLifeScalingPerLevelDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading Level
            (var levelLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Scaling
            (var scalingLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            if (offset != expectedOffset)
            {
                throw new NotImplementedException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new LakeBossLifeScalingPerLevelDat()
            {
                Level = levelLoading,
                Scaling = scalingLoading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
