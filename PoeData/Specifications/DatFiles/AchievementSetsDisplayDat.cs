// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.DatFiles;

/// <summary>
/// Class containing AchievementSetsDisplay.dat data.
/// </summary>
public sealed partial class AchievementSetsDisplayDat
{
    /// <summary> Gets Id.</summary>
    public required int Id { get; init; }

    /// <summary> Gets Title.</summary>
    public required string Title { get; init; }

    /// <summary>
    /// Gets AchievementSetsDisplayDat data.
    /// </summary>
    /// <param name="dataLoader">data loader.</param>
    /// <returns>array of AchievementSetsDisplayDat.</returns>
    internal static AchievementSetsDisplayDat[] Load(DataLoader dataLoader)
    {
        if (dataLoader is null)
        {
            throw new ArgumentNullException(nameof(dataLoader));
        }

        const string filePath = "Data/AchievementSetsDisplay.dat64";
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new AchievementSetsDisplayDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading Id
            (var idLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Title
            (var titleLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            if (offset != expectedOffset)
            {
                throw new SchemaMismatchException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new AchievementSetsDisplayDat()
            {
                Id = idLoading,
                Title = titleLoading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
