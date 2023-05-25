// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.DatFiles;

/// <summary>
/// Class containing Prophecies.dat data.
/// </summary>
public sealed partial class PropheciesDat
{
    /// <summary> Gets Id.</summary>
    public required string Id { get; init; }

    /// <summary> Gets PredictionText.</summary>
    public required string PredictionText { get; init; }

    /// <summary> Gets Unknown16.</summary>
    public required int Unknown16 { get; init; }

    /// <summary> Gets Name.</summary>
    public required string Name { get; init; }

    /// <summary> Gets FlavourText.</summary>
    public required string FlavourText { get; init; }

    /// <summary> Gets QuestTracker_ClientStringsKeys.</summary>
    /// <remarks> references <see cref="ClientStringsDat"/> on <see cref="Specification.LoadClientStringsDat"/> index.</remarks>
    public required ReadOnlyCollection<int> QuestTracker_ClientStringsKeys { get; init; }

    /// <summary> Gets OGGFile.</summary>
    public required string OGGFile { get; init; }

    /// <summary> Gets ProphecyChainKey.</summary>
    /// <remarks> references <see cref="ProphecyChainDat"/> on <see cref="Specification.LoadProphecyChainDat"/> index.</remarks>
    public required int? ProphecyChainKey { get; init; }

    /// <summary> Gets ProphecyChainPosition.</summary>
    public required int ProphecyChainPosition { get; init; }

    /// <summary> Gets a value indicating whether IsEnabled is set.</summary>
    public required bool IsEnabled { get; init; }

    /// <summary> Gets SealCost.</summary>
    public required int SealCost { get; init; }

    /// <summary> Gets PredictionText2.</summary>
    public required string PredictionText2 { get; init; }
}
