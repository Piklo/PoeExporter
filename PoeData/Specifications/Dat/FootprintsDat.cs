// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.Dat;

/// <summary>
/// Class containing Footprints.dat data.
/// </summary>
public sealed partial class FootprintsDat
{
    /// <summary> Gets Id.</summary>
    public required string Id { get; init; }

    /// <summary> Gets Active_AOFiles.</summary>
    public required ReadOnlyCollection<string> Active_AOFiles { get; init; }

    /// <summary> Gets Idle_AOFiles.</summary>
    public required ReadOnlyCollection<string> Idle_AOFiles { get; init; }

    /// <summary> Gets Unknown40.</summary>
    public required ReadOnlyCollection<string> Unknown40 { get; init; }

    /// <summary> Gets Unknown56.</summary>
    public required ReadOnlyCollection<string> Unknown56 { get; init; }

    /// <summary> Gets Unknown72.</summary>
    public required int Unknown72 { get; init; }

    /// <summary>
    /// Gets FootprintsDat data.
    /// </summary>
    /// <param name="dataLoader">data loader.</param>
    /// <returns>array of FootprintsDat.</returns>
    internal static FootprintsDat[] Load(DataLoader dataLoader)
    {
        if (dataLoader is null)
        {
            throw new ArgumentNullException(nameof(dataLoader));
        }

        const string filePath = "Data/Footprints.dat64";
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new FootprintsDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading Id
            (var idLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Active_AOFiles
            (var tempactive_aofilesLoading, offset) = SpecificationFileLoader.LoadStringArray(decompressedFile, offset, dataOffset);
            var active_aofilesLoading = tempactive_aofilesLoading.AsReadOnly();

            // loading Idle_AOFiles
            (var tempidle_aofilesLoading, offset) = SpecificationFileLoader.LoadStringArray(decompressedFile, offset, dataOffset);
            var idle_aofilesLoading = tempidle_aofilesLoading.AsReadOnly();

            // loading Unknown40
            (var tempunknown40Loading, offset) = SpecificationFileLoader.LoadStringArray(decompressedFile, offset, dataOffset);
            var unknown40Loading = tempunknown40Loading.AsReadOnly();

            // loading Unknown56
            (var tempunknown56Loading, offset) = SpecificationFileLoader.LoadStringArray(decompressedFile, offset, dataOffset);
            var unknown56Loading = tempunknown56Loading.AsReadOnly();

            // loading Unknown72
            (var unknown72Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            if (offset != expectedOffset)
            {
                throw new NotImplementedException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new FootprintsDat()
            {
                Id = idLoading,
                Active_AOFiles = active_aofilesLoading,
                Idle_AOFiles = idle_aofilesLoading,
                Unknown40 = unknown40Loading,
                Unknown56 = unknown56Loading,
                Unknown72 = unknown72Loading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
