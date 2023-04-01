﻿// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.SpecificationFiles;

/// <summary>
/// Class containing LabyrinthIzaroChests.dat data.
/// </summary>
public sealed partial class LabyrinthIzaroChestsDat : ISpecificationFile<LabyrinthIzaroChestsDat>
{
    /// <summary> Gets Id.</summary>
    public required string Id { get; init; }

    /// <summary> Gets ChestsKey.</summary>
    public required int? ChestsKey { get; init; }

    /// <summary> Gets SpawnWeight.</summary>
    public required int SpawnWeight { get; init; }

    /// <summary> Gets MinLabyrinthTier.</summary>
    public required int MinLabyrinthTier { get; init; }

    /// <summary> Gets MaxLabyrinthTier.</summary>
    public required int MaxLabyrinthTier { get; init; }

    /// <inheritdoc/>
    public static LabyrinthIzaroChestsDat[] Load(Specification specification)
    {
        if (specification is null)
        {
            throw new ArgumentNullException(nameof(specification));
        }

        var fileToFind = Encoding.ASCII.GetBytes("Data/LabyrinthIzaroChests.dat64");
        var fileRecord = specification.DataLoader.GetFileRecord(fileToFind);
        var decompressedFile = specification.DataLoader.GetFileBytes(fileRecord);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new LabyrinthIzaroChestsDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading referenced tables if any
            // specification.GetChestsDat();

            // loading Id
            (var idLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading ChestsKey
            (var chestskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading SpawnWeight
            (var spawnweightLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading MinLabyrinthTier
            (var minlabyrinthtierLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading MaxLabyrinthTier
            (var maxlabyrinthtierLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            if (offset != expectedOffset)
            {
                throw new NotImplementedException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new LabyrinthIzaroChestsDat()
            {
                Id = idLoading,
                ChestsKey = chestskeyLoading,
                SpawnWeight = spawnweightLoading,
                MinLabyrinthTier = minlabyrinthtierLoading,
                MaxLabyrinthTier = maxlabyrinthtierLoading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}