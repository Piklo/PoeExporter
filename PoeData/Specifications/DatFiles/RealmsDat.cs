// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.DatFiles;

/// <summary>
/// Class containing Realms.dat data.
/// </summary>
public sealed partial class RealmsDat
{
    /// <summary> Gets Id.</summary>
    public required string Id { get; init; }

    /// <summary> Gets Name.</summary>
    public required string Name { get; init; }

    /// <summary> Gets Server.</summary>
    public required ReadOnlyCollection<string> Server { get; init; }

    /// <summary> Gets a value indicating whether IsEnabled is set.</summary>
    public required bool IsEnabled { get; init; }

    /// <summary> Gets Server2.</summary>
    public required ReadOnlyCollection<string> Server2 { get; init; }

    /// <summary> Gets ShortName.</summary>
    public required string ShortName { get; init; }

    /// <summary> Gets Unknown57.</summary>
    /// <remarks> references <see cref="RealmsDat"/> on <see cref="Specification.LoadRealmsDat"/> index.</remarks>
    public required ReadOnlyCollection<int> Unknown57 { get; init; }

    /// <summary> Gets Unknown73.</summary>
    /// <remarks> references <see cref="RealmsDat"/> on <see cref="Specification.LoadRealmsDat"/> index.</remarks>
    public required int? Unknown73 { get; init; }

    /// <summary> Gets Unknown81.</summary>
    public required int Unknown81 { get; init; }

    /// <summary> Gets a value indicating whether IsGammaRealm is set.</summary>
    public required bool IsGammaRealm { get; init; }

    /// <summary> Gets SpeedtestUrl.</summary>
    public required ReadOnlyCollection<string> SpeedtestUrl { get; init; }
}
