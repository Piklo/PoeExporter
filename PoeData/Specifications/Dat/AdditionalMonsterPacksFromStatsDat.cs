// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.Dat;

/// <summary>
/// Class containing AdditionalMonsterPacksFromStats.dat data.
/// </summary>
public sealed partial class AdditionalMonsterPacksFromStatsDat : IDat<AdditionalMonsterPacksFromStatsDat>
{
    /// <summary> Gets StatsKey.</summary>
    /// <remarks> references <see cref="StatsDat"/> on <see cref="Specification.GetStatsDat"/> index.</remarks>
    public required int? StatsKey { get; init; }

    /// <summary> Gets Unknown16.</summary>
    public required int Unknown16 { get; init; }

    /// <summary> Gets MonsterPacksKeys.</summary>
    /// <remarks> references <see cref="MonsterPacksDat"/> on <see cref="Specification.GetMonsterPacksDat"/> index.</remarks>
    public required ReadOnlyCollection<int> MonsterPacksKeys { get; init; }

    /// <summary> Gets AdditionalMonsterPacksStatMode.</summary>
    public required int AdditionalMonsterPacksStatMode { get; init; }

    /// <summary> Gets PackCountStatsKey.</summary>
    /// <remarks> references <see cref="StatsDat"/> on <see cref="Specification.GetStatsDat"/> index.</remarks>
    public required int? PackCountStatsKey { get; init; }

    /// <summary> Gets StatsKeys.</summary>
    /// <remarks> references <see cref="StatsDat"/> on <see cref="Specification.GetStatsDat"/> index.</remarks>
    public required ReadOnlyCollection<int> StatsKeys { get; init; }

    /// <summary> Gets StatsValues.</summary>
    public required ReadOnlyCollection<int> StatsValues { get; init; }

    /// <summary> Gets Unknown88.</summary>
    public required int Unknown88 { get; init; }

    /// <inheritdoc/>
    public static AdditionalMonsterPacksFromStatsDat[] Load(Specification specification)
    {
        if (specification is null)
        {
            throw new ArgumentNullException(nameof(specification));
        }

        const string filePath = "Data/AdditionalMonsterPacksFromStats.dat64";
        var decompressedFile = specification.DataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new AdditionalMonsterPacksFromStatsDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading StatsKey
            (var statskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading Unknown16
            (var unknown16Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading MonsterPacksKeys
            (var tempmonsterpackskeysLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var monsterpackskeysLoading = tempmonsterpackskeysLoading.AsReadOnly();

            // loading AdditionalMonsterPacksStatMode
            (var additionalmonsterpacksstatmodeLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading PackCountStatsKey
            (var packcountstatskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading StatsKeys
            (var tempstatskeysLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var statskeysLoading = tempstatskeysLoading.AsReadOnly();

            // loading StatsValues
            (var tempstatsvaluesLoading, offset) = SpecificationFileLoader.LoadIntArray(decompressedFile, offset, dataOffset);
            var statsvaluesLoading = tempstatsvaluesLoading.AsReadOnly();

            // loading Unknown88
            (var unknown88Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            if (offset != expectedOffset)
            {
                throw new NotImplementedException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new AdditionalMonsterPacksFromStatsDat()
            {
                StatsKey = statskeyLoading,
                Unknown16 = unknown16Loading,
                MonsterPacksKeys = monsterpackskeysLoading,
                AdditionalMonsterPacksStatMode = additionalmonsterpacksstatmodeLoading,
                PackCountStatsKey = packcountstatskeyLoading,
                StatsKeys = statskeysLoading,
                StatsValues = statsvaluesLoading,
                Unknown88 = unknown88Loading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
