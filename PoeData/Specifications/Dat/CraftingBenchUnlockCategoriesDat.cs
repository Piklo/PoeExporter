// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.Dat;

/// <summary>
/// Class containing CraftingBenchUnlockCategories.dat data.
/// </summary>
public sealed partial class CraftingBenchUnlockCategoriesDat : IDat<CraftingBenchUnlockCategoriesDat>
{
    /// <summary> Gets Id.</summary>
    public required string Id { get; init; }

    /// <summary> Gets Unknown8.</summary>
    public required int Unknown8 { get; init; }

    /// <summary> Gets Unknown12.</summary>
    public required ReadOnlyCollection<int> Unknown12 { get; init; }

    /// <summary> Gets UnlockType.</summary>
    public required string UnlockType { get; init; }

    /// <summary> Gets CraftingItemClassCategories.</summary>
    /// <remarks> references <see cref="CraftingItemClassCategoriesDat"/> on <see cref="Specification.GetCraftingItemClassCategoriesDat"/> index.</remarks>
    public required ReadOnlyCollection<int> CraftingItemClassCategories { get; init; }

    /// <summary> Gets ObtainingDescription.</summary>
    public required string ObtainingDescription { get; init; }

    /// <inheritdoc/>
    public static CraftingBenchUnlockCategoriesDat[] Load(Specification specification)
    {
        if (specification is null)
        {
            throw new ArgumentNullException(nameof(specification));
        }

        var fileToFind = Encoding.ASCII.GetBytes("Data/CraftingBenchUnlockCategories.dat64");
        var fileRecord = specification.DataLoader.GetFileRecord(fileToFind);
        var decompressedFile = specification.DataLoader.GetFileBytes(fileRecord);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new CraftingBenchUnlockCategoriesDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading Id
            (var idLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Unknown8
            (var unknown8Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown12
            (var tempunknown12Loading, offset) = SpecificationFileLoader.LoadIntArray(decompressedFile, offset, dataOffset);
            var unknown12Loading = tempunknown12Loading.AsReadOnly();

            // loading UnlockType
            (var unlocktypeLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading CraftingItemClassCategories
            (var tempcraftingitemclasscategoriesLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var craftingitemclasscategoriesLoading = tempcraftingitemclasscategoriesLoading.AsReadOnly();

            // loading ObtainingDescription
            (var obtainingdescriptionLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            if (offset != expectedOffset)
            {
                throw new NotImplementedException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new CraftingBenchUnlockCategoriesDat()
            {
                Id = idLoading,
                Unknown8 = unknown8Loading,
                Unknown12 = unknown12Loading,
                UnlockType = unlocktypeLoading,
                CraftingItemClassCategories = craftingitemclasscategoriesLoading,
                ObtainingDescription = obtainingdescriptionLoading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
