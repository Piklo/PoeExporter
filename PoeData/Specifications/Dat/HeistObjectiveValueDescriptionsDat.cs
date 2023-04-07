// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.Dat;

/// <summary>
/// Class containing HeistObjectiveValueDescriptions.dat data.
/// </summary>
public sealed partial class HeistObjectiveValueDescriptionsDat : IDat<HeistObjectiveValueDescriptionsDat>
{
    /// <summary> Gets Tier.</summary>
    public required int Tier { get; init; }

    /// <summary> Gets MarkersMultiply.</summary>
    public required float MarkersMultiply { get; init; }

    /// <summary> Gets Description.</summary>
    public required string Description { get; init; }

    /// <inheritdoc/>
    public static HeistObjectiveValueDescriptionsDat[] Load(Specification specification)
    {
        if (specification is null)
        {
            throw new ArgumentNullException(nameof(specification));
        }

        const string filePath = "Data/HeistObjectiveValueDescriptions.dat64";
        var decompressedFile = specification.DataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new HeistObjectiveValueDescriptionsDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading Tier
            (var tierLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading MarkersMultiply
            (var markersmultiplyLoading, offset) = SpecificationFileLoader.LoadFloat(decompressedFile, offset);

            // loading Description
            (var descriptionLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            if (offset != expectedOffset)
            {
                throw new NotImplementedException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new HeistObjectiveValueDescriptionsDat()
            {
                Tier = tierLoading,
                MarkersMultiply = markersmultiplyLoading,
                Description = descriptionLoading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
