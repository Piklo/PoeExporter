// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.DatFiles;

/// <summary>
/// Class containing RaceTimes.dat data.
/// </summary>
public sealed partial class RaceTimesDat
{
    /// <summary> Gets RacesKey.</summary>
    /// <remarks> references <see cref="RacesDat"/> on <see cref="Specification.LoadRacesDat"/> index.</remarks>
    public required int? RacesKey { get; init; }

    /// <summary> Gets Index.</summary>
    public required int Index { get; init; }

    /// <summary> Gets StartUNIXTime.</summary>
    public required int StartUNIXTime { get; init; }

    /// <summary> Gets EndUNIXTime.</summary>
    public required int EndUNIXTime { get; init; }
}
