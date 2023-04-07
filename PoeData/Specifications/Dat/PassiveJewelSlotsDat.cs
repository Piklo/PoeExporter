// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.Dat;

/// <summary>
/// Class containing PassiveJewelSlots.dat data.
/// </summary>
public sealed partial class PassiveJewelSlotsDat
{
    /// <summary> Gets Slot.</summary>
    /// <remarks> references <see cref="PassiveSkillsDat"/> on <see cref="Specification.GetPassiveSkillsDat"/> index.</remarks>
    public required int? Slot { get; init; }

    /// <summary> Gets ClusterJewelSize.</summary>
    /// <remarks> references <see cref="PassiveTreeExpansionJewelSizesDat"/> on <see cref="Specification.GetPassiveTreeExpansionJewelSizesDat"/> index.</remarks>
    public required int? ClusterJewelSize { get; init; }

    /// <summary> Gets Unknown32.</summary>
    public required int Unknown32 { get; init; }

    /// <summary> Gets ReplacesSlot.</summary>
    /// <remarks> references <see cref="PassiveJewelSlotsDat"/> on <see cref="Specification.GetPassiveJewelSlotsDat"/> index.</remarks>
    public required int? ReplacesSlot { get; init; }

    /// <summary> Gets ProxySlot.</summary>
    /// <remarks> references <see cref="PassiveSkillsDat"/> on <see cref="Specification.GetPassiveSkillsDat"/> index.</remarks>
    public required int? ProxySlot { get; init; }

    /// <summary> Gets StartIndices.</summary>
    public required ReadOnlyCollection<int> StartIndices { get; init; }

    /// <summary>
    /// Gets PassiveJewelSlotsDat data.
    /// </summary>
    /// <param name="dataLoader">data loader.</param>
    /// <returns>array of PassiveJewelSlotsDat.</returns>
    internal static PassiveJewelSlotsDat[] Load(DataLoader dataLoader)
    {
        if (dataLoader is null)
        {
            throw new ArgumentNullException(nameof(dataLoader));
        }

        const string filePath = "Data/PassiveJewelSlots.dat64";
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new PassiveJewelSlotsDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading Slot
            (var slotLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading ClusterJewelSize
            (var clusterjewelsizeLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading Unknown32
            (var unknown32Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading ReplacesSlot
            (var replacesslotLoading, offset) = SpecificationFileLoader.LoadRowPrimaryKey(decompressedFile, offset);

            // loading ProxySlot
            (var proxyslotLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading StartIndices
            (var tempstartindicesLoading, offset) = SpecificationFileLoader.LoadIntArray(decompressedFile, offset, dataOffset);
            var startindicesLoading = tempstartindicesLoading.AsReadOnly();

            if (offset != expectedOffset)
            {
                throw new NotImplementedException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new PassiveJewelSlotsDat()
            {
                Slot = slotLoading,
                ClusterJewelSize = clusterjewelsizeLoading,
                Unknown32 = unknown32Loading,
                ReplacesSlot = replacesslotLoading,
                ProxySlot = proxyslotLoading,
                StartIndices = startindicesLoading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
