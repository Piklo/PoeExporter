// this file is auto generated
// the generated class is partial, please extend it in another file
#nullable enable

using PoeData.Extensions;
using System.Collections.ObjectModel;
using System.Text;

namespace PoeData.Specifications.Dat;

/// <summary>
/// Class containing LakeMetaOptionsUnlockText.dat data.
/// </summary>
public sealed partial class LakeMetaOptionsUnlockTextDat : IDat<LakeMetaOptionsUnlockTextDat>
{
    /// <summary> Gets Id.</summary>
    public required int Id { get; init; }

    /// <summary> Gets Text.</summary>
    public required string Text { get; init; }

    /// <inheritdoc/>
    public static LakeMetaOptionsUnlockTextDat[] Load(Specification specification)
    {
        if (specification is null)
        {
            throw new ArgumentNullException(nameof(specification));
        }

        const string filePath = "Data/LakeMetaOptionsUnlockText.dat64";
        var decompressedFile = specification.DataLoader.GetFileBytes(filePath);

        var dataOffset = decompressedFile.IndexOfSubArray(Specification.DatFileMagicNumber);
        const int TableOffset = 4;
        var offset = 0;
        (var tableRows, offset) = BitConverterExtended.ToUInt32(decompressedFile, offset);
        var tableLength = dataOffset - TableOffset;
        var tableRecordLength = tableLength / (int)tableRows;

        var objects = new LakeMetaOptionsUnlockTextDat[tableRows];
        for (var rowId = 0; rowId < tableRows; rowId++)
        {
            // offset = 4 + (rowId * tableRecordLength); // debug only
            var expectedOffset = 4 + ((rowId + 1) * tableRecordLength);

            // loading Id
            (var idLoading, offset) = SpecificationFileLoader.LoadInt(decompressedFile, offset);

            // loading Text
            (var textLoading, offset) = SpecificationFileLoader.LoadString(decompressedFile, offset, dataOffset);

            if (offset != expectedOffset)
            {
                throw new NotImplementedException($"offset {offset} != expectedOffset {expectedOffset}");
            }

            var obj = new LakeMetaOptionsUnlockTextDat()
            {
                Id = idLoading,
                Text = textLoading,
            };

            objects[rowId] = obj;
        }

        return objects;
    }
}
