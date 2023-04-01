// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.SpecificationFiles;

/// <summary>
/// Class containing Inventories.dat data.
/// </summary>
public sealed partial class InventoriesDat : ISpecificationFile<InventoriesDat>
{
    /// <summary> Gets Id.</summary>
    public required string Id { get; init; }

    /// <summary> Gets InventoryIdKey.</summary>
    public required int InventoryIdKey { get; init; }

    /// <summary> Gets InventoryTypeKey.</summary>
    public required int InventoryTypeKey { get; init; }

    /// <summary> Gets Unknown16.</summary>
    public required int Unknown16 { get; init; }

    /// <summary> Gets a value indicating whether Unknown20 is set.</summary>
    public required bool Unknown20 { get; init; }

    /// <summary> Gets a value indicating whether Unknown21 is set.</summary>
    public required bool Unknown21 { get; init; }

    /// <summary> Gets a value indicating whether Unknown22 is set.</summary>
    public required bool Unknown22 { get; init; }

    /// <inheritdoc/>
    public static InventoriesDat[] Load(Specification specification)
    {
        if (specification is null)
        {
            throw new ArgumentNullException(nameof(specification));
        }

        var fileToFind = Encoding.ASCII.GetBytes("Data/Inventories.dat64");
        var fileRecord = specification.DataLoader.GetFileRecord(fileToFind);
        var decompressedFile = specification.DataLoader.GetFileBytes(fileRecord);

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

            // loading referenced tables if any
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
