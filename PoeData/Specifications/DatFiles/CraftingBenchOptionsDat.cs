// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.DatFiles;

/// <summary>
/// Class containing CraftingBenchOptions.dat data.
/// </summary>
public sealed partial class CraftingBenchOptionsDat
{
    /// <summary> Gets HideoutNPCsKey.</summary>
    /// <remarks> references <see cref="HideoutNPCsDat"/> on <see cref="Specification.GetHideoutNPCsDat"/> index.</remarks>
    public required int? HideoutNPCsKey { get; init; }

    /// <summary> Gets Order.</summary>
    public required int Order { get; init; }

    /// <summary> Gets AddMod.</summary>
    /// <remarks> references <see cref="ModsDat"/> on <see cref="Specification.GetModsDat"/> index.</remarks>
    public required int? AddMod { get; init; }

    /// <summary> Gets Cost_BaseItemTypes.</summary>
    /// <remarks> references <see cref="BaseItemTypesDat"/> on <see cref="Specification.GetBaseItemTypesDat"/> index.</remarks>
    public required ReadOnlyCollection<int> Cost_BaseItemTypes { get; init; }

    /// <summary> Gets Cost_Values.</summary>
    public required ReadOnlyCollection<int> Cost_Values { get; init; }

    /// <summary> Gets RequiredLevel.</summary>
    public required int RequiredLevel { get; init; }

    /// <summary> Gets Name.</summary>
    public required string Name { get; init; }

    /// <summary> Gets CraftingBenchCustomAction.</summary>
    /// <remarks> references <see cref="CraftingBenchCustomActionsDat"/> on <see cref="Specification.GetCraftingBenchCustomActionsDat"/> index.</remarks>
    public required int CraftingBenchCustomAction { get; init; }

    /// <summary> Gets ItemClasses.</summary>
    /// <remarks> references <see cref="ItemClassesDat"/> on <see cref="Specification.GetItemClassesDat"/> index.</remarks>
    public required ReadOnlyCollection<int> ItemClasses { get; init; }

    /// <summary> Gets Links.</summary>
    public required int Links { get; init; }

    /// <summary> Gets SocketColours.</summary>
    public required string SocketColours { get; init; }

    /// <summary> Gets Sockets.</summary>
    public required int Sockets { get; init; }

    /// <summary> Gets ItemQuantity.</summary>
    public required int ItemQuantity { get; init; }

    /// <summary> Gets Unknown120.</summary>
    public required ReadOnlyCollection<int> Unknown120 { get; init; }

    /// <summary> Gets Description.</summary>
    public required string Description { get; init; }

    /// <summary> Gets a value indicating whether IsDisabled is set.</summary>
    public required bool IsDisabled { get; init; }

    /// <summary> Gets a value indicating whether IsAreaOption is set.</summary>
    public required bool IsAreaOption { get; init; }

    /// <summary> Gets RecipeIds.</summary>
    /// <remarks> references <see cref="RecipeUnlockDisplayDat"/> on <see cref="RecipeUnlockDisplayDat.RecipeId"/>.</remarks>
    public required ReadOnlyCollection<int> RecipeIds { get; init; }

    /// <summary> Gets Tier.</summary>
    public required int Tier { get; init; }

    /// <summary> Gets CraftingItemClassCategories.</summary>
    /// <remarks> references <see cref="CraftingItemClassCategoriesDat"/> on <see cref="Specification.GetCraftingItemClassCategoriesDat"/> index.</remarks>
    public required ReadOnlyCollection<int> CraftingItemClassCategories { get; init; }

    /// <summary> Gets Unknown182.</summary>
    public required int Unknown182 { get; init; }

    /// <summary> Gets UnlockCategory.</summary>
    /// <remarks> references <see cref="CraftingBenchUnlockCategoriesDat"/> on <see cref="Specification.GetCraftingBenchUnlockCategoriesDat"/> index.</remarks>
    public required int? UnlockCategory { get; init; }

    /// <summary> Gets UnveilsRequired.</summary>
    public required int UnveilsRequired { get; init; }

    /// <summary> Gets UnveilsRequired2.</summary>
    public required int UnveilsRequired2 { get; init; }

    /// <summary> Gets Unknown210.</summary>
    public required ReadOnlyCollection<int> Unknown210 { get; init; }

    /// <summary> Gets Unknown226.</summary>
    public required ReadOnlyCollection<int> Unknown226 { get; init; }

    /// <summary> Gets Unknown242.</summary>
    public required int Unknown242 { get; init; }

    /// <summary> Gets Unknown246.</summary>
    public required int Unknown246 { get; init; }

    /// <summary> Gets Unknown250.</summary>
    public required int? Unknown250 { get; init; }

    /// <summary> Gets AddEnchantment.</summary>
    /// <remarks> references <see cref="ModsDat"/> on <see cref="Specification.GetModsDat"/> index.</remarks>
    public required int? AddEnchantment { get; init; }

    /// <summary> Gets SortCategory.</summary>
    /// <remarks> references <see cref="CraftingBenchSortCategoriesDat"/> on <see cref="Specification.GetCraftingBenchSortCategoriesDat"/> index.</remarks>
    public required int? SortCategory { get; init; }

    /// <summary> Gets Unknown298.</summary>
    public required int? Unknown298 { get; init; }

    /// <summary> Gets a value indicating whether Unknown314 is set.</summary>
    public required bool Unknown314 { get; init; }

    /// <summary> Gets Unknown315.</summary>
    public required int Unknown315 { get; init; }

    /// <summary> Gets Unknown319.</summary>
    /// <remarks> references <see cref="StatsDat"/> on <see cref="Specification.GetStatsDat"/> index.</remarks>
    public required int? Unknown319 { get; init; }

