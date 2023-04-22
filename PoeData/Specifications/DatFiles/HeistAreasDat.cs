﻿// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.DatFiles;

/// <summary>
/// Class containing HeistAreas.dat data.
/// </summary>
public sealed partial class HeistAreasDat
{
    /// <summary> Gets Id.</summary>
    public required string Id { get; init; }

    /// <summary> Gets WorldAreasKeys.</summary>
    /// <remarks> references <see cref="WorldAreasDat"/> on <see cref="Specification.LoadWorldAreasDat"/> index.</remarks>
    public required ReadOnlyCollection<int> WorldAreasKeys { get; init; }

    /// <summary> Gets Unknown24.</summary>
    public required int Unknown24 { get; init; }

    /// <summary> Gets EnvironmentsKey1.</summary>
    /// <remarks> references <see cref="EnvironmentsDat"/> on <see cref="Specification.LoadEnvironmentsDat"/> index.</remarks>
    public required int? EnvironmentsKey1 { get; init; }

    /// <summary> Gets EnvironmentsKey2.</summary>
    /// <remarks> references <see cref="EnvironmentsDat"/> on <see cref="Specification.LoadEnvironmentsDat"/> index.</remarks>
    public required int? EnvironmentsKey2 { get; init; }

    /// <summary> Gets HeistJobsKeys.</summary>
    /// <remarks> references <see cref="HeistJobsDat"/> on <see cref="Specification.LoadHeistJobsDat"/> index.</remarks>
    public required ReadOnlyCollection<int> HeistJobsKeys { get; init; }

    /// <summary> Gets Contract_BaseItemTypesKey.</summary>
    /// <remarks> references <see cref="BaseItemTypesDat"/> on <see cref="Specification.LoadBaseItemTypesDat"/> index.</remarks>
    public required int? Contract_BaseItemTypesKey { get; init; }

    /// <summary> Gets Blueprint_BaseItemTypesKey.</summary>
    /// <remarks> references <see cref="BaseItemTypesDat"/> on <see cref="Specification.LoadBaseItemTypesDat"/> index.</remarks>
    public required int? Blueprint_BaseItemTypesKey { get; init; }

    /// <summary> Gets DGRFile.</summary>
    public required string DGRFile { get; init; }

    /// <summary> Gets Unknown116.</summary>
    public required int Unknown116 { get; init; }

    /// <summary> Gets Unknown120.</summary>
    public required int Unknown120 { get; init; }

    /// <summary> Gets a value indicating whether Unknown124 is set.</summary>
    public required bool Unknown124 { get; init; }

    /// <summary> Gets a value indicating whether Unknown125 is set.</summary>
    public required bool Unknown125 { get; init; }

    /// <summary> Gets Blueprint_DDSFile.</summary>
    public required string Blueprint_DDSFile { get; init; }

    /// <summary> Gets Achievements1.</summary>
    /// <remarks> references <see cref="AchievementItemsDat"/> on <see cref="Specification.LoadAchievementItemsDat"/> index.</remarks>
    public required ReadOnlyCollection<int> Achievements1 { get; init; }

    /// <summary> Gets Achievements2.</summary>
    /// <remarks> references <see cref="AchievementItemsDat"/> on <see cref="Specification.LoadAchievementItemsDat"/> index.</remarks>
    public required ReadOnlyCollection<int> Achievements2 { get; init; }

    /// <summary> Gets Reward.</summary>
    /// <remarks> references <see cref="ClientStringsDat"/> on <see cref="Specification.LoadClientStringsDat"/> index.</remarks>
    public required int? Reward { get; init; }

    /// <summary> Gets Achievements3.</summary>
    /// <remarks> references <see cref="AchievementItemsDat"/> on <see cref="Specification.LoadAchievementItemsDat"/> index.</remarks>
    public required ReadOnlyCollection<int> Achievements3 { get; init; }

    /// <summary> Gets RewardHardmode.</summary>
    /// <remarks> references <see cref="ClientStringsDat"/> on <see cref="Specification.LoadClientStringsDat"/> index.</remarks>
    public required int? RewardHardmode { get; init; }

    /// <summary>
    /// Gets HeistAreasDat data.
    /// </summary>
    /// <param name="dataLoader">data loader.</param>
    /// <returns>array of HeistAreasDat.</returns>
    internal static HeistAreasDat[] Load(DataLoader dataLoader)
    {
        if (dataLoader is null)
        {
            throw new ArgumentNullException(nameof(dataLoader));
        }

        const string filePath = "Data/HeistAreas.dat64";
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new HeistAreasDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading Id
            (var idLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading WorldAreasKeys
            (var tempworldareaskeysLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var worldareaskeysLoading = tempworldareaskeysLoading.AsReadOnly();

            // loading Unknown24
            (var unknown24Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading EnvironmentsKey1
            (var environmentskey1Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading EnvironmentsKey2
            (var environmentskey2Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading HeistJobsKeys
            (var tempheistjobskeysLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var heistjobskeysLoading = tempheistjobskeysLoading.AsReadOnly();

            // loading Contract_BaseItemTypesKey
            (var contract_baseitemtypeskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading Blueprint_BaseItemTypesKey
            (var blueprint_baseitemtypeskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading DGRFile
            (var dgrfileLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Unknown116
            (var unknown116Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown120
            (var unknown120Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown124
            (var unknown124Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown125
            (var unknown125Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Blueprint_DDSFile
            (var blueprint_ddsfileLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Achievements1
            (var tempachievements1Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var achievements1Loading = tempachievements1Loading.AsReadOnly();

            // loading Achievements2
            (var tempachievements2Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var achievements2Loading = tempachievements2Loading.AsReadOnly();

            // loading Reward
            (var rewardLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading Achievements3
            (var tempachievements3Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var achievements3Loading = tempachievements3Loading.AsReadOnly();

            // loading RewardHardmode
            (var rewardhardmodeLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            if (offset != expectedOffset)
            {
                throw new SchemaMismatchException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new HeistAreasDat()
            {
                Id = idLoading,
                WorldAreasKeys = worldareaskeysLoading,
                Unknown24 = unknown24Loading,
                EnvironmentsKey1 = environmentskey1Loading,
                EnvironmentsKey2 = environmentskey2Loading,
                HeistJobsKeys = heistjobskeysLoading,
                Contract_BaseItemTypesKey = contract_baseitemtypeskeyLoading,
                Blueprint_BaseItemTypesKey = blueprint_baseitemtypeskeyLoading,
                DGRFile = dgrfileLoading,
                Unknown116 = unknown116Loading,
                Unknown120 = unknown120Loading,
                Unknown124 = unknown124Loading,
                Unknown125 = unknown125Loading,
                Blueprint_DDSFile = blueprint_ddsfileLoading,
                Achievements1 = achievements1Loading,
                Achievements2 = achievements2Loading,
                Reward = rewardLoading,
                Achievements3 = achievements3Loading,
                RewardHardmode = rewardhardmodeLoading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
