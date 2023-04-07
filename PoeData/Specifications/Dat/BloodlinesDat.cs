// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.Dat;

/// <summary>
/// Class containing Bloodlines.dat data.
/// </summary>
public sealed partial class BloodlinesDat : IDat<BloodlinesDat>
{
    /// <summary> Gets Id.</summary>
    public required string Id { get; init; }

    /// <summary> Gets ModsKeys.</summary>
    /// <remarks> references <see cref="ModsDat"/> on <see cref="Specification.GetModsDat"/> index.</remarks>
    public required ReadOnlyCollection<int> ModsKeys { get; init; }

    /// <summary> Gets MinZoneLevel.</summary>
    public required int MinZoneLevel { get; init; }

    /// <summary> Gets MaxZoneLevel.</summary>
    public required int MaxZoneLevel { get; init; }

    /// <summary> Gets SpawnWeight_TagsKeys.</summary>
    /// <remarks> references <see cref="TagsDat"/> on <see cref="Specification.GetTagsDat"/> index.</remarks>
    public required ReadOnlyCollection<int> SpawnWeight_TagsKeys { get; init; }

    /// <summary> Gets SpawnWeight_Values.</summary>
    public required ReadOnlyCollection<int> SpawnWeight_Values { get; init; }

    /// <summary> Gets Unknown64.</summary>
    public required int Unknown64 { get; init; }

    /// <summary> Gets BuffDefinitionsKey.</summary>
    /// <remarks> references <see cref="BuffDefinitionsDat"/> on <see cref="Specification.GetBuffDefinitionsDat"/> index.</remarks>
    public required int? BuffDefinitionsKey { get; init; }

    /// <summary> Gets Unknown84.</summary>
    public required ReadOnlyCollection<int> Unknown84 { get; init; }

    /// <summary> Gets ItemWeight_TagsKeys.</summary>
    /// <remarks> references <see cref="TagsDat"/> on <see cref="Specification.GetTagsDat"/> index.</remarks>
    public required ReadOnlyCollection<int> ItemWeight_TagsKeys { get; init; }

    /// <summary> Gets ItemWeight_Values.</summary>
    public required ReadOnlyCollection<int> ItemWeight_Values { get; init; }

    /// <summary> Gets MonsterVarietiesKey.</summary>
    /// <remarks> references <see cref="MonsterVarietiesDat"/> on <see cref="Specification.GetMonsterVarietiesDat"/> index.</remarks>
    public required int? MonsterVarietiesKey { get; init; }

    /// <summary> Gets Unknown148.</summary>
    public required int Unknown148 { get; init; }

    /// <summary> Gets a value indicating whether Unknown152 is set.</summary>
    public required bool Unknown152 { get; init; }

    /// <summary> Gets AchievementItemsKey.</summary>
    /// <remarks> references <see cref="AchievementItemsDat"/> on <see cref="Specification.GetAchievementItemsDat"/> index.</remarks>
    public required ReadOnlyCollection<int> AchievementItemsKey { get; init; }

    /// <summary> Gets a value indicating whether Unknown169 is set.</summary>
    public required bool Unknown169 { get; init; }

    /// <inheritdoc/>
    public static BloodlinesDat[] Load(Specification specification)
    {
        if (specification is null)
        {
            throw new ArgumentNullException(nameof(specification));
        }

        const string filePath = "Data/Bloodlines.dat64";
        var decompressedFile = specification.DataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new BloodlinesDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading Id
            (var idLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading ModsKeys
            (var tempmodskeysLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var modskeysLoading = tempmodskeysLoading.AsReadOnly();

            // loading MinZoneLevel
            (var minzonelevelLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading MaxZoneLevel
            (var maxzonelevelLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading SpawnWeight_TagsKeys
            (var tempspawnweight_tagskeysLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var spawnweight_tagskeysLoading = tempspawnweight_tagskeysLoading.AsReadOnly();

            // loading SpawnWeight_Values
            (var tempspawnweight_valuesLoading, offset) = SpecificationFileLoader.LoadIntArray(decompressedFile, offset, dataOffset);
            var spawnweight_valuesLoading = tempspawnweight_valuesLoading.AsReadOnly();

            // loading Unknown64
            (var unknown64Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading BuffDefinitionsKey
            (var buffdefinitionskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading Unknown84
            (var tempunknown84Loading, offset) = SpecificationFileLoader.LoadIntArray(decompressedFile, offset, dataOffset);
            var unknown84Loading = tempunknown84Loading.AsReadOnly();

            // loading ItemWeight_TagsKeys
            (var tempitemweight_tagskeysLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var itemweight_tagskeysLoading = tempitemweight_tagskeysLoading.AsReadOnly();

            // loading ItemWeight_Values
            (var tempitemweight_valuesLoading, offset) = SpecificationFileLoader.LoadIntArray(decompressedFile, offset, dataOffset);
            var itemweight_valuesLoading = tempitemweight_valuesLoading.AsReadOnly();

            // loading MonsterVarietiesKey
            (var monstervarietieskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading Unknown148
            (var unknown148Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown152
            (var unknown152Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading AchievementItemsKey
            (var tempachievementitemskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var achievementitemskeyLoading = tempachievementitemskeyLoading.AsReadOnly();

            // loading Unknown169
            (var unknown169Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            if (offset != expectedOffset)
            {
                throw new NotImplementedException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new BloodlinesDat()
            {
                Id = idLoading,
                ModsKeys = modskeysLoading,
                MinZoneLevel = minzonelevelLoading,
                MaxZoneLevel = maxzonelevelLoading,
                SpawnWeight_TagsKeys = spawnweight_tagskeysLoading,
                SpawnWeight_Values = spawnweight_valuesLoading,
                Unknown64 = unknown64Loading,
                BuffDefinitionsKey = buffdefinitionskeyLoading,
                Unknown84 = unknown84Loading,
                ItemWeight_TagsKeys = itemweight_tagskeysLoading,
                ItemWeight_Values = itemweight_valuesLoading,
                MonsterVarietiesKey = monstervarietieskeyLoading,
                Unknown148 = unknown148Loading,
                Unknown152 = unknown152Loading,
                AchievementItemsKey = achievementitemskeyLoading,
                Unknown169 = unknown169Loading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
