// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.DatFiles;

/// <summary>
/// Class containing HellscapeFactions.dat data.
/// </summary>
public sealed partial class HellscapeFactionsDat
{
    /// <summary> Gets Id.</summary>
    public required string Id { get; init; }

    /// <summary> Gets Unknown8.</summary>
    /// <remarks> references <see cref="EnvironmentsDat"/> on <see cref="Specification.LoadEnvironmentsDat"/> index.</remarks>
    public required int? Unknown8 { get; init; }

    /// <summary> Gets Unknown24.</summary>
    /// <remarks> references <see cref="EnvironmentsDat"/> on <see cref="Specification.LoadEnvironmentsDat"/> index.</remarks>
    public required int? Unknown24 { get; init; }

    /// <summary> Gets Unknown40.</summary>
    /// <remarks> references <see cref="EnvironmentsDat"/> on <see cref="Specification.LoadEnvironmentsDat"/> index.</remarks>
    public required ReadOnlyCollection<int> Unknown40 { get; init; }

    /// <summary> Gets Unknown56.</summary>
    public required int Unknown56 { get; init; }

    /// <summary> Gets Unknown60.</summary>
    /// <remarks> references <see cref="MonsterVarietiesDat"/> on <see cref="Specification.LoadMonsterVarietiesDat"/> index.</remarks>
    public required ReadOnlyCollection<int> Unknown60 { get; init; }

    /// <summary> Gets Unknown76.</summary>
    public required string Unknown76 { get; init; }

    /// <summary> Gets Boss.</summary>
    /// <remarks> references <see cref="MonsterVarietiesDat"/> on <see cref="Specification.LoadMonsterVarietiesDat"/> index.</remarks>
    public required int? Boss { get; init; }
}
