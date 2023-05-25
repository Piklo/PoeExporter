// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.DatFiles;

/// <summary>
/// Class containing Animation.dat data.
/// </summary>
public sealed partial class AnimationDat
{
    /// <summary> Gets Id.</summary>
    public required string Id { get; init; }

    /// <summary> Gets a value indicating whether Unknown8 is set.</summary>
    public required bool Unknown8 { get; init; }

    /// <summary> Gets a value indicating whether Unknown9 is set.</summary>
    public required bool Unknown9 { get; init; }

    /// <summary> Gets a value indicating whether Unknown10 is set.</summary>
    public required bool Unknown10 { get; init; }

    /// <summary> Gets Mainhand_AnimationKey.</summary>
    /// <remarks> references <see cref="AnimationDat"/> on <see cref="AnimationDat.Id"/>.</remarks>
    public required string Mainhand_AnimationKey { get; init; }

    /// <summary> Gets Offhand_AnimationKey.</summary>
    /// <remarks> references <see cref="AnimationDat"/> on <see cref="AnimationDat.Id"/>.</remarks>
    public required string Offhand_AnimationKey { get; init; }

    /// <summary> Gets a value indicating whether Unknown27 is set.</summary>
    public required bool Unknown27 { get; init; }

    /// <summary> Gets Unknown28.</summary>
    public required int? Unknown28 { get; init; }
}
