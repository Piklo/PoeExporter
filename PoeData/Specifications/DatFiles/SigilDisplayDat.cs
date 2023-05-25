// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.DatFiles;

/// <summary>
/// Class containing SigilDisplay.dat data.
/// </summary>
public sealed partial class SigilDisplayDat
{
    /// <summary> Gets Id.</summary>
    public required string Id { get; init; }

    /// <summary> Gets Active_StatsKey.</summary>
    /// <remarks> references <see cref="StatsDat"/> on <see cref="Specification.LoadStatsDat"/> index.</remarks>
    public required int? Active_StatsKey { get; init; }

    /// <summary> Gets Inactive_StatsKey.</summary>
    /// <remarks> references <see cref="StatsDat"/> on <see cref="Specification.LoadStatsDat"/> index.</remarks>
    public required int? Inactive_StatsKey { get; init; }

    /// <summary> Gets DDSFile.</summary>
    public required string DDSFile { get; init; }

    /// <summary> Gets Inactive_ArtFile.</summary>
    public required string Inactive_ArtFile { get; init; }

    /// <summary> Gets Active_ArtFile.</summary>
    public required string Active_ArtFile { get; init; }

    /// <summary> Gets Frame_ArtFile.</summary>
    public required string Frame_ArtFile { get; init; }
}
