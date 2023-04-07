// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.Dat;

/// <summary>
/// Class containing ItemVisualIdentity.dat data.
/// </summary>
public sealed partial class ItemVisualIdentityDat : IDat<ItemVisualIdentityDat>
{
    /// <summary> Gets Id.</summary>
    public required string Id { get; init; }

    /// <summary> Gets DDSFile.</summary>
    public required string DDSFile { get; init; }

    /// <summary> Gets AOFile.</summary>
    public required string AOFile { get; init; }

    /// <summary> Gets InventorySoundEffect.</summary>
    /// <remarks> references <see cref="SoundEffectsDat"/> on <see cref="Specification.GetSoundEffectsDat"/> index.</remarks>
    public required int? InventorySoundEffect { get; init; }

    /// <summary> Gets Unknown40.</summary>
    public required int Unknown40 { get; init; }

    /// <summary> Gets AOFile2.</summary>
    public required string AOFile2 { get; init; }

    /// <summary> Gets MarauderSMFiles.</summary>
    public required ReadOnlyCollection<string> MarauderSMFiles { get; init; }

    /// <summary> Gets RangerSMFiles.</summary>
    public required ReadOnlyCollection<string> RangerSMFiles { get; init; }

    /// <summary> Gets WitchSMFiles.</summary>
    public required ReadOnlyCollection<string> WitchSMFiles { get; init; }

    /// <summary> Gets DuelistDexSMFiles.</summary>
    public required ReadOnlyCollection<string> DuelistDexSMFiles { get; init; }

    /// <summary> Gets TemplarSMFiles.</summary>
    public required ReadOnlyCollection<string> TemplarSMFiles { get; init; }

    /// <summary> Gets ShadowSMFiles.</summary>
    public required ReadOnlyCollection<string> ShadowSMFiles { get; init; }

    /// <summary> Gets ScionSMFiles.</summary>
    public required ReadOnlyCollection<string> ScionSMFiles { get; init; }

    /// <summary> Gets MarauderShape.</summary>
    public required string MarauderShape { get; init; }

    /// <summary> Gets RangerShape.</summary>
    public required string RangerShape { get; init; }

    /// <summary> Gets WitchShape.</summary>
    public required string WitchShape { get; init; }

    /// <summary> Gets DuelistShape.</summary>
    public required string DuelistShape { get; init; }

    /// <summary> Gets TemplarShape.</summary>
    public required string TemplarShape { get; init; }

    /// <summary> Gets ShadowShape.</summary>
    public required string ShadowShape { get; init; }

    /// <summary> Gets ScionShape.</summary>
    public required string ScionShape { get; init; }

    /// <summary> Gets Unknown220.</summary>
    public required int Unknown220 { get; init; }

    /// <summary> Gets Unknown224.</summary>
    public required int Unknown224 { get; init; }

    /// <summary> Gets Pickup_AchievementItemsKeys.</summary>
    /// <remarks> references <see cref="AchievementItemsDat"/> on <see cref="Specification.GetAchievementItemsDat"/> index.</remarks>
    public required ReadOnlyCollection<int> Pickup_AchievementItemsKeys { get; init; }

    /// <summary> Gets SMFiles.</summary>
    public required ReadOnlyCollection<string> SMFiles { get; init; }

    /// <summary> Gets Identify_AchievementItemsKeys.</summary>
    /// <remarks> references <see cref="AchievementItemsDat"/> on <see cref="Specification.GetAchievementItemsDat"/> index.</remarks>
    public required ReadOnlyCollection<int> Identify_AchievementItemsKeys { get; init; }

    /// <summary> Gets EPKFile.</summary>
    public required string EPKFile { get; init; }

    /// <summary> Gets Corrupt_AchievementItemsKeys.</summary>
    /// <remarks> references <see cref="AchievementItemsDat"/> on <see cref="Specification.GetAchievementItemsDat"/> index.</remarks>
    public required ReadOnlyCollection<int> Corrupt_AchievementItemsKeys { get; init; }

    /// <summary> Gets a value indicating whether IsAlternateArt is set.</summary>
    public required bool IsAlternateArt { get; init; }

    /// <summary> Gets a value indicating whether Unknown301 is set.</summary>
    public required bool Unknown301 { get; init; }

    /// <summary> Gets CreateCorruptedJewelAchievementItemsKey.</summary>
    /// <remarks> references <see cref="AchievementItemsDat"/> on <see cref="Specification.GetAchievementItemsDat"/> index.</remarks>
    public required int? CreateCorruptedJewelAchievementItemsKey { get; init; }

    /// <summary> Gets AnimationLocation.</summary>
    public required string AnimationLocation { get; init; }

    /// <summary> Gets Unknown326.</summary>
    public required string Unknown326 { get; init; }

    /// <summary> Gets Unknown334.</summary>
    public required string Unknown334 { get; init; }

    /// <summary> Gets Unknown342.</summary>
    public required string Unknown342 { get; init; }

    /// <summary> Gets Unknown350.</summary>
    public required string Unknown350 { get; init; }

