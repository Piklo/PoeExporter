﻿// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.SpecificationFiles;

/// <summary>
/// Class containing PassiveJewelSlots.dat data.
/// </summary>
public sealed partial class PassiveJewelSlotsDat : ISpecificationFile<PassiveJewelSlotsDat>
{
    /// <summary> Gets Slot.</summary>
    public required int? Slot { get; init; }

    /// <summary> Gets ClusterJewelSize.</summary>
    public required int? ClusterJewelSize { get; init; }

    /// <summary> Gets Unknown32.</summary>
    public required int Unknown32 { get; init; }

    /// <summary> Gets ReplacesSlot.</summary>
    public required int? ReplacesSlot { get; init; }

    /// <summary> Gets ProxySlot.</summary>
    public required int? ProxySlot { get; init; }

    /// <summary> Gets StartIndices.</summary>
    public required ReadOnlyCollection<int> StartIndices { get; init; }

    /// <inheritdoc/>
    public static PassiveJewelSlotsDat[] Load(Specification specification)
    {
        if (specification is null)
        {
            throw new ArgumentNullException(nameof(specification));
        }

        var fileToFind = Encoding.ASCII.GetBytes("Data/PassiveJewelSlots.dat64");
        var fileRecord = specification.DataLoader.GetFileRecord(fileToFind);
        var decompressedFile = specification.DataLoader.GetFileBytes(fileRecord);

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

            // loading referenced tables if any
            // specification.GetPassiveSkillsDat();
            // specification.GetPassiveTreeExpansionJewelSizesDat();

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