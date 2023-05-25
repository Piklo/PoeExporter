// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.DatFiles;

/// <summary>
/// Class containing HellscapeModificationInventoryLayout.dat data.
/// </summary>
public sealed partial class HellscapeModificationInventoryLayoutDat
{
    /// <summary> Gets Id.</summary>
    public required string Id { get; init; }

    /// <summary> Gets Column.</summary>
    public required int Column { get; init; }

    /// <summary> Gets Row.</summary>
    public required int Row { get; init; }

    /// <summary> Gets a value indicating whether IsMapSlot is set.</summary>
    public required bool IsMapSlot { get; init; }

    /// <summary> Gets Unknown17.</summary>
    public required int Unknown17 { get; init; }

    /// <summary> Gets Width.</summary>
    public required int Width { get; init; }

    /// <summary> Gets Height.</summary>
    public required int Height { get; init; }

    /// <summary> Gets Stat.</summary>
    /// <remarks> references <see cref="StatsDat"/> on <see cref="Specification.LoadStatsDat"/> index.</remarks>
    public required int? Stat { get; init; }

    /// <summary> Gets StatValue.</summary>
    public required int StatValue { get; init; }

    /// <summary> Gets UnlockedWith.</summary>
    /// <remarks> references <see cref="HellscapePassivesDat"/> on <see cref="Specification.LoadHellscapePassivesDat"/> index.</remarks>
    public required int? UnlockedWith { get; init; }

    /// <summary> Gets Quest.</summary>
    /// <remarks> references <see cref="QuestDat"/> on <see cref="Specification.LoadQuestDat"/> index.</remarks>
    public required int? Quest { get; init; }
}
