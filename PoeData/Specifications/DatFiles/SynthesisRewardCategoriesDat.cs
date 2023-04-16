// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.DatFiles;

/// <summary>
/// Class containing SynthesisRewardCategories.dat data.
/// </summary>
public sealed partial class SynthesisRewardCategoriesDat
{
    /// <summary> Gets Id.</summary>
    public required string Id { get; init; }

    /// <summary>
    /// Gets SynthesisRewardCategoriesDat data.
    /// </summary>
    /// <param name="dataLoader">data loader.</param>
    /// <returns>array of SynthesisRewardCategoriesDat.</returns>
    internal static SynthesisRewardCategoriesDat[] Load(DataLoader dataLoader)
    {
        if (dataLoader is null)
        {
            throw new ArgumentNullException(nameof(dataLoader));
        }

        const string filePath = "Data/SynthesisRewardCategories.dat64";
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new SynthesisRewardCategoriesDat[tableRows];
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

            var obj = new SynthesisRewardCategoriesDat()
            {
                Id = idLoading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
