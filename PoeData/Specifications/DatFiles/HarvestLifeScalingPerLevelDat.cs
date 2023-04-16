// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.DatFiles;

/// <summary>
/// Class containing HarvestLifeScalingPerLevel.dat data.
/// </summary>
public sealed partial class HarvestLifeScalingPerLevelDat
{
    /// <summary> Gets Level.</summary>
    public required int Level { get; init; }

    /// <summary> Gets Life.</summary>
    public required int Life { get; init; }

    /// <summary>
    /// Gets HarvestLifeScalingPerLevelDat data.
    /// </summary>
    /// <param name="dataLoader">data loader.</param>
    /// <returns>array of HarvestLifeScalingPerLevelDat.</returns>
    internal static HarvestLifeScalingPerLevelDat[] Load(DataLoader dataLoader)
    {
        if (dataLoader is null)
        {
            throw new ArgumentNullException(nameof(dataLoader));
        }

        const string filePath = "Data/HarvestLifeScalingPerLevel.dat64";
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new HarvestLifeScalingPerLevelDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading Level
            (var levelLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Life
            (var lifeLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            if (offset != expectedOffset)
            {
                throw new SchemaMismatchException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new HarvestLifeScalingPerLevelDat()
            {
                Level = levelLoading,
                Life = lifeLoading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
