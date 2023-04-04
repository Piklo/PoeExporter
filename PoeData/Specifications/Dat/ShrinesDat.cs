// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.Dat;

/// <summary>
/// Class containing Shrines.dat data.
/// </summary>
public sealed partial class ShrinesDat : IDat<ShrinesDat>
{
    /// <summary> Gets Id.</summary>
    public required string Id { get; init; }

    /// <summary> Gets TimeoutInSeconds.</summary>
    public required int TimeoutInSeconds { get; init; }

    /// <summary> Gets a value indicating whether ChargesShared is set.</summary>
    public required bool ChargesShared { get; init; }

    /// <summary> Gets Player_ShrineBuffsKey.</summary>
    /// <remarks> references <see cref="ShrineBuffsDat"/> on <see cref="Specification.GetShrineBuffsDat"/> index.</remarks>
    public required int? Player_ShrineBuffsKey { get; init; }

    /// <summary> Gets Unknown29.</summary>
    public required int Unknown29 { get; init; }

    /// <summary> Gets Unknown33.</summary>
    public required int Unknown33 { get; init; }

    /// <summary> Gets Monster_ShrineBuffsKey.</summary>
    /// <remarks> references <see cref="ShrineBuffsDat"/> on <see cref="Specification.GetShrineBuffsDat"/> index.</remarks>
    public required int? Monster_ShrineBuffsKey { get; init; }

    /// <summary> Gets SummonMonster_MonsterVarietiesKey.</summary>
    /// <remarks> references <see cref="MonsterVarietiesDat"/> on <see cref="Specification.GetMonsterVarietiesDat"/> index.</remarks>
    public required int? SummonMonster_MonsterVarietiesKey { get; init; }

    /// <summary> Gets SummonPlayer_MonsterVarietiesKey.</summary>
    /// <remarks> references <see cref="MonsterVarietiesDat"/> on <see cref="Specification.GetMonsterVarietiesDat"/> index.</remarks>
    public required int? SummonPlayer_MonsterVarietiesKey { get; init; }

    /// <summary> Gets Unknown85.</summary>
    public required int Unknown85 { get; init; }

    /// <summary> Gets Unknown89.</summary>
    public required int Unknown89 { get; init; }

    /// <summary> Gets ShrineSoundsKey.</summary>
    /// <remarks> references <see cref="ShrineSoundsDat"/> on <see cref="Specification.GetShrineSoundsDat"/> index.</remarks>
    public required int? ShrineSoundsKey { get; init; }

    /// <summary> Gets a value indicating whether Unknown109 is set.</summary>
    public required bool Unknown109 { get; init; }

    /// <summary> Gets AchievementItemsKeys.</summary>
    /// <remarks> references <see cref="AchievementItemsDat"/> on <see cref="Specification.GetAchievementItemsDat"/> index.</remarks>
    public required ReadOnlyCollection<int> AchievementItemsKeys { get; init; }

    /// <summary> Gets a value indicating whether IsPVPOnly is set.</summary>
    public required bool IsPVPOnly { get; init; }

    /// <summary> Gets a value indicating whether Unknown127 is set.</summary>
    public required bool Unknown127 { get; init; }

    /// <summary> Gets a value indicating whether IsLesserShrine is set.</summary>
    public required bool IsLesserShrine { get; init; }

    /// <summary> Gets Description.</summary>
    /// <remarks> references <see cref="ClientStringsDat"/> on <see cref="Specification.GetClientStringsDat"/> index.</remarks>
    public required int? Description { get; init; }

    /// <summary> Gets Name.</summary>
    /// <remarks> references <see cref="ClientStringsDat"/> on <see cref="Specification.GetClientStringsDat"/> index.</remarks>
    public required int? Name { get; init; }

    /// <summary> Gets a value indicating whether Unknown161 is set.</summary>
    public required bool Unknown161 { get; init; }

    /// <inheritdoc/>
    public static ShrinesDat[] Load(Specification specification)
    {
        if (specification is null)
        {
            throw new ArgumentNullException(nameof(specification));
        }

        var fileToFind = Encoding.ASCII.GetBytes("Data/Shrines.dat64");
        var fileRecord = specification.DataLoader.GetFileRecord(fileToFind);
        var decompressedFile = specification.DataLoader.GetFileBytes(fileRecord);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new ShrinesDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading Id
            (var idLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading TimeoutInSeconds
            (var timeoutinsecondsLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading ChargesShared
            (var chargessharedLoading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Player_ShrineBuffsKey
            (var player_shrinebuffskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading Unknown29
            (var unknown29Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown33
            (var unknown33Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Monster_ShrineBuffsKey
            (var monster_shrinebuffskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading SummonMonster_MonsterVarietiesKey
            (var summonmonster_monstervarietieskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading SummonPlayer_MonsterVarietiesKey
            (var summonplayer_monstervarietieskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading Unknown85
            (var unknown85Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown89
            (var unknown89Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading ShrineSoundsKey
            (var shrinesoundskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading Unknown109
            (var unknown109Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading AchievementItemsKeys
            (var tempachievementitemskeysLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var achievementitemskeysLoading = tempachievementitemskeysLoading.AsReadOnly();

            // loading IsPVPOnly
            (var ispvponlyLoading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown127
            (var unknown127Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading IsLesserShrine
            (var islessershrineLoading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Description
            (var descriptionLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading Name
            (var nameLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading Unknown161
            (var unknown161Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            if (offset != expectedOffset)
            {
                throw new NotImplementedException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new ShrinesDat()
            {
                Id = idLoading,
                TimeoutInSeconds = timeoutinsecondsLoading,
                ChargesShared = chargessharedLoading,
                Player_ShrineBuffsKey = player_shrinebuffskeyLoading,
                Unknown29 = unknown29Loading,
                Unknown33 = unknown33Loading,
                Monster_ShrineBuffsKey = monster_shrinebuffskeyLoading,
                SummonMonster_MonsterVarietiesKey = summonmonster_monstervarietieskeyLoading,
                SummonPlayer_MonsterVarietiesKey = summonplayer_monstervarietieskeyLoading,
                Unknown85 = unknown85Loading,
                Unknown89 = unknown89Loading,
                ShrineSoundsKey = shrinesoundskeyLoading,
                Unknown109 = unknown109Loading,
                AchievementItemsKeys = achievementitemskeysLoading,
                IsPVPOnly = ispvponlyLoading,
                Unknown127 = unknown127Loading,
                IsLesserShrine = islessershrineLoading,
                Description = descriptionLoading,
                Name = nameLoading,
                Unknown161 = unknown161Loading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
