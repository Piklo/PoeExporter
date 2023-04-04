// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.Dat;

/// <summary>
/// Class containing MapStashUniqueMapInfo.dat data.
/// </summary>
public sealed partial class MapStashUniqueMapInfoDat : ISpecificationFile<MapStashUniqueMapInfoDat>
{
    /// <summary> Gets UniqueMap.</summary>
    /// <remarks> references <see cref="UniqueMapsDat"/> on <see cref="Specification.GetUniqueMapsDat"/> index.</remarks>
    public required int? UniqueMap { get; init; }

    /// <summary> Gets BaseItem.</summary>
    /// <remarks> references <see cref="BaseItemTypesDat"/> on <see cref="Specification.GetBaseItemTypesDat"/> index.</remarks>
    public required int? BaseItem { get; init; }

    /// <inheritdoc/>
    public static MapStashUniqueMapInfoDat[] Load(Specification specification)
    {
        if (specification is null)
        {
            throw new ArgumentNullException(nameof(specification));
        }

        var fileToFind = Encoding.ASCII.GetBytes("Data/MapStashUniqueMapInfo.dat64");
        var fileRecord = specification.DataLoader.GetFileRecord(fileToFind);
        var decompressedFile = specification.DataLoader.GetFileBytes(fileRecord);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new MapStashUniqueMapInfoDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading UniqueMap
            (var uniquemapLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading BaseItem
            (var baseitemLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            if (offset != expectedOffset)
            {
                throw new NotImplementedException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new MapStashUniqueMapInfoDat()
            {
                UniqueMap = uniquemapLoading,
                BaseItem = baseitemLoading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
