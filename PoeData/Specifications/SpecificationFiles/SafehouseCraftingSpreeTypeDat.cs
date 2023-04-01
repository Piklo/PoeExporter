﻿// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.SpecificationFiles;

/// <summary>
/// Class containing SafehouseCraftingSpreeType.dat data.
/// </summary>
public sealed partial class SafehouseCraftingSpreeTypeDat : ISpecificationFile<SafehouseCraftingSpreeTypeDat>
{
    /// <summary> Gets Id.</summary>
    public required string Id { get; init; }

    /// <summary> Gets Currencies.</summary>
    public required ReadOnlyCollection<int> Currencies { get; init; }

    /// <summary> Gets CurrencyCount.</summary>
    public required ReadOnlyCollection<int> CurrencyCount { get; init; }

    /// <summary> Gets Unknown40.</summary>
    public required ReadOnlyCollection<int> Unknown40 { get; init; }

    /// <summary> Gets a value indicating whether Disabled is set.</summary>
    public required bool Disabled { get; init; }

    /// <summary> Gets ItemClassText.</summary>
    public required string ItemClassText { get; init; }

    /// <inheritdoc/>
    public static SafehouseCraftingSpreeTypeDat[] Load(Specification specification)
    {
        if (specification is null)
        {
            throw new ArgumentNullException(nameof(specification));
        }

        var fileToFind = Encoding.ASCII.GetBytes("Data/SafehouseCraftingSpreeType.dat64");
        var fileRecord = specification.DataLoader.GetFileRecord(fileToFind);
        var decompressedFile = specification.DataLoader.GetFileBytes(fileRecord);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new SafehouseCraftingSpreeTypeDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading referenced tables if any
            // specification.GetSafehouseCraftingSpreeCurrenciesDat();

            // loading Id
            (var idLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Currencies
            (var tempcurrenciesLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var currenciesLoading = tempcurrenciesLoading.AsReadOnly();

            // loading CurrencyCount
            (var tempcurrencycountLoading, offset) = SpecificationFileLoader.LoadIntArray(decompressedFile, offset, dataOffset);
            var currencycountLoading = tempcurrencycountLoading.AsReadOnly();

            // loading Unknown40
            (var tempunknown40Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var unknown40Loading = tempunknown40Loading.AsReadOnly();

            // loading Disabled
            (var disabledLoading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading ItemClassText
            (var itemclasstextLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            if (offset != expectedOffset)
            {
                throw new NotImplementedException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new SafehouseCraftingSpreeTypeDat()
            {
                Id = idLoading,
                Currencies = currenciesLoading,
                CurrencyCount = currencycountLoading,
                Unknown40 = unknown40Loading,
                Disabled = disabledLoading,
                ItemClassText = itemclasstextLoading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
