// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.DatFiles;

/// <summary>
/// Class containing SurgeTypes.dat data.
/// </summary>
public sealed partial class SurgeTypesDat
{
    /// <summary> Gets Id.</summary>
    public required string Id { get; init; }

    /// <summary> Gets SurgeEffects.</summary>
    /// <remarks> references <see cref="SurgeEffectsDat"/> on <see cref="Specification.LoadSurgeEffectsDat"/> index.</remarks>
    public required ReadOnlyCollection<int> SurgeEffects { get; init; }

    /// <summary> Gets IntId.</summary>
    public required int IntId { get; init; }
}
