// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.DatFiles;

/// <summary>
/// Class containing RitualRuneTypes.dat data.
/// </summary>
public sealed partial class RitualRuneTypesDat
{
    /// <summary> Gets Id.</summary>
    public required string Id { get; init; }

    /// <summary> Gets MiscAnimatedKey1.</summary>
    /// <remarks> references <see cref="MiscAnimatedDat"/> on <see cref="Specification.LoadMiscAnimatedDat"/> index.</remarks>
    public required int? MiscAnimatedKey1 { get; init; }

    /// <summary> Gets SpawnWeight.</summary>
    public required int SpawnWeight { get; init; }

    /// <summary> Gets LevelMin.</summary>
    public required int LevelMin { get; init; }

    /// <summary> Gets LevelMax.</summary>
    public required int LevelMax { get; init; }

    /// <summary> Gets BuffDefinitionsKey.</summary>
    /// <remarks> references <see cref="BuffDefinitionsDat"/> on <see cref="Specification.LoadBuffDefinitionsDat"/> index.</remarks>
    public required int? BuffDefinitionsKey { get; init; }

    /// <summary> Gets Unknown52.</summary>
    public required ReadOnlyCollection<int> Unknown52 { get; init; }

    /// <summary> Gets SpawnPatterns.</summary>
    /// <remarks> references <see cref="RitualSpawnPatternsDat"/> on <see cref="Specification.LoadRitualSpawnPatternsDat"/> index.</remarks>
    public required ReadOnlyCollection<int> SpawnPatterns { get; init; }

    /// <summary> Gets ModsKey.</summary>
    /// <remarks> references <see cref="ModsDat"/> on <see cref="Specification.LoadModsDat"/> index.</remarks>
    public required ReadOnlyCollection<int> ModsKey { get; init; }

    /// <summary> Gets Unknown100.</summary>
    public required ReadOnlyCollection<int> Unknown100 { get; init; }

    /// <summary> Gets Unknown116.</summary>
    public required ReadOnlyCollection<int> Unknown116 { get; init; }

    /// <summary> Gets MiscAnimatedKey2.</summary>
    /// <remarks> references <see cref="MiscAnimatedDat"/> on <see cref="Specification.LoadMiscAnimatedDat"/> index.</remarks>
    public required int? MiscAnimatedKey2 { get; init; }

    /// <summary> Gets EnvironmentsKey.</summary>
    /// <remarks> references <see cref="EnvironmentsDat"/> on <see cref="Specification.LoadEnvironmentsDat"/> index.</remarks>
    public required int? EnvironmentsKey { get; init; }

    /// <summary> Gets Unknown164.</summary>
    public required int Unknown164 { get; init; }

    /// <summary> Gets Achievements.</summary>
    /// <remarks> references <see cref="AchievementItemsDat"/> on <see cref="Specification.LoadAchievementItemsDat"/> index.</remarks>
    public required ReadOnlyCollection<int> Achievements { get; init; }

    /// <summary> Gets Type.</summary>
    public required string Type { get; init; }

    /// <summary> Gets Description.</summary>
    public required string Description { get; init; }

    /// <summary> Gets DaemonSpawningDataKey.</summary>
    /// <remarks> references <see cref="DaemonSpawningDataDat"/> on <see cref="Specification.LoadDaemonSpawningDataDat"/> index.</remarks>
    public required int? DaemonSpawningDataKey { get; init; }

    /// <summary> Gets a value indicating whether Unknown216 is set.</summary>
    public required bool Unknown216 { get; init; }

    /// <summary>
    /// Gets RitualRuneTypesDat data.
    /// </summary>
    /// <param name="dataLoader">data loader.</param>
    /// <returns>array of RitualRuneTypesDat.</returns>
    internal static RitualRuneTypesDat[] Load(DataLoader dataLoader)
    {
        if (dataLoader is null)
        {
            throw new ArgumentNullException(nameof(dataLoader));
        }

        const string filePath = "Data/RitualRuneTypes.dat64";
        var decompressedFile = dataLoader.GetFileBytes(filePath);

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

            // loading Id
            (var idLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading MiscAnimatedKey1
            (var miscanimatedkey1Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading SpawnWeight
            (var spawnweightLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading LevelMin
            (var levelminLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading LevelMax
            (var levelmaxLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading BuffDefinitionsKey
            (var buffdefinitionskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

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
            (var miscanimatedkey2Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading EnvironmentsKey
            (var environmentskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

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
            (var daemonspawningdatakeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading Unknown216
            (var unknown216Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            if (offset != expectedOffset)
            {
                throw new SchemaMismatchException($"offset {offset} != expectedOffset {expectedOffset}");
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
