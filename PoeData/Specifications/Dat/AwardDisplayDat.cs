// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.Dat;

/// <summary>
/// Class containing AwardDisplay.dat data.
/// </summary>
public sealed partial class AwardDisplayDat
{
    /// <summary> Gets Id.</summary>
    public required string Id { get; init; }

    /// <summary> Gets Text.</summary>
    public required string Text { get; init; }

    /// <summary> Gets BackgroundImage.</summary>
    public required string BackgroundImage { get; init; }

    /// <summary> Gets Unknown24.</summary>
    public required int Unknown24 { get; init; }

    /// <summary> Gets Unknown28.</summary>
    public required float Unknown28 { get; init; }

    /// <summary> Gets Unknown32.</summary>
    public required float Unknown32 { get; init; }

    /// <summary> Gets Unknown36.</summary>
    public required string Unknown36 { get; init; }

    /// <summary> Gets Unknown44.</summary>
    public required string Unknown44 { get; init; }

    /// <summary> Gets ForegroundImage.</summary>
    public required string ForegroundImage { get; init; }

    /// <summary> Gets OGGFile.</summary>
    public required string OGGFile { get; init; }

    /// <summary> Gets Unknown68.</summary>
    public required int? Unknown68 { get; init; }

    /// <summary> Gets Unknown84.</summary>
    public required int Unknown84 { get; init; }

    /// <inheritdoc/>
    public static AwardDisplayDat[] Load(Specification specification)
    {
        if (specification is null)
        {
            throw new ArgumentNullException(nameof(specification));
        }

        const string filePath = "Data/AwardDisplay.dat64";
        var decompressedFile = specification.DataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new AwardDisplayDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading Id
            (var idLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Text
            (var textLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading BackgroundImage
            (var backgroundimageLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Unknown24
            (var unknown24Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Unknown28
            (var unknown28Loading, offset) = SpecificationFileLoader.LoadFloat(decompressedFile, offset);

            // loading Unknown32
            (var unknown32Loading, offset) = SpecificationFileLoader.LoadFloat(decompressedFile, offset);

            // loading Unknown36
            (var unknown36Loading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Unknown44
            (var unknown44Loading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading ForegroundImage
            (var foregroundimageLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading OGGFile
            (var oggfileLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Unknown68
            (var unknown68Loading, offset) = SpecificationFileLoader.LoadForeignRowPrimaryKey(decompressedFile, offset, dataOffset);

            // loading Unknown84
            (var unknown84Loading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            if (offset != expectedOffset)
            {
                throw new NotImplementedException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new AwardDisplayDat()
            {
                Id = idLoading,
                Text = textLoading,
                BackgroundImage = backgroundimageLoading,
                Unknown24 = unknown24Loading,
                Unknown28 = unknown28Loading,
                Unknown32 = unknown32Loading,
                Unknown36 = unknown36Loading,
                Unknown44 = unknown44Loading,
                ForegroundImage = foregroundimageLoading,
                OGGFile = oggfileLoading,
                Unknown68 = unknown68Loading,
                Unknown84 = unknown84Loading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
