// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.DatFiles;

/// <summary>
/// Class containing MultiPartAchievementConditions.dat data.
/// </summary>
public sealed partial class MultiPartAchievementConditionsDat
{
    /// <summary> Gets Id.</summary>
    public required string Id { get; init; }

    /// <summary> Gets MultiPartAchievementsKey1.</summary>
    /// <remarks> references <see cref="MultiPartAchievementsDat"/> on <see cref="Specification.LoadMultiPartAchievementsDat"/> index.</remarks>
    public required int? MultiPartAchievementsKey1 { get; init; }

    /// <summary> Gets MultiPartAchievementsKey2.</summary>
    /// <remarks> references <see cref="MultiPartAchievementsDat"/> on <see cref="Specification.LoadMultiPartAchievementsDat"/> index.</remarks>
    public required int? MultiPartAchievementsKey2 { get; init; }

    /// <summary> Gets Unknown40.</summary>
    public required int Unknown40 { get; init; }

    /// <summary> Gets Unknown44.</summary>
    public required int Unknown44 { get; init; }
}
