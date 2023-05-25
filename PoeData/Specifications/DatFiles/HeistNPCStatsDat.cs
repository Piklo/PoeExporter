// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.DatFiles;

/// <summary>
/// Class containing HeistNPCStats.dat data.
/// </summary>
public sealed partial class HeistNPCStatsDat
{
    /// <summary> Gets StatsKey.</summary>
    /// <remarks> references <see cref="StatsDat"/> on <see cref="Specification.LoadStatsDat"/> index.</remarks>
    public required int? StatsKey { get; init; }

    /// <summary> Gets a value indicating whether Unknown16 is set.</summary>
    public required bool Unknown16 { get; init; }

    /// <summary> Gets a value indicating whether Unknown17 is set.</summary>
    public required bool Unknown17 { get; init; }

    /// <summary> Gets a value indicating whether Unknown18 is set.</summary>
    public required bool Unknown18 { get; init; }

    /// <summary> Gets a value indicating whether Unknown19 is set.</summary>
    public required bool Unknown19 { get; init; }
}
