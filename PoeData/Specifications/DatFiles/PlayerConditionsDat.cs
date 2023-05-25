// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.DatFiles;

/// <summary>
/// Class containing PlayerConditions.dat data.
/// </summary>
public sealed partial class PlayerConditionsDat
{
    /// <summary> Gets Id.</summary>
    public required string Id { get; init; }

    /// <summary> Gets BuffDefinitionsKeys.</summary>
    /// <remarks> references <see cref="BuffDefinitionsDat"/> on <see cref="Specification.LoadBuffDefinitionsDat"/> index.</remarks>
    public required ReadOnlyCollection<int> BuffDefinitionsKeys { get; init; }

    /// <summary> Gets a value indicating whether Unknown24 is set.</summary>
    public required bool Unknown24 { get; init; }

    /// <summary> Gets BuffStacks.</summary>
    public required int BuffStacks { get; init; }

    /// <summary> Gets CharactersKey.</summary>
    /// <remarks> references <see cref="CharactersDat"/> on <see cref="Specification.LoadCharactersDat"/> index.</remarks>
    public required int? CharactersKey { get; init; }

    /// <summary> Gets StatsKeys.</summary>
    /// <remarks> references <see cref="StatsDat"/> on <see cref="Specification.LoadStatsDat"/> index.</remarks>
    public required ReadOnlyCollection<int> StatsKeys { get; init; }

    /// <summary> Gets a value indicating whether Unknown61 is set.</summary>
    public required bool Unknown61 { get; init; }

    /// <summary> Gets StatValue.</summary>
    public required int StatValue { get; init; }

    /// <summary> Gets Unknown66.</summary>
    public required ReadOnlyCollection<int> Unknown66 { get; init; }

    /// <summary> Gets a value indicating whether Unknown82 is set.</summary>
    public required bool Unknown82 { get; init; }
}
