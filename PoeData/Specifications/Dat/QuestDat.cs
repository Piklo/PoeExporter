// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.Dat;

/// <summary>
/// Class containing Quest.dat data.
/// </summary>
public sealed partial class QuestDat
{
    /// <summary> Gets Id.</summary>
    public required string Id { get; init; }

    /// <summary> Gets Act.</summary>
    public required int Act { get; init; }

    /// <summary> Gets Name.</summary>
    public required string Name { get; init; }

    /// <summary> Gets Icon_DDSFile.</summary>
    public required string Icon_DDSFile { get; init; }

    /// <summary> Gets QuestId.</summary>
    public required int QuestId { get; init; }

    /// <summary> Gets a value indicating whether Unknown32 is set.</summary>
    public required bool Unknown32 { get; init; }

    /// <summary> Gets Type.</summary>
    /// <remarks> references <see cref="QuestTypeDat"/> on <see cref="Specification.GetQuestTypeDat"/> index.</remarks>
    public required int? Type { get; init; }

    /// <summary> Gets Unknown49.</summary>
    public required ReadOnlyCollection<int> Unknown49 { get; init; }

    /// <summary> Gets Unknown65.</summary>
    public required int Unknown65 { get; init; }

    /// <summary> Gets TrackerGroup.</summary>
    /// <remarks> references <see cref="QuestTrackerGroupDat"/> on <see cref="Specification.GetQuestTrackerGroupDat"/> index.</remarks>
    public required int? TrackerGroup { get; init; }

    /// <inheritdoc/>
    public static QuestDat[] Load(Specification specification)
    {
        if (specification is null)
        {
            throw new ArgumentNullException(nameof(specification));
        }

        const string filePath = "Data/Quest.dat64";
        var decompressedFile = specification.DataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new QuestDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading Id
            (var idLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Act
            (var actLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Name
            (var nameLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Icon_DDSFile
            (var icon_ddsfileLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading QuestId
            (var questidLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown32
            (var unknown32Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Type
            (var typeLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading Unknown49
            (var tempunknown49Loading, offset) = SpecificationFileLoader.LoadIntArray(decompressedFile, offset, dataOffset);
            var unknown49Loading = tempunknown49Loading.AsReadOnly();

            // loading Unknown65
            (var unknown65Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading TrackerGroup
            (var trackergroupLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            if (offset != expectedOffset)
            {
                throw new NotImplementedException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new QuestDat()
            {
                Id = idLoading,
                Act = actLoading,
                Name = nameLoading,
                Icon_DDSFile = icon_ddsfileLoading,
                QuestId = questidLoading,
                Unknown32 = unknown32Loading,
                Type = typeLoading,
                Unknown49 = unknown49Loading,
                Unknown65 = unknown65Loading,
                TrackerGroup = trackergroupLoading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
