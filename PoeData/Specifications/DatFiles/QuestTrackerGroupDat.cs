// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.DatFiles;

/// <summary>
/// Class containing QuestTrackerGroup.dat data.
/// </summary>
public sealed partial class QuestTrackerGroupDat
{
    /// <summary> Gets Id.</summary>
    public required string Id { get; init; }

    /// <summary> Gets Name.</summary>
    public required string Name { get; init; }

    /// <summary> Gets QuestType.</summary>
    /// <remarks> references <see cref="QuestTypeDat"/> on <see cref="Specification.LoadQuestTypeDat"/> index.</remarks>
    public required int? QuestType { get; init; }
}
