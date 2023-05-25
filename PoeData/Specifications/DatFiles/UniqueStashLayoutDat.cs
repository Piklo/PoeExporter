// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.DatFiles;

/// <summary>
/// Class containing UniqueStashLayout.dat data.
/// </summary>
public sealed partial class UniqueStashLayoutDat
{
    /// <summary> Gets WordsKey.</summary>
    /// <remarks> references <see cref="WordsDat"/> on <see cref="Specification.LoadWordsDat"/> index.</remarks>
    public required int? WordsKey { get; init; }

    /// <summary> Gets ItemVisualIdentityKey.</summary>
    /// <remarks> references <see cref="ItemVisualIdentityDat"/> on <see cref="Specification.LoadItemVisualIdentityDat"/> index.</remarks>
    public required int? ItemVisualIdentityKey { get; init; }

    /// <summary> Gets UniqueStashTypesKey.</summary>
    /// <remarks> references <see cref="UniqueStashTypesDat"/> on <see cref="Specification.LoadUniqueStashTypesDat"/> index.</remarks>
    public required int? UniqueStashTypesKey { get; init; }

    /// <summary> Gets Unknown48.</summary>
    public required int Unknown48 { get; init; }

    /// <summary> Gets Unknown52.</summary>
    public required int Unknown52 { get; init; }

    /// <summary> Gets OverrideWidth.</summary>
    public required int OverrideWidth { get; init; }

    /// <summary> Gets OverrideHeight.</summary>
    public required int OverrideHeight { get; init; }

    /// <summary> Gets a value indicating whether ShowIfEmptyChallengeLeague is set.</summary>
    public required bool ShowIfEmptyChallengeLeague { get; init; }

    /// <summary> Gets a value indicating whether ShowIfEmptyStandard is set.</summary>
    public required bool ShowIfEmptyStandard { get; init; }

    /// <summary> Gets RenamedVersion.</summary>
    /// <remarks> references <see cref="UniqueStashLayoutDat"/> on <see cref="Specification.LoadUniqueStashLayoutDat"/> index.</remarks>
    public required int? RenamedVersion { get; init; }

    /// <summary> Gets BaseVersion.</summary>
    /// <remarks> references <see cref="UniqueStashLayoutDat"/> on <see cref="Specification.LoadUniqueStashLayoutDat"/> index.</remarks>
    public required int? BaseVersion { get; init; }

    /// <summary> Gets a value indicating whether IsAlternateArt is set.</summary>
    public required bool IsAlternateArt { get; init; }
}
