// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.DatFiles;

/// <summary>
/// Class containing AchievementItems.dat data.
/// </summary>
public sealed partial class AchievementItemsDat
{
    /// <summary> Gets Id.</summary>
    public required string Id { get; init; }

    /// <summary> Gets Unknown8.</summary>
    public required int Unknown8 { get; init; }

    /// <summary> Gets Unknown12.</summary>
    public required int Unknown12 { get; init; }

    /// <summary> Gets Name.</summary>
    public required string Name { get; init; }

    /// <summary> Gets CompletionsRequired.</summary>
    public required int CompletionsRequired { get; init; }

    /// <summary> Gets AchievementsKey.</summary>
    /// <remarks> references <see cref="AchievementsDat"/> on <see cref="Specification.LoadAchievementsDat"/> index.</remarks>
    public required int? AchievementsKey { get; init; }

    /// <summary> Gets a value indicating whether Unknown44 is set.</summary>
    public required bool Unknown44 { get; init; }

    /// <summary> Gets a value indicating whether Unknown45 is set.</summary>
    public required bool Unknown45 { get; init; }

    /// <summary> Gets a value indicating whether Unknown46 is set.</summary>
    public required bool Unknown46 { get; init; }

    /// <summary> Gets a value indicating whether Unknown47 is set.</summary>
    public required bool Unknown47 { get; init; }
}
