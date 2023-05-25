// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.DatFiles;

/// <summary>
/// Class containing TalismanMonsterMods.dat data.
/// </summary>
public sealed partial class TalismanMonsterModsDat
{
    /// <summary> Gets ModTypeKey.</summary>
    /// <remarks> references <see cref="ModTypeDat"/> on <see cref="Specification.LoadModTypeDat"/> index.</remarks>
    public required int? ModTypeKey { get; init; }

    /// <summary> Gets ModsKey.</summary>
    /// <remarks> references <see cref="ModsDat"/> on <see cref="Specification.LoadModsDat"/> index.</remarks>
    public required int? ModsKey { get; init; }
}
