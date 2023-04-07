// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.Dat;

/// <summary>
/// Class containing HellscapeLifeScalingPerLevel.dat data.
/// </summary>
public sealed partial class HellscapeLifeScalingPerLevelDat : IDat<HellscapeLifeScalingPerLevelDat>
{
    /// <summary> Gets AreaLevel.</summary>
    public required int AreaLevel { get; init; }

    /// <summary> Gets Scale.</summary>
    public required int Scale { get; init; }

    /// <inheritdoc/>
    public static HellscapeLifeScalingPerLevelDat[] Load(Specification specification)
    {
        if (specification is null)
        {
            throw new ArgumentNullException(nameof(specification));
        }

        const string filePath = "Data/HellscapeLifeScalingPerLevel.dat64";
        var decompressedFile = specification.DataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new HellscapeLifeScalingPerLevelDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading AreaLevel
            (var arealevelLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Scale
            (var scaleLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            if (offset != expectedOffset)
            {
                throw new NotImplementedException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new HellscapeLifeScalingPerLevelDat()
            {
                AreaLevel = arealevelLoading,
                Scale = scaleLoading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
