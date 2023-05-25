// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.DatFiles;

/// <summary>
/// Class containing DelveFeatures.dat data.
/// </summary>
public sealed partial class DelveFeaturesDat
{
    /// <summary> Gets Id.</summary>
    public required string Id { get; init; }

    /// <summary> Gets Name.</summary>
    public required string Name { get; init; }

    /// <summary> Gets SpawnWeight.</summary>
    public required ReadOnlyCollection<int> SpawnWeight { get; init; }

    /// <summary> Gets WorldAreasKey.</summary>
    /// <remarks> references <see cref="WorldAreasDat"/> on <see cref="Specification.LoadWorldAreasDat"/> index.</remarks>
    public required int? WorldAreasKey { get; init; }

    /// <summary> Gets Image.</summary>
    public required string Image { get; init; }

    /// <summary> Gets AchievementItemsKeys.</summary>
    /// <remarks> references <see cref="AchievementItemsDat"/> on <see cref="Specification.LoadAchievementItemsDat"/> index.</remarks>
    public required ReadOnlyCollection<int> AchievementItemsKeys { get; init; }

    /// <summary> Gets Unknown72.</summary>
    public required ReadOnlyCollection<int> Unknown72 { get; init; }

    /// <summary> Gets MinDepth.</summary>
    public required ReadOnlyCollection<int> MinDepth { get; init; }

    /// <summary> Gets Description.</summary>
    public required string Description { get; init; }

    /// <summary> Gets Unknown112.</summary>
    public required int Unknown112 { get; init; }

    /// <summary> Gets Unknown116.</summary>
    public required ReadOnlyCollection<int> Unknown116 { get; init; }

    /// <summary> Gets Unknown132.</summary>
    public required ReadOnlyCollection<int> Unknown132 { get; init; }

    /// <summary> Gets Unknown148.</summary>
    public required ReadOnlyCollection<int> Unknown148 { get; init; }
}
