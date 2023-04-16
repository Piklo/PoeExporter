// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.DatFiles;

/// <summary>
/// Class containing LakeRooms.dat data.
/// </summary>
public sealed partial class LakeRoomsDat
{
    /// <summary> Gets Id.</summary>
    public required string Id { get; init; }

    /// <summary> Gets ARMFiles.</summary>
    public required ReadOnlyCollection<string> ARMFiles { get; init; }

    /// <summary> Gets Script.</summary>
    public required string Script { get; init; }

    /// <summary> Gets Unknown32.</summary>
    public required ReadOnlyCollection<int> Unknown32 { get; init; }

    /// <summary> Gets Stats.</summary>
    /// <remarks> references <see cref="StatsDat"/> on <see cref="Specification.LoadStatsDat"/> index.</remarks>
    public required ReadOnlyCollection<int> Stats { get; init; }

    /// <summary> Gets StatsValues.</summary>
    public required ReadOnlyCollection<int> StatsValues { get; init; }

    /// <summary> Gets Description.</summary>
    public required string Description { get; init; }

    /// <summary> Gets Name.</summary>
    public required string Name { get; init; }

    /// <summary> Gets Type.</summary>
    public required int Type { get; init; }

    /// <summary> Gets WorldArea.</summary>
    /// <remarks> references <see cref="WorldAreasDat"/> on <see cref="Specification.LoadWorldAreasDat"/> index.</remarks>
    public required int? WorldArea { get; init; }

    /// <summary> Gets Icon.</summary>
    public required string Icon { get; init; }

    /// <summary> Gets Unknown124.</summary>
    public required int Unknown124 { get; init; }

    /// <summary> Gets MinLevel.</summary>
    public required int MinLevel { get; init; }

    /// <summary> Gets Unknown132.</summary>
    public required int? Unknown132 { get; init; }

    /// <summary> Gets a value indicating whether Unknown148 is set.</summary>
    public required bool Unknown148 { get; init; }

    /// <summary> Gets ExtraStats.</summary>
    /// <remarks> references <see cref="StatsDat"/> on <see cref="Specification.LoadStatsDat"/> index.</remarks>
    public required ReadOnlyCollection<int> ExtraStats { get; init; }

    /// <summary> Gets ExtraStatsValues.</summary>
    public required ReadOnlyCollection<int> ExtraStatsValues { get; init; }

    /// <summary> Gets ReminderText.</summary>
    public required string ReminderText { get; init; }

    /// <summary> Gets TextAudio.</summary>
    /// <remarks> references <see cref="NPCTextAudioDat"/> on <see cref="Specification.LoadNPCTextAudioDat"/> index.</remarks>
    public required int? TextAudio { get; init; }

    /// <summary>
    /// Gets LakeRoomsDat data.
    /// </summary>
    /// <param name="dataLoader">data loader.</param>
    /// <returns>array of LakeRoomsDat.</returns>
    internal static LakeRoomsDat[] Load(DataLoader dataLoader)
    {
        if (dataLoader is null)
        {
            throw new ArgumentNullException(nameof(dataLoader));
        }

        const string filePath = "Data/LakeRooms.dat64";
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new LakeRoomsDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading Id
            (var idLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading ARMFiles
            (var temparmfilesLoading, offset) = SpecificationFileLoader.LoadStringArray(decompressedFile, offset, dataOffset);
            var armfilesLoading = temparmfilesLoading.AsReadOnly();

            // loading Script
            (var scriptLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Unknown32
            (var tempunknown32Loading, offset) = SpecificationFileLoader.LoadIntArray(decompressedFile, offset, dataOffset);
            var unknown32Loading = tempunknown32Loading.AsReadOnly();

            // loading Stats
            (var tempstatsLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var statsLoading = tempstatsLoading.AsReadOnly();

            // loading StatsValues
            (var tempstatsvaluesLoading, offset) = SpecificationFileLoader.LoadIntArray(decompressedFile, offset, dataOffset);
            var statsvaluesLoading = tempstatsvaluesLoading.AsReadOnly();

            // loading Description
            (var descriptionLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Name
            (var nameLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Type
            (var typeLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading WorldArea
            (var worldareaLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading Icon
            (var iconLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Unknown124
            (var unknown124Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading MinLevel
            (var minlevelLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown132
            (var unknown132Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading Unknown148
            (var unknown148Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading ExtraStats
            (var tempextrastatsLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var extrastatsLoading = tempextrastatsLoading.AsReadOnly();

            // loading ExtraStatsValues
            (var tempextrastatsvaluesLoading, offset) = SpecificationFileLoader.LoadIntArray(decompressedFile, offset, dataOffset);
            var extrastatsvaluesLoading = tempextrastatsvaluesLoading.AsReadOnly();

            // loading ReminderText
            (var remindertextLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading TextAudio
            (var textaudioLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            if (offset != expectedOffset)
            {
                throw new SchemaMismatchException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new LakeRoomsDat()
            {
                Id = idLoading,
                ARMFiles = armfilesLoading,
                Script = scriptLoading,
                Unknown32 = unknown32Loading,
                Stats = statsLoading,
                StatsValues = statsvaluesLoading,
                Description = descriptionLoading,
                Name = nameLoading,
                Type = typeLoading,
                WorldArea = worldareaLoading,
                Icon = iconLoading,
                Unknown124 = unknown124Loading,
                MinLevel = minlevelLoading,
                Unknown132 = unknown132Loading,
                Unknown148 = unknown148Loading,
                ExtraStats = extrastatsLoading,
                ExtraStatsValues = extrastatsvaluesLoading,
                ReminderText = remindertextLoading,
                TextAudio = textaudioLoading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
