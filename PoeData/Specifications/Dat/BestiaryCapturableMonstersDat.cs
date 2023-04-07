// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.Dat;

/// <summary>
/// Class containing BestiaryCapturableMonsters.dat data.
/// </summary>
public sealed partial class BestiaryCapturableMonstersDat
{
    /// <summary> Gets MonsterVarietiesKey.</summary>
    /// <remarks> references <see cref="MonsterVarietiesDat"/> on <see cref="Specification.GetMonsterVarietiesDat"/> index.</remarks>
    public required int? MonsterVarietiesKey { get; init; }

    /// <summary> Gets BestiaryGroupsKey.</summary>
    /// <remarks> references <see cref="BestiaryGroupsDat"/> on <see cref="Specification.GetBestiaryGroupsDat"/> index.</remarks>
    public required int? BestiaryGroupsKey { get; init; }

    /// <summary> Gets Name.</summary>
    public required string Name { get; init; }

    /// <summary> Gets BestiaryEncountersKey.</summary>
    /// <remarks> references <see cref="BestiaryEncountersDat"/> on <see cref="Specification.GetBestiaryEncountersDat"/> index.</remarks>
    public required int? BestiaryEncountersKey { get; init; }

    /// <summary> Gets a value indicating whether Unknown56 is set.</summary>
    public required bool Unknown56 { get; init; }

    /// <summary> Gets IconSmall.</summary>
    public required string IconSmall { get; init; }

    /// <summary> Gets Icon.</summary>
    public required string Icon { get; init; }

    /// <summary> Gets Boss_MonsterVarietiesKey.</summary>
    /// <remarks> references <see cref="MonsterVarietiesDat"/> on <see cref="Specification.GetMonsterVarietiesDat"/> index.</remarks>
    public required int? Boss_MonsterVarietiesKey { get; init; }

    /// <summary> Gets BestiaryGenusKey.</summary>
    /// <remarks> references <see cref="BestiaryGenusDat"/> on <see cref="Specification.GetBestiaryGenusDat"/> index.</remarks>
    public required int? BestiaryGenusKey { get; init; }

    /// <summary> Gets a value indicating whether Unknown105 is set.</summary>
    public required bool Unknown105 { get; init; }

    /// <summary> Gets BestiaryCapturableMonstersKey.</summary>
    /// <remarks> references <see cref="BestiaryCapturableMonstersDat"/> on <see cref="Specification.GetBestiaryCapturableMonstersDat"/> index.</remarks>
    public required int? BestiaryCapturableMonstersKey { get; init; }

    /// <summary> Gets a value indicating whether IsDisabled is set.</summary>
    public required bool IsDisabled { get; init; }

    /// <summary> Gets Unknown115.</summary>
    public required int Unknown115 { get; init; }

    /// <summary> Gets a value indicating whether Unknown119 is set.</summary>
    public required bool Unknown119 { get; init; }

    /// <summary>
    /// Gets BestiaryCapturableMonstersDat data.
    /// </summary>
    /// <param name="dataLoader">data loader.</param>
    /// <returns>array of BestiaryCapturableMonstersDat.</returns>
    internal static BestiaryCapturableMonstersDat[] Load(DataLoader dataLoader)
    {
        if (dataLoader is null)
        {
            throw new ArgumentNullException(nameof(dataLoader));
        }

        const string filePath = "Data/BestiaryCapturableMonsters.dat64";
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new BestiaryCapturableMonstersDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading MonsterVarietiesKey
            (var monstervarietieskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading BestiaryGroupsKey
            (var bestiarygroupskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading Name
            (var nameLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading BestiaryEncountersKey
            (var bestiaryencounterskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading Unknown56
            (var unknown56Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading IconSmall
            (var iconsmallLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Icon
            (var iconLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Boss_MonsterVarietiesKey
            (var boss_monstervarietieskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading BestiaryGenusKey
            (var bestiarygenuskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading Unknown105
            (var unknown105Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading BestiaryCapturableMonstersKey
            (var bestiarycapturablemonsterskeyLoading, offset) = SpecificationFileLoader.LoadRowPrimaryKey(decompressedFile, offset);

            // loading IsDisabled
            (var isdisabledLoading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown115
            (var unknown115Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown119
            (var unknown119Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            if (offset != expectedOffset)
            {
                throw new NotImplementedException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new BestiaryCapturableMonstersDat()
            {
                MonsterVarietiesKey = monstervarietieskeyLoading,
                BestiaryGroupsKey = bestiarygroupskeyLoading,
                Name = nameLoading,
                BestiaryEncountersKey = bestiaryencounterskeyLoading,
                Unknown56 = unknown56Loading,
                IconSmall = iconsmallLoading,
                Icon = iconLoading,
                Boss_MonsterVarietiesKey = boss_monstervarietieskeyLoading,
                BestiaryGenusKey = bestiarygenuskeyLoading,
                Unknown105 = unknown105Loading,
                BestiaryCapturableMonstersKey = bestiarycapturablemonsterskeyLoading,
                IsDisabled = isdisabledLoading,
                Unknown115 = unknown115Loading,
                Unknown119 = unknown119Loading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
