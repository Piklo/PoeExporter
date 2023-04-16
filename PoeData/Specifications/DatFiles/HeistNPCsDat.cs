// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.DatFiles;

/// <summary>
/// Class containing HeistNPCs.dat data.
/// </summary>
public sealed partial class HeistNPCsDat
{
    /// <summary> Gets NPCsKey.</summary>
    /// <remarks> references <see cref="NPCsDat"/> on <see cref="Specification.LoadNPCsDat"/> index.</remarks>
    public required int? NPCsKey { get; init; }

    /// <summary> Gets MonsterVarietiesKey.</summary>
    /// <remarks> references <see cref="MonsterVarietiesDat"/> on <see cref="Specification.LoadMonsterVarietiesDat"/> index.</remarks>
    public required int? MonsterVarietiesKey { get; init; }

    /// <summary> Gets SkillLevel_HeistJobsKeys.</summary>
    /// <remarks> references <see cref="HeistJobsDat"/> on <see cref="Specification.LoadHeistJobsDat"/> index.</remarks>
    public required ReadOnlyCollection<int> SkillLevel_HeistJobsKeys { get; init; }

    /// <summary> Gets PortraitFile.</summary>
    public required string PortraitFile { get; init; }

    /// <summary> Gets HeistNPCStatsKeys.</summary>
    /// <remarks> references <see cref="HeistNPCStatsDat"/> on <see cref="Specification.LoadHeistNPCStatsDat"/> index.</remarks>
    public required ReadOnlyCollection<int> HeistNPCStatsKeys { get; init; }

    /// <summary> Gets StatValues.</summary>
    public required ReadOnlyCollection<float> StatValues { get; init; }

    /// <summary> Gets Unknown88.</summary>
    public required float Unknown88 { get; init; }

    /// <summary> Gets SkillLevel_Values.</summary>
    public required ReadOnlyCollection<int> SkillLevel_Values { get; init; }

    /// <summary> Gets Name.</summary>
    public required string Name { get; init; }

    /// <summary> Gets SilhouetteFile.</summary>
    public required string SilhouetteFile { get; init; }

    /// <summary> Gets Unknown124.</summary>
    public required int Unknown124 { get; init; }

    /// <summary> Gets Unknown128.</summary>
    public required int Unknown128 { get; init; }

    /// <summary> Gets HeistNPCsKey.</summary>
    /// <remarks> references <see cref="HeistNPCsDat"/> on <see cref="Specification.LoadHeistNPCsDat"/> index.</remarks>
    public required int? HeistNPCsKey { get; init; }

    /// <summary> Gets StatValues2.</summary>
    public required ReadOnlyCollection<float> StatValues2 { get; init; }

    /// <summary> Gets Ally_NPCsKey.</summary>
    /// <remarks> references <see cref="NPCsDat"/> on <see cref="Specification.LoadNPCsDat"/> index.</remarks>
    public required int? Ally_NPCsKey { get; init; }

    /// <summary> Gets ActiveNPCIcon.</summary>
    public required string ActiveNPCIcon { get; init; }

    /// <summary> Gets HeistJobsKey.</summary>
    /// <remarks> references <see cref="HeistJobsDat"/> on <see cref="Specification.LoadHeistJobsDat"/> index.</remarks>
    public required int? HeistJobsKey { get; init; }

    /// <summary> Gets Equip_AchievementItemsKeys.</summary>
    /// <remarks> references <see cref="AchievementItemsDat"/> on <see cref="Specification.LoadAchievementItemsDat"/> index.</remarks>
    public required ReadOnlyCollection<int> Equip_AchievementItemsKeys { get; init; }

    /// <summary> Gets AOFile.</summary>
    public required string AOFile { get; init; }

    /// <summary> Gets Unknown220.</summary>
    public required int? Unknown220 { get; init; }

    /// <summary>
    /// Gets HeistNPCsDat data.
    /// </summary>
    /// <param name="dataLoader">data loader.</param>
    /// <returns>array of HeistNPCsDat.</returns>
    internal static HeistNPCsDat[] Load(DataLoader dataLoader)
    {
        if (dataLoader is null)
        {
            throw new ArgumentNullException(nameof(dataLoader));
        }

        const string filePath = "Data/HeistNPCs.dat64";
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new HeistNPCsDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading NPCsKey
            (var npcskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading MonsterVarietiesKey
            (var monstervarietieskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading SkillLevel_HeistJobsKeys
            (var tempskilllevel_heistjobskeysLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var skilllevel_heistjobskeysLoading = tempskilllevel_heistjobskeysLoading.AsReadOnly();

            // loading PortraitFile
            (var portraitfileLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading HeistNPCStatsKeys
            (var tempheistnpcstatskeysLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var heistnpcstatskeysLoading = tempheistnpcstatskeysLoading.AsReadOnly();

            // loading StatValues
            (var tempstatvaluesLoading, offset) = SpecificationFileLoader.LoadFloatArray(decompressedFile, offset, dataOffset);
            var statvaluesLoading = tempstatvaluesLoading.AsReadOnly();

            // loading Unknown88
            (var unknown88Loading, offset) = SpecificationFileLoader.LoadFloat(decompressedFile, offset);

            // loading SkillLevel_Values
            (var tempskilllevel_valuesLoading, offset) = SpecificationFileLoader.LoadIntArray(decompressedFile, offset, dataOffset);
            var skilllevel_valuesLoading = tempskilllevel_valuesLoading.AsReadOnly();

            // loading Name
            (var nameLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading SilhouetteFile
            (var silhouettefileLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Unknown124
            (var unknown124Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown128
            (var unknown128Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading HeistNPCsKey
            (var heistnpcskeyLoading, offset) = SpecificationFileLoader.LoadRowPrimaryKey(decompressedFile, offset);

            // loading StatValues2
            (var tempstatvalues2Loading, offset) = SpecificationFileLoader.LoadFloatArray(decompressedFile, offset, dataOffset);
            var statvalues2Loading = tempstatvalues2Loading.AsReadOnly();

            // loading Ally_NPCsKey
            (var ally_npcskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading ActiveNPCIcon
            (var activenpciconLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading HeistJobsKey
            (var heistjobskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading Equip_AchievementItemsKeys
            (var tempequip_achievementitemskeysLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var equip_achievementitemskeysLoading = tempequip_achievementitemskeysLoading.AsReadOnly();

            // loading AOFile
            (var aofileLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Unknown220
            (var unknown220Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            if (offset != expectedOffset)
            {
                throw new SchemaMismatchException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new HeistNPCsDat()
            {
                NPCsKey = npcskeyLoading,
                MonsterVarietiesKey = monstervarietieskeyLoading,
                SkillLevel_HeistJobsKeys = skilllevel_heistjobskeysLoading,
                PortraitFile = portraitfileLoading,
                HeistNPCStatsKeys = heistnpcstatskeysLoading,
                StatValues = statvaluesLoading,
                Unknown88 = unknown88Loading,
                SkillLevel_Values = skilllevel_valuesLoading,
                Name = nameLoading,
                SilhouetteFile = silhouettefileLoading,
                Unknown124 = unknown124Loading,
                Unknown128 = unknown128Loading,
                HeistNPCsKey = heistnpcskeyLoading,
                StatValues2 = statvalues2Loading,
                Ally_NPCsKey = ally_npcskeyLoading,
                ActiveNPCIcon = activenpciconLoading,
                HeistJobsKey = heistjobskeyLoading,
                Equip_AchievementItemsKeys = equip_achievementitemskeysLoading,
                AOFile = aofileLoading,
                Unknown220 = unknown220Loading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
