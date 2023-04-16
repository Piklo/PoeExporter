// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.DatFiles;

/// <summary>
/// Class containing UniqueJewelLimits.dat data.
/// </summary>
public sealed partial class UniqueJewelLimitsDat
{
    /// <summary> Gets JewelName.</summary>
    /// <remarks> references <see cref="WordsDat"/> on <see cref="Specification.LoadWordsDat"/> index.</remarks>
    public required int? JewelName { get; init; }

    /// <summary> Gets Limit.</summary>
    public required int Limit { get; init; }

    /// <summary>
    /// Gets UniqueJewelLimitsDat data.
    /// </summary>
    /// <param name="dataLoader">data loader.</param>
    /// <returns>array of UniqueJewelLimitsDat.</returns>
    internal static UniqueJewelLimitsDat[] Load(DataLoader dataLoader)
    {
        if (dataLoader is null)
        {
            throw new ArgumentNullException(nameof(dataLoader));
        }

        const string filePath = "Data/UniqueJewelLimits.dat64";
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new UniqueJewelLimitsDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading JewelName
            (var jewelnameLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading Limit
            (var limitLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            if (offset != expectedOffset)
            {
                throw new SchemaMismatchException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new UniqueJewelLimitsDat()
            {
                JewelName = jewelnameLoading,
                Limit = limitLoading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
