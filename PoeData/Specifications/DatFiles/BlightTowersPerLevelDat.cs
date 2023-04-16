// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.DatFiles;

/// <summary>
/// Class containing BlightTowersPerLevel.dat data.
/// </summary>
public sealed partial class BlightTowersPerLevelDat
{
    /// <summary> Gets BlightTowersKey.</summary>
    /// <remarks> references <see cref="BlightTowersDat"/> on <see cref="Specification.GetBlightTowersDat"/> index.</remarks>
    public required int? BlightTowersKey { get; init; }

    /// <summary> Gets Unknown16.</summary>
    public required int Unknown16 { get; init; }

    /// <summary> Gets MonsterVarietiesKey.</summary>
    /// <remarks> references <see cref="MonsterVarietiesDat"/> on <see cref="Specification.GetMonsterVarietiesDat"/> index.</remarks>
    public required int? MonsterVarietiesKey { get; init; }

    /// <summary> Gets Cost.</summary>
    public required int Cost { get; init; }

    /// <summary> Gets Unknown40.</summary>
    public required int Unknown40 { get; init; }

    /// <summary>
    /// Gets BlightTowersPerLevelDat data.
    /// </summary>
    /// <param name="dataLoader">data loader.</param>
    /// <returns>array of BlightTowersPerLevelDat.</returns>
    internal static BlightTowersPerLevelDat[] Load(DataLoader dataLoader)
    {
        if (dataLoader is null)
        {
            throw new ArgumentNullException(nameof(dataLoader));
        }

        const string filePath = "Data/BlightTowersPerLevel.dat64";
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new BlightTowersPerLevelDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading BlightTowersKey
            (var blighttowerskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading Unknown16
            (var unknown16Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading MonsterVarietiesKey
            (var monstervarietieskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading Cost
            (var costLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown40
            (var unknown40Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            if (offset != expectedOffset)
            {
                throw new SchemaMismatchException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new BlightTowersPerLevelDat()
            {
                BlightTowersKey = blighttowerskeyLoading,
                Unknown16 = unknown16Loading,
                MonsterVarietiesKey = monstervarietieskeyLoading,
                Cost = costLoading,
                Unknown40 = unknown40Loading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
