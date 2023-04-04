// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.Dat;

/// <summary>
/// Class containing MonsterSpawnerGroupsPerLevel.dat data.
/// </summary>
public sealed partial class MonsterSpawnerGroupsPerLevelDat : ISpecificationFile<MonsterSpawnerGroupsPerLevelDat>
{
    /// <summary> Gets MonsterSpawnerGroupsKey.</summary>
    /// <remarks> references <see cref="MonsterSpawnerGroupsDat"/> on <see cref="Specification.GetMonsterSpawnerGroupsDat"/> index.</remarks>
    public required int? MonsterSpawnerGroupsKey { get; init; }

    /// <summary> Gets MinLevel.</summary>
    public required int MinLevel { get; init; }

    /// <summary> Gets Unknown20.</summary>
    public required int Unknown20 { get; init; }

    /// <summary> Gets Unknown24.</summary>
    public required int Unknown24 { get; init; }

    /// <summary> Gets Unknown28.</summary>
    public required int Unknown28 { get; init; }

    /// <inheritdoc/>
    public static MonsterSpawnerGroupsPerLevelDat[] Load(Specification specification)
    {
        if (specification is null)
        {
            throw new ArgumentNullException(nameof(specification));
        }

        var fileToFind = Encoding.ASCII.GetBytes("Data/MonsterSpawnerGroupsPerLevel.dat64");
        var fileRecord = specification.DataLoader.GetFileRecord(fileToFind);
        var decompressedFile = specification.DataLoader.GetFileBytes(fileRecord);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new MonsterSpawnerGroupsPerLevelDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading MonsterSpawnerGroupsKey
            (var monsterspawnergroupskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading MinLevel
            (var minlevelLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown20
            (var unknown20Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown24
            (var unknown24Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown28
            (var unknown28Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            if (offset != expectedOffset)
            {
                throw new NotImplementedException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new MonsterSpawnerGroupsPerLevelDat()
            {
                MonsterSpawnerGroupsKey = monsterspawnergroupskeyLoading,
                MinLevel = minlevelLoading,
                Unknown20 = unknown20Loading,
                Unknown24 = unknown24Loading,
                Unknown28 = unknown28Loading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
