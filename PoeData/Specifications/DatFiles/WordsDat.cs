// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.DatFiles;

/// <summary>
/// Class containing Words.dat data.
/// </summary>
public sealed partial class WordsDat
{
    /// <summary> Gets Wordlist.</summary>
    /// <remarks> references <see cref="WordlistsDat"/> on <see cref="Specification.LoadWordlistsDat"/> index.</remarks>
    public required int Wordlist { get; init; }

    /// <summary> Gets Text.</summary>
    public required string Text { get; init; }

    /// <summary> Gets SpawnWeight_Tags.</summary>
    /// <remarks> references <see cref="TagsDat"/> on <see cref="Specification.LoadTagsDat"/> index.</remarks>
    public required ReadOnlyCollection<int> SpawnWeight_Tags { get; init; }

    /// <summary> Gets SpawnWeight_Values.</summary>
    public required ReadOnlyCollection<int> SpawnWeight_Values { get; init; }

    /// <summary> Gets Unknown44.</summary>
    public required int Unknown44 { get; init; }

    /// <summary> Gets Text2.</summary>
    public required string Text2 { get; init; }

    /// <summary> Gets Inflection.</summary>
    public required string Inflection { get; init; }
}
