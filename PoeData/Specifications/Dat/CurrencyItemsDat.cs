// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.Dat;

/// <summary>
/// Class containing CurrencyItems.dat data.
/// </summary>
public sealed partial class CurrencyItemsDat : ISpecificationFile<CurrencyItemsDat>
{
    /// <summary> Gets BaseItemTypesKey.</summary>
    public required int? BaseItemTypesKey { get; init; }

    /// <summary> Gets Stacks.</summary>
    public required int Stacks { get; init; }

    /// <summary> Gets CurrencyUseType.</summary>
    public required int CurrencyUseType { get; init; }

    /// <summary> Gets Action.</summary>
    public required string Action { get; init; }

    /// <summary> Gets Directions.</summary>
    public required string Directions { get; init; }

    /// <summary> Gets FullStack_BaseItemTypesKey.</summary>
    public required int? FullStack_BaseItemTypesKey { get; init; }

    /// <summary> Gets Description.</summary>
    public required string Description { get; init; }

    /// <summary> Gets Usage_AchievementItemsKeys.</summary>
    public required ReadOnlyCollection<int> Usage_AchievementItemsKeys { get; init; }

    /// <summary> Gets a value indicating whether Unknown80 is set.</summary>
    public required bool Unknown80 { get; init; }

    /// <summary> Gets CosmeticTypeName.</summary>
    public required string CosmeticTypeName { get; init; }

    /// <summary> Gets Possession_AchievementItemsKey.</summary>
    public required int? Possession_AchievementItemsKey { get; init; }

    /// <summary> Gets Unknown105.</summary>
    public required ReadOnlyCollection<int> Unknown105 { get; init; }

    /// <summary> Gets Unknown121.</summary>
    public required ReadOnlyCollection<int> Unknown121 { get; init; }

    /// <summary> Gets CurrencyTab_StackSize.</summary>
    public required int CurrencyTab_StackSize { get; init; }

    /// <summary> Gets XBoxDirections.</summary>
    public required string XBoxDirections { get; init; }

    /// <summary> Gets Unknown149.</summary>
    public required int Unknown149 { get; init; }

    /// <summary> Gets Unknown153.</summary>
    public required int? Unknown153 { get; init; }

    /// <summary> Gets Unknown169.</summary>
    public required int? Unknown169 { get; init; }

    /// <summary> Gets ModifyMapsAchievements.</summary>
    public required ReadOnlyCollection<int> ModifyMapsAchievements { get; init; }

    /// <summary> Gets ModifyContractsAchievements.</summary>
    public required ReadOnlyCollection<int> ModifyContractsAchievements { get; init; }

    /// <summary> Gets CombineAchievements.</summary>
    public required ReadOnlyCollection<int> CombineAchievements { get; init; }

    /// <summary> Gets Unknown233.</summary>
    public required int Unknown233 { get; init; }

    /// <summary> Gets Unknown237.</summary>
    public required ReadOnlyCollection<int> Unknown237 { get; init; }

    /// <summary> Gets ShopTag.</summary>
    public required int? ShopTag { get; init; }

    /// <summary> Gets a value indicating whether IsHardmode is set.</summary>
    public required bool IsHardmode { get; init; }

    /// <summary> Gets DescriptionHardmode.</summary>
    public required string DescriptionHardmode { get; init; }

    /// <inheritdoc/>
    public static CurrencyItemsDat[] Load(Specification specification)
    {
        if (specification is null)
        {
            throw new ArgumentNullException(nameof(specification));
        }

        var fileToFind = Encoding.ASCII.GetBytes("Data/CurrencyItems.dat64");
        var fileRecord = specification.DataLoader.GetFileRecord(fileToFind);
        var decompressedFile = specification.DataLoader.GetFileBytes(fileRecord);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new CurrencyItemsDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading referenced tables if any
            // specification.GetBaseItemTypesDat();
            // specification.GetAchievementItemsDat();
            // specification.GetShopTagDat();

            // loading BaseItemTypesKey
            (var baseitemtypeskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading Stacks
            (var stacksLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading CurrencyUseType
            (var currencyusetypeLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Action
            (var actionLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Directions
            (var directionsLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading FullStack_BaseItemTypesKey
            (var fullstack_baseitemtypeskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading Description
            (var descriptionLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Usage_AchievementItemsKeys
            (var tempusage_achievementitemskeysLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var usage_achievementitemskeysLoading = tempusage_achievementitemskeysLoading.AsReadOnly();

            // loading Unknown80
            (var unknown80Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading CosmeticTypeName
            (var cosmetictypenameLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Possession_AchievementItemsKey
            (var possession_achievementitemskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading Unknown105
            (var tempunknown105Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var unknown105Loading = tempunknown105Loading.AsReadOnly();

            // loading Unknown121
            (var tempunknown121Loading, offset) = SpecificationFileLoader.LoadIntArray(decompressedFile, offset, dataOffset);
            var unknown121Loading = tempunknown121Loading.AsReadOnly();

            // loading CurrencyTab_StackSize
            (var currencytab_stacksizeLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading XBoxDirections
            (var xboxdirectionsLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Unknown149
            (var unknown149Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown153
            (var unknown153Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading Unknown169
            (var unknown169Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading ModifyMapsAchievements
            (var tempmodifymapsachievementsLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var modifymapsachievementsLoading = tempmodifymapsachievementsLoading.AsReadOnly();

            // loading ModifyContractsAchievements
            (var tempmodifycontractsachievementsLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var modifycontractsachievementsLoading = tempmodifycontractsachievementsLoading.AsReadOnly();

            // loading CombineAchievements
            (var tempcombineachievementsLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var combineachievementsLoading = tempcombineachievementsLoading.AsReadOnly();

            // loading Unknown233
            (var unknown233Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown237
            (var tempunknown237Loading, offset) = SpecificationFileLoader.LoadIntArray(decompressedFile, offset, dataOffset);
            var unknown237Loading = tempunknown237Loading.AsReadOnly();

            // loading ShopTag
            (var shoptagLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading IsHardmode
            (var ishardmodeLoading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading DescriptionHardmode
            (var descriptionhardmodeLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            if (offset != expectedOffset)
            {
                throw new NotImplementedException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new CurrencyItemsDat()
            {
                BaseItemTypesKey = baseitemtypeskeyLoading,
                Stacks = stacksLoading,
                CurrencyUseType = currencyusetypeLoading,
                Action = actionLoading,
                Directions = directionsLoading,
                FullStack_BaseItemTypesKey = fullstack_baseitemtypeskeyLoading,
                Description = descriptionLoading,
                Usage_AchievementItemsKeys = usage_achievementitemskeysLoading,
                Unknown80 = unknown80Loading,
                CosmeticTypeName = cosmetictypenameLoading,
                Possession_AchievementItemsKey = possession_achievementitemskeyLoading,
                Unknown105 = unknown105Loading,
                Unknown121 = unknown121Loading,
                CurrencyTab_StackSize = currencytab_stacksizeLoading,
                XBoxDirections = xboxdirectionsLoading,
                Unknown149 = unknown149Loading,
                Unknown153 = unknown153Loading,
                Unknown169 = unknown169Loading,
                ModifyMapsAchievements = modifymapsachievementsLoading,
                ModifyContractsAchievements = modifycontractsachievementsLoading,
                CombineAchievements = combineachievementsLoading,
                Unknown233 = unknown233Loading,
                Unknown237 = unknown237Loading,
                ShopTag = shoptagLoading,
                IsHardmode = ishardmodeLoading,
                DescriptionHardmode = descriptionhardmodeLoading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
