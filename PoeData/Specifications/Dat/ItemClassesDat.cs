// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.Dat;

/// <summary>
/// Class containing ItemClasses.dat data.
/// </summary>
public sealed partial class ItemClassesDat : ISpecificationFile<ItemClassesDat>
{
    /// <summary> Gets Id.</summary>
    public required string Id { get; init; }

    /// <summary> Gets Name.</summary>
    public required string Name { get; init; }

    /// <summary> Gets TradeMarketCategory.</summary>
    public required int? TradeMarketCategory { get; init; }

    /// <summary> Gets ItemClassCategory.</summary>
    public required int? ItemClassCategory { get; init; }

    /// <summary> Gets a value indicating whether RemovedIfLeavesArea is set.</summary>
    public required bool RemovedIfLeavesArea { get; init; }

    /// <summary> Gets Unknown49.</summary>
    public required ReadOnlyCollection<int> Unknown49 { get; init; }

    /// <summary> Gets IdentifyAchievements.</summary>
    public required ReadOnlyCollection<int> IdentifyAchievements { get; init; }

    /// <summary> Gets a value indicating whether AllocateToMapOwner is set.</summary>
    public required bool AllocateToMapOwner { get; init; }

    /// <summary> Gets a value indicating whether AlwaysAllocate is set.</summary>
    public required bool AlwaysAllocate { get; init; }

    /// <summary> Gets a value indicating whether CanHaveVeiledMods is set.</summary>
    public required bool CanHaveVeiledMods { get; init; }

    /// <summary> Gets PickedUpQuest.</summary>
    public required int? PickedUpQuest { get; init; }

    /// <summary> Gets Unknown100.</summary>
    public required int Unknown100 { get; init; }

    /// <summary> Gets a value indicating whether AlwaysShow is set.</summary>
    public required bool AlwaysShow { get; init; }

    /// <summary> Gets a value indicating whether CanBeCorrupted is set.</summary>
    public required bool CanBeCorrupted { get; init; }

    /// <summary> Gets a value indicating whether CanHaveIncubators is set.</summary>
    public required bool CanHaveIncubators { get; init; }

    /// <summary> Gets a value indicating whether CanHaveInfluence is set.</summary>
    public required bool CanHaveInfluence { get; init; }

    /// <summary> Gets a value indicating whether CanBeDoubleCorrupted is set.</summary>
    public required bool CanBeDoubleCorrupted { get; init; }

    /// <summary> Gets a value indicating whether CanHaveAspects is set.</summary>
    public required bool CanHaveAspects { get; init; }

    /// <summary> Gets a value indicating whether CanTransferSkin is set.</summary>
    public required bool CanTransferSkin { get; init; }

    /// <summary> Gets ItemStance.</summary>
    public required int? ItemStance { get; init; }

    /// <summary> Gets a value indicating whether CanScourge is set.</summary>
    public required bool CanScourge { get; init; }

    /// <summary> Gets a value indicating whether CanUpgradeRarity is set.</summary>
    public required bool CanUpgradeRarity { get; init; }

    /// <summary> Gets a value indicating whether Unknown129 is set.</summary>
    public required bool Unknown129 { get; init; }

    /// <summary> Gets a value indicating whether Unknown130 is set.</summary>
    public required bool Unknown130 { get; init; }

    /// <summary> Gets MaxInventoryDimensions.</summary>
    public required ReadOnlyCollection<int> MaxInventoryDimensions { get; init; }

    /// <summary> Gets Flags.</summary>
    public required ReadOnlyCollection<int> Flags { get; init; }

    /// <summary> Gets a value indicating whether IsUnmodifiable is set.</summary>
    public required bool IsUnmodifiable { get; init; }

    /// <summary> Gets a value indicating whether CanBeFractured is set.</summary>
    public required bool CanBeFractured { get; init; }

    /// <summary> Gets EquipAchievements.</summary>
    public required int? EquipAchievements { get; init; }

