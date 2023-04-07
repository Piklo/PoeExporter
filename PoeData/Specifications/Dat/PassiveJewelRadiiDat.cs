// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.Dat;

/// <summary>
/// Class containing PassiveJewelRadii.dat data.
/// </summary>
public sealed partial class PassiveJewelRadiiDat
{
    /// <summary> Gets ID.</summary>
    public required string ID { get; init; }

    /// <summary> Gets RingOuterRadius.</summary>
    public required int RingOuterRadius { get; init; }

    /// <summary> Gets RingInnerRadius.</summary>
    public required int RingInnerRadius { get; init; }

    /// <summary> Gets Radius.</summary>
    public required int Radius { get; init; }

    /// <summary>
    /// Gets PassiveJewelRadiiDat data.
    /// </summary>
    /// <param name="dataLoader">data loader.</param>
    /// <returns>array of PassiveJewelRadiiDat.</returns>
    internal static PassiveJewelRadiiDat[] Load(DataLoader dataLoader)
    {
        if (dataLoader is null)
        {
            throw new ArgumentNullException(nameof(dataLoader));
        }

        const string filePath = "Data/PassiveJewelRadii.dat64";
        var decompressedFile = dataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new PassiveJewelRadiiDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading ID
            (var idLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading RingOuterRadius
            (var ringouterradiusLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading RingInnerRadius
            (var ringinnerradiusLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Radius
            (var radiusLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            if (offset != expectedOffset)
            {
                throw new NotImplementedException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new PassiveJewelRadiiDat()
            {
                ID = idLoading,
                RingOuterRadius = ringouterradiusLoading,
                RingInnerRadius = ringinnerradiusLoading,
                Radius = radiusLoading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
