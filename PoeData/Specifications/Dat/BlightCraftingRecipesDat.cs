// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.Dat;

/// <summary>
/// Class containing BlightCraftingRecipes.dat data.
/// </summary>
public sealed partial class BlightCraftingRecipesDat : IDat<BlightCraftingRecipesDat>
{
    /// <summary> Gets Id.</summary>
    public required string Id { get; init; }

    /// <summary> Gets BlightCraftingItemsKeys.</summary>
    /// <remarks> references <see cref="BlightCraftingItemsDat"/> on <see cref="Specification.GetBlightCraftingItemsDat"/> index.</remarks>
    public required ReadOnlyCollection<int> BlightCraftingItemsKeys { get; init; }

    /// <summary> Gets BlightCraftingResultsKey.</summary>
    /// <remarks> references <see cref="BlightCraftingResultsDat"/> on <see cref="Specification.GetBlightCraftingResultsDat"/> index.</remarks>
    public required int? BlightCraftingResultsKey { get; init; }

    /// <summary> Gets BlightCraftingTypesKey.</summary>
    /// <remarks> references <see cref="BlightCraftingTypesDat"/> on <see cref="Specification.GetBlightCraftingTypesDat"/> index.</remarks>
    public required int? BlightCraftingTypesKey { get; init; }

    /// <inheritdoc/>
    public static BlightCraftingRecipesDat[] Load(Specification specification)
    {
        if (specification is null)
        {
            throw new ArgumentNullException(nameof(specification));
        }

        var fileToFind = Encoding.ASCII.GetBytes("Data/BlightCraftingRecipes.dat64");
        var fileRecord = specification.DataLoader.GetFileRecord(fileToFind);
        var decompressedFile = specification.DataLoader.GetFileBytes(fileRecord);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new BlightCraftingRecipesDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading Id
            (var idLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading BlightCraftingItemsKeys
            (var tempblightcraftingitemskeysLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var blightcraftingitemskeysLoading = tempblightcraftingitemskeysLoading.AsReadOnly();

            // loading BlightCraftingResultsKey
            (var blightcraftingresultskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading BlightCraftingTypesKey
            (var blightcraftingtypeskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            if (offset != expectedOffset)
            {
                throw new NotImplementedException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new BlightCraftingRecipesDat()
            {
                Id = idLoading,
                BlightCraftingItemsKeys = blightcraftingitemskeysLoading,
                BlightCraftingResultsKey = blightcraftingresultskeyLoading,
                BlightCraftingTypesKey = blightcraftingtypeskeyLoading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
