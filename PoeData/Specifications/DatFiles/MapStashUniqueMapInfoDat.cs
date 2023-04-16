// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.DatFiles;

/// <summary>
/// Class containing MapStashUniqueMapInfo.dat data.
/// </summary>
public sealed partial class MapStashUniqueMapInfoDat
{
    /// <summary> Gets UniqueMap.</summary>
    /// <remarks> references <see cref="UniqueMapsDat"/> on <see cref="Specification.LoadUniqueMapsDat"/> index.</remarks>
    public required int? UniqueMap { get; init; }

    /// <summary> Gets BaseItem.</summary>
    /// <remarks> references <see cref="BaseItemTypesDat"/> on <see cref="Specification.LoadBaseItemTypesDat"/> index.</remarks>
    public required int? BaseItem { get; init; }

    /// <summary>
    /// Gets MapStashUniqueMapInfoDat data.
    /// </summary>
    /// <param name="dataLoader">data loader.</param>
    /// <returns>array of MapStashUniqueMapInfoDat.</returns>
    internal static MapStashUniqueMapInfoDat[] Load(DataLoader dataLoader)
    {
        if (dataLoader is null)
        {
            throw new ArgumentNullException(nameof(dataLoader));
        }

        const string filePath = "Data/MapStashUniqueMapInfo.dat64";
        var decompressedFile = dataLoader.GetFileBytes(filePath);

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
                throw new SchemaMismatchException($"offset {offset} != expectedOffset {expectedOffset}");
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
