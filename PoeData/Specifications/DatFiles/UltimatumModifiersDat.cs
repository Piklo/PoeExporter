// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.DatFiles;

/// <summary>
/// Class containing UltimatumModifiers.dat data.
/// </summary>
public sealed partial class UltimatumModifiersDat
{
    /// <summary> Gets Id.</summary>
    public required string Id { get; init; }

    /// <summary> Gets Types.</summary>
    /// <remarks> references <see cref="UltimatumModifierTypesDat"/> on <see cref="Specification.GetUltimatumModifierTypesDat"/> index.</remarks>
    public required ReadOnlyCollection<int> Types { get; init; }

    /// <summary> Gets ExtraMods.</summary>
    /// <remarks> references <see cref="ModsDat"/> on <see cref="Specification.GetModsDat"/> index.</remarks>
    public required ReadOnlyCollection<int> ExtraMods { get; init; }

    /// <summary> Gets TypesFiltered.</summary>
    /// <remarks> references <see cref="UltimatumModifierTypesDat"/> on <see cref="Specification.GetUltimatumModifierTypesDat"/> index.</remarks>
    public required ReadOnlyCollection<int> TypesFiltered { get; init; }

    /// <summary> Gets DaemonSpawningData.</summary>
    /// <remarks> references <see cref="DaemonSpawningDataDat"/> on <see cref="Specification.GetDaemonSpawningDataDat"/> index.</remarks>
    public required int? DaemonSpawningData { get; init; }

    /// <summary> Gets PreviousTiers.</summary>
    /// <remarks> references <see cref="UltimatumModifiersDat"/> on <see cref="Specification.GetUltimatumModifiersDat"/> index.</remarks>
    public required ReadOnlyCollection<int> PreviousTiers { get; init; }

    /// <summary> Gets a value indicating whether Unknown88 is set.</summary>
    public required bool Unknown88 { get; init; }

    /// <summary> Gets Bosses.</summary>
    /// <remarks> references <see cref="MonsterVarietiesDat"/> on <see cref="Specification.GetMonsterVarietiesDat"/> index.</remarks>
    public required ReadOnlyCollection<int> Bosses { get; init; }

    /// <summary> Gets Radius.</summary>
    public required int Radius { get; init; }

    /// <summary> Gets Name.</summary>
    public required string Name { get; init; }

    /// <summary> Gets Icon.</summary>
    public required string Icon { get; init; }

    /// <summary> Gets HASH16.</summary>
    public required int HASH16 { get; init; }

    /// <summary> Gets TypesExtra.</summary>
    /// <remarks> references <see cref="UltimatumModifierTypesDat"/> on <see cref="Specification.GetUltimatumModifierTypesDat"/> index.</remarks>
    public required ReadOnlyCollection<int> TypesExtra { get; init; }

    /// <summary> Gets MonsterTypesApplyingRuin.</summary>
    public required int MonsterTypesApplyingRuin { get; init; }

    /// <summary> Gets MiscAnimated.</summary>
    /// <remarks> references <see cref="MiscAnimatedDat"/> on <see cref="Specification.GetMiscAnimatedDat"/> index.</remarks>
    public required int? MiscAnimated { get; init; }

    /// <summary> Gets BuffTemplates.</summary>
    /// <remarks> references <see cref="BuffTemplatesDat"/> on <see cref="Specification.GetBuffTemplatesDat"/> index.</remarks>
    public required ReadOnlyCollection<int> BuffTemplates { get; init; }

    /// <summary> Gets Tier.</summary>
    public required int Tier { get; init; }

    /// <summary> Gets Unknown185.</summary>
    public required int Unknown185 { get; init; }

    /// <summary> Gets Description.</summary>
    public required string Description { get; init; }

    /// <summary> Gets MonsterSpawners.</summary>
    public required ReadOnlyCollection<string> MonsterSpawners { get; init; }

    /// <summary> Gets a value indicating whether Unknown213 is set.</summary>
    public required bool Unknown213 { get; init; }

    /// <summary> Gets Achievements.</summary>
    /// <remarks> references <see cref="AchievementItemsDat"/> on <see cref="Specification.GetAchievementItemsDat"/> index.</remarks>
    public required ReadOnlyCollection<int> Achievements { get; init; }

