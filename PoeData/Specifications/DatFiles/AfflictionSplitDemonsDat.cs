// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.DatFiles;

/// <summary>
/// Class containing AfflictionSplitDemons.dat data.
/// </summary>
public sealed partial class AfflictionSplitDemonsDat
{
    /// <summary> Gets Unknown0.</summary>
    public required int Unknown0 { get; init; }

    /// <summary> Gets MonsterVarietiesKey.</summary>
    /// <remarks> references <see cref="MonsterVarietiesDat"/> on <see cref="Specification.LoadMonsterVarietiesDat"/> index.</remarks>
    public required int? MonsterVarietiesKey { get; init; }

    /// <summary> Gets AfflictionRandomModCategoriesKey.</summary>
    /// <remarks> references <see cref="AfflictionRandomModCategoriesDat"/> on <see cref="Specification.LoadAfflictionRandomModCategoriesDat"/> index.</remarks>
    public required int? AfflictionRandomModCategoriesKey { get; init; }
}
