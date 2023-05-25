// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.DatFiles;

/// <summary>
/// Class containing BetrayalDialogue.dat data.
/// </summary>
public sealed partial class BetrayalDialogueDat
{
    /// <summary> Gets Unknown0.</summary>
    public required int? Unknown0 { get; init; }

    /// <summary> Gets Unknown16.</summary>
    public required int Unknown16 { get; init; }

    /// <summary> Gets Unknown20.</summary>
    public required int Unknown20 { get; init; }

    /// <summary> Gets Unknown24.</summary>
    public required ReadOnlyCollection<int> Unknown24 { get; init; }

    /// <summary> Gets BetrayalTargetsKey.</summary>
    /// <remarks> references <see cref="BetrayalTargetsDat"/> on <see cref="Specification.LoadBetrayalTargetsDat"/> index.</remarks>
    public required int? BetrayalTargetsKey { get; init; }

    /// <summary> Gets Unknown56.</summary>
    public required int Unknown56 { get; init; }

    /// <summary> Gets Unknown60.</summary>
    public required int? Unknown60 { get; init; }

    /// <summary> Gets Unknown76.</summary>
    public required ReadOnlyCollection<int> Unknown76 { get; init; }

    /// <summary> Gets BetrayalUpgradesKey.</summary>
    /// <remarks> references <see cref="BetrayalUpgradesDat"/> on <see cref="Specification.LoadBetrayalUpgradesDat"/> index.</remarks>
    public required int? BetrayalUpgradesKey { get; init; }

    /// <summary> Gets a value indicating whether Unknown108 is set.</summary>
    public required bool Unknown108 { get; init; }

    /// <summary> Gets Unknown109.</summary>
    public required ReadOnlyCollection<int> Unknown109 { get; init; }

    /// <summary> Gets Unknown125.</summary>
    public required ReadOnlyCollection<int> Unknown125 { get; init; }

    /// <summary> Gets a value indicating whether Unknown141 is set.</summary>
    public required bool Unknown141 { get; init; }

    /// <summary> Gets Unknown142.</summary>
    public required ReadOnlyCollection<int> Unknown142 { get; init; }

    /// <summary> Gets NPCTextAudioKey.</summary>
    /// <remarks> references <see cref="NPCTextAudioDat"/> on <see cref="Specification.LoadNPCTextAudioDat"/> index.</remarks>
    public required int? NPCTextAudioKey { get; init; }

    /// <summary> Gets Unknown174.</summary>
    public required ReadOnlyCollection<int> Unknown174 { get; init; }
}
