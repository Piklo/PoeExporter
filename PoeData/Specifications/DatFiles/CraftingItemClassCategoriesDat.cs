﻿// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.DatFiles;

/// <summary>
/// Class containing CraftingItemClassCategories.dat data.
/// </summary>
public sealed partial class CraftingItemClassCategoriesDat
{
    /// <summary> Gets Id.</summary>
    public required string Id { get; init; }

    /// <summary> Gets ItemClasses.</summary>
    /// <remarks> references <see cref="ItemClassesDat"/> on <see cref="Specification.LoadItemClassesDat"/> index.</remarks>
    public required ReadOnlyCollection<int> ItemClasses { get; init; }

    /// <summary> Gets Unknown24.</summary>
    public required string Unknown24 { get; init; }

    /// <summary> Gets Text.</summary>
    public required string Text { get; init; }

    /// <summary>
    /// Gets CraftingItemClassCategoriesDat data.
    /// </summary>
    /// <param name="dataLoader">data loader.</param>
    /// <returns>array of CraftingItemClassCategoriesDat.</returns>
    internal static CraftingItemClassCategoriesDat[] Load(DataLoader dataLoader)
    {
        if (dataLoader is null)
        {
            throw new ArgumentNullException(nameof(dataLoader));
        }

        const string filePath = "Data/CraftingItemClassCategories.dat64";
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new CraftingItemClassCategoriesDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading Id
            (var idLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading ItemClasses
            (var tempitemclassesLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var itemclassesLoading = tempitemclassesLoading.AsReadOnly();

            // loading Unknown24
            (var unknown24Loading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Text
            (var textLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            if (offset != expectedOffset)
            {
                throw new SchemaMismatchException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new CraftingItemClassCategoriesDat()
            {
                Id = idLoading,
                ItemClasses = itemclassesLoading,
                Unknown24 = unknown24Loading,
                Text = textLoading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
