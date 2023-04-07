// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.Dat;

/// <summary>
/// Class containing HellscapeExperienceLevels.dat data.
/// </summary>
public sealed partial class HellscapeExperienceLevelsDat : IDat<HellscapeExperienceLevelsDat>
{
    /// <summary> Gets Level.</summary>
    public required int Level { get; init; }

    /// <summary> Gets Experience.</summary>
    public required int Experience { get; init; }

    /// <inheritdoc/>
    public static HellscapeExperienceLevelsDat[] Load(Specification specification)
    {
        if (specification is null)
        {
            throw new ArgumentNullException(nameof(specification));
        }

        const string filePath = "Data/HellscapeExperienceLevels.dat64";
        var decompressedFile = specification.DataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new HellscapeExperienceLevelsDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading Level
            (var levelLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Experience
            (var experienceLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            if (offset != expectedOffset)
            {
                throw new NotImplementedException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new HellscapeExperienceLevelsDat()
            {
                Level = levelLoading,
                Experience = experienceLoading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
