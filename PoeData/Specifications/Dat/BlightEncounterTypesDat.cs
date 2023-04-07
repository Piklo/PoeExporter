// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.Dat;

/// <summary>
/// Class containing BlightEncounterTypes.dat data.
/// </summary>
public sealed partial class BlightEncounterTypesDat
{
    /// <summary> Gets Id.</summary>
    public required string Id { get; init; }

    /// <summary> Gets Icon.</summary>
    public required string Icon { get; init; }

    /// <summary> Gets a value indicating whether IsGeneric is set.</summary>
    public required bool IsGeneric { get; init; }

    /// <summary> Gets Weight.</summary>
    public required int Weight { get; init; }

    /// <inheritdoc/>
    public static BlightEncounterTypesDat[] Load(Specification specification)
    {
        if (specification is null)
        {
            throw new ArgumentNullException(nameof(specification));
        }

        const string filePath = "Data/BlightEncounterTypes.dat64";
        var decompressedFile = specification.DataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new BlightEncounterTypesDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading Id
            (var idLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Icon
            (var iconLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading IsGeneric
            (var isgenericLoading, offset) = SpecificationFileLoader.LoadBoolean(decompressedFile, offset);

            // loading Weight
            (var weightLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            if (offset != expectedOffset)
            {
                throw new NotImplementedException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new BlightEncounterTypesDat()
            {
                Id = idLoading,
                Icon = iconLoading,
                IsGeneric = isgenericLoading,
                Weight = weightLoading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
