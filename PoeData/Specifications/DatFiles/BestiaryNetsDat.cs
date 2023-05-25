// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.DatFiles;

/// <summary>
/// Class containing BestiaryNets.dat data.
/// </summary>
public sealed partial class BestiaryNetsDat
{
    /// <summary> Gets BaseItemTypesKey.</summary>
    /// <remarks> references <see cref="BaseItemTypesDat"/> on <see cref="Specification.LoadBaseItemTypesDat"/> index.</remarks>
    public required int? BaseItemTypesKey { get; init; }

    /// <summary> Gets Unknown16.</summary>
    public required int Unknown16 { get; init; }

    /// <summary> Gets CaptureMinLevel.</summary>
    public required int CaptureMinLevel { get; init; }

    /// <summary> Gets CaptureMaxLevel.</summary>
    public required int CaptureMaxLevel { get; init; }

    /// <summary> Gets DropMinLevel.</summary>
    public required int DropMinLevel { get; init; }

    /// <summary> Gets DropMaxLevel.</summary>
    public required int DropMaxLevel { get; init; }

    /// <summary> Gets Unknown36.</summary>
    public required int Unknown36 { get; init; }

    /// <summary> Gets a value indicating whether IsEnabled is set.</summary>
    public required bool IsEnabled { get; init; }
}
