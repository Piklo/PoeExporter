// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.DatFiles;

/// <summary>
/// Class containing DelveCraftingTags.dat data.
/// </summary>
public sealed partial class DelveCraftingTagsDat
{
    /// <summary> Gets TagsKey.</summary>
    /// <remarks> references <see cref="TagsDat"/> on <see cref="Specification.LoadTagsDat"/> index.</remarks>
    public required int? TagsKey { get; init; }

    /// <summary> Gets ItemClass.</summary>
    public required string ItemClass { get; init; }
}
