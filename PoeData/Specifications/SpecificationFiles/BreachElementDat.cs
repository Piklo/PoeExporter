﻿// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.SpecificationFiles;

/// <summary>
/// Class containing BreachElement.dat data.
/// </summary>
public sealed partial class BreachElementDat : ISpecificationFile<BreachElementDat>
{
    /// <summary> Gets Element.</summary>
    public required string Element { get; init; }

    /// <summary> Gets Unknown8.</summary>
    public required int? Unknown8 { get; init; }

    /// <summary> Gets BaseBreachstone.</summary>
    public required int? BaseBreachstone { get; init; }

    /// <summary> Gets BossMapMod.</summary>
    public required int? BossMapMod { get; init; }

    /// <summary> Gets DuplicateBoss.</summary>
    public required int? DuplicateBoss { get; init; }

    /// <inheritdoc/>
    public static BreachElementDat[] Load(Specification specification)
    {
        if (specification is null)
        {
            throw new ArgumentNullException(nameof(specification));
        }

        var fileToFind = Encoding.ASCII.GetBytes("Data/BreachElement.dat64");
        var fileRecord = specification.DataLoader.GetFileRecord(fileToFind);
        var decompressedFile = specification.DataLoader.GetFileBytes(fileRecord);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new BreachElementDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading referenced tables if any
            // specification.GetBaseItemTypesDat();
            // specification.GetStatsDat();

            // loading Element
            (var elementLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Unknown8
            (var unknown8Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading BaseBreachstone
            (var basebreachstoneLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading BossMapMod
            (var bossmapmodLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading DuplicateBoss
            (var duplicatebossLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            if (offset != expectedOffset)
            {
                throw new NotImplementedException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new BreachElementDat()
            {
                Element = elementLoading,
                Unknown8 = unknown8Loading,
                BaseBreachstone = basebreachstoneLoading,
                BossMapMod = bossmapmodLoading,
                DuplicateBoss = duplicatebossLoading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}