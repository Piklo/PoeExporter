// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.DatFiles;

/// <summary>
/// Class containing ExpeditionNPCs.dat data.
/// </summary>
public sealed partial class ExpeditionNPCsDat
{
    /// <summary> Gets Id.</summary>
    public required string Id { get; init; }

    /// <summary> Gets NPCs.</summary>
    /// <remarks> references <see cref="NPCsDat"/> on <see cref="Specification.LoadNPCsDat"/> index.</remarks>
    public required ReadOnlyCollection<int> NPCs { get; init; }

    /// <summary> Gets RerollItem.</summary>
    /// <remarks> references <see cref="BaseItemTypesDat"/> on <see cref="Specification.LoadBaseItemTypesDat"/> index.</remarks>
    public required int? RerollItem { get; init; }

    /// <summary> Gets Unknown40.</summary>
    public required int Unknown40 { get; init; }

    /// <summary> Gets Unknown44.</summary>
    public required int Unknown44 { get; init; }

    /// <summary> Gets Unknown48.</summary>
    /// <remarks> references <see cref="QuestFlagsDat"/> on <see cref="Specification.LoadQuestFlagsDat"/> index.</remarks>
    public required int? Unknown48 { get; init; }

    /// <summary> Gets Faction.</summary>
    /// <remarks> references <see cref="ExpeditionFactionsDat"/> on <see cref="Specification.LoadExpeditionFactionsDat"/> index.</remarks>
    public required int? Faction { get; init; }

    /// <summary> Gets Reroll.</summary>
    /// <remarks> references <see cref="NPCTextAudioDat"/> on <see cref="Specification.LoadNPCTextAudioDat"/> index.</remarks>
    public required int? Reroll { get; init; }

    /// <summary> Gets AllBombsPlaced.</summary>
    /// <remarks> references <see cref="NPCTextAudioDat"/> on <see cref="Specification.LoadNPCTextAudioDat"/> index.</remarks>
    public required int? AllBombsPlaced { get; init; }

    /// <summary> Gets BombPlacedRemnant.</summary>
    /// <remarks> references <see cref="NPCTextAudioDat"/> on <see cref="Specification.LoadNPCTextAudioDat"/> index.</remarks>
    public required int? BombPlacedRemnant { get; init; }

    /// <summary> Gets BombPlacedTreasure.</summary>
    /// <remarks> references <see cref="NPCTextAudioDat"/> on <see cref="Specification.LoadNPCTextAudioDat"/> index.</remarks>
    public required int? BombPlacedTreasure { get; init; }

    /// <summary> Gets BombPlacedMonsters.</summary>
    /// <remarks> references <see cref="NPCTextAudioDat"/> on <see cref="Specification.LoadNPCTextAudioDat"/> index.</remarks>
    public required int? BombPlacedMonsters { get; init; }

    /// <summary> Gets BombPlacedGeneric.</summary>
    /// <remarks> references <see cref="NPCTextAudioDat"/> on <see cref="Specification.LoadNPCTextAudioDat"/> index.</remarks>
    public required int? BombPlacedGeneric { get; init; }

    /// <summary> Gets EncounterComplete.</summary>
    /// <remarks> references <see cref="NPCTextAudioDat"/> on <see cref="Specification.LoadNPCTextAudioDat"/> index.</remarks>
    public required int? EncounterComplete { get; init; }

    /// <summary> Gets Unknown192.</summary>
    public required int Unknown192 { get; init; }

    /// <summary> Gets Unknown196.</summary>
    public required int Unknown196 { get; init; }
}
