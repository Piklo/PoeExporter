// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.Dat;

/// <summary>
/// Class containing AtlasPrimordialBossOptions.dat data.
/// </summary>
public sealed partial class AtlasPrimordialBossOptionsDat : ISpecificationFile<AtlasPrimordialBossOptionsDat>
{
    /// <summary> Gets Unknown0.</summary>
    public required int Unknown0 { get; init; }

    /// <summary> Gets Unknown4.</summary>
    public required int Unknown4 { get; init; }

    /// <summary> Gets DefaultIcon.</summary>
    public required string DefaultIcon { get; init; }

    /// <summary> Gets HoverIcon.</summary>
    public required string HoverIcon { get; init; }

    /// <summary> Gets HighlightIcon.</summary>
    public required string HighlightIcon { get; init; }

    /// <summary> Gets EmptyIcon.</summary>
    public required string EmptyIcon { get; init; }

    /// <summary> Gets Description.</summary>
    /// <remarks> references <see cref="ClientStringsDat"/> on <see cref="Specification.GetClientStringsDat"/> index.</remarks>
    public required int? Description { get; init; }

    /// <summary> Gets DescriptionActive.</summary>
    /// <remarks> references <see cref="ClientStringsDat"/> on <see cref="Specification.GetClientStringsDat"/> index.</remarks>
    public required int? DescriptionActive { get; init; }

    /// <summary> Gets ProgressTracker.</summary>
    public required string ProgressTracker { get; init; }

    /// <summary> Gets ProgressTrackerFill.</summary>
    public required string ProgressTrackerFill { get; init; }

    /// <summary> Gets Name.</summary>
    public required string Name { get; init; }

    /// <summary> Gets MapDeviceTrackerFill.</summary>
    public required string MapDeviceTrackerFill { get; init; }

    /// <inheritdoc/>
    public static AtlasPrimordialBossOptionsDat[] Load(Specification specification)
    {
        if (specification is null)
        {
            throw new ArgumentNullException(nameof(specification));
        }

        var fileToFind = Encoding.ASCII.GetBytes("Data/AtlasPrimordialBossOptions.dat64");
        var fileRecord = specification.DataLoader.GetFileRecord(fileToFind);
        var decompressedFile = specification.DataLoader.GetFileBytes(fileRecord);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new AtlasPrimordialBossOptionsDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading Unknown0
            (var unknown0Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown4
            (var unknown4Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading DefaultIcon
            (var defaulticonLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading HoverIcon
            (var hovericonLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading HighlightIcon
            (var highlighticonLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading EmptyIcon
            (var emptyiconLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Description
            (var descriptionLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading DescriptionActive
            (var descriptionactiveLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading ProgressTracker
            (var progresstrackerLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading ProgressTrackerFill
            (var progresstrackerfillLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Name
            (var nameLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading MapDeviceTrackerFill
            (var mapdevicetrackerfillLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            if (offset != expectedOffset)
            {
                throw new NotImplementedException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new AtlasPrimordialBossOptionsDat()
            {
                Unknown0 = unknown0Loading,
                Unknown4 = unknown4Loading,
                DefaultIcon = defaulticonLoading,
                HoverIcon = hovericonLoading,
                HighlightIcon = highlighticonLoading,
                EmptyIcon = emptyiconLoading,
                Description = descriptionLoading,
                DescriptionActive = descriptionactiveLoading,
                ProgressTracker = progresstrackerLoading,
                ProgressTrackerFill = progresstrackerfillLoading,
                Name = nameLoading,
                MapDeviceTrackerFill = mapdevicetrackerfillLoading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
