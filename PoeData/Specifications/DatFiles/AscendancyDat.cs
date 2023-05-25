// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.DatFiles;

/// <summary>
/// Class containing Ascendancy.dat data.
/// </summary>
public sealed partial class AscendancyDat
{
    /// <summary> Gets Id.</summary>
    public required string Id { get; init; }

    /// <summary> Gets ClassNo.</summary>
    public required int ClassNo { get; init; }

    /// <summary> Gets Characters.</summary>
    /// <remarks> references <see cref="CharactersDat"/> on <see cref="Specification.LoadCharactersDat"/> index.</remarks>
    public required ReadOnlyCollection<int> Characters { get; init; }

    /// <summary> Gets CoordinateRect.</summary>
    public required string CoordinateRect { get; init; }

    /// <summary> Gets RGBFlavourTextColour.</summary>
    public required string RGBFlavourTextColour { get; init; }

    /// <summary> Gets Name.</summary>
    public required string Name { get; init; }

    /// <summary> Gets FlavourText.</summary>
    public required string FlavourText { get; init; }

    /// <summary> Gets OGGFile.</summary>
    public required string OGGFile { get; init; }

    /// <summary> Gets PassiveTreeImage.</summary>
    public required string PassiveTreeImage { get; init; }

    /// <summary> Gets Unknown76.</summary>
    public required int Unknown76 { get; init; }

    /// <summary> Gets Unknown80.</summary>
    public required int Unknown80 { get; init; }

    /// <summary> Gets BackgroundImage.</summary>
    public required string BackgroundImage { get; init; }
}
