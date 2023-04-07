// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.DatFiles;

/// <summary>
/// Class containing ItemCostPerLevel.dat data.
/// </summary>
public sealed partial class ItemCostPerLevelDat
{
    /// <summary> Gets Contract_BaseItemTypesKey.</summary>
    /// <remarks> references <see cref="BaseItemTypesDat"/> on <see cref="Specification.GetBaseItemTypesDat"/> index.</remarks>
    public required int? Contract_BaseItemTypesKey { get; init; }

    /// <summary> Gets Level.</summary>
    public required int Level { get; init; }

    /// <summary> Gets Cost.</summary>
    /// <remarks> references <see cref="ItemCostsDat"/> on <see cref="Specification.GetItemCostsDat"/> index.</remarks>
    public required int? Cost { get; init; }

    /// <summary> Gets CostHardmode.</summary>
    /// <remarks> references <see cref="ItemCostsDat"/> on <see cref="Specification.GetItemCostsDat"/> index.</remarks>
    public required int? CostHardmode { get; init; }

    /// <summary>
    /// Gets ItemCostPerLevelDat data.
    /// </summary>
    /// <param name="dataLoader">data loader.</param>
    /// <returns>array of ItemCostPerLevelDat.</returns>
    internal static ItemCostPerLevelDat[] Load(DataLoader dataLoader)
    {
        if (dataLoader is null)
        {
            throw new ArgumentNullException(nameof(dataLoader));
        }

        const string filePath = "Data/ItemCostPerLevel.dat64";
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new ItemCostPerLevelDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading Contract_BaseItemTypesKey
            (var contract_baseitemtypeskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading Level
            (var levelLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Cost
            (var costLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading CostHardmode
            (var costhardmodeLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            if (offset != expectedOffset)
            {
                throw new NotImplementedException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new ItemCostPerLevelDat()
            {
                Contract_BaseItemTypesKey = contract_baseitemtypeskeyLoading,
                Level = levelLoading,
                Cost = costLoading,
                CostHardmode = costhardmodeLoading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
