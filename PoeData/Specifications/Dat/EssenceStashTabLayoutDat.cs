// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.Dat;

/// <summary>
/// Class containing EssenceStashTabLayout.dat data.
/// </summary>
public sealed partial class EssenceStashTabLayoutDat : ISpecificationFile<EssenceStashTabLayoutDat>
{
    /// <summary> Gets Id.</summary>
    public required string Id { get; init; }

    /// <summary> Gets BaseItemTypesKey.</summary>
    public required int? BaseItemTypesKey { get; init; }

    /// <summary> Gets X.</summary>
    public required int X { get; init; }

    /// <summary> Gets Y.</summary>
    public required int Y { get; init; }

    /// <summary> Gets IntId.</summary>
    public required int IntId { get; init; }

    /// <summary> Gets SlotWidth.</summary>
    public required int SlotWidth { get; init; }

    /// <summary> Gets SlotHeight.</summary>
    public required int SlotHeight { get; init; }

    /// <summary> Gets a value indicating whether IsUpgradableEssenceSlot is set.</summary>
    public required bool IsUpgradableEssenceSlot { get; init; }

    /// <inheritdoc/>
    public static EssenceStashTabLayoutDat[] Load(Specification specification)
    {
        if (specification is null)
        {
            throw new ArgumentNullException(nameof(specification));
        }

        var fileToFind = Encoding.ASCII.GetBytes("Data/EssenceStashTabLayout.dat64");
        var fileRecord = specification.DataLoader.GetFileRecord(fileToFind);
        var decompressedFile = specification.DataLoader.GetFileBytes(fileRecord);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new EssenceStashTabLayoutDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading referenced tables if any
            // specification.GetBaseItemTypesDat();

            // loading Id
            (var idLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading BaseItemTypesKey
            (var baseitemtypeskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading X
            (var xLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Y
            (var yLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading IntId
            (var intidLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading SlotWidth
            (var slotwidthLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading SlotHeight
            (var slotheightLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading IsUpgradableEssenceSlot
            (var isupgradableessenceslotLoading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            if (offset != expectedOffset)
            {
                throw new NotImplementedException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new EssenceStashTabLayoutDat()
            {
                Id = idLoading,
                BaseItemTypesKey = baseitemtypeskeyLoading,
                X = xLoading,
                Y = yLoading,
                IntId = intidLoading,
                SlotWidth = slotwidthLoading,
                SlotHeight = slotheightLoading,
                IsUpgradableEssenceSlot = isupgradableessenceslotLoading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
