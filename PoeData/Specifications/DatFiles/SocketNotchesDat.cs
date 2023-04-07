// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.DatFiles;

/// <summary>
/// Class containing SocketNotches.dat data.
/// </summary>
public sealed partial class SocketNotchesDat
{
    /// <summary> Gets Id.</summary>
    public required string Id { get; init; }

    /// <summary> Gets Description.</summary>
    public required string Description { get; init; }

    /// <summary> Gets RedSocketImage.</summary>
    public required string RedSocketImage { get; init; }

    /// <summary> Gets BlueSocketImage.</summary>
    public required string BlueSocketImage { get; init; }

    /// <summary> Gets GreenSocketImage.</summary>
    public required string GreenSocketImage { get; init; }

    /// <summary>
    /// Gets SocketNotchesDat data.
    /// </summary>
    /// <param name="dataLoader">data loader.</param>
    /// <returns>array of SocketNotchesDat.</returns>
    internal static SocketNotchesDat[] Load(DataLoader dataLoader)
    {
        if (dataLoader is null)
        {
            throw new ArgumentNullException(nameof(dataLoader));
        }

        const string filePath = "Data/SocketNotches.dat64";
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new SocketNotchesDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading Id
            (var idLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Description
            (var descriptionLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading RedSocketImage
            (var redsocketimageLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading BlueSocketImage
            (var bluesocketimageLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading GreenSocketImage
            (var greensocketimageLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            if (offset != expectedOffset)
            {
                throw new NotImplementedException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new SocketNotchesDat()
            {
                Id = idLoading,
                Description = descriptionLoading,
                RedSocketImage = redsocketimageLoading,
                BlueSocketImage = bluesocketimageLoading,
                GreenSocketImage = greensocketimageLoading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
