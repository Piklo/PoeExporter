// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.SpecificationFiles;

/// <summary>
/// Class containing BaseItemTypes.dat data.
/// </summary>
public sealed partial class BaseItemTypesDat : ISpecificationFile<BaseItemTypesDat>
{
    /// <summary> Gets Id.</summary>
    public required string Id { get; init; }

    /// <summary> Gets ItemClassesKey.</summary>
    public required int? ItemClassesKey { get; init; }

    /// <summary> Gets Width.</summary>
    public required int Width { get; init; }

    /// <summary> Gets Height.</summary>
    public required int Height { get; init; }

    /// <summary> Gets Name.</summary>
    public required string Name { get; init; }

    /// <summary> Gets InheritsFrom.</summary>
    public required string InheritsFrom { get; init; }

    /// <summary> Gets DropLevel.</summary>
    public required int DropLevel { get; init; }

    /// <summary> Gets FlavourTextKey.</summary>
    public required int? FlavourTextKey { get; init; }

    /// <summary> Gets Implicit_ModsKeys.</summary>
    public required ReadOnlyCollection<int> Implicit_ModsKeys { get; init; }

    /// <summary> Gets SizeOnGround.</summary>
    public required int SizeOnGround { get; init; }

    /// <summary> Gets SoundEffect.</summary>
    public required int? SoundEffect { get; init; }

    /// <summary> Gets TagsKeys.</summary>
    public required ReadOnlyCollection<int> TagsKeys { get; init; }

    /// <summary> Gets ModDomain.</summary>
    public required int ModDomain { get; init; }

    /// <summary> Gets SiteVisibility.</summary>
    public required int SiteVisibility { get; init; }

    /// <summary> Gets ItemVisualIdentity.</summary>
    public required int? ItemVisualIdentity { get; init; }

    /// <summary> Gets HASH32.</summary>
    public required int HASH32 { get; init; }

    /// <summary> Gets VendorRecipe_AchievementItems.</summary>
    public required ReadOnlyCollection<int> VendorRecipe_AchievementItems { get; init; }

    /// <summary> Gets Inflection.</summary>
    public required string Inflection { get; init; }

    /// <summary> Gets Equip_AchievementItemsKey.</summary>
    public required int? Equip_AchievementItemsKey { get; init; }

    /// <summary> Gets a value indicating whether IsCorrupted is set.</summary>
    public required bool IsCorrupted { get; init; }

    /// <summary> Gets Identify_AchievementItems.</summary>
    public required ReadOnlyCollection<int> Identify_AchievementItems { get; init; }

    /// <summary> Gets IdentifyMagic_AchievementItems.</summary>
    public required ReadOnlyCollection<int> IdentifyMagic_AchievementItems { get; init; }

    /// <summary> Gets FragmentBaseItemTypesKey.</summary>
    public required int? FragmentBaseItemTypesKey { get; init; }

    /// <summary> Gets a value indicating whether Unknown229 is set.</summary>
    public required bool Unknown229 { get; init; }

    /// <summary> Gets Unknown230.</summary>
    public required int? Unknown230 { get; init; }

    /// <summary> Gets Unknown246.</summary>
    public required int? Unknown246 { get; init; }

    /// <summary> Gets a value indicating whether Unknown262 is set.</summary>
    public required bool Unknown262 { get; init; }

    /// <summary> Gets TradeMarketCategory.</summary>
    public required int? TradeMarketCategory { get; init; }

