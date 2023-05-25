// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.DatFiles;

/// <summary>
/// Class containing EssenceType.dat data.
/// </summary>
public sealed partial class EssenceTypeDat
{
    /// <summary> Gets Id.</summary>
    public required string Id { get; init; }

    /// <summary> Gets EssenceType.</summary>
    public required int EssenceType { get; init; }

    /// <summary> Gets a value indicating whether IsCorruptedEssence is set.</summary>
    public required bool IsCorruptedEssence { get; init; }

    /// <summary> Gets WordsKey.</summary>
    /// <remarks> references <see cref="WordsDat"/> on <see cref="Specification.LoadWordsDat"/> index.</remarks>
    public required int? WordsKey { get; init; }
}
