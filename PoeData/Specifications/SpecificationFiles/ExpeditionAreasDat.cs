// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.SpecificationFiles;

/// <summary>
/// Class containing ExpeditionAreas.dat data.
/// </summary>
public sealed partial class ExpeditionAreasDat : ISpecificationFile<ExpeditionAreasDat>
{
    /// <summary> Gets Area.</summary>
    public required int? Area { get; init; }

    /// <summary> Gets PosX.</summary>
    public required int PosX { get; init; }

    /// <summary> Gets PosY.</summary>
    public required int PosY { get; init; }

    /// <summary> Gets Tags.</summary>
    public required ReadOnlyCollection<int> Tags { get; init; }

    /// <summary> Gets Unknown40.</summary>
    public required ReadOnlyCollection<int> Unknown40 { get; init; }

    /// <summary> Gets a value indicating whether Unknown56 is set.</summary>
    public required bool Unknown56 { get; init; }

    /// <summary> Gets TextAudio.</summary>
    public required int? TextAudio { get; init; }

    /// <summary> Gets CompletionAchievements.</summary>
    public required ReadOnlyCollection<int> CompletionAchievements { get; init; }

    /// <inheritdoc/>
    public static ExpeditionAreasDat[] Load(Specification specification)
    {
        if (specification is null)
        {
            throw new ArgumentNullException(nameof(specification));
        }

        var fileToFind = Encoding.ASCII.GetBytes("Data/ExpeditionAreas.dat64");
        var fileRecord = specification.DataLoader.GetFileRecord(fileToFind);
        var decompressedFile = specification.DataLoader.GetFileBytes(fileRecord);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new ExpeditionAreasDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading referenced tables if any
            // specification.GetWorldAreasDat();
            // specification.GetTagsDat();
            // specification.GetNPCTextAudioDat();
            // specification.GetAchievementItemsDat();

            // loading Area
            (var areaLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading PosX
            (var posxLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading PosY
            (var posyLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Tags
            (var temptagsLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var tagsLoading = temptagsLoading.AsReadOnly();

            // loading Unknown40
            (var tempunknown40Loading, offset) = SpecificationFileLoader.LoadIntArray(decompressedFile, offset, dataOffset);
            var unknown40Loading = tempunknown40Loading.AsReadOnly();

            // loading Unknown56
            (var unknown56Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading TextAudio
            (var textaudioLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading CompletionAchievements
            (var tempcompletionachievementsLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKeys(decompressedFile, offset, dataOffset);
            var completionachievementsLoading = tempcompletionachievementsLoading.AsReadOnly();

            if (offset != expectedOffset)
            {
                throw new NotImplementedException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new ExpeditionAreasDat()
            {
                Area = areaLoading,
                PosX = posxLoading,
                PosY = posyLoading,
                Tags = tagsLoading,
                Unknown40 = unknown40Loading,
                Unknown56 = unknown56Loading,
                TextAudio = textaudioLoading,
                CompletionAchievements = completionachievementsLoading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