    /// <summary> Gets Unknown335.</summary>
    /// <remarks> references <see cref="StatsDat"/> on <see cref="Specification.GetStatsDat"/> index.</remarks>
    public required int? Unknown335 { get; init; }

    /// <summary> Gets Unknown351.</summary>
    /// <remarks> references <see cref="StatsDat"/> on <see cref="Specification.GetStatsDat"/> index.</remarks>
    public required int? Unknown351 { get; init; }

    /// <summary>
    /// Gets CraftingBenchOptionsDat data.
    /// </summary>
    /// <param name="dataLoader">data loader.</param>
    /// <returns>array of CraftingBenchOptionsDat.</returns>
    internal static CraftingBenchOptionsDat[] Load(DataLoader dataLoader)
    {
        if (dataLoader is null)
        {
            throw new ArgumentNullException(nameof(dataLoader));
        }

        const string filePath = "Data/CraftingBenchOptions.dat64";
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new CraftingBenchOptionsDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading HideoutNPCsKey
            (var hideoutnpcskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading Order
            (var orderLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading AddMod
            (var addmodLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading Cost_BaseItemTypes
            (var tempcost_baseitemtypesLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var cost_baseitemtypesLoading = tempcost_baseitemtypesLoading.AsReadOnly();

            // loading Cost_Values
            (var tempcost_valuesLoading, offset) = SpecificationFileLoader.LoadIntArray(decompressedFile, offset, dataOffset);
            var cost_valuesLoading = tempcost_valuesLoading.AsReadOnly();

            // loading RequiredLevel
            (var requiredlevelLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Name
            (var nameLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading CraftingBenchCustomAction
            (var craftingbenchcustomactionLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading ItemClasses
            (var tempitemclassesLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var itemclassesLoading = tempitemclassesLoading.AsReadOnly();

            // loading Links
            (var linksLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading SocketColours
            (var socketcoloursLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Sockets
            (var socketsLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading ItemQuantity
            (var itemquantityLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown120
            (var tempunknown120Loading, offset) = SpecificationFileLoader.LoadIntArray(decompressedFile, offset, dataOffset);
            var unknown120Loading = tempunknown120Loading.AsReadOnly();

            // loading Description
            (var descriptionLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading IsDisabled
            (var isdisabledLoading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading IsAreaOption
            (var isareaoptionLoading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading RecipeIds
            (var temprecipeidsLoading, offset) = SpecificationFileLoader.LoadIntArray(decompressedFile, offset, dataOffset);
            var recipeidsLoading = temprecipeidsLoading.AsReadOnly();

            // loading Tier
            (var tierLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading CraftingItemClassCategories
            (var tempcraftingitemclasscategoriesLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var craftingitemclasscategoriesLoading = tempcraftingitemclasscategoriesLoading.AsReadOnly();

            // loading Unknown182
            (var unknown182Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading UnlockCategory
            (var unlockcategoryLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading UnveilsRequired
            (var unveilsrequiredLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading UnveilsRequired2
            (var unveilsrequired2Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown210
            (var tempunknown210Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var unknown210Loading = tempunknown210Loading.AsReadOnly();

            // loading Unknown226
            (var tempunknown226Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var unknown226Loading = tempunknown226Loading.AsReadOnly();

            // loading Unknown242
            (var unknown242Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown246
            (var unknown246Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown250
            (var unknown250Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading AddEnchantment
            (var addenchantmentLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading SortCategory
            (var sortcategoryLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading Unknown298
            (var unknown298Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading Unknown314
            (var unknown314Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown315
            (var unknown315Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown319
            (var unknown319Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading Unknown335
            (var unknown335Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading Unknown351
            (var unknown351Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            if (offset != expectedOffset)
            {
                throw new SchemaMismatchException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new CraftingBenchOptionsDat()
            {
                HideoutNPCsKey = hideoutnpcskeyLoading,
                Order = orderLoading,
                AddMod = addmodLoading,
                Cost_BaseItemTypes = cost_baseitemtypesLoading,
                Cost_Values = cost_valuesLoading,
                RequiredLevel = requiredlevelLoading,
                Name = nameLoading,
                CraftingBenchCustomAction = craftingbenchcustomactionLoading,
                ItemClasses = itemclassesLoading,
                Links = linksLoading,
                SocketColours = socketcoloursLoading,
                Sockets = socketsLoading,
                ItemQuantity = itemquantityLoading,
                Unknown120 = unknown120Loading,
                Description = descriptionLoading,
                IsDisabled = isdisabledLoading,
                IsAreaOption = isareaoptionLoading,
                RecipeIds = recipeidsLoading,
                Tier = tierLoading,
                CraftingItemClassCategories = craftingitemclasscategoriesLoading,
                Unknown182 = unknown182Loading,
                UnlockCategory = unlockcategoryLoading,
                UnveilsRequired = unveilsrequiredLoading,
                UnveilsRequired2 = unveilsrequired2Loading,
                Unknown210 = unknown210Loading,
                Unknown226 = unknown226Loading,
                Unknown242 = unknown242Loading,
                Unknown246 = unknown246Loading,
                Unknown250 = unknown250Loading,
                AddEnchantment = addenchantmentLoading,
                SortCategory = sortcategoryLoading,
                Unknown298 = unknown298Loading,
                Unknown314 = unknown314Loading,
                Unknown315 = unknown315Loading,
                Unknown319 = unknown319Loading,
                Unknown335 = unknown335Loading,
                Unknown351 = unknown351Loading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
