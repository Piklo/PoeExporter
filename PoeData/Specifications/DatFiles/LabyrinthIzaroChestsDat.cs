// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.DatFiles;

/// <summary>
/// Class containing LabyrinthIzaroChests.dat data.
/// </summary>
public sealed partial class LabyrinthIzaroChestsDat
{
    /// <summary> Gets Id.</summary>
    public required string Id { get; init; }

    /// <summary> Gets ChestsKey.</summary>
    /// <remarks> references <see cref="ChestsDat"/> on <see cref="Specification.LoadChestsDat"/> index.</remarks>
    public required int? ChestsKey { get; init; }

    /// <summary> Gets SpawnWeight.</summary>
    public required int SpawnWeight { get; init; }

    /// <summary> Gets MinLabyrinthTier.</summary>
    public required int MinLabyrinthTier { get; init; }

    /// <summary> Gets MaxLabyrinthTier.</summary>
    public required int MaxLabyrinthTier { get; init; }

    /// <summary>
    /// Gets LabyrinthIzaroChestsDat data.
    /// </summary>
    /// <param name="dataLoader">data loader.</param>
    /// <returns>array of LabyrinthIzaroChestsDat.</returns>
    internal static LabyrinthIzaroChestsDat[] Load(DataLoader dataLoader)
    {
        if (dataLoader is null)
        {
            throw new ArgumentNullException(nameof(dataLoader));
        }

        const string filePath = "Data/LabyrinthIzaroChests.dat64";
        var decompressedFile = dataLoader.GetFileBytes(filePath);

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
                throw new SchemaMismatchException($"offset {offset} != expectedOffset {expectedOffset}");
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
