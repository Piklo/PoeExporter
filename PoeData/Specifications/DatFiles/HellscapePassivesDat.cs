// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.DatFiles;

/// <summary>
/// Class containing HellscapePassives.dat data.
/// </summary>
public sealed partial class HellscapePassivesDat
{
    /// <summary> Gets Id.</summary>
    public required string Id { get; init; }

    /// <summary> Gets Name.</summary>
    public required string Name { get; init; }

    /// <summary> Gets Stats.</summary>
    /// <remarks> references <see cref="StatsDat"/> on <see cref="Specification.GetStatsDat"/> index.</remarks>
    public required ReadOnlyCollection<int> Stats { get; init; }

    /// <summary> Gets StatsValues.</summary>
    public required ReadOnlyCollection<int> StatsValues { get; init; }

    /// <summary> Gets Points.</summary>
    public required int Points { get; init; }

    /// <summary> Gets HASH16.</summary>
    public required int HASH16 { get; init; }

    /// <summary> Gets Icon.</summary>
    public required string Icon { get; init; }

    /// <summary> Gets IconMaxed.</summary>
    public required string IconMaxed { get; init; }

    /// <summary> Gets SoundEffect.</summary>
    /// <remarks> references <see cref="SoundEffectsDat"/> on <see cref="Specification.GetSoundEffectsDat"/> index.</remarks>
    public required int? SoundEffect { get; init; }

    /// <summary> Gets Unknown88.</summary>
    public required int Unknown88 { get; init; }

    /// <summary> Gets Quest.</summary>
    /// <remarks> references <see cref="QuestDat"/> on <see cref="Specification.GetQuestDat"/> index.</remarks>
    public required int? Quest { get; init; }

    /// <summary>
    /// Gets HellscapePassivesDat data.
    /// </summary>
    /// <param name="dataLoader">data loader.</param>
    /// <returns>array of HellscapePassivesDat.</returns>
    internal static HellscapePassivesDat[] Load(DataLoader dataLoader)
    {
        if (dataLoader is null)
        {
            throw new ArgumentNullException(nameof(dataLoader));
        }

        const string filePath = "Data/HellscapePassives.dat64";
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new HellscapePassivesDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading Id
            (var idLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Name
            (var nameLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Stats
            (var tempstatsLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var statsLoading = tempstatsLoading.AsReadOnly();

            // loading StatsValues
            (var tempstatsvaluesLoading, offset) = SpecificationFileLoader.LoadIntArray(decompressedFile, offset, dataOffset);
            var statsvaluesLoading = tempstatsvaluesLoading.AsReadOnly();

            // loading Points
            (var pointsLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading HASH16
            (var hash16Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Icon
            (var iconLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading IconMaxed
            (var iconmaxedLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading SoundEffect
            (var soundeffectLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading Unknown88
            (var unknown88Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Quest
            (var questLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            if (offset != expectedOffset)
            {
                throw new NotImplementedException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new HellscapePassivesDat()
            {
                Id = idLoading,
                Name = nameLoading,
                Stats = statsLoading,
                StatsValues = statsvaluesLoading,
                Points = pointsLoading,
                HASH16 = hash16Loading,
                Icon = iconLoading,
                IconMaxed = iconmaxedLoading,
                SoundEffect = soundeffectLoading,
                Unknown88 = unknown88Loading,
                Quest = questLoading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
