// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.Dat;

/// <summary>
/// Class containing RecipeUnlockDisplay.dat data.
/// </summary>
public sealed partial class RecipeUnlockDisplayDat
{
    /// <summary> Gets RecipeId.</summary>
    public required int RecipeId { get; init; }

    /// <summary> Gets Description.</summary>
    public required string Description { get; init; }

    /// <summary> Gets CraftingItemClassCategoriesKeys.</summary>
    /// <remarks> references <see cref="CraftingItemClassCategoriesDat"/> on <see cref="Specification.GetCraftingItemClassCategoriesDat"/> index.</remarks>
    public required ReadOnlyCollection<int> CraftingItemClassCategoriesKeys { get; init; }

    /// <summary> Gets UnlockDescription.</summary>
    public required string UnlockDescription { get; init; }

    /// <summary> Gets Rank.</summary>
    public required int Rank { get; init; }

    /// <summary> Gets UnlockArea.</summary>
    /// <remarks> references <see cref="WorldAreasDat"/> on <see cref="Specification.GetWorldAreasDat"/> index.</remarks>
    public required int? UnlockArea { get; init; }

    /// <summary>
    /// Gets RecipeUnlockDisplayDat data.
    /// </summary>
    /// <param name="dataLoader">data loader.</param>
    /// <returns>array of RecipeUnlockDisplayDat.</returns>
    internal static RecipeUnlockDisplayDat[] Load(DataLoader dataLoader)
    {
        if (dataLoader is null)
        {
            throw new ArgumentNullException(nameof(dataLoader));
        }

        const string filePath = "Data/RecipeUnlockDisplay.dat64";
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new RecipeUnlockDisplayDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading RecipeId
            (var recipeidLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Description
            (var descriptionLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading CraftingItemClassCategoriesKeys
            (var tempcraftingitemclasscategorieskeysLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var craftingitemclasscategorieskeysLoading = tempcraftingitemclasscategorieskeysLoading.AsReadOnly();

            // loading UnlockDescription
            (var unlockdescriptionLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Rank
            (var rankLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading UnlockArea
            (var unlockareaLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            if (offset != expectedOffset)
            {
                throw new NotImplementedException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new RecipeUnlockDisplayDat()
            {
                RecipeId = recipeidLoading,
                Description = descriptionLoading,
                CraftingItemClassCategoriesKeys = craftingitemclasscategorieskeysLoading,
                UnlockDescription = unlockdescriptionLoading,
                Rank = rankLoading,
                UnlockArea = unlockareaLoading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
