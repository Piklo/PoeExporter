// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.Dat;

/// <summary>
/// Class containing LegionMonsterVarieties.dat data.
/// </summary>
public sealed partial class LegionMonsterVarietiesDat
{
    /// <summary> Gets MonsterVarietiesKey.</summary>
    /// <remarks> references <see cref="MonsterVarietiesDat"/> on <see cref="Specification.GetMonsterVarietiesDat"/> index.</remarks>
    public required int? MonsterVarietiesKey { get; init; }

    /// <summary> Gets LegionFactionsKey.</summary>
    /// <remarks> references <see cref="LegionFactionsDat"/> on <see cref="Specification.GetLegionFactionsDat"/> index.</remarks>
    public required int? LegionFactionsKey { get; init; }

    /// <summary> Gets LegionRanksKey.</summary>
    /// <remarks> references <see cref="LegionRanksDat"/> on <see cref="Specification.GetLegionRanksDat"/> index.</remarks>
    public required int? LegionRanksKey { get; init; }

    /// <summary> Gets Unknown48.</summary>
    public required int Unknown48 { get; init; }

    /// <summary> Gets MiscAnimatedKey.</summary>
    /// <remarks> references <see cref="MiscAnimatedDat"/> on <see cref="Specification.GetMiscAnimatedDat"/> index.</remarks>
    public required ReadOnlyCollection<int> MiscAnimatedKey { get; init; }

    /// <summary> Gets Unknown68.</summary>
    public required int Unknown68 { get; init; }

    /// <summary> Gets Unknown72.</summary>
    public required int Unknown72 { get; init; }

    /// <summary> Gets Unknown76.</summary>
    public required ReadOnlyCollection<int> Unknown76 { get; init; }

    /// <summary> Gets Unknown92.</summary>
    public required ReadOnlyCollection<int> Unknown92 { get; init; }

    /// <summary> Gets Unknown108.</summary>
    public required ReadOnlyCollection<int> Unknown108 { get; init; }

    /// <summary> Gets Unknown124.</summary>
    public required ReadOnlyCollection<int> Unknown124 { get; init; }

    /// <summary> Gets Unknown140.</summary>
    public required ReadOnlyCollection<int> Unknown140 { get; init; }

    /// <summary> Gets Unknown156.</summary>
    public required ReadOnlyCollection<int> Unknown156 { get; init; }

    /// <summary> Gets Unknown172.</summary>
    public required int Unknown172 { get; init; }

    /// <summary> Gets Unknown176.</summary>
    public required int Unknown176 { get; init; }

    /// <summary> Gets Unknown180.</summary>
    public required ReadOnlyCollection<int> Unknown180 { get; init; }

    /// <summary> Gets MonsterVarietiesKey2.</summary>
    /// <remarks> references <see cref="MonsterVarietiesDat"/> on <see cref="Specification.GetMonsterVarietiesDat"/> index.</remarks>
    public required int? MonsterVarietiesKey2 { get; init; }

    /// <inheritdoc/>
    public static LegionMonsterVarietiesDat[] Load(Specification specification)
    {
        if (specification is null)
        {
            throw new ArgumentNullException(nameof(specification));
        }

        const string filePath = "Data/LegionMonsterVarieties.dat64";
        var decompressedFile = specification.DataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new LegionMonsterVarietiesDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading MonsterVarietiesKey
            (var monstervarietieskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading LegionFactionsKey
            (var legionfactionskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading LegionRanksKey
            (var legionrankskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading Unknown48
            (var unknown48Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading MiscAnimatedKey
            (var tempmiscanimatedkeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var miscanimatedkeyLoading = tempmiscanimatedkeyLoading.AsReadOnly();

            // loading Unknown68
            (var unknown68Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown72
            (var unknown72Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown76
            (var tempunknown76Loading, offset) = SpecificationFileLoader.LoadIntArray(decompressedFile, offset, dataOffset);
            var unknown76Loading = tempunknown76Loading.AsReadOnly();

            // loading Unknown92
            (var tempunknown92Loading, offset) = SpecificationFileLoader.LoadIntArray(decompressedFile, offset, dataOffset);
            var unknown92Loading = tempunknown92Loading.AsReadOnly();

            // loading Unknown108
            (var tempunknown108Loading, offset) = SpecificationFileLoader.LoadIntArray(decompressedFile, offset, dataOffset);
            var unknown108Loading = tempunknown108Loading.AsReadOnly();

            // loading Unknown124
            (var tempunknown124Loading, offset) = SpecificationFileLoader.LoadIntArray(decompressedFile, offset, dataOffset);
            var unknown124Loading = tempunknown124Loading.AsReadOnly();

            // loading Unknown140
            (var tempunknown140Loading, offset) = SpecificationFileLoader.LoadIntArray(decompressedFile, offset, dataOffset);
            var unknown140Loading = tempunknown140Loading.AsReadOnly();

            // loading Unknown156
            (var tempunknown156Loading, offset) = SpecificationFileLoader.LoadIntArray(decompressedFile, offset, dataOffset);
            var unknown156Loading = tempunknown156Loading.AsReadOnly();

            // loading Unknown172
            (var unknown172Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown176
            (var unknown176Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown180
            (var tempunknown180Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var unknown180Loading = tempunknown180Loading.AsReadOnly();

            // loading MonsterVarietiesKey2
            (var monstervarietieskey2Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            if (offset != expectedOffset)
            {
                throw new NotImplementedException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new LegionMonsterVarietiesDat()
            {
                MonsterVarietiesKey = monstervarietieskeyLoading,
                LegionFactionsKey = legionfactionskeyLoading,
                LegionRanksKey = legionrankskeyLoading,
                Unknown48 = unknown48Loading,
                MiscAnimatedKey = miscanimatedkeyLoading,
                Unknown68 = unknown68Loading,
                Unknown72 = unknown72Loading,
                Unknown76 = unknown76Loading,
                Unknown92 = unknown92Loading,
                Unknown108 = unknown108Loading,
                Unknown124 = unknown124Loading,
                Unknown140 = unknown140Loading,
                Unknown156 = unknown156Loading,
                Unknown172 = unknown172Loading,
                Unknown176 = unknown176Loading,
                Unknown180 = unknown180Loading,
                MonsterVarietiesKey2 = monstervarietieskey2Loading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
