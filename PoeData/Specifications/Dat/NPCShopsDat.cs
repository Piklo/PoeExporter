// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.Dat;

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
    /// <remarks> references <see cref="NPCShopDat"/> on <see cref="Specification.GetNPCShopDat"/> index.</remarks>
    public required int? Shop { get; init; }

    /// <summary> Gets ShopHardmode.</summary>
    /// <remarks> references <see cref="NPCShopDat"/> on <see cref="Specification.GetNPCShopDat"/> index.</remarks>
    public required int? ShopHardmode { get; init; }

    /// <inheritdoc/>
    public static NPCShopsDat[] Load(Specification specification)
    {
        if (specification is null)
        {
            throw new ArgumentNullException(nameof(specification));
        }

        const string filePath = "Data/NPCShops.dat64";
        var decompressedFile = specification.DataLoader.GetFileBytes(filePath);

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
            (var shopLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading ShopHardmode
            (var shophardmodeLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            if (offset != expectedOffset)
            {
                throw new NotImplementedException($"offset {offset} != expectedOffset {expectedOffset}");
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
