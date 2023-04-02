// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.Dat;

/// <summary>
/// Class containing Strongboxes.dat data.
/// </summary>
public sealed partial class StrongboxesDat : ISpecificationFile<StrongboxesDat>
{
    /// <summary> Gets ChestsKey.</summary>
    public required int? ChestsKey { get; init; }

    /// <summary> Gets SpawnWeight.</summary>
    public required int SpawnWeight { get; init; }

    /// <summary> Gets Unknown20.</summary>
    public required int Unknown20 { get; init; }

    /// <summary> Gets a value indicating whether IsCartographerBox is set.</summary>
    public required bool IsCartographerBox { get; init; }

    /// <summary> Gets a value indicating whether Unknown25 is set.</summary>
    public required bool Unknown25 { get; init; }

    /// <summary> Gets SpawnWeightIncrease.</summary>
    public required int? SpawnWeightIncrease { get; init; }

    /// <summary> Gets SpawnWeightHardmode.</summary>
    public required int SpawnWeightHardmode { get; init; }

    /// <inheritdoc/>
    public static StrongboxesDat[] Load(Specification specification)
    {
        if (specification is null)
        {
            throw new ArgumentNullException(nameof(specification));
        }

        var fileToFind = Encoding.ASCII.GetBytes("Data/Strongboxes.dat64");
        var fileRecord = specification.DataLoader.GetFileRecord(fileToFind);
        var decompressedFile = specification.DataLoader.GetFileBytes(fileRecord);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new StrongboxesDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading referenced tables if any
            // specification.GetChestsDat();
            // specification.GetStatsDat();

            // loading ChestsKey
            (var chestskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading SpawnWeight
            (var spawnweightLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown20
            (var unknown20Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading IsCartographerBox
            (var iscartographerboxLoading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown25
            (var unknown25Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading SpawnWeightIncrease
            (var spawnweightincreaseLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading SpawnWeightHardmode
            (var spawnweighthardmodeLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            if (offset != expectedOffset)
            {
                throw new NotImplementedException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new StrongboxesDat()
            {
                ChestsKey = chestskeyLoading,
                SpawnWeight = spawnweightLoading,
                Unknown20 = unknown20Loading,
                IsCartographerBox = iscartographerboxLoading,
                Unknown25 = unknown25Loading,
                SpawnWeightIncrease = spawnweightincreaseLoading,
                SpawnWeightHardmode = spawnweighthardmodeLoading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
