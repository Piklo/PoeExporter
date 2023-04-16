// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.DatFiles;

/// <summary>
/// Class containing LabyrinthExclusionGroups.dat data.
/// </summary>
public sealed partial class LabyrinthExclusionGroupsDat
{
    /// <summary> Gets Id.</summary>
    public required string Id { get; init; }

    /// <summary>
    /// Gets LabyrinthExclusionGroupsDat data.
    /// </summary>
    /// <param name="dataLoader">data loader.</param>
    /// <returns>array of LabyrinthExclusionGroupsDat.</returns>
    internal static LabyrinthExclusionGroupsDat[] Load(DataLoader dataLoader)
    {
        if (dataLoader is null)
        {
            throw new ArgumentNullException(nameof(dataLoader));
        }

        const string filePath = "Data/LabyrinthExclusionGroups.dat64";
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new LabyrinthExclusionGroupsDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading Id
            (var idLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            if (offset != expectedOffset)
            {
                throw new SchemaMismatchException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new LabyrinthExclusionGroupsDat()
            {
                Id = idLoading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
