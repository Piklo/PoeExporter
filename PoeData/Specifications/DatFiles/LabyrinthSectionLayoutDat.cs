// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.DatFiles;

/// <summary>
/// Class containing LabyrinthSectionLayout.dat data.
/// </summary>
public sealed partial class LabyrinthSectionLayoutDat
{
    /// <summary> Gets LabyrinthSectionKey.</summary>
    /// <remarks> references <see cref="LabyrinthSectionDat"/> on <see cref="Specification.LoadLabyrinthSectionDat"/> index.</remarks>
    public required int? LabyrinthSectionKey { get; init; }

    /// <summary> Gets Unknown16.</summary>
    public required int Unknown16 { get; init; }

    /// <summary> Gets LabyrinthSectionLayoutKeys.</summary>
    /// <remarks> references <see cref="LabyrinthSectionLayoutDat"/> on <see cref="Specification.LoadLabyrinthSectionLayoutDat"/> index.</remarks>
    public required ReadOnlyCollection<int> LabyrinthSectionLayoutKeys { get; init; }

    /// <summary> Gets LabyrinthSecretsKey0.</summary>
    /// <remarks> references <see cref="LabyrinthSecretsDat"/> on <see cref="Specification.LoadLabyrinthSecretsDat"/> index.</remarks>
    public required int? LabyrinthSecretsKey0 { get; init; }

    /// <summary> Gets LabyrinthSecretsKey1.</summary>
    /// <remarks> references <see cref="LabyrinthSecretsDat"/> on <see cref="Specification.LoadLabyrinthSecretsDat"/> index.</remarks>
    public required int? LabyrinthSecretsKey1 { get; init; }

    /// <summary> Gets LabyrinthAreasKey.</summary>
    /// <remarks> references <see cref="LabyrinthAreasDat"/> on <see cref="Specification.LoadLabyrinthAreasDat"/> index.</remarks>
    public required int? LabyrinthAreasKey { get; init; }

    /// <summary> Gets Float0.</summary>
    public required float Float0 { get; init; }

    /// <summary> Gets Float1.</summary>
    public required float Float1 { get; init; }

    /// <summary> Gets LabyrinthNodeOverridesKeys.</summary>
    /// <remarks> references <see cref="LabyrinthNodeOverridesDat"/> on <see cref="Specification.LoadLabyrinthNodeOverridesDat"/> index.</remarks>
    public required ReadOnlyCollection<int> LabyrinthNodeOverridesKeys { get; init; }
}
