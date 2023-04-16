// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.DatFiles;

/// <summary>
/// Class containing SentinelCraftingCurrency.dat data.
/// </summary>
public sealed partial class SentinelCraftingCurrencyDat
{
    /// <summary> Gets Currency.</summary>
    /// <remarks> references <see cref="BaseItemTypesDat"/> on <see cref="Specification.LoadBaseItemTypesDat"/> index.</remarks>
    public required int? Currency { get; init; }

    /// <summary> Gets Type.</summary>
    public required int Type { get; init; }

    /// <summary>
    /// Gets SentinelCraftingCurrencyDat data.
    /// </summary>
    /// <param name="dataLoader">data loader.</param>
    /// <returns>array of SentinelCraftingCurrencyDat.</returns>
    internal static SentinelCraftingCurrencyDat[] Load(DataLoader dataLoader)
    {
        if (dataLoader is null)
        {
            throw new ArgumentNullException(nameof(dataLoader));
        }

        const string filePath = "Data/SentinelCraftingCurrency.dat64";
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new SentinelCraftingCurrencyDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading Currency
            (var currencyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading Type
            (var typeLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            if (offset != expectedOffset)
            {
                throw new SchemaMismatchException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new SentinelCraftingCurrencyDat()
            {
                Currency = currencyLoading,
                Type = typeLoading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
