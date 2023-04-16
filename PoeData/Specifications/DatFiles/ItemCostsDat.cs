// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.DatFiles;

/// <summary>
/// Class containing ItemCosts.dat data.
/// </summary>
public sealed partial class ItemCostsDat
{
    /// <summary> Gets Id.</summary>
    public required string Id { get; init; }

    /// <summary> Gets Cost1Currencies.</summary>
    /// <remarks> references <see cref="BaseItemTypesDat"/> on <see cref="Specification.GetBaseItemTypesDat"/> index.</remarks>
    public required ReadOnlyCollection<int> Cost1Currencies { get; init; }

    /// <summary> Gets Cost1Values.</summary>
    public required ReadOnlyCollection<int> Cost1Values { get; init; }

    /// <summary> Gets Cost2Currencies.</summary>
    /// <remarks> references <see cref="BaseItemTypesDat"/> on <see cref="Specification.GetBaseItemTypesDat"/> index.</remarks>
    public required ReadOnlyCollection<int> Cost2Currencies { get; init; }

    /// <summary> Gets Cost2Values.</summary>
    public required ReadOnlyCollection<int> Cost2Values { get; init; }

    /// <summary> Gets Cost3Currencies.</summary>
    /// <remarks> references <see cref="BaseItemTypesDat"/> on <see cref="Specification.GetBaseItemTypesDat"/> index.</remarks>
    public required ReadOnlyCollection<int> Cost3Currencies { get; init; }

    /// <summary> Gets Cost3Values.</summary>
    public required ReadOnlyCollection<int> Cost3Values { get; init; }

    /// <summary> Gets Cost4Currencies.</summary>
    /// <remarks> references <see cref="BaseItemTypesDat"/> on <see cref="Specification.GetBaseItemTypesDat"/> index.</remarks>
    public required ReadOnlyCollection<int> Cost4Currencies { get; init; }

    /// <summary> Gets Cost4Values.</summary>
    public required ReadOnlyCollection<int> Cost4Values { get; init; }

    /// <summary>
    /// Gets ItemCostsDat data.
    /// </summary>
    /// <param name="dataLoader">data loader.</param>
    /// <returns>array of ItemCostsDat.</returns>
    internal static ItemCostsDat[] Load(DataLoader dataLoader)
    {
        if (dataLoader is null)
        {
            throw new ArgumentNullException(nameof(dataLoader));
        }

        const string filePath = "Data/ItemCosts.dat64";
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new ItemCostsDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading Id
            (var idLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Cost1Currencies
            (var tempcost1currenciesLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var cost1currenciesLoading = tempcost1currenciesLoading.AsReadOnly();

            // loading Cost1Values
            (var tempcost1valuesLoading, offset) = SpecificationFileLoader.LoadIntArray(decompressedFile, offset, dataOffset);
            var cost1valuesLoading = tempcost1valuesLoading.AsReadOnly();

            // loading Cost2Currencies
            (var tempcost2currenciesLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var cost2currenciesLoading = tempcost2currenciesLoading.AsReadOnly();

            // loading Cost2Values
            (var tempcost2valuesLoading, offset) = SpecificationFileLoader.LoadIntArray(decompressedFile, offset, dataOffset);
            var cost2valuesLoading = tempcost2valuesLoading.AsReadOnly();

            // loading Cost3Currencies
            (var tempcost3currenciesLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var cost3currenciesLoading = tempcost3currenciesLoading.AsReadOnly();

            // loading Cost3Values
            (var tempcost3valuesLoading, offset) = SpecificationFileLoader.LoadIntArray(decompressedFile, offset, dataOffset);
            var cost3valuesLoading = tempcost3valuesLoading.AsReadOnly();

            // loading Cost4Currencies
            (var tempcost4currenciesLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var cost4currenciesLoading = tempcost4currenciesLoading.AsReadOnly();

            // loading Cost4Values
            (var tempcost4valuesLoading, offset) = SpecificationFileLoader.LoadIntArray(decompressedFile, offset, dataOffset);
            var cost4valuesLoading = tempcost4valuesLoading.AsReadOnly();

            if (offset != expectedOffset)
            {
                throw new SchemaMismatchException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new ItemCostsDat()
            {
                Id = idLoading,
                Cost1Currencies = cost1currenciesLoading,
                Cost1Values = cost1valuesLoading,
                Cost2Currencies = cost2currenciesLoading,
                Cost2Values = cost2valuesLoading,
                Cost3Currencies = cost3currenciesLoading,
                Cost3Values = cost3valuesLoading,
                Cost4Currencies = cost4currenciesLoading,
                Cost4Values = cost4valuesLoading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
