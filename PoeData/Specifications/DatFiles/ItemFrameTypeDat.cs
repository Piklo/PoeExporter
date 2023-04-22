﻿// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.DatFiles;

/// <summary>
/// Class containing ItemFrameType.dat data.
/// </summary>
public sealed partial class ItemFrameTypeDat
{
    /// <summary> Gets Id.</summary>
    public required string Id { get; init; }

    /// <summary> Gets a value indicating whether Unknown8 is set.</summary>
    public required bool Unknown8 { get; init; }

    /// <summary> Gets a value indicating whether DoubleLine is set.</summary>
    public required bool DoubleLine { get; init; }

    /// <summary> Gets HeaderSingle.</summary>
    public required string HeaderSingle { get; init; }

    /// <summary> Gets HeaderDouble.</summary>
    public required string HeaderDouble { get; init; }

    /// <summary> Gets HardmodeHeaderSingle.</summary>
    public required string HardmodeHeaderSingle { get; init; }

    /// <summary> Gets HardmodeHeaderDouble.</summary>
    public required string HardmodeHeaderDouble { get; init; }

    /// <summary> Gets Color.</summary>
    public required ReadOnlyCollection<int> Color { get; init; }

    /// <summary> Gets Separator.</summary>
    public required string Separator { get; init; }

    /// <summary> Gets a value indicating whether Unknown66 is set.</summary>
    public required bool Unknown66 { get; init; }

    /// <summary> Gets Rarity.</summary>
    /// <remarks> references <see cref="RarityDat"/> on <see cref="Specification.LoadRarityDat"/> index.</remarks>
    public required int? Rarity { get; init; }

    /// <summary> Gets DisplayString.</summary>
    /// <remarks> references <see cref="ClientStringsDat"/> on <see cref="Specification.LoadClientStringsDat"/> index.</remarks>
    public required int? DisplayString { get; init; }

    /// <summary> Gets ColorMarkup.</summary>
    public required string ColorMarkup { get; init; }

    /// <summary>
    /// Gets ItemFrameTypeDat data.
    /// </summary>
    /// <param name="dataLoader">data loader.</param>
    /// <returns>array of ItemFrameTypeDat.</returns>
    internal static ItemFrameTypeDat[] Load(DataLoader dataLoader)
    {
        if (dataLoader is null)
        {
            throw new ArgumentNullException(nameof(dataLoader));
        }

        const string filePath = "Data/ItemFrameType.dat64";
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new ItemFrameTypeDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading Id
            (var idLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Unknown8
            (var unknown8Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading DoubleLine
            (var doublelineLoading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading HeaderSingle
            (var headersingleLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading HeaderDouble
            (var headerdoubleLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading HardmodeHeaderSingle
            (var hardmodeheadersingleLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading HardmodeHeaderDouble
            (var hardmodeheaderdoubleLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Color
            (var tempcolorLoading, offset) = SpecificationFileLoader.LoadIntArray(decompressedFile, offset, dataOffset);
            var colorLoading = tempcolorLoading.AsReadOnly();

            // loading Separator
            (var separatorLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Unknown66
            (var unknown66Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Rarity
            (var rarityLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading DisplayString
            (var displaystringLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset);

            // loading ColorMarkup
            (var colormarkupLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            if (offset != expectedOffset)
            {
                throw new SchemaMismatchException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new ItemFrameTypeDat()
            {
                Id = idLoading,
                Unknown8 = unknown8Loading,
                DoubleLine = doublelineLoading,
                HeaderSingle = headersingleLoading,
                HeaderDouble = headerdoubleLoading,
                HardmodeHeaderSingle = hardmodeheadersingleLoading,
                HardmodeHeaderDouble = hardmodeheaderdoubleLoading,
                Color = colorLoading,
                Separator = separatorLoading,
                Unknown66 = unknown66Loading,
                Rarity = rarityLoading,
                DisplayString = displaystringLoading,
                ColorMarkup = colormarkupLoading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
