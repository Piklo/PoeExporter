// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.DatFiles;

/// <summary>
/// Class containing MonsterPacks.dat data.
/// </summary>
public sealed partial class MonsterPacksDat
{
    /// <summary> Gets Id.</summary>
    public required string Id { get; init; }

    /// <summary> Gets WorldAreasKeys.</summary>
    /// <remarks> references <see cref="WorldAreasDat"/> on <see cref="Specification.GetWorldAreasDat"/> index.</remarks>
    public required ReadOnlyCollection<int> WorldAreasKeys { get; init; }

    /// <summary> Gets Unknown24.</summary>
    public required int Unknown24 { get; init; }

    /// <summary> Gets Unknown28.</summary>
    public required int Unknown28 { get; init; }

    /// <summary> Gets BossMonsterSpawnChance.</summary>
    public required int BossMonsterSpawnChance { get; init; }

    /// <summary> Gets BossMonsterCount.</summary>
    public required int BossMonsterCount { get; init; }

    /// <summary> Gets BossMonster_MonsterVarietiesKeys.</summary>
    /// <remarks> references <see cref="MonsterVarietiesDat"/> on <see cref="Specification.GetMonsterVarietiesDat"/> index.</remarks>
    public required ReadOnlyCollection<int> BossMonster_MonsterVarietiesKeys { get; init; }

    /// <summary> Gets a value indicating whether Unknown56 is set.</summary>
    public required bool Unknown56 { get; init; }

    /// <summary> Gets Unknown57.</summary>
    public required int Unknown57 { get; init; }

    /// <summary> Gets Unknown61.</summary>
    public required ReadOnlyCollection<string> Unknown61 { get; init; }

    /// <summary> Gets TagsKeys.</summary>
    /// <remarks> references <see cref="TagsDat"/> on <see cref="Specification.GetTagsDat"/> index.</remarks>
    public required ReadOnlyCollection<int> TagsKeys { get; init; }

    /// <summary> Gets MinLevel.</summary>
    public required int MinLevel { get; init; }

    /// <summary> Gets MaxLevel.</summary>
    public required int MaxLevel { get; init; }

    /// <summary> Gets WorldAreas2.</summary>
    /// <remarks> references <see cref="TagsDat"/> on <see cref="Specification.GetTagsDat"/> index.</remarks>
    public required ReadOnlyCollection<int> WorldAreas2 { get; init; }

    /// <summary> Gets Unknown117.</summary>
    public required int Unknown117 { get; init; }

    /// <summary> Gets PackFormation.</summary>
    /// <remarks> references <see cref="PackFormationDat"/> on <see cref="Specification.GetPackFormationDat"/> index.</remarks>
    public required int? PackFormation { get; init; }

    /// <summary> Gets Unknown137.</summary>
    public required int Unknown137 { get; init; }

    /// <summary> Gets a value indicating whether Unknown141 is set.</summary>
    public required bool Unknown141 { get; init; }

    /// <summary>
    /// Gets MonsterPacksDat data.
    /// </summary>
    /// <param name="dataLoader">data loader.</param>
    /// <returns>array of MonsterPacksDat.</returns>
    internal static MonsterPacksDat[] Load(DataLoader dataLoader)
    {
        if (dataLoader is null)
        {
            throw new ArgumentNullException(nameof(dataLoader));
        }

        const string filePath = "Data/MonsterPacks.dat64";
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new MonsterPacksDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading Id
            (var idLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading WorldAreasKeys
            (var tempworldareaskeysLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var worldareaskeysLoading = tempworldareaskeysLoading.AsReadOnly();

            // loading Unknown24
            (var unknown24Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown28
            (var unknown28Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading BossMonsterSpawnChance
            (var bossmonsterspawnchanceLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading BossMonsterCount
            (var bossmonstercountLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading BossMonster_MonsterVarietiesKeys
            (var tempbossmonster_monstervarietieskeysLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var bossmonster_monstervarietieskeysLoading = tempbossmonster_monstervarietieskeysLoading.AsReadOnly();

            // loading Unknown56
            (var unknown56Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown57
            (var unknown57Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown61
            (var tempunknown61Loading, offset) = SpecificationFileLoader.LoadStringArray(decompressedFile, offset, dataOffset);
            var unknown61Loading = tempunknown61Loading.AsReadOnly();

            // loading TagsKeys
            (var temptagskeysLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var tagskeysLoading = temptagskeysLoading.AsReadOnly();

            // loading MinLevel
            (var minlevelLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading MaxLevel
            (var maxlevelLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading WorldAreas2
            (var tempworldareas2Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var worldareas2Loading = tempworldareas2Loading.AsReadOnly();

            // loading Unknown117
            (var unknown117Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading PackFormation
            (var packformationLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading Unknown137
            (var unknown137Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown141
            (var unknown141Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            if (offset != expectedOffset)
            {
                throw new NotImplementedException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new MonsterPacksDat()
            {
                Id = idLoading,
                WorldAreasKeys = worldareaskeysLoading,
                Unknown24 = unknown24Loading,
                Unknown28 = unknown28Loading,
                BossMonsterSpawnChance = bossmonsterspawnchanceLoading,
                BossMonsterCount = bossmonstercountLoading,
                BossMonster_MonsterVarietiesKeys = bossmonster_monstervarietieskeysLoading,
                Unknown56 = unknown56Loading,
                Unknown57 = unknown57Loading,
                Unknown61 = unknown61Loading,
                TagsKeys = tagskeysLoading,
                MinLevel = minlevelLoading,
                MaxLevel = maxlevelLoading,
                WorldAreas2 = worldareas2Loading,
                Unknown117 = unknown117Loading,
                PackFormation = packformationLoading,
                Unknown137 = unknown137Loading,
                Unknown141 = unknown141Loading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
