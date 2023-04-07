// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.Dat;

/// <summary>
/// Class containing Chests.dat data.
/// </summary>
public sealed partial class ChestsDat : IDat<ChestsDat>
{
    /// <summary> Gets Id.</summary>
    public required string Id { get; init; }

    /// <summary> Gets a value indicating whether Unknown8 is set.</summary>
    public required bool Unknown8 { get; init; }

    /// <summary> Gets Unknown9.</summary>
    public required int Unknown9 { get; init; }

    /// <summary> Gets Name.</summary>
    public required string Name { get; init; }

    /// <summary> Gets AOFiles.</summary>
    public required ReadOnlyCollection<string> AOFiles { get; init; }

    /// <summary> Gets a value indicating whether Unknown37 is set.</summary>
    public required bool Unknown37 { get; init; }

    /// <summary> Gets a value indicating whether Unknown38 is set.</summary>
    public required bool Unknown38 { get; init; }

    /// <summary> Gets Unknown39.</summary>
    public required int Unknown39 { get; init; }

    /// <summary> Gets a value indicating whether Unknown43 is set.</summary>
    public required bool Unknown43 { get; init; }

    /// <summary> Gets a value indicating whether Unknown44 is set.</summary>
    public required bool Unknown44 { get; init; }

    /// <summary> Gets Unknown45.</summary>
    public required int Unknown45 { get; init; }

    /// <summary> Gets Unknown49.</summary>
    public required ReadOnlyCollection<int> Unknown49 { get; init; }

    /// <summary> Gets BaseItemTypesKey.</summary>
    /// <remarks> references <see cref="BaseItemTypesDat"/> on <see cref="Specification.GetBaseItemTypesDat"/> index.</remarks>
    public required int? BaseItemTypesKey { get; init; }

    /// <summary> Gets a value indicating whether Unknown81 is set.</summary>
    public required bool Unknown81 { get; init; }

    /// <summary> Gets ModsKeys.</summary>
    /// <remarks> references <see cref="ModsDat"/> on <see cref="Specification.GetModsDat"/> index.</remarks>
    public required ReadOnlyCollection<int> ModsKeys { get; init; }

    /// <summary> Gets TagsKeys.</summary>
    /// <remarks> references <see cref="TagsDat"/> on <see cref="Specification.GetTagsDat"/> index.</remarks>
    public required ReadOnlyCollection<int> TagsKeys { get; init; }

    /// <summary> Gets ChestEffectsKey.</summary>
    /// <remarks> references <see cref="ChestEffectsDat"/> on <see cref="Specification.GetChestEffectsDat"/> index.</remarks>
    public required int? ChestEffectsKey { get; init; }

    /// <summary> Gets MinLevel.</summary>
    public required int MinLevel { get; init; }

    /// <summary> Gets Unknown134.</summary>
    public required string Unknown134 { get; init; }

    /// <summary> Gets MaxLevel.</summary>
    public required int MaxLevel { get; init; }

    /// <summary> Gets Corrupt_AchievementItemsKey.</summary>
    /// <remarks> references <see cref="AchievementItemsDat"/> on <see cref="Specification.GetAchievementItemsDat"/> index.</remarks>
    public required int? Corrupt_AchievementItemsKey { get; init; }

    /// <summary> Gets CurrencyUse_AchievementItemsKey.</summary>
    /// <remarks> references <see cref="AchievementItemsDat"/> on <see cref="Specification.GetAchievementItemsDat"/> index.</remarks>
    public required int? CurrencyUse_AchievementItemsKey { get; init; }

    /// <summary> Gets Encounter_AchievementItemsKeys.</summary>
    /// <remarks> references <see cref="AchievementItemsDat"/> on <see cref="Specification.GetAchievementItemsDat"/> index.</remarks>
    public required ReadOnlyCollection<int> Encounter_AchievementItemsKeys { get; init; }

    /// <summary> Gets Unknown194.</summary>
    public required int? Unknown194 { get; init; }

    /// <summary> Gets InheritsFrom.</summary>
    public required string InheritsFrom { get; init; }

    /// <summary> Gets a value indicating whether Unknown218 is set.</summary>
    public required bool Unknown218 { get; init; }

    /// <summary> Gets Unknown219.</summary>
    public required int? Unknown219 { get; init; }

    /// <summary> Gets Unknown235.</summary>
    public required ReadOnlyCollection<int> Unknown235 { get; init; }

    /// <summary> Gets Unknown251.</summary>
    public required string Unknown251 { get; init; }

    /// <summary> Gets Unknown259.</summary>
    public required int Unknown259 { get; init; }

    /// <summary> Gets Unknown263.</summary>
    public required int Unknown263 { get; init; }

    /// <summary> Gets a value indicating whether Unknown267 is set.</summary>
    public required bool Unknown267 { get; init; }

    /// <summary> Gets Unknown268.</summary>
    public required int? Unknown268 { get; init; }

    /// <summary> Gets Unknown284.</summary>
    public required int? Unknown284 { get; init; }

    /// <summary> Gets a value indicating whether Unknown300 is set.</summary>
    public required bool Unknown300 { get; init; }

    /// <summary> Gets a value indicating whether Unknown301 is set.</summary>
    public required bool Unknown301 { get; init; }

