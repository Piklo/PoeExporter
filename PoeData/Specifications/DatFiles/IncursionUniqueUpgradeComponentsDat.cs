// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.DatFiles;

/// <summary>
/// Class containing IncursionUniqueUpgradeComponents.dat data.
/// </summary>
public sealed partial class IncursionUniqueUpgradeComponentsDat
{
    /// <summary> Gets BaseUnique.</summary>
    /// <remarks> references <see cref="WordsDat"/> on <see cref="Specification.LoadWordsDat"/> index.</remarks>
    public required int? BaseUnique { get; init; }

    /// <summary> Gets UpgradeCurrency.</summary>
    /// <remarks> references <see cref="BaseItemTypesDat"/> on <see cref="Specification.LoadBaseItemTypesDat"/> index.</remarks>
    public required int? UpgradeCurrency { get; init; }

    /// <summary>
    /// Gets IncursionUniqueUpgradeComponentsDat data.
    /// </summary>
    /// <param name="dataLoader">data loader.</param>
    /// <returns>array of IncursionUniqueUpgradeComponentsDat.</returns>
    internal static IncursionUniqueUpgradeComponentsDat[] Load(DataLoader dataLoader)
    {
        if (dataLoader is null)
        {
            throw new ArgumentNullException(nameof(dataLoader));
        }

        const string filePath = "Data/IncursionUniqueUpgradeComponents.dat64";
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new IncursionUniqueUpgradeComponentsDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading BaseUnique
            (var baseuniqueLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading UpgradeCurrency
            (var upgradecurrencyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            if (offset != expectedOffset)
            {
                throw new SchemaMismatchException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new IncursionUniqueUpgradeComponentsDat()
            {
                BaseUnique = baseuniqueLoading,
                UpgradeCurrency = upgradecurrencyLoading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
