// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.Dat;

/// <summary>
/// Class containing MapSeries.dat data.
/// </summary>
public sealed partial class MapSeriesDat : ISpecificationFile<MapSeriesDat>
{
    /// <summary> Gets Id.</summary>
    public required string Id { get; init; }

    /// <summary> Gets Name.</summary>
    public required string Name { get; init; }

    /// <summary> Gets BaseIcon_DDSFile.</summary>
    public required string BaseIcon_DDSFile { get; init; }

    /// <summary> Gets Infected_DDSFile.</summary>
    public required string Infected_DDSFile { get; init; }

    /// <summary> Gets Shaper_DDSFile.</summary>
    public required string Shaper_DDSFile { get; init; }

    /// <summary> Gets Elder_DDSFile.</summary>
    public required string Elder_DDSFile { get; init; }

    /// <summary> Gets Drawn_DDSFile.</summary>
    public required string Drawn_DDSFile { get; init; }

    /// <summary> Gets Delirious_DDSFile.</summary>
    public required string Delirious_DDSFile { get; init; }

    /// <summary> Gets UberBlight_DDSFile.</summary>
    public required string UberBlight_DDSFile { get; init; }

    /// <inheritdoc/>
    public static MapSeriesDat[] Load(Specification specification)
    {
        if (specification is null)
        {
            throw new ArgumentNullException(nameof(specification));
        }

        var fileToFind = Encoding.ASCII.GetBytes("Data/MapSeries.dat64");
        var fileRecord = specification.DataLoader.GetFileRecord(fileToFind);
        var decompressedFile = specification.DataLoader.GetFileBytes(fileRecord);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new MapSeriesDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading Id
            (var idLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Name
            (var nameLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading BaseIcon_DDSFile
            (var baseicon_ddsfileLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Infected_DDSFile
            (var infected_ddsfileLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Shaper_DDSFile
            (var shaper_ddsfileLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Elder_DDSFile
            (var elder_ddsfileLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Drawn_DDSFile
            (var drawn_ddsfileLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Delirious_DDSFile
            (var delirious_ddsfileLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading UberBlight_DDSFile
            (var uberblight_ddsfileLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            if (offset != expectedOffset)
            {
                throw new NotImplementedException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new MapSeriesDat()
            {
                Id = idLoading,
                Name = nameLoading,
                BaseIcon_DDSFile = baseicon_ddsfileLoading,
                Infected_DDSFile = infected_ddsfileLoading,
                Shaper_DDSFile = shaper_ddsfileLoading,
                Elder_DDSFile = elder_ddsfileLoading,
                Drawn_DDSFile = drawn_ddsfileLoading,
                Delirious_DDSFile = delirious_ddsfileLoading,
                UberBlight_DDSFile = uberblight_ddsfileLoading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
