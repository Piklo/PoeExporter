// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.DatFiles;

/// <summary>
/// Class containing ExperienceLevels.dat data.
/// </summary>
public sealed partial class ExperienceLevelsDat
{
    /// <summary> Gets Unknown0.</summary>
    public required string Unknown0 { get; init; }

    /// <summary> Gets Level.</summary>
    public required int Level { get; init; }

    /// <summary> Gets Experience.</summary>
    public required int Experience { get; init; }

    /// <summary>
    /// Gets ExperienceLevelsDat data.
    /// </summary>
    /// <param name="dataLoader">data loader.</param>
    /// <returns>array of ExperienceLevelsDat.</returns>
    internal static ExperienceLevelsDat[] Load(DataLoader dataLoader)
    {
        if (dataLoader is null)
        {
            throw new ArgumentNullException(nameof(dataLoader));
        }

        const string filePath = "Data/ExperienceLevels.dat64";
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new ExperienceLevelsDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading Unknown0
            (var unknown0Loading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Level
            (var levelLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Experience
            (var experienceLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            if (offset != expectedOffset)
            {
                throw new SchemaMismatchException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new ExperienceLevelsDat()
            {
                Unknown0 = unknown0Loading,
                Level = levelLoading,
                Experience = experienceLoading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