    /// <summary> Gets TextAudio.</summary>
    /// <remarks> references <see cref="NPCTextAudioDat"/> on <see cref="Specification.GetNPCTextAudioDat"/> index.</remarks>
    public required int? TextAudio { get; init; }

    /// <summary> Gets UniqueMapMod.</summary>
    /// <remarks> references <see cref="ModsDat"/> on <see cref="Specification.GetModsDat"/> index.</remarks>
    public required int? UniqueMapMod { get; init; }

    /// <summary>
    /// Gets UltimatumModifiersDat data.
    /// </summary>
    /// <param name="dataLoader">data loader.</param>
    /// <returns>array of UltimatumModifiersDat.</returns>
    internal static UltimatumModifiersDat[] Load(DataLoader dataLoader)
    {
        if (dataLoader is null)
        {
            throw new ArgumentNullException(nameof(dataLoader));
        }

        const string filePath = "Data/UltimatumModifiers.dat64";
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new UltimatumModifiersDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading Id
            (var idLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Types
            (var temptypesLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var typesLoading = temptypesLoading.AsReadOnly();

            // loading ExtraMods
            (var tempextramodsLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var extramodsLoading = tempextramodsLoading.AsReadOnly();

            // loading TypesFiltered
            (var temptypesfilteredLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var typesfilteredLoading = temptypesfilteredLoading.AsReadOnly();

            // loading DaemonSpawningData
            (var daemonspawningdataLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading PreviousTiers
            (var tempprevioustiersLoading, offset) = SpecificationFileLoader.LoadRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var previoustiersLoading = tempprevioustiersLoading.AsReadOnly();

            // loading Unknown88
            (var unknown88Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Bosses
            (var tempbossesLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var bossesLoading = tempbossesLoading.AsReadOnly();

            // loading Radius
            (var radiusLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Name
            (var nameLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Icon
            (var iconLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading HASH16
            (var hash16Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading TypesExtra
            (var temptypesextraLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var typesextraLoading = temptypesextraLoading.AsReadOnly();

            // loading MonsterTypesApplyingRuin
            (var monstertypesapplyingruinLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading MiscAnimated
            (var miscanimatedLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading BuffTemplates
            (var tempbufftemplatesLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var bufftemplatesLoading = tempbufftemplatesLoading.AsReadOnly();

            // loading Tier
            (var tierLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown185
            (var unknown185Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Description
            (var descriptionLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading MonsterSpawners
            (var tempmonsterspawnersLoading, offset) = SpecificationFileLoader.LoadStringArray(decompressedFile, offset, dataOffset);
            var monsterspawnersLoading = tempmonsterspawnersLoading.AsReadOnly();

            // loading Unknown213
            (var unknown213Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Achievements
            (var tempachievementsLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var achievementsLoading = tempachievementsLoading.AsReadOnly();

            // loading TextAudio
            (var textaudioLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading UniqueMapMod
            (var uniquemapmodLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            if (offset != expectedOffset)
            {
                throw new NotImplementedException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new UltimatumModifiersDat()
            {
                Id = idLoading,
                Types = typesLoading,
                ExtraMods = extramodsLoading,
                TypesFiltered = typesfilteredLoading,
                DaemonSpawningData = daemonspawningdataLoading,
                PreviousTiers = previoustiersLoading,
                Unknown88 = unknown88Loading,
                Bosses = bossesLoading,
                Radius = radiusLoading,
                Name = nameLoading,
                Icon = iconLoading,
                HASH16 = hash16Loading,
                TypesExtra = typesextraLoading,
                MonsterTypesApplyingRuin = monstertypesapplyingruinLoading,
                MiscAnimated = miscanimatedLoading,
                BuffTemplates = bufftemplatesLoading,
                Tier = tierLoading,
                Unknown185 = unknown185Loading,
                Description = descriptionLoading,
                MonsterSpawners = monsterspawnersLoading,
                Unknown213 = unknown213Loading,
                Achievements = achievementsLoading,
                TextAudio = textaudioLoading,
                UniqueMapMod = uniquemapmodLoading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
