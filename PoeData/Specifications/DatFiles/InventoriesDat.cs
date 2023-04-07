// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.DatFiles;

/// <summary>
/// Class containing Inventories.dat data.
/// </summary>
public sealed partial class InventoriesDat
{
    /// <summary> Gets Id.</summary>
    public required string Id { get; init; }

    /// <summary> Gets InventoryIdKey.</summary>
    /// <remarks> references <see cref="InventoryIdDat"/> on <see cref="Specification.GetInventoryIdDat"/> index.</remarks>
    public required int InventoryIdKey { get; init; }

    /// <summary> Gets InventoryTypeKey.</summary>
    /// <remarks> references <see cref="InventoryTypeDat"/> on <see cref="Specification.GetInventoryTypeDat"/> index.</remarks>
    public required int InventoryTypeKey { get; init; }

    /// <summary> Gets Unknown16.</summary>
    public required int Unknown16 { get; init; }

    /// <summary> Gets a value indicating whether Unknown20 is set.</summary>
    public required bool Unknown20 { get; init; }

    /// <summary> Gets a value indicating whether Unknown21 is set.</summary>
    public required bool Unknown21 { get; init; }

    /// <summary> Gets a value indicating whether Unknown22 is set.</summary>
    public required bool Unknown22 { get; init; }

    /// <summary>
    /// Gets InventoriesDat data.
    /// </summary>
    /// <param name="dataLoader">data loader.</param>
    /// <returns>array of InventoriesDat.</returns>
    internal static InventoriesDat[] Load(DataLoader dataLoader)
    {
        if (dataLoader is null)
        {
            throw new ArgumentNullException(nameof(dataLoader));
        }

        const string filePath = "Data/Inventories.dat64";
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new InventoriesDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading Id
            (var idLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading InventoryIdKey
            (var inventoryidkeyLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading InventoryTypeKey
            (var inventorytypekeyLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown16
            (var unknown16Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown20
            (var unknown20Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown21
            (var unknown21Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown22
            (var unknown22Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            if (offset != expectedOffset)
            {
                throw new NotImplementedException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new InventoriesDat()
            {
                Id = idLoading,
                InventoryIdKey = inventoryidkeyLoading,
                InventoryTypeKey = inventorytypekeyLoading,
                Unknown16 = unknown16Loading,
                Unknown20 = unknown20Loading,
                Unknown21 = unknown21Loading,
                Unknown22 = unknown22Loading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
