// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.Dat;

/// <summary>
/// Class containing AtlasPrimordialBossInfluence.dat data.
/// </summary>
public sealed partial class AtlasPrimordialBossInfluenceDat
{
    /// <summary> Gets Boss.</summary>
    /// <remarks> references <see cref="AtlasPrimordialBossesDat"/> on <see cref="Specification.GetAtlasPrimordialBossesDat"/> index.</remarks>
    public required int? Boss { get; init; }

    /// <summary> Gets Progress.</summary>
    public required int Progress { get; init; }

    /// <summary> Gets MinMapTier.</summary>
    public required int MinMapTier { get; init; }

    /// <summary> Gets Unknown24.</summary>
    public required int Unknown24 { get; init; }

    /// <summary> Gets Unknown28.</summary>
    public required int Unknown28 { get; init; }

    /// <summary> Gets Unknown32.</summary>
    public required int? Unknown32 { get; init; }

    /// <summary> Gets Unknown48.</summary>
    public required float Unknown48 { get; init; }

    /// <summary> Gets Unknown52.</summary>
    /// <remarks> references <see cref="QuestFlagsDat"/> on <see cref="Specification.GetQuestFlagsDat"/> index.</remarks>
    public required int? Unknown52 { get; init; }

    /// <inheritdoc/>
    public static AtlasPrimordialBossInfluenceDat[] Load(Specification specification)
    {
        if (specification is null)
        {
            throw new ArgumentNullException(nameof(specification));
        }

        const string filePath = "Data/AtlasPrimordialBossInfluence.dat64";
        var decompressedFile = specification.DataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new AtlasPrimordialBossInfluenceDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading Boss
            (var bossLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading Progress
            (var progressLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading MinMapTier
            (var minmaptierLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown24
            (var unknown24Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown28
            (var unknown28Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown32
            (var unknown32Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading Unknown48
            (var unknown48Loading, offset) = SpecificationFileLoader.LoadFloat(decompressedFile, offset);

            // loading Unknown52
            (var unknown52Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            if (offset != expectedOffset)
            {
                throw new NotImplementedException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new AtlasPrimordialBossInfluenceDat()
            {
                Boss = bossLoading,
                Progress = progressLoading,
                MinMapTier = minmaptierLoading,
                Unknown24 = unknown24Loading,
                Unknown28 = unknown28Loading,
                Unknown32 = unknown32Loading,
                Unknown48 = unknown48Loading,
                Unknown52 = unknown52Loading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
