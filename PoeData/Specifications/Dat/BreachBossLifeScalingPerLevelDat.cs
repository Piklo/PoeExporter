// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.Dat;

/// <summary>
/// Class containing BreachBossLifeScalingPerLevel.dat data.
/// </summary>
public sealed partial class BreachBossLifeScalingPerLevelDat : ISpecificationFile<BreachBossLifeScalingPerLevelDat>
{
    /// <summary> Gets MonsterLevel.</summary>
    public required int MonsterLevel { get; init; }

    /// <summary> Gets LifeMultiplier.</summary>
    public required int LifeMultiplier { get; init; }

    /// <inheritdoc/>
    public static BreachBossLifeScalingPerLevelDat[] Load(Specification specification)
    {
        if (specification is null)
        {
            throw new ArgumentNullException(nameof(specification));
        }

        var fileToFind = Encoding.ASCII.GetBytes("Data/BreachBossLifeScalingPerLevel.dat64");
        var fileRecord = specification.DataLoader.GetFileRecord(fileToFind);
        var decompressedFile = specification.DataLoader.GetFileBytes(fileRecord);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new BreachBossLifeScalingPerLevelDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading referenced tables if any
            // loading MonsterLevel
            (var monsterlevelLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading LifeMultiplier
            (var lifemultiplierLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            if (offset != expectedOffset)
            {
                throw new NotImplementedException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new BreachBossLifeScalingPerLevelDat()
            {
                MonsterLevel = monsterlevelLoading,
                LifeMultiplier = lifemultiplierLoading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
