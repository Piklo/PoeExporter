// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.DatFiles;

/// <summary>
/// Class containing Acts.dat data.
/// </summary>
public sealed partial class ActsDat
{
    /// <summary> Gets Id.</summary>
    public required string Id { get; init; }

    /// <summary> Gets Part.</summary>
    public required int Part { get; init; }

    /// <summary> Gets Unknown12.</summary>
    public required string Unknown12 { get; init; }

    /// <summary> Gets ActNumber.</summary>
    public required int ActNumber { get; init; }

    /// <summary> Gets Unknown24.</summary>
    public required int Unknown24 { get; init; }

    /// <summary> Gets WorldPanelImage.</summary>
    public required string WorldPanelImage { get; init; }

    /// <summary> Gets WorldPanelImageEpilogue.</summary>
    public required string WorldPanelImageEpilogue { get; init; }

    /// <summary> Gets Unknown44.</summary>
    public required int Unknown44 { get; init; }

    /// <summary> Gets a value indicating whether IsPostGame is set.</summary>
    public required bool IsPostGame { get; init; }

    /// <summary> Gets Unknown49.</summary>
    public required int Unknown49 { get; init; }

    /// <summary> Gets Unknown53.</summary>
    /// <remarks> references <see cref="QuestFlagsDat"/> on <see cref="Specification.LoadQuestFlagsDat"/> index.</remarks>
    public required ReadOnlyCollection<int> Unknown53 { get; init; }
}
