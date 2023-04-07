// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.Dat;

/// <summary>
/// Class containing AfflictionRewardTypeVisuals.dat data.
/// </summary>
public sealed partial class AfflictionRewardTypeVisualsDat
{
    /// <summary> Gets AfflictionRewardTypes.</summary>
    /// <remarks> references <see cref="AfflictionRewardTypesDat"/> on <see cref="Specification.GetAfflictionRewardTypesDat"/> index.</remarks>
    public required int AfflictionRewardTypes { get; init; }

    /// <summary> Gets Id.</summary>
    public required string Id { get; init; }

    /// <summary> Gets Name.</summary>
    public required string Name { get; init; }

    /// <inheritdoc/>
    public static AfflictionRewardTypeVisualsDat[] Load(Specification specification)
    {
        if (specification is null)
        {
            throw new ArgumentNullException(nameof(specification));
        }

        const string filePath = "Data/AfflictionRewardTypeVisuals.dat64";
        var decompressedFile = specification.DataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new AfflictionRewardTypeVisualsDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading AfflictionRewardTypes
            (var afflictionrewardtypesLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Id
            (var idLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Name
            (var nameLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            if (offset != expectedOffset)
            {
                throw new NotImplementedException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new AfflictionRewardTypeVisualsDat()
            {
                AfflictionRewardTypes = afflictionrewardtypesLoading,
                Id = idLoading,
                Name = nameLoading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