    /// <inheritdoc/>
    public static BaseItemTypesDat[] Load(Specification specification)
    {
        if (specification is null)
        {
            throw new ArgumentNullException(nameof(specification));
        }

        var fileToFind = Encoding.ASCII.GetBytes("Data/BaseItemTypes.dat64");
        var fileRecord = specification.DataLoader.GetFileRecord(fileToFind);
        var decompressedFile = specification.DataLoader.GetFileBytes(fileRecord);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new BaseItemTypesDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading referenced tables if any
            // specification.GetItemClassesDat();
            // specification.GetFlavourTextDat();
            // specification.GetModsDat();
            // specification.GetSoundEffectsDat();
            // specification.GetTagsDat();
            // specification.GetItemVisualIdentityDat();
            // specification.GetAchievementItemsDat();
            // specification.GetTradeMarketCategoryDat();

            // loading Id
            (var idLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading ItemClassesKey
            (var itemclasseskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading Width
            (var widthLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Height
            (var heightLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Name
            (var nameLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading InheritsFrom
            (var inheritsfromLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading DropLevel
            (var droplevelLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading FlavourTextKey
            (var flavourtextkeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading Implicit_ModsKeys
            (var tempimplicit_modskeysLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var implicit_modskeysLoading = tempimplicit_modskeysLoading.AsReadOnly();

            // loading SizeOnGround
            (var sizeongroundLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading SoundEffect
            (var soundeffectLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading TagsKeys
            (var temptagskeysLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var tagskeysLoading = temptagskeysLoading.AsReadOnly();

            // loading ModDomain
            (var moddomainLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading SiteVisibility
            (var sitevisibilityLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading ItemVisualIdentity
            (var itemvisualidentityLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading HASH32
            (var hash32Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading VendorRecipe_AchievementItems
            (var tempvendorrecipe_achievementitemsLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var vendorrecipe_achievementitemsLoading = tempvendorrecipe_achievementitemsLoading.AsReadOnly();

            // loading Inflection
            (var inflectionLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Equip_AchievementItemsKey
            (var equip_achievementitemskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading IsCorrupted
            (var iscorruptedLoading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Identify_AchievementItems
            (var tempidentify_achievementitemsLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var identify_achievementitemsLoading = tempidentify_achievementitemsLoading.AsReadOnly();

            // loading IdentifyMagic_AchievementItems
            (var tempidentifymagic_achievementitemsLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var identifymagic_achievementitemsLoading = tempidentifymagic_achievementitemsLoading.AsReadOnly();

            // loading FragmentBaseItemTypesKey
            (var fragmentbaseitemtypeskeyLoading, offset) = SpecificationFileLoader.LoadRowPrimaryKey(decompressedFile, offset);

            // loading Unknown229
            (var unknown229Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown230
            (var unknown230Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading Unknown246
            (var unknown246Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading Unknown262
            (var unknown262Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading TradeMarketCategory
            (var trademarketcategoryLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            if (offset != expectedOffset)
            {
                throw new NotImplementedException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new BaseItemTypesDat()
            {
                Id = idLoading,
                ItemClassesKey = itemclasseskeyLoading,
                Width = widthLoading,
                Height = heightLoading,
                Name = nameLoading,
                InheritsFrom = inheritsfromLoading,
                DropLevel = droplevelLoading,
                FlavourTextKey = flavourtextkeyLoading,
                Implicit_ModsKeys = implicit_modskeysLoading,
                SizeOnGround = sizeongroundLoading,
                SoundEffect = soundeffectLoading,
                TagsKeys = tagskeysLoading,
                ModDomain = moddomainLoading,
                SiteVisibility = sitevisibilityLoading,
                ItemVisualIdentity = itemvisualidentityLoading,
                HASH32 = hash32Loading,
                VendorRecipe_AchievementItems = vendorrecipe_achievementitemsLoading,
                Inflection = inflectionLoading,
                Equip_AchievementItemsKey = equip_achievementitemskeyLoading,
                IsCorrupted = iscorruptedLoading,
                Identify_AchievementItems = identify_achievementitemsLoading,
                IdentifyMagic_AchievementItems = identifymagic_achievementitemsLoading,
                FragmentBaseItemTypesKey = fragmentbaseitemtypeskeyLoading,
                Unknown229 = unknown229Loading,
                Unknown230 = unknown230Loading,
                Unknown246 = unknown246Loading,
                Unknown262 = unknown262Loading,
                TradeMarketCategory = trademarketcategoryLoading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
