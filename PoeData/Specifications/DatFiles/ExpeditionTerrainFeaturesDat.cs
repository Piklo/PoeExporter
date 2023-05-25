// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.DatFiles;

/// <summary>
/// Class containing ExpeditionTerrainFeatures.dat data.
/// </summary>
public sealed partial class ExpeditionTerrainFeaturesDat
{
    /// <summary> Gets Id.</summary>
    public required string Id { get; init; }

    /// <summary> Gets ExtraFeature.</summary>
    /// <remarks> references <see cref="ExtraTerrainFeaturesDat"/> on <see cref="Specification.LoadExtraTerrainFeaturesDat"/> index.</remarks>
    public required int? ExtraFeature { get; init; }

    /// <summary> Gets ExpeditionFaction.</summary>
    /// <remarks> references <see cref="ExpeditionFactionsDat"/> on <see cref="Specification.LoadExpeditionFactionsDat"/> index.</remarks>
    public required int? ExpeditionFaction { get; init; }

    /// <summary> Gets MinLevel.</summary>
    public required int MinLevel { get; init; }

    /// <summary> Gets MaxLevel.</summary>
    public required int MaxLevel { get; init; }

    /// <summary> Gets Unknown48.</summary>
    public required int Unknown48 { get; init; }

    /// <summary> Gets Area.</summary>
    /// <remarks> references <see cref="WorldAreasDat"/> on <see cref="Specification.LoadWorldAreasDat"/> index.</remarks>
    public required int? Area { get; init; }

    /// <summary> Gets ExpeditionAreas.</summary>
    /// <remarks> references <see cref="ExpeditionAreasDat"/> on <see cref="Specification.LoadExpeditionAreasDat"/> index.</remarks>
    public required ReadOnlyCollection<int> ExpeditionAreas { get; init; }

    /// <summary> Gets Unknown84.</summary>
    public required int Unknown84 { get; init; }

    /// <summary> Gets a value indicating whether Unknown88 is set.</summary>
    public required bool Unknown88 { get; init; }

    /// <summary> Gets UnearthAchievements.</summary>
    /// <remarks> references <see cref="AchievementItemsDat"/> on <see cref="Specification.LoadAchievementItemsDat"/> index.</remarks>
    public required ReadOnlyCollection<int> UnearthAchievements { get; init; }
}
