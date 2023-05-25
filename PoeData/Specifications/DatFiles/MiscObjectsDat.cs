// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.DatFiles;

/// <summary>
/// Class containing MiscObjects.dat data.
/// </summary>
public sealed partial class MiscObjectsDat
{
    /// <summary> Gets Id.</summary>
    public required string Id { get; init; }

    /// <summary> Gets EffectVirtualPath.</summary>
    public required string EffectVirtualPath { get; init; }

    /// <summary> Gets PreloadGroupsKeys.</summary>
    /// <remarks> references <see cref="PreloadGroupsDat"/> on <see cref="Specification.LoadPreloadGroupsDat"/> index.</remarks>
    public required ReadOnlyCollection<int> PreloadGroupsKeys { get; init; }

    /// <summary> Gets Unknown32.</summary>
    public required int Unknown32 { get; init; }

    /// <summary> Gets Unknown36.</summary>
    public required int Unknown36 { get; init; }
}
