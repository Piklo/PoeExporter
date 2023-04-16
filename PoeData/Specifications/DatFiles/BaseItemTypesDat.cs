// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.DatFiles;

/// <summary>
/// Class containing BaseItemTypes.dat data.
/// </summary>
public sealed partial class BaseItemTypesDat
{
    /// <summary> Gets Id.</summary>
    public required string Id { get; init; }

    /// <summary> Gets ItemClassesKey.</summary>
    /// <remarks> references <see cref="ItemClassesDat"/> on <see cref="Specification.GetItemClassesDat"/> index.</remarks>
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
    /// <remarks> references <see cref="FlavourTextDat"/> on <see cref="Specification.GetFlavourTextDat"/> index.</remarks>
    public required int? FlavourTextKey { get; init; }

    /// <summary> Gets Implicit_ModsKeys.</summary>
    /// <remarks> references <see cref="ModsDat"/> on <see cref="Specification.GetModsDat"/> index.</remarks>
    public required ReadOnlyCollection<int> Implicit_ModsKeys { get; init; }

    /// <summary> Gets SizeOnGround.</summary>
    public required int SizeOnGround { get; init; }

    /// <summary> Gets SoundEffect.</summary>
    /// <remarks> references <see cref="SoundEffectsDat"/> on <see cref="Specification.GetSoundEffectsDat"/> index.</remarks>
    public required int? SoundEffect { get; init; }

    /// <summary> Gets TagsKeys.</summary>
    /// <remarks> references <see cref="TagsDat"/> on <see cref="Specification.GetTagsDat"/> index.</remarks>
    public required ReadOnlyCollection<int> TagsKeys { get; init; }

    /// <summary> Gets ModDomain.</summary>
    /// <remarks> references <see cref="ModDomainsDat"/> on <see cref="Specification.GetModDomainsDat"/> index.</remarks>
    public required int ModDomain { get; init; }

    /// <summary> Gets SiteVisibility.</summary>
    /// <remarks> references <see cref="BaseItemTypeVisibilityDat"/> on <see cref="Specification.GetBaseItemTypeVisibilityDat"/> index.</remarks>
    public required int SiteVisibility { get; init; }

    /// <summary> Gets ItemVisualIdentity.</summary>
    /// <remarks> references <see cref="ItemVisualIdentityDat"/> on <see cref="Specification.GetItemVisualIdentityDat"/> index.</remarks>
    public required int? ItemVisualIdentity { get; init; }

    /// <summary> Gets HASH32.</summary>
    public required int HASH32 { get; init; }

    /// <summary> Gets VendorRecipe_AchievementItems.</summary>
    /// <remarks> references <see cref="AchievementItemsDat"/> on <see cref="Specification.GetAchievementItemsDat"/> index.</remarks>
    public required ReadOnlyCollection<int> VendorRecipe_AchievementItems { get; init; }

    /// <summary> Gets Inflection.</summary>
    public required string Inflection { get; init; }

    /// <summary> Gets Equip_AchievementItemsKey.</summary>
    /// <remarks> references <see cref="AchievementItemsDat"/> on <see cref="Specification.GetAchievementItemsDat"/> index.</remarks>
    public required int? Equip_AchievementItemsKey { get; init; }

    /// <summary> Gets a value indicating whether IsCorrupted is set.</summary>
    public required bool IsCorrupted { get; init; }

    /// <summary> Gets Identify_AchievementItems.</summary>
    /// <remarks> references <see cref="AchievementItemsDat"/> on <see cref="Specification.GetAchievementItemsDat"/> index.</remarks>
    public required ReadOnlyCollection<int> Identify_AchievementItems { get; init; }

    /// <summary> Gets IdentifyMagic_AchievementItems.</summary>
    /// <remarks> references <see cref="AchievementItemsDat"/> on <see cref="Specification.GetAchievementItemsDat"/> index.</remarks>
    public required ReadOnlyCollection<int> IdentifyMagic_AchievementItems { get; init; }

    /// <summary> Gets FragmentBaseItemTypesKey.</summary>
    /// <remarks> references <see cref="BaseItemTypesDat"/> on <see cref="Specification.GetBaseItemTypesDat"/> index.</remarks>
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
    /// <remarks> references <see cref="TradeMarketCategoryDat"/> on <see cref="Specification.GetTradeMarketCategoryDat"/> index.</remarks>
    public required int? TradeMarketCategory { get; init; }

    /// <summary> Gets a value indicating whether Unknown279 is set.</summary>
    public required bool Unknown279 { get; init; }

    /// <summary> Gets Achievement.</summary>
    /// <remarks> references <see cref="AchievementItemsDat"/> on <see cref="Specification.GetAchievementItemsDat"/> index.</remarks>
    public required ReadOnlyCollection<int> Achievement { get; init; }

    /// <summary>
    /// Gets BaseItemTypesDat data.
    /// </summary>
    /// <param name="dataLoader">data loader.</param>
    /// <returns>array of BaseItemTypesDat.</returns>
    internal static BaseItemTypesDat[] Load(DataLoader dataLoader)
    {
        if (dataLoader is null)
        {
            throw new ArgumentNullException(nameof(dataLoader));
        }

        const string filePath = "Data/BaseItemTypes.dat64";
        var decompressedFile = dataLoader.GetFileBytes(filePath);

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

            // loading Unknown279
            (var unknown279Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Achievement
            (var tempachievementLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var achievementLoading = tempachievementLoading.AsReadOnly();

            if (offset != expectedOffset)
            {
                throw new SchemaMismatchException($"offset {offset} != expectedOffset {expectedOffset}");
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
                Unknown279 = unknown279Loading,
                Achievement = achievementLoading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
