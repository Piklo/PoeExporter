// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.Dat;

/// <summary>
/// Class containing HeistQuestContracts.dat data.
/// </summary>
public sealed partial class HeistQuestContractsDat : IDat<HeistQuestContractsDat>
{
    /// <summary> Gets HeistContractsKey.</summary>
    /// <remarks> references <see cref="HeistContractsDat"/> on <see cref="Specification.GetHeistContractsDat"/> index.</remarks>
    public required int? HeistContractsKey { get; init; }

    /// <summary> Gets HeistObjectivesKey.</summary>
    /// <remarks> references <see cref="HeistObjectivesDat"/> on <see cref="Specification.GetHeistObjectivesDat"/> index.</remarks>
    public required int? HeistObjectivesKey { get; init; }

    /// <summary> Gets HeistNPCsKey.</summary>
    /// <remarks> references <see cref="HeistNPCsDat"/> on <see cref="Specification.GetHeistNPCsDat"/> index.</remarks>
    public required ReadOnlyCollection<int> HeistNPCsKey { get; init; }

    /// <summary> Gets HeistJobsKey.</summary>
    /// <remarks> references <see cref="HeistJobsDat"/> on <see cref="Specification.GetHeistJobsDat"/> index.</remarks>
    public required int? HeistJobsKey { get; init; }

    /// <summary> Gets Unknown64.</summary>
    public required int Unknown64 { get; init; }

    /// <summary> Gets Unknown68.</summary>
    public required int Unknown68 { get; init; }

    /// <summary> Gets Unknown72.</summary>
    public required int Unknown72 { get; init; }

    /// <summary> Gets a value indicating whether Unknown76 is set.</summary>
    public required bool Unknown76 { get; init; }

    /// <summary> Gets HeistRoomsKey1.</summary>
    /// <remarks> references <see cref="HeistRoomsDat"/> on <see cref="Specification.GetHeistRoomsDat"/> index.</remarks>
    public required int? HeistRoomsKey1 { get; init; }

    /// <summary> Gets WorldAreasKey.</summary>
    /// <remarks> references <see cref="WorldAreasDat"/> on <see cref="Specification.GetWorldAreasDat"/> index.</remarks>
    public required int? WorldAreasKey { get; init; }

    /// <summary> Gets a value indicating whether Unknown109 is set.</summary>
    public required bool Unknown109 { get; init; }

    /// <summary> Gets a value indicating whether Unknown110 is set.</summary>
    public required bool Unknown110 { get; init; }

    /// <summary> Gets Unknown111.</summary>
    public required int Unknown111 { get; init; }

    /// <summary> Gets Unknown115.</summary>
    public required int Unknown115 { get; init; }

    /// <summary> Gets a value indicating whether Unknown119 is set.</summary>
    public required bool Unknown119 { get; init; }

    /// <summary> Gets a value indicating whether Unknown120 is set.</summary>
    public required bool Unknown120 { get; init; }

    /// <summary> Gets HaveObjective.</summary>
    /// <remarks> references <see cref="QuestFlagsDat"/> on <see cref="Specification.GetQuestFlagsDat"/> index.</remarks>
    public required int? HaveObjective { get; init; }

    /// <summary> Gets a value indicating whether Unknown137 is set.</summary>
    public required bool Unknown137 { get; init; }

    /// <summary> Gets QuestActive.</summary>
    /// <remarks> references <see cref="QuestFlagsDat"/> on <see cref="Specification.GetQuestFlagsDat"/> index.</remarks>
    public required int? QuestActive { get; init; }

    /// <summary> Gets HaveQuest.</summary>
    /// <remarks> references <see cref="QuestFlagsDat"/> on <see cref="Specification.GetQuestFlagsDat"/> index.</remarks>
    public required int? HaveQuest { get; init; }

    /// <summary> Gets HaveObjective2.</summary>
    /// <remarks> references <see cref="QuestFlagsDat"/> on <see cref="Specification.GetQuestFlagsDat"/> index.</remarks>
    public required int? HaveObjective2 { get; init; }

    /// <summary> Gets a value indicating whether Unknown186 is set.</summary>
    public required bool Unknown186 { get; init; }

    /// <summary> Gets a value indicating whether Unknown187 is set.</summary>
    public required bool Unknown187 { get; init; }

    /// <summary> Gets Objective.</summary>
    public required string Objective { get; init; }

    /// <summary> Gets a value indicating whether Unknown196 is set.</summary>
    public required bool Unknown196 { get; init; }

