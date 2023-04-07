// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.DatFiles;

/// <summary>
/// Class containing MicrotransactionCategory.dat data.
/// </summary>
public sealed partial class MicrotransactionCategoryDat
{
    /// <summary> Gets Id.</summary>
    /// <remarks> references <see cref="MicrotransactionCategoryIdDat"/> on <see cref="Specification.GetMicrotransactionCategoryIdDat"/> index.</remarks>
    public required int Id { get; init; }

    /// <summary> Gets Name.</summary>
    public required string Name { get; init; }

    /// <summary>
    /// Gets MicrotransactionCategoryDat data.
    /// </summary>
    /// <param name="dataLoader">data loader.</param>
    /// <returns>array of MicrotransactionCategoryDat.</returns>
    internal static MicrotransactionCategoryDat[] Load(DataLoader dataLoader)
    {
        if (dataLoader is null)
        {
            throw new ArgumentNullException(nameof(dataLoader));
        }

        const string filePath = "Data/MicrotransactionCategory.dat64";
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new MicrotransactionCategoryDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading Id
            (var idLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Name
            (var nameLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            if (offset != expectedOffset)
            {
                throw new NotImplementedException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new MicrotransactionCategoryDat()
            {
                Id = idLoading,
                Name = nameLoading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
