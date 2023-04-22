﻿// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.DatFiles;

/// <summary>
/// Class containing NPCShops.dat data.
/// </summary>
public sealed partial class NPCShopsDat
{
    /// <summary> Gets Id.</summary>
    public required string Id { get; init; }

    /// <summary> Gets Unknown8.</summary>
    public required int Unknown8 { get; init; }

    /// <summary> Gets Shop.</summary>
    /// <remarks> references <see cref="NPCShopDat"/> on <see cref="Specification.LoadNPCShopDat"/> index.</remarks>
    public required int? Shop { get; init; }

    /// <summary> Gets ShopHardmode.</summary>
    /// <remarks> references <see cref="NPCShopDat"/> on <see cref="Specification.LoadNPCShopDat"/> index.</remarks>
    public required int? ShopHardmode { get; init; }

    /// <summary>
    /// Gets NPCShopsDat data.
    /// </summary>
    /// <param name="dataLoader">data loader.</param>
    /// <returns>array of NPCShopsDat.</returns>
    internal static NPCShopsDat[] Load(DataLoader dataLoader)
    {
        if (dataLoader is null)
        {
            throw new ArgumentNullException(nameof(dataLoader));
        }

        const string filePath = "Data/NPCShops.dat64";
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new NPCShopsDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading Id
            (var idLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Unknown8
            (var unknown8Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Shop
            (var shopLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading ShopHardmode
            (var shophardmodeLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            if (offset != expectedOffset)
            {
                throw new SchemaMismatchException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new NPCShopsDat()
            {
                Id = idLoading,
                Unknown8 = unknown8Loading,
                Shop = shopLoading,
                ShopHardmode = shophardmodeLoading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
