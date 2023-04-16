// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.DatFiles;

/// <summary>
/// Class containing ItemNoteCode.dat data.
/// </summary>
public sealed partial class ItemNoteCodeDat
{
    /// <summary> Gets BaseItem.</summary>
    /// <remarks> references <see cref="BaseItemTypesDat"/> on <see cref="Specification.GetBaseItemTypesDat"/> index.</remarks>
    public required int? BaseItem { get; init; }

    /// <summary> Gets Code.</summary>
    public required string Code { get; init; }

    /// <summary> Gets Order1.</summary>
    public required int Order1 { get; init; }

    /// <summary> Gets a value indicating whether Show is set.</summary>
    public required bool Show { get; init; }

    /// <summary> Gets Order2.</summary>
    public required int Order2 { get; init; }

    /// <summary>
    /// Gets ItemNoteCodeDat data.
    /// </summary>
    /// <param name="dataLoader">data loader.</param>
    /// <returns>array of ItemNoteCodeDat.</returns>
    internal static ItemNoteCodeDat[] Load(DataLoader dataLoader)
    {
        if (dataLoader is null)
        {
            throw new ArgumentNullException(nameof(dataLoader));
        }

        const string filePath = "Data/ItemNoteCode.dat64";
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new ItemNoteCodeDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading BaseItem
            (var baseitemLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading Code
            (var codeLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Order1
            (var order1Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Show
            (var showLoading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Order2
            (var order2Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            if (offset != expectedOffset)
            {
                throw new NotImplementedException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new ItemNoteCodeDat()
            {
                BaseItem = baseitemLoading,
                Code = codeLoading,
                Order1 = order1Loading,
                Show = showLoading,
                Order2 = order2Loading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
