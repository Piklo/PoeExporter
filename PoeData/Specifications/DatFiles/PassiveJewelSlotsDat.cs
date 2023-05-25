// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.DatFiles;

/// <summary>
/// Class containing PassiveJewelSlots.dat data.
/// </summary>
public sealed partial class PassiveJewelSlotsDat
{
    /// <summary> Gets Slot.</summary>
    /// <remarks> references <see cref="PassiveSkillsDat"/> on <see cref="Specification.LoadPassiveSkillsDat"/> index.</remarks>
    public required int? Slot { get; init; }

    /// <summary> Gets ClusterJewelSize.</summary>
    /// <remarks> references <see cref="PassiveTreeExpansionJewelSizesDat"/> on <see cref="Specification.LoadPassiveTreeExpansionJewelSizesDat"/> index.</remarks>
    public required int? ClusterJewelSize { get; init; }

    /// <summary> Gets Unknown32.</summary>
    public required int Unknown32 { get; init; }

    /// <summary> Gets ReplacesSlot.</summary>
    /// <remarks> references <see cref="PassiveJewelSlotsDat"/> on <see cref="Specification.LoadPassiveJewelSlotsDat"/> index.</remarks>
    public required int? ReplacesSlot { get; init; }

    /// <summary> Gets ProxySlot.</summary>
    /// <remarks> references <see cref="PassiveSkillsDat"/> on <see cref="Specification.LoadPassiveSkillsDat"/> index.</remarks>
    public required int? ProxySlot { get; init; }

    /// <summary> Gets StartIndices.</summary>
    public required ReadOnlyCollection<int> StartIndices { get; init; }
}