    /// <summary> Gets Unknown302.</summary>
    public required ReadOnlyCollection<int> Unknown302 { get; init; }

    /// <summary> Gets a value indicating whether IsHardmode is set.</summary>
    public required bool IsHardmode { get; init; }

    /// <summary> Gets StatsHardmode.</summary>
    /// <remarks> references <see cref="StatsDat"/> on <see cref="Specification.GetStatsDat"/> index.</remarks>
    public required ReadOnlyCollection<int> StatsHardmode { get; init; }

    /// <inheritdoc/>
    public static ChestsDat[] Load(Specification specification)
    {
        if (specification is null)
        {
            throw new ArgumentNullException(nameof(specification));
        }

        const string filePath = "Data/Chests.dat64";
        var decompressedFile = specification.DataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new ChestsDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading Id
            (var idLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Unknown8
            (var unknown8Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown9
            (var unknown9Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Name
            (var nameLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading AOFiles
            (var tempaofilesLoading, offset) = SpecificationFileLoader.LoadStringArray(decompressedFile, offset, dataOffset);
            var aofilesLoading = tempaofilesLoading.AsReadOnly();

            // loading Unknown37
            (var unknown37Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown38
            (var unknown38Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown39
            (var unknown39Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown43
            (var unknown43Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown44
            (var unknown44Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown45
            (var unknown45Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown49
            (var tempunknown49Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var unknown49Loading = tempunknown49Loading.AsReadOnly();

            // loading BaseItemTypesKey
            (var baseitemtypeskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading Unknown81
            (var unknown81Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading ModsKeys
            (var tempmodskeysLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var modskeysLoading = tempmodskeysLoading.AsReadOnly();

            // loading TagsKeys
            (var temptagskeysLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var tagskeysLoading = temptagskeysLoading.AsReadOnly();

            // loading ChestEffectsKey
            (var chesteffectskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading MinLevel
            (var minlevelLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown134
            (var unknown134Loading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading MaxLevel
            (var maxlevelLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Corrupt_AchievementItemsKey
            (var corrupt_achievementitemskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading CurrencyUse_AchievementItemsKey
            (var currencyuse_achievementitemskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading Encounter_AchievementItemsKeys
            (var tempencounter_achievementitemskeysLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var encounter_achievementitemskeysLoading = tempencounter_achievementitemskeysLoading.AsReadOnly();

            // loading Unknown194
            (var unknown194Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading InheritsFrom
            (var inheritsfromLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Unknown218
            (var unknown218Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown219
            (var unknown219Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading Unknown235
            (var tempunknown235Loading, offset) = SpecificationFileLoader.LoadIntArray(decompressedFile, offset, dataOffset);
            var unknown235Loading = tempunknown235Loading.AsReadOnly();

            // loading Unknown251
            (var unknown251Loading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Unknown259
            (var unknown259Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown263
            (var unknown263Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown267
            (var unknown267Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown268
            (var unknown268Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading Unknown284
            (var unknown284Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading Unknown300
            (var unknown300Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown301
            (var unknown301Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown302
            (var tempunknown302Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var unknown302Loading = tempunknown302Loading.AsReadOnly();

            // loading IsHardmode
            (var ishardmodeLoading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading StatsHardmode
            (var tempstatshardmodeLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var statshardmodeLoading = tempstatshardmodeLoading.AsReadOnly();

            if (offset != expectedOffset)
            {
                throw new NotImplementedException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new ChestsDat()
            {
                Id = idLoading,
                Unknown8 = unknown8Loading,
                Unknown9 = unknown9Loading,
                Name = nameLoading,
                AOFiles = aofilesLoading,
                Unknown37 = unknown37Loading,
                Unknown38 = unknown38Loading,
                Unknown39 = unknown39Loading,
                Unknown43 = unknown43Loading,
                Unknown44 = unknown44Loading,
                Unknown45 = unknown45Loading,
                Unknown49 = unknown49Loading,
                BaseItemTypesKey = baseitemtypeskeyLoading,
                Unknown81 = unknown81Loading,
                ModsKeys = modskeysLoading,
                TagsKeys = tagskeysLoading,
                ChestEffectsKey = chesteffectskeyLoading,
                MinLevel = minlevelLoading,
                Unknown134 = unknown134Loading,
                MaxLevel = maxlevelLoading,
                Corrupt_AchievementItemsKey = corrupt_achievementitemskeyLoading,
                CurrencyUse_AchievementItemsKey = currencyuse_achievementitemskeyLoading,
                Encounter_AchievementItemsKeys = encounter_achievementitemskeysLoading,
                Unknown194 = unknown194Loading,
                InheritsFrom = inheritsfromLoading,
                Unknown218 = unknown218Loading,
                Unknown219 = unknown219Loading,
                Unknown235 = unknown235Loading,
                Unknown251 = unknown251Loading,
                Unknown259 = unknown259Loading,
                Unknown263 = unknown263Loading,
                Unknown267 = unknown267Loading,
                Unknown268 = unknown268Loading,
                Unknown284 = unknown284Loading,
                Unknown300 = unknown300Loading,
                Unknown301 = unknown301Loading,
                Unknown302 = unknown302Loading,
                IsHardmode = ishardmodeLoading,
                StatsHardmode = statshardmodeLoading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
