// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.DatFiles;

/// <summary>
/// Class containing SigilDisplay.dat data.
/// </summary>
public sealed partial class SigilDisplayDat
{
    /// <summary> Gets Id.</summary>
    public required string Id { get; init; }

    /// <summary> Gets Active_StatsKey.</summary>
    /// <remarks> references <see cref="StatsDat"/> on <see cref="Specification.LoadStatsDat"/> index.</remarks>
    public required int? Active_StatsKey { get; init; }

    /// <summary> Gets Inactive_StatsKey.</summary>
    /// <remarks> references <see cref="StatsDat"/> on <see cref="Specification.LoadStatsDat"/> index.</remarks>
    public required int? Inactive_StatsKey { get; init; }

    /// <summary> Gets DDSFile.</summary>
    public required string DDSFile { get; init; }

    /// <summary> Gets Inactive_ArtFile.</summary>
    public required string Inactive_ArtFile { get; init; }

    /// <summary> Gets Active_ArtFile.</summary>
    public required string Active_ArtFile { get; init; }

    /// <summary> Gets Frame_ArtFile.</summary>
    public required string Frame_ArtFile { get; init; }

    /// <summary>
    /// Gets SigilDisplayDat data.
    /// </summary>
    /// <param name="dataLoader">data loader.</param>
    /// <returns>array of SigilDisplayDat.</returns>
    internal static SigilDisplayDat[] Load(DataLoader dataLoader)
    {
        if (dataLoader is null)
        {
            throw new ArgumentNullException(nameof(dataLoader));
        }

        const string filePath = "Data/SigilDisplay.dat64";
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new SigilDisplayDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading Id
            (var idLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Active_StatsKey
            (var active_statskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading Inactive_StatsKey
            (var inactive_statskeyLoading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading DDSFile
            (var ddsfileLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Inactive_ArtFile
            (var inactive_artfileLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Active_ArtFile
            (var active_artfileLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Frame_ArtFile
            (var frame_artfileLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            if (offset != expectedOffset)
            {
                throw new SchemaMismatchException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new SigilDisplayDat()
            {
                Id = idLoading,
                Active_StatsKey = active_statskeyLoading,
                Inactive_StatsKey = inactive_statskeyLoading,
                DDSFile = ddsfileLoading,
                Inactive_ArtFile = inactive_artfileLoading,
                Active_ArtFile = active_artfileLoading,
                Frame_ArtFile = frame_artfileLoading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
