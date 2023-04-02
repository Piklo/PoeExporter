// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.Dat;

/// <summary>
/// Class containing ExpeditionNPCs.dat data.
/// </summary>
public sealed partial class ExpeditionNPCsDat : ISpecificationFile<ExpeditionNPCsDat>
{
    /// <summary> Gets Id.</summary>
    public required string Id { get; init; }

    /// <summary> Gets NPCs.</summary>
    public required ReadOnlyCollection<int> NPCs { get; init; }

    /// <summary> Gets RerollItem.</summary>
    public required int? RerollItem { get; init; }

    /// <summary> Gets Unknown40.</summary>
    public required int Unknown40 { get; init; }

    /// <summary> Gets Unknown44.</summary>
    public required int Unknown44 { get; init; }

    /// <summary> Gets Unknown48.</summary>
    public required int? Unknown48 { get; init; }

    /// <summary> Gets Faction.</summary>
    public required int? Faction { get; init; }

    /// <summary> Gets Reroll.</summary>
    public required int? Reroll { get; init; }

    /// <summary> Gets AllBombsPlaced.</summary>
    public required int? AllBombsPlaced { get; init; }

    /// <summary> Gets BombPlacedRemnant.</summary>
    public required int? BombPlacedRemnant { get; init; }

    /// <summary> Gets BombPlacedTreasure.</summary>
    public required int? BombPlacedTreasure { get; init; }

    /// <summary> Gets BombPlacedMonsters.</summary>
    public required int? BombPlacedMonsters { get; init; }

    /// <summary> Gets BombPlacedGeneric.</summary>
    public required int? BombPlacedGeneric { get; init; }

    /// <summary> Gets EncounterComplete.</summary>
    public required int? EncounterComplete { get; init; }

    /// <summary> Gets Unknown192.</summary>
    public required int Unknown192 { get; init; }

    /// <summary> Gets Unknown196.</summary>
    public required int Unknown196 { get; init; }

    /// <inheritdoc/>
    public static ExpeditionNPCsDat[] Load(Specification specification)
    {
        if (specification is null)
        {
            throw new ArgumentNullException(nameof(specification));
        }

        var fileToFind = Encoding.ASCII.GetBytes("Data/ExpeditionNPCs.dat64");
        var fileRecord = specification.DataLoader.GetFileRecord(fileToFind);
        var decompressedFile = specification.DataLoader.GetFileBytes(fileRecord);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new ExpeditionNPCsDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading referenced tables if any
            // specification.GetNPCsDat();
            // specification.GetBaseItemTypesDat();
            // specification.GetQuestFlagsDat();
            // specification.GetExpeditionFactionsDat();
            // specification.GetNPCTextAudioDat();

            // loading Id
            (var idLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading NPCs
            (var tempnpcsLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var npcsLoading = tempnpcsLoading.AsReadOnly();

            // loading RerollItem
            (var rerollitemLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading Unknown40
            (var unknown40Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown44
            (var unknown44Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown48
            (var unknown48Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading Faction
            (var factionLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading Reroll
            (var rerollLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading AllBombsPlaced
            (var allbombsplacedLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading BombPlacedRemnant
            (var bombplacedremnantLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading BombPlacedTreasure
            (var bombplacedtreasureLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading BombPlacedMonsters
            (var bombplacedmonstersLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading BombPlacedGeneric
            (var bombplacedgenericLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading EncounterComplete
            (var encountercompleteLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading Unknown192
            (var unknown192Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown196
            (var unknown196Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            if (offset != expectedOffset)
            {
                throw new NotImplementedException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new ExpeditionNPCsDat()
            {
                Id = idLoading,
                NPCs = npcsLoading,
                RerollItem = rerollitemLoading,
                Unknown40 = unknown40Loading,
                Unknown44 = unknown44Loading,
                Unknown48 = unknown48Loading,
                Faction = factionLoading,
                Reroll = rerollLoading,
                AllBombsPlaced = allbombsplacedLoading,
                BombPlacedRemnant = bombplacedremnantLoading,
                BombPlacedTreasure = bombplacedtreasureLoading,
                BombPlacedMonsters = bombplacedmonstersLoading,
                BombPlacedGeneric = bombplacedgenericLoading,
                EncounterComplete = encountercompleteLoading,
                Unknown192 = unknown192Loading,
                Unknown196 = unknown196Loading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
