// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.Dat;

/// <summary>
/// Class containing BestiaryEncounters.dat data.
/// </summary>
public sealed partial class BestiaryEncountersDat : ISpecificationFile<BestiaryEncountersDat>
{
    /// <summary> Gets Id.</summary>
    public required string Id { get; init; }

    /// <summary> Gets MonsterPacksKey.</summary>
    /// <remarks> references <see cref="MonsterPacksDat"/> on <see cref="Specification.GetMonsterPacksDat"/> index.</remarks>
    public required int? MonsterPacksKey { get; init; }

    /// <summary> Gets MonsterVarietiesKey.</summary>
    /// <remarks> references <see cref="MonsterVarietiesDat"/> on <see cref="Specification.GetMonsterVarietiesDat"/> index.</remarks>
    public required int? MonsterVarietiesKey { get; init; }

    /// <summary> Gets MonsterSpawnerId.</summary>
    public required string MonsterSpawnerId { get; init; }

    /// <inheritdoc/>
    public static BestiaryEncountersDat[] Load(Specification specification)
    {
        if (specification is null)
        {
            throw new ArgumentNullException(nameof(specification));
        }

        var fileToFind = Encoding.ASCII.GetBytes("Data/BestiaryEncounters.dat64");
        var fileRecord = specification.DataLoader.GetFileRecord(fileToFind);
        var decompressedFile = specification.DataLoader.GetFileBytes(fileRecord);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new BestiaryEncountersDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading Id
            (var idLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading MonsterPacksKey
            (var monsterpackskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading MonsterVarietiesKey
            (var monstervarietieskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading MonsterSpawnerId
            (var monsterspawneridLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            if (offset != expectedOffset)
            {
                throw new NotImplementedException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new BestiaryEncountersDat()
            {
                Id = idLoading,
                MonsterPacksKey = monsterpackskeyLoading,
                MonsterVarietiesKey = monstervarietieskeyLoading,
                MonsterSpawnerId = monsterspawneridLoading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
