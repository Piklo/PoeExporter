// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.DatFiles;

/// <summary>
/// Class containing MapTiers.dat data.
/// </summary>
public sealed partial class MapTiersDat
{
    /// <summary> Gets Tier.</summary>
    public required int Tier { get; init; }

    /// <summary> Gets Level.</summary>
    public required int Level { get; init; }

    /// <summary> Gets Level2.</summary>
    public required int Level2 { get; init; }

    /// <summary>
    /// Gets MapTiersDat data.
    /// </summary>
    /// <param name="dataLoader">data loader.</param>
    /// <returns>array of MapTiersDat.</returns>
    internal static MapTiersDat[] Load(DataLoader dataLoader)
    {
        if (dataLoader is null)
        {
            throw new ArgumentNullException(nameof(dataLoader));
        }

        const string filePath = "Data/MapTiers.dat64";
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new MapTiersDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading Tier
            (var tierLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Level
            (var levelLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Level2
            (var level2Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            if (offset != expectedOffset)
            {
                throw new NotImplementedException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new MapTiersDat()
            {
                Tier = tierLoading,
                Level = levelLoading,
                Level2 = level2Loading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
