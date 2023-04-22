// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.DatFiles;

/// <summary>
/// Class containing StrDexIntMissionExtraRequirement.dat data.
/// </summary>
public sealed partial class StrDexIntMissionExtraRequirementDat
{
    /// <summary> Gets Id.</summary>
    public required string Id { get; init; }

    /// <summary> Gets SpawnWeight.</summary>
    public required int SpawnWeight { get; init; }

    /// <summary> Gets MinLevel.</summary>
    public required int MinLevel { get; init; }

    /// <summary> Gets MaxLevel.</summary>
    public required int MaxLevel { get; init; }

    /// <summary> Gets TimeLimit.</summary>
    public required int TimeLimit { get; init; }

    /// <summary> Gets a value indicating whether HasTimeBonusPerKill is set.</summary>
    public required bool HasTimeBonusPerKill { get; init; }

    /// <summary> Gets a value indicating whether HasTimeBonusPerObjectTagged is set.</summary>
    public required bool HasTimeBonusPerObjectTagged { get; init; }

    /// <summary> Gets a value indicating whether HasLimitedPortals is set.</summary>
    public required bool HasLimitedPortals { get; init; }

    /// <summary> Gets NPCTalkKey.</summary>
    /// <remarks> references <see cref="NPCTalkDat"/> on <see cref="Specification.LoadNPCTalkDat"/> index.</remarks>
    public required int? NPCTalkKey { get; init; }

    /// <summary> Gets TimeLimitBonusFromObjective.</summary>
    public required int TimeLimitBonusFromObjective { get; init; }

    /// <summary> Gets ObjectCount.</summary>
    public required int ObjectCount { get; init; }

    /// <summary> Gets Unknown51.</summary>
    public required ReadOnlyCollection<int> Unknown51 { get; init; }

    /// <summary> Gets a value indicating whether Unknown67 is set.</summary>
    public required bool Unknown67 { get; init; }

    /// <summary> Gets Unknown68.</summary>
    public required ReadOnlyCollection<int> Unknown68 { get; init; }

    /// <summary>
    /// Gets StrDexIntMissionExtraRequirementDat data.
    /// </summary>
    /// <param name="dataLoader">data loader.</param>
    /// <returns>array of StrDexIntMissionExtraRequirementDat.</returns>
    internal static StrDexIntMissionExtraRequirementDat[] Load(DataLoader dataLoader)
    {
        if (dataLoader is null)
        {
            throw new ArgumentNullException(nameof(dataLoader));
        }

        const string filePath = "Data/StrDexIntMissionExtraRequirement.dat64";
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new StrDexIntMissionExtraRequirementDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading Id
            (var idLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading SpawnWeight
            (var spawnweightLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading MinLevel
            (var minlevelLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading MaxLevel
            (var maxlevelLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading TimeLimit
            (var timelimitLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading HasTimeBonusPerKill
            (var hastimebonusperkillLoading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading HasTimeBonusPerObjectTagged
            (var hastimebonusperobjecttaggedLoading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading HasLimitedPortals
            (var haslimitedportalsLoading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading NPCTalkKey
            (var npctalkkeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading TimeLimitBonusFromObjective
            (var timelimitbonusfromobjectiveLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading ObjectCount
            (var objectcountLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown51
            (var tempunknown51Loading, offset) = SpecificationFileLoader.LoadIntArray(decompressedFile, offset, dataOffset);
            var unknown51Loading = tempunknown51Loading.AsReadOnly();

            // loading Unknown67
            (var unknown67Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown68
            (var tempunknown68Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var unknown68Loading = tempunknown68Loading.AsReadOnly();

            if (offset != expectedOffset)
            {
                throw new SchemaMismatchException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new StrDexIntMissionExtraRequirementDat()
            {
                Id = idLoading,
                SpawnWeight = spawnweightLoading,
                MinLevel = minlevelLoading,
                MaxLevel = maxlevelLoading,
                TimeLimit = timelimitLoading,
                HasTimeBonusPerKill = hastimebonusperkillLoading,
                HasTimeBonusPerObjectTagged = hastimebonusperobjecttaggedLoading,
                HasLimitedPortals = haslimitedportalsLoading,
                NPCTalkKey = npctalkkeyLoading,
                TimeLimitBonusFromObjective = timelimitbonusfromobjectiveLoading,
                ObjectCount = objectcountLoading,
                Unknown51 = unknown51Loading,
                Unknown67 = unknown67Loading,
                Unknown68 = unknown68Loading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