    /// <summary> Gets Unknown358.</summary>
    public required string Unknown358 { get; init; }

    /// <summary> Gets Unknown366.</summary>
    public required string Unknown366 { get; init; }

    /// <summary> Gets Unknown374.</summary>
    public required string Unknown374 { get; init; }

    /// <summary> Gets Unknown382.</summary>
    public required string Unknown382 { get; init; }

    /// <summary> Gets Unknown390.</summary>
    public required string Unknown390 { get; init; }

    /// <summary> Gets Unknown398.</summary>
    public required string Unknown398 { get; init; }

    /// <summary> Gets Unknown406.</summary>
    public required string Unknown406 { get; init; }

    /// <summary> Gets Unknown414.</summary>
    public required string Unknown414 { get; init; }

    /// <summary> Gets a value indicating whether IsAtlasOfWorldsMapIcon is set.</summary>
    public required bool IsAtlasOfWorldsMapIcon { get; init; }

    /// <summary> Gets a value indicating whether IsTier16Icon is set.</summary>
    public required bool IsTier16Icon { get; init; }

    /// <summary> Gets Unknown424.</summary>
    public required ReadOnlyCollection<int> Unknown424 { get; init; }

    /// <summary> Gets a value indicating whether Unknown440 is set.</summary>
    public required bool Unknown440 { get; init; }

    /// <summary> Gets Unknown441.</summary>
    public required ReadOnlyCollection<int> Unknown441 { get; init; }

    /// <summary> Gets Unknown457.</summary>
    public required string Unknown457 { get; init; }

    /// <summary> Gets Unknown465.</summary>
    public required int Unknown465 { get; init; }

    /// <summary> Gets Unknown469.</summary>
    public required int? Unknown469 { get; init; }

    /// <summary> Gets Unknown485.</summary>
    public required int? Unknown485 { get; init; }

    /// <summary> Gets Unknown501.</summary>
    public required int? Unknown501 { get; init; }

    /// <summary> Gets Unknown517.</summary>
    public required int? Unknown517 { get; init; }