    /// <inheritdoc/>
    public static ItemClassesDat[] Load(Specification specification)
    {
        if (specification is null)
        {
            throw new ArgumentNullException(nameof(specification));
        }

        var fileToFind = Encoding.ASCII.GetBytes("Data/ItemClasses.dat64");
        var fileRecord = specification.DataLoader.GetFileRecord(fileToFind);
        var decompressedFile = specification.DataLoader.GetFileBytes(fileRecord);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new ItemClassesDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading referenced tables if any
            // specification.GetTradeMarketCategoryDat();
            // specification.GetItemClassCategoriesDat();
            // specification.GetAchievementItemsDat();
            // specification.GetQuestFlagsDat();
            // specification.GetItemStancesDat();

            // loading Id
            (var idLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Name
            (var nameLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading TradeMarketCategory
            (var trademarketcategoryLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading ItemClassCategory
            (var itemclasscategoryLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading RemovedIfLeavesArea
            (var removedifleavesareaLoading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown49
            (var tempunknown49Loading, offset) = SpecificationFileLoader.LoadIntArray(decompressedFile, offset, dataOffset);
            var unknown49Loading = tempunknown49Loading.AsReadOnly();

            // loading IdentifyAchievements
            (var tempidentifyachievementsLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var identifyachievementsLoading = tempidentifyachievementsLoading.AsReadOnly();

            // loading AllocateToMapOwner
            (var allocatetomapownerLoading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading AlwaysAllocate
            (var alwaysallocateLoading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading CanHaveVeiledMods
            (var canhaveveiledmodsLoading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading PickedUpQuest
            (var pickedupquestLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading Unknown100
            (var unknown100Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading AlwaysShow
            (var alwaysshowLoading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading CanBeCorrupted
            (var canbecorruptedLoading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading CanHaveIncubators
            (var canhaveincubatorsLoading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading CanHaveInfluence
            (var canhaveinfluenceLoading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading CanBeDoubleCorrupted
            (var canbedoublecorruptedLoading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading CanHaveAspects
            (var canhaveaspectsLoading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading CanTransferSkin
            (var cantransferskinLoading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading ItemStance
            (var itemstanceLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading CanScourge
            (var canscourgeLoading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading CanUpgradeRarity
            (var canupgraderarityLoading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown129
            (var unknown129Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown130
            (var unknown130Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading MaxInventoryDimensions
            (var tempmaxinventorydimensionsLoading, offset) = SpecificationFileLoader.LoadIntArray(decompressedFile, offset, dataOffset);
            var maxinventorydimensionsLoading = tempmaxinventorydimensionsLoading.AsReadOnly();

            // loading Flags
            (var tempflagsLoading, offset) = SpecificationFileLoader.LoadIntArray(decompressedFile, offset, dataOffset);
            var flagsLoading = tempflagsLoading.AsReadOnly();

            // loading IsUnmodifiable
            (var isunmodifiableLoading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading CanBeFractured
            (var canbefracturedLoading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading EquipAchievements
            (var equipachievementsLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            if (offset != expectedOffset)
            {
                throw new NotImplementedException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new ItemClassesDat()
            {
                Id = idLoading,
                Name = nameLoading,
                TradeMarketCategory = trademarketcategoryLoading,
                ItemClassCategory = itemclasscategoryLoading,
                RemovedIfLeavesArea = removedifleavesareaLoading,
                Unknown49 = unknown49Loading,
                IdentifyAchievements = identifyachievementsLoading,
                AllocateToMapOwner = allocatetomapownerLoading,
                AlwaysAllocate = alwaysallocateLoading,
                CanHaveVeiledMods = canhaveveiledmodsLoading,
                PickedUpQuest = pickedupquestLoading,
                Unknown100 = unknown100Loading,
                AlwaysShow = alwaysshowLoading,
                CanBeCorrupted = canbecorruptedLoading,
                CanHaveIncubators = canhaveincubatorsLoading,
                CanHaveInfluence = canhaveinfluenceLoading,
                CanBeDoubleCorrupted = canbedoublecorruptedLoading,
                CanHaveAspects = canhaveaspectsLoading,
                CanTransferSkin = cantransferskinLoading,
                ItemStance = itemstanceLoading,
                CanScourge = canscourgeLoading,
                CanUpgradeRarity = canupgraderarityLoading,
                Unknown129 = unknown129Loading,
                Unknown130 = unknown130Loading,
                MaxInventoryDimensions = maxinventorydimensionsLoading,
                Flags = flagsLoading,
                IsUnmodifiable = isunmodifiableLoading,
                CanBeFractured = canbefracturedLoading,
                EquipAchievements = equipachievementsLoading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
