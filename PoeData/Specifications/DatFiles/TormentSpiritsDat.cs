// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.DatFiles;

/// <summary>
/// Class containing TormentSpirits.dat data.
/// </summary>
public sealed partial class TormentSpiritsDat
{
    /// <summary> Gets MonsterVarietiesKey.</summary>
    /// <remarks> references <see cref="MonsterVarietiesDat"/> on <see cref="Specification.GetMonsterVarietiesDat"/> index.</remarks>
    public required int? MonsterVarietiesKey { get; init; }

    /// <summary> Gets Spirit_ModsKeys.</summary>
    /// <remarks> references <see cref="ModsDat"/> on <see cref="Specification.GetModsDat"/> index.</remarks>
    public required ReadOnlyCollection<int> Spirit_ModsKeys { get; init; }

    /// <summary> Gets Touched_ModsKeys.</summary>
    /// <remarks> references <see cref="ModsDat"/> on <see cref="Specification.GetModsDat"/> index.</remarks>
    public required ReadOnlyCollection<int> Touched_ModsKeys { get; init; }

    /// <summary> Gets Possessed_ModsKeys.</summary>
    /// <remarks> references <see cref="ModsDat"/> on <see cref="Specification.GetModsDat"/> index.</remarks>
    public required ReadOnlyCollection<int> Possessed_ModsKeys { get; init; }

    /// <summary> Gets MinZoneLevel.</summary>
    public required int MinZoneLevel { get; init; }

    /// <summary> Gets MaxZoneLevel.</summary>
    public required int MaxZoneLevel { get; init; }

    /// <summary> Gets SpawnWeight.</summary>
    public required int SpawnWeight { get; init; }

    /// <summary> Gets SummonedMonster_MonsterVarietiesKey.</summary>
    /// <remarks> references <see cref="MonsterVarietiesDat"/> on <see cref="Specification.GetMonsterVarietiesDat"/> index.</remarks>
    public required int? SummonedMonster_MonsterVarietiesKey { get; init; }

    /// <summary> Gets Unknown92.</summary>
    public required int Unknown92 { get; init; }

    /// <summary> Gets ModsKeys0.</summary>
    /// <remarks> references <see cref="ModsDat"/> on <see cref="Specification.GetModsDat"/> index.</remarks>
    public required ReadOnlyCollection<int> ModsKeys0 { get; init; }

    /// <summary> Gets ModsKeys1.</summary>
    /// <remarks> references <see cref="ModsDat"/> on <see cref="Specification.GetModsDat"/> index.</remarks>
    public required ReadOnlyCollection<int> ModsKeys1 { get; init; }

    /// <summary>
    /// Gets TormentSpiritsDat data.
    /// </summary>
    /// <param name="dataLoader">data loader.</param>
    /// <returns>array of TormentSpiritsDat.</returns>
    internal static TormentSpiritsDat[] Load(DataLoader dataLoader)
    {
        if (dataLoader is null)
        {
            throw new ArgumentNullException(nameof(dataLoader));
        }

        const string filePath = "Data/TormentSpirits.dat64";
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new TormentSpiritsDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading MonsterVarietiesKey
            (var monstervarietieskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading Spirit_ModsKeys
            (var tempspirit_modskeysLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var spirit_modskeysLoading = tempspirit_modskeysLoading.AsReadOnly();

            // loading Touched_ModsKeys
            (var temptouched_modskeysLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var touched_modskeysLoading = temptouched_modskeysLoading.AsReadOnly();

            // loading Possessed_ModsKeys
            (var temppossessed_modskeysLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var possessed_modskeysLoading = temppossessed_modskeysLoading.AsReadOnly();

            // loading MinZoneLevel
            (var minzonelevelLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading MaxZoneLevel
            (var maxzonelevelLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading SpawnWeight
            (var spawnweightLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading SummonedMonster_MonsterVarietiesKey
            (var summonedmonster_monstervarietieskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading Unknown92
            (var unknown92Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading ModsKeys0
            (var tempmodskeys0Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var modskeys0Loading = tempmodskeys0Loading.AsReadOnly();

            // loading ModsKeys1
            (var tempmodskeys1Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var modskeys1Loading = tempmodskeys1Loading.AsReadOnly();

            if (offset != expectedOffset)
            {
                throw new NotImplementedException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new TormentSpiritsDat()
            {
                MonsterVarietiesKey = monstervarietieskeyLoading,
                Spirit_ModsKeys = spirit_modskeysLoading,
                Touched_ModsKeys = touched_modskeysLoading,
                Possessed_ModsKeys = possessed_modskeysLoading,
                MinZoneLevel = minzonelevelLoading,
                MaxZoneLevel = maxzonelevelLoading,
                SpawnWeight = spawnweightLoading,
                SummonedMonster_MonsterVarietiesKey = summonedmonster_monstervarietieskeyLoading,
                Unknown92 = unknown92Loading,
                ModsKeys0 = modskeys0Loading,
                ModsKeys1 = modskeys1Loading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
