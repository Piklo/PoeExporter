// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.DatFiles;

/// <summary>
/// Class containing LeagueInfo.dat data.
/// </summary>
public sealed partial class LeagueInfoDat
{
    /// <summary> Gets Id.</summary>
    public required string Id { get; init; }

    /// <summary> Gets PanelImage.</summary>
    public required string PanelImage { get; init; }

    /// <summary> Gets HeaderImage.</summary>
    public required string HeaderImage { get; init; }

    /// <summary> Gets Screenshots.</summary>
    public required ReadOnlyCollection<string> Screenshots { get; init; }

    /// <summary> Gets Description.</summary>
    public required string Description { get; init; }

    /// <summary> Gets League.</summary>
    public required string League { get; init; }

    /// <summary> Gets a value indicating whether Unknown56 is set.</summary>
    public required bool Unknown56 { get; init; }

    /// <summary> Gets TrailerVideoLink.</summary>
    public required string TrailerVideoLink { get; init; }

    /// <summary> Gets BackgroundImage.</summary>
    public required string BackgroundImage { get; init; }

    /// <summary> Gets a value indicating whether Unknown73 is set.</summary>
    public required bool Unknown73 { get; init; }

    /// <summary> Gets a value indicating whether Unknown74 is set.</summary>
    public required bool Unknown74 { get; init; }

    /// <summary> Gets PanelItems.</summary>
    public required ReadOnlyCollection<string> PanelItems { get; init; }
}
