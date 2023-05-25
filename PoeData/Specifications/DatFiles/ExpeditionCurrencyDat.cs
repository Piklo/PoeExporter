// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.DatFiles;

/// <summary>
/// Class containing ExpeditionCurrency.dat data.
/// </summary>
public sealed partial class ExpeditionCurrencyDat
{
    /// <summary> Gets BaseItemType.</summary>
    /// <remarks> references <see cref="BaseItemTypesDat"/> on <see cref="Specification.LoadBaseItemTypesDat"/> index.</remarks>
    public required int? BaseItemType { get; init; }

    /// <summary> Gets Tier.</summary>
    public required int Tier { get; init; }

    /// <summary> Gets NPC.</summary>
    /// <remarks> references <see cref="ExpeditionNPCsDat"/> on <see cref="Specification.LoadExpeditionNPCsDat"/> index.</remarks>
    public required int? NPC { get; init; }

    /// <summary> Gets LootSound.</summary>
    /// <remarks> references <see cref="SoundEffectsDat"/> on <see cref="Specification.LoadSoundEffectsDat"/> index.</remarks>
    public required int? LootSound { get; init; }
}
