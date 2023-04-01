// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.SpecificationFiles;

/// <summary>
/// Class containing QuestItems.dat data.
/// </summary>
public sealed partial class QuestItemsDat : ISpecificationFile<QuestItemsDat>
{
    /// <summary> Gets Item.</summary>
    public required int? Item { get; init; }

    /// <summary> Gets Unknown16.</summary>
    public required int? Unknown16 { get; init; }

    /// <summary> Gets Unknown32.</summary>
    public required int? Unknown32 { get; init; }

    /// <summary> Gets Unknown48.</summary>
    public required int Unknown48 { get; init; }

    /// <summary> Gets Unknown52.</summary>
    public required int? Unknown52 { get; init; }

    /// <summary> Gets Unknown68.</summary>
    public required ReadOnlyCollection<int> Unknown68 { get; init; }

    /// <summary> Gets a value indicating whether Unknown84 is set.</summary>
    public required bool Unknown84 { get; init; }

    /// <summary> Gets a value indicating whether Unknown85 is set.</summary>
    public required bool Unknown85 { get; init; }

    /// <summary> Gets Unknown86.</summary>
    public required int? Unknown86 { get; init; }

    /// <summary> Gets Unknown102.</summary>
    public required int Unknown102 { get; init; }

    /// <summary> Gets Unknown106.</summary>
    public required int? Unknown106 { get; init; }

    /// <summary> Gets Script.</summary>
    public required string Script { get; init; }

    /// <summary> Gets Unknown130.</summary>
    public required int? Unknown130 { get; init; }

    /// <inheritdoc/>
    public static QuestItemsDat[] Load(Specification specification)
    {
        if (specification is null)
        {
            throw new ArgumentNullException(nameof(specification));
        }

        var fileToFind = Encoding.ASCII.GetBytes("Data/QuestItems.dat64");
        var fileRecord = specification.DataLoader.GetFileRecord(fileToFind);
        var decompressedFile = specification.DataLoader.GetFileBytes(fileRecord);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new QuestItemsDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading referenced tables if any
            // specification.GetBaseItemTypesDat();

            // loading Item
            (var itemLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading Unknown16
            (var unknown16Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading Unknown32
            (var unknown32Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading Unknown48
            (var unknown48Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown52
            (var unknown52Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading Unknown68
            (var tempunknown68Loading, offset) = SpecificationFileLoader.LoadIntArray(decompressedFile, offset, dataOffset);
            var unknown68Loading = tempunknown68Loading.AsReadOnly();

            // loading Unknown84
            (var unknown84Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown85
            (var unknown85Loading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Unknown86
            (var unknown86Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading Unknown102
            (var unknown102Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown106
            (var unknown106Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading Script
            (var scriptLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Unknown130
            (var unknown130Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            if (offset != expectedOffset)
            {
                throw new NotImplementedException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new QuestItemsDat()
            {
                Item = itemLoading,
                Unknown16 = unknown16Loading,
                Unknown32 = unknown32Loading,
                Unknown48 = unknown48Loading,
                Unknown52 = unknown52Loading,
                Unknown68 = unknown68Loading,
                Unknown84 = unknown84Loading,
                Unknown85 = unknown85Loading,
                Unknown86 = unknown86Loading,
                Unknown102 = unknown102Loading,
                Unknown106 = unknown106Loading,
                Script = scriptLoading,
                Unknown130 = unknown130Loading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
