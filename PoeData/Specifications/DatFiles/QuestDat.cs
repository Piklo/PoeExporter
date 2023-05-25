// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.DatFiles;

/// <summary>
/// Class containing Quest.dat data.
/// </summary>
public sealed partial class QuestDat
{
    /// <summary> Gets Id.</summary>
    public required string Id { get; init; }

    /// <summary> Gets Act.</summary>
    public required int Act { get; init; }

    /// <summary> Gets Name.</summary>
    public required string Name { get; init; }

    /// <summary> Gets Icon_DDSFile.</summary>
    public required string Icon_DDSFile { get; init; }

    /// <summary> Gets QuestId.</summary>
    public required int QuestId { get; init; }

    /// <summary> Gets a value indicating whether Unknown32 is set.</summary>
    public required bool Unknown32 { get; init; }

    /// <summary> Gets Type.</summary>
    /// <remarks> references <see cref="QuestTypeDat"/> on <see cref="Specification.LoadQuestTypeDat"/> index.</remarks>
    public required int? Type { get; init; }

    /// <summary> Gets Unknown49.</summary>
    public required ReadOnlyCollection<int> Unknown49 { get; init; }

    /// <summary> Gets Unknown65.</summary>
    public required int Unknown65 { get; init; }

    /// <summary> Gets TrackerGroup.</summary>
    /// <remarks> references <see cref="QuestTrackerGroupDat"/> on <see cref="Specification.LoadQuestTrackerGroupDat"/> index.</remarks>
    public required int? TrackerGroup { get; init; }
}
