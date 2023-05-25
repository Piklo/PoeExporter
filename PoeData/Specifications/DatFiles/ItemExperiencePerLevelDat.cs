// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.DatFiles;

/// <summary>
/// Class containing ItemExperiencePerLevel.dat data.
/// </summary>
public sealed partial class ItemExperiencePerLevelDat
{
    /// <summary> Gets ItemExperienceType.</summary>
    /// <remarks> references <see cref="ItemExperienceTypesDat"/> on <see cref="Specification.LoadItemExperienceTypesDat"/> index.</remarks>
    public required int? ItemExperienceType { get; init; }

    /// <summary> Gets ItemCurrentLevel.</summary>
    public required int ItemCurrentLevel { get; init; }

    /// <summary> Gets Experience.</summary>
    public required int Experience { get; init; }
}
