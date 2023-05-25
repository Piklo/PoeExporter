// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.DatFiles;

/// <summary>
/// Class containing UltimatumEncounters.dat data.
/// </summary>
public sealed partial class UltimatumEncountersDat
{
    /// <summary> Gets Id.</summary>
    public required string Id { get; init; }

    /// <summary> Gets Description.</summary>
    public required string Description { get; init; }

    /// <summary> Gets ModTypes.</summary>
    /// <remarks> references <see cref="UltimatumModifierTypesDat"/> on <see cref="Specification.LoadUltimatumModifierTypesDat"/> index.</remarks>
    public required ReadOnlyCollection<int> ModTypes { get; init; }

    /// <summary> Gets BossARMFile.</summary>
    public required string BossARMFile { get; init; }

    /// <summary> Gets Type.</summary>
    /// <remarks> references <see cref="UltimatumEncounterTypesDat"/> on <see cref="Specification.LoadUltimatumEncounterTypesDat"/> index.</remarks>
    public required int? Type { get; init; }

    /// <summary> Gets Icon.</summary>
    public required string Icon { get; init; }

    /// <summary> Gets HASH16.</summary>
    public required int HASH16 { get; init; }

    /// <summary> Gets a value indicating whether Unknown68 is set.</summary>
    public required bool Unknown68 { get; init; }

    /// <summary> Gets Unknown69.</summary>
    public required int Unknown69 { get; init; }
}
