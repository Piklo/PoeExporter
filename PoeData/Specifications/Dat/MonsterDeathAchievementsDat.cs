// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.Dat;

/// <summary>
/// Class containing MonsterDeathAchievements.dat data.
/// </summary>
public sealed partial class MonsterDeathAchievementsDat : ISpecificationFile<MonsterDeathAchievementsDat>
{
    /// <summary> Gets Id.</summary>
    public required string Id { get; init; }

    /// <summary> Gets MonsterVarietiesKeys.</summary>
    public required ReadOnlyCollection<int> MonsterVarietiesKeys { get; init; }

    /// <summary> Gets AchievementItemsKeys.</summary>
    public required ReadOnlyCollection<int> AchievementItemsKeys { get; init; }

    /// <summary> Gets a value indicating whether Unknown40 is set.</summary>
    public required bool Unknown40 { get; init; }

    /// <summary> Gets PlayerConditionsKeys.</summary>
    public required ReadOnlyCollection<int> PlayerConditionsKeys { get; init; }

    /// <summary> Gets MonsterDeathConditionsKeys.</summary>
    public required ReadOnlyCollection<int> MonsterDeathConditionsKeys { get; init; }

    /// <summary> Gets Unknown73.</summary>
    public required ReadOnlyCollection<int> Unknown73 { get; init; }

    /// <summary> Gets Unknown89.</summary>
    public required int Unknown89 { get; init; }

    /// <summary> Gets Unknown93.</summary>
    public required int Unknown93 { get; init; }

    /// <summary> Gets a value indicating whether Unknown97 is set.</summary>
    public required bool Unknown97 { get; init; }

    /// <summary> Gets a value indicating whether Unknown98 is set.</summary>
    public required bool Unknown98 { get; init; }

    /// <summary> Gets Unknown99.</summary>
    public required int? Unknown99 { get; init; }

    /// <summary> Gets Unknown115.</summary>
    public required ReadOnlyCollection<int> Unknown115 { get; init; }

    /// <summary> Gets Unknown131.</summary>
    public required ReadOnlyCollection<int> Unknown131 { get; init; }

    /// <summary> Gets Unknown147.</summary>
    public required ReadOnlyCollection<int> Unknown147 { get; init; }

    /// <summary> Gets Unknown163.</summary>
    public required int? Unknown163 { get; init; }

    /// <summary> Gets Unknown179.</summary>
    public required int Unknown179 { get; init; }

    /// <summary> Gets NearbyMonsterConditionsKeys.</summary>
    public required ReadOnlyCollection<int> NearbyMonsterConditionsKeys { get; init; }

    /// <summary> Gets a value indicating whether Unknown199 is set.</summary>
    public required bool Unknown199 { get; init; }

    /// <summary> Gets MultiPartAchievementConditionsKeys.</summary>
    public required ReadOnlyCollection<int> MultiPartAchievementConditionsKeys { get; init; }

    /// <summary> Gets Unknown216.</summary>
    public required int Unknown216 { get; init; }

    /// <summary> Gets a value indicating whether Unknown220 is set.</summary>
    public required bool Unknown220 { get; init; }

    /// <inheritdoc/>
    public static MonsterDeathAchievementsDat[] Load(Specification specification)
    {
        if (specification is null)
        {
            throw new ArgumentNullException(nameof(specification));
        }

        var fileToFind = Encoding.ASCII.GetBytes("Data/MonsterDeathAchievements.dat64");
        var fileRecord = specification.DataLoader.GetFileRecord(fileToFind);
        var decompressedFile = specification.DataLoader.GetFileBytes(fileRecord);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new MonsterDeathAchievementsDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading referenced tables if any
            // specification.GetMonsterVarietiesDat();
            // specification.GetAchievementItemsDat();
            // specification.GetPlayerConditionsDat();
            // specification.GetMonsterDeathConditionsDat();
            // specification.GetNearbyMonsterConditionsDat();
            // specification.GetMultiPartAchievementConditionsDat();

            // loading Id
            (var idLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading MonsterVarietiesKeys
            (var tempmonstervarietieskeysLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var monstervarietieskeysLoading = tempmonstervarietieskeysLoading.AsReadOnly();

            // loading AchievementItemsKeys
            (var tempachievementitemskeysLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var achievementitemskeysLoading = tempachievementitemskeysLoading.AsReadOnly();

            // loading Unknown40
            (var unknown40Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading PlayerConditionsKeys
            (var tempplayerconditionskeysLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var playerconditionskeysLoading = tempplayerconditionskeysLoading.AsReadOnly();

            // loading MonsterDeathConditionsKeys
            (var tempmonsterdeathconditionskeysLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var monsterdeathconditionskeysLoading = tempmonsterdeathconditionskeysLoading.AsReadOnly();

            // loading Unknown73
            (var tempunknown73Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var unknown73Loading = tempunknown73Loading.AsReadOnly();

            // loading Unknown89
            (var unknown89Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown93
            (var unknown93Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown97
            (var unknown97Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown98
            (var unknown98Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown99
            (var unknown99Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading Unknown115
            (var tempunknown115Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var unknown115Loading = tempunknown115Loading.AsReadOnly();

            // loading Unknown131
            (var tempunknown131Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var unknown131Loading = tempunknown131Loading.AsReadOnly();

            // loading Unknown147
            (var tempunknown147Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var unknown147Loading = tempunknown147Loading.AsReadOnly();

            // loading Unknown163
            (var unknown163Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading Unknown179
            (var unknown179Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading NearbyMonsterConditionsKeys
            (var tempnearbymonsterconditionskeysLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var nearbymonsterconditionskeysLoading = tempnearbymonsterconditionskeysLoading.AsReadOnly();

            // loading Unknown199
            (var unknown199Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading MultiPartAchievementConditionsKeys
            (var tempmultipartachievementconditionskeysLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var multipartachievementconditionskeysLoading = tempmultipartachievementconditionskeysLoading.AsReadOnly();

            // loading Unknown216
            (var unknown216Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown220
            (var unknown220Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            if (offset != expectedOffset)
            {
                throw new NotImplementedException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new MonsterDeathAchievementsDat()
            {
                Id = idLoading,
                MonsterVarietiesKeys = monstervarietieskeysLoading,
                AchievementItemsKeys = achievementitemskeysLoading,
                Unknown40 = unknown40Loading,
                PlayerConditionsKeys = playerconditionskeysLoading,
                MonsterDeathConditionsKeys = monsterdeathconditionskeysLoading,
                Unknown73 = unknown73Loading,
                Unknown89 = unknown89Loading,
                Unknown93 = unknown93Loading,
                Unknown97 = unknown97Loading,
                Unknown98 = unknown98Loading,
                Unknown99 = unknown99Loading,
                Unknown115 = unknown115Loading,
                Unknown131 = unknown131Loading,
                Unknown147 = unknown147Loading,
                Unknown163 = unknown163Loading,
                Unknown179 = unknown179Loading,
                NearbyMonsterConditionsKeys = nearbymonsterconditionskeysLoading,
                Unknown199 = unknown199Loading,
                MultiPartAchievementConditionsKeys = multipartachievementconditionskeysLoading,
                Unknown216 = unknown216Loading,
                Unknown220 = unknown220Loading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
