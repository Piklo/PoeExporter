// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.DatFiles;

/// <summary>
/// Class containing MonsterConditionalEffectPacks.dat data.
/// </summary>
public sealed partial class MonsterConditionalEffectPacksDat
{
    /// <summary> Gets Id.</summary>
    public required string Id { get; init; }

    /// <summary> Gets MiscEffectPack1.</summary>
    /// <remarks> references <see cref="MiscEffectPacksDat"/> on <see cref="Specification.LoadMiscEffectPacksDat"/> index.</remarks>
    public required ReadOnlyCollection<int> MiscEffectPack1 { get; init; }

    /// <summary> Gets MiscEffectPack2.</summary>
    /// <remarks> references <see cref="MiscEffectPacksDat"/> on <see cref="Specification.LoadMiscEffectPacksDat"/> index.</remarks>
    public required ReadOnlyCollection<int> MiscEffectPack2 { get; init; }

    /// <summary> Gets MiscEffectPack3.</summary>
    /// <remarks> references <see cref="MiscEffectPacksDat"/> on <see cref="Specification.LoadMiscEffectPacksDat"/> index.</remarks>
    public required ReadOnlyCollection<int> MiscEffectPack3 { get; init; }

    /// <summary> Gets MiscEffectPack4.</summary>
    /// <remarks> references <see cref="MiscEffectPacksDat"/> on <see cref="Specification.LoadMiscEffectPacksDat"/> index.</remarks>
    public required ReadOnlyCollection<int> MiscEffectPack4 { get; init; }

    /// <summary> Gets Unknown72.</summary>
    public required int Unknown72 { get; init; }
}
