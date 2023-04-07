// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.DatFiles;

/// <summary>
/// Class containing BreachstoneUpgrades.dat data.
/// </summary>
public sealed partial class BreachstoneUpgradesDat
{
    /// <summary> Gets BaseItemTypesKey0.</summary>
    /// <remarks> references <see cref="BaseItemTypesDat"/> on <see cref="Specification.GetBaseItemTypesDat"/> index.</remarks>
    public required int? BaseItemTypesKey0 { get; init; }

    /// <summary> Gets BaseItemTypesKey1.</summary>
    /// <remarks> references <see cref="BaseItemTypesDat"/> on <see cref="Specification.GetBaseItemTypesDat"/> index.</remarks>
    public required int? BaseItemTypesKey1 { get; init; }

    /// <summary> Gets BaseItemTypesKey2.</summary>
    /// <remarks> references <see cref="BaseItemTypesDat"/> on <see cref="Specification.GetBaseItemTypesDat"/> index.</remarks>
    public required int? BaseItemTypesKey2 { get; init; }

    /// <summary> Gets BaseItemTypesKey3.</summary>
    /// <remarks> references <see cref="BaseItemTypesDat"/> on <see cref="Specification.GetBaseItemTypesDat"/> index.</remarks>
    public required int? BaseItemTypesKey3 { get; init; }

    /// <summary> Gets Unknown64.</summary>
    public required int? Unknown64 { get; init; }

    /// <summary>
    /// Gets BreachstoneUpgradesDat data.
    /// </summary>
    /// <param name="dataLoader">data loader.</param>
    /// <returns>array of BreachstoneUpgradesDat.</returns>
    internal static BreachstoneUpgradesDat[] Load(DataLoader dataLoader)
    {
        if (dataLoader is null)
        {
            throw new ArgumentNullException(nameof(dataLoader));
        }

        const string filePath = "Data/BreachstoneUpgrades.dat64";
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new BreachstoneUpgradesDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading BaseItemTypesKey0
            (var baseitemtypeskey0Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading BaseItemTypesKey1
            (var baseitemtypeskey1Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading BaseItemTypesKey2
            (var baseitemtypeskey2Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading BaseItemTypesKey3
            (var baseitemtypeskey3Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading Unknown64
            (var unknown64Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            if (offset != expectedOffset)
            {
                throw new NotImplementedException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new BreachstoneUpgradesDat()
            {
                BaseItemTypesKey0 = baseitemtypeskey0Loading,
                BaseItemTypesKey1 = baseitemtypeskey1Loading,
                BaseItemTypesKey2 = baseitemtypeskey2Loading,
                BaseItemTypesKey3 = baseitemtypeskey3Loading,
                Unknown64 = unknown64Loading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
