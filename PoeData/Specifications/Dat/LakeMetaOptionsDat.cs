// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.Dat;

/// <summary>
/// Class containing LakeMetaOptions.dat data.
/// </summary>
public sealed partial class LakeMetaOptionsDat : IDat<LakeMetaOptionsDat>
{
    /// <summary> Gets Id.</summary>
    public required string Id { get; init; }

    /// <summary> Gets Unknown8.</summary>
    public required ReadOnlyCollection<int> Unknown8 { get; init; }

    /// <inheritdoc/>
    public static LakeMetaOptionsDat[] Load(Specification specification)
    {
        if (specification is null)
        {
            throw new ArgumentNullException(nameof(specification));
        }

        const string filePath = "Data/LakeMetaOptions.dat64";
        var decompressedFile = specification.DataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new LakeMetaOptionsDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading Id
            (var idLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            // loading Unknown8
            (var tempunknown8Loading, offset) = SpecificationFileLoader.LoadIntArray(decompressedFile, offset, dataOffset);
            var unknown8Loading = tempunknown8Loading.AsReadOnly();

            if (offset != expectedOffset)
            {
                throw new NotImplementedException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new LakeMetaOptionsDat()
            {
                Id = idLoading,
                Unknown8 = unknown8Loading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