    /// <summary> Gets BaseItemTypesKey.</summary>
    /// <remarks> references <see cref="BaseItemTypesDat"/> on <see cref="Specification.GetBaseItemTypesDat"/> index.</remarks>
    public required int? BaseItemTypesKey { get; init; }

    /// <summary> Gets a value indicating whether Unknown213 is set.</summary>
    public required bool Unknown213 { get; init; }

    /// <summary> Gets HeistIntroAreasKey.</summary>
    /// <remarks> references <see cref="HeistIntroAreasDat"/> on <see cref="Specification.GetHeistIntroAreasDat"/> index.</remarks>
    public required int? HeistIntroAreasKey { get; init; }

    /// <summary> Gets Unknown230.</summary>
    public required int Unknown230 { get; init; }

    /// <summary> Gets HeistRoomsKey2.</summary>
    /// <remarks> references <see cref="HeistRoomsDat"/> on <see cref="Specification.GetHeistRoomsDat"/> index.</remarks>
    public required int? HeistRoomsKey2 { get; init; }

    /// <summary> Gets Unknown250.</summary>
    public required string Unknown250 { get; init; }

    /// <inheritdoc/>
    public static HeistQuestContractsDat[] Load(Specification specification)
    {
        if (specification is null)
        {
            throw new ArgumentNullException(nameof(specification));
        }

        var fileToFind = Encoding.ASCII.GetBytes("Data/HeistQuestContracts.dat64");
        var fileRecord = specification.DataLoader.GetFileRecord(fileToFind);
        var decompressedFile = specification.DataLoader.GetFileBytes(fileRecord);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new HeistQuestContractsDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading HeistContractsKey
            (var heistcontractskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading HeistObjectivesKey
            (var heistobjectiveskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading HeistNPCsKey
            (var tempheistnpcskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var heistnpcskeyLoading = tempheistnpcskeyLoading.AsReadOnly();

            // loading HeistJobsKey
            (var heistjobskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading Unknown64
            (var unknown64Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown68
            (var unknown68Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown72
            (var unknown72Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown76
            (var unknown76Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading HeistRoomsKey1
            (var heistroomskey1Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading WorldAreasKey
            (var worldareaskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading Unknown109
            (var unknown109Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown110
            (var unknown110Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown111
            (var unknown111Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown115
            (var unknown115Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown119
            (var unknown119Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown120
            (var unknown120Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading HaveObjective
            (var haveobjectiveLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading Unknown137
            (var unknown137Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading QuestActive
            (var questactiveLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading HaveQuest
            (var havequestLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading HaveObjective2
            (var haveobjective2Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading Unknown186
            (var unknown186Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown187
            (var unknown187Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Objective
            (var objectiveLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Unknown196
            (var unknown196Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading BaseItemTypesKey
            (var baseitemtypeskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading Unknown213
            (var unknown213Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading HeistIntroAreasKey
            (var heistintroareaskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading Unknown230
            (var unknown230Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading HeistRoomsKey2
            (var heistroomskey2Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading Unknown250
            (var unknown250Loading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            if (offset != expectedOffset)
            {
                throw new NotImplementedException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new HeistQuestContractsDat()
            {
                HeistContractsKey = heistcontractskeyLoading,
                HeistObjectivesKey = heistobjectiveskeyLoading,
                HeistNPCsKey = heistnpcskeyLoading,
                HeistJobsKey = heistjobskeyLoading,
                Unknown64 = unknown64Loading,
                Unknown68 = unknown68Loading,
                Unknown72 = unknown72Loading,
                Unknown76 = unknown76Loading,
                HeistRoomsKey1 = heistroomskey1Loading,
                WorldAreasKey = worldareaskeyLoading,
                Unknown109 = unknown109Loading,
                Unknown110 = unknown110Loading,
                Unknown111 = unknown111Loading,
                Unknown115 = unknown115Loading,
                Unknown119 = unknown119Loading,
                Unknown120 = unknown120Loading,
                HaveObjective = haveobjectiveLoading,
                Unknown137 = unknown137Loading,
                QuestActive = questactiveLoading,
                HaveQuest = havequestLoading,
                HaveObjective2 = haveobjective2Loading,
                Unknown186 = unknown186Loading,
                Unknown187 = unknown187Loading,
                Objective = objectiveLoading,
                Unknown196 = unknown196Loading,
                BaseItemTypesKey = baseitemtypeskeyLoading,
                Unknown213 = unknown213Loading,
                HeistIntroAreasKey = heistintroareaskeyLoading,
                Unknown230 = unknown230Loading,
                HeistRoomsKey2 = heistroomskey2Loading,
                Unknown250 = unknown250Loading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
