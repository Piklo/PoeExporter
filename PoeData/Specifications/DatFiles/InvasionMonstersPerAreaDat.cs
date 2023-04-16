// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.DatFiles;

/// <summary>
/// Class containing InvasionMonstersPerArea.dat data.
/// </summary>
public sealed partial class InvasionMonstersPerAreaDat
{
    /// <summary> Gets WorldAreasKey.</summary>
    /// <remarks> references <see cref="WorldAreasDat"/> on <see cref="Specification.GetWorldAreasDat"/> index.</remarks>
    public required int? WorldAreasKey { get; init; }

    /// <summary> Gets Unknown16.</summary>
    public required int Unknown16 { get; init; }

    /// <summary> Gets Unknown20.</summary>
    public required ReadOnlyCollection<int> Unknown20 { get; init; }

    /// <summary> Gets MonsterVarietiesKeys1.</summary>
    /// <remarks> references <see cref="MonsterVarietiesDat"/> on <see cref="Specification.GetMonsterVarietiesDat"/> index.</remarks>
    public required ReadOnlyCollection<int> MonsterVarietiesKeys1 { get; init; }

    /// <summary> Gets MonsterVarietiesKeys2.</summary>
    /// <remarks> references <see cref="MonsterVarietiesDat"/> on <see cref="Specification.GetMonsterVarietiesDat"/> index.</remarks>
    public required ReadOnlyCollection<int> MonsterVarietiesKeys2 { get; init; }

    /// <summary> Gets Unknown68.</summary>
    public required int Unknown68 { get; init; }

    /// <summary> Gets Unknown72.</summary>
    public required int Unknown72 { get; init; }

    /// <summary> Gets Unknown76.</summary>
    public required int Unknown76 { get; init; }

    /// <summary> Gets Unknown80.</summary>
    public required int Unknown80 { get; init; }

    /// <summary>
    /// Gets InvasionMonstersPerAreaDat data.
    /// </summary>
    /// <param name="dataLoader">data loader.</param>
    /// <returns>array of InvasionMonstersPerAreaDat.</returns>
    internal static InvasionMonstersPerAreaDat[] Load(DataLoader dataLoader)
    {
        if (dataLoader is null)
        {
            throw new ArgumentNullException(nameof(dataLoader));
        }

        const string filePath = "Data/InvasionMonstersPerArea.dat64";
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new InvasionMonstersPerAreaDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading WorldAreasKey
            (var worldareaskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading Unknown16
            (var unknown16Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown20
            (var tempunknown20Loading, offset) = SpecificationFileLoader.LoadIntArray(decompressedFile, offset, dataOffset);
            var unknown20Loading = tempunknown20Loading.AsReadOnly();

            // loading MonsterVarietiesKeys1
            (var tempmonstervarietieskeys1Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var monstervarietieskeys1Loading = tempmonstervarietieskeys1Loading.AsReadOnly();

            // loading MonsterVarietiesKeys2
            (var tempmonstervarietieskeys2Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var monstervarietieskeys2Loading = tempmonstervarietieskeys2Loading.AsReadOnly();

            // loading Unknown68
            (var unknown68Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown72
            (var unknown72Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown76
            (var unknown76Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown80
            (var unknown80Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            if (offset != expectedOffset)
            {
                throw new SchemaMismatchException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new InvasionMonstersPerAreaDat()
            {
                WorldAreasKey = worldareaskeyLoading,
                Unknown16 = unknown16Loading,
                Unknown20 = unknown20Loading,
                MonsterVarietiesKeys1 = monstervarietieskeys1Loading,
                MonsterVarietiesKeys2 = monstervarietieskeys2Loading,
                Unknown68 = unknown68Loading,
                Unknown72 = unknown72Loading,
                Unknown76 = unknown76Loading,
                Unknown80 = unknown80Loading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
