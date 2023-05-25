// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.DatFiles;

/// <summary>
/// Class containing Music.dat data.
/// </summary>
public sealed partial class MusicDat
{
    /// <summary> Gets Id.</summary>
    public required string Id { get; init; }

    /// <summary> Gets SoundFile.</summary>
    public required string SoundFile { get; init; }

    /// <summary> Gets BankFile.</summary>
    public required string BankFile { get; init; }

    /// <summary> Gets HASH16.</summary>
    public required int HASH16 { get; init; }

    /// <summary> Gets a value indicating whether IsAvailableInHideout is set.</summary>
    public required bool IsAvailableInHideout { get; init; }

    /// <summary> Gets Name.</summary>
    public required string Name { get; init; }

    /// <summary> Gets Unknown37.</summary>
    public required string Unknown37 { get; init; }

    /// <summary> Gets MusicCategories.</summary>
    /// <remarks> references <see cref="MusicCategoriesDat"/> on <see cref="Specification.LoadMusicCategoriesDat"/> index.</remarks>
    public required ReadOnlyCollection<int> MusicCategories { get; init; }

    /// <summary> Gets a value indicating whether Unknown61 is set.</summary>
    public required bool Unknown61 { get; init; }

    /// <summary> Gets Unknown62.</summary>
    public required int Unknown62 { get; init; }
}