    /// <inheritdoc/>
    public static ItemVisualIdentityDat[] Load(Specification specification)
    {
        if (specification is null)
        {
            throw new ArgumentNullException(nameof(specification));
        }

        const string filePath = "Data/ItemVisualIdentity.dat64";
        var decompressedFile = specification.DataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new ItemVisualIdentityDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading Id
            (var idLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading DDSFile
            (var ddsfileLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading AOFile
            (var aofileLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading InventorySoundEffect
            (var inventorysoundeffectLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading Unknown40
            (var unknown40Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading AOFile2
            (var aofile2Loading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading MarauderSMFiles
            (var tempmaraudersmfilesLoading, offset) = SpecificationFileLoader.LoadStringArray(decompressedFile, offset, dataOffset);
            var maraudersmfilesLoading = tempmaraudersmfilesLoading.AsReadOnly();

            // loading RangerSMFiles
            (var temprangersmfilesLoading, offset) = SpecificationFileLoader.LoadStringArray(decompressedFile, offset, dataOffset);
            var rangersmfilesLoading = temprangersmfilesLoading.AsReadOnly();

            // loading WitchSMFiles
            (var tempwitchsmfilesLoading, offset) = SpecificationFileLoader.LoadStringArray(decompressedFile, offset, dataOffset);
            var witchsmfilesLoading = tempwitchsmfilesLoading.AsReadOnly();

            // loading DuelistDexSMFiles
            (var tempduelistdexsmfilesLoading, offset) = SpecificationFileLoader.LoadStringArray(decompressedFile, offset, dataOffset);
            var duelistdexsmfilesLoading = tempduelistdexsmfilesLoading.AsReadOnly();

            // loading TemplarSMFiles
            (var temptemplarsmfilesLoading, offset) = SpecificationFileLoader.LoadStringArray(decompressedFile, offset, dataOffset);
            var templarsmfilesLoading = temptemplarsmfilesLoading.AsReadOnly();

            // loading ShadowSMFiles
            (var tempshadowsmfilesLoading, offset) = SpecificationFileLoader.LoadStringArray(decompressedFile, offset, dataOffset);
            var shadowsmfilesLoading = tempshadowsmfilesLoading.AsReadOnly();

            // loading ScionSMFiles
            (var tempscionsmfilesLoading, offset) = SpecificationFileLoader.LoadStringArray(decompressedFile, offset, dataOffset);
            var scionsmfilesLoading = tempscionsmfilesLoading.AsReadOnly();

            // loading MarauderShape
            (var maraudershapeLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading RangerShape
            (var rangershapeLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading WitchShape
            (var witchshapeLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading DuelistShape
            (var duelistshapeLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading TemplarShape
            (var templarshapeLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading ShadowShape
            (var shadowshapeLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading ScionShape
            (var scionshapeLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Unknown220
            (var unknown220Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown224
            (var unknown224Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Pickup_AchievementItemsKeys
            (var temppickup_achievementitemskeysLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var pickup_achievementitemskeysLoading = temppickup_achievementitemskeysLoading.AsReadOnly();

            // loading SMFiles
            (var tempsmfilesLoading, offset) = SpecificationFileLoader.LoadStringArray(decompressedFile, offset, dataOffset);
            var smfilesLoading = tempsmfilesLoading.AsReadOnly();

            // loading Identify_AchievementItemsKeys
            (var tempidentify_achievementitemskeysLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var identify_achievementitemskeysLoading = tempidentify_achievementitemskeysLoading.AsReadOnly();

            // loading EPKFile
            (var epkfileLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Corrupt_AchievementItemsKeys
            (var tempcorrupt_achievementitemskeysLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var corrupt_achievementitemskeysLoading = tempcorrupt_achievementitemskeysLoading.AsReadOnly();

            // loading IsAlternateArt
            (var isalternateartLoading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown301
            (var unknown301Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading CreateCorruptedJewelAchievementItemsKey
            (var createcorruptedjewelachievementitemskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading AnimationLocation
            (var animationlocationLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Unknown326
            (var unknown326Loading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Unknown334
            (var unknown334Loading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Unknown342
            (var unknown342Loading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Unknown350
            (var unknown350Loading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Unknown358
            (var unknown358Loading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Unknown366
            (var unknown366Loading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Unknown374
            (var unknown374Loading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Unknown382
            (var unknown382Loading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Unknown390
            (var unknown390Loading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Unknown398
            (var unknown398Loading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Unknown406
            (var unknown406Loading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Unknown414
            (var unknown414Loading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading IsAtlasOfWorldsMapIcon
            (var isatlasofworldsmapiconLoading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading IsTier16Icon
            (var istier16iconLoading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown424
            (var tempunknown424Loading, offset) = SpecificationFileLoader.LoadIntArray(decompressedFile, offset, dataOffset);
            var unknown424Loading = tempunknown424Loading.AsReadOnly();

            // loading Unknown440
            (var unknown440Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown441
            (var tempunknown441Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var unknown441Loading = tempunknown441Loading.AsReadOnly();

            // loading Unknown457
            (var unknown457Loading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Unknown465
            (var unknown465Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown469
            (var unknown469Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading Unknown485
            (var unknown485Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading Unknown501
            (var unknown501Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading Unknown517
            (var unknown517Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            if (offset != expectedOffset)
            {
                throw new NotImplementedException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new ItemVisualIdentityDat()
            {
                Id = idLoading,
                DDSFile = ddsfileLoading,
                AOFile = aofileLoading,
                InventorySoundEffect = inventorysoundeffectLoading,
                Unknown40 = unknown40Loading,
                AOFile2 = aofile2Loading,
                MarauderSMFiles = maraudersmfilesLoading,
                RangerSMFiles = rangersmfilesLoading,
                WitchSMFiles = witchsmfilesLoading,
                DuelistDexSMFiles = duelistdexsmfilesLoading,
                TemplarSMFiles = templarsmfilesLoading,
                ShadowSMFiles = shadowsmfilesLoading,
                ScionSMFiles = scionsmfilesLoading,
                MarauderShape = maraudershapeLoading,
                RangerShape = rangershapeLoading,
                WitchShape = witchshapeLoading,
                DuelistShape = duelistshapeLoading,
                TemplarShape = templarshapeLoading,
                ShadowShape = shadowshapeLoading,
                ScionShape = scionshapeLoading,
                Unknown220 = unknown220Loading,
                Unknown224 = unknown224Loading,
                Pickup_AchievementItemsKeys = pickup_achievementitemskeysLoading,
                SMFiles = smfilesLoading,
                Identify_AchievementItemsKeys = identify_achievementitemskeysLoading,
                EPKFile = epkfileLoading,
                Corrupt_AchievementItemsKeys = corrupt_achievementitemskeysLoading,
                IsAlternateArt = isalternateartLoading,
                Unknown301 = unknown301Loading,
                CreateCorruptedJewelAchievementItemsKey = createcorruptedjewelachievementitemskeyLoading,
                AnimationLocation = animationlocationLoading,
                Unknown326 = unknown326Loading,
                Unknown334 = unknown334Loading,
                Unknown342 = unknown342Loading,
                Unknown350 = unknown350Loading,
                Unknown358 = unknown358Loading,
                Unknown366 = unknown366Loading,
                Unknown374 = unknown374Loading,
                Unknown382 = unknown382Loading,
                Unknown390 = unknown390Loading,
                Unknown398 = unknown398Loading,
                Unknown406 = unknown406Loading,
                Unknown414 = unknown414Loading,
                IsAtlasOfWorldsMapIcon = isatlasofworldsmapiconLoading,
                IsTier16Icon = istier16iconLoading,
                Unknown424 = unknown424Loading,
                Unknown440 = unknown440Loading,
                Unknown441 = unknown441Loading,
                Unknown457 = unknown457Loading,
                Unknown465 = unknown465Loading,
                Unknown469 = unknown469Loading,
                Unknown485 = unknown485Loading,
                Unknown501 = unknown501Loading,
                Unknown517 = unknown517Loading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
