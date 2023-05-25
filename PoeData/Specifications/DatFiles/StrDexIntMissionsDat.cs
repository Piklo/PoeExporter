// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.DatFiles;

/// <summary>
/// Class containing StrDexIntMissions.dat data.
/// </summary>
public sealed partial class StrDexIntMissionsDat
{
    /// <summary> Gets Id.</summary>
    public required string Id { get; init; }

    /// <summary> Gets Unknown8.</summary>
    public required int Unknown8 { get; init; }

    /// <summary> Gets Unknown12.</summary>
    public required int Unknown12 { get; init; }

    /// <summary> Gets SpawnWeight.</summary>
    public required int SpawnWeight { get; init; }

    /// <summary> Gets Unknown20.</summary>
    public required int? Unknown20 { get; init; }

    /// <summary> Gets Unknown36.</summary>
    public required int? Unknown36 { get; init; }

    /// <summary> Gets Extra_ModsKeys.</summary>
    /// <remarks> references <see cref="ModsDat"/> on <see cref="Specification.LoadModsDat"/> index.</remarks>
    public required ReadOnlyCollection<int> Extra_ModsKeys { get; init; }

    /// <summary> Gets a value indicating whether Unknown68 is set.</summary>
    public required bool Unknown68 { get; init; }

    /// <summary> Gets a value indicating whether Unknown69 is set.</summary>
    public required bool Unknown69 { get; init; }

    /// <summary> Gets a value indicating whether Unknown70 is set.</summary>
    public required bool Unknown70 { get; init; }

    /// <summary> Gets Unknown71.</summary>
    public required int? Unknown71 { get; init; }

    /// <summary> Gets Unknown87.</summary>
    public required int Unknown87 { get; init; }

    /// <summary> Gets Unknown91.</summary>
    public required int Unknown91 { get; init; }

    /// <summary> Gets Unknown95.</summary>
    public required int Unknown95 { get; init; }

    /// <summary> Gets Unknown99.</summary>
    public required int Unknown99 { get; init; }

    /// <summary> Gets Unknown103.</summary>
    public required int? Unknown103 { get; init; }

    /// <summary> Gets Unknown119.</summary>
    public required int? Unknown119 { get; init; }

    /// <summary> Gets Unknown135.</summary>
    public required int? Unknown135 { get; init; }

    /// <summary> Gets a value indicating whether Unknown151 is set.</summary>
    public required bool Unknown151 { get; init; }

    /// <summary> Gets Unknown152.</summary>
    public required int? Unknown152 { get; init; }

    /// <summary> Gets a value indicating whether Unknown168 is set.</summary>
    public required bool Unknown168 { get; init; }

    /// <summary> Gets Unknown169.</summary>
    public required int? Unknown169 { get; init; }
}
