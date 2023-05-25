// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.DatFiles;

/// <summary>
/// Class containing MonsterHeights.dat data.
/// </summary>
public sealed partial class MonsterHeightsDat
{
    /// <summary> Gets MonsterVariety.</summary>
    /// <remarks> references <see cref="MonsterVarietiesDat"/> on <see cref="Specification.LoadMonsterVarietiesDat"/> index.</remarks>
    public required int? MonsterVariety { get; init; }

    /// <summary> Gets Unknown16.</summary>
    public required float Unknown16 { get; init; }

    /// <summary> Gets MonsterHeightBracket.</summary>
    /// <remarks> references <see cref="MonsterHeightBracketsDat"/> on <see cref="Specification.LoadMonsterHeightBracketsDat"/> index.</remarks>
    public required int? MonsterHeightBracket { get; init; }

    /// <summary> Gets Unknown36.</summary>
    public required int Unknown36 { get; init; }

    /// <summary> Gets Unknown40.</summary>
    public required int Unknown40 { get; init; }
}
