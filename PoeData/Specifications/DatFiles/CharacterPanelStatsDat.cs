// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.DatFiles;

/// <summary>
/// Class containing CharacterPanelStats.dat data.
/// </summary>
public sealed partial class CharacterPanelStatsDat
{
    /// <summary> Gets Id.</summary>
    public required string Id { get; init; }

    /// <summary> Gets Text.</summary>
    public required string Text { get; init; }

    /// <summary> Gets StatsKeys1.</summary>
    /// <remarks> references <see cref="StatsDat"/> on <see cref="Specification.LoadStatsDat"/> index.</remarks>
    public required ReadOnlyCollection<int> StatsKeys1 { get; init; }

    /// <summary> Gets CharacterPanelDescriptionModesKey.</summary>
    /// <remarks> references <see cref="CharacterPanelDescriptionModesDat"/> on <see cref="Specification.LoadCharacterPanelDescriptionModesDat"/> index.</remarks>
    public required int? CharacterPanelDescriptionModesKey { get; init; }

    /// <summary> Gets StatsKeys2.</summary>
    /// <remarks> references <see cref="StatsDat"/> on <see cref="Specification.LoadStatsDat"/> index.</remarks>
    public required ReadOnlyCollection<int> StatsKeys2 { get; init; }

    /// <summary> Gets StatsKeys3.</summary>
    /// <remarks> references <see cref="StatsDat"/> on <see cref="Specification.LoadStatsDat"/> index.</remarks>
    public required ReadOnlyCollection<int> StatsKeys3 { get; init; }

    /// <summary> Gets CharacterPanelTabsKey.</summary>
    /// <remarks> references <see cref="CharacterPanelTabsDat"/> on <see cref="Specification.LoadCharacterPanelTabsDat"/> index.</remarks>
    public required int? CharacterPanelTabsKey { get; init; }

    /// <summary> Gets a value indicating whether Unknown96 is set.</summary>
    public required bool Unknown96 { get; init; }

    /// <summary> Gets Unknown97.</summary>
    public required ReadOnlyCollection<int> Unknown97 { get; init; }

    /// <summary> Gets Unknown113.</summary>
    public required int Unknown113 { get; init; }
}
