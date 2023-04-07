// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.Dat;

/// <summary>
/// Class containing StashType.dat data.
/// </summary>
public sealed partial class StashTypeDat
{
    /// <summary> Gets Id.</summary>
    public required string Id { get; init; }

    /// <summary> Gets IntId.</summary>
    public required int IntId { get; init; }

    /// <summary> Gets Id2.</summary>
    public required string Id2 { get; init; }

    /// <summary> Gets Width.</summary>
    public required int Width { get; init; }

    /// <summary> Gets Height.</summary>
    public required int Height { get; init; }

    /// <summary> Gets Unknown28.</summary>
    public required int Unknown28 { get; init; }

    /// <inheritdoc/>
    public static StashTypeDat[] Load(Specification specification)
    {
        if (specification is null)
        {
            throw new ArgumentNullException(nameof(specification));
        }

        const string filePath = "Data/StashType.dat64";
        var decompressedFile = specification.DataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new StashTypeDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading Id
            (var idLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading IntId
            (var intidLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Id2
            (var id2Loading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Width
            (var widthLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Height
            (var heightLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown28
            (var unknown28Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            if (offset != expectedOffset)
            {
                throw new NotImplementedException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new StashTypeDat()
            {
                Id = idLoading,
                IntId = intidLoading,
                Id2 = id2Loading,
                Width = widthLoading,
                Height = heightLoading,
                Unknown28 = unknown28Loading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
