﻿// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.SpecificationFiles;

/// <summary>
/// Class containing HellscapeModificationInventoryLayout.dat data.
/// </summary>
public sealed partial class HellscapeModificationInventoryLayoutDat : ISpecificationFile<HellscapeModificationInventoryLayoutDat>
{
    /// <summary> Gets Id.</summary>
    public required string Id { get; init; }

    /// <summary> Gets Column.</summary>
    public required int Column { get; init; }

    /// <summary> Gets Row.</summary>
    public required int Row { get; init; }

    /// <summary> Gets a value indicating whether IsMapSlot is set.</summary>
    public required bool IsMapSlot { get; init; }

    /// <summary> Gets Unknown17.</summary>
    public required int Unknown17 { get; init; }

    /// <summary> Gets Width.</summary>
    public required int Width { get; init; }

    /// <summary> Gets Height.</summary>
    public required int Height { get; init; }

    /// <summary> Gets Stat.</summary>
    public required int? Stat { get; init; }

    /// <summary> Gets StatValue.</summary>
    public required int StatValue { get; init; }

    /// <summary> Gets UnlockedWith.</summary>
    public required int? UnlockedWith { get; init; }

    /// <summary> Gets Quest.</summary>
    public required int? Quest { get; init; }

    /// <inheritdoc/>
    public static HellscapeModificationInventoryLayoutDat[] Load(Specification specification)
    {
        if (specification is null)
        {
            throw new ArgumentNullException(nameof(specification));
        }

        var fileToFind = Encoding.ASCII.GetBytes("Data/HellscapeModificationInventoryLayout.dat64");
        var fileRecord = specification.DataLoader.GetFileRecord(fileToFind);
        var decompressedFile = specification.DataLoader.GetFileBytes(fileRecord);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new HellscapeModificationInventoryLayoutDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading referenced tables if any
            // specification.GetStatsDat();
            // specification.GetHellscapePassivesDat();
            // specification.GetQuestDat();

            // loading Id
            (var idLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Column
            (var columnLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Row
            (var rowLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading IsMapSlot
            (var ismapslotLoading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown17
            (var unknown17Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Width
            (var widthLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Height
            (var heightLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Stat
            (var statLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading StatValue
            (var statvalueLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading UnlockedWith
            (var unlockedwithLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading Quest
            (var questLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            if (offset != expectedOffset)
            {
                throw new NotImplementedException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new HellscapeModificationInventoryLayoutDat()
            {
                Id = idLoading,
                Column = columnLoading,
                Row = rowLoading,
                IsMapSlot = ismapslotLoading,
                Unknown17 = unknown17Loading,
                Width = widthLoading,
                Height = heightLoading,
                Stat = statLoading,
                StatValue = statvalueLoading,
                UnlockedWith = unlockedwithLoading,
                Quest = questLoading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}