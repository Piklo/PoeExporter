// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.Dat;

/// <summary>
/// Class containing StashTabAffinities.dat data.
/// </summary>
public sealed partial class StashTabAffinitiesDat : IDat<StashTabAffinitiesDat>
{
    /// <summary> Gets SpecializedStash.</summary>
    /// <remarks> references <see cref="StashIdDat"/> on <see cref="Specification.GetStashIdDat"/> index.</remarks>
    public required int SpecializedStash { get; init; }

    /// <summary> Gets Name.</summary>
    public required string Name { get; init; }

    /// <summary> Gets ShowInStashes.</summary>
    /// <remarks> references <see cref="StashIdDat"/> on <see cref="Specification.GetStashIdDat"/> index.</remarks>
    public required ReadOnlyCollection<int> ShowInStashes { get; init; }

    /// <inheritdoc/>
    public static StashTabAffinitiesDat[] Load(Specification specification)
    {
        if (specification is null)
        {
            throw new ArgumentNullException(nameof(specification));
        }

        const string filePath = "Data/StashTabAffinities.dat64";
        var decompressedFile = specification.DataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new StashTabAffinitiesDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading SpecializedStash
            (var specializedstashLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Name
            (var nameLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading ShowInStashes
            (var tempshowinstashesLoading, offset) = SpecificationFileLoader.LoadIntArray(decompressedFile, offset, dataOffset);
            var showinstashesLoading = tempshowinstashesLoading.AsReadOnly();

            if (offset != expectedOffset)
            {
                throw new NotImplementedException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new StashTabAffinitiesDat()
            {
                SpecializedStash = specializedstashLoading,
                Name = nameLoading,
                ShowInStashes = showinstashesLoading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
