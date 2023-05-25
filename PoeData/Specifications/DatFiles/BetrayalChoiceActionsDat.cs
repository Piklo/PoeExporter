// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.DatFiles;

/// <summary>
/// Class containing BetrayalChoiceActions.dat data.
/// </summary>
public sealed partial class BetrayalChoiceActionsDat
{
    /// <summary> Gets Id.</summary>
    public required string Id { get; init; }

    /// <summary> Gets BetrayalChoicesKey.</summary>
    /// <remarks> references <see cref="BetrayalChoicesDat"/> on <see cref="Specification.LoadBetrayalChoicesDat"/> index.</remarks>
    public required int? BetrayalChoicesKey { get; init; }

    /// <summary> Gets ClientStringsKey.</summary>
    /// <remarks> references <see cref="ClientStringsDat"/> on <see cref="Specification.LoadClientStringsDat"/> index.</remarks>
    public required int? ClientStringsKey { get; init; }
}
