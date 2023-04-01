﻿// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.SpecificationFiles;

/// <summary>
/// Class containing RitualRuneTypes.dat data.
/// </summary>
public sealed partial class RitualRuneTypesDat : ISpecificationFile<RitualRuneTypesDat>
{
    /// <summary> Gets Id.</summary>
    public required string Id { get; init; }

    /// <summary> Gets MiscAnimatedKey1.</summary>
    public required int? MiscAnimatedKey1 { get; init; }

    /// <summary> Gets SpawnWeight.</summary>
    public required int SpawnWeight { get; init; }

    /// <summary> Gets LevelMin.</summary>
    public required int LevelMin { get; init; }

    /// <summary> Gets LevelMax.</summary>
    public required int LevelMax { get; init; }

    /// <summary> Gets BuffDefinitionsKey.</summary>
    public required int? BuffDefinitionsKey { get; init; }

    /// <summary> Gets Unknown52.</summary>
    public required ReadOnlyCollection<int> Unknown52 { get; init; }

    /// <summary> Gets SpawnPatterns.</summary>
    public required ReadOnlyCollection<int> SpawnPatterns { get; init; }

    /// <summary> Gets ModsKey.</summary>
    public required ReadOnlyCollection<int> ModsKey { get; init; }

    /// <summary> Gets Unknown100.</summary>
    public required ReadOnlyCollection<int> Unknown100 { get; init; }

    /// <summary> Gets Unknown116.</summary>
    public required ReadOnlyCollection<int> Unknown116 { get; init; }

    /// <summary> Gets MiscAnimatedKey2.</summary>
    public required int? MiscAnimatedKey2 { get; init; }

    /// <summary> Gets EnvironmentsKey.</summary>
    public required int? EnvironmentsKey { get; init; }

    /// <summary> Gets Unknown164.</summary>
    public required int Unknown164 { get; init; }

    /// <summary> Gets Achievements.</summary>
    public required ReadOnlyCollection<int> Achievements { get; init; }

    /// <summary> Gets Type.</summary>
    public required string Type { get; init; }

    /// <summary> Gets Description.</summary>
    public required string Description { get; init; }

    /// <summary> Gets DaemonSpawningDataKey.</summary>
    public required int? DaemonSpawningDataKey { get; init; }

    /// <summary> Gets a value indicating whether Unknown216 is set.</summary>
    public required bool Unknown216 { get; init; }

    /// <inheritdoc/>
    public static RitualRuneTypesDat[] Load(Specification specification)
    {
        if (specification is null)
        {
            throw new ArgumentNullException(nameof(specification));
        }

        var fileToFind = Encoding.ASCII.GetBytes("Data/RitualRuneTypes.dat64");
        var fileRecord = specification.DataLoader.GetFileRecord(fileToFind);
        var decompressedFile = specification.DataLoader.GetFileBytes(fileRecord);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new RitualRuneTypesDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading referenced tables if any
            // specification.GetMiscAnimatedDat();
            // specification.GetBuffDefinitionsDat();
            // specification.GetRitualSpawnPatternsDat();
            // specification.GetModsDat();
            // specification.GetEnvironmentsDat();
            // specification.GetAchievementItemsDat();
            // specification.GetDaemonSpawningDataDat();

            // loading Id
            (var idLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading MiscAnimatedKey1
            (var miscanimatedkey1Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading SpawnWeight
            (var spawnweightLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading LevelMin
            (var levelminLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading LevelMax
            (var levelmaxLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading BuffDefinitionsKey
            (var buffdefinitionskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading Unknown52
            (var tempunknown52Loading, offset) = SpecificationFileLoader.LoadIntArray(decompressedFile, offset, dataOffset);
            var unknown52Loading = tempunknown52Loading.AsReadOnly();

            // loading SpawnPatterns
            (var tempspawnpatternsLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var spawnpatternsLoading = tempspawnpatternsLoading.AsReadOnly();

            // loading ModsKey
            (var tempmodskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var modskeyLoading = tempmodskeyLoading.AsReadOnly();

            // loading Unknown100
            (var tempunknown100Loading, offset) = SpecificationFileLoader.LoadIntArray(decompressedFile, offset, dataOffset);
            var unknown100Loading = tempunknown100Loading.AsReadOnly();

            // loading Unknown116
            (var tempunknown116Loading, offset) = SpecificationFileLoader.LoadIntArray(decompressedFile, offset, dataOffset);
            var unknown116Loading = tempunknown116Loading.AsReadOnly();

            // loading MiscAnimatedKey2
            (var miscanimatedkey2Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading EnvironmentsKey
            (var environmentskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading Unknown164
            (var unknown164Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Achievements
            (var tempachievementsLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var achievementsLoading = tempachievementsLoading.AsReadOnly();

            // loading Type
            (var typeLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Description
            (var descriptionLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading DaemonSpawningDataKey
            (var daemonspawningdatakeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading Unknown216
            (var unknown216Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            if (offset != expectedOffset)
            {
                throw new NotImplementedException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new RitualRuneTypesDat()
            {
                Id = idLoading,
                MiscAnimatedKey1 = miscanimatedkey1Loading,
                SpawnWeight = spawnweightLoading,
                LevelMin = levelminLoading,
                LevelMax = levelmaxLoading,
                BuffDefinitionsKey = buffdefinitionskeyLoading,
                Unknown52 = unknown52Loading,
                SpawnPatterns = spawnpatternsLoading,
                ModsKey = modskeyLoading,
                Unknown100 = unknown100Loading,
                Unknown116 = unknown116Loading,
                MiscAnimatedKey2 = miscanimatedkey2Loading,
                EnvironmentsKey = environmentskeyLoading,
                Unknown164 = unknown164Loading,
                Achievements = achievementsLoading,
                Type = typeLoading,
                Description = descriptionLoading,
                DaemonSpawningDataKey = daemonspawningdatakeyLoading,
                Unknown216 = unknown216Loading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}