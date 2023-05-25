// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.DatFiles;

/// <summary>
/// Class containing AtlasPrimordialAltarChoices.dat data.
/// </summary>
public sealed partial class AtlasPrimordialAltarChoicesDat
{
    /// <summary> Gets Mod.</summary>
    /// <remarks> references <see cref="ModsDat"/> on <see cref="Specification.LoadModsDat"/> index.</remarks>
    public required int? Mod { get; init; }

    /// <summary> Gets Type.</summary>
    /// <remarks> references <see cref="AtlasPrimordialAltarChoiceTypesDat"/> on <see cref="Specification.LoadAtlasPrimordialAltarChoiceTypesDat"/> index.</remarks>
    public required int? Type { get; init; }

    /// <summary> Gets a value indicating whether Unknown32 is set.</summary>
    public required bool Unknown32 { get; init; }
}
