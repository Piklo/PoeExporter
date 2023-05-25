// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.DatFiles;

/// <summary>
/// Class containing NPCMaster.dat data.
/// </summary>
public sealed partial class NPCMasterDat
{
    /// <summary> Gets Id.</summary>
    public required string Id { get; init; }

    /// <summary> Gets a value indicating whether Unknown8 is set.</summary>
    public required bool Unknown8 { get; init; }

    /// <summary> Gets a value indicating whether Unknown9 is set.</summary>
    public required bool Unknown9 { get; init; }

    /// <summary> Gets Signature_ModsKey.</summary>
    /// <remarks> references <see cref="ModsDat"/> on <see cref="Specification.LoadModsDat"/> index.</remarks>
    public required int? Signature_ModsKey { get; init; }

    /// <summary> Gets a value indicating whether Unknown26 is set.</summary>
    public required bool Unknown26 { get; init; }

    /// <summary> Gets SpawnWeight_TagsKeys.</summary>
    /// <remarks> references <see cref="TagsDat"/> on <see cref="Specification.LoadTagsDat"/> index.</remarks>
    public required ReadOnlyCollection<int> SpawnWeight_TagsKeys { get; init; }

    /// <summary> Gets SpawnWeight_Values.</summary>
    public required ReadOnlyCollection<int> SpawnWeight_Values { get; init; }

    /// <summary> Gets Unknown59.</summary>
    public required ReadOnlyCollection<int> Unknown59 { get; init; }

    /// <summary> Gets Unknown75.</summary>
    public required ReadOnlyCollection<int> Unknown75 { get; init; }

    /// <summary> Gets Unknown91.</summary>
    public required int? Unknown91 { get; init; }

    /// <summary> Gets Unknown107.</summary>
    public required int Unknown107 { get; init; }

    /// <summary> Gets AreaDescription.</summary>
    public required string AreaDescription { get; init; }

    /// <summary> Gets Unknown119.</summary>
    public required int? Unknown119 { get; init; }

    /// <summary> Gets Unknown135.</summary>
    public required int Unknown135 { get; init; }

    /// <summary> Gets Unknown139.</summary>
    /// <remarks> references <see cref="StatsDat"/> on <see cref="Specification.LoadStatsDat"/> index.</remarks>
    public required int? Unknown139 { get; init; }

    /// <summary> Gets a value indicating whether HasAreaMissions is set.</summary>
    public required bool HasAreaMissions { get; init; }

    /// <summary> Gets Unknown156.</summary>
    public required ReadOnlyCollection<int> Unknown156 { get; init; }

    /// <summary> Gets Unknown172.</summary>
    public required ReadOnlyCollection<int> Unknown172 { get; init; }

    /// <summary> Gets Unknown188.</summary>
    public required int? Unknown188 { get; init; }
}
